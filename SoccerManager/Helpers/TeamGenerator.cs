using SoccerManager.Models;

namespace SoccerManager.Helpers
{
    public class TeamGenerator : ITeamGenerator
    {
        private readonly string countriesFile = @"Data\countries.txt";
        private readonly string firstnamesFile = @"Data\firstnames.txt";
        private readonly string surnamesFile = @"Data\surnames.txt";
        private readonly string teamsFile = @"Data\teams.txt";

        private readonly int defaultTeamMoney = 5_000_000;
        private readonly int defaultPlayerValue = 1_000_000;
        private readonly int minPlayerAge = 18;
        private readonly int maxPlayerAge = 40;
        private readonly int yearsToHours = 365 * 24;
        private readonly (PlayerRole, int)[] teamStructure = new (PlayerRole, int)[]
        {
            (PlayerRole.Goalkeeper, 3),
            (PlayerRole.Defender, 6),
            (PlayerRole.Midfield, 6),
            (PlayerRole.Attack, 5)
        };

        private readonly Random random = new();

        private string[] countries = null!;
        private string[] firstnames = null!;
        private string[] surnames = null!;
        private string[] teams = null!;

        public TeamGenerator()
        {
            LoadNames();
        }

        public Team Generate()
        {
            Team team = new()
            {
                Name = GetRandomString(teams),
                Country = GetRandomString(countries),
                Money = defaultTeamMoney,
                Players = GeneratePlayers(),
            };
            return team;
        }

        private List<Player> GeneratePlayers()
        {
            List<Player> players = new();
            foreach (var (role, count) in teamStructure)
            {
                for (int i = 0; i < count; i++)
                {
                    var randomHours = random.Next(minPlayerAge * yearsToHours, maxPlayerAge * yearsToHours);
                    var birthDate = DateTime.Today.AddHours(-randomHours);
                    Player player = new()
                    {
                        FirstName = GetRandomString(firstnames),
                        LastName = GetRandomString(surnames),
                        Country = GetRandomString(countries),
                        BirthDate = birthDate,
                        Role = role,
                        Value = defaultPlayerValue
                    };
                    players.Add(player);
                }
            }
            return players;
        }

        private void LoadNames()
        {
            countries = File.ReadAllLines(countriesFile);
            firstnames = File.ReadAllLines(firstnamesFile);
            surnames = File.ReadAllLines(surnamesFile);
            teams = File.ReadAllLines(teamsFile);
        }

        private string GetRandomString(string[] strings)
        {
            int i = random.Next(0, strings.Length);
            return strings[i];
        }
    }
}
