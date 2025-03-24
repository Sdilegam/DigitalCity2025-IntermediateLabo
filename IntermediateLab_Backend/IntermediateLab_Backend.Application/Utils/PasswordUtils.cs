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


	public static string GeneratePassword(int length = 10)
	{
		string result = string.Empty;
		for (int i = 0; i < length; i++)
		{
			char letter = (char)new Random().Next(65, 91);
			result += letter;
		}
		return result;
	}
}