using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12adaae2-195e-4297-ac00-35104bc4600d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ba1bace-5f33-4727-84db-6fb67d89b532");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8800e310-0b9f-4bd4-9dcc-acbf65af76e5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17825746-f146-4edc-a220-0885085cbf62", "3", "HR", "HR" },
                    { "e109d72d-a8ff-400e-a794-0bd94dc39351", "1", "Admin", "ADMIN" },
                    { "fbe37e9b-294a-4350-a86f-163e5d223171", "2", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17825746-f146-4edc-a220-0885085cbf62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e109d72d-a8ff-400e-a794-0bd94dc39351");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbe37e9b-294a-4350-a86f-163e5d223171");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12adaae2-195e-4297-ac00-35104bc4600d", null, "HR", null },
                    { "5ba1bace-5f33-4727-84db-6fb67d89b532", null, "Admin", null },
                    { "8800e310-0b9f-4bd4-9dcc-acbf65af76e5", null, "User", null }
                });
        }
    }
}
