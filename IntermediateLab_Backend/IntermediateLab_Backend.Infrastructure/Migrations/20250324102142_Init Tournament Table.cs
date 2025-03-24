using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntermediateLab_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitTournamentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ELO",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaxPlayerAmount = table.Column<int>(type: "int", nullable: false),
                    MinPlayerAmount = table.Column<int>(type: "int", nullable: false),
                    CurrRound = table.Column<int>(type: "int", nullable: false),
                    MaxPlayerElo = table.Column<int>(type: "int", nullable: true),
                    MinPlayerElo = table.Column<int>(type: "int", nullable: true),
                    Categories = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsWomenOnly = table.Column<bool>(type: "bit", nullable: false),
                    InscriptionsEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.CheckConstraint("CK_TOURNAMENT_END_DATE", "InscriptionsEndDate >= CreationDate");
                    table.CheckConstraint("CK_TOURNAMENT_MAX_ELO", "MaxPlayerElo BETWEEN 0 AND 3000");
                    table.CheckConstraint("CK_TOURNAMENT_MAX_PLAYERS", "MaxPlayerAMOUNT BETWEEN 2 AND 32");
                    table.CheckConstraint("CK_TOURNAMENT_MIN_ELO", "MinPlayerElo BETWEEN 0 AND MaxPlayerElo");
                    table.CheckConstraint("CK_TOURNAMENT_MIN_PLAYERS", "MinPlayerAMOUNT BETWEEN 2 AND MaxPlayerAmount");
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_MEMBER_ELO",
                table: "Members",
                sql: "Elo BETWEEN 0 AND 3000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_MEMBER_ELO",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ELO",
                table: "Members",
                sql: "Elo BETWEEN 0 AND 3000");
        }
    }
}
