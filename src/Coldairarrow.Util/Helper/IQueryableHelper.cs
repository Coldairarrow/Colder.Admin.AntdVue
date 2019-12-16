using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class IQueryableHelper
    {
        /// <summary>
        /// 获取Count
        /// </summary>
        /// <param name="queryable">数据源</param>
        /// <returns></returns>
        public static Task<int> CountAsync(IQueryable queryable)
        {
            dynamic q = queryable;
            return EntityFrameworkQueryableExtensions.CountAsync(q);
        }
    }
}
