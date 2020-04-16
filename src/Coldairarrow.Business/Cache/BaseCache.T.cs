using Coldairarrow.Util;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Cache
{
    public abstract class BaseCache<T> : IBaseCache<T> where T : class
    {
        readonly IDistributedCache _cache;
        public BaseCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        #region 私有成员

        protected abstract Task<T> GetDbDataAsync(string key);
        protected string BuildKey(string idKey)
        {
            return $"Cache_{GetType().FullName}_{idKey}";
        }

        #endregion

        #region 外部接口

        public async Task<T> GetCacheAsync(string idKey)
        {
            if (idKey.IsNullOrEmpty())
                return null;

            string cacheKey = BuildKey(idKey);
            var cache = (await _cache.GetStringAsync(cacheKey))?.ToObject<T>();
            if (cache == null)
            {
                cache = await GetDbDataAsync(idKey);
                if (cache != null)
                    await _cache.SetStringAsync(cacheKey, cache.ToJson());
            }

            return cache;
        }

        public async Task UpdateCacheAsync(string idKey)
        {
            await _cache.RemoveAsync(BuildKey(idKey));
        }

        public async Task UpdateCacheAsync(List<string> idKeys)
        {
            foreach (var aKey in idKeys)
            {
                await UpdateCacheAsync(aKey);
            }
        }

        #endregion
    }
}
