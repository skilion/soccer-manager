using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddSwaggerGen();

            // Add SQLite as database context
            builder.Services.AddDbContext<SoccerManagerDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"))
            );
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
            app.MapControllers();
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
