using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Coldairarrow.DataRepository
{
    public interface IShardingQueryable<T> where T : class, new()
    {
        List<T> ToList();
        int Count();
        IShardingQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IShardingQueryable<T> Where(string predicate, params object[] values);
        IShardingQueryable<T> Skip(int count);
        IShardingQueryable<T> Take(int count);
        IShardingQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
        IShardingQueryable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
        IShardingQueryable<T> OrderBy(string ordering, params object[] values);
        T FirstOrDefault();
        List<T> GetPagination(Pagination pagination);
        bool Any(Expression<Func<T, bool>> predicate);
        TResult Max<TResult>(Expression<Func<T, TResult>> selector);
        TResult Min<TResult>(Expression<Func<T, TResult>> selector);
        double Average(Expression<Func<T, int>> selector);
        double? Average(Expression<Func<T, int?>> selector);
        float Average(Expression<Func<T, float>> selector);
        float? Average(Expression<Func<T, float?>> selector);
        double Average(Expression<Func<T, long>> selector);
        double? Average(Expression<Func<T, long?>> selector);
        double Average(Expression<Func<T, double>> selector);
        double? Average(Expression<Func<T, double?>> selector);
        decimal Average(Expression<Func<T, decimal>> selector);
        decimal? Average(Expression<Func<T, decimal?>> selector);
        decimal Sum(Expression<Func<T, decimal>> selector);
        decimal? Sum(Expression<Func<T, decimal?>> selector);
        double Sum(Expression<Func<T, double>> selector);
        double? Sum(Expression<Func<T, double?>> selector);
        float Sum(Expression<Func<T, float>> selector);
        float? Sum(Expression<Func<T, float?>> selector);
        int Sum(Expression<Func<T, int>> selector);
        int? Sum(Expression<Func<T, int?>> selector);
        long Sum(Expression<Func<T, long>> selector);
        long? Sum(Expression<Func<T, long?>> selector);
    }
}
