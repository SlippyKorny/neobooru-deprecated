using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class AddedRegisteredOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "AspNetUsers");
        }
    }
}
