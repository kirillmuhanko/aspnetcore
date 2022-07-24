using System.Reflection;
using NUglify.Css;
using NUglify.JavaScript;
using WebApplication1.Attributes;

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

    public static void AddServicesByScanning(this IServiceCollection collection, Assembly assembly)
    {
        var types = assembly.GetTypes();
        bool Filter(Type t) => t.IsDefined(typeof(ServiceAttribute)) && t.IsClass && !t.IsAbstract;
        var implementations = types.Where(Filter).ToList();

        implementations.ForEach(t =>
        {
            var attribute = t.GetCustomAttribute<ServiceAttribute>();
            var contract = t.GetInterfaces().FirstOrDefault();

            if (contract == null)
                throw new ApplicationException($"The {t.Name} class must implement the interface.");

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (attribute?.Lifetime)
            {
                case ServiceLifetime.Scoped:
                    collection.AddScoped(contract, t);
                    break;
                case ServiceLifetime.Transient:
                    collection.AddTransient(contract, t);
                    break;
                default:
                    collection.AddSingleton(contract, t);
                    break;
            }
        });
    }
}