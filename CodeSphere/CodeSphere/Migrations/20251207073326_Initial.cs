using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleLevel = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatThemes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emojis",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    EmojiType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emojis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayThemes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StickerTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StickerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRatings",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RaterUsername = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Stars = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRatings", x => new { x.Username, x.RaterUsername });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_ApplicationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChatThemeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_ChatThemes_ChatThemeId",
                        column: x => x.ChatThemeId,
                        principalTable: "ChatThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCodeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_CountryCodes_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "CountryCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmojiSkins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    EmojiId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiSkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmojiSkins_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolidayIcons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayThemeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayIcons_HolidayThemes_HolidayThemeId",
                        column: x => x.HolidayThemeId,
                        principalTable: "HolidayThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stickers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    StickerTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stickers_StickerTypes_StickerTypeId",
                        column: x => x.StickerTypeId,
                        principalTable: "StickerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZipCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZipCodes_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ZipCodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CountryCodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GitHubUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackoverflowUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedinUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    ReasonToBeBlocked = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_CountryCodes_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "CountryCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_ZipCodes_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "ZipCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsersRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUsersRoles_ApplicationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersRoles_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecieverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteStickers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StickerTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFavourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteStickers", x => new { x.ApplicationUserId, x.StickerTypeId });
                    table.ForeignKey(
                        name: "FK_FavouriteStickers_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteStickers_StickerTypes_StickerTypeId",
                        column: x => x.StickerTypeId,
                        principalTable: "StickerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUnfollows",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFollowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUnfollows", x => new { x.ApplicationUserId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_FollowUnfollows_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowUnfollows_ApplicationUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuickChatReplies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickChatReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuickChatReplies_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedFriends",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecommendedApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedFriends", x => new { x.ApplicationUserId, x.RecommendedApplicationUserId });
                    table.ForeignKey(
                        name: "FK_RecommendedFriends_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecommendedFriends_ApplicationUsers_RecommendedApplicationUserId",
                        column: x => x.RecommendedApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetUsername = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersGroups",
                columns: table => new
                {
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGroups", x => new { x.GroupId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_UsersGroups_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatMessageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatImages_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatImages_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatImages_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockedPosts",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedPosts", x => new { x.ApplicationUserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_BlockedPosts_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlockedPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentCommentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavouritePosts",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFavourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritePosts", x => new { x.ApplicationUserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_FavouritePosts_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouritePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PendingPosts",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPending = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingPosts", x => new { x.ApplicationUserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PendingPosts_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostImages_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostsLikes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsLikes", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostsLikes_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsLikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsTags",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsTags", x => new { x.TagId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostsTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseBlogActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaseUserActionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseBlogActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_BaseBlogActions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionStatus = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    SystemMessage = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    BaseBlogActionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_BaseUserActions_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseUserActions_BaseBlogActions_BaseBlogActionId",
                        column: x => x.BaseBlogActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId");
                });

            migrationBuilder.CreateTable(
                name: "CreatePostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatePostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_CreatePostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EditedPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EditorApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditedPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_EditedPostUserActions_ApplicationUsers_EditorApplicationUserId",
                        column: x => x.EditorApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditedPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EditOwnPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditOwnPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_EditOwnPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EditPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_EditPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikerApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_LikedPostUserActions_ApplicationUsers_LikerApplicationUserId",
                        column: x => x.LikerApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeOwnPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeOwnPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_LikeOwnPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikePostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikePostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_LikePostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnlikedPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnlikerApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnlikedPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_UnlikedPostUserActions_ApplicationUsers_UnlikerApplicationUserId",
                        column: x => x.UnlikerApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnlikedPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnlikeOwnPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnlikeOwnPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_UnlikeOwnPostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnlikePostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnlikePostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_UnlikePostUserActions_BaseBlogActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseBlogActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeCoverImageUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeCoverImageUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_ChangeCoverImageUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeProfilePictureUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeProfilePictureUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_ChangeProfilePictureUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeletedPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    DeleterApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_DeletedPostUserActions_ApplicationUsers_DeleterApplicationUserId",
                        column: x => x.DeleterApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeletedPostUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeleteOwnPostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteOwnPostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_DeleteOwnPostUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeletePostUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletePostUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_DeletePostUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EditPersonalDataUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditPersonalDataUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_EditPersonalDataUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowedUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowerApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_FollowedUserActions_ApplicationUsers_FollowerApplicationUserId",
                        column: x => x.FollowerApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowedUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowingApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_FollowUserActions_ApplicationUsers_FollowingApplicationUserId",
                        column: x => x.FollowingApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnfollowedUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnfollowerApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnfollowedUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_UnfollowedUserActions_ApplicationUsers_UnfollowerApplicationUserId",
                        column: x => x.UnfollowerApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnfollowedUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnfollowUserActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnfollowingApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnfollowUserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_UnfollowUserActions_ApplicationUsers_UnfollowingApplicationUserId",
                        column: x => x.UnfollowingApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnfollowUserActions_BaseUserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaseUserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActions_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserActions_BaseUserActions_BaseUserActionId",
                        column: x => x.BaseUserActionId,
                        principalTable: "BaseUserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ApplicationRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ApplicationUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_CityId",
                table: "ApplicationUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_CountryCodeId",
                table: "ApplicationUsers",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_CountryId",
                table: "ApplicationUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_StateId",
                table: "ApplicationUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ZipCodeId",
                table: "ApplicationUsers",
                column: "ZipCodeId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ApplicationUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersRoles_RoleId",
                table: "ApplicationUsersRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseBlogActions_PostId",
                table: "BaseBlogActions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUserActions_ApplicationUserId",
                table: "BaseUserActions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUserActions_BaseBlogActionId",
                table: "BaseUserActions",
                column: "BaseBlogActionId",
                unique: true,
                filter: "[BaseBlogActionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedPosts_PostId",
                table: "BlockedPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatImages_ApplicationUserId",
                table: "ChatImages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatImages_ChatMessageId",
                table: "ChatImages",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatImages_GroupId",
                table: "ChatImages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ApplicationUserId",
                table: "ChatMessages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_GroupId",
                table: "ChatMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryCodeId",
                table: "Countries",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedPostUserActions_DeleterApplicationUserId",
                table: "DeletedPostUserActions",
                column: "DeleterApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EditedPostUserActions_EditorApplicationUserId",
                table: "EditedPostUserActions",
                column: "EditorApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmojiSkins_EmojiId",
                table: "EmojiSkins",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritePosts_PostId",
                table: "FavouritePosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteStickers_StickerTypeId",
                table: "FavouriteStickers",
                column: "StickerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedUserActions_FollowerApplicationUserId",
                table: "FollowedUserActions",
                column: "FollowerApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUnfollows_FollowerId",
                table: "FollowUnfollows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUserActions_FollowingApplicationUserId",
                table: "FollowUserActions",
                column: "FollowingApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ChatThemeId",
                table: "Groups",
                column: "ChatThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayIcons_HolidayThemeId",
                table: "HolidayIcons",
                column: "HolidayThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedPostUserActions_LikerApplicationUserId",
                table: "LikedPostUserActions",
                column: "LikerApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingPosts_PostId",
                table: "PendingPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostImages_PostId",
                table: "PostImages",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsLikes_UserId",
                table: "PostsLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsTags_PostId",
                table: "PostsTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_QuickChatReplies_ApplicationUserId",
                table: "QuickChatReplies",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedFriends_RecommendedApplicationUserId",
                table: "RecommendedFriends",
                column: "RecommendedApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_StickerTypeId",
                table: "Stickers",
                column: "StickerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnfollowedUserActions_UnfollowerApplicationUserId",
                table: "UnfollowedUserActions",
                column: "UnfollowerApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnfollowUserActions_UnfollowingApplicationUserId",
                table: "UnfollowUserActions",
                column: "UnfollowingApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnlikedPostUserActions_UnlikerApplicationUserId",
                table: "UnlikedPostUserActions",
                column: "UnlikerApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_ApplicationUserId",
                table: "UserActions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_BaseUserActionId",
                table: "UserActions",
                column: "BaseUserActionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_PostId",
                table: "UserActions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ApplicationUserId",
                table: "UserNotifications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroups_ApplicationUserId",
                table: "UsersGroups",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodes_CityId",
                table: "ZipCodes",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseBlogActions_BaseUserActions_UserActionId",
                table: "BaseBlogActions",
                column: "UserActionId",
                principalTable: "BaseUserActions",
                principalColumn: "UserActionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Cities_CityId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ZipCodes_Cities_CityId",
                table: "ZipCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Countries_CountryId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_CountryId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_CountryCodes_CountryCodeId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_States_StateId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_ZipCodes_ZipCodeId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUserActions_ApplicationUsers_ApplicationUserId",
                table: "BaseUserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ApplicationUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseBlogActions_BaseUserActions_UserActionId",
                table: "BaseBlogActions");

            migrationBuilder.DropTable(
                name: "ApplicationUsersRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlockedPosts");

            migrationBuilder.DropTable(
                name: "ChangeCoverImageUserActions");

            migrationBuilder.DropTable(
                name: "ChangeProfilePictureUserActions");

            migrationBuilder.DropTable(
                name: "ChatImages");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CreatePostUserActions");

            migrationBuilder.DropTable(
                name: "DeletedPostUserActions");

            migrationBuilder.DropTable(
                name: "DeleteOwnPostUserActions");

            migrationBuilder.DropTable(
                name: "DeletePostUserActions");

            migrationBuilder.DropTable(
                name: "EditedPostUserActions");

            migrationBuilder.DropTable(
                name: "EditOwnPostUserActions");

            migrationBuilder.DropTable(
                name: "EditPersonalDataUserActions");

            migrationBuilder.DropTable(
                name: "EditPostUserActions");

            migrationBuilder.DropTable(
                name: "EmojiSkins");

            migrationBuilder.DropTable(
                name: "FavouritePosts");

            migrationBuilder.DropTable(
                name: "FavouriteStickers");

            migrationBuilder.DropTable(
                name: "FollowedUserActions");

            migrationBuilder.DropTable(
                name: "FollowUnfollows");

            migrationBuilder.DropTable(
                name: "FollowUserActions");

            migrationBuilder.DropTable(
                name: "HolidayIcons");

            migrationBuilder.DropTable(
                name: "LikedPostUserActions");

            migrationBuilder.DropTable(
                name: "LikeOwnPostUserActions");

            migrationBuilder.DropTable(
                name: "LikePostUserActions");

            migrationBuilder.DropTable(
                name: "PendingPosts");

            migrationBuilder.DropTable(
                name: "PostImages");

            migrationBuilder.DropTable(
                name: "PostsLikes");

            migrationBuilder.DropTable(
                name: "PostsTags");

            migrationBuilder.DropTable(
                name: "QuickChatReplies");

            migrationBuilder.DropTable(
                name: "RecommendedFriends");

            migrationBuilder.DropTable(
                name: "Stickers");

            migrationBuilder.DropTable(
                name: "UnfollowedUserActions");

            migrationBuilder.DropTable(
                name: "UnfollowUserActions");

            migrationBuilder.DropTable(
                name: "UnlikedPostUserActions");

            migrationBuilder.DropTable(
                name: "UnlikeOwnPostUserActions");

            migrationBuilder.DropTable(
                name: "UnlikePostUserActions");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "UserRatings");

            migrationBuilder.DropTable(
                name: "UsersGroups");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Emojis");

            migrationBuilder.DropTable(
                name: "HolidayThemes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "StickerTypes");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "ChatThemes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "CountryCodes");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "ZipCodes");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "BaseUserActions");

            migrationBuilder.DropTable(
                name: "BaseBlogActions");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
