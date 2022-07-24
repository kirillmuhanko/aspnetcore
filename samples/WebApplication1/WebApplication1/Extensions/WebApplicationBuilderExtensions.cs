using System.Globalization;
using Microsoft.AspNetCore.Localization;
using WebApplication1.Models.Options;

namespace WebApplication1.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureRequestLocalizationOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CultureOptions>(builder.Configuration.GetSection(CultureOptions.Config));
        var options = new CultureOptions();
        builder.Configuration.GetSection(CultureOptions.Config).Bind(options);
        var supportedCultures = options.Names.Select(t => new CultureInfo(t)).ToList();
        var defaultCulture = supportedCultures.First();

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