using Be.Khunly.EFRepository;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntermediateLab_Backend.Infrastructure.Repositories;

public class MemberRepository(LaboContext context):RepositoryBase<Member>(context), IMemberRepository
{
	
}