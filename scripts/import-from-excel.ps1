<#
.SYNOPSIS
    Imports data from schema/data.xlsx into AgenticCommerceDB.
    Clears all user tables first, then inserts Excel data with FK constraints disabled.
#>
param(
    [string]$Server   = "localhost,1433",
    [string]$Database = "AgenticCommerceDB",
    [string]$User     = "sa",
    [string]$Password = ""
)

$ErrorActionPreference = "Stop"

if (-not $Password) { $Password = $env:SQL_SA_PASSWORD }
if (-not $Password) { $Password = Read-Host "SQL password for $User@$Server" }

Add-Type -AssemblyName System.Data

$root = Split-Path -Parent $PSScriptRoot
$xlsxPath = Join-Path $root "schema\data.xlsx"
$tempPath = Join-Path $env:TEMP "seed_data_update.xlsx"
if (Test-Path $tempPath) {
    $xlsxPath = $tempPath
    Write-Host "Using temp Excel: $tempPath"
} elseif (-not (Test-Path $xlsxPath)) {
    Write-Host "File not found: $xlsxPath"; exit 1
}

$connString = "Server=$Server;Database=$Database;User Id=$User;Password=$Password;TrustServerCertificate=True;"
$conn = New-Object System.Data.SqlClient.SqlConnection $connString
$conn.Open()
Write-Host "Connected to $Database on $Server"

$excel = New-Object -ComObject Excel.Application
$excel.Visible = $false
$excel.DisplayAlerts = $false
$wb = $excel.Workbooks.Open($xlsxPath)

function Invoke-Sql([string]$sql) {
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = $sql
    $cmd.ExecuteNonQuery() | Out-Null
}

function Get-TableColumns([string]$table) {
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = "SELECT c.name, TYPE_NAME(c.user_type_id) AS typ, CASE WHEN c.is_identity=1 THEN 1 ELSE 0 END AS is_id, c.is_nullable FROM sys.columns c WHERE c.object_id = OBJECT_ID('$table') ORDER BY c.column_id"
    $r = $cmd.ExecuteReader()
    $cols = New-Object System.Collections.ArrayList
    while ($r.Read()) {
        [void]$cols.Add([pscustomobject]@{
            Name = $r.GetString(0); Type = $r.GetString(1); IsId = ($r.GetInt32(2) -eq 1); Nullable = $r.GetBoolean(3)
        })
    }
    $r.Close()
    return @($cols)
}

# Disable FK constraints
Write-Host "Disabling FK constraints..."
Invoke-Sql "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'"

# Delete all data from user tables (respecting FK order by disabling first)
Write-Host "Clearing existing data..."
Invoke-Sql "EXEC sp_MSforeachtable 'DELETE FROM ?'"

$imported = 0
$skipped = 0

