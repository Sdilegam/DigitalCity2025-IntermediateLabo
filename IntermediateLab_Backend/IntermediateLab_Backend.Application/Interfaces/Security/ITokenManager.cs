using IntermediateLab_Backend.Domain.Enums;

namespace IntermediateLab_Backend.Application.Interfaces.Security;

public interface ITokenManager
{
    string GenerateToken(int id, string Email, RoleEnum role);
    int validateTokenWithoutLifetime(string token);
}