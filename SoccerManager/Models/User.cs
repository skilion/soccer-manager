using Microsoft.EntityFrameworkCore;

namespace SoccerManager.Models
{
    [Index(nameof(Email))]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
