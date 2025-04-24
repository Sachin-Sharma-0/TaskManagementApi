using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using TaskManagementApi.Services;
using Xunit;

// Unit tests for TaskService
// To run tests:
// 1. Navigate to the test project: cd TaskManagementApi.Tests
// 2. Run tests: dotnet test
// Tests cover:
// - TaskService.GetTask: Valid ID, invalid ID, non-existent ID
// - TaskService.GetAllTasks: Returns all tasks
namespace TaskManagementApi.Tests
{
    public class TaskServiceTests
    {
        private readonly TaskService _taskService;
        private readonly Mock<TaskDbContext> _mockContext;
        private readonly Mock<DbSet<Models.Task>> _mockTasks;

        public TaskServiceTests()
        {
            // Mock DbSet<Task>
            var tasks = new List<Models.Task>
            {
                new Models.Task { Id = 1, Title = "Task 1", Description = "First task", UserId = 2 },
                new Models.Task { Id = 2, Title = "Task 2", Description = "Second task", UserId = 2 }
            }.AsQueryable();

            _mockTasks = new Mock<DbSet<Models.Task>>();
            _mockTasks.As<IQueryable<Models.Task>>().Setup(m => m.Provider).Returns(tasks.Provider);
            _mockTasks.As<IQueryable<Models.Task>>().Setup(m => m.Expression).Returns(tasks.Expression);
            _mockTasks.As<IQueryable<Models.Task>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
            _mockTasks.As<IQueryable<Models.Task>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

            // Mock TaskDbContext
            _mockContext = new Mock<TaskDbContext>(new DbContextOptions<TaskDbContext>());
            _mockContext.Setup(c => c.Tasks).Returns(_mockTasks.Object);

            _taskService = new TaskService(_mockContext.Object);
        }

        [Fact]
        public async Models.Task GetTask_ValidId_ReturnsTask()
        {
            // Act
            var task = await _taskService.GetTask(1);

            // Assert
            Assert.NotNull(task);
            Assert.Equal(1, task.Id);
            Assert.Equal("Task 1", task.Title);
        }

        [Fact]
        public async Models.Task GetTask_InvalidId_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.GetTask(0));
        }

        [Fact]
        public async Models.Task GetTask_NonExistentId_ReturnsNull()
        {
            // Act
            var task = await _taskService.GetTask(999);

            // Assert
            Assert.Null(task);
        }

        [Fact]
        public async Models.Task GetAllTasks_ReturnsAllTasks()
        {
            // Act
            var tasks = await _taskService.GetAllTasks();

            // Assert
            Assert.Equal(2, tasks.Count);
            Assert.Contains(tasks, t => t.Id == 1);
            Assert.Contains(tasks, t => t.Id == 2);
        }
    }
}