using Microsoft.Extensions.Options;

namespace WebApplication1.Extensions;

public static class WebApplicationExtensions
{
    public static void UseRequestLocalizationMiddleware(this WebApplication application)
    {
        var options = application.Services.GetService<IOptions<RequestLocalizationOptions>>();
        application.UseRequestLocalization(options!.Value);
    }
}