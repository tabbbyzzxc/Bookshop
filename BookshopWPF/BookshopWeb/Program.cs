using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BookshopWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseNpgsql(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=159874;Database=ProductDb;");
            });


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}