using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public UserRoles Roles { get; set; }
}