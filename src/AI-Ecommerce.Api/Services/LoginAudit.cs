using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models.Masters;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// Writes a login-audit row (EmployeeLogTable / CustomerLogTable) on every
    /// successful login. The schema already existed — this is the missing
    /// wiring that was flagged in FutureScope.md.
    /// </summary>
    public static class LoginAudit
    {
        public static async Task RecordAsync(
            ApplicationDbContext db,
            HttpContext http,
            long? customerId,
            long? employeeId,
            string? token)
        {
            var now = DateTime.UtcNow;
            var info = UserAgentInfo.Parse(http.Request.Headers.UserAgent.ToString());

            if (customerId.HasValue)
            {
                db.CustomerLogs.Add(new CustomerLogTable
                {
                    CustomerId = customerId,
                    Token = token,
                    LogDateTime = now,
                    LogTime = now.TimeOfDay,
                    IPAddress = http.Connection.RemoteIpAddress?.ToString(),
                    CompName = info.CompName,
                    MacAddress = null,
                    GeoLocation = null,
                    Latitude = null,
                    Longitude = null,
                    OSFamily = info.OSFamily,
                    OSVersion = info.OSVersion,
                    BrowserFamily = info.BrowserFamily,
                    BrowserVersion = info.BrowserVersion
                });
            }

            if (employeeId.HasValue)
            {
                db.EmployeeLogs.Add(new EmployeeLogTable
                {
                    EmployeeId = employeeId,
                    Token = token,
                    LogDateTime = now,
                    LogTime = now.TimeOfDay,
                    IPAddress = http.Connection.RemoteIpAddress?.ToString(),
                    CompName = info.CompName,
                    MacAddress = null,
                    GeoLocation = null,
                    Latitude = null,
                    Longitude = null,
                    OSFamily = info.OSFamily,
                    OSVersion = info.OSVersion,
                    BrowserFamily = info.BrowserFamily,
                    BrowserVersion = info.BrowserVersion
                });
            }

            await db.SaveChangesAsync();
        }
    }

    /// <summary>Minimal User-Agent parser — extracts browser and OS family/version.</summary>
    public static class UserAgentInfo
    {
        public static (string CompName, string OSFamily, string OSVersion, string BrowserFamily, string BrowserVersion) Parse(string userAgent)
        {
            var ua = userAgent ?? string.Empty;
            var compName = "Unknown";
            var osFamily = "Unknown";
            var osVersion = "";
            var browserFamily = "Unknown";
            var browserVersion = "";

            // OS family/version
            if (ua.Contains("Windows", StringComparison.OrdinalIgnoreCase))
            {
                osFamily = "Windows";
                osVersion = MatchVersion(ua, @"Windows NT ([\d.]+)") switch
                {
                    "10.0" => "10/11",
                    var v => v ?? ""
                };
            }
            else if (ua.Contains("Android", StringComparison.OrdinalIgnoreCase))
            {
                osFamily = "Android";
                osVersion = MatchVersion(ua, @"Android ([\d.]+)");
            }
            else if (ua.Contains("iPhone", StringComparison.OrdinalIgnoreCase) || ua.Contains("iPad", StringComparison.OrdinalIgnoreCase))
            {
                osFamily = ua.Contains("iPad", StringComparison.OrdinalIgnoreCase) ? "iPadOS" : "iOS";
                osVersion = MatchVersion(ua, @"OS ([\d_]+)");
                osVersion = osVersion.Replace('_', '.');
            }
            else if (ua.Contains("Mac OS X", StringComparison.OrdinalIgnoreCase) || ua.Contains("Macintosh", StringComparison.OrdinalIgnoreCase))
            {
                osFamily = "macOS";
                osVersion = MatchVersion(ua, @"Mac OS X ([\d_]+)")?.Replace('_', '.');
            }
            else if (ua.Contains("Linux", StringComparison.OrdinalIgnoreCase))
            {
                osFamily = "Linux";
            }

            // Browser family/version
            if (ua.Contains("Edg/", StringComparison.OrdinalIgnoreCase)) { browserFamily = "Edge"; browserVersion = MatchVersion(ua, @"Edg/([\d.]+)") ?? ""; }
            else if (ua.Contains("OPR/", StringComparison.OrdinalIgnoreCase)) { browserFamily = "Opera"; browserVersion = MatchVersion(ua, @"OPR/([\d.]+)") ?? ""; }
            else if (ua.Contains("Chrome/", StringComparison.OrdinalIgnoreCase)) { browserFamily = "Chrome"; browserVersion = MatchVersion(ua, @"Chrome/([\d.]+)") ?? ""; }
            else if (ua.Contains("Firefox/", StringComparison.OrdinalIgnoreCase)) { browserFamily = "Firefox"; browserVersion = MatchVersion(ua, @"Firefox/([\d.]+)") ?? ""; }
            else if (ua.Contains("Safari/", StringComparison.OrdinalIgnoreCase)) { browserFamily = "Safari"; browserVersion = MatchVersion(ua, @"Safari/([\d.]+)") ?? ""; }

            return (compName, osFamily, osVersion, browserFamily, browserVersion);
        }

        private static string? MatchVersion(string input, string pattern)
        {
            var m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            return m.Success ? m.Groups[1].Value : null;
        }
    }
}