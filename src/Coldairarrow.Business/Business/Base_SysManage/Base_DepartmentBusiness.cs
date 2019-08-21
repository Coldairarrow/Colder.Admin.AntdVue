using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    public class Base_DepartmentBusiness : BaseBusiness<Base_Department>, IBase_DepartmentBusiness, IDependency
    {
        #region 外部接口

        public List<Base_Department> GetDataList(Pagination pagination, string departmentName = null)
        {
            var q = GetIQueryable();

            //筛选
            var where = LinqHelper.True<Base_Department>();
            if (!departmentName.IsNullOrEmpty())
                where = where.And(x => x.Name.Contains(departmentName));

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<string> GetChildrenIds(string departmentId)
        {
            var allNode = GetIQueryable().Select(x => new TreeModel
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Text = x.Name,
                Value = x.Id
            }).ToList();

            var children = TreeHelper
                .GetChildren(allNode, allNode.Where(x => x.Id == departmentId).FirstOrDefault())
                .Select(x => x.Id)
                .ToList();

            return children;
        }

        public Base_Department GetTheData(string id)
        {
            return GetEntity(id);
        }

        [DataRepeatValidate(new string[] { "Name" }, new string[] { "部门名" })]
        [DataAddLog(LogType.部门管理, "Name", "部门名")]
        public AjaxResult AddData(Base_Department newData)
        {
            Insert(newData);

            return Success();
        }

        [DataRepeatValidate(new string[] { "Name" }, new string[] { "部门名" })]
        [DataEditLog(LogType.部门管理, "Name", "部门名")]
        public AjaxResult UpdateData(Base_Department theData)
        {
            Update(theData);

            return Success();
        }

        [DataDeleteLog(LogType.部门管理, "Name", "部门名")]
        public AjaxResult DeleteData(List<string> ids)
        {
            if (GetIQueryable().Any(x => ids.Contains(x.ParentId)))
                return Error("禁止删除！请先删除所有子级！");

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