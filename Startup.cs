using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Core.IConfig;
using Proiect.Utilities.JWTUtilis;
using Proiect.Utilities;
using Proiect.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proiect.Configuration;

namespace Proiect
{
    public static class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {

            
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
           
            Configure(app);
            return app;
        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {

            //builder.Services.Configure<JwtConfig>("xcacafhgjhgjfhgjfghjfghjfghjfghjfhgj");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;"));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsImplementationPolicy", builder => builder.WithOrigins("*"));
            });
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IJWTUtils, JWTUtils>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();
            builder.Services.AddScoped<IRentingsService, RentingsService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IUnitofWork, UnitOfWork>();

        }
        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<JWTMiddleware>();
            app.UseAuthorization();
            app.UseCors("MyCorsImplementationPolicy");
            app.MapControllers();
        }
    }
}
