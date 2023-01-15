//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using BaseFramework.Core;

namespace ScriptsHotfix
{
    public sealed class EventManager : Singleton<EventManager>, ISingletonAwake
    {
        private EasyEvents _events;

        public void Awake()
        {
            _events = new EasyEvents();
        }

        #region 一个参数的事件

        public void Subscribe<T>(Action<T> onEvent)
        {
            var e = _events.GetOrAddEvent<EasyEvent<T>>();
            e.Register(onEvent);
        }

        public void Unsubscribe<T>(Action<T> onEvent)
        {
            var e = _events.GetEvent<EasyEvent<T>>();
            if (e != null)
            {
                e.UnRegister(onEvent);
            }
        }
        
        public void Fire<T>() where T : new()
        {
            _events.GetEvent<EasyEvent<T>>()?.Trigger(new T());
        }

        public void Fire<T>(T e)
        {
            _events.GetEvent<EasyEvent<T>>()?.Trigger(e);
        }
        
        // TODO: 事件延迟，下一帧处理
        public void FireNow()
        {
            
        }

        #endregion
        

        #region 分层事件

        public void Subscribe<T, TK>(Action<T, TK> onEvent)
        {
            var e = _events.GetOrAddEvent<EasyEvent<T, TK>>();
            e.Register(onEvent);
        }

        public void Unsubscribe<T, TK>(Action<T, TK> onEvent)
        {
            var e = _events.GetEvent<EasyEvent<T, TK>>();
            if (e != null)
            {
                e.UnRegister(onEvent);
            }
        }
        
        public void Fire<T, TK>() where T : new() 
                                 where TK : new()
        {
            _events.GetEvent<EasyEvent<T, TK>>()?.Trigger(new T(), new TK());
        }

        public void Fire<T, TK>(T t, TK tk)
        {
            _events.GetEvent<EasyEvent<T, TK>>()?.Trigger(t, tk);
        }

        #endregion
    }
}
