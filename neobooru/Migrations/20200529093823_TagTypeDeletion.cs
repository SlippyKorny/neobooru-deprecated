using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class TagTypeDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "Arts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Arts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "Width",
                table: "Arts",
                type: "real",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Height",
                table: "Arts",
                type: "real",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
