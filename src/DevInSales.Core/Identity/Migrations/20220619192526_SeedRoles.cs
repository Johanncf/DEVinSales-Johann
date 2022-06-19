using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevInSales.Core.Identity.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79348264-31a7-4ae4-a2cb-fb97cad3aec8", "9926916c-0022-4220-afb9-d5e58ffc24f9", "Usuario", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b513ab38-64ec-48f4-ae5c-276fcc972aef", "32b2bf03-1e20-49c9-b59d-a65d5e54f304", "Administrador", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d6f3befa-9e4d-4fd4-a1c9-62ffcb56fd73", "97c8dc5b-fad5-41ec-b7ca-2e02a32347ad", "Gerente", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79348264-31a7-4ae4-a2cb-fb97cad3aec8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b513ab38-64ec-48f4-ae5c-276fcc972aef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6f3befa-9e4d-4fd4-a1c9-62ffcb56fd73");
        }
    }
}
