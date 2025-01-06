using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class Like : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Profiles_Id_User",
                table: "Threads");

            migrationBuilder.AlterColumn<string>(
                name: "ThreadText",
                table: "Threads",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ThreadLikes",
                columns: table => new
                {
                    ThreadLikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadLikes", x => x.ThreadLikeId);
                    table.ForeignKey(
                        name: "FK_ThreadLikes_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThreadLikes_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "ThreadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadLikes_ProfileId",
                table: "ThreadLikes",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadLikes_ThreadId",
                table: "ThreadLikes",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Profiles_Id_User",
                table: "Threads",
                column: "Id_User",
                principalTable: "Profiles",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Profiles_Id_User",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "ThreadLikes");

            migrationBuilder.AlterColumn<string>(
                name: "ThreadText",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Profiles_Id_User",
                table: "Threads",
                column: "Id_User",
                principalTable: "Profiles",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
