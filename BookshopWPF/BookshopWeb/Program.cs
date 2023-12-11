using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Bookshop.ProductsLib;

namespace BookshopWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ProductDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ProductDbContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseNpgsql(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=159874;Database=ProductDb;");
            });

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProductDbContext>();

            /*builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ProductDbContext>();*/

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddRouting();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}