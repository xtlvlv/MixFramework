using System;
using System.Collections.Generic;

namespace ScriptsHotfix
{
    /// <summary>
    /// 客户端socket
    /// </summary>
    public class SocketClient : BaseSocketClient
    {
        public SocketClient(string ip, int port):base(ip, port)
        {
            _packagePool = new InfinitePool<NetPackage>();
        }

        ///// <summary>
        ///// 连接服务器
        ///// </summary>
        //public override void ConnectServer()
        //{
        //    base.ConnectServer();

        //}

        ///// <summary>
        ///// 发送信息到服务器
        ///// </summary>
        ///// <param name="msg"></param>
        //public override void SendMsg(byte[] msg)
        //{
        //    base.SendMsg(msg);

        //}

        /// <summary>
        /// 发送包
        /// </summary>
        /// <param name="package"></param>
        public void SendPackage(NetPackage package)
        {
            SendMsg(package.AllData);
        }

        /// <summary>
        /// 对服务器数据进行操作
        /// </summary>
        /// <param name="svrMsg"></param>
        protected override void HandleMsg(JySvrMsg svrMsg)
        {
            int index = 0;
            HandleStickyData(svrMsg, index);
        }


        /// <summary>
        /// 处理粘包
        /// </summary>
        /// <param name="svrMsg"> 粘包的数据 </param>
        /// <param name="index"> 处理索引 </param>
        private void HandleStickyData(JySvrMsg svrMsg, int index)
        {
            if (index == svrMsg.Len) return;

            // 1.取长度
            int msgAllLen = BitConverter.ToInt32(svrMsg.Msg, index);
            byte[] buffer = new byte[msgAllLen];
            Array.Copy(svrMsg.Msg, index, buffer, 0, msgAllLen);

            if (_packagePool.Count == 0)
            {
                NetPackage tempPg = new NetPackage(0, null);
                _packagePool.AddObject(tempPg);
            }

            NetPackage pg = _packagePool.GetObject();
            pg.SetSvrMsg(msgAllLen, buffer);
            pg.DeCode();
            if (mMessageHandler.TryGetValue((NetID)pg.MsgId, out var handler))
            {
                MainThreadSynchronizationContext.Instance.Post((object o) =>
                {
                    handler((NetID)pg.MsgId, pg);
                });
            }
            _packagePool.ReleaseObject(pg);
            
            index += msgAllLen;
            HandleStickyData(svrMsg, index);
        }

        private InfinitePool<NetPackage> _packagePool;
        
        public delegate void OnCustomPacket(NetID packet, NetPackage buffer);
        private static Dictionary<NetID, OnCustomPacket> mMessageHandler = new Dictionary<NetID, OnCustomPacket>();

        public OnCustomPacket GetMessageHandler(NetID netID)
        {
            mMessageHandler.TryGetValue(netID, out var handler);
            return handler;
        }
        
        public void RegisterHandler(NetID packet, OnCustomPacket onCustomPacket)
        {
            if (mMessageHandler != null && !mMessageHandler.ContainsKey(packet))
            {
                mMessageHandler.Add(packet, onCustomPacket);
            }
        }

        public void UnregisterHandler(NetID packet)
        {
            if (mMessageHandler != null && mMessageHandler.ContainsKey(packet))
            {
                mMessageHandler.Remove(packet);
            }
        }

        public void ClearMessageHandler()
        {
            mMessageHandler.Clear();
        }
    }
}
