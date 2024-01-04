using System.Net.Sockets;
using NetLinkCore.Common;

namespace NetLinkCore.Server
{
    public class NetServer
    {
        private readonly NetConfig _config;
        private readonly object _lock = new object();
        //private TcpListener _listener = new TcpListener();

        public NetServer(NetConfig config)
        {
            this._config = config;
        }

        public async Task Start()
        {

        }

    }
}
