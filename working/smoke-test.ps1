$ErrorActionPreference = "Stop"
$base = "http://localhost:5015/api"
$secret = (Get-Content "$PSScriptRoot\..\.env" | Where-Object { $_ -match '^JWT_SECRET=' }) -replace '^JWT_SECRET=',''

function B64Url([byte[]]$bytes) { return [Convert]::ToBase64String($bytes).TrimEnd('=').Replace('+','-').Replace('/','_') }
function New-Jwt {
  param($sub, $accountType, $userTypeId)
  $now = [DateTimeOffset]::UtcNow
  $payload = @{ sub = "$sub"; nameid = "$sub"; email = "test@example.com"; AccountType = $accountType; UserTypeId = "$userTypeId"; jti = [guid]::NewGuid().ToString(); iss = "AgenticCommerce"; aud = "AgenticCommerceUsers"; exp = $now.AddHours(1).ToUnixTimeSeconds(); nbf = $now.AddSeconds(-30).ToUnixTimeSeconds() } | ConvertTo-Json -Compress
  $hb = B64Url ([Text.Encoding]::UTF8.GetBytes('{"alg":"HS256","typ":"JWT"}'))
  $pb = B64Url ([Text.Encoding]::UTF8.GetBytes($payload))
  $hmac = New-Object System.Security.Cryptography.HMACSHA256
  $hmac.Key = [Text.Encoding]::UTF8.GetBytes($secret)
  $sig = B64Url ($hmac.ComputeHash([Text.Encoding]::UTF8.GetBytes("$hb.$pb")))
  return "$hb.$pb.$sig"
}

function Unwrap {
  param($node, $seen)
  if ($node -is [System.Array]) { return @($node | ForEach-Object { Unwrap $_ $seen }) }
  if ($null -eq $node -or $node -isnot [System.Management.Automation.PSCustomObject]) { return $node }
  if ($node.PSObject.Properties.Name -contains '$ref') { return $seen[[string]$node.'$ref'] }
  if ($node.PSObject.Properties.Name -contains '$values') {
    $arr = @($node.'$values' | ForEach-Object { Unwrap $_ $seen })
    if ($node.PSObject.Properties.Name -contains '$id') { $seen[[string]$node.'$id'] = $arr }
    return $arr
  }
  $out = @{}
  if ($node.PSObject.Properties.Name -contains '$id') { $seen[[string]$node.'$id'] = $out }
  foreach ($p in $node.PSObject.Properties) { if ($p.Name -ne '$id') { $out[$p.Name] = Unwrap $p.Value $seen } }
  return [PSCustomObject]$out
}

function Call-Api {
  param($Method, $Path, $Token, $Body)
  $h = @{}
  if ($Token) { $h.Authorization = "Bearer $Token" }
  $p = @{ UseBasicParsing = $true; Method = $Method; Uri = "$base$Path"; Headers = $h }
  if ($Body) { $p.ContentType = 'application/json'; $p.Body = ($Body | ConvertTo-Json -Compress) }
  $r = Invoke-WebRequest @p
  return Unwrap ($r.Content | ConvertFrom-Json) @{}
}

function Check {
  param($Name, $Cond, $Detail)
  if ($Cond) { Write-Output "PASS: $Name" } else { Write-Output "FAIL: $Name -> $Detail" }
}

Write-Output "=== A. Customer login ==="
$login = Call-Api Post "/auth/login" $null @{ email = "demo@example.com"; password = "Demo@1234" }
$tok = $login.token
Check "login accountType=Customer" ($login.accountType -eq "Customer") $login

Write-Output "=== B. Catalog ==="
$cat = Call-Api Get "/catalog" $tok $null
Check "catalog has 3 products" ($cat.Count -eq 3) "count=$($cat.Count)"
Check "laptop avail=48" ($cat[0].availableQuantity -eq 48) "avail=$($cat[0].availableQuantity)"

