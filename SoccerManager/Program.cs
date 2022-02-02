using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoccerManager.Helpers;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

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
            builder.Services.AddSingleton<ITeamGenerator, TeamGenerator>();
            builder.Services
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // Add SQLite as database context
            builder.Services.AddDbContext<SoccerManagerDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"))
            );

            AddSwaggerGen(builder);
            AddJwtBearer(builder);
        }

        private static void AddSwaggerGen(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                // Add documentation from XML comments
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                
                // Add JWT Authorization
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static void AddJwtBearer(WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:IssuerSigningKey"]))
                        };
                    }
                );
        }

        private static void RegisterMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
                EnsureDbExists(app);
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();
            app.UseHttpsRedirection();
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
