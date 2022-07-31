using System.Security.Cryptography;
using System.Text;
using WebApplication1.Attributes;
using WebApplication1.Interfaces.Cryptography;

namespace WebApplication1.Cryptography;

[Service]
public class StringEncryptor : IStringEncryptor
{
    private const int VectorSize = 16;

    public string Encrypt(string text, string securityKey)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(securityKey);
        var transform = aes.CreateEncryptor();
        var buffer = Encoding.UTF8.GetBytes(text);
        using var inputStream = new MemoryStream(buffer, false);
        using var outputStream = new MemoryStream();
        outputStream.Write(aes.IV, 0, VectorSize);

        using (var cryptoStream = new CryptoStream(outputStream, transform, CryptoStreamMode.Write))
        {
            inputStream.CopyTo(cryptoStream);
        }

        var bytes = outputStream.ToArray();
        var result = Convert.ToBase64String(bytes);
        return result;
    }

    public string Decrypt(string text, string securityKey)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(securityKey);
        var buffer = Convert.FromBase64String(text);
        using var inputStream = new MemoryStream(buffer, false);
        using var outputStream = new MemoryStream();
        var vector = new byte[VectorSize];
        var unused = inputStream.Read(vector, 0, VectorSize);
        var transform = aes.CreateDecryptor(aes.Key, vector);

        using (var cryptoStream = new CryptoStream(inputStream, transform, CryptoStreamMode.Read))
        {
            cryptoStream.CopyTo(outputStream);
        }

        var bytes = outputStream.ToArray();
        var result = Encoding.UTF8.GetString(bytes);
        return result;
    }
}