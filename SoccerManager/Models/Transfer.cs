using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SoccerManager.Models
{
    [Index(nameof(PlayerId), IsUnique = true)]
    public class Transfer
    {
        [ConcurrencyCheck]
        public int TransferId { get; set; }
        public int AskPrice { get; set; }
        public int PlayerId { get; set; }

        public Player Player { get; set; } = null!;
    }
}
