using SoccerManager.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerManagerIntegrationTests
{
    public class OnePlayersIntegrationTests : IntegrationTestBase
    {
        private readonly string email = "test1@example.com";
        private readonly string password = "password";

        private string token = "";

        [Fact]
        public async Task TeamShouldFollowRequirements()
        {
            // Arrange
            await CreateUser();
            await LoginUser();

            // Act
            var team = await GetTeam(token);

            //Assert
            Assert.True(team.Money == 5_000_000);
            Assert.Equal(3, team.Players.Where(player => player.Role == PlayerRole.Goalkeeper).Count());
            Assert.Equal(6, team.Players.Where(player => player.Role == PlayerRole.Defender).Count());
            Assert.Equal(6, team.Players.Where(player => player.Role == PlayerRole.Midfield).Count());
            Assert.Equal(5, team.Players.Where(player => player.Role == PlayerRole.Attack).Count());
            Assert.True(team.Players.All(player => player.Value == 1_000_000));
            Assert.True(team.Players.All(player => (player.Age >= 18) && (player.Age <= 40)));
        }

        private async Task CreateUser()
        {
            await RegisterUser(new RegisterRequest()
            {
                Email = email,
                Password = password
            });
        }

        private async Task LoginUser()
        {
            var response = await AuthenticateUser(new AuthRequest()
            {
                Email = email,
                Password = password
            });
            token = response.Bearer;
        }
    }
}
