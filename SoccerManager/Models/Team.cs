namespace SoccerManager.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Money { get; set; }

        public ICollection<Player> Players { get; set; }
        public int Value { get => Players.Sum(p => p.Value); }
    }
}
