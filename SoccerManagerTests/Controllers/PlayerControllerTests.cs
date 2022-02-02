using Microsoft.AspNetCore.Mvc;
using SoccerManager.Controllers;
using SoccerManager.Models;
using SoccerManagerTests.Stubs;
using System;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class PlayerControllerTests : IDisposable
    {
        private readonly SoccerManagerDbContextStub context = new();
        private readonly PlayerController controller;

        public PlayerControllerTests()
        {
            controller = new PlayerController(context);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        [Fact]
        public void ShouldReturnPlayer()
        {
            // Arrange
            int playerId = 1;
            Player player = new()
            {
                PlayerId = playerId,
                FirstName = "test",
                LastName = "test",
                Country = "test"
            };
            context.Players.Add(player);
            context.SaveChanges();

            // Act
            var response = controller.Get(playerId);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var resultPlayer = Assert.IsAssignableFrom<Player>(result.Value);
            Assert.Equal(player.PlayerId, resultPlayer.PlayerId);
            Assert.Equal(player.FirstName, resultPlayer.FirstName);
        }

        [Fact]
        public void ShouldReturnNotFound()
        {
            // Arrange
            int playerId = 1;

            // Act
            var response = controller.Get(playerId);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

    }
}
