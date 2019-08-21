using Coldairarrow.Util;
using System;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 基于雪花Id的mod分片,具体的规则请参考此本写法
    /// 优点:数据扩容无需数据迁移,以时间轴进行扩容
    /// 缺点:可能会存在数据热点问题
    /// 建议:推荐使用此分片规则,易于使用
    /// </summary>
    /// <seealso cref="Coldairarrow.DataRepository.IShardingRule" />
    public class SnowflakeModShardingRule : IShardingRule
    {
        public virtual string FindTable(object obj)
        {
            //主键Id必须为SnowflakeId
            SnowflakeId snowflakeId = new SnowflakeId((long)obj.GetPropertyValue("Id"));
            //2019-5-10之前mod3
            if (snowflakeId.Time < DateTime.Parse("2019-5-10"))
                return BuildTable(snowflakeId.Id.GetHashCode() % 3);
            //2019-5-10之后mod10
            if (snowflakeId.Time >= DateTime.Parse("2019-5-10"))
                return BuildTable(snowflakeId.Id.GetHashCode() % 10);
            //以此类推balabala

            throw new NotImplementedException();

            string BuildTable(int num)
            {
                return $"Base_SysLog_{num}";
            }
        }
    }
}
