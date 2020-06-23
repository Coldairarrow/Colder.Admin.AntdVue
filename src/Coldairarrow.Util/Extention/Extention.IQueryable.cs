using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// IQueryable"T"的拓展操作
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 符合条件则Where
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="q">数据源</param>
        /// <param name="need">是否符合条件</param>
        /// <param name="where">筛选</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> q, bool need, Expression<Func<T, bool>> where)
        {
            if (need)
            {
                return q.Where(where);
            }
            else
            {
                return q;
            }
        }

        /// <summary>
        /// 动态排序法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">IQueryable数据源</param>
        /// <param name="sortColumn">排序的列</param>
        /// <param name="sortType">排序的方法</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortColumn, string sortType)
        {
            //return source.OrderBy(new KeyValuePair<string, string>(sortColumn, sortType));
            return source.OrderBy($"{sortColumn} {sortType}");
        }

        /// <summary>
        /// 动态排序法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="sort">排序规则，Key为排序列，Value为排序类型</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, params KeyValuePair<string, string>[] sort)
        {
            var parameter = Expression.Parameter(typeof(T), "o");

            sort.ForEach((aSort, index) =>
            {
                //根据属性名获取属性
                var property = GetTheProperty(typeof(T), aSort.Key);
                //创建一个访问属性的表达式
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                string OrderName = "";
                if (index > 0)
                {
                    OrderName = aSort.Value.ToLower() == "desc" ? "ThenByDescending" : "ThenBy";
                }
                else
                    OrderName = aSort.Value.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

                MethodCallExpression resultExp = Expression.Call(
                    typeof(Queryable), OrderName,
                    new Type[] { typeof(T), property.PropertyType },
                    source.Expression,
                    Expression.Quote(orderByExp));

                source = source.Provider.CreateQuery<T>(resultExp);
            });

            return (IOrderedQueryable<T>)source;

            //必须追溯到最基类属性
            PropertyInfo GetTheProperty(Type type, string propertyName)
            {
                if (type.BaseType.GetProperties().Any(x => x.Name == propertyName))
                    return GetTheProperty(type.BaseType, propertyName);
                else
                    return type.GetProperty(propertyName);
            }
        }

        /// <summary>
        /// 获取分页数据(包括总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static PageResult<T> GetPageResult<T>(this IQueryable<T> source, PageInput pageInput)
        {
            int count = source.Count();

            var list = source.OrderBy($@"{pageInput.SortField} {pageInput.SortType}")
                .Skip((pageInput.PageIndex - 1) * pageInput.PageRows)
                .Take(pageInput.PageRows)
                .ToList();

            return new PageResult<T> { Data = list, Total = count };
        }

        /// <summary>
        /// 获取分页数据(包括总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static async Task<PageResult<T>> GetPageResultAsync<T>(this IQueryable<T> source, PageInput pageInput)
        {
            int count = await source.CountAsync();

            var list = await source.OrderBy($@"{pageInput.SortField} {pageInput.SortType}")
                .Skip((pageInput.PageIndex - 1) * pageInput.PageRows)
                .Take(pageInput.PageRows)
                .ToListAsync();

            return new PageResult<T> { Data = list, Total = count };
        }

        /// <summary>
        /// 获取分页数据(仅获取列表,不获取总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static List<T> GetPageList<T>(this IQueryable<T> source, PageInput pageInput)
        {
            var list = source.OrderBy($@"{pageInput.SortField} {pageInput.SortType}")
                .Skip((pageInput.PageIndex - 1) * pageInput.PageRows)
                .Take(pageInput.PageRows)
                .ToList();

            return list;
        }

        /// <summary>
        /// 获取分页数据(仅获取列表,不获取总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static async Task<List<T>> GetPageListAsync<T>(this IQueryable<T> source, PageInput pageInput)
        {
            var list = await source.OrderBy($@"{pageInput.SortField} {pageInput.SortType}")
                .Skip((pageInput.PageIndex - 1) * pageInput.PageRows)
                .Take(pageInput.PageRows)
                .ToListAsync();

            return list;
        }
    }
}
