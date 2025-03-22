using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntermediateLab_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.CheckConstraint("CK_ELO", "Elo BETWEEN 0 AND 3000");
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BirthDate", "Elo", "Email", "Gender", "Password", "Role", "Salt", "Username" },
                values: new object[] { 1, new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, "test@gmail.com", 0, "��O)�a��=o�bz�\n٠į8����P����\n'�:�Q-n��h��p�Ɋ����>A��", 0, new Guid("00000000-0000-0000-0000-000000000000"), "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Salt",
                table: "Members",
                column: "Salt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Username",
                table: "Members",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
