using Be.Khunly.EFRepository;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntermediateLab_Backend.Infrastructure.Repositories;

public class TournamentRepository(LaboContext context): RepositoryBase<Tournament>(context), ITournamentRepository
{
	public List<Tournament> GetAllWithPlayers()
	{
		return context.Tournaments.Include(t=>t.Players).ToList();
	}

	public Tournament? GetOneWithPlayers(int id)
	{
		return context.Tournaments.Include(t => t.Players).SingleOrDefault(t => t.Id == id);
	}

	public Tournament AddPlayerToTournament(Tournament tournament, Member player)
	{
		tournament.Players.Add(player);
		context.SaveChanges();
		return tournament;
	}

	public void StartTournament(Tournament tournamentToStart)
	{
		tournamentToStart.CurrRound = 1;
		tournamentToStart.LatestUpdate = DateTime.Now;
		context.SaveChanges();
	}
}