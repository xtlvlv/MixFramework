using System;
using System.Collections.Generic;

namespace ScriptsHotfix
{
    /// <summary>
    /// 网络包
    /// </summary>
    public class NetPackage
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        public string EventMsgId { get { return "NetPackage=" + MsgId; } }

        /// <summary>
        /// 本包的所有长度
        /// </summary>
        public int AllLen { get; private set; }

        /// <summary>
        /// 本消息的ID
        /// </summary>
        public int MsgId { get; private set; }

        /// <summary>
        /// 本包的所有二进制内容
        /// </summary>
        public byte[] AllData
        {
            get
            {
                if(!_isEnCode) EnCode();
                return _allData;
            }
        }

        /// <summary>
        /// 本包包体的内容
        /// </summary>
        public byte[] MsgData { get; private set; }

        /// <summary>
        /// 封包时的构造
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="msgData"></param>
        public NetPackage(int msgId, byte[] msgData)
        {
            MsgId = msgId;
            MsgData = msgData;
            _isEnCode = false;
            // _isDeCode = false;
        }

        /// <summary>
        /// 解包时的构造
        /// </summary>
        /// <param name="allData"></param>
        /// <param name="len"></param>
        public NetPackage(byte[] allData, int len = 0)
        {
            if (len > 0) AllLen = len;
            _allData = allData;
            _isEnCode = false;
            // _isDeCode = false;
        }

        /// <summary>
        /// 设置服务器数据
        /// </summary>
        /// <param name="len"> 数据总长度 </param>
        /// <param name="allData"> 总数据 </param>
        public void SetSvrMsg(int len, byte[] allData)
        {
            AllLen = len;
            _allData = allData;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        public void DeCode()
        {
            int index = INTSIZE;
            MsgId = BitConverter.ToInt32(_allData, index);
            index += INTSIZE;

            int msgLen = AllLen - INTSIZE*2;
            MsgData = new byte[msgLen];
            Array.Copy(_allData, index, MsgData, 0, msgLen);
            // _isDeCode = true;
        }

        /// <summary>
        /// 生成所有数据
        /// </summary>
        private void EnCode()
        {
            int len = MsgData.Length;
            int intSize = sizeof(int);
            int allLen = intSize * 2 + len;

            byte[] msgLenBytes = BitConverter.GetBytes(allLen);
            byte[] msgIdBytes = BitConverter.GetBytes(MsgId);

            List<byte> bufferList = new List<byte>();
            bufferList.AddRange(msgLenBytes);
            bufferList.AddRange(msgIdBytes);
            bufferList.AddRange(MsgData);
            _allData = bufferList.ToArray();
            AllLen = allLen + len;
            _isEnCode = true;
        }

        // todo:待定
        // 是否已经封包
        private bool _isEnCode;
        // 是否已经解包
        // private bool _isDeCode;

        // 所有数据
        private byte[] _allData;
        // 一个int的尺寸
        private const int INTSIZE = sizeof (int);
    }
}