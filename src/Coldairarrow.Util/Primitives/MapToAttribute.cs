using System;

namespace Coldairarrow.Util
{
    public class MapToAttribute : Attribute
    {
        public MapToAttribute(Type targetType)
        {
            TargetType = targetType;
        }
        public Type TargetType { get; }
    }
}
