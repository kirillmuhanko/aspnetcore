using System.Text.Json.Serialization;

namespace WebApplication1.Models.JsonModels;

public class TokenJsonModel
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("exp")] public DateTime Expires { get; set; }
}