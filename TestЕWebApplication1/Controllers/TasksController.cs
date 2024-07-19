using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestЕWebApplication1.Contracts;
using TestЕWebApplication1.DbContext;
using TestЕWebApplication1.RedisContext;
using Task = TestЕWebApplication1.Models.Task;

namespace TestЕWebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly RedisDbContext _redisDbContext;
    private readonly TaskDbContext _taskDbContext;

    public TasksController(TaskDbContext taskDbContext, RedisDbContext redisDbContext)
    {
        _taskDbContext = taskDbContext;
        _redisDbContext = redisDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskRequest createTaskRequest)
    {
        var task = new Task(createTaskRequest.Title, createTaskRequest.Summary, createTaskRequest.Image);

        await _taskDbContext.AddAsync(task);

        await _taskDbContext.SaveChangesAsync();

        var hashKey = "task";
        await _redisDbContext.GetDatabase().KeyPersistAsync(hashKey).ConfigureAwait(false);

        await _redisDbContext.GetDatabase().HashSetAsync(hashKey, task?.Id.ToString(), JsonSerializer.Serialize(task))
            .ConfigureAwait(false);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tasks = await _taskDbContext.Tasks.ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        var task = await _taskDbContext.Tasks
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        if (task != null) return Ok(task);

        return NotFound();
    }
}