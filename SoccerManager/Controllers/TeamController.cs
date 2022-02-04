using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesErrorResponseType(typeof(void))]
    public class TeamController : ControllerBase
    {
        private readonly SoccerManagerDbContext context;

        public TeamController(SoccerManagerDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the team of the team of the current user
        /// </summary>
        [HttpGet(Name = "GetTeam")]
        [ProducesResponseType(typeof(Team), 200)]
        public IActionResult Get()
        {
            var team = LoadUserTeam();
            return Ok(team);
        }

        /// <summary>
        /// Updates the details of the team of the current user
        /// </summary>
        /// <returns>The team of the authenticated user</returns>
        [HttpPut(Name = "UpdateTeam")]
        [ProducesResponseType(typeof(Team), 200)]
        public IActionResult Put([FromBody] UpdateTeamRequest request)
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
