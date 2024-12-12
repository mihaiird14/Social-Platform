using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class UpTh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Threads");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
