using System.Data;

namespace Coldairarrow.DataRepository
{
    internal interface IInternalTransaction : ITransaction
    {
        void BeginTransaction(IsolationLevel isolationLevel);

        void CommitTransaction();

        void RollbackTransaction();
    }
}
