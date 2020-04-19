using AspectCore.DynamicProxy;
using System;

namespace Coldairarrow.Util
{
    public abstract class WriteDataLogAttribute : AbstractInterceptorAttribute
    {
        public WriteDataLogAttribute(UserLogType logType, string nameField, string dataName)
        {
            _logType = logType;
            _dataName = dataName;
            _nameField = nameField;
        }
        protected UserLogType _logType { get; }
        protected string _dataName { get; }
        protected string _nameField { get; }
        protected Type _entityType { get; }
    }
}
