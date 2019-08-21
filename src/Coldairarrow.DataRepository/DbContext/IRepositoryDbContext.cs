using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Coldairarrow.DataRepository
{
    public interface IRepositoryDbContext : IDisposable
    {
        DbContext GetDbContext();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        IQueryable GetIQueryable(Type type);
        EntityEntry Attach(object entity);
        EntityEntry Entry(object entity);
        int SaveChanges();
        DatabaseFacade Database { get; }
        Type CheckEntityType(Type entityType);
        void UseTransaction(DbTransaction transaction);
        void RefreshDb();
    }
}
