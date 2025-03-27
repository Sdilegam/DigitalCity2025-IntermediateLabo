using IntermediateLab_Backend.Application.Utils;
using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntermediateLab_Backend.Infrastructure.Configs;

public class TournamentConfig: IEntityTypeConfiguration<Tournament>
{
	public void Configure(EntityTypeBuilder<Tournament> builder)
	{
		builder.ToTable(table=> table.HasCheckConstraint("CK_TOURNAMENT_MAX_ELO", "MaxPlayerElo BETWEEN 0 AND 3000"));
		builder.ToTable(table=> table.HasCheckConstraint("CK_TOURNAMENT_MIN_ELO", "MinPlayerElo BETWEEN 0 AND MaxPlayerElo"));
		builder.ToTable(table=> table.HasCheckConstraint("CK_TOURNAMENT_MAX_PLAYERS", "MaxPlayerAmount BETWEEN 2 AND 32"));
		builder.ToTable(table=> table.HasCheckConstraint("CK_TOURNAMENT_MIN_PLAYERS", "MinPlayerAmount BETWEEN 2 AND MaxPlayerAmount"));
		builder.ToTable(table=> table.HasCheckConstraint("CK_TOURNAMENT_END_DATE", "InscriptionsEndDate >= CreationDate"));

		builder.Property(tournament => tournament.Name).HasMaxLength(200);
		builder.Property(tournament => tournament.Location).HasMaxLength(200);
		builder.HasData(new Tournament()
		{
			Id = -1,
			Name = "Tournament From Seed",
			Location = "Bruxelles",
			Categories = TournamentCatEnum.Junior,
			Status = TournamentStatusEnum.WaitingForPlayers,
			CreationDate = DateTime.Now,
			LatestUpdate = DateTime.Now,
			CurrRound = 0,
			InscriptionsEndDate = DateTime.Now.AddDays(25),
			MinPlayerAmount = 2,
			MaxPlayerAmount = 10,
			MinPlayerElo = 100,
			MaxPlayerElo = 3000,
			IsWomenOnly = false,
			Players = []
		});
		
	}
	
}