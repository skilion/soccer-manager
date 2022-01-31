using System.ComponentModel.DataAnnotations;

namespace SoccerManager.Models
{
    public class AuthResponse
    {
        [Required]
        public string Bearer { get; set;}
    }
}
