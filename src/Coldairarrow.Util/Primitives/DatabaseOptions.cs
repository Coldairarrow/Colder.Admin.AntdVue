using EFCore.Sharding;

namespace Coldairarrow.Util
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public DatabaseType  DatabaseType { get; set; }
    }
}
