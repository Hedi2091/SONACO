using Microsoft.EntityFrameworkCore;
using MonApplicationMVC.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "UserAuthCookie";
        config.LoginPath = "/Account/Login";
    });
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     new MySqlServerVersion(new Version(8, 0, 23)))); // Remplace 8, 0, 23 par ta version de MySQL


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "lotIndex", // Un nom spécifique à cette route
    pattern: "Lots/Index", // Cela fera en sorte que l'action Index du LotController soit accessible à l'URL /Lots/Index
    defaults: new { controller = "Lot", action = "Index" } // Assurez-vous de spécifier le contrôleur et l'action
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
