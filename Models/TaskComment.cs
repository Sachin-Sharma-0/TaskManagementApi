using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class TaskComment
    {
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public int TaskId { get; set; }

        public Task Task { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
