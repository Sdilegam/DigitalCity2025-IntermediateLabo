using System.Transactions;
using IntermediateLab_Backend.Application.DTO.Tournament;
using IntermediateLab_Backend.Application.Interfaces;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.Services;

public class TournamentService(ITournamentRepository tournamentRepository, IMemberRepository memberRepository,IMailer mailer) : ITournamentService
{
	public GetTournamentsDTO[] GetAllTournaments()
	{
		List<Tournament>  tournamentsToReturn = tournamentRepository.GetAllWithPlayers().OrderByDescending(t=>t.LatestUpdate).Take(10).ToList();
		GetTournamentsDTO[] DTOToReturn = tournamentsToReturn.Select(tournament => new GetTournamentsDTO (){
			Name = tournament.Name,
			Location =  tournament.Location,
			MinPlayerAmount =  tournament.MinPlayerAmount,
			MaxPlayerAmount =   tournament.MaxPlayerAmount,
			Categories = CategoriesEnumToIntArray(tournament.Categories),
			Status = tournament.Status,
			InscriptionsEndDate = tournament.InscriptionsEndDate,
			MaxPlayerElo =  tournament.MaxPlayerElo,
			MinPlayerElo =  tournament.MinPlayerElo,
			CurrentRound = tournament.CurrRound,
			CurrentPlayerNumber = tournament.Players.Count
		}).ToArray();
		return (DTOToReturn);
	}
	public Tournament? GetTournament(int id)
	{
		Tournament? tournament = tournamentRepository.GetOneWithPlayers(id);
		return tournament;
	}
	public Tournament Create(CreateTournamentDTO DTO)
	{
		if (DTO.MaxPlayerElo < 0 || DTO.MaxPlayerElo > 3000)
			throw new ArgumentOutOfRangeException(nameof(DTO.MaxPlayerElo));
		if (DTO.MinPlayerElo < 0 || DTO.MinPlayerElo > DTO.MaxPlayerElo)
			throw new ArgumentOutOfRangeException(nameof(DTO.MinPlayerElo));
		if (DTO.MaxPlayerAmount < 2 || DTO.MaxPlayerAmount > 32)
			throw new ArgumentOutOfRangeException(nameof(DTO.MaxPlayerAmount));
		if (DTO.MinPlayerAmount < 2 || DTO.MinPlayerAmount> DTO.MaxPlayerAmount)
			throw new ArgumentOutOfRangeException(nameof(DTO.MinPlayerAmount));
		DateTime creationDate = DateTime.Now;

		if (DTO.InscriptionsEndDate < creationDate.AddDays(DTO.MaxPlayerAmount))
			throw new ArgumentOutOfRangeException(nameof(DTO.InscriptionsEndDate));
		int categories = DTO.Categories.Sum();
		using TransactionScope transactionScope = new();
		Tournament tournamentToReturn = new()
										{
											Name                = DTO.Name,
											Location            = DTO.Location,
											MinPlayerAmount     = DTO.MinPlayerAmount,
											MaxPlayerAmount     = DTO.MaxPlayerAmount,
											MinPlayerElo        = DTO.MinPlayerElo,
											MaxPlayerElo        = DTO.MaxPlayerElo,
											InscriptionsEndDate = DTO.InscriptionsEndDate,
											CreationDate        = creationDate,
											LatestUpdate        = creationDate,
											Categories          = (TournamentCatEnum)categories,
											// Categories          = DTO.Categories,
											CurrRound           = 0,
											Status              = TournamentStatusEnum.WaitingForPlayers,
											IsWomenOnly         = DTO.IsWomenOnly,
										};
		tournamentToReturn = tournamentRepository.Add(tournamentToReturn);
		
		mailer.Send(["test@test.com"], $"Creation du tournoi {DTO.Name}",
					$"Le tournoi {DTO.Name} vient d'etre cree.<br>Veuillez vous inscrire ci-apres: <a href=\"localhost:5134/tournament/{tournamentToReturn.Id}\">lien d'inscritption</a>", []);
		transactionScope.Complete();
		return (tournamentToReturn);
	}

