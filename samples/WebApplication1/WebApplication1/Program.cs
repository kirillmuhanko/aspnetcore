using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using WebApplication1.Extensions;
using WebApplication1.Models.Options;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureRequestLocalizationOptions();
builder.Services.RegisterServices(Assembly.GetExecutingAssembly());
builder.Services.RegisterWebOptimizer(builder.Environment);

builder.Services.AddLocalization();
builder.Services.Configure<SystemOptions>(builder.Configuration.GetSection(SystemOptions.SectionName));

builder.Services.Configure<TokenOptions>(
    builder.Configuration.GetSection($"{SecurityOptions.SectionName}:{TokenOptions.SectionName}"));

builder.Services.AddControllersWithViews()    
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRequestLocalizationMiddleware();
app.UseHttpsRedirection();
app.UseWebOptimizer();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
