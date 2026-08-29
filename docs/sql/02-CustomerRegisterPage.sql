-- ============================================================
-- Page: Customer Register
-- Route: /register
-- Access: Public (always creates a CustomerMaster row)
-- API: POST /api/auth/register
-- Status: BUILT
-- Tables used: CustomerMaster (SELECT + INSERT)
-- ============================================================

-- 1. Reject duplicate emails (AuthController.Register)
SELECT COUNT(*) AS EmailExists
FROM   CustomerMaster
WHERE  Email = @Email;

-- 2. Create the customer. PasswordHash is PBKDF2 (PasswordHasher.HashPassword).
INSERT INTO CustomerMaster (UniqueId, Email, PasswordHash, FirstName, LastName,
                            PhoneNumber, AddressLine, City, State, Country,
                            Pincode, IsActive, CreatedAt)
VALUES (NEWID(), @Email, @PasswordHash, @FirstName, @LastName,
        @PhoneNumber, NULL, NULL, NULL, NULL,
        NULL, 1, SYSUTCDATETIME());