using IntermediateLab_Backend.Application.DTO;
using IntermediateLab_Backend.Domain.Entities;

namespace IntermediateLab_Backend.Application.Interfaces.Service;

public interface IMemberService
{
	Member Register(RegisterMemberDTO dto);
	bool ExistsEmail(string email);
}