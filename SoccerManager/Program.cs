using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SoccerManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AddServices(builder);
            var app = builder.Build();
            RegisterMiddleware(app);
            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add SQLite as database context
            builder.Services.AddDbContext<SoccerManagerDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"))
            );

            AddSwaggerGen(builder);
        }

        private static void AddSwaggerGen(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                // Add documentation from XML comments
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        private static void RegisterMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                EnsureDbExists(app);
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();
            app.UseHttpLogging();
        }

        private static void EnsureDbExists(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<SoccerManagerDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
