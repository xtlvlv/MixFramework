using System;
using System.Collections.Generic;

namespace ScriptsHotfix
{
    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseObjectPool<T>
    {
        /// <summary>
        /// 对象池
        /// </summary>
        public Stack<T> Pool { get { return _pool;} }

        /// <summary>
        /// 对象池容量
        /// </summary>
        public virtual int PoolCapacity { get; protected set; }

        /// <summary>
        /// 对象池已有对象的数量
        /// </summary>
        public int Count { get { return _pool.Count;} }

        public BaseObjectPool()
        {
            _pool = new Stack<T>();
        }

        /// <summary>
        /// 获取对象池中的对象
        /// </summary>
        /// <returns></returns>
        public virtual T GetObject()
        {
            return default(T);
        }

        /// <summary>
        /// 添加对象到对象池
        /// </summary>
        /// <param name="obj"></param>
        public virtual void AddObject(T obj, Action action = null)
        {
            action?.Invoke();
        }

        /// <summary>
        /// 释放该对象
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ReleaseObject(T obj, Action action = null)
        {
            action?.Invoke();
        }

        /// <summary>
        /// 清理
        /// </summary>
        public virtual void Clear()
        {
            _pool.Clear();
        }

        protected Stack<T> _pool;
    }
}