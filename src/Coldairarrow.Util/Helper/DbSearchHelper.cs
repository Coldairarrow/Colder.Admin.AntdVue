using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 数据库查询帮助类
    /// </summary>
    public static class DbSearchHelper
    {
        /// <summary>
        /// 获取数据库统计数据
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="groupColumn">分组的列</param>
        /// <param name="statisColumn">统计的列</param>
        /// <param name="funcName">统计方法名(Max,Min,Average,Count())</param>
        /// <returns></returns>
        public static List<DbStatisData> GetDbStatisData(this IQueryable dataSource, string groupColumn, string statisColumn, string funcName)
        {
            List<DbStatisData> resData = new List<DbStatisData>();
            var q = dataSource.GroupBy(groupColumn, "it")
            .Select($"new (it.Key as Key,{funcName}(it.{statisColumn}) as Value)")
            .CastToList<dynamic>();
            foreach (dynamic aData in q)
            {
                DbStatisData newData = new DbStatisData();
                resData.Add(newData);
                
                newData.Key = aData.Key;
                newData.Value = aData.Value;
            }

            return resData;
        }

        /// <summary>
        /// 获取数据库统计数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataSource">数据源</param>
        /// <param name="groupColumn">分组的列</param>
        /// <param name="searchEntris">查询的配置项</param>
        /// <returns></returns>
        public static List<DynamicModel> GetDbStatisData(this IQueryable dataSource, string groupColumn, SearchEntry[] searchEntris)
        {
            if (searchEntris.Count() == 0)
                throw new Exception("请输入至少一个查询配置项");
            StringBuilder selectStr = new StringBuilder();
            selectStr.Append("new (it.Key as Key");
            int count = searchEntris.Count();
            searchEntris.ForEach((item, index) => {
                string end = "";
                if (index == count - 1)
                    end = ")";
                selectStr.Append($",{item.FuncName}(it.{item.StatisColoum}) as {item.ResultName}{end}");
            });

            List<DynamicModel> resData = new List<DynamicModel>();
            var q = dataSource.GroupBy(groupColumn, "it")
            .Select(selectStr.ToString())
            .CastToList<dynamic>();
            foreach (dynamic aData in q)
            {
                DynamicModel newData = new DynamicModel();
                resData.Add(newData);
                object obj = (object)aData;
                Type type = obj.GetType();
                newData.AddProperty("Key", type.GetProperty("Key").GetValue(obj));
                searchEntris.ForEach(item =>
                {
                    newData.AddProperty(item.ResultName, type.GetProperty(item.ResultName).GetValue(obj));
                });
            }

            return resData;
        }

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <param name="obj">包含获取IQueryable方法的对象</param>
        /// <param name="funcName">获取IQueryable的方法名</param>
        /// <param name="entityName">实体名</param>
        /// <param name="nameSpace">命名空间</param>
        /// <returns></returns>
        public static IQueryable GetIQueryable(object obj, string funcName, string entityName, string nameSpace)
        {
            Type type = obj.GetType();
            MethodInfo method = type.GetMethod(funcName);
            var entityType = Assembly.Load(nameSpace).GetTypes().Where(x => x.Name.ToLower().Contains(entityName.ToLower())).FirstOrDefault();
            if (entityType.IsNullOrEmpty())
                throw new Exception("请输入有效的实体名！");

            var iQueryable = (IQueryable)method.MakeGenericMethod(entityType).Invoke(obj, null);

            return iQueryable;
        }
    }

    /// <summary>
    /// 统计数据模型
    /// </summary>
    public class DbStatisData
    {
        /// <summary>
        /// 分组查询的列
        /// </summary>
        public string Key { get; set;}

        /// <summary>
        /// 统计后的数值
        /// </summary>
        public double? Value { get; set; }
    }

    /// <summary>
    /// 统计查询配置项
    /// </summary>
    public struct SearchEntry
    {
        /// <summary>
        /// 统计的列
        /// </summary>
        public string StatisColoum { get; set; }

        /// <summary>
        /// 返回数据列名
        /// </summary>
        public string ResultName { get; set; }

        /// <summary>
        /// 统计方法名（Max,Min,Average,Count()等）
        /// </summary>
        public string FuncName { get; set; }
    }
}
