using Microsoft.AspNetCore.Mvc;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/{id}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PlayerController : ControllerBase
    {
        private readonly SoccerManagerDbContext context;

        public PlayerController(SoccerManagerDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the details of a player.
        /// </summary>
        /// <returns>The team of the authenticated user</returns>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(typeof(Player), 200)]
        public IActionResult Get([FromQuery] int id)
        {
            var player = LoadPlayer(id);
            if (player is null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        private Player? LoadPlayer(int id)
        {
            var player = context.Players.Where(player => player.PlayerId == id);
            if (!player.Any())
            {
                return null;
            }
            return player.Single();
        }
    }
}
