using System.ComponentModel.DataAnnotations;

namespace SoccerManager.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";

        [ConcurrencyCheck]
        public int Money { get; set; }

        public ICollection<Player> Players { get; set; } = null!;

        public int Value { get => Players.Sum(p => p.Value); }
    }
}
