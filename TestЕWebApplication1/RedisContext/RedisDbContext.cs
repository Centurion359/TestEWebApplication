using StackExchange.Redis;

namespace TestЕWebApplication1.RedisContext;

public class RedisDbContext
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionMultiplexer _redis;

    public RedisDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis"));
    }

    public IDatabase GetDatabase()
    {
        return _redis.GetDatabase();
    }
}