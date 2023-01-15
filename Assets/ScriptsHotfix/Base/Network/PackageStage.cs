using System;
using System.IO;
using System.Collections.Generic;
using BaseFramework.Core;
using Google.Protobuf;

namespace ScriptsHotfix
{
    /// <summary>
    /// 所有的ID和对应的protobuf对象
    /// </summary>
    public class BasePackageStaged
    {
        public BasePackageStaged()
        {
            Ctor();
        }

        /// <summary>
        /// 设置协议和对象
        /// </summary>
        /// <typeparam name="T"> 对象类型 </typeparam>
        /// <param name="id"> 协议号 </param>
        /// <param name="val"> 对象实例 </param>
        public virtual void SetPackId()
        {
            // todo: 测试
            // AddNetIdAndProtoIns(NetID.Login, new Login());
        }

        /// <summary>
        /// 移除
        /// </summary>
        public virtual void Remove()
        {
            // _netEvent.OnRemove();
        }

        /// <summary>
        /// 添加协议和proto对象
        /// </summary>
        /// <typeparam name="T"> 对象类型 </typeparam>
        /// <param name="id"> 协议号 </param>
        /// <param name="val"> 对象实例 </param>
        protected void AddNetIdAndProtoIns<T>(NetID id, T val) where T : IMessage
        {
            if (_allNetIds.ContainsKey(id))
            {
                _allNetIds[id] = val;
            }
            else
            {
                _allNetIds.Add(id, val);
                // _netEvent.RegistEvent(NetEventId.GetProtobufInsEvent((int)id), OnHandleMsg);
            }
        }

        /// <summary>
        /// 处理接收到服务器数据
        /// </summary>
        /// <param name="args"></param>
        protected void OnHandleMsg(object[] args)
        {
            var package = args[0] as NetPackage;
            if(package == null) return;
            //return;
            NetID nId = (NetID) package.MsgId;

            if (_allNetIds.ContainsKey(nId))
            {
                var protoMsgIns = _allNetIds[nId];
                var newMsg = protoMsgIns.Descriptor.Parser.ParseFrom(package.MsgData);
                _allNetIds[nId] = newMsg;
            }
            SimpleLog.LogInfo(_allNetIds[nId]);
        }

        private void Ctor()
        {
            _allNetIds = new Dictionary<NetID, IMessage>();
        }

        private Dictionary<NetID, IMessage> _allNetIds;

    }
}