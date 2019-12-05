using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.DataRepository
{
    internal class OracleRepository : DbRepository, IRepository
    {
        #region 构造函数

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

        public override void BulkInsert<T>(List<T> entities)
        {
            throw new Exception("暂不支持");
        }

        #endregion

        #region 删除数据

        #endregion
    }
}
