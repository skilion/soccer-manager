using System.ComponentModel.DataAnnotations;

namespace SoccerManager.Models
{
    public class AuthRequest
    {
        [Required, RegularExpression(@".+@.+\..+", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
