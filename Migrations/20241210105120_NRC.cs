using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class NRC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThreadComments",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Edited",
                table: "ThreadComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThreadComments",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "ThreadComments");
        }
    }
}
