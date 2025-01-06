using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class comPostsLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostCommentsLikes",
                columns: table => new
                {
                    PostCMLK_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment_Id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentsLikes", x => x.PostCMLK_Id);
                    table.ForeignKey(
                        name: "FK_PostCommentsLikes_PostsComments_Comment_Id",
                        column: x => x.Comment_Id,
                        principalTable: "PostsComments",
                        principalColumn: "PostCommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostCommentsLikes_Profiles_User_id",
                        column: x => x.User_id,
                        principalTable: "Profiles",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentsLikes_Comment_Id",
                table: "PostCommentsLikes",
                column: "Comment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentsLikes_User_id",
                table: "PostCommentsLikes",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCommentsLikes");
        }
    }
}
