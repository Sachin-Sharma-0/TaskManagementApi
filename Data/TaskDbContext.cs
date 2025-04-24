using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            // Ensure database is created and seeded
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin123", Role = Role.Admin },
                new User { Id = 2, Username = "user", Password = "user123", Role = Role.User }
            );

            modelBuilder.Entity<Models.Task>().HasData(
                new Models.Task { Id = 1, Title = "Task 1", Description = "First task", UserId = 2 },
                new Models.Task { Id = 2, Title = "Task 2", Description = "Second task", UserId = 2 },
                new Models.Task { Id = 3, Title = "Admin Task", Description = "Admin task", UserId = 1 }
            );

            modelBuilder.Entity<TaskComment>().HasData(
                new TaskComment { Id = 1, Comment = "Looks good", TaskId = 1, UserId = 1 }
            );

            Console.WriteLine("Seed data applied.");
        }
    }
}
