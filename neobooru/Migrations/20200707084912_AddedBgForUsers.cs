using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class AddedBgForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce520153-1cd3-487f-9c2f-3e4fce4d6cfa");

            migrationBuilder.DropColumn(
                name: "PfpThumbnailUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BgUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c030b06c-5fd0-4ae0-84e0-a560ec053d81", "9b1ec352-4ccb-4c38-bca3-5ab5e52a724e", "root", "ROOT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c030b06c-5fd0-4ae0-84e0-a560ec053d81");

            migrationBuilder.DropColumn(
                name: "BgUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PfpThumbnailUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce520153-1cd3-487f-9c2f-3e4fce4d6cfa", "61a95a0e-ba18-47e0-9afe-3d383b12c352", "root", "ROOT" });
        }
    }
}
