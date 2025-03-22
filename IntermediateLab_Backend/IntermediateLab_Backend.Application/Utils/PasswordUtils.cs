using System.Security.Cryptography;
using System.Text;

namespace IntermediateLab_Backend.Application.Utils;

public class PasswordUtils
{
	public static string HashPassword(in string password, in Guid salt)
	{
		string hashedPassword      = null!;
		byte[] saltedPasswordArray = Encoding.UTF8.GetBytes(password + salt);
		hashedPassword = Encoding.UTF8.GetString(SHA512.HashData(saltedPasswordArray));
		return (hashedPassword);
	}

	public static string GeneratePassword(in int lenght)
	{
		string generatedPassword = string.Empty;
		return (generatedPassword);
	}
}