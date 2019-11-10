namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 分布式事务工厂
    /// </summary>
    public static class DistributedTransactionFactory
    {
        /// <summary>
        /// 获取分布式事务
        /// </summary>
        /// <param name="repositories">多个仓储</param>
        /// <returns></returns>
        public static ITransaction GetDistributedTransaction(params IRepository[] repositories)
        {
            return new DistributedTransaction(repositories);
        }
    }
}
