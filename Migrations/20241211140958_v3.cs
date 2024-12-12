using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadCommentsLikes_Profiles_User_id",
                table: "ThreadCommentsLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadCommentsLikes_ThreadComments_Comment_Id",
                table: "ThreadCommentsLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadCommentsLikes_Profiles_User_id",
                table: "ThreadCommentsLikes",
                column: "User_id",
                principalTable: "Profiles",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadCommentsLikes_ThreadComments_Comment_Id",
                table: "ThreadCommentsLikes",
                column: "Comment_Id",
                principalTable: "ThreadComments",
                principalColumn: "ThreadCommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadCommentsLikes_Profiles_User_id",
                table: "ThreadCommentsLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadCommentsLikes_ThreadComments_Comment_Id",
                table: "ThreadCommentsLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadCommentsLikes_Profiles_User_id",
                table: "ThreadCommentsLikes",
                column: "User_id",
                principalTable: "Profiles",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadCommentsLikes_ThreadComments_Comment_Id",
                table: "ThreadCommentsLikes",
                column: "Comment_Id",
                principalTable: "ThreadComments",
                principalColumn: "ThreadCommentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
