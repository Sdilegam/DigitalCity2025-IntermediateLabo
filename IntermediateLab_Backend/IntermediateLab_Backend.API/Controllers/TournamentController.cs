using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using IntermediateLab_Backend.Application.DTO.Tournament;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;
using IntermediateLab_Backend.Infrastructure;
using Newtonsoft.Json;

namespace IntermediateLab_Backend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController(ITournamentService tournamentService) : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAllTournaments()
		{
			GetTournamentsDTO[] tournamentsToReturn = tournamentService.GetAllTournaments();

			return (Ok((tournamentsToReturn)));
		}
		[HttpGet("{tournamentId}")]
		public IActionResult GetTournament([FromRoute] int tournamentId)
		{
			Tournament? tournamentsToReturn = tournamentService.GetTournament(tournamentId);
			if (tournamentsToReturn == null)
				return NotFound();
			return (Ok((tournamentsToReturn)));
		}
		[HttpPost]
		public IActionResult PostTournament(CreateTournamentDTO tournamentDTO)
		{
			try
			{
				Tournament tournamentToCreate = tournamentService.Create(tournamentDTO);
				return (Created("tournament/" + tournamentToCreate.Id, tournamentToCreate));
			}
			catch (SmtpException)
			{
				return (Problem("L'email n'a pas pu etre envoye"));
			}
			catch (Exception e)
			{
				return (Problem((e.Message)));
			}
		}

		[HttpDelete]
		public IActionResult Delete([FromQuery] int id)
		{
			try
			{
				if (tournamentService.Delete(id) == true)
					return (NoContent());
				else
				{
					return (NotFound());
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPatch("{id}/start")]
		public IActionResult StartTournament([FromRoute] int id)
		{
			bool returnValue;
			try
			{
				returnValue = tournamentService.StartTournament(id);
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
			if (!returnValue)
			{
				return (Problem("Le tournoi ne remplis pas les conditions requises pour etre lancé"));
			}
			return (Ok());
		}
		[HttpPatch("{id}/nextRound")]
		public IActionResult NextRound([FromRoute] int id)
		{
			return (Ok());
		}
		
		[Route("inscription/{tournamentId}")]
		[HttpPost]
		public IActionResult RegisterToTournament([FromRoute] int tournamentId)
		{
			GetTournamentsDTO? tournamentToReturn = null;
			
			return Ok(tournamentToReturn = tournamentToReturn);
		}
		[Route("inscription/{tournamentId}")]
		[HttpDelete]
		public IActionResult UnregisterFromTournament([FromRoute] int tournamentId)
		{
			GetTournamentsDTO? tournamentToReturn = null;
			
			return Ok(tournamentToReturn = tournamentToReturn);
		}
		
	}
}
