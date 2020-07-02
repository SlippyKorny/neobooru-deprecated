using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class ArtistRegisteredBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisteredById",
                table: "Artists",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_RegisteredById",
                table: "Artists",
                column: "RegisteredById");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_AspNetUsers_RegisteredById",
                table: "Artists",
                column: "RegisteredById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_AspNetUsers_RegisteredById",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_RegisteredById",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "RegisteredById",
                table: "Artists");
        }
    }
}
