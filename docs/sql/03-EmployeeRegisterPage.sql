-- ============================================================
-- Page: Employee Register
-- Route: /employeeregister
-- Access: Employee, MasterAdmin (UserTypeId 1) or Admin (UserTypeId 2) only
-- API: POST /api/auth/register-employee
-- Status: BUILT
-- Tables used: UserTypeMaster (SELECT), DepartmentMaster (SELECT),
--              EmployeeMaster (SELECT + INSERT)
-- ============================================================

-- 1. Validate the requested role and department exist
SELECT COUNT(*) FROM UserTypeMaster  WHERE UserTypeId  = @UserTypeId;
SELECT COUNT(*) FROM DepartmentMaster WHERE DepartmentId = @DepartmentId;

-- 2. Reject duplicate employee emails
SELECT COUNT(*) AS EmailExists
FROM   EmployeeMaster
WHERE  Email = @Email;

-- 3. Create the employee. Caller's EmployeeId is stamped into CreatedBy.
--    Security: request.UserTypeId must be >= caller's UserTypeId, and only a
--    MasterAdmin may create another MasterAdmin (enforced in AuthController).
INSERT INTO EmployeeMaster (UniqueId, Email, PasswordHash, FirstName, LastName,
                            PhoneNumber, DepartmentId, UserTypeId, IsActive,
                            CreatedBy, CreatedAt)
VALUES (NEWID(), @Email, @PasswordHash, @FirstName, @LastName,
        @PhoneNumber, @DepartmentId, @UserTypeId, 1,
        @CallerEmployeeId, SYSUTCDATETIME());