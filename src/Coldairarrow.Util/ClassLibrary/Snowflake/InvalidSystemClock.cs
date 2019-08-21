using System;

namespace Coldairarrow.Util.Snowflake
{
    class InvalidSystemClock : Exception
    {      
        public InvalidSystemClock(string message) : base(message) { }
    }
}