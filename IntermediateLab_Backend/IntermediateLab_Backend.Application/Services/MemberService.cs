using System.Transactions;
using IntermediateLab_Backend.Application.DTO;
using IntermediateLab_Backend.Application.DTO.Member;
using IntermediateLab_Backend.Application.Exceptions;
using IntermediateLab_Backend.Application.Interfaces;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Application.Utils;
using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.Services;

public class MemberService(IMemberRepository memberRepository, IMailer mailer): IMemberService
{
	public Member Register(RegisterMemberDTO memberDTO)
	{
		Member memberToReturn;
		// Verifier email unique
		if (memberRepository.Any(member => member.Email == memberDTO.Email))
			throw new DuplicatePropertyException(nameof(memberDTO.Email));
		// Verifier username unique
		if (memberRepository.Any(member => member.Username == memberDTO.Username))
			throw new DuplicatePropertyException(nameof(memberDTO.Username));
		// Creer un mot de passe pour le membre
		string password = PasswordUtils.GeneratePassword(10);
		Guid salt = Guid.NewGuid();
		string hashedPassword = PasswordUtils.HashPassword(password, salt);
		// inserer le membre
		using TransactionScope transactionScope = new();
		memberToReturn = memberRepository.Add(new Member
							 {
								 Username  = memberDTO.Username,
								 Email     = memberDTO.Email,
								 Password  = hashedPassword,
								 Salt      = salt,
								 BirthDate = memberDTO.BirthDate,
								 Role      = RoleEnum.Member,
								 Gender    = memberDTO.Gender,
								 Elo       = memberDTO.Elo ?? 1200,
							 });
		// envoyer un email au membre
		mailer.Send([memberToReturn.Email], 
					"Bienvenue sur ChessTournament.Com",
					$"Vos informations de connections sont:<br>" +
					$"- Pseudo: {memberToReturn.Username}<br>"   +
					$"- Mot de passe: {password}"
					);
		transactionScope.Complete();
		return (memberToReturn);
	}

	public bool ExistsEmail(string email)
	{
		var toReturn = memberRepository.Any(member => member.Email == email);
		return (toReturn);
	}

	public Member[] GetTournamentMembers(int tournamentID)
	{
		return (memberRepository.FindWhere(member => member.Tournaments.Any(tournament => tournament.Id == tournamentID)).ToArray());
	}

	public Tournament? GetTournament(int id)
	{
		throw new NotImplementedException();
	}
}