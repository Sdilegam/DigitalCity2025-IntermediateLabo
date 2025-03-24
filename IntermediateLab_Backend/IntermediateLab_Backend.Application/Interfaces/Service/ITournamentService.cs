using IntermediateLab_Backend.Application.DTO.Tournament;
using IntermediateLab_Backend.Domain.Entities;

namespace IntermediateLab_Backend.Application.Interfaces.Service;

public interface ITournamentService
{
	public Tournament Create(CreateTournamentDTO DTO);
	public bool       Delete(int                 id);
}