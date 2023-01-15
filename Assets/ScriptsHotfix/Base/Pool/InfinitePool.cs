using System;

namespace ScriptsHotfix
{
    /// <summary>
    /// 无限容量对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InfinitePool<T> : BaseObjectPool<T>
    {
        public InfinitePool()
        {
            
        }

        /// <summary>
        /// 对象池容量
        /// </summary>
        public override int PoolCapacity { get { return _pool.Count;} }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="obj"> 对象 </param>
        /// <param name="action"> 添加对象时需要做的事 </param>
        public override void AddObject(T obj, Action action = null)
        {
            base.AddObject(obj, action);
            
            _pool.Push(obj);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns> 获得对象池中的对象 </returns>
        public override T GetObject() 
        {
            if (Count > 0)
                return _pool.Pop();

            return default(T);
        }
        
        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="obj"> 对象 </param>
        /// <param name="action"> 释放对象时需要做的事 </param>
        public override void ReleaseObject(T obj, Action action = null)
        {
            base.ReleaseObject(obj, action);
            _pool.Push(obj);
        }
    }
}