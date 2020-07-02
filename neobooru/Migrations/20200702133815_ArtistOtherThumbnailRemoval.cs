using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class ArtistOtherThumbnailRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargePfpUrl",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "PreviewPfpUrl",
                table: "Artists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LargePfpUrl",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviewPfpUrl",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
