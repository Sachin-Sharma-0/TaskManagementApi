using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;

namespace TaskManagementApi.Services
{
    public class TaskService
    {
        private readonly TaskDbContext _dbContext;

        public TaskService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Task?> GetTask(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Task ID must be greater than zero.", nameof(id));
            }

            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Models.Task>> GetAllTasks()
        {
            return await _dbContext.Tasks.ToListAsync();
        }
    }
}
