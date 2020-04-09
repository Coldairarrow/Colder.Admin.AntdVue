using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 初始化实体必要信息
    /// </summary>
    public class InitEntityAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var entity = context.Parameters[0];
            var op = context.ServiceProvider.GetService<IOperator>();
            if (entity.ContainsProperty("Id"))
                entity.SetPropertyValue("Id", IdHelper.GetId());
            if (entity.ContainsProperty("CreateTime"))
                entity.SetPropertyValue("CreateTime", DateTime.Now);
            if (entity.ContainsProperty("CreatorId"))
                entity.SetPropertyValue("CreatorId", op?.UserId);
            if (entity.ContainsProperty("CreatorRealName"))
                entity.SetPropertyValue("CreatorRealName", op?.Property?.RealName);

            await next(context);
        }
    }
}
