using System.Diagnostics;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 数据表格分页
    /// </summary>
    public class Pagination
    {
        #region 构造函数

        public Pagination()
        {
            _watch.Start();

            _sortField = "Id";
            _sortType = "asc";
            PageIndex = 1;
            PageRows = int.MaxValue;
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 当前页码
        /// </summary>
        private int _pageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        private int _pageRows { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        private string _sortField { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        private string _sortType { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        private int _recordCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageCount
        {
            get
            {
                int pages = _recordCount / _pageRows;
                int pageCount = _recordCount % _pageRows == 0 ? pages : pages + 1;
                return pages;
            }
        }

        private Stopwatch _watch { get; set; } = new Stopwatch();

        #endregion

        #region 默认方案

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get => _pageIndex; set => _pageIndex = value; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageRows { get => _pageRows; set => _pageRows = value; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string SortField { get => _sortField; set => _sortField = value; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortType { get => _sortType; set => _sortType = value; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get => _recordCount; set => _recordCount = value; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get => _pageCount; }

        #endregion

        #region jqGrid方案

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get => _pageIndex; set => _pageIndex = value; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get => _pageRows; set => _pageRows = value; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string sidx { get => _sortField; set => _sortField = value; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get => _sortType; set => _sortType = value; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get => _recordCount; set => _recordCount = value; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get => _pageCount; }

        /// <summary>
        /// 构建jqGrid返回的数据
        /// </summary>
        /// <param name="dataList">返回的数据列表</param>
        /// <returns></returns>
        public object BuildTableResult_JqGrid(object dataList)
        {
            _watch.Stop();

            var resData = new
            {
                rows = dataList,
                total = _pageCount,
                page = _pageIndex,
                records = _recordCount,
                costtime = _watch.ElapsedMilliseconds
            };

            return resData;
        }

        #endregion

        #region layui方案

        public int limit { get => _pageRows; set => _pageRows = value; }

        /// <summary>
        /// 构建Layui 返回的表格数据
        /// </summary>
        /// <param name="dataList">返回的数据列表</param>
        /// <returns></returns>
        public object BuildTableResult_Layui(object dataList)
        {
            _watch.Stop();

            var resData = new
            {
                code = 0,
                msg = "获取成功！",
                count = _recordCount,
                data = dataList,
                costtime = _watch.ElapsedMilliseconds
            };

            return resData;
        }

        #endregion

        #region Easyui DataGrid方案

        /// <summary>
        /// 排序列
        /// </summary>
        public string sort { get => _sortField; set => _sortField = value; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string order { get => _sortType; set => _sortType = value; }

        /// <summary>
        /// 构建DataGrid返回的数据
        /// </summary>
        /// <param name="dataList">返回的数据列表</param>
        /// <returns></returns>
        public object BuildTableResult_DataGrid(object dataList)
        {
            _watch.Stop();

            var resData = new
            {
                rows = dataList,
                total = _recordCount,
                page = _pageIndex,
                records = _recordCount,
                costtime = _watch.ElapsedMilliseconds
            };

            return resData;
        }

        #endregion

        #region BootstrapTable方案

        public string sortName { get => _sortField; set => _sortField = value; }
        public string sortOrder { get => _sortType; set => _sortType = value; }
        public int pageNumber { get => _pageIndex; set => _pageIndex = value; }
        public int pageSize { get => _pageRows; set => _pageRows = value; }
        public object BuildTableResult_BootstrapTable(object dataList)
        {
            return BuildTableResult_DataGrid(dataList);
        }

        #endregion
    }
}
