using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
