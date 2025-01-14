using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("347d0c02-e819-4c48-bf8b-204c61cd5a1c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ecddc0c7-c39a-408f-83c4-0f74a0c24228"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("5d4b0be2-bb99-4122-84d4-5cd2a82d02bf"), "Regular" },
                    { new Guid("d89e88ef-bf41-466a-8120-4be1c51b64fb"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("8f47154f-be3e-4e82-a013-07c7d2cb7f70"), "admin@example.com", "Admin", "hjN2JDjGD8nhsUZyIGOa7w==:Zi8Amp0D5sZw1zbBMmkZU0ft6z3bWIIOD6AqLPzggFk=" },
                    { new Guid("eeec799f-14c7-4ee4-b625-74da815e2313"), "user@example.com", "Regular", "ShNjoDzo7v7n6Wzb9z3Rtw==:LPGjWpb5RTq5WMaA7QptYZY3ksQQRWk55zmN5eWrXMA=" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { new Guid("114b75b9-6a1c-4d3d-942f-0d46146e82af"), new Guid("8f47154f-be3e-4e82-a013-07c7d2cb7f70") },
                    { new Guid("5c0db531-0267-4d96-a941-eb8c56b81f86"), new Guid("eeec799f-14c7-4ee4-b625-74da815e2313") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("114b75b9-6a1c-4d3d-942f-0d46146e82af"), new Guid("8f47154f-be3e-4e82-a013-07c7d2cb7f70") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("5c0db531-0267-4d96-a941-eb8c56b81f86"), new Guid("eeec799f-14c7-4ee4-b625-74da815e2313") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("5d4b0be2-bb99-4122-84d4-5cd2a82d02bf"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d89e88ef-bf41-466a-8120-4be1c51b64fb"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("8f47154f-be3e-4e82-a013-07c7d2cb7f70"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("eeec799f-14c7-4ee4-b625-74da815e2313"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("347d0c02-e819-4c48-bf8b-204c61cd5a1c"), "Admin" },
                    { new Guid("ecddc0c7-c39a-408f-83c4-0f74a0c24228"), "Regular" }
                });
        }
    }
}
