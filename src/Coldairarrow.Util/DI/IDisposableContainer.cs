using System;

namespace Coldairarrow.Util
{
    public interface IDisposableContainer : IDisposable
    {
        void AddDisposableObj(IDisposable disposableObj);
    }
}
