using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class AddStandardRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b74de79c-c569-46c6-9146-d60f51acde46");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10509fae-ce43-46f7-93dd-512ece19f69e", "bbc99602-d0a9-4517-826b-9db8ca522536", "root", "ROOT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40854210-1f8a-49c7-be90-af74374cfd73", "c3f9e81f-dd3b-46e1-a4db-37f435ad7a76", "standard", "STANDARD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10509fae-ce43-46f7-93dd-512ece19f69e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40854210-1f8a-49c7-be90-af74374cfd73");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b74de79c-c569-46c6-9146-d60f51acde46", "46aa5054-bc77-41cf-968d-a46718361b35", "root", "ROOT" });
        }
    }
}
