using Microsoft.AspNetCore.Mvc;
using SoccerManager.Controllers;
using SoccerManager.Models;
using SoccerManagerTests.Stubs;
using System.Collections.Generic;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class MarketControllerTests
    {
        private readonly SoccerManagerDbContextStub context = new();
        private readonly MarketController controller;

        public MarketControllerTests()
        {
            controller = new MarketController(context);
        }

        [Fact]
        public void GetShouldReturnPlayersOnTheMarket()
        {
            // Arrange
            Player player = new()
            {
                FirstName = "test",
                LastName = "test",
                Country = "test",
                Value = 10
            };
            Team team = new()
            {
                Name = "Name",
                Country = "Country",
                Money = 100,
                Players = new List<Player> { player }
            };
            Transfer transfer = new()
            {
                AskPrice = 10,
                Player = player
            };
            context.Add(transfer);
            context.Add(team);
            context.SaveChanges();

            // Act
            var response = controller.Get();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var resultTransfers = Assert.IsAssignableFrom<List<Transfer>>(result.Value);
            var resultTransfer = Assert.Single(resultTransfers);
            Assert.Equal(transfer.TransferId, resultTransfer.TransferId);
            Assert.Equal(transfer.AskPrice, resultTransfer.AskPrice);
        }
    }
}
