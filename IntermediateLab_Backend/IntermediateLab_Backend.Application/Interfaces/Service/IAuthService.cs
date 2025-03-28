using IntermediateLab_Backend.Application.DTO;

namespace IntermediateLab_Backend.Application.Interfaces.Service;

public interface IAuthService
{
    public string Login(LoginFormDTO LoginDTO);
    public string refreshToken(string token);

}