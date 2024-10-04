using Weight_Scales.Configuration;
using Weight_Scales.Services;

namespace Weight_Scales
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Bind configuration

            // Register dapperQuery
            builder.Services.AddScoped<dapperQuery>();

            // Register the ConnectionService as a singleton
            builder.Services.AddSingleton<IConnectionService, ConnectionService>();

            //builder.Services.Configure<conStr>(options =>
            //{
            //    options.dbCon = ""; // Initial connection string if needed
            //});

            builder.Services.AddControllersWithViews();

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