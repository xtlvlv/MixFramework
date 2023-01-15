using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class NetManager: Singleton<NetManager>, ISingletonAwake
    {
        private SocketClient _client;
        
        public bool IsMobileNetwork
        {
            get
            {
                if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsWiFi
        {
            get
            {
                if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    return true;
                }

                return false;
            }
        }
        
        public void Awake()
        {
            _client = new SocketClient("127.0.0.1", 8888);
            // _client.ConnectServer();

            _client.OnConnectedEvent += OnConnected;
            _client.OnConnectFailedEvent += () => { };
            _client.OnDisconnectedEvent += () => { };
        }

        public void OnConnected()
        {
            
        }
    }
}