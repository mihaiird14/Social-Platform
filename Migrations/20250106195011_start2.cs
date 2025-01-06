using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Life.Migrations
{
    /// <inheritdoc />
    public partial class start2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "PostareLikes2",
                columns: table => new
                {
                    PostareLikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostareId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostareLikes2", x => x.PostareLikeId);
                    table.ForeignKey(
                        name: "FK_PostareLikes2_Postari_PostareId",
                        column: x => x.PostareId,
                        principalTable: "Postari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostareLikes2_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostareLikes2_PostareId",
                table: "PostareLikes2",
                column: "PostareId");

            migrationBuilder.CreateIndex(
                name: "IX_PostareLikes2_ProfileId",
                table: "PostareLikes2",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostareLikes2");

            migrationBuilder.CreateTable(
                name: "PostareLikes",
                columns: table => new
                {
                    PostareLikeId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostareLikes", x => x.PostareLikeId);
                    table.ForeignKey(
                        name: "FK_PostareLikes_Postari_PostareLikeId",
                        column: x => x.PostareLikeId,
                        principalTable: "Postari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostareLikes_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostareLikes_ProfileId",
                table: "PostareLikes",
                column: "ProfileId");
        }
    }
}
