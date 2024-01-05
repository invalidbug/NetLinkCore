using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetLinkCore.Common;
using NetLinkCore.Common.Packet;

namespace NetLinkCore.Server
{

    internal enum HandShakeStatus
    {
        None = 0,
        Something1 = 1,
        Something2 = 2,
        Something3 = 3,
        Something4 = 4,
        Complete = 100
    }

    public class NetPeer : INetConnection
    {
        public string PeerId { get; set; } = Guid.NewGuid().ToString();
        private readonly NetServer _server;
        private readonly TcpClient _client;
        public NetPeer(NetServer server, TcpClient client)
        {
            _server = server;
            _client = client;
        }

        public async Task SendPacketAsync(INetPacket packet)
        {
            await _server.SendPacketToPeer(this, packet);
        }

        internal TcpClient GetClient()
        {
            return _client;
        }

    }
}
