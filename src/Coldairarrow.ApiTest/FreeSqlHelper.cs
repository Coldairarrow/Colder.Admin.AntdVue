using Coldairarrow.Api;
using FreeSql;

namespace Coldairarrow.Util
{
    public static class FreeSqlHelper
    {
        static FreeSqlHelper()
        {
            FreeSql = new FreeSqlBuilder()
                .UseConnectionString(DataType.PostgreSQL, DefaultDbContext.ConString)
                .Build(); //请务必定义成 Singleton 单例模式
        }
        public static readonly IFreeSql FreeSql;
    }
}
