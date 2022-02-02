using SoccerManager.Helpers;
using SoccerManager.Models;
using System.Collections.Generic;

namespace SoccerManagerTests.Stubs
{
    internal class TeamGeneratorStub : ITeamGenerator
    {
        public Team Generate()
        {
            Player player = new()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Country = "Country"
            };
            Team team = new()
            {
                Name = "Name",
                Country = "Country",
                Players = new List<Player>() { player }
            };
            return team;
        }
    }
}
