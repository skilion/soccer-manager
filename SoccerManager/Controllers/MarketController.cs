using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class MarketController : ControllerBase
    {
        private readonly SoccerManagerDbContext context;

        public MarketController(SoccerManagerDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the list of players on the market
        /// </summary>
        [HttpGet(Name = "GetMarketList")]
        [ProducesResponseType(typeof(List<Transfer>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var transfers = context.Transfers
                .Include(transfer => transfer.Player)
                .ToList();
            return Ok(transfers);
        }
    }
}
