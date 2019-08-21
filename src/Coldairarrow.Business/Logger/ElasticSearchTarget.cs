using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Elasticsearch.Net;
using Nest;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business
{
    public class ElasticSearchTarget : BaseTarget, ILogSearcher
    {
        #region 构造函数

        static ElasticSearchTarget()
        {
            string index = $"{GlobalSwitch.ProjectName}.{typeof(Base_SysLog).Name}".ToLower();

            var pool = new StaticConnectionPool(GlobalSwitch.ElasticSearchNodes);
            _connectionSettings = new ConnectionSettings(pool).DefaultIndex(index);

            _elasticClient = new ElasticClient(_connectionSettings);
            if (!_elasticClient.IndexExists(Indices.Parse(index)).Exists)
            {
                var descriptor = new CreateIndexDescriptor(index)
                    .Mappings(ms => ms
                        .Map<Base_SysLog>(m => m.AutoMap())
                    );
                var res = _elasticClient.CreateIndex(descriptor);
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

        public List<Base_SysLog> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            var client = GetElasticClient();
            var filters = new List<Func<QueryContainerDescriptor<Base_SysLog>, QueryContainer>>();
            if (!logContent.IsNullOrEmpty())
                filters.Add(q => q.Wildcard(w => w.Field(f => f.LogContent).Value($"*{logContent}*")));
            if (!logType.IsNullOrEmpty())
                filters.Add(q => q.Terms(t => t.Field(f => f.LogType).Terms(logType)));
            if (!level.IsNullOrEmpty())
                filters.Add(q => q.Terms(t => t.Field(f => f.Level).Terms(level)));
            if (!opUserName.IsNullOrEmpty())
                filters.Add(q => q.Wildcard(w => w.Field(f => f.OpUserName).Value($"*{opUserName}*")));
            if (!startTime.IsNullOrEmpty())
                filters.Add(q => q.DateRange(d => d.Field(f => f.OpTime).GreaterThan(startTime)));
            if (!endTime.IsNullOrEmpty())
                filters.Add(q => q.DateRange(d => d.Field(f => f.OpTime).LessThan(endTime)));

            SortOrder sortOrder = pagination.SortType.ToLower() == "asc" ? SortOrder.Ascending : SortOrder.Descending;
            var result = client.Search<Base_SysLog>(s =>
                s.Query(q =>
                    q.Bool(b => b.Filter(filters.ToArray()))
                )
                .Sort(o => o.Field(typeof(Base_SysLog).GetProperty(pagination.SortField), sortOrder))
                .Skip((pagination.page - 1) * pagination.rows)
                .Take(pagination.rows)
            );
            pagination.RecordCount = (int)result.Total;

            return result.Documents.ToList();
        }

        #endregion
    }
}
