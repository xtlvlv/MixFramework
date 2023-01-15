
using System;
using BaseFramework.Core;

namespace ScriptsHotfix
{
    /// <summary>
    /// 固定容量大小对象池
    /// </summary>
    /// <typeparam name="T"> 对象泛类型 </typeparam>
    public class LimitPool<T> : BaseObjectPool<T>
    {
        public LimitPool(int capacity)
        {
            PoolCapacity = capacity;
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
        /// 添加对象
        /// </summary>
        /// <param name="obj"> 对象 </param>
        /// <param name="action"> 添加对象时需要做的事 </param>
        public override void AddObject(T obj, Action action = null)
        {
            base.AddObject(obj, action);
            if (Count < PoolCapacity)
            {
                _pool.Push(obj);
            }
            else
            {
                Log.Warning("LimitPool of {0} is full!", typeof(T));
            }
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