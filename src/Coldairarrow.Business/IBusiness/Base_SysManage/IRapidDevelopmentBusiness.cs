using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IRapidDevelopmentBusiness
    {
        /// <summary>
        /// 获取所有数据库连接
        /// </summary>
        /// <returns></returns>
        List<Base_DatabaseLink> GetAllDbLink();

        /// <summary>
        /// 获取数据库所有表
        /// </summary>
        /// <param name="linkId">数据库连接Id</param>
        /// <returns></returns>
        List<DbTableInfo> GetDbTableList(string linkId);

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="linkId">连接Id</param>
        /// <param name="areaName">区域名</param>
        /// <param name="tables">表列表</param>
        /// <param name="buildType">需要生成类型</param>
        void BuildCode(string linkId, string areaName, string tables, string buildType);
    }
}
