using System.ComponentModel.DataAnnotations;

namespace IntermediateLab_Backend.Application.DTO;

public class LoginFormDTO
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}