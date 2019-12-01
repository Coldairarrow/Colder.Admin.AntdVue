using Coldairarrow.Entity.Base_Manage;
using Microsoft.EntityFrameworkCore;

namespace Coldairarrow.Api
{
    public class DefaultDbContext : DbContext
    {
        public const string ConString = "SERVER=localhost;PORT=5432;DATABASE=Colder.Admin.AntdVue;USER ID=postgres;PASSWORD=postgres;Pooling=true;MinPoolSize=40;MaxPoolSize=40;";
        public DefaultDbContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Base_Log> Base_Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConString);
        }
    }
}