foreach ($ws in $wb.Worksheets) {
    $tableName = $ws.Name
    $usedRows = $ws.UsedRange.Rows.Count
    $usedCols = $ws.UsedRange.Columns.Count

    if ($usedRows -le 1 -or $usedCols -eq 0) {
        $skipped++
        continue
    }

    # Check if table exists in database
    $checkCmd = $conn.CreateCommand()
    $checkCmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='$tableName' AND TABLE_TYPE='BASE TABLE'"
    $exists = ($checkCmd.ExecuteScalar() -gt 0)
    if (-not $exists) {
        Write-Host "  SKIP $tableName (table not in database)"
        $skipped++
        continue
    }

    # Read headers from Excel row 1
    $headers = @()
    for ($c = 1; $c -le $usedCols; $c++) {
        $val = $ws.Cells.Item(1, $c).Text
        if ($val) { $headers += $val.Trim() }
    }

    # Get database columns
    $dbCols = @(Get-TableColumns $tableName)
    $dbColNames = $dbCols | ForEach-Object { $_.Name }

    # Find identity column
    $idCol = $dbCols | Where-Object { $_.IsId } | Select-Object -First 1

    # Match Excel headers to DB columns
    $matchedCols = @()
    foreach ($h in $headers) {
        $idx = [Array]::IndexOf($dbColNames, $h)
        if ($idx -ge 0) { $matchedCols += @{ ExcelCol = [Array]::IndexOf($headers, $h) + 1; DbCol = $dbCols[$idx] } }
    }

    if ($matchedCols.Count -eq 0) {
        Write-Host "  SKIP $tableName (no matching columns)"
        $skipped++
        continue
    }

    # Build column list
    $colNames = ($matchedCols | ForEach-Object { "[$($_.DbCol.Name)]" }) -join ", "
    if ($idCol -and ($matchedCols | Where-Object { $_.DbCol.Name -eq $idCol.Name })) {
        Invoke-Sql "SET IDENTITY_INSERT [$tableName] ON"
    }

    $inserted = 0
    for ($r = 2; $r -le $usedRows; $r++) {
        $rowHasData = $false
        $vals = @()
        foreach ($mc in $matchedCols) {
            $cell = $ws.Cells.Item($r, $mc.ExcelCol)
            $cellVal = $cell.Value2
            $dbCol = $mc.DbCol

            if ($null -eq $cellVal -or $cellVal -eq "") {
                if ($dbCol.Nullable) {
                    $vals += "NULL"
                } else {
                    # Default values for non-nullable columns
                    switch -Regex ($dbCol.Type) {
                        '(?i)^(int|bigint|smallint|tinyint)$' { $vals += "0" }
                        '(?i)^(decimal|numeric|money|smallmoney|float|real)$' { $vals += "0" }
                        '(?i)^(bit)$' { $vals += "0" }
                        '(?i)^(datetime|datetime2|datetimeoffset|smalldatetime|date|time)$' { $vals += "GETUTCDATE()" }
                        '(?i)^(uniqueidentifier)$' { $vals += "NEWID()" }
                        default { $vals += "''" }
                    }
                }
                continue
            }
            $rowHasData = $true

            switch -Regex ($dbCol.Type) {
                '(?i)^(bit)$' { $vals += ([int][bool]$cellVal) }
                '(?i)^(int|bigint|smallint|tinyint)$' { $vals += [math]::Floor([double]$cellVal) }
                '(?i)^(decimal|numeric|money|smallmoney)$' { $vals += [string][decimal]$cellVal }
                '(?i)^(float|real)$' { $vals += [string][double]$cellVal }
                '(?i)^(uniqueidentifier)$' { $vals += "'" + [string]$cellVal + "'" }
                '(?i)^(datetime|datetime2|datetimeoffset|smalldatetime|date|time)$' {
                    try {
                        $dt = [DateTime]::FromOADate($cellVal)
                        $vals += "'" + $dt.ToString("yyyy-MM-dd HH:mm:ss.fffffff") + "'"
                    } catch {
                        $vals += "'" + [string]$cellVal + "'"
                    }
                }
                default {
                    $text = [string]$cellVal
                    $vals += "'" + $text.Replace("'", "''") + "'"
                }
            }
        }

        if ($rowHasData) {
            $sql = "INSERT INTO [$tableName] ($colNames) VALUES ($($vals -join ', '))"
            try {
                Invoke-Sql $sql
                $inserted++
            } catch {
                Write-Host "  ERROR on $tableName row $r : $($_.Exception.InnerException.Message)"
            }
        }
    }

    if ($idCol -and ($matchedCols | Where-Object { $_.DbCol.Name -eq $idCol.Name })) {
        Invoke-Sql "SET IDENTITY_INSERT [$tableName] OFF"
    }

    Write-Host "  OK $tableName - $inserted rows inserted"
    $imported++
}

# Re-enable FK constraints
Write-Host "Re-enabling FK constraints..."
try {
    Invoke-Sql "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'"
} catch {
    Write-Host "  WARNING: Some FK constraints could not be validated. This may be due to missing reference data."
}

$wb.Close($false)
$excel.Quit()
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($wb) | Out-Null
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($excel) | Out-Null

$conn.Close()
Write-Host "Done. Imported $imported tables, skipped $skipped."
