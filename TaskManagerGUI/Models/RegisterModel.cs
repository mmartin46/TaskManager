using System.ComponentModel.DataAnnotations;

namespace TaskManagerGUI.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [EmailAddress]
        [Required]
        public string ConfirmEmail { get; set; }
    }
}
