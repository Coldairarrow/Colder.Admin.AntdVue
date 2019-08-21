using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// DbContext容器
    /// </summary>
    public class RepositoryDbContext : IRepositoryDbContext
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conString">数据库连接名或连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="entityNamespace">数据库实体命名空间,注意,该命名空间应该包含所有需要的数据库实体</param>
        public RepositoryDbContext(string conString, DatabaseType dbType)
        {
            _conString = conString;
            _dbType = dbType;
            RefreshDb();
            DbModelFactory.AddObserver(this);
        }

        #endregion

        #region 外部接口

        public void RefreshDb()
        {
            //重用DbConnection,使用底层相同的DbConnection,支持Model持热更新
            DbConnection con = null;
            if (_transaction != null)
                con = _transaction.Connection;
            else
                con = _db?.Database?.GetDbConnection() ?? DbProviderFactoryHelper.GetDbConnection(_conString, _dbType);

            var dBCompiledModel = DbModelFactory.GetDbCompiledModel(_conString, _dbType);
            _db = new BaseDbContext(_dbType,con, dBCompiledModel);
            _db.Database.UseTransaction(_transaction);
            disposedValue = false;
        }

        public DatabaseFacade Database => _db.Database;

        public EntityEntry Entry(object entity)
        {
            var type = entity.GetType();
            var model = CheckModel(entity.GetType());
            object targetObj;
            if (type == model)
                targetObj = entity;
            else
                targetObj = entity.ChangeType(model);

            return _db.Entry(targetObj);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _db.Set<TEntity>();
        }

        public IQueryable GetIQueryable(Type type)
        {
            var model = CheckModel(type);

            return _db.GetIQueryable(model);
        }

        public EntityEntry Attach(object entity)
        {
            var type = entity.GetType();
            var model = CheckModel(entity.GetType());
            object targetObj;
            if (type == model)
                targetObj = entity;
            else
                targetObj = entity.ChangeType(model);

            return _db.Attach(targetObj);
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public DbContext GetDbContext()
        {
            return _db;
        }

        public Type CheckEntityType(Type entityType)
        {
            return CheckModel(entityType);
        }

        public void UseTransaction(DbTransaction transaction)
        {
            if (_transaction == transaction)
                return;

            if (_transaction == null && _db.Database.GetDbConnection() == transaction.Connection)
            {
                _transaction = transaction;
            }
            if (_transaction == null && _db.Database.GetDbConnection() != transaction.Connection)
            {
                _transaction = transaction;
                RefreshDb();
            }
        }

        #endregion

        #region 私有成员

        private DbTransaction _transaction { get; set; }
        private BaseDbContext _db { get; set; }
        private DatabaseType _dbType { get; }
        private string _conString { get; }
        private Type CheckModel(Type type)
        {
            Type model = DbModelFactory.GetModel(type);

            return model;
        }
        private Action<string> _HandleSqlLog { get; set; }

        #endregion

        #region Dispose

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db?.Dispose();
                }
                _transaction = null;
                DbModelFactory.RemoveObserver(this);
                disposedValue = true;
            }
        }

        ~RepositoryDbContext()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        #endregion
    }
}
