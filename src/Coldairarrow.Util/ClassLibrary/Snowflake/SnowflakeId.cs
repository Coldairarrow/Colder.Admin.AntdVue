using Coldairarrow.Util.Snowflake;
using System;
using System.Linq;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 雪花Id,全局唯一,性能高,取代GUID
    /// </summary>
    public struct SnowflakeId
    {
        public SnowflakeId(long id)
        {
            Id = id;
            var numBin = Convert.ToString(Id, 2).PadLeft(64, '0');
            var newNum = Convert.ToInt64(numBin, 2);
            long timestamp = Convert.ToInt64(new string(numBin.Copy(1, 41).ToArray()), 2) + IdWorker.Twepoch;
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp);
            Time = dateTime.ToLocalTime();
        }
        static SnowflakeId()
        {
            _idWorker = new IdWorker(GlobalSwitch.WorkerId, GlobalSwitch.WorkerId);
        }
        private static IdWorker _idWorker { get; }
        public long Id { get; set; }
        public DateTime Time { get; }
        public static SnowflakeId NewSnowflakeId()
        {
            return new SnowflakeId(_idWorker.NextId());
        }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
