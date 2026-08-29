-- ============================================================
-- Page: User Type Master
-- Route: /masters/usertype (planned)
-- Access: Employee account only, typically MasterAdmin/Admin
-- API: GET/POST/PUT/DELETE /api/usertype-master (planned)
-- Status: PLANNED (model exists: UserTypeMaster; seeded 1=MasterAdmin,
--          2=Admin, 3=Senior, 4=Junior, 5=User; no API/UI yet)
-- NOTE: Role numbers are load-bearing - JWT claim UserTypeId and the
--       agent's allowWriteTools gate (1 or 2) depend on these IDs.
-- Tables used: UserTypeMaster (CRUD), EmployeeMaster (count check on delete)
-- ============================================================

-- ADD tab
INSERT INTO UserTypeMaster (UserTypeName, CreatedBy, CreatedAt)
VALUES (@UserTypeName, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT UserTypeId, UserTypeName, CreatedBy, CreatedAt, ModifiedAt
FROM   UserTypeMaster
WHERE  DeletedAt IS NULL
ORDER  BY UserTypeId;

-- EDIT button
UPDATE UserTypeMaster
SET    UserTypeName = @UserTypeName,
       ModifiedBy   = @ModifiedBy,
       ModifiedAt   = SYSUTCDATETIME()
WHERE  UserTypeId = @UserTypeId;

-- DELETE button (soft delete)
UPDATE UserTypeMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  UserTypeId = @UserTypeId;

-- Guard: roles still in use by employees
SELECT COUNT(*) FROM EmployeeMaster WHERE UserTypeId = @UserTypeId AND DeletedAt IS NULL;