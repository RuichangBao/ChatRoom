using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool
{
    public class DisposeablePool
    {
        private int num;//默认同类型只保存50个
        private Type type;
        private Queue<object> pool = new Queue<object>();
        public DisposeablePool(Type type) : this(type, 50)
        { }

        public DisposeablePool(Type type, int num)
        {
            this.type = type;
            this.num = num;
        }
        public object GetFetch()
        {
            if (pool.Count > 0)
                return pool.Dequeue();
            return Activator.CreateInstance(type);
        }

        public T GetFetch<T>() where T : class
        {
            if (pool.Count > 0)
                return pool.Dequeue() as T;
            return Activator.CreateInstance(type) as T;
        }
        public bool TryFetch<T>(out T t) where T : class
        {
            if (pool.Count > 0)
            {
                t = pool.Dequeue() as T;
                return t != null;
            }
            t = null;
            return false;
        }

        public void Recycle(object item)
        {
            if (pool.Count >= num)
                return;
            pool.Enqueue(item);
        }
    }
}
