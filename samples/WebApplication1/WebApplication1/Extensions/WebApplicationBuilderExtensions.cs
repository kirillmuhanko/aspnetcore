using System.Globalization;
using Microsoft.AspNetCore.Localization;
using WebApplication1.Models.Options;

namespace WebApplication1.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureRequestLocalizationOptions(this WebApplicationBuilder builder)
    {
        var options = new SystemOptions();
        builder.Configuration.GetSection(SystemOptions.SectionName).Bind(options);
        var supportedCultures = options.SupportedCultures.Select(CultureInfo.GetCultureInfo).ToList();
        var defaultCulture = supportedCultures.FirstOrDefault() ?? new CultureInfo("en-US");

        builder.Services.Configure<RequestLocalizationOptions>(t =>
        {
            t.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);
            t.SupportedCultures = supportedCultures;
            t.SupportedUICultures = supportedCultures;
            t.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            };
        });
    }
}