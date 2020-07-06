using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace neobooru.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    RegisteredOn = table.Column<DateTime>(nullable: true),
                    PfpUrl = table.Column<string>(nullable: true),
                    PfpThumbnailUrl = table.Column<string>(nullable: true),
                    ProfileDescription = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Views = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArtistName = table.Column<string>(nullable: false),
                    RegisteredAt = table.Column<DateTime>(nullable: false),
                    RegisteredById = table.Column<string>(nullable: false),
                    ProfileViews = table.Column<int>(nullable: false),
                    BackgroundImageUrl = table.Column<string>(nullable: true),
                    PfpUrl = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    FacebookProfileUrl = table.Column<string>(nullable: true),
                    TwitterProfileUrl = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_AspNetUsers_RegisteredById",
                        column: x => x.RegisteredById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelpEntrySections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SectionName = table.Column<string>(nullable: false),
                    SectionDescription = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpEntrySections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpEntrySections_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PoolName = table.Column<string>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pools_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    TagString = table.Column<string>(nullable: false),
                    AddedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArtistSubscriptions_AspNetUsers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HelpEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ParentSectionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpEntries_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HelpEntries_HelpEntrySections_ParentSectionId",
                        column: x => x.ParentSectionId,
                        principalTable: "HelpEntrySections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Arts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileUrl = table.Column<string>(nullable: true),
                    PreviewFileUrl = table.Column<string>(nullable: true),
                    LargeFileUrl = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UploaderId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: true),
                    Stars = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Md5Hash = table.Column<string>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    PoolId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arts_Artists_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Artists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arts_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arts_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArtComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CommentedOn = table.Column<DateTime>(nullable: false),
                    EditedOn = table.Column<DateTime>(nullable: false),
                    PlusVotes = table.Column<int>(nullable: false),
                    MinusVotes = table.Column<int>(nullable: false),
                    CommentedArtId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtComments_Arts_CommentedArtId",
                        column: x => x.CommentedArtId,
                        principalTable: "Arts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArtComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArtLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagOccurrences",
                columns: table => new
                {
                    TagId = table.Column<Guid>(nullable: false),
                    ArtId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagOccurrences", x => new { x.ArtId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagOccurrences_Arts_ArtId",
                        column: x => x.ArtId,
                        principalTable: "Arts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TagOccurrences_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce520153-1cd3-487f-9c2f-3e4fce4d6cfa", "61a95a0e-ba18-47e0-9afe-3d383b12c352", "root", "ROOT" });

            migrationBuilder.CreateIndex(
                name: "IX_ArtComments_CommentedArtId",
                table: "ArtComments",
                column: "CommentedArtId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtComments_UserId",
                table: "ArtComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBans_BannedArtistId",
                table: "ArtistBans",
                column: "BannedArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_RegisteredById",
                table: "Artists",
                column: "RegisteredById");

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
                name: "IX_Arts_AuthorId",
                table: "Arts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_PoolId",
                table: "Arts",
                column: "PoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_UploaderId",
                table: "Arts",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntries_CreatorId",
                table: "HelpEntries",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntries_ParentSectionId",
                table: "HelpEntries",
                column: "ParentSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpEntrySections_CreatorId",
                table: "HelpEntrySections",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pools_CreatorId",
                table: "Pools",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TagOccurrences_TagId",
                table: "TagOccurrences",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtComments");

            migrationBuilder.DropTable(
                name: "ArtistBans");

            migrationBuilder.DropTable(
                name: "ArtistSubscriptions");

            migrationBuilder.DropTable(
                name: "ArtLikes");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HelpEntries");

            migrationBuilder.DropTable(
                name: "TagOccurrences");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "HelpEntrySections");

            migrationBuilder.DropTable(
                name: "Arts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
