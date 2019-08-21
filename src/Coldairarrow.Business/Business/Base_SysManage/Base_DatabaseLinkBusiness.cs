using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    public class Base_DatabaseLinkBusiness : BaseBusiness<Base_DatabaseLink>, IBase_DatabaseLinkBusiness, IDependency
    {
        #region 外部接口

        public List<Base_DatabaseLink> GetDataList(Pagination pagination)
        {
            return GetIQueryable().GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Base_DatabaseLink GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public AjaxResult AddData(Base_DatabaseLink newData)
        {
            Insert(newData);

            return Success();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public AjaxResult UpdateData(Base_DatabaseLink theData)
        {
            Update(theData);

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}