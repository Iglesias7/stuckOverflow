using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prid1920_g03.Migrations
{
    public partial class role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Pseudo = table.Column<string>(maxLength: 10, nullable: false),
                    Password = table.Column<string>(maxLength: 10, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Reputation = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "Password", "Pseudo", "Reputation", "Role" },
                values: new object[,]
                {
                    { 1, null, "ben@test.com", "Benoît Penelle", null, "ben", "ben", 0, 2 },
                    { 2, null, "bruno@test.com", "Bruno Lacroix", null, "bruno", "bruno", 0, 0 },
                    { 3, null, "iglesias@test.com", "iglesias Chendjou", null, "iglesias", "iglesias", 0, 0 },
                    { 4, null, "merveil@test.com", "merveil Nzitusu", null, "bruno", "merveil", 0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Pseudo",
                table: "Users",
                column: "Pseudo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
