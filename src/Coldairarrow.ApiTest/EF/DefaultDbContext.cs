using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.BusManage;
using Microsoft.EntityFrameworkCore;

namespace Coldairarrow.Api
{
    public class DefaultDbContext : DbContext
    {
        public const string ConString = "SERVER=localhost;PORT=5432;DATABASE=Colder.Admin.AntdVue;USER ID=postgres;PASSWORD=postgres;Pooling=true;MinPoolSize=40;MaxPoolSize=40;";
        //public const string ConString = "SERVER=61.153.17.10;PORT=5432;DATABASE=db_gxprize_201912;USER ID=u_gxprize_201912;PASSWORD=Cm1nLqOt4t3q-9r4VVWiDdWe;Pooling=true;MaxPoolSize=40;";
        public DefaultDbContext()
        {
            //ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Base_Log> Base_Logs { get; set; }
        public DbSet<Bus_PrizePool> Bus_PrizePools { get; set; }
        public DbSet<Bus_Config> Bus_Configs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConString);
        }
    }
}