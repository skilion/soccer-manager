using ToptalProject.Models;

namespace SoccerManager.Models
{
    public class Transfer
    {
        public int AskPrice { get; set; }
        public int PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
