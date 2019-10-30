using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_BuildTestBusiness : BaseBusiness<Base_BuildTest>, IBase_BuildTestBusiness, IDependency
    {
        #region 外部接口

        public List<Base_BuildTest> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_BuildTest>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Base_BuildTest, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public Base_BuildTest GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(Base_BuildTest data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(Base_BuildTest data)
        {
            Update(data);

            return Success();
        }

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