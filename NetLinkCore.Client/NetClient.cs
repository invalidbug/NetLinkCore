using System.Net;
using System.Net.Sockets;
using NetLinkCore.Common;

namespace NetLinkCore.Client
{
    public class NetClient : INetConnection, IDisposable
    {
        private readonly NetConfig _config;
        private readonly object _lock = new();
        private readonly TcpClient _client = new();
        private readonly IPAddress _hostAddress;

        public NetClient(NetConfig config, string connectionIp)
        {
            this._config = config;
            this._hostAddress = IPAddress.Parse(connectionIp);
        }

        /// <summary>
        /// Connects to the server. Will throw a SocketException if it fails.
        /// </summary>
        public async Task ConnectAsync()
        {
            // TODO: what if its already connected?

            // set the client properties
            _client.ReceiveTimeout = _config.TimeoutMs;
            _client.SendTimeout = _config.TimeoutMs;
            _client.SendBufferSize = _config.BufferSize;
            _client.ReceiveBufferSize = _config.BufferSize;

            // actually connect
            await _client.ConnectAsync(_hostAddress, _config.Port);
        }

        /// <summary>
        /// Trys to connect to the server. Will supress a SocketException if it fails,
        /// doesnt supress other exceptions because that means something else is wrong
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TryConnectAsync()
        {
            try
            {
                await ConnectAsync();
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        /// <summary>
        /// Send a packet to the server
        /// </summary>
        /// <param name="packet">The packet we'd want to send</param>
        public async Task SendPacketAsync(INetPacket packet)
        {
            // todo:


        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
