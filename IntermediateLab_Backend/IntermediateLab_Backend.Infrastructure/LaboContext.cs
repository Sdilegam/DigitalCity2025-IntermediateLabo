using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace IntermediateLab_Backend.Infrastructure;

/*
 * sh command to run sqlServer
 * docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password_123#" -p 1433:1433 --name SQLServer1 --hostname SQLServer1 -d mcr.microsoft.com/mssql/server:2022-latest
 * * docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password_123#" -p 1433:1433 b 
 */

public class LaboContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Member> Members { get; set; }
	public DbSet<Tournament> Tournaments { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new MemberConfig());
		modelBuilder.ApplyConfiguration(new TournamentConfig());
		
	}
}

