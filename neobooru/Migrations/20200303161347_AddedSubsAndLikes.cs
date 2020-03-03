using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class AddedSubsAndLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_arts_artists_AuthorId",
                table: "arts");

            migrationBuilder.DropForeignKey(
                name: "FK_arts_pools_Poolid",
                table: "arts");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_arts_commentedArtId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_arts_ArtId",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pools",
                table: "pools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_arts",
                table: "arts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_artists",
                table: "artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "key",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "pools",
                newName: "Pools");

            migrationBuilder.RenameTable(
                name: "arts",
                newName: "Arts");

            migrationBuilder.RenameTable(
                name: "artists",
                newName: "Artists");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pools",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Poolid",
                table: "Arts",
                newName: "PoolId");

            migrationBuilder.RenameIndex(
                name: "IX_arts_Poolid",
                table: "Arts",
                newName: "IX_Arts_PoolId");

            migrationBuilder.RenameIndex(
                name: "IX_arts_AuthorId",
                table: "Arts",
                newName: "IX_Arts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_ArtId",
                table: "Tags",
                newName: "IX_Tags_ArtId");

            migrationBuilder.RenameColumn(
                name: "plusVotes",
                table: "Comments",
                newName: "PlusVotes");

            migrationBuilder.RenameColumn(
                name: "minusVotes",
                table: "Comments",
                newName: "MinusVotes");

            migrationBuilder.RenameColumn(
                name: "editedOn",
                table: "Comments",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "commentedOn",
                table: "Comments",
                newName: "CommentedOn");

            migrationBuilder.RenameColumn(
                name: "commentedArtId",
                table: "Comments",
                newName: "CommentedArtId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_commentedArtId",
                table: "Comments",
                newName: "IX_Comments_CommentedArtId");

            migrationBuilder.AlterColumn<string>(
                name: "PoolName",
                table: "Pools",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Pools",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Tags",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Comments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pools",
                table: "Pools",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arts",
                table: "Arts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArtistBans",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    BanDate = table.Column<DateTime>(nullable: false),
                    BanDuration = table.Column<TimeSpan>(nullable: false),
                    BannedArtistId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistBans", x => x.id);
                    table.ForeignKey(
                        name: "FK_ArtistBans_Artists_BannedArtistId",
                        column: x => x.BannedArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubscriberId = table.Column<string>(nullable: false),
                    ArtistId = table.Column<Guid>(nullable: false),
                    SubscribedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtistSubscriptions_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSubscriptions_AspNetUsers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    LikedArtId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtLikes_Arts_LikedArtId",
                        column: x => x.LikedArtId,
                        principalTable: "Arts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelpEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: false),
                    UpdaterId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpEntries_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HelpEntries_AspNetUsers_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pools_CreatorId",
                table: "Pools",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBans_BannedArtistId",
                table: "ArtistBans",
                column: "BannedArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSubscriptions_ArtistId",
                table: "ArtistSubscriptions",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSubscriptions_SubscriberId",
                table: "ArtistSubscriptions",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtLikes_LikedArtId",
                table: "ArtLikes",
                column: "LikedArtId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtLikes_UserId",
                table: "ArtLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntries_CreatorId",
                table: "HelpEntries",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntries_UpdaterId",
                table: "HelpEntries",
                column: "UpdaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arts_Artists_AuthorId",
                table: "Arts",
                column: "AuthorId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Arts_Pools_PoolId",
                table: "Arts",
                column: "PoolId",
                principalTable: "Pools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Arts_CommentedArtId",
                table: "Comments",
                column: "CommentedArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_AspNetUsers_CreatorId",
                table: "Pools",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Arts_ArtId",
                table: "Tags",
                column: "ArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_CreatorId",
                table: "Tags",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arts_Artists_AuthorId",
                table: "Arts");

            migrationBuilder.DropForeignKey(
                name: "FK_Arts_Pools_PoolId",
                table: "Arts");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Arts_CommentedArtId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Pools_AspNetUsers_CreatorId",
                table: "Pools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Arts_ArtId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_CreatorId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "ArtistBans");

            migrationBuilder.DropTable(
                name: "ArtistSubscriptions");

            migrationBuilder.DropTable(
                name: "ArtLikes");

            migrationBuilder.DropTable(
                name: "HelpEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pools",
                table: "Pools");

            migrationBuilder.DropIndex(
                name: "IX_Pools_CreatorId",
                table: "Pools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arts",
                table: "Arts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Pools",
                newName: "pools");

            migrationBuilder.RenameTable(
                name: "Arts",
                newName: "arts");

            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "artists");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pools",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PoolId",
                table: "arts",
                newName: "Poolid");

            migrationBuilder.RenameIndex(
                name: "IX_Arts_PoolId",
                table: "arts",
                newName: "IX_arts_Poolid");

            migrationBuilder.RenameIndex(
                name: "IX_Arts_AuthorId",
                table: "arts",
                newName: "IX_arts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_ArtId",
                table: "Tag",
                newName: "IX_Tag_ArtId");

            migrationBuilder.RenameColumn(
                name: "PlusVotes",
                table: "Comment",
                newName: "plusVotes");

            migrationBuilder.RenameColumn(
                name: "MinusVotes",
                table: "Comment",
                newName: "minusVotes");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Comment",
                newName: "editedOn");

            migrationBuilder.RenameColumn(
                name: "CommentedOn",
                table: "Comment",
                newName: "commentedOn");

            migrationBuilder.RenameColumn(
                name: "CommentedArtId",
                table: "Comment",
                newName: "commentedArtId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentedArtId",
                table: "Comment",
                newName: "IX_Comment_commentedArtId");

            migrationBuilder.AlterColumn<string>(
                name: "PoolName",
                table: "pools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "key",
                table: "Comment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pools",
                table: "pools",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_arts",
                table: "arts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_artists",
                table: "artists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "key");

            migrationBuilder.AddForeignKey(
                name: "FK_arts_artists_AuthorId",
                table: "arts",
                column: "AuthorId",
                principalTable: "artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_arts_pools_Poolid",
                table: "arts",
                column: "Poolid",
                principalTable: "pools",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_arts_commentedArtId",
                table: "Comment",
                column: "commentedArtId",
                principalTable: "arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_arts_ArtId",
                table: "Tag",
                column: "ArtId",
                principalTable: "arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
