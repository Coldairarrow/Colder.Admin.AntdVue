using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_DepartmentBusiness : BaseBusiness<Base_Department>, IBase_DepartmentBusiness, IDependency
    {
        #region 外部接口

        public List<Base_DepartmentTreeDTO> GetTreeDataList(string parentId = null)
        {
            var where = LinqHelper.True<Base_Department>();
            if (!parentId.IsNullOrEmpty())
                where = where.And(x => x.ParentId == parentId);
            var list = GetIQueryable().Where(where).ToList()
                .Select(x => new Base_DepartmentTreeDTO
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Text = x.Name,
                    Value = x.Id
                }).ToList();

            return TreeHelper.BuildTree(list);
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
                .GetChildren(allNode, allNode.Where(x => x.Id == departmentId).FirstOrDefault(), true)
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
    }

    public class Base_DepartmentTreeDTO : TreeModel
    {
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
    }
}