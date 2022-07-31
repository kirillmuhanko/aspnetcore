using WebApplication1.Models.JsonModels;

namespace WebApplication1.Models.ResultModels;

public class TokenResultModel
{
    public bool IsValid { get; set; }

    public TokenJsonModel? JsonModel { get; set; }
}