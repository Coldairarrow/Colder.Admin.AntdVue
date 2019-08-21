namespace Coldairarrow.Util
{
    /// <summary>
    /// 数据库表信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 字段Id
        /// </summary>
        public int ColumnId { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 字段描述说明
        /// </summary>
        public string Description
        {
            get
            {
                return _description.IsNullOrEmpty() ? Name : _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _description { get; set; }
    }
}
