-- ============================================================
-- Page: Employee Master (staff management)
-- Route: /masters/employee (planned)
-- Access: Employee account only, MasterAdmin/Admin for most operations
-- API: GET/POST/PUT/DELETE /api/employee-master (planned)
-- Status: PLANNED (model exists: EmployeeMaster; employee self-registration
--          via /api/auth/register-employee already built; no admin UI/API yet)
-- NOTE: Creation already exists via register-employee, which enforces the
--       privilege rule (a caller can only create accounts at their own
--       UserTypeId or lower-privileged ones; only MasterAdmin mints
--       MasterAdmin).
-- Tables used: EmployeeMaster (CRUD), DepartmentMaster / UserTypeMaster
--              (SELECT dropdowns), EmployeeLogTable (view login history)
-- ============================================================

-- ADD tab (same rule as register-employee)
INSERT INTO EmployeeMaster (UniqueId, Email, PasswordHash, FirstName, LastName,
                            PhoneNumber, DepartmentId, UserTypeId, IsActive,
                            CreatedBy, CreatedAt)
VALUES (NEWID(), @Email, @PasswordHash, @FirstName, @LastName,
        @PhoneNumber, @DepartmentId, @UserTypeId, 1,
        @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT e.EmployeeId, e.Email, e.FirstName, e.LastName, e.PhoneNumber,
       d.DepartmentName, t.UserTypeName, e.IsActive, e.CreatedAt
FROM   EmployeeMaster e
JOIN   DepartmentMaster d ON d.DepartmentId = e.DepartmentId
JOIN   UserTypeMaster   t ON t.UserTypeId   = e.UserTypeId
WHERE  e.DeletedAt IS NULL
ORDER  BY e.CreatedAt DESC;

-- EDIT button (role/department changes only; password is a separate flow)
UPDATE EmployeeMaster
SET    FirstName    = @FirstName,
       LastName     = @LastName,
       PhoneNumber  = @PhoneNumber,
       DepartmentId = @DepartmentId,
       UserTypeId   = @UserTypeId,
       IsActive     = @IsActive,
       UpdatedAt    = SYSUTCDATETIME()
WHERE  EmployeeId = @EmployeeId;

-- DELETE button (soft delete)
UPDATE EmployeeMaster
SET    DeletedAt = SYSUTCDATETIME()
WHERE  EmployeeId = @EmployeeId;

-- Login history for an employee
SELECT LogDateTime, IPAddress, CompName, OSFamily, BrowserFamily
FROM   EmployeeLogTable
WHERE  EmployeeId = @EmployeeId
ORDER  BY LogDateTime DESC;