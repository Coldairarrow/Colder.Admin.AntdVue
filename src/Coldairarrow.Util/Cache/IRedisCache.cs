using CSRedis;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public interface IRedisCache : ICache
    {
        CSRedisClient Db { get; }
    }
}
