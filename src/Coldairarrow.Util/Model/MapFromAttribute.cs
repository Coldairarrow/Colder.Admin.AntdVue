using System;

namespace Coldairarrow.Util
{
    public class MapFromAttribute : Attribute
    {
        public MapFromAttribute(Type fromType)
        {
            FromType = fromType;
        }
        public Type FromType { get; }
    }
}
