using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntermediateLab_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addingtournamentseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Categories", "CreationDate", "CurrRound", "InscriptionsEndDate", "IsWomenOnly", "LatestUpdate", "Location", "MaxPlayerAmount", "MaxPlayerElo", "MinPlayerAmount", "MinPlayerElo", "Name", "Status" },
                values: new object[] { -1, 1, new DateTime(2025, 3, 27, 15, 32, 39, 351, DateTimeKind.Local).AddTicks(6132), 0, new DateTime(2025, 4, 21, 15, 32, 39, 355, DateTimeKind.Local).AddTicks(2161), false, new DateTime(2025, 3, 27, 15, 32, 39, 355, DateTimeKind.Local).AddTicks(1499), "Bruxelles", 10, 3000, 2, 100, "Tournament From Seed", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
