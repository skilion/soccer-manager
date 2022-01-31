namespace SoccerManager.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int AskPrice { get; set; }
        public int PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
