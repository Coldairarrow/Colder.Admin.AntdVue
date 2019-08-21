namespace Coldairarrow.Util
{
    /// <summary>
    /// GUID帮助类
    /// </summary>
    public static class IdHelper
    {
        /// <summary>
        /// 生成主键,统一使用雪花Id
        /// </summary>
        /// <returns></returns>
        public static string GetId()
        {
            return SnowflakeId.NewSnowflakeId().ToString();
        }
    }
}