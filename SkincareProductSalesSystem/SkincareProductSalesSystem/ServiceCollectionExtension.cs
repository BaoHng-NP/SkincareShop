
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.BLL.Services;
using System.BLL.UnitOfWorks;
using System.DAL;
using System.DAL.Repositories;

namespace FUNewsManagementSystem
{
    public static class ServiceCollectionExtension
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SkincareShopContext>
            (
              option => option.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
            );
        }
        public static void AddRepositoryUOW(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductService, ProductService>();


        }
        public static void AddAuthen(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.LoginPath = "/SystemAccounts/Login";
         options.AccessDeniedPath = "/SystemAccounts/AccessDenied";
     });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
                options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
            });

        }
    }
}
