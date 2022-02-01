using Microsoft.AspNetCore.Mvc;
using SoccerManager.Controllers;
using SoccerManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class TeamControllerTests
    {
        private readonly SoccerManagerDbContextStub context = new();

        private Player testPlayer;
        private Team testTeam;
        private User testUser;

        public TeamControllerTests()
        {
            testPlayer = new Player
            {
                FirstName = "test",
                LastName = "test",
                Country = "test",
                BirthDate = new DateOnly(2000, 1, 1)
            };
            testTeam = new Team
            {
                Name = "test",
                Country = "test",
                Players = new List<Player> { testPlayer }
            };
            testUser = new User
            {
                Email = "test@example.com",
                PasswordHash = "test",
                Team = testTeam
            };
            context.Players.Add(testPlayer);
            context.Teams.Add(testTeam);
            context.Users.Add(testUser);
            context.SaveChanges();
        }

        [Fact]
        public void ShouldReturnTheUserTeam()
        {
            // Arrange
            TeamController controller = new(context);
            controller.SetUserEmail(testUser.Email);

            // Act
            var response = controller.Get();

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var team = Assert.IsAssignableFrom<Team>(result.Value);
            Assert.Equal(testTeam.Name, team.Name);
            Assert.Single(team.Players);
            Assert.Equal(testPlayer.FirstName, team.Players.Single().FirstName);
        }

        [Fact]
        public void ShouldEditTheUserTeam()
        {
            // Arrange
            TeamController controller = new(context);
            controller.SetUserEmail(testUser.Email);
            EditTeamRequest request = new()
            {
                Name = "new",
                Country = "new"
            };

            // Act
            var response = controller.Post(request);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var team = Assert.IsAssignableFrom<Team>(result.Value);
            Assert.Equal(team.Name, request.Name);
            Assert.Equal(team.Country, request.Country);
        }
    }
}
