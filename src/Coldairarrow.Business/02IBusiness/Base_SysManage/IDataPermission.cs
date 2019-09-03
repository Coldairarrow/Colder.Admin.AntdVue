using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    /// <summary>
    /// 数据权限控制接口
    /// </summary>
    public interface IDataPermission
    {
        IQueryable<Base_User> GetIQ_Base_User(IRepository repository);
    }
}