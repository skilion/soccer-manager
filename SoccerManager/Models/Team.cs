using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoccerManager.Models
{
    public class Team
    {
        [JsonIgnore]
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        [ConcurrencyCheck]
        public int Money { get; set; }

        public ICollection<Player> Players { get; set; } = null!;

        public int Value { get => Players.Sum(p => p.Value); }
    }
}
