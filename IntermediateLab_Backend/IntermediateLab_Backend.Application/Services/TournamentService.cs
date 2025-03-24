using System.Transactions;
using IntermediateLab_Backend.Application.DTO.Tournament;
using IntermediateLab_Backend.Application.Interfaces;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.Services;

public class TournamentService(ITournamentRepository tournamentRepository, IMailer mailer) : ITournamentService
{
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
											Categories          = DTO.Categories,
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
}