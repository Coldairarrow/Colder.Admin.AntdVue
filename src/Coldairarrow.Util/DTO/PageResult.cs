using System.Collections.Generic;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 分页返回结果
    /// </summary>
    /// <typeparam name="U"></typeparam>
    public class PageResult<U> : AjaxResult<List<U>>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }
    }
}
