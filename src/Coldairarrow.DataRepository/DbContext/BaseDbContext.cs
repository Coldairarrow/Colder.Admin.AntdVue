using Coldairarrow.Util;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    internal class BaseDbContext : DbContext
    {
        public BaseDbContext([NotNull] DbContextOptions options)
            : base(options)
        {

        }

        public void Detach()
        {
            ChangeTracker.Entries().ForEach(aEntry =>
            {
                aEntry.State = EntityState.Detached;
            });
        }

        public override int SaveChanges()
        {
            int count = base.SaveChanges();
            Detach();

            return count;
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int count = await base.SaveChangesAsync(cancellationToken);
            Detach();

            return count;
        }
    }
}
