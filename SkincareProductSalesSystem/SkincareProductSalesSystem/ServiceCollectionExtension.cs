
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
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductDetailService, ProductDetailService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ISkinQuizQuestionService, SkinQuizQuestionsService>();
            services.AddScoped<ISkinTypeService, SkinTypeService>();
            services.AddScoped<IUserSkinTestService, UserSkinTestService>();
            services.AddScoped<ISkinQuizAnswerService, SkinQuizAnswerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IUserVourcherService, UserVourcherService>();
            services.AddScoped<DashboardService>();
            services.AddScoped<DashboardRepository>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IContentRepository, ContentRepository>();





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

            // Session để lưu cart
            services.AddDistributedMemoryCache(); // Bộ nhớ session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(24); // Thời gian tồn tại của session
                options.Cookie.HttpOnly = true; // Chỉ truy cập qua HTTP
                options.Cookie.IsEssential = true; // Quan trọng với hệ thống
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        }
    }
}
