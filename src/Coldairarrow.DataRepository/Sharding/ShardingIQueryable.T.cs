using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    public class ShardingQueryable<T> : IShardingQueryable<T> where T : class, new()
    {
        public ShardingQueryable(IQueryable<T> source, DistributedTransaction transaction = null)
        {
            _source = source;
            _absTableType = (_source.GetObjQuery() as IQueryable).ElementType;
            _absTableName = _absTableType.Name;
            _transaction = transaction;
        }
        private DistributedTransaction _transaction { get; }
        private bool _openTransaction { get => _transaction?.Disposed == false; }
        private Type _absTableType { get; }
        private string _absTableName { get; }
        private IQueryable<T> _source { get; set; }
        private Type MapTable(Type absTable, string targetTableName)
        {
            return ShardingHelper.MapTable(absTable, targetTableName);
        }
        public List<T> ToList()
        {
            //去除分页,获取前Take+Skip数量
            int? take = _source.GetTakeCount();
            int? skip = _source.GetSkipCount();
            skip = skip == null ? 0 : skip;
            var (sortColumn, sortType) = _source.GetOrderBy();
            var noPaginSource = _source.RemoveTake().RemoveSkip();
            if (!take.IsNullOrEmpty())
                noPaginSource = noPaginSource.Take(take.Value + skip.Value);

            //从各个分表获取数据
            var tables = ShardingConfig.Instance.GetReadTables(_absTableName);
            List<Task<List<T>>> tasks = new List<Task<List<T>>>();
            Dictionary<string, object> lockMap = new Dictionary<string, object>();
            tables.GroupBy(x => x.conString)
                .Select(x => x.Key)
                .ForEach(x => lockMap.Add(x, new object()));
            tables.ForEach(aTable =>
            {
                tasks.Add(Task.Run(() =>
                {
                    var targetTable = MapTable(_absTableType, aTable.tableName);
                    var targetDb = DbFactory.GetRepository(aTable.conString, aTable.dbType);
                    if (_openTransaction)
                        _transaction.AddRepository(targetDb);
                    var targetIQ = targetDb.GetIQueryable(targetTable);
                    var newQ = noPaginSource.ChangeSource(targetIQ);
                    List<T> list = new List<T>();
                    var theLock = lockMap[aTable.conString];
                    if (_openTransaction)
                        lock (theLock)
                        {
                            Run();
                        }
                    else
                        Run();

                    return list;

                    void Run()
                    {
                        list = newQ
                            .CastToList<object>()
                            .Select(x => x.ChangeType<T>())
                            .ToList();
                    }
                }));
            });
            Task.WaitAll(tasks.ToArray());
            List<T> all = new List<T>();
            tasks.ForEach(aTask =>
            {
                all.AddRange(aTask.Result);
            });

            //合并数据
            var resList = all;
            if (!sortColumn.IsNullOrEmpty() && !sortType.IsNullOrEmpty())
                resList = resList.AsQueryable().OrderBy($"{sortColumn} {sortType}").ToList();
            if (!skip.IsNullOrEmpty())
                resList = resList.Skip(skip.Value).ToList();
            if (!take.IsNullOrEmpty())
                resList = resList.Take(take.Value).ToList();

            return resList;
        }
        public T FirstOrDefault()
        {
            return ToList().FirstOrDefault();
        }
        public IShardingQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _source = _source.Where(predicate);

            return this;
        }
        public IShardingQueryable<T> Where(string predicate, params object[] values)
        {
            _source = _source.Where(predicate, values);

            return this;
        }
        public IShardingQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _source = _source.OrderBy(keySelector);

            return this;
        }
        public IShardingQueryable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _source = _source.OrderByDescending(keySelector);

            return this;
        }
        public IShardingQueryable<T> OrderBy(string ordering, params object[] values)
        {
            _source = _source.OrderBy(ordering, values);

            return this;
        }
        public IShardingQueryable<T> Skip(int count)
        {
            _source = _source.Skip(count);

            return this;
        }
        public IShardingQueryable<T> Take(int count)
        {
            _source = _source.Take(count);

            return this;
        }
        public List<T> GetPagination(Pagination pagination)
        {
            pagination.records = Count();
            _source = _source.OrderBy($"{pagination.sidx} {pagination.sord}");

            return Skip((pagination.page - 1) * pagination.rows).Take(pagination.rows).ToList();
        }
        private List<dynamic> GetStatisData(Func<IQueryable, dynamic> access, IQueryable<T> newSource = null)
        {
            newSource = newSource ?? _source;
            var tables = ShardingConfig.Instance.GetReadTables(_absTableName);
            List<Task<dynamic>> tasks = new List<Task<dynamic>>();
            tables.ForEach(aTable =>
            {
                tasks.Add(Task.Run(() =>
                {
                    var targetTable = MapTable(_absTableType, aTable.tableName);
                    var targetIQ = DbFactory.GetRepository(aTable.conString, aTable.dbType).GetIQueryable(targetTable);
                    var newQ = newSource.ChangeSource(targetIQ);

                    return access(newQ);
                }));
            });
            Task.WaitAll(tasks.ToArray());

            return tasks.Select(x => x.Result).ToList();
        }
        public int Count()
        {
            return GetStatisData(x => x.Count()).Sum(x => (int)x);
        }
        public TResult Max<TResult>(Expression<Func<T, TResult>> selector)
        {
            return GetStatisData(x => x.Max(selector)).Max(x => (TResult)x);
        }
        public TResult Min<TResult>(Expression<Func<T, TResult>> selector)
        {
            return GetStatisData(x => x.Min(selector)).Min(x => (TResult)x);
        }
        private dynamic DynamicAverage(dynamic selector)
        {
            var list = GetStatisData(x => new KeyValuePair<int, dynamic>(x.Count(), Coldairarrow.Util.Extention.DynamicSum(x, selector))).Select(x => (KeyValuePair<int, dynamic>)x).ToList();
            var count = list.Sum(x => x.Key);
            dynamic sumList = list.Select(x => (decimal?)x.Value).ToList();
            dynamic sum = Enumerable.Sum(sumList);

            return (decimal?)sum / count;
        }
        private dynamic DynamicSum(dynamic selector)
        {
            return GetStatisData(x => Coldairarrow.Util.Extention.DynamicSum(x, selector)).Sum(x => (decimal?)x);
        }
        public double Average(Expression<Func<T, int>> selector)
        {
            return (double)DynamicAverage(selector);
        }
        public double? Average(Expression<Func<T, int?>> selector)
        {
            return (double?)DynamicAverage(selector);
        }
        public float Average(Expression<Func<T, float>> selector)
        {
            return (float)DynamicAverage(selector);
        }
        public float? Average(Expression<Func<T, float?>> selector)
        {
            return (float?)DynamicAverage(selector);
        }
        public double Average(Expression<Func<T, long>> selector)
        {
            return (double)DynamicAverage(selector);
        }
        public double? Average(Expression<Func<T, long?>> selector)
        {
            return (double?)DynamicAverage(selector);
        }
        public double Average(Expression<Func<T, double>> selector)
        {
            return (double)DynamicAverage(selector);
        }
        public double? Average(Expression<Func<T, double?>> selector)
        {
            return (double?)DynamicAverage(selector);
        }
        public decimal Average(Expression<Func<T, decimal>> selector)
        {
            return (decimal)DynamicAverage(selector);
        }
        public decimal? Average(Expression<Func<T, decimal?>> selector)
        {
            return (decimal?)DynamicAverage(selector);
        }
        public decimal Sum(Expression<Func<T, decimal>> selector)
        {
            return (decimal)DynamicSum(selector);
        }
        public decimal? Sum(Expression<Func<T, decimal?>> selector)
        {
            return (decimal?)DynamicSum(selector);
        }
        public double Sum(Expression<Func<T, double>> selector)
        {
            return (double)DynamicSum(selector);
        }
        public double? Sum(Expression<Func<T, double?>> selector)
        {
            return (double?)DynamicSum(selector);
        }
        public float Sum(Expression<Func<T, float>> selector)
        {
            return (float)DynamicSum(selector);
        }
        public float? Sum(Expression<Func<T, float?>> selector)
        {
            return (float?)DynamicSum(selector);
        }
        public int Sum(Expression<Func<T, int>> selector)
        {
            return (int)DynamicSum(selector);
        }
        public int? Sum(Expression<Func<T, int?>> selector)
        {
            return (int?)DynamicSum(selector);
        }
        public long Sum(Expression<Func<T, long>> selector)
        {
            return (long)DynamicSum(selector);
        }
        public long? Sum(Expression<Func<T, long?>> selector)
        {
            return (long?)DynamicSum(selector);
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            var newSource = _source.Where(predicate);
            return GetStatisData(x => x.Any(), newSource).Any(x => x == true);
        }
    }
}
