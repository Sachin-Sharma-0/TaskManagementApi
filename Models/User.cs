using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public enum Role { Admin, User }
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; // In production, hash passwords

        public Role Role { get; set; }
    }
}