	public bool Delete(int id)
	{
		Tournament? tournamentToDelete = tournamentRepository.FindOne(id);
		if (tournamentToDelete == null)
			return (false);
		if (tournamentToDelete.Status != TournamentStatusEnum.WaitingForPlayers)
			throw new Exception("You can not delete a tournament that already started");
		using TransactionScope transactionScope = new();
		tournamentRepository.Remove(tournamentToDelete);
		mailer.Send(["test@test.com"], $"Supression du tournoi {tournamentToDelete.Id}", $"Le tournoi {tournamentToDelete.Name} auquel vous vous etes inscris vient d'etre supprime.",[]);
		transactionScope.Complete();
		return (true);
	}
	private int[] CategoriesEnumToIntArray(TournamentCatEnum catEnum)
	{
		int[] flags = catEnum.ToString()
			.Split(new string[] { ", " }, StringSplitOptions.None)
			.Select(i => (int)Enum.Parse(catEnum.GetType(), i))
			.ToArray();
		return flags;
	}

	public Tournament RegisterToTournament(int memberId, int tournamentId)
	{
		Member? member = memberRepository.FindOne(memberId);
		Tournament? tournament = tournamentRepository.GetOneWithPlayers(tournamentId);
		
		if (tournament == null) //le tournoi existe
			throw new Exception();
		if (tournament.Status != TournamentStatusEnum.WaitingForPlayers) //Tournoi n'as pas commencé
			throw new Exception("You can not register a tournament that already started");
		if (tournament.InscriptionsEndDate < DateTime.Now) //la date d'inscription n'est pas dépassée
			throw new Exception("La date d'inscription est dépassée.");
		if (tournament.MaxPlayerAmount <= tournament.Players.Count) //Le nombre max de joureur n'est pas atteint
			throw new Exception("Le nombre maximal de joueur est deja atteint");
		
		
		if (member == null) // Le joueur existe
			throw new Exception();
		if (!tournament.Players.Contains(member)) //le joueur est deja inscrit
			throw new Exception("Le joueur est deja inscrit");
		if (member.Elo < tournament.MinPlayerElo || member.Elo > tournament.MaxPlayerElo) // le joueur n'as pas l'elo requis
			throw new Exception("Le joueur n'as pas l'elo requies pour s'inscrire");
		if (tournament.IsWomenOnly && member.Gender == GenderEnum.Male) // le joueur est un homme dans un tournoi reservé aux femmes
			throw new Exception("Le tournoi est reservé aux femmes et aux genres marginalisés");
		int memberAge = (int)((tournament.InscriptionsEndDate - member.BirthDate).Days / 365.25);
		if (!((tournament.Categories.HasFlag(TournamentCatEnum.Junior) // L'age du joueur ne correspond pas aux categories du tournoi
		     && (memberAge < 18)) 
		    || (tournament.Categories.HasFlag(TournamentCatEnum.Senior) 
		        &&(18 < memberAge && memberAge < 60))
		    || (tournament.Categories.HasFlag(TournamentCatEnum.Veteran) 
		        && (60 < memberAge))))
		{
			throw new Exception("L'age du membre ne correspond a aucune des categories du tournoi");
		}
		return(tournamentRepository.AddPlayerToTournament(tournament, member));
	}

	public bool StartTournament(int tournamentId)
	{
		Tournament? tournamentToStart = tournamentRepository.GetOneWithPlayers(tournamentId);
		if (tournamentToStart == null)
			throw new Exception("Le tournoi n'as pas été trouvé");
		if (tournamentToStart.Status != TournamentStatusEnum.WaitingForPlayers)
			throw new Exception("le tournoi a deja commencé");
		if (tournamentToStart.Players.Count < tournamentToStart.MinPlayerAmount)
			return false;
		if (tournamentToStart.InscriptionsEndDate > DateTime.Now)
			return false;
		
		tournamentRepository.StartTournament(tournamentToStart);
		return true;
	}

}