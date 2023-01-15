using System;
using System.Collections.Generic;

namespace ScriptsHotfix
{
    public class EasyEvents
    {
        private Dictionary<Type, IEasyEvent> mTypeEvents = new Dictionary<Type, IEasyEvent>();

        public void AddEvent<T>() where T : IEasyEvent, new()
        {
            mTypeEvents.Add(typeof(T), new T());
        }

        public T GetEvent<T>() where T : IEasyEvent
        {
            IEasyEvent e;

            if (mTypeEvents.TryGetValue(typeof(T), out e))
            {
                return (T)e;
            }

            return default;
        }

        public T GetOrAddEvent<T>() where T : IEasyEvent, new()
        {
            var eType = typeof(T);
            if (mTypeEvents.TryGetValue(eType, out var e))
            {
                return (T)e;
            }

            var t = new T();
            mTypeEvents.Add(eType, t);
            return t;
        }
    }
    
    public interface IEasyEvent
    {
    }

    public class EasyEvent<T> : IEasyEvent
    {
        private Action<T> mOnEvent = e => { };

        public void Register(Action<T> onEvent)
        {
            mOnEvent += onEvent;
        }

        public void UnRegister(Action<T> onEvent)
        {
            mOnEvent -= onEvent;
        }

        public void Trigger(T t)
        {
            mOnEvent?.Invoke(t);
        }
    }

    public class EasyEvent<T, K> : IEasyEvent
    {
        private Action<T, K> mOnEvent = (t, k) => { };
    
        public void Register(Action<T, K> onEvent)
        {
            mOnEvent += onEvent;
        }
    
        public void UnRegister(Action<T, K> onEvent)
        {
            mOnEvent -= onEvent;
        }
    
        public void Trigger(T t, K k)
        {
            mOnEvent?.Invoke(t, k);
        }
    }
    //
    // public class EasyEvent<T, K, S> : IEasyEvent
    // {
    //     private Action<T, K, S> mOnEvent = (t, k, s) => { };
    //
    //     public IUnRegister Register(Action<T, K, S> onEvent)
    //     {
    //         mOnEvent += onEvent;
    //         return new CustomUnRegister(() => { UnRegister(onEvent); });
    //     }
    //
    //     public void UnRegister(Action<T, K, S> onEvent)
    //     {
    //         mOnEvent -= onEvent;
    //     }
    //
    //     public void Trigger(T t, K k, S s)
    //     {
    //         mOnEvent?.Invoke(t, k, s);
    //     }
    // }
}