namespace Coldairarrow.Util
{
    /// <summary>
    /// 更新模式
    /// 注:[Field]=[Field] {UpdateType} value
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// 等,即赋值,[Field]= value
        /// </summary>
        Equal,
        /// <summary>
        /// 自增,[Field]=[Field] + value
        /// </summary>
        Add,
        /// <summary>
        /// 自减,[Field]=[Field] - value
        /// </summary>
        Minus,
        /// <summary>
        /// 自乘,[Field]=[Field] * value
        /// </summary>
        Multiply,
        /// <summary>
        /// 自除,[Field]=[Field] / value
        /// </summary>
        Divide
    }
}
