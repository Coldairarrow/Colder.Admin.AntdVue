using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    public interface IShardingQueryable<T> where T : class, new()
    {
        IShardingQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IShardingQueryable<T> Where(string predicate, params object[] values);
        IShardingQueryable<T> Skip(int count);
        IShardingQueryable<T> Take(int count);
        IShardingQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
        IShardingQueryable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
        IShardingQueryable<T> OrderBy(string ordering, params object[] values);
        int Count();
        Task<int> CountAsync();
        List<T> ToList();
        Task<List<T>> ToListAsync();
        T FirstOrDefault();
        Task<T> FirstOrDefaultAsync();
        List<T> GetPagination(Pagination pagination);
        Task<List<T>> GetPaginationAsync(Pagination pagination);
        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        TResult Max<TResult>(Expression<Func<T, TResult>> selector);
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector);
        TResult Min<TResult>(Expression<Func<T, TResult>> selector);
        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector);
        double Average(Expression<Func<T, int>> selector);
        Task<double> AverageAsync(Expression<Func<T, int>> selector);
        double? Average(Expression<Func<T, int?>> selector);
        Task<double?> AverageAsync(Expression<Func<T, int?>> selector);
        float Average(Expression<Func<T, float>> selector);
        Task<float> AverageAsync(Expression<Func<T, float>> selector);
        float? Average(Expression<Func<T, float?>> selector);
        Task<float?> AverageAsync(Expression<Func<T, float?>> selector);
        double Average(Expression<Func<T, long>> selector);
        Task<double> AverageAsync(Expression<Func<T, long>> selector);
        double? Average(Expression<Func<T, long?>> selector);
        Task<double?> AverageAsync(Expression<Func<T, long?>> selector);
        double Average(Expression<Func<T, double>> selector);
        Task<double> AverageAsync(Expression<Func<T, double>> selector);
        double? Average(Expression<Func<T, double?>> selector);
        Task<double?> AverageAsync(Expression<Func<T, double?>> selector);
        decimal Average(Expression<Func<T, decimal>> selector);
        Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector);
        decimal? Average(Expression<Func<T, decimal?>> selector);
        Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector);
        decimal Sum(Expression<Func<T, decimal>> selector);
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector);
        decimal? Sum(Expression<Func<T, decimal?>> selector);
        Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector);
        double Sum(Expression<Func<T, double>> selector);
        Task<double> SumAsync(Expression<Func<T, double>> selector);
        double? Sum(Expression<Func<T, double?>> selector);
        Task<double?> SumAsync(Expression<Func<T, double?>> selector);
        float Sum(Expression<Func<T, float>> selector);
        Task<float> SumAsync(Expression<Func<T, float>> selector);
        float? Sum(Expression<Func<T, float?>> selector);
        Task<float?> SumAsync(Expression<Func<T, float?>> selector);
        int Sum(Expression<Func<T, int>> selector);
        Task<int> SumAsync(Expression<Func<T, int>> selector);
        int? Sum(Expression<Func<T, int?>> selector);
        Task<int?> SumAsync(Expression<Func<T, int?>> selector);
        long Sum(Expression<Func<T, long>> selector);
        Task<long> SumAsync(Expression<Func<T, long>> selector);
        long? Sum(Expression<Func<T, long?>> selector);
        Task<long?> SumAsync(Expression<Func<T, long?>> selector);
    }
}
