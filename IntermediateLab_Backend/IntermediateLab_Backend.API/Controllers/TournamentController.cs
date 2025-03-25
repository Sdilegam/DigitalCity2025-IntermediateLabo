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
		public IActionResult Get()
		{
			GetTournamentsDTO[] tournamentsToReturn = tournamentService.Get();

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

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] int id)
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


	}
}