using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Domain.Entities;

public class Match
{
    public int Id { get; set; }
    public Tournament Tournament { get; set; }
    public Member BlackPlayer { get; set; }
    public Member WhitePlayer { get; set; }
    public int RoundNumber { get; set; }
    public MatchResultEnum Result { get; set; }
}