using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Convergence
{
    public class ConcurrentObjectPool<T>
    {
        private ConcurrentBag<T> _pool;
        private Func<T> _factory;
        private int _size;

        public int Size { get { return _size; } }
        public int Available
        {
            get
            {
                return _pool.Count;
            }
        }

        public ConcurrentObjectPool(Func<T> factory)
        {
            _factory = factory;
            _pool = new ConcurrentBag<T>();
        }

        public T Get()
        {
            if (_pool.TryTake(out T item))
            {
                return item;
            }

            // todo set max size and async await for an item
            item = _factory();
            Interlocked.Increment(ref _size);
            return item;
        }

        public void Return(T item)
        {
            // todo dispose of old items eventually
            _pool.Add(item);
        }
    }
}
