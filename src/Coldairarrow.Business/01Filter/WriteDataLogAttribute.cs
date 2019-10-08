using Coldairarrow.Util;
using System;

namespace Coldairarrow.Business
{
    public abstract class WriteDataLogAttribute : BaseFilterAttribute
    {
        public WriteDataLogAttribute(LogType logType, string nameField, string dataName)
        {
            _logType = logType;
            _dataName = dataName;
            _nameField = nameField;
        }
        protected LogType _logType { get; }
        protected string _dataName { get; }
        protected string _nameField { get; }
        protected Type _entityType { get; }
        protected ILogger Logger { get; } = AutofacHelper.GetScopeService<ILogger>();
    }
}
