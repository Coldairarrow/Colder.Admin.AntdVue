namespace Coldairarrow.Util
{
    /// <summary>
    /// 删除模式
    /// </summary>
    public enum DeleteMode
    {
        /// <summary>
        /// 物理删除,即直接从数据库删除
        /// </summary>
        Physic,

        /// <summary>
        /// 逻辑删除,即仅将Deleted字段置为true
        /// </summary>
        Logic
    }
}
