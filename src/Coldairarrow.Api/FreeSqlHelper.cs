using FreeSql;

namespace Coldairarrow.Util
{
    public static class FreeSqlHelper
    {
        static FreeSqlHelper()
        {
            var conStr = ConfigHelper.GetConnectionString(GlobalSwitch.DefaultDbConName);
            FreeSql = new FreeSqlBuilder()
                .UseConnectionString(DataType.SqlServer, conStr)
                .Build(); //请务必定义成 Singleton 单例模式
        }
        public static readonly IFreeSql FreeSql;
    }
}
