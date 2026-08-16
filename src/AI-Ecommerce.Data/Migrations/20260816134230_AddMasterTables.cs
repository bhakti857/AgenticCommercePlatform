using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AI_Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMasters",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMasters", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMasters",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMasters", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMasters",
                columns: table => new
                {
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMasters", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "UnitMasters",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMasters", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "UserTypeMasters",
                columns: table => new
                {
                    UserTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeMasters", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VendorMasters",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorMasters", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseMasters",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMasters", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryMasters",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryMasters", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategoryMasters_CategoryMasters_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryMasters",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLogs",
                columns: table => new
                {
                    LogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MacAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GeoLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OSFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OSVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrowserFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrowserVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_CustomerLogs_CustomerMasters_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerMasters",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterialMasters",
                columns: table => new
                {
                    RawMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawMaterialCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RawMaterialName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialMasters", x => x.RawMaterialId);
                    table.ForeignKey(
                        name: "FK_RawMaterialMasters_UnitMasters_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitMasters",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMasters",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    UserTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMasters", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeMasters_DepartmentMasters_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentMasters",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMasters_UserTypeMasters_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypeMasters",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductMasters",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GSTPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Approval1By = table.Column<long>(type: "bigint", nullable: true),
                    Approval1At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approval2By = table.Column<long>(type: "bigint", nullable: true),
                    Approval2At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approval3By = table.Column<long>(type: "bigint", nullable: true),
                    Approval3At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMasters", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductMasters_CategoryMasters_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryMasters",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductMasters_SubCategoryMasters_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategoryMasters",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductMasters_UnitMasters_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitMasters",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLogs",
                columns: table => new
                {
                    LogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MacAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GeoLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OSFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OSVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrowserFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrowserVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_EmployeeLogs_EmployeeMasters_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeMasters",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "DepartmentMasters",
                columns: new[] { "DepartmentId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DepartmentName", "LogId", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "CEO", null, null, null },
                    { 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Software Developer", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserTypeMasters",
                columns: new[] { "UserTypeId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "LogId", "ModifiedAt", "ModifiedBy", "UserTypeName" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, null, "MasterAdmin" },
                    { 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, null, "Admin" },
                    { 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, null, "Senior" },
                    { 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, null, "Junior" },
                    { 5L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, null, "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMasters_CategoryName",
                table: "CategoryMasters",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLogs_CustomerId",
                table: "CustomerLogs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMasters_Email",
                table: "CustomerMasters",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMasters_UniqueId",
                table: "CustomerMasters",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMasters_DepartmentName",
                table: "DepartmentMasters",
                column: "DepartmentName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLogs_EmployeeId",
                table: "EmployeeLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_DepartmentId",
                table: "EmployeeMasters",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_Email",
                table: "EmployeeMasters",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_UniqueId",
                table: "EmployeeMasters",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_UserTypeId",
                table: "EmployeeMasters",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMasters_CategoryId",
                table: "ProductMasters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMasters_ProductCode",
                table: "ProductMasters",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMasters_SubCategoryId",
                table: "ProductMasters",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMasters_UnitId",
                table: "ProductMasters",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMasters_RawMaterialCode",
                table: "RawMaterialMasters",
                column: "RawMaterialCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMasters_UnitId",
                table: "RawMaterialMasters",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryMasters_CategoryId_SubCategoryName",
                table: "SubCategoryMasters",
                columns: new[] { "CategoryId", "SubCategoryName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitMasters_UnitName",
                table: "UnitMasters",
                column: "UnitName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTypeMasters_UserTypeName",
                table: "UserTypeMasters",
                column: "UserTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorMasters_VendorName",
                table: "VendorMasters",
                column: "VendorName");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMasters_WarehouseName",
                table: "WarehouseMasters",
                column: "WarehouseName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerLogs");

            migrationBuilder.DropTable(
                name: "EmployeeLogs");

            migrationBuilder.DropTable(
                name: "ProductMasters");

            migrationBuilder.DropTable(
                name: "RawMaterialMasters");

            migrationBuilder.DropTable(
                name: "VendorMasters");

            migrationBuilder.DropTable(
                name: "WarehouseMasters");

            migrationBuilder.DropTable(
                name: "CustomerMasters");

            migrationBuilder.DropTable(
                name: "EmployeeMasters");

            migrationBuilder.DropTable(
                name: "SubCategoryMasters");

            migrationBuilder.DropTable(
                name: "UnitMasters");

            migrationBuilder.DropTable(
                name: "DepartmentMasters");

            migrationBuilder.DropTable(
                name: "UserTypeMasters");

            migrationBuilder.DropTable(
                name: "CategoryMasters");
        }
    }
}
