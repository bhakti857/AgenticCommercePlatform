-- ============================================================
-- Page: Vendor Master
-- Route: /masters/vendor (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/vendor-master (planned)
-- Status: PLANNED (model exists: VendorMaster, no API/UI yet)
-- Tables used: VendorMaster (CRUD), PurchaseOrder (future, count check on delete)
-- ============================================================

-- ADD tab
INSERT INTO VendorMaster (VendorName, Email, PhoneNumber, Address, City,
                          State, Country, Pincode, GSTNumber,
                          IsActive, CreatedBy, CreatedAt)
VALUES (@VendorName, @Email, @PhoneNumber, @Address, @City,
        @State, @Country, @Pincode, @GSTNumber,
        1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT VendorId, VendorName, Email, PhoneNumber, City, State, GSTNumber,
       IsActive, CreatedAt
FROM   VendorMaster
WHERE  DeletedAt IS NULL
ORDER  BY VendorName;

-- EDIT button
UPDATE VendorMaster
SET    VendorName   = @VendorName,
       Email        = @Email,
       PhoneNumber  = @PhoneNumber,
       Address      = @Address,
       City         = @City,
       State        = @State,
       Country      = @Country,
       Pincode      = @Pincode,
       GSTNumber    = @GSTNumber,
       IsActive     = @IsActive,
       ModifiedBy   = @ModifiedBy,
       ModifiedAt   = SYSUTCDATETIME()
WHERE  VendorId = @VendorId;

-- DELETE button (soft delete)
UPDATE VendorMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  VendorId = @VendorId;