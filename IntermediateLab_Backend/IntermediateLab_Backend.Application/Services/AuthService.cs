using System.Security.Cryptography;
using System.Text;
using IntermediateLab_Backend.Application.DTO;
using IntermediateLab_Backend.Application.Interfaces.Repositories;
using IntermediateLab_Backend.Application.Interfaces.Security;
using IntermediateLab_Backend.Application.Interfaces.Service;
using IntermediateLab_Backend.Domain.Entities;

namespace IntermediateLab_Backend.Application.Services;

public class AuthService(IMemberRepository memberRepository,ITokenManager tokenManager):IAuthService
{
    public string Login(LoginFormDTO LoginDTO)
    {
        Member? user = memberRepository.FindOneWhere(member=>member.Username == LoginDTO.Username);
        if (user is null)
            throw new Exception("Les informations de connection ne sont pas correctes");
        if(Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(LoginDTO.Password + user.Salt))) != user.Password)
        { // 400 // 401
            throw new Exception("Les informations de connection ne sont pas correctes");
        }
        var tokenToReturn = tokenManager.GenerateToken(user.Id,  user.Email,  user.Role);
        return tokenToReturn;
    }

    public string refreshToken(string token)
    {
        int id = tokenManager.validateTokenWithoutLifetime(token);
        Member? user = memberRepository.FindOne(id);
        if (user is null)
        {
            throw new Exception();
        }
        string newToken = tokenManager.GenerateToken(user.Id, user.Email, user.Role);
        return (newToken);
    }
}
