using Microsoft.EntityFrameworkCore;

namespace SoccerManager.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public int TeamId { get; set; }

        public Team Team { get; set; } = null!;
    }
}
