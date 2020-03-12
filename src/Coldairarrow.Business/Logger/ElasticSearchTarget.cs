using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Elasticsearch.Net;
using Nest;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public class ElasticSearchTarget : BaseTarget, ILogSearcher, ILogDeleter
    {
        #region 构造函数

        static ElasticSearchTarget()
        {
            string index = $"{GlobalSwitch.ProjectName}.{typeof(Base_Log).Name}".ToLower();

            var pool = new StaticConnectionPool(GlobalSwitch.ElasticSearchNodes);
            _connectionSettings = new ConnectionSettings(pool).DefaultIndex(index);
            _elasticClient = new ElasticClient(_connectionSettings);
            if (!_elasticClient.Indices.Exists(Indices.Parse(index)).Exists)
            {
                var descriptor = new CreateIndexDescriptor(index)
                    .Map(ms => ms
                        .AutoMap<Base_Log>()
                    );
                var res = _elasticClient.Indices.Create(descriptor);
            }
        }

        #endregion

        #region 私有成员

        private static ConnectionSettings _connectionSettings { get; set; }
        private static ElasticClient _elasticClient { get; set; }
        protected override void Write(LogEventInfo logEvent)
        {
            GetElasticClient().IndexDocument(GetBase_SysLogInfo(logEvent));
        }
        private ElasticClient GetElasticClient()
        {
            return _elasticClient;
        }

        #endregion

        #region 外部接口

        public async Task<List<Base_Log>> GetLogListAsync(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            var client = GetElasticClient();
            var filters = new List<Func<QueryContainerDescriptor<Base_Log>, QueryContainer>>();
            if (!logContent.IsNullOrEmpty())
            {
                logContent.Split(' ').ForEach(aKeyword =>
                {
                    filters.Add(q => q.Terms(t => t.Field(f => f.LogContent).Terms(aKeyword.ToLower())));
                });
            }
            if (!logType.IsNullOrEmpty())
                filters.Add(q => q.Terms(t => t.Field(f => f.LogType).Terms(logType)));
            if (!level.IsNullOrEmpty())
                filters.Add(q => q.Terms(t => t.Field(f => f.Level).Terms(level)));
            if (!opUserName.IsNullOrEmpty())
                filters.Add(q => q.Wildcard(w => w.Field(f => f.CreatorRealName).Value($"*{opUserName}*")));
            if (!startTime.IsNullOrEmpty())
                filters.Add(q => q.DateRange(d => d.Field(f => f.CreateTime).GreaterThan(startTime)));
            if (!endTime.IsNullOrEmpty())
                filters.Add(q => q.DateRange(d => d.Field(f => f.CreateTime).LessThan(endTime)));

            SortOrder sortOrder = pagination.SortType.ToLower() == "asc" ? SortOrder.Ascending : SortOrder.Descending;
            var result = await client.SearchAsync<Base_Log>(s =>
                s.Query(q =>
                    q.Bool(b => b.Filter(filters.ToArray()))
                )
                .Sort(o => o.Field(typeof(Base_Log).GetProperty(pagination.SortField), sortOrder))
                .Skip((pagination.PageIndex - 1) * pagination.PageRows)
                .Take(pagination.PageRows)
            );
            pagination.Total = (int)result.Total;

            return result.Documents.ToList();
        }

        public async Task DeleteLogAsync(string logContent, string logType, string level, string opUserName, DateTime? startTime, DateTime? endTime)
        {
            var client = GetElasticClient();
            var filters = new List<Func<QueryContainerDescriptor<Base_Log>, QueryContainer>>();
            if (!logContent.IsNullOrEmpty())
            {
                logContent.Split(' ').ForEach(aKeyword =>
                {
                    filters.Add(q => q.Terms(t => t.Field(f => f.LogContent).Terms(aKeyword.ToLower())));
                });
            }
            if (!logType.IsNullOrEmpty())
                filters.Add(q => q.Terms(t => t.Field(f => f.LogType).Terms(logType)));
            if (!level.IsNullOrEmpty())
                filters.Add(q => q.Terms(t => t.Field(f => f.Level).Terms(level)));
            if (!opUserName.IsNullOrEmpty())
                filters.Add(q => q.Wildcard(w => w.Field(f => f.CreatorRealName).Value($"*{opUserName}*")));
            if (!startTime.IsNullOrEmpty())
                filters.Add(q => q.DateRange(d => d.Field(f => f.CreateTime).GreaterThan(startTime)));
            if (!endTime.IsNullOrEmpty())
                filters.Add(q => q.DateRange(d => d.Field(f => f.CreateTime).LessThan(endTime)));

            await client.DeleteByQueryAsync<Base_Log>(s => s.Query(q =>
                      q.Bool(b => b.Filter(filters.ToArray()))
                ));
        }

        #endregion
    }
}
