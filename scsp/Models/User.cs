using System.ComponentModel.DataAnnotations;

namespace scsp.Models
{
    public class User
    {
        [Key] [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public int? PhotoId { get; set; }
    }
}