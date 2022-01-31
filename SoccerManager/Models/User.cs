namespace SoccerManager.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
