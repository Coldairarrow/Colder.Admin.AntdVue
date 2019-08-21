using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Linq;
using static Coldairarrow.Entity.Base_SysManage.EnumType;

namespace Coldairarrow.Business.Base_SysManage
{
    public class DataPermission : IDataPermission, IDependency
    {
        #region DI

        public IOperator Operator { get => AutofacHelper.GetScopeService<IOperator>(); }
        public IBase_DepartmentBusiness DepartmentBus { get => AutofacHelper.GetScopeService<IBase_DepartmentBusiness>(); }

        #endregion

        #region 外部接口

        public IQueryable<Base_User> GetIQ_Base_User(IRepository repository)
        {
            //根据角色来控制数据权限,超级管理员能够看到所有用户,部门管理员尽能够看到自己部门及下属机构的用户
            var theUser = Operator.Property;
            var role = Operator.Property.RoleType;
            var where = LinqHelper.False<Base_User>();
            if (Operator.IsAdmin())
                where = where.Or(x => true);
            if (role.HasFlag(RoleType.部门管理员))
            {
                var departmentIdList = DepartmentBus.GetChildrenIds(theUser.DepartmentId);
                where = where.Or(x => departmentIdList.Contains(x.DepartmentId));
            }

            return repository.GetIQueryable<Base_User>().Where(where);
        }

        #endregion
    }
}
