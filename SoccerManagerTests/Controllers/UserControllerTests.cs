using Microsoft.AspNetCore.Mvc;
using SoccerManager.Controllers;
using SoccerManager.Models;
using SoccerManagerTests.Stubs;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class UsersControllerTest
    {
        private readonly SoccerManagerDbContextStub context = new();
        private readonly TeamGeneratorStub teamGenerator = new();
        private readonly JwtGeneratorStub jwtGenerator = new();
        private readonly UserController controller;

        public UsersControllerTest()
        {
            controller = new(context, teamGenerator, jwtGenerator);
        }
        
        [Fact]
        public void ShouldCreateNewUserAndAuthenticate()
        {
            // Arrange
            string email = "test@example.com";
            string pass = "test";

            // Act
            controller.Register(new RegisterRequest()
            {
                Email = email,
                Password = pass
            });
            var response = controller.Authenticate(new AuthRequest()
            {
                Email = email,
                Password = pass
            });

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var authResponse = Assert.IsAssignableFrom<AuthResponse>(result.Value);
            Assert.NotEmpty(authResponse.Bearer);
        }

        [Fact]
        public void ShouldCreateUserWithTeam()
        {
            // Arrange
            string email = "test@example.com";
            string pass = "test";

            // Act
            var response = controller.Register(new RegisterRequest()
            {
                Email = email,
                Password = pass
            });

            // Assert
            Assert.IsType<OkResult>(response);
            Assert.NotEmpty(context.Users);
            Assert.NotEmpty(context.Teams);
            Assert.NotEmpty(context.Players);
        }
    }
}
