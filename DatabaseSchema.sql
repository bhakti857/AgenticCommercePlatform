

-- ============================================
-- USER MANAGEMENT TABLES
-- ============================================

-- Users Table
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    UserType INT NOT NULL, -- 1:MasterAdmin, 2:Admin, 3:Employee, 4:Customer
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    CreatedBy UNIQUEIDENTIFIER NULL,
    UpdatedBy UNIQUEIDENTIFIER NULL,
    LastLoginAt DATETIME2 NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);

-- User Roles (Permissions)
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL, -- MasterAdmin, Admin, Employee, Customer
    Description NVARCHAR(200)
);

-- User Permissions
CREATE TABLE Permissions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL, -- ViewProducts, ManageUsers, ProcessOrders etc.
    Description NVARCHAR(200)
);

-- Role Permissions Mapping
CREATE TABLE RolePermissions (
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    PRIMARY KEY (RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    FOREIGN KEY (PermissionId) REFERENCES Permissions(Id)
);

-- User Refresh Tokens
CREATE TABLE RefreshTokens (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Token NVARCHAR(1000) NOT NULL,
    ExpiryDate DATETIME2 NOT NULL,
    IsRevoked BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- ============================================
-- CUSTOMER TABLES
-- ============================================

-- Customers (extends Users)
CREATE TABLE Customers (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    DateOfBirth DATE,
    Gender NVARCHAR(20),
    IsSubscribed BIT DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- Customer Addresses
CREATE TABLE CustomerAddresses (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    AddressLine1 NVARCHAR(200) NOT NULL,
    AddressLine2 NVARCHAR(200),
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    PostalCode NVARCHAR(20) NOT NULL,
    Country NVARCHAR(100) NOT NULL,
    IsDefault BIT DEFAULT 0,
    IsShipping BIT DEFAULT 0,
    IsBilling BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);

-- ============================================
-- PRODUCT TABLES
-- ============================================

-- Products
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SKU NVARCHAR(50) UNIQUE NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    Cost DECIMAL(18,2) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    SubCategory NVARCHAR(100),
    Brand NVARCHAR(100),
    Weight DECIMAL(10,2),
    Dimensions NVARCHAR(100),
    StockQuantity INT NOT NULL DEFAULT 0,
    LowStockThreshold INT DEFAULT 10,
    IsActive BIT DEFAULT 1,
    IsFeatured BIT DEFAULT 0,
    IsDigital BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    CreatedBy UNIQUEIDENTIFIER NULL,
    UpdatedBy UNIQUEIDENTIFIER NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);

-- Product Images
CREATE TABLE ProductImages (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL,
    ImageUrl NVARCHAR(500) NOT NULL,
    AltText NVARCHAR(200),
    IsPrimary BIT DEFAULT 0,
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

-- Product Categories
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ParentCategoryId INT NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (ParentCategoryId) REFERENCES Categories(Id)
);

-- Product Reviews
CREATE TABLE ProductReviews (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
    Title NVARCHAR(200),
    Comment NVARCHAR(MAX),
    IsApproved BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);

-- ============================================
-- ORDER TABLES
-- ============================================

-- Orders
CREATE TABLE Orders (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderNumber NVARCHAR(50) UNIQUE NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    OrderDate DATETIME2 DEFAULT GETUTCDATE(),
    ShippingAddressId UNIQUEIDENTIFIER NOT NULL,
    BillingAddressId UNIQUEIDENTIFIER NOT NULL,
    OrderStatus NVARCHAR(50) NOT NULL, -- Pending, Processing, Shipped, Delivered, Cancelled, Refunded
    PaymentStatus NVARCHAR(50) NOT NULL, -- Pending, Paid, Failed, Refunded
    SubTotal DECIMAL(18,2) NOT NULL,
    TaxAmount DECIMAL(18,2) DEFAULT 0,
    ShippingCost DECIMAL(18,2) DEFAULT 0,
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL,
    ShippingMethod NVARCHAR(100),
    TrackingNumber NVARCHAR(100),
    OrderNotes NVARCHAR(MAX),
    PaymentMethod NVARCHAR(100),
    TransactionId NVARCHAR(200),
    PaymentGateway NVARCHAR(50),
    PaymentResponse NVARCHAR(MAX),
    ProcessedBy UNIQUEIDENTIFIER NULL,
    ShippedDate DATETIME2 NULL,
    DeliveredDate DATETIME2 NULL,
    CancelledDate DATETIME2 NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (ShippingAddressId) REFERENCES CustomerAddresses(Id),
    FOREIGN KEY (BillingAddressId) REFERENCES CustomerAddresses(Id),
    FOREIGN KEY (ProcessedBy) REFERENCES Users(Id)
);

-- Order Items
CREATE TABLE OrderItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId UNIQUEIDENTIFIER NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    ProductSKU NVARCHAR(50) NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- ============================================
-- SHOPPING CART TABLES
-- ============================================

-- Shopping Cart
CREATE TABLE ShoppingCarts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    AddedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

-- ============================================
-- PAYMENT TABLES
-- ============================================

-- Payments
CREATE TABLE Payments (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderId UNIQUEIDENTIFIER NOT NULL,
    PaymentGateway NVARCHAR(50) NOT NULL,
    TransactionId NVARCHAR(200) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Currency NVARCHAR(10) DEFAULT 'USD',
    Status NVARCHAR(50) NOT NULL, -- Pending, Success, Failed, Refunded
    PaymentMethod NVARCHAR(50) NOT NULL,
    ResponseData NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
);

-- ============================================
-- EMPLOYEE TABLES
-- ============================================

-- Employee Records
CREATE TABLE Employees (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Department NVARCHAR(100) NOT NULL, -- Sales, Inventory, Shipping, Support
    Position NVARCHAR(100) NOT NULL,
    Salary DECIMAL(18,2),
    HireDate DATE NOT NULL,
    EmploymentStatus NVARCHAR(50) DEFAULT 'Active',
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- Employee Activity Logs
CREATE TABLE EmployeeActivityLogs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId UNIQUEIDENTIFIER NOT NULL,
    Action NVARCHAR(100) NOT NULL,
    ActionDetails NVARCHAR(MAX),
    IPAddress NVARCHAR(50),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE
);

-- ============================================
-- AI AGENT TABLES (Harness Memory)
-- ============================================

-- Conversation History
CREATE TABLE ConversationHistory (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL,
    SessionId NVARCHAR(100) NOT NULL,
    UserMessage NVARCHAR(MAX) NOT NULL,
    AssistantMessage NVARCHAR(MAX),
    Intent NVARCHAR(100),
    AgentName NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- Agent Actions (Tool Calls)
CREATE TABLE AgentActions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SessionId NVARCHAR(100) NOT NULL,
    ToolName NVARCHAR(100) NOT NULL,
    ToolParameters NVARCHAR(MAX),
    ToolResult NVARCHAR(MAX),
    ExecutionTimeMs INT,
    IsApproved BIT,
    ApprovedBy UNIQUEIDENTIFIER NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (ApprovedBy) REFERENCES Users(Id)
);

-- User Preferences (Learned by AI)
CREATE TABLE UserPreferences (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL,
    PreferenceType NVARCHAR(50) NOT NULL, -- CategoryPreference, PriceRange, etc.
    PreferenceValue NVARCHAR(500) NOT NULL,
    Score FLOAT DEFAULT 1.0,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- ============================================
-- AUDIT TABLES
-- ============================================

-- Audit Logs
CREATE TABLE AuditLogs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER,
    EntityName NVARCHAR(200),
    EntityId NVARCHAR(100),
    Action NVARCHAR(50) NOT NULL, -- CREATE, UPDATE, DELETE, VIEW
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    IPAddress NVARCHAR(50),
    UserAgent NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);