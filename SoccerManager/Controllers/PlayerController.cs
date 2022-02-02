using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    [ApiController]
    [Route("[controller]/{id}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PlayerController : ControllerBase
    {
        private readonly SoccerManagerDbContext context;
        private readonly Random random = new();

        private readonly int minPlayerValueIncreasePercent = 10;
        private readonly int maxPlayerValueIncreasePercent = 100;

        public PlayerController(SoccerManagerDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the details of a player.
        /// </summary>
        /// <returns>The details of the player</returns>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        public IActionResult Get([FromRoute] int id)
        {
            var player = context.Players.Find(id);
            if (player is null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        /// <summary>
        /// Update the details of a player. The player must belong to the user.
        /// </summary>
        /// <returns>The details of the player</returns>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        public IActionResult Post([FromRoute] int id, [FromBody] EditPlayerRequest request)
        {
            var player = context.Players.Find(id);
            if (player is null)
            {
                return NotFound();
            }
            if (!DoesPlayerBelongToCurrentUser(player))
            {
                return Unauthorized();
            }
            if (request.FirstName is not null)
            {
                player.FirstName = request.FirstName;
            }
            if (request.LastName is not null)
            {
                player.LastName = request.LastName;
            }
            if (request.Country is not null)
            {
                player.Country = request.Country;
            }
            context.SaveChanges();
            return Ok(player);
        }

        /// <summary>
        /// Buys a player. The player must be on the market and the user's team must have enough money.
        /// If the user already owns the player, it is removed from the market.
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="409">Conflict error while buying the player. Retry.</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("Buy")]
        public IActionResult Buy([FromRoute] int id)
        {
            var player = context.Players .Find(id);
            if (player is null)
            {
                return NotFound();
            }

            if (!BuyPlayer(player))
            {
                return Forbid();
            }

            if (!TrySaveChanges())
            {
                return Conflict();
            }

            return Ok();
        }

        /// <summary>
        /// Puts a player on the market. The player must belong to the user's team.
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("Sell")]
        public IActionResult Sell([FromRoute] int id, SellRequest request)
        {
            var player = context.Players.Find(id);
            if (player is null)
            {
                return NotFound();
            }

            if (!DoesPlayerBelongToCurrentUser(player))
            {
                return Forbid();
            }

            Transfer transfer = new()
            {
                AskPrice = request.AskPrice,
                PlayerId = id,
            };
            context.Add(transfer);

            context.SaveChanges();
            return Ok();
        }

        private bool DoesPlayerBelongToCurrentUser(Player player)
        {
            return GetUserEmailOwningTeam(player.TeamId) == this.GetUserEmail();
        }

        private string GetUserEmailOwningTeam(int teamId)
        {
            var email = context.Users
                .Where(user => user.TeamId == teamId)
                .Select(user => user.Email);
            return email.Single();
        }

        private Transfer? GetPlayerTransfer(int playerId)
        {
            var transfer = context.Transfers
                .Where(transfer => transfer.PlayerId == playerId);
            if (!transfer.Any())
            {
                return null;
            }
            return transfer.Single();
        }

        private bool BuyPlayer(Player player)
        {
            var transfer = GetPlayerTransfer(player.PlayerId);
            if (transfer is null)
            {
                // Player is not on sale
                return false;
            }
            var newTeam = GetCurrentUserTeam();
            if (player.TeamId != newTeam.TeamId)
            {
                if (newTeam.Money < transfer.AskPrice)
                {
                    // New team has not enough money
                    return false;
                }
                var oldTeam = context.Teams.Find(player.TeamId)!;
                oldTeam.Money += transfer.AskPrice;
                newTeam.Money -= transfer.AskPrice;
                player.TeamId = newTeam.TeamId;
                IncreasePlayerValue(player);
            }
            context.Transfers.Remove(transfer);
            return true;
        }

        private Team GetCurrentUserTeam()
        {
            var team = context.Users
                .Include(user => user.Team)
                .Single(user => user.Email == this.GetUserEmail())
                .Team;
            return team;
        }

        private bool TrySaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        private void IncreasePlayerValue(Player player)
        {
            int incrementPercent = random.Next(minPlayerValueIncreasePercent, maxPlayerValueIncreasePercent + 1);
            player.Value += player.Value * incrementPercent / 100;
        }
    }
}
