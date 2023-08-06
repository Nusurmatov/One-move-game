using System.Security.Cryptography;
using System.Text;

public class HmacGenerator
{
    public static string GeneratRandomKey()
    {
        using var generator = RandomNumberGenerator.Create();
        var key = new byte[32];
        generator.GetBytes(key);
        return BitConverter.ToString(key).Replace("-", "");
    }

    public static string GenerateHmac(string key, string message)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}