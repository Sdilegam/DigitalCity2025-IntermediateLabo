using Be.Khunly.EFRepository.Abstraction;
using IntermediateLab_Backend.Domain.Entities;

namespace IntermediateLab_Backend.Application.Interfaces.Repositories;

public interface ITournamentRepository:IRepositoryBase<Tournament>
{
	public List<Tournament> GetAllWithPlayers();
	public Tournament? GetOneWithPlayers(int id);
	public Tournament AddPlayerToTournament(Tournament tournament, Member player);
	public void StartTournament(Tournament tournamentToStart);


}