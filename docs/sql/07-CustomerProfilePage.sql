-- ============================================================
-- Page: Customer Profile
-- Route: /profile
-- Access: Customer account only
-- API: GET /api/profile, PUT /api/profile (planned)
-- Status: PLANNED (schema exists, no page/API yet)
-- Tables used: CustomerMaster (SELECT + UPDATE)
-- ============================================================

-- 1. Load the customer's profile
SELECT CustomerId, Email, FirstName, LastName, PhoneNumber,
       AddressLine, City, State, Country, Pincode
FROM   CustomerMaster
WHERE  CustomerId = @CustomerId;

-- 2. Update profile details (address, contact). Password change is a
--    separate flow and always re-hashes via PasswordHasher.
UPDATE CustomerMaster
SET    FirstName   = @FirstName,
       LastName    = @LastName,
       PhoneNumber = @PhoneNumber,
       AddressLine = @AddressLine,
       City        = @City,
       State       = @State,
       Country     = @Country,
       Pincode     = @Pincode,
       UpdatedAt   = SYSUTCDATETIME()
WHERE  CustomerId  = @CustomerId;