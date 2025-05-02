using Microsoft.AspNetCore.Authentication.Cookies;
using Projet_mvc.Core.Infrastructure;
using Projet_mvc.Core.Repository;
using Projet_mvc.Core.Services;

namespace Projet_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IDbConnectionProvider, PGSqlDbConnectionProvider>();
            builder.Services.AddScoped<IListingRepository, DapperListingRepository>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IUserRepository, DapperUserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IImageRepository, DapperImageRepository>();
            builder.Services.AddScoped<ITagRepository, DapperTagsRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configuration de l'authentification par cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromDays(14);
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateProjectPolicy",
                    policy => policy.RequireRole("Admin", "User"));
            });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
