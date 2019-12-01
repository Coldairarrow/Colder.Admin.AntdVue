using Coldairarrow.Entity.Base_Manage;
using Microsoft.EntityFrameworkCore;

namespace Coldairarrow.Api
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Base_Log> Base_Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=.;Initial Catalog=Colder.Admin.AntdVue;Integrated Security=True;Pooling=true;Max Pool Size=500", x => x.UseRowNumberForPaging());
        }
    }
}