namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 分片规则接口
    /// </summary>
    public interface IShardingRule
    {
        /// <summary>
        /// 找表方法
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        string FindTable(object obj);
    }
}
