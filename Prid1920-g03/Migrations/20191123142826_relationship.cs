using Microsoft.EntityFrameworkCore.Migrations;

namespace Prid1920_g03.Migrations
{
    public partial class relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    FollowerPseudo = table.Column<int>(nullable: false),
                    FolloweePseudo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => new { x.FollowerPseudo, x.FolloweePseudo });
                    table.ForeignKey(
                        name: "FK_Follows_Users_FolloweePseudo",
                        column: x => x.FolloweePseudo,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_Users_FollowerPseudo",
                        column: x => x.FollowerPseudo,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FolloweePseudo",
                table: "Follows",
                column: "FolloweePseudo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follows");
        }
    }
}
