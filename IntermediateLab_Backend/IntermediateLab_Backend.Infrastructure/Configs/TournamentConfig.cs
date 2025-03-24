using IntermediateLab_Backend.Domain.Entities;
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
		
	}
	
}