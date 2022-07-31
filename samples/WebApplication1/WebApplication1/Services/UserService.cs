using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WebApplication1.Attributes;
using WebApplication1.Interfaces.Cryptography;
using WebApplication1.Interfaces.Services;
using WebApplication1.Models.Entities;
using WebApplication1.Models.JsonModels;
using WebApplication1.Models.Options;
using WebApplication1.Models.ResultModels;

namespace WebApplication1.Services;

[Service]
public class UserService : IUserService
{
    private readonly IStringEncryptor _stringEncryptor;

    public UserService(IStringEncryptor stringEncryptor)
    {
        _stringEncryptor = stringEncryptor;
    }

    public void CreatePasswordHash(UserEntity user, string password)
    {
        using var hmac = new HMACSHA256();
        user.PasswordSalt = hmac.Key;
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool VerifyPasswordHash(UserEntity user, string password)
    {
        using var hmac = new HMACSHA256(user.PasswordSalt);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var result = hash.SequenceEqual(user.PasswordHash);
        return result;
    }

    public string CreateToken(UserEntity user, TokenOptions options)
    {
        var model = new TokenJsonModel
        {
            Id = user.Id,
            Expires = DateTime.UtcNow.AddDays(options.ExpirationDays)
        };

        var json = JsonSerializer.Serialize(model);
        var result = _stringEncryptor.Encrypt(json, options.SecurityKey);
        return result;
    }

    public TokenResultModel VerifyToken(UserEntity user, string token, TokenOptions options)
    {
        var json = _stringEncryptor.Decrypt(token, options.SecurityKey);
        var model = JsonSerializer.Deserialize<TokenJsonModel>(json);

        var result = new TokenResultModel
        {
            IsValid = user.Id == model?.Id && model.Expires > DateTime.UtcNow,
            JsonModel = model
        };

        return result;
    }
}