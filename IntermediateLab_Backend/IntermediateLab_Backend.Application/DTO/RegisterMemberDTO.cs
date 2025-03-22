using System.ComponentModel.DataAnnotations;
using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.DTO;

public class RegisterMemberDTO
{
	[MaxLength(100)]
	public string Username { get; set; } = null!;

	[MaxLength(400)]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Range(0, 3000)]
	public int? Elo { get; set; }

	public DateTime   BirthDate { get; set; }
	public GenderEnum Gender    { get; set; }
}