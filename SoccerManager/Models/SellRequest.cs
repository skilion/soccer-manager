using System.ComponentModel.DataAnnotations;

namespace SoccerManager.Models
{
    public class SellRequest
    {
        [Range(1, int.MaxValue)]
        public int AskPrice { get; set; }
    }
}
