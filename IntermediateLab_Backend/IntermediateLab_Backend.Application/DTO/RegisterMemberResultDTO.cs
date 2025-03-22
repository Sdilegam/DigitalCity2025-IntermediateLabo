using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.DTO;

public class RegisterMemberResultDTO(Member member)
{
	public int        Id       { get; set; } = member.Id;
	public string     Username { get; set; } = member.Username;
	public string     Email    { get; set; } = member.Email;
	public GenderEnum Gender   { get; set; } = member.Gender;
	public DateTime   Birthday { get; set; } = member.BirthDate;
	public RoleEnum   Role     { get; set; } = member.Role;
}