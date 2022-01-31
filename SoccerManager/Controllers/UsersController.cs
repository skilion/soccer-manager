using Microsoft.AspNetCore.Mvc;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Authenticates an user
        /// </summary>
        /// <returns>A JWT bearer token</returns>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Wrong email or password</response>
        [HttpPost]
        [Route("Authenticate")]
        public AuthResponse Authenticate([FromBody] AuthRequest authRequest)
        {
            return new AuthResponse
            {
                Bearer = "token"
            };
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns>None</returns>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [Route("Register")]
        public void Register([FromBody] RegisterRequest registerRequest)
        {
            return;
        }
    }
}
