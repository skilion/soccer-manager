using System.ComponentModel.DataAnnotations;

namespace SoccerManager.Models
{
    public enum PlayerRole
    {
        Goalkeeper,
        Defender,
        Midfield,
        Attack
    }

    public class Player
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public PlayerRole Role { get; set; }
        public int Value { get; set; }

        public int Age
        {
            get
            {
                var start = BirthDate;
                var end = DateTime.Today;
                return (end.Year - start.Year - 1) +
                    // considers the case when the birthday has passed
                    (
                        (
                            (end.Month > start.Month) ||
                            ((end.Month == start.Month) && (end.Day >= start.Day))
                        ) ? 1 : 0
                    );
            }    
        }
    }
}
