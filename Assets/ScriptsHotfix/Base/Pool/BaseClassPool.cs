using System;
using System.Collections.Generic;

namespace ScriptsHotfix
{
    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseClassPool<T> where T: class, new()
    {
        protected Stack<T> _pool;
        public Stack<T> Pool { get { return _pool;} }

        public virtual int PoolCapacity { get; protected set; }
        public int Count { get { return _pool.Count;} }

        public BaseClassPool()
        {
            _pool = new Stack<T>();
        }

        public virtual void AddClass(T obj, Action action = null)
        {
            _pool.Push(obj);
            action?.Invoke();
        }
        
        public virtual T SpawnClass()
        {
            if (Count > 0)
                return _pool.Pop();
            return new T();
        }

        public virtual void DespawnClass(T obj, Action action = null)
        {
            action?.Invoke();
            _pool.Push(obj);
        }

        public virtual void Clear()
        {
            _pool.Clear();
        }

    }
}