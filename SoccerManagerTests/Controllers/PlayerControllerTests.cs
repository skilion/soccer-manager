using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerManager.Controllers;
using SoccerManager.Models;
using SoccerManagerTests.Stubs;
using System;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class PlayerControllerTests
    {
        private readonly SoccerManagerDbContextStub context = new();
        private readonly PlayerController controller;

        public PlayerControllerTests()
        {
            controller = new PlayerController(context);
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

        [Fact]
        public void ShouldEditPlayer()
        {
            // Arrange
            string email = "test@example.com";
            int playerId = 1;
            Player player = new()
            {
                PlayerId = playerId,
                FirstName = "test",
                LastName = "test",
                Country = "test"
            };
            User user = new()
            {
                Email = email,
                PasswordHash = "test",
                Team = new Team()
                {
                    Name = "Name",
                    Country = "Country",
                    Players = new Player[] { player }
                }
            };
            EditPlayerRequest request = new()
            {
                FirstName = "new",
                LastName = "new",
                Country = "new"
            };

            context.Users.Add(user);
            context.SaveChanges();
            controller.SetUserEmail(email);

            // Act
            var response = controller.Post(playerId, request);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var playerResult = Assert.IsAssignableFrom<Player>(result.Value);
            Assert.Equal(request.FirstName, playerResult.FirstName);
            Assert.Equal(request.LastName, playerResult.LastName);
            Assert.Equal(request.Country, playerResult.Country);
            Assert.Equal(request.FirstName, player.FirstName);
            Assert.Equal(request.LastName, player.LastName);
            Assert.Equal(request.Country, player.Country);
        }

        [Fact]
        public void ShouldRejectEditPlayerOfOtherUser()
        {
            // Arrange
            string email1 = "test1@example.com";
            string email2 = "test2@example.com";
            int playerId1 = 1;
            int playerId2 = 2;
            Player player1 = new()
            {
                PlayerId = playerId1,
                FirstName = "test",
                LastName = "test",
                Country = "test"
            };
            Player player2 = new()
            {
                PlayerId = playerId2,
                FirstName = "test",
                LastName = "test",
                Country = "test"
            };
            User user1 = new()
            {
                Email = email1,
                PasswordHash = "test",
                Team = new Team()
                {
                    Name = "Name",
                    Country = "Country",
                    Players = new Player[] { player1 }
                }
            };
            User user2 = new()
            {
                Email = email2,
                PasswordHash = "test",
                Team = new Team()
                {
                    Name = "Name",
                    Country = "Country",
                    Players = new Player[] { player2 }
                }
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();
            controller.SetUserEmail(email1);

            // Act
            var response = controller.Post(playerId2, new EditPlayerRequest());

            // Assert
            Assert.IsType<UnauthorizedResult>(response);
        }

    }
}
