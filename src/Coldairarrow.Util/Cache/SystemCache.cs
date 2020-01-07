using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 系统缓存帮助类
    /// </summary>
    class SystemCache : ICache
    {
        static SystemCache()
        {
            var provider = new ServiceCollection().AddMemoryCache().BuildServiceProvider();
            _cache = provider.GetService<IMemoryCache>();
        }

        private static IMemoryCache _cache { get; }

        public bool ContainsKey(string key)
        {
            return _cache.TryGetValue(key, out object value);
        }

        public object GetCache(string key)
        {
            return _cache.Get(key);
        }

        public T GetCache<T>(string key) where T : class
        {
            return (T)GetCache(key);
        }

        public void RemoveCache(string key)
        {
            _cache.Remove(key);
        }

        public void SetCache(string key, object value)
        {
            _cache.Set(key, value);
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            _cache.Set(key, value, new DateTimeOffset(DateTime.Now.ToCstTime() + timeout));
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            if (expireType == ExpireType.Absolute)
                _cache.Set(key, value, new DateTimeOffset(DateTime.Now.ToCstTime() + timeout));
            else
                _cache.Set(key, value, timeout);
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            var value = GetCache(key);
            SetCache(key, value, expire);
        }
    }
}