using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.DTO.Tournament;

public class GetTournamentsDTO
{
    public int Id { get; set; }
    public string   Name                { get; set; } = null!;
    public string?  Location            { get; set; }
    public int  CurrentPlayerNumber            { get; set; }
    public int      MaxPlayerAmount     { get; set; }
    public int      MinPlayerAmount     { get; set; }
    public int?     MaxPlayerElo        { get; set; } = 3000;
    public int?     MinPlayerElo        { get; set; } = 0;
    public int[] Categories { get; set; }
    public TournamentStatusEnum Status { get; set; }
    public DateTime InscriptionsEndDate { get; set; }
    public int CurrentRound { get; set; }
}