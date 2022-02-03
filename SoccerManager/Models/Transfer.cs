using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoccerManager.Models
{
    [Index(nameof(PlayerId), IsUnique = true)]
    public class Transfer
    {
        [JsonIgnore]
        [ConcurrencyCheck]
        public int TransferId { get; set; }
        public int AskPrice { get; set; }
        [JsonIgnore]
        public int PlayerId { get; set; }

        public Player Player { get; set; } = null!;
    }
}
