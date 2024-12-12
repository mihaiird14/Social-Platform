using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class ComTh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThreadComments",
                columns: table => new
                {
                    ThreadCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadComments", x => x.ThreadCommentId);
                    table.ForeignKey(
                        name: "FK_ThreadComments_Profiles_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Profiles",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThreadComments_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "ThreadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadComments_Id_User",
                table: "ThreadComments",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadComments_ThreadId",
                table: "ThreadComments",
                column: "ThreadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadComments");
        }
    }
}
