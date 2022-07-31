using WebApplication1.Models.Entities;
using WebApplication1.Models.Options;
using WebApplication1.Models.ResultModels;

namespace WebApplication1.Interfaces.Services;

public interface IUserService
{
    void CreatePasswordHash(UserEntity user, string password);

    bool VerifyPasswordHash(UserEntity user, string password);

    string CreateToken(UserEntity user, TokenOptions options);

    TokenResultModel VerifyToken(UserEntity user, string token, TokenOptions options);
}