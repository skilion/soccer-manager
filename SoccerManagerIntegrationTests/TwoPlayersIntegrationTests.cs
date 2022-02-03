using SoccerManager.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerManagerIntegrationTests
{
    public class TwoPlayersIntegrationTests : IntegrationTestBase
    {
        private readonly string email1 = "test1@example.com";
        private readonly string email2 = "test2@example.com";
        private readonly string password = "password";

        private string token1 = "";
        private string token2 = "";

        // Creates two users and exchange one player from each team
        [Fact]
        public async Task ExchangePlayers()
        {
            // Arrange
            int price1 = 100;
            int price2 = 150;
            await CreateUsers();
            await LoginUsers();
            var team1 = await GetTeam(token1);
            var team2 = await GetTeam(token2);
            await SellPlayer(token1, team1.Players.First().PlayerId, price1);
            await SellPlayer(token2, team2.Players.First().PlayerId, price2);

            // Act
            await BuyPlayer(token1, team2.Players.First().PlayerId);
            await BuyPlayer(token2, team1.Players.First().PlayerId);

            //Assert
            var teamAfter1 = await GetTeam(token1);
            var teamAfter2 = await GetTeam(token2);
            Assert.Equal(team1.Money + price1 - price2, teamAfter1.Money);
            Assert.Equal(team2.Money - price1 + price2, teamAfter2.Money);
            Assert.Single(teamAfter1.Players.Where(player => player.PlayerId == team2.Players.First().PlayerId));
            Assert.Single(teamAfter2.Players.Where(player => player.PlayerId == team1.Players.First().PlayerId));
        }

        private async Task CreateUsers()
        {
            await RegisterUser(new RegisterRequest()
            {
                Email = email1,
                Password = password
            });
            await RegisterUser(new RegisterRequest()
            {
                Email = email2,
                Password = password
            });
        }

        private async Task LoginUsers()
        {
            var response = await AuthenticateUser(new AuthRequest()
            {
                Email = email1,
                Password = password
            });
            token1 = response.Bearer;
            response = await AuthenticateUser(new AuthRequest()
            {
                Email = email2,
                Password = password
            });
            token2 = response.Bearer;
        }
    }
}
