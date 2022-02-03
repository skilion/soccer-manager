using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoccerManager;
using SoccerManager.Models;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace SoccerManagerIntegrationTests
{
    public class IntegrationTestBase
    {
        protected HttpClient Client;

        private readonly SqliteConnection connection;
        private readonly JsonSerializerOptions jsonOptions;

        public IntegrationTestBase()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var application = CreateWebApplication();
            Client = application.CreateClient();
            jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };
        }

        protected async Task RegisterUser(RegisterRequest request)
        {
            var response = await Client.PostAsJsonAsync("/user/register", request, jsonOptions);
            response.EnsureSuccessStatusCode();
        }

        protected async Task<AuthResponse> AuthenticateUser(AuthRequest request)
        {
            var response = await Client.PostAsJsonAsync("/user/authenticate", request, jsonOptions);
            response.EnsureSuccessStatusCode();
            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>(jsonOptions);
            Assert.NotNull(authResponse);
            return authResponse!;
        }

        protected async Task<Team> GetTeam(string bearer)
        {
            SetAuthorization(bearer);
            var response = await Client.GetAsync("/team");
            response.EnsureSuccessStatusCode();
            var teamResponse = await response.Content.ReadFromJsonAsync<Team>(jsonOptions);
            Assert.NotNull(teamResponse);
            return teamResponse!;
        }

        protected async Task BuyPlayer(string bearer, int playerId)
        {
            SetAuthorization(bearer);
            var response = await Client.PostAsync($"/player/{playerId}/buy", null);
            response.EnsureSuccessStatusCode();
        }

        protected async Task SellPlayer(string bearer, int playerId, int askPrice)
        {
            SetAuthorization(bearer);
            var response = await Client.PostAsJsonAsync($"/player/{playerId}/sell", new SellRequest() { AskPrice = askPrice });
            response.EnsureSuccessStatusCode();
        }

        private void SetAuthorization(string bearer)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        }

        private WebApplicationFactory<Program> CreateWebApplication()
        {
            var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    ReplaceDbWithInMemorySQLite(services);
                    EnsureDbExists(services);
                });
            });
            return application;
        }

        private void ReplaceDbWithInMemorySQLite(IServiceCollection services)
        {
            var descriptor = services.Single(service => service.ServiceType == typeof(DbContextOptions<SoccerManagerDbContext>));
            services.Remove(descriptor);
            services.AddDbContext<SoccerManagerDbContext>(options => options.UseSqlite(connection));
        }

        private static void EnsureDbExists(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<SoccerManagerDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
