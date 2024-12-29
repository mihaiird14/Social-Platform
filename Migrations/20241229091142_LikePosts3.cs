using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class LikePosts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrLikePostare",
                table: "PostareLikes");

            migrationBuilder.AddColumn<int>(
                name: "NrLikePostare",
                table: "Postari",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrLikePostare",
                table: "Postari");

            migrationBuilder.AddColumn<int>(
                name: "NrLikePostare",
                table: "PostareLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
