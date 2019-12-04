using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Coldairarrow.DataRepository
{
    internal class BaseDbContext : DbContext
    {
        public BaseDbContext([NotNull] DbContextOptions options)
            : base(options)
        {

        }
    }
}
