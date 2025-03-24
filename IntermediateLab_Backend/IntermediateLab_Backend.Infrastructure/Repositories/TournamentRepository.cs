using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Domain.Entities;
using Khun.ToolBox.EF.Repository;

namespace IntermediateLab_Backend.Infrastructure.Repositories;

public class TournamentRepository(LaboContext context): RepositoryBase<Tournament>(context), ITournamentRepository
{
	
}