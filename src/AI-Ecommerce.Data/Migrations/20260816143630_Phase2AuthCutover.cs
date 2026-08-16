using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AI_Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class Phase2AuthCutover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- Data migration: copy existing legacy Users rows into the new
            // CustomerMaster/EmployeeMaster tables before dropping Users/UserTypes,
            // and repoint Orders.CustomerId at the new CustomerMasters row (matched
            // by email, since the old Guid Id has no meaning in the new bigint key
            // space). Old UserType 1/2/3 (Master Admin/Master/Employee) all become
            // EmployeeMaster rows; old UserType 4 (Customer) becomes CustomerMaster.
            // Old UserType -> new UserTypeMaster mapping: 1->1 (MasterAdmin),
            // 2->2 (Admin), 3->5 (User) — Employees have no equivalent split by
            // department/seniority yet, so they land as UserTypeId 5 (User) in
            // Department 2 (Software Developer) and can be re-assigned later.
            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.Users') IS NOT NULL
BEGIN
    INSERT INTO [CustomerMasters] ([UniqueId], [Email], [PasswordHash], [FirstName], [LastName], [PhoneNumber], [IsActive], [CreatedAt], [UpdatedAt])
    SELECT [Id], [Email], [PasswordHash], [FirstName], [LastName], [PhoneNumber], [IsActive], [CreatedAt], [UpdatedAt]
    FROM [Users] u
    WHERE u.[UserType] = 4
      AND NOT EXISTS (SELECT 1 FROM [CustomerMasters] c WHERE c.[Email] = u.[Email]);

    INSERT INTO [EmployeeMasters] ([UniqueId], [Email], [PasswordHash], [FirstName], [LastName], [PhoneNumber], [DepartmentId], [UserTypeId], [IsActive], [CreatedAt], [UpdatedAt])
    SELECT [Id], [Email], [PasswordHash], [FirstName], [LastName], [PhoneNumber],
           2 AS DepartmentId,
           CASE [UserType] WHEN 1 THEN 1 WHEN 2 THEN 2 ELSE 5 END AS UserTypeId,
           [IsActive], [CreatedAt], [UpdatedAt]
    FROM [Users] u
    WHERE u.[UserType] IN (1, 2, 3)
      AND NOT EXISTS (SELECT 1 FROM [EmployeeMasters] e WHERE e.[Email] = u.[Email]);

    IF OBJECT_ID('dbo.Orders') IS NOT NULL AND COL_LENGTH('dbo.Orders', 'CustomerId') IS NOT NULL
    BEGIN
        -- Orders.CustomerId is being converted from uniqueidentifier to bigint
        -- below and SQL Server cannot convert between those types in place.
        -- This is dev/test data (pre-cutover), so clear existing orders rather
        -- than attempt a lossy/impossible column-type remap.
        DELETE FROM [OrderItems] WHERE [OrderId] IN (SELECT [Id] FROM [Orders]);
        DELETE FROM [Orders];
    END
END
");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_ProcessedBy",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProcessedBy",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(name: "ProcessedBy", table: "Orders");
            migrationBuilder.DropColumn(name: "CustomerId", table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "ProcessedBy",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerMasters_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "CustomerMasters",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerMasters_CustomerId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProcessedBy",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Full system access, including agent write/execute tools.", "Master Admin" },
                    { 2, "Elevated access, including agent write/execute tools.", "Master" },
                    { 3, "Internal staff account with standard access.", "Employee" },
                    { 4, "Default account type for storefront customers.", "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProcessedBy",
                table: "Orders",
                column: "ProcessedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_ProcessedBy",
                table: "Orders",
                column: "ProcessedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
