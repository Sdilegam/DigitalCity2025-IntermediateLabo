using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntermediateLab_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tournamentmembercrosstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TOURNAMENT_MIN_PLAYERS",
                table: "Tournaments");

            migrationBuilder.CreateTable(
                name: "MemberTournament",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTournament", x => new { x.PlayersId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_MemberTournament_Members_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberTournament_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_TOURNAMENT_MIN_PLAYERS",
                table: "Tournaments",
                sql: "MinPlayerAmount BETWEEN 2 AND MaxPlayerAmount");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTournament_TournamentsId",
                table: "MemberTournament",
                column: "TournamentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberTournament");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TOURNAMENT_MIN_PLAYERS",
                table: "Tournaments");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TOURNAMENT_MIN_PLAYERS",
                table: "Tournaments",
                sql: "MinPlayerAMOUNT BETWEEN 2 AND MaxPlayerAmo");
        }
    }
}
