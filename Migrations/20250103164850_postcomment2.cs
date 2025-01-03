using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class postcomment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postsComments_Postari_PostId",
                table: "postsComments");

            migrationBuilder.DropForeignKey(
                name: "FK_postsComments_Profiles_Id_User",
                table: "postsComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_postsComments",
                table: "postsComments");

            migrationBuilder.RenameTable(
                name: "postsComments",
                newName: "PostsComments");

            migrationBuilder.RenameIndex(
                name: "IX_postsComments_PostId",
                table: "PostsComments",
                newName: "IX_PostsComments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_postsComments_Id_User",
                table: "PostsComments",
                newName: "IX_PostsComments_Id_User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsComments",
                table: "PostsComments",
                column: "PostCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsComments_Postari_PostId",
                table: "PostsComments",
                column: "PostId",
                principalTable: "Postari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsComments_Profiles_Id_User",
                table: "PostsComments",
                column: "Id_User",
                principalTable: "Profiles",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsComments_Postari_PostId",
                table: "PostsComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsComments_Profiles_Id_User",
                table: "PostsComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsComments",
                table: "PostsComments");

            migrationBuilder.RenameTable(
                name: "PostsComments",
                newName: "postsComments");

            migrationBuilder.RenameIndex(
                name: "IX_PostsComments_PostId",
                table: "postsComments",
                newName: "IX_postsComments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsComments_Id_User",
                table: "postsComments",
                newName: "IX_postsComments_Id_User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_postsComments",
                table: "postsComments",
                column: "PostCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_postsComments_Postari_PostId",
                table: "postsComments",
                column: "PostId",
                principalTable: "Postari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_postsComments_Profiles_Id_User",
                table: "postsComments",
                column: "Id_User",
                principalTable: "Profiles",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
