using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.DTO.Tournament;

public record CreateTournamentDTO
{
	public string            Name                { get; set; } = null!;
	public string?           Location            { get; set; }
	public DateTime          InscriptionsEndDate { get; set; }
	public int               MaxPlayerAmount     { get; set; }
	public int               MinPlayerAmount     { get; set; }
	public int?               MaxPlayerElo        { get; set; } = 3000;
	public int?               MinPlayerElo        { get; set; } = 0;
	public TournamentCatEnum Categories          { get; set; }
	public bool              IsWomenOnly         { get; set; } = false;
};