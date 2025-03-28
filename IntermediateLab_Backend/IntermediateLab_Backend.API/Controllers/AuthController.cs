using IntermediateLab_Backend.Application.DTO;
using IntermediateLab_Backend.Application.Interfaces.Security;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Application.Services;
using IntermediateLab_Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntermediateLab_Backend.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormDTO loginDTO)
        {
            try
            {
                string tokenToReturn = authService.Login(loginDTO);
                return Ok(tokenToReturn);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("refreshToken")]
        public IActionResult RefreshToken([FromQuery] string token)
        {
            string tokenToReturn = null!;
            try
            {
                tokenToReturn = authService.refreshToken(token);

            }
            catch (Exception e)
            {
                return Unauthorized();
            }
            return Ok( new {Token = tokenToReturn});
        }
    }
}
