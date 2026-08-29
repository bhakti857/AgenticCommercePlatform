-- ============================================================
-- Page: Login
-- Route: /login
-- Access: Public (Customer or Employee)
-- API: POST /api/auth/login
-- Status: BUILT
-- Tables used: CustomerMaster (SELECT), EmployeeMaster (SELECT),
--              CustomerLogTable (INSERT - planned), EmployeeLogTable (INSERT - planned)
-- ============================================================

-- 1. Try the customer account first (AuthController.Login)
SELECT CustomerId, Email, PasswordHash, FirstName, LastName, IsActive
FROM   CustomerMaster
WHERE  Email = @Email;

-- 2. If no customer row matched, try the employee account
SELECT EmployeeId, Email, PasswordHash, FirstName, LastName,
       DepartmentId, UserTypeId, IsActive
FROM   EmployeeMaster
WHERE  Email = @Email;

-- 3. On success, a JWT is issued (JwtService.GenerateToken).
--    PLAINED - not wired up yet: write a login-audit row on every success.
INSERT INTO CustomerLogTable (CustomerId, Token, LogDateTime, LogTime,
                              IPAddress, CompName, MacAddress, GeoLocation,
                              Latitude, Longitude, OSFamily, OSVersion,
                              BrowserFamily, BrowserVersion)
VALUES (@CustomerId, @Token, SYSUTCDATETIME(), CONVERT(time(7), SYSUTCDATETIME()),
        @IPAddress, @CompName, @MacAddress, @GeoLocation,
        @Latitude, @Longitude, @OSFamily, @OSVersion,
        @BrowserFamily, @BrowserVersion);

INSERT INTO EmployeeLogTable (EmployeeId, Token, LogDateTime, LogTime,
                              IPAddress, CompName, MacAddress, GeoLocation,
                              Latitude, Longitude, OSFamily, OSVersion,
                              BrowserFamily, BrowserVersion)
VALUES (@EmployeeId, @Token, SYSUTCDATETIME(), CONVERT(time(7), SYSUTCDATETIME()),
        @IPAddress, @CompName, @MacAddress, @GeoLocation,
        @Latitude, @Longitude, @OSFamily, @OSVersion,
        @BrowserFamily, @BrowserVersion);