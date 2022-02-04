using Microsoft.AspNetCore.Mvc;
using SoccerManager.Controllers;
using SoccerManager.Models;
using SoccerManagerTests.Stubs;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class PlayerControllerTests
    {
        private readonly SoccerManagerDbContextStub context = new();
        private readonly PlayerController controller;

        private readonly string email1 = "test1@example.com";
        private readonly string email2 = "test2@example.com";

        private Player player1 = null!;
        private Player player2 = null!;
        private Team team1 = null!;
        private Team team2 = null!;

        public PlayerControllerTests()
        {
            controller = new PlayerController(context);
            AddTestUsers();
        }

        [Fact]
        public void GetShouldReturnPlayer()
        {
            // Arrange
            int playerId = player1.PlayerId;

            // Act
            var response = controller.Get(playerId);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var resultPlayer = Assert.IsAssignableFrom<Player>(result.Value);
            Assert.Equal(player1.FirstName, resultPlayer.FirstName);
        }

        [Fact]
        public void GetShouldReturnNotFoundForInvalidPlayer()
        {
            // Arrange
            int playerId = 100;

            // Act
            var response = controller.Get(playerId);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void PutShouldUpdatePlayer()
        {
            // Arrange
            UpdatePlayerRequest request = new()
            {
                FirstName = "new",
                LastName = "new",
                Country = "new"
            };
            controller.SetUserEmail(email1);

            // Act
            var response = controller.Put(player1.PlayerId, request);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var playerResult = Assert.IsAssignableFrom<Player>(result.Value);
            Assert.Equal(request.FirstName, playerResult.FirstName);
            Assert.Equal(request.LastName, playerResult.LastName);
            Assert.Equal(request.Country, playerResult.Country);
            Assert.Equal(request.FirstName, player1.FirstName);
            Assert.Equal(request.LastName, player1.LastName);
            Assert.Equal(request.Country, player1.Country);
        }

        [Fact]
        public void PutShouldRejectForOtherUserPlayer()
        {
            // Arrange
            controller.SetUserEmail(email1);

            // Act
            var response = controller.Put(player2.PlayerId, new UpdatePlayerRequest());

            // Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void BuyShouldIncreasePlayerValue()
        {
            // Arrange
            controller.SetUserEmail(email1);
            int initialPlayerValue = player2.Value;
            context.Transfers.Add(new Transfer()
            {
                PlayerId = player2.PlayerId,
                AskPrice = 10
            });
            context.SaveChanges();

            // Act
            var response = controller.Buy(player2.PlayerId);

            // Assert
            Assert.IsType<OkResult>(response);
            Assert.True(player2.Value > initialPlayerValue);
        }

        [Fact]
        public void BuyShouldUpdateTeamMoney()
        {
            // Arrange
            controller.SetUserEmail(email1);
            int teamMoney1 = team1.Money;
            int teamMoney2 = team2.Money;
            int askPrice = 10;
            context.Transfers.Add(new Transfer()
            {
                PlayerId = player2.PlayerId,
                AskPrice = askPrice
            });
            context.SaveChanges();

            // Act
            var response = controller.Buy(player2.PlayerId);

            // Assert
            Assert.Equal(teamMoney1 - askPrice, team1.Money);
            Assert.Equal(teamMoney2 + askPrice, team2.Money);
        }

        [Fact]
        public void BuyShouldUpdateOwnership()
        {
            // Arrange
            controller.SetUserEmail(email1);
            context.Transfers.Add(new Transfer()
            {
                PlayerId = player2.PlayerId,
                AskPrice = 10
            });
            context.SaveChanges();

            // Act
            var response = controller.Buy(player2.PlayerId);

            // Assert
            Assert.Equal(team1.TeamId, player2.TeamId);
        }

        [Fact]
        public void BuyShouldForbidIfNotEnoughMoney()
        {
            // Arrange
            controller.SetUserEmail(email1);
            context.Transfers.Add(new Transfer()
            {
                PlayerId = player2.PlayerId,
                AskPrice = 1000
            });

            // Act
            var response = controller.Buy(player2.PlayerId);

            // Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void BuyShouldForbidForPlayerNotOnSale()
        {
            // Arrange
            controller.SetUserEmail(email1);

            // Act
            var response = controller.Buy(player2.PlayerId);

            // Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void SellShouldCreateTransferRecord()
        {
            // Arrange
            controller.SetUserEmail(email1);
            SellRequest request = new()
            {
                AskPrice = 10
            };

            // Act
            var response = controller.Sell(player1.PlayerId, request);

            // Assert
            Assert.IsType<OkResult>(response);
            var transfer = context.Transfers.Where(transfer => transfer.PlayerId == player1.PlayerId);
            Assert.Equal(1, transfer.Count());
        }

        [Fact]
        public void SellShouldFrobidForOtherUserPlayer()
        {
            // Arrange
            controller.SetUserEmail(email1);
            SellRequest request = new()
            {
                AskPrice = 10
            };

            // Act
            var response = controller.Sell(player2.PlayerId, request);

            // Assert
            Assert.IsType<ForbidResult>(response);
        }

        private void AddTestUsers()
        {
            player1 = new Player()
            {
                FirstName = "test",
                LastName = "test",
                Country = "test",
                Value = 10
            };
            team1 = new Team()
            {
                Name = "Name",
                Country = "Country",
                Money = 100,
                Players = new List<Player> { player1 }
            };
            player2 = new Player()
            {
                FirstName = "test",
                LastName = "test",
                Country = "test",
                Value = 10
            };
            team2 = new Team()
            {
                Name = "Name",
                Country = "Country",
                Money = 100,
                Players = new List<Player> { player2 }
            };
            context.Users.Add(new User()
            {
                Email = email1,
                PasswordHash = "test",
                Team = team1
            });
            context.Users.Add(new User()
            {
                Email = email2,
                PasswordHash = "test",
                Team = team2
            });
            context.SaveChanges();
        }
    }
}
