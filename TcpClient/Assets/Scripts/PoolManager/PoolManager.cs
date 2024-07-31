using System;
using System.Collections.Generic;

namespace Pool
{
    public class PoolManager
    {
        private static Dictionary<Type, DisposeablePool> allPools = new Dictionary<Type, DisposeablePool>();
        public static T GetFetch<T>() where T : class, new()
        {
            Type type = typeof(T);

            if (!allPools.ContainsKey(type) || !allPools[type].TryFetch(out T t))
                return new T();
            return t;
        }
        public static object GetFetch(Type type)
        {
            if (!allPools.ContainsKey(type))
                return Activator.CreateInstance(type);
            DisposeablePool disposeablePool = allPools[type];
            return disposeablePool.GetFetch();
        }
        public static void Recycle(object item)
        {
            Type type = item.GetType();
            if (!allPools.ContainsKey(type))
                allPools[type] = new DisposeablePool(type);
            DisposeablePool disposeablePool = allPools[type];
            disposeablePool.Recycle(item);
        }
    }
}
