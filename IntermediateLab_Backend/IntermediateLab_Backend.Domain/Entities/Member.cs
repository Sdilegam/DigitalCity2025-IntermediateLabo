using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Domain.Entities;

public class Member
{
	public int        Id        { get; set; }
	public int        Elo       { get; set; }
	public string     Username  { get; set; } = null!;
	public string     Email     { get; set; } = null!;
	public string     Password  { get; set; } = null!;
	public Guid       Salt      { get; set; }
	public DateTime   BirthDate { get; set; }
	public RoleEnum   Role      { get; set; }
	public GenderEnum Gender    { get; set; }
	public List<Tournament> Tournaments { get; set; }
}