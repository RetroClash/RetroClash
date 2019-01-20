using System.Collections.Generic;

namespace RetroClashCore.Network
{
    public class Pool<T>
    {
        private readonly List<T> _list = new List<T>();
        public readonly object SyncObject = new object();

        public T Pop
        {
            get
            {
                lock (SyncObject)
                {
                    if (_list.Count <= 0) return default(T);

                    var item = _list[0];

                    _list.Remove(item);

                    return item;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (SyncObject)
                {
                    return _list.Count;
                }
            }
        }

        public void Push(T item)
        {
            lock (SyncObject)
            {
                if (_list.Count < Configuration.MaxClients)
                    _list.Add(item);
            }
        }
    }
}