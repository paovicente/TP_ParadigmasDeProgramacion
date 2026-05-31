using System;
using System.Collections.Generic;

///
///     This is a generic object pool where the generic type T implements the IPoolable interface
///
namespace EngineGDI
{
    public class ObjectPool<T> where T : IPoolable
    {
        private List<T> objects;

        public List<T> Objects => objects;

        public ObjectPool(int initialSize,Func<T> createFunc)
        {
            objects = new List<T>();

            for (int i = 0; i < initialSize; i++)
            {
                objects.Add(createFunc());
            }
        }

        public T Get()
        {
            foreach (var obj in objects)
            {
                if (!obj.IsActive)
                    return obj;
            }

            return default(T);
        }
    }
}