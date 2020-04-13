using EFCore.Sharding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Util
{
    public class DbContainer:IDisposable
    {
        readonly IServiceProvider _serviceProvider;
        public DbContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool OpenTransaction { get; set; }
        //private 

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return default;
        }

        #region Dispose

        private bool _disposed = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
        }

        #endregion

    }
}
