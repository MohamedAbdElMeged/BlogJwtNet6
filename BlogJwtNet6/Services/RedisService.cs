using ServiceStack.Redis;
using System.Diagnostics.Metrics;

namespace BlogJwtNet6.Services
{
    public class RedisService : IRedisService
    {
        public static RedisClient redisClient { get; set; }
        public static int counter = 0;
        public RedisService() {
            if (counter < 1)
            {
                RedisEndpoint conf = new RedisEndpoint { Host = "localhost", Port = 5002 };
                redisClient = new RedisClient(conf);
                counter++;
            }

        }
        


        public RedisClient GetRedisClient()
        {
            return redisClient;
        }

    }
}