using System.Collections.Generic;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 数据表格分页
    /// </summary>
    public class Pagination
    {
        private string _sortType { get; set; } = "asc";

        #region 默认方案

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
        public string SortType { get => _sortType; set => _sortType = (value ?? string.Empty).Contains("desc") ? "desc" : "asc"; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                int pages = Total / PageRows;
                pages = Total % PageRows == 0 ? pages : pages + 1;
                return pages;
            }
        }

        #endregion

        #region AntdVue方案

        public AjaxResult<List<T>> BuildTableResult_AntdVue<T>(List<T> dataList)
        {
            return new AjaxResult<List<T>> { Data = dataList, Success = true, Total = Total, Msg = "操作成功!" };
        }

        #endregion
    }
}
