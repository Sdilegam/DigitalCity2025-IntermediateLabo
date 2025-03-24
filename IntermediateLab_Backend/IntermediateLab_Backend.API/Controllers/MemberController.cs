using System.Net.Mail;
using IntermediateLab_Backend.Application.DTO;
using IntermediateLab_Backend.Application.DTO.Member;
using IntermediateLab_Backend.Application.Exceptions;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IntermediateLab_Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController(IMemberService memberService) : ControllerBase
{
	[HttpPost]
	public IActionResult Post([FromBody] RegisterMemberDTO memberDTO)
	{
		try
		{
			Member member = memberService.Register(memberDTO);
			return Created("member/" + member.Id, new RegisterMemberResultDTO(member));
		}
		catch (DuplicatePropertyException ex)
		{
			return (Conflict(ex.Message));
		}
		catch (SmtpException)
		{
			return (Problem("L'email n'a pas pu etre envoye"));
		}
	}

	[HttpHead]
	public IActionResult Head([FromQuery] string email)
	{
		if (memberService.ExistsEmail(email))
			return (Ok());
		return (NotFound());
	}
}