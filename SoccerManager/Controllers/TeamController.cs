using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TeamController : ControllerBase
    {
        private readonly SoccerManagerDbContext context;

        public TeamController(SoccerManagerDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the user's team
        /// </summary>
        /// <returns>The team of the authenticated user</returns>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(typeof(Team), 200)]
        public IActionResult Get()
        {
            var team = LoadUserTeam();
            return Ok(team);
        }

        /// <summary>
        /// Edit the details of the user's team
        /// </summary>
        /// <returns>The team of the authenticated user</returns>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(typeof(Team), 200)]
        public IActionResult Post([FromBody] EditTeamRequest request)
        {
            var team = LoadUserTeam();
            if (request.Name is not null)
            {
                team.Name = request.Name;
            }
            if (request.Country is not null)
            {
                team.Country = request.Country;
            }
            context.SaveChanges();
            return Ok(team);
        }

        private Team LoadUserTeam()
        {
            var email = this.GetUserEmail();
            var user = context.Users
                .Include(user => user.Team)
                .ThenInclude(team => team.Players)
                .Single(user => user.Email == email);
            return user.Team;
        }
    }
}
