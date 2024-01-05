using System.Net;
using System.Net.Sockets;
using NetLinkCore.Client.Exceptions;
using NetLinkCore.Common;
using NetLinkCore.Common.Packet;

namespace NetLinkCore.Client
{
    public class NetClient : NetPacketProcessor, INetConnection, IDisposable
    {
        private readonly NetConfig _config;
        private readonly object _lock = new();
        private readonly TcpClient _client = new();

        public NetClient(NetConfig config)
        {
            this._config = config;
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
            await _client.ConnectAsync(_config.Ip, _config.Port);
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

        public bool IsConnected()
        {
            return _client.Connected;
        }

        /// <summary>
        /// Send a packet to the server
        /// </summary>
        /// <param name="packet">The packet we'd want to send</param>
        public async Task SendPacketAsync(INetPacket packet)
        {
            if (!IsConnected())
                throw new NotConnectedException();

            // todo:


        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
