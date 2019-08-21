using System.Collections.Generic;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 动态数据模型类
    /// </summary>
    public class DynamicModel : Dictionary<string, object>
    {
        /// <summary>
        /// 索引器
        /// </summary>
        public new object this[string key]
        {
            get
            {
                return GetProperty(key);
            }
            set
            {
                AddProperty(key, value);
            }
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="obj">属性值</param>
        public void AddProperty(string key, object obj)
        {
            if (ContainsKey(key))
                base[key] = obj;
            else
                Add(key, obj);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="obj">属性值</param>
        public void SetProperty(string key, object obj)
        {
            if (ContainsKey(key))
                base[key] = obj;
            else
                Add(key, obj);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="key">属性名</param>
        /// <returns></returns>
        public object GetProperty(string key)
        {
            if (ContainsKey(key))
                return base[key];
            else
                return null;
        }
    }
}
