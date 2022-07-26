using System.Reflection;
using NUglify.Css;
using NUglify.JavaScript;
using WebApplication1.Attributes;

namespace WebApplication1.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterWebOptimizer(this IServiceCollection collection, IWebHostEnvironment environment)
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

    public static void RegisterServices(this IServiceCollection collection, Assembly assembly)
    {
        var types = assembly.GetTypes();
        bool Filter(Type t) => t.IsDefined(typeof(ServiceAttribute)) && t.IsClass && !t.IsAbstract;
        var implementations = types.Where(Filter).ToList();

        implementations.ForEach(i =>
        {
            var attribute = i.GetCustomAttribute<ServiceAttribute>();
            var contracts = i.GetInterfaces().Where(t => t != typeof(IDisposable)).ToList();

            contracts.ForEach(c =>
            {
                switch (attribute?.Lifetime)
                {
                    case ServiceLifetime.Transient:
                        collection.AddTransient(c, i);
                        break;
                    case ServiceLifetime.Scoped:
                        collection.AddScoped(c, i);
                        break;
                    default:
                        collection.AddSingleton(c, i);
                        break;
                }
            });
        });
    }
}