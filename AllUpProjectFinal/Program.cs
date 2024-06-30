using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Core.RepositoryAbstract;
using Data.DAL;
using Data.RepositoryConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AllUpProjectFinal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();


        builder.Services.AddScoped<ITagRepository, TagRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
        builder.Services.AddScoped<IBlogRepository, BlogRepository>();
        builder.Services.AddScoped<ISettingRepository, SettingRepository>();
        builder.Services.AddScoped<ISliderRepository,SliderRepository>();


        builder.Services.AddScoped<ITagService, TagService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IBlogService, BlogService>();
        builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
        builder.Services.AddScoped<ISettingService, SettingService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IEmailService,EmailService>();
        builder.Services.AddScoped<ISliderService, SliderService>();
        builder.Services.AddScoped<ILayoutService,LayoutService>();

        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();



        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;

            opt.SignIn.RequireConfirmedEmail = true;
            opt.Lockout.AllowedForNewUsers = false;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 3;

        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}