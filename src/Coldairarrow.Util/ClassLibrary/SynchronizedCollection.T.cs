using Coldairarrow.Util;

namespace System.Collections.Generic
{
    public class SynchronizedCollection<T> : IEnumerable<T>,IDisposable
    {
        #region 外部接口

        public T this[int index]
        {
            get
            {
                using (_lock.Read())
                {
                    return _list[index];
                }
            }
            set
            {
                using (_lock.Write())
                {
                    _list[index] = value;
                }
            }
        }
        public int Count
        {
            get
            {
                using (_lock.Read())
                {
                    return _list.Count;
                }
            }
        }
        public void Add(T item)
        {
            using (_lock.Write())
            {
                _list.Add(item);
            }
        }
        public void Clear()
        {
            using (_lock.Write())
            {
                _list.Clear();
            }
        }
        public bool Contains(T item)
        {
            using (_lock.Read())
            {
                return _list.Contains(item);
            }
        }
        public void CopyTo(T[] array, int index)
        {
            using (_lock.Read())
            {
                _list.CopyTo(array, index);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            using (_lock.Read())
            {
                return _list.GetEnumerator();
            }
        }
        public int IndexOf(T item)
        {
            using (_lock.Read())
            {
                return _list.IndexOf(item);
            }
        }
        public void Insert(int index, T item)
        {
            using (_lock.Write())
            {
                _list.Insert(index, item);
            }
        }
        public bool Remove(T item)
        {
            using (_lock.Write())
            {
                return _list.Remove(item);
            }
        }
        public void RemoveAt(int index)
        {
            using (_lock.Write())
            {
                _list.RemoveAt(index);
            }
        }

        #endregion

        #region 私有成员

        private UsingLock<object> _lock { get; } = new UsingLock<object>();
        private List<T> _list = new List<T>();
        IEnumerator IEnumerable.GetEnumerator()
        {
            using (_lock.Read())
            {
                return _list.GetEnumerator();
            }
        }

        public void Dispose()
        {
            _list.Clear();
            _list = null;

            _lock.Dispose();
        }

        #endregion
    }
}