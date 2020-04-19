namespace Coldairarrow.Util
{
    /// <summary>
    /// 分页查询基类
    /// </summary>
    public class PageInput
    {
        private string _sortType { get; set; } = "asc";

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageRows { get; set; } = int.MaxValue;

        /// <summary>
        /// 排序列
        /// </summary>
        public string SortField { get; set; } = "Id";

        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortType { get => _sortType; set => _sortType = (value ?? string.Empty).ToLower().Contains("desc") ? "desc" : "asc"; }
    }
}
