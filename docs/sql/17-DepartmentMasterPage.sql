-- ============================================================
-- Page: Department Master
-- Route: /masters/department (planned)
-- Access: Employee account only, typically MasterAdmin/Admin
-- API: GET/POST/PUT/DELETE /api/department-master (planned)
-- Status: PLANNED (model exists: DepartmentMaster; seeded 1=CEO,
--          2=Software Developer; no API/UI yet)
-- Tables used: DepartmentMaster (CRUD), EmployeeMaster (count check on delete)
-- ============================================================

-- ADD tab
INSERT INTO DepartmentMaster (DepartmentName, CreatedBy, CreatedAt)
VALUES (@DepartmentName, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT DepartmentId, DepartmentName, CreatedBy, CreatedAt, ModifiedAt
FROM   DepartmentMaster
WHERE  DeletedAt IS NULL
ORDER  BY DepartmentName;

-- EDIT button
UPDATE DepartmentMaster
SET    DepartmentName = @DepartmentName,
       ModifiedBy     = @ModifiedBy,
       ModifiedAt     = SYSUTCDATETIME()
WHERE  DepartmentId = @DepartmentId;

-- DELETE button (soft delete)
UPDATE DepartmentMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  DepartmentId = @DepartmentId;

-- Guard: departments still assigned to employees
SELECT COUNT(*) FROM EmployeeMaster WHERE DepartmentId = @DepartmentId AND DeletedAt IS NULL;