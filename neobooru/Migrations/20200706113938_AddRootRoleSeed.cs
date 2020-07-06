using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class AddRootRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b74de79c-c569-46c6-9146-d60f51acde46", "46aa5054-bc77-41cf-968d-a46718361b35", "root", "ROOT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b74de79c-c569-46c6-9146-d60f51acde46");
        }
    }
}
