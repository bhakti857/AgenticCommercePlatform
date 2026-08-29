-- ============================================================
-- Page: Customer Master (employee-facing customer management)
-- Route: /masters/customer (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/customer-master (planned)
-- Status: PLANNED (model exists: CustomerMaster; self-registration via
--          /api/auth/register already builds; no admin UI/API yet)
-- Tables used: CustomerMaster (CRUD), CustomerLogTable (view login history)
-- ============================================================

-- ADD tab (staff creating a customer on their behalf)
INSERT INTO CustomerMaster (UniqueId, Email, PasswordHash, FirstName, LastName,
                            PhoneNumber, AddressLine, City, State, Country,
                            Pincode, IsActive, CreatedAt)
VALUES (NEWID(), @Email, @PasswordHash, @FirstName, @LastName,
        @PhoneNumber, @AddressLine, @City, @State, @Country,
        @Pincode, 1, SYSUTCDATETIME());

-- LIST tab
SELECT CustomerId, Email, FirstName, LastName, PhoneNumber,
       City, State, Country, Pincode, IsActive, CreatedAt
FROM   CustomerMaster
WHERE  DeletedAt IS NULL
ORDER  BY CreatedAt DESC;

-- EDIT button (staff updating a customer's details)
UPDATE CustomerMaster
SET    FirstName   = @FirstName,
       LastName    = @LastName,
       PhoneNumber = @PhoneNumber,
       AddressLine = @AddressLine,
       City        = @City,
       State       = @State,
       Country     = @Country,
       Pincode     = @Pincode,
       IsActive    = @IsActive,
       UpdatedAt   = SYSUTCDATETIME()
WHERE  CustomerId = @CustomerId;

-- DELETE button (soft delete)
UPDATE CustomerMaster
SET    DeletedAt = SYSUTCDATETIME()
WHERE  CustomerId = @CustomerId;

-- Login history for a customer
SELECT LogDateTime, IPAddress, CompName, OSFamily, BrowserFamily
FROM   CustomerLogTable
WHERE  CustomerId = @CustomerId
ORDER  BY LogDateTime DESC;