Write-Output "=== C. Fresh cart flow ==="
$add1 = Call-Api Post "/cart/items" $tok @{ productId = 1; quantity = 2 }
Check "add laptop OK" ($null -ne $add1.message) $add1
$add2 = Call-Api Post "/cart/items" $tok @{ productId = 3; quantity = 1 }
Check "add headphones OK" ($null -ne $add2.message) $add2
$cart = Call-Api Get "/cart" $tok $null
Check "cart count=3 total=2089.97" ($cart.count -eq 3 -and $cart.total -eq 2089.97) "count=$($cart.count) total=$($cart.total)"
Check "cart items length=2" ($cart.items.Count -eq 2) "items=$($cart.items.Count)"

Write-Output "=== D. Checkout UPI ==="
$co = Call-Api Post "/cart/checkout" $tok @{ paymentMethod = "UPI"; paymentReference = "UPI-001" }
$oid = $co.salesOrderId
Check "order created with id" ($null -ne $oid) $co
Check "order total=2298.967" ($co.totalAmount -eq 2298.967) "total=$($co.totalAmount)"
Check "paymentStatus=Pending" ($co.paymentStatus -eq "Pending") $co.paymentStatus

Write-Output "=== E. Order tracking (customer) ==="
$orders = Call-Api Get "/sales-orders" $tok $null
Check "customer sees 2 orders" ($orders.Count -eq 2) "count=$($orders.Count)"
$last = $orders | Where-Object { $_.salesOrderId -eq $oid }
Check "latest order status=Placed items=2" ($last.orderStatus -eq "Placed" -and $last.Items.Count -eq 2) "status=$($last.orderStatus) items=$($last.Items.Count)"

Write-Output "=== F. Employee: status update ==="
$emp = New-Jwt -sub 1 -accountType "Employee" -userTypeId 1
$st = Call-Api Patch "/sales-orders/$oid/status" $emp @{ status = "Processing" }
Check "status -> Processing" ($st.orderStatus -eq "Processing") $st
$st2 = Call-Api Patch "/sales-orders/$oid/status" $emp @{ status = "Shipped" }
Check "status -> Shipped (shippedDate set)" ($st2.orderStatus -eq "Shipped") $st2
$all = Call-Api Get "/sales-orders/all" $emp $null
Check "employee sees all orders (2)" ($all.Count -eq 2) "count=$($all.Count)"

Write-Output "=== G. Dashboard ==="
$dash = Call-Api Get "/dashboard/summary" $emp $null
Check "counts.products=3" ($dash.counts.products -eq 3) $dash
Check "openOrders=2" ($dash.counts.openOrders -eq 2) "openOrders=$($dash.counts.openOrders)"
Check "pendingPayments=2" ($dash.counts.pendingPayments -eq 2) "pendingPayments=$($dash.counts.pendingPayments)"

Write-Output "=== H. Master CRUD (customer-master) ==="
$list = Call-Api Get "/customer-master" $emp $null
$before = $list.Count
$newC = Call-Api Post "/customer-master" $emp @{ firstName = "Smoke"; lastName = "Test"; email = "smoke$([guid]::NewGuid().ToString().Substring(0,6))@test.com"; phoneNumber = "9999999999"; password = "Smoke@1234"; userTypeId = 3 }
Check "create customer id" ($null -ne $newC.customerId) $newC
$updC = Call-Api Put "/customer-master/$($newC.customerId)" $emp @{ firstName = "Smoke2"; lastName = "Test"; email = $newC.email; phoneNumber = "9999999999"; userTypeId = 3 }
Check "update firstName" ($updC.firstName -eq "Smoke2") $updC
Call-Api Delete "/customer-master/$($newC.customerId)" $emp $null | Out-Null
$list2 = Call-Api Get "/customer-master" $emp $null
Check "soft delete hides row" ($list2.Count -eq $before) "before=$before after=$($list2.Count)"

Write-Output "=== I. Profile ==="
$prof = Call-Api Get "/profile" $tok $null
Check "profile city=Mumbai" ($prof.city -eq "Mumbai") $prof.city
$upd = Call-Api Put "/profile" $tok @{ email = "demo@example.com"; firstName = "Demo"; lastName = "Customer"; phoneNumber = "9876543210"; addressLine = "1 Demo Street"; city = "Pune"; state = "Maharashtra"; country = "India"; pincode = "411001" }
Check "profile update" ($upd.message -eq "Profile updated.") $upd

Write-Output "ALL CHECKS DONE"