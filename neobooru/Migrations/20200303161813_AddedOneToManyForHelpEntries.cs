using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class AddedOneToManyForHelpEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentSectionId",
                table: "HelpEntries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "HelpEntrySection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SectionName = table.Column<string>(nullable: false),
                    SectionDescription = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: false),
                    UpdaterId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpEntrySection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpEntrySection_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HelpEntrySection_AspNetUsers_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntries_ParentSectionId",
                table: "HelpEntries",
                column: "ParentSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntrySection_CreatorId",
                table: "HelpEntrySection",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntrySection_UpdaterId",
                table: "HelpEntrySection",
                column: "UpdaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpEntries_HelpEntrySection_ParentSectionId",
                table: "HelpEntries",
                column: "ParentSectionId",
                principalTable: "HelpEntrySection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpEntries_HelpEntrySection_ParentSectionId",
                table: "HelpEntries");

            migrationBuilder.DropTable(
                name: "HelpEntrySection");

            migrationBuilder.DropIndex(
                name: "IX_HelpEntries_ParentSectionId",
                table: "HelpEntries");

            migrationBuilder.DropColumn(
                name: "ParentSectionId",
                table: "HelpEntries");
        }
    }
}
