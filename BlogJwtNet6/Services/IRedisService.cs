using ServiceStack.Redis;

namespace BlogJwtNet6.Services
{
    public interface IRedisService
    {

        public RedisClient GetRedisClient();
    }
}
