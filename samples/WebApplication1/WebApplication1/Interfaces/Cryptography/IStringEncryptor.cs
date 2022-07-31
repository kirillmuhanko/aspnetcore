namespace WebApplication1.Interfaces.Cryptography;

public interface IStringEncryptor
{
    string Encrypt(string text, string securityKey);

    string Decrypt(string text, string securityKey);
}