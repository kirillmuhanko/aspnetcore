namespace WebApplication1.Models.Options;

public class TokenOptions
{
    public const string SectionName = "Token";

    public string SecurityKey { get; set; } = null!;

    public int ExpirationDays { get; set; }
}