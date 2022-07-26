namespace WebApplication1.Models.Options;

public class SystemOptions
{
    public const string SectionName = "System";

    public List<string> SupportedCultures { get; set; } = null!;
}