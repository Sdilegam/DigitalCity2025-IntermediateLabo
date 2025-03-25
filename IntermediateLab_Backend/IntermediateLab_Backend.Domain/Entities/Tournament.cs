using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Domain.Entities;

/*
 * ◦ 1 nom
   ◦ 1 lieu (nullable)
   ◦ 1 nombre minimum de joueurs (2-32)
   ◦ 1 nombre maximum de joueurs (2-32)
   ◦ 1 ELO minimum (0-3000, nullable)
   ◦ 1 ELO maximum (0-3000, nullable)
   ◦ 1 ou plusieurs catégories (junior, senior, veteran)
   ◦ 1 statut (en attente de joueurs, en cours, terminé)
   ◦ 1 numéro correspondant à la ronde courante
   ◦ 1 booléen (WomenOnly) qui détermine si le tournoi n’est autorisé qu’aux filles
   ◦ 1 date de fin des inscriptions
   ◦ 1 date de création
   ◦ 1 date de mise à jour
 */
public class Tournament
{
	public int                  Id                  { get; set; }
	public string               Name                { get; set; } = null!;
	public string?              Location            { get; set; }
	public int                  MaxPlayerAmount     { get; set; }
	public int                  MinPlayerAmount     { get; set; }
	public int                  CurrRound           { get; set; }
	public int?                 MaxPlayerElo        { get; set; }
	public int?                 MinPlayerElo        { get; set; }
	public TournamentCatEnum    Categories          { get; set; }
	public TournamentStatusEnum Status              { get; set; }
	public bool                 IsWomenOnly         { get; set; }
	public DateTime             InscriptionsEndDate { get; set; }
	public DateTime             CreationDate        { get; set; }
	public DateTime             LatestUpdate        { get; set; }
	public Member[] Players { get; set; }
}