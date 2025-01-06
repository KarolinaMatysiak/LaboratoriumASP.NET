using Microsoft.DotNet.Scaffolding.Shared;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.University;

//using WebApp.Models.Services;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<AppDbContex>();
        builder.Services.AddDbContext<UniversityDbContext>(op =>
        {
            op.UseSqlite((builder.Configuration["UniversityDatabse:ConnectionString"]));
        });

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequiredLength = 5;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;

        })
            
        .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContex>();
        
        builder.Services.AddTransient<IKomputerService, EFKomputerService>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
            
        builder.Services.AddSingleton<IDateTimeProvider, CurrentDateTimeProvider>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();


        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}