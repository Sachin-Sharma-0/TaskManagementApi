using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTask([FromBody] Models.Task task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .ToListAsync();

            // If user is not Admin, ensure they can only see their own tasks
            var currentUser = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUser);
            if (user?.Role != Role.Admin && user?.Id != userId)
            {
                return Forbid();
            }

            return Ok(tasks);
        }
    }
}
