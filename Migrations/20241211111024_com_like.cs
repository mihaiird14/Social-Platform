using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class com_like : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThreadCommentsLikes",
                columns: table => new
                {
                    ThreadCMLK_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment_Id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadCommentsLikes", x => x.ThreadCMLK_Id);
                    table.ForeignKey(
                        name: "FK_ThreadCommentsLikes_Profiles_User_id",
                        column: x => x.User_id,
                        principalTable: "Profiles",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThreadCommentsLikes_ThreadComments_Comment_Id",
                        column: x => x.Comment_Id,
                        principalTable: "ThreadComments",
                        principalColumn: "ThreadCommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadCommentsLikes_Comment_Id",
                table: "ThreadCommentsLikes",
                column: "Comment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadCommentsLikes_User_id",
                table: "ThreadCommentsLikes",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadCommentsLikes");
        }
    }
}
