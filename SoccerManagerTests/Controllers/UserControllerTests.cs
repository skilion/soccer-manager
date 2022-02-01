using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoccerManager.Controllers;
using SoccerManager.Models;
using Xunit;

namespace SoccerManagerTests.Controllers
{
    public class UsersControllerTest
    {
        private readonly SoccerManagerDbContextStub context = new();

        [Fact]
        public void ShouldCreateNewUserAndAuthenticate()
        {
            // Arrange
            string email = "test@example.com";
            string pass = "test";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            UsersController controller = new(context, configuration);

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
    }
}
