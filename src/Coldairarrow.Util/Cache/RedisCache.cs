using CSRedis;
using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : IRedisCache
    {
        /// <summary>
        /// 构造函数
        /// 注意：请以单例使用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public RedisCache(string config)
        {
            _redisCLient = new CSRedisClient(config);
        }
        private CSRedisClient _redisCLient { get; }

        public CSRedisClient Db => _redisCLient;

        public bool ContainsKey(string key)
        {
            return _redisCLient.Exists(key);
        }

        public object GetCache(string key)
        {
            object value = null;
            var redisValue = _redisCLient.Get(key);
            if (redisValue.IsNullOrEmpty())
                return null;
            ValueInfoEntry valueEntry = redisValue.ToString().ToObject<ValueInfoEntry>();
            if (valueEntry.TypeName == typeof(string).AssemblyQualifiedName)
                value = valueEntry.Value;
            else
                value = valueEntry.Value.ToObject(Type.GetType(valueEntry.TypeName));

            if (valueEntry.ExpireTime != null && valueEntry.ExpireType == ExpireType.Relative)
                SetKeyExpire(key, valueEntry.ExpireTime.Value);

            return value;
        }

        public T GetCache<T>(string key) where T : class
        {
            return (T)GetCache(key);
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            _redisCLient.Expire(key, expire);
        }

        public void RemoveCache(string key)
        {
            _redisCLient.Del(key);
        }

        public void SetCache(string key, object value)
        {
            _SetCache(key, value, null, null);
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            _SetCache(key, value, timeout, ExpireType.Absolute);
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            _SetCache(key, value, timeout, expireType);
        }

        private void _SetCache(string key, object value, TimeSpan? timeout, ExpireType? expireType)
        {
            string jsonStr = string.Empty;
            if (value is string)
                jsonStr = value as string;
            else
                jsonStr = value.ToJson();

            ValueInfoEntry entry = new ValueInfoEntry
            {
                Value = jsonStr,
                TypeName = value.GetType().AssemblyQualifiedName,
                ExpireTime = timeout,
                ExpireType = expireType
            };

            string theValue = entry.ToJson();
            if (timeout == null)
                _redisCLient.Set(key, theValue);
            else
                _redisCLient.Set(key, theValue, (int)timeout.Value.TotalSeconds);
        }
    }
}
