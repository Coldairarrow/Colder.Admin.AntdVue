using System;
using System.Collections.Generic;

namespace Coldairarrow.Util
{
    public class DisposableContainer : IDisposableContainer, IDisposable
    {
        SynchronizedCollection<IDisposable> _objList
            = new SynchronizedCollection<IDisposable>();

        public void AddDisposableObj(IDisposable disposableObj)
        {
            if (!_objList.Contains(disposableObj))
                _objList.Add(disposableObj);
        }

        public void Dispose()
        {
            _objList.ForEach(x =>
            {
                try
                {
                    x.Dispose();
                }
                catch
                {

                }
            });
            _objList.Dispose();
            _objList = null;
        }
    }
}

