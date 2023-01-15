using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using BaseFramework.Core;

namespace ScriptsHotfix
{
    /// <summary>
    /// 基础socket客户端
    /// </summary>
    public class BaseSocketClient
    {
        public event Action OnConnectedEvent;//连接成功
        public event Action OnConnectFailedEvent;//连接失败
        public event Action OnDisconnectedEvent;//连接断开
        // public event Action OnConnectTimeoutEvent;//连接超时


        public BaseSocketClient(string ip, int port, 
            AddressFamily address = AddressFamily.InterNetwork, 
            SocketType sType = SocketType.Stream, 
            ProtocolType pType = ProtocolType.Tcp)
        {
            _ip = ip;
            _port = port;

            _sType = sType;
            _pType = pType;
            _address = address;
            _socket = new Socket(address, _sType, _pType);

            Ctor();
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public virtual void ConnectServer()
        {
            _socket.BeginConnect(_ip, _port, OnConnectedMsg, null);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public virtual void SendMsg(byte[] msg)
        {
            _socket.Send(msg);
        }

        /// <summary>
        /// 连接成功的回调
        /// </summary>
        /// <param name="ar"></param>
        protected void OnConnectedMsg(IAsyncResult asyncResult)
        {
            // 判断连接状态，进行不同的回调
            Socket socket = asyncResult.AsyncState as Socket;
            if (socket == null)
            {
                return;
            }

            if (_socket != null && _socket == socket)
            {
                try
                {
                    _socket.EndConnect(asyncResult);
                    _isConnected = true;
                    OnConnectedEvent?.Invoke();
                    _socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, OnReciveMsg, _socket);
                }
                catch (SocketException socketException)
                {
                    Close();
                    if (socketException.ErrorCode == (int)SocketError.ConnectionRefused)
                    {
                        OnConnectFailedEvent?.Invoke();
                    }
                    else if (socketException.SocketErrorCode == SocketError.NetworkDown)
                    {
                        OnDisconnectedEvent?.Invoke();
                    }
                    else if (socketException.SocketErrorCode == SocketError.NetworkUnreachable)
                    {
                        OnConnectFailedEvent?.Invoke();
                    }
                    else
                    {
                        OnConnectFailedEvent?.Invoke();
                    }

                    Log.Error("connect failed! exception={0}", socketException);
                }
                catch (Exception exception)
                {
                    Close();
                    Log.Error("connect failed! exception={0}", exception);
                }
            }

        }
        public void Close()
        {
            lock (this)
            {
                if (_socket != null)
                {
                    try
                    {
                        if (_socket.Connected)
                        {
                            _socket.Shutdown(SocketShutdown.Both);
                            _socket.Disconnect(false);
                            _socket.Close();
                        }
                        else
                        {
                            _socket.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        Log.Error("close socket failed! exception={0}", exception);
                    }
                    finally
                    {
                        _socket = null;
                        _isConnected = false;
                    }
                }
            }
        }

        /// <summary>
        /// 接收信息的回调
        /// </summary>
        /// <param name="ar"></param>
        protected void OnReciveMsg(IAsyncResult ar)
        {
            try
            {
                int len = _socket.EndReceive(ar);
                if (len > 0 && ar.IsCompleted)
                {
                    OnReadMsg(len, _buffer);
                }
                else if (len==0)
                {
                    OnDisconnectedEvent?.Invoke();
                }
                
                _socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, OnReciveMsg, _socket);
            }
            catch (SocketException socketException)
            {
                if (socketException.SocketErrorCode == SocketError.ConnectionReset)
                {
                    OnDisconnectedEvent?.Invoke();
                }
                else if (socketException.SocketErrorCode == SocketError.NetworkDown)
                {
                    OnDisconnectedEvent?.Invoke();
                }

                Close();
                Log.Error("Socket accept svrMsg error! exception={0}" + socketException);
            }
        }

        /// <summary>
        /// 在线程中读取信息
        /// </summary>
        /// <param name="len"></param>
        /// <param name="buffer"></param>
        protected void OnReadMsg(int len, byte[] buffer)
        {
            if (_threadPool.Count == 0)
            {
                Thread td = new Thread(HandleMsgBegin);
                td.IsBackground = true;
                JySvrMsg svrMsg = new JySvrMsg(0, null, td);

                _threadPool.AddObject(svrMsg);
            }

            try
            {
                var jyMsg = _threadPool.GetObject();
                jyMsg.Len = len;
                jyMsg.Msg = buffer;
                jyMsg.Td.Start(jyMsg);
            }
            catch (ThreadAbortException e) { Log.Info(e.ToString()); }
        }

        /// <summary>
        /// 开始操作msg
        /// </summary>
        /// <param name="msg"></param>
        protected void HandleMsgBegin(object msg)
        {
            lock (msg)
            {
                var jyMsg = (JySvrMsg)msg;
                HandleMsg(jyMsg);
            }
            HandleMsgEnd(msg);
        }

        /// <summary>
        /// 操作msg
        /// </summary>
        /// <param name="svrMsg"></param>
        protected virtual void HandleMsg(JySvrMsg svrMsg)
        {
             
        }

        /// <summary>
        /// 操作完成,回收对象
        /// </summary>
        /// <param name="msg"></param>
        protected void HandleMsgEnd(object msg)
        {
            try
            {
                var jyMsg = (JySvrMsg)msg;
                _threadPool.ReleaseObject(jyMsg);
            }
            catch(ThreadAbortException e) { Log.Info(e.ToString()); }
        }

        /// <summary>
        /// 其他属性的初始化
        /// </summary>
        protected void Ctor()
        {
            _bufferSize = 1024 * 10;
            _buffer = new byte[_bufferSize];

            _threadPool = new InfinitePool<JySvrMsg>();
            _bufferPool = new InfinitePool<List<byte>>();
        }

        protected int _port;
        protected string _ip;
        protected Socket _socket;
        protected SocketType _sType;
        protected ProtocolType _pType;
        protected AddressFamily _address;
        
        protected byte[] _buffer;
        protected int _bufferSize;
        protected bool _isConnected;

        protected InfinitePool<JySvrMsg> _threadPool;
        protected InfinitePool<List<byte>> _bufferPool;

        /// <summary>
        /// 线程操作需要的参数
        /// </summary>
        protected struct JySvrMsg
        {
            public int Len { get; set; }
            public byte[] Msg { get; set; }
            public Thread Td { get; set; }

            public JySvrMsg(int len, byte[] msg, Thread thread)
            {
                Len = 0;
                Msg = msg;
                Td = thread;
            }
        }
    }
}