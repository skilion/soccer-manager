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
            UsersController controller = new(context);

            // Act
            controller.Register(new RegisterRequest()
            {
                Email = email,
                Password = pass
            });

            // Assert
            var response = controller.Authenticate(new AuthRequest()
            {
                Email = email,
                Password = pass
            });
            Assert.NotEmpty(response.Bearer);
        }
    }
}
