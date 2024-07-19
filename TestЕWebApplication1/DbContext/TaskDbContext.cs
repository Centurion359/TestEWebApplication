using Microsoft.EntityFrameworkCore;
using Task = TestЕWebApplication1.Models.Task;

namespace TestЕWebApplication1.DbContext;

public class TaskDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IConfiguration _configuration;

    public TaskDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Task> Tasks => Set<Task>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ConDatabase"));
    }
}