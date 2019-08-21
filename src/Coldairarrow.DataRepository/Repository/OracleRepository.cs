using Coldairarrow.Util;

namespace Coldairarrow.DataRepository
{
    public class OracleRepository : DbRepository, IRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public OracleRepository()
            : base(null, DatabaseType.Oracle)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">数据库连接名</param>
        public OracleRepository(string conStr)
            : base(conStr, DatabaseType.Oracle)
        {
        }

        #endregion

        #region 私有成员

        protected override string FormatFieldName(string name)
        {
            return $"\"{name}\"";
        }

        protected override string FormatParamterName(string name)
        {
            return $":{name}";
        }

        #endregion

        #region 插入数据

        #endregion

        #region 删除数据

        #endregion
    }
}
