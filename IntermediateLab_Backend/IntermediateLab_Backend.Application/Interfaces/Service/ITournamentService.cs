using IntermediateLab_Backend.Application.DTO.Tournament;
using IntermediateLab_Backend.Domain.Entities;

namespace IntermediateLab_Backend.Application.Interfaces.Service;

public interface ITournamentService
{
	public Tournament Create(CreateTournamentDTO DTO);
	public bool       Delete(int                 id);
	public GetTournamentsDTO[] GetAllTournaments();
	public Tournament RegisterToTournament(int memberId, int tournamentId);
	public Tournament? GetTournament(int id);
	public bool StartTournament(int tournamentId);

}