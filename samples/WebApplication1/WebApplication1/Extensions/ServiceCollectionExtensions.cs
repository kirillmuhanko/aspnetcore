using NUglify.Css;
using NUglify.JavaScript;

namespace WebApplication1.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWebOptimizer(this IServiceCollection collection, IWebHostEnvironment environment)
    {
        var isDevelopment = environment.IsDevelopment();
        var cssSettings = isDevelopment ? CssSettings.Pretty() : new CssSettings();
        var codeSettings = isDevelopment ? CodeSettings.Pretty() : new CodeSettings();

        collection.AddWebOptimizer(assetPipeline =>
        {
            assetPipeline.AddCssBundle("/css/site.css", cssSettings, "/css/**/*.css");
            assetPipeline.AddJavaScriptBundle("/js/site.js", codeSettings, "/js/**/*.js");
        });
    }
}