using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAbstractModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_ApplicationUsers_ApplicationUserId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Posts_PostId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseBlogActions_BaseUserActions_UserActionId",
                table: "BaseBlogActions");

            migrationBuilder.DropTable(
                name: "ChangeCoverImageUserActions");

            migrationBuilder.DropTable(
                name: "ChangeProfilePictureUserActions");

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
                name: "FollowedUserActions");

            migrationBuilder.DropTable(
                name: "FollowUserActions");

            migrationBuilder.DropTable(
                name: "LikedPostUserActions");

            migrationBuilder.DropTable(
                name: "LikeOwnPostUserActions");

            migrationBuilder.DropTable(
                name: "LikePostUserActions");

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
                name: "BaseUserActions");

            migrationBuilder.DropTable(
                name: "BaseBlogActions");

            migrationBuilder.RenameColumn(
                name: "BaseUserActionId",
                table: "UserActions",
                newName: "ProfileImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "UserActions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserActions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "UserActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "UserActions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ActionStatus",
                table: "UserActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "UserActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FollowerUsername",
                table: "UserActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonUsername",
                table: "UserActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostContent",
                table: "UserActions",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostTitle",
                table: "UserActions",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_ApplicationUsers_ApplicationUserId",
                table: "UserActions",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Posts_PostId",
                table: "UserActions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_ApplicationUsers_ApplicationUserId",
                table: "UserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Posts_PostId",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "ActionStatus",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "FollowerUsername",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "PersonUsername",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "PostContent",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "PostTitle",
                table: "UserActions");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "UserActions",
                newName: "BaseUserActionId");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "UserActions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserActions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "BaseBlogActions",
                columns: table => new
                {
                    UserActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaseUserActionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    BaseBlogActionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActionStatus = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemMessage = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
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
                    table.ForeignKey(
                        name: "FK_BaseUserActions_UserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "UserActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DeleterApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
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
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
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
                    ShortContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
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
                name: "IX_DeletedPostUserActions_DeleterApplicationUserId",
                table: "DeletedPostUserActions",
                column: "DeleterApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EditedPostUserActions_EditorApplicationUserId",
                table: "EditedPostUserActions",
                column: "EditorApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedUserActions_FollowerApplicationUserId",
                table: "FollowedUserActions",
                column: "FollowerApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUserActions_FollowingApplicationUserId",
                table: "FollowUserActions",
                column: "FollowingApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedPostUserActions_LikerApplicationUserId",
                table: "LikedPostUserActions",
                column: "LikerApplicationUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_ApplicationUsers_ApplicationUserId",
                table: "UserActions",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Posts_PostId",
                table: "UserActions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseBlogActions_BaseUserActions_UserActionId",
                table: "BaseBlogActions",
                column: "UserActionId",
                principalTable: "BaseUserActions",
                principalColumn: "UserActionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
