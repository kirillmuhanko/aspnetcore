using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureRequestLocalizationOptions();
builder.Services.RegisterServices(Assembly.GetExecutingAssembly());
builder.Services.RegisterWebOptimizer(builder.Environment);
builder.Services.AddLocalization();
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
