using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Coldairarrow.Util
{
    public static partial class Extention
    {
        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        public static IQueryable GetIQueryable(this DbContext context, Type entityType)
        {
            var dbSet = context.GetType().GetMethod("Set").MakeGenericMethod(entityType).Invoke(context, null);
            var resQ = typeof(EntityFrameworkQueryableExtensions).GetMethod("AsNoTracking").MakeGenericMethod(entityType).Invoke(null, new object[] { dbSet });

            return resQ as IQueryable;
        }
    }
}
