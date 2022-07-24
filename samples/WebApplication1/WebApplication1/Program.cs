using System.Reflection;
using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesByScanning(Assembly.GetExecutingAssembly());
builder.Services.AddWebOptimizer(builder.Environment);
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseWebOptimizer();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
