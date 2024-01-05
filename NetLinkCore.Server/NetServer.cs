using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using NetLinkCore.Common;
using NetLinkCore.Common.Packet;
using NetLinkCore.Server.Exceptions;

namespace NetLinkCore.Server
{
    public class NetServer : NetPacketProcessor, IDisposable
    {
        public int ConnectionCount { get; set; } = 0;

        private readonly NetConfig _config;
        private readonly object _serverLock = new object();
        private TcpListener? _listener;
        private CancellationTokenSource? _listenCancellationToken;

        public NetServer(NetConfig config)
        {
            this._config = config;
        }

        public bool IsRunning()
        {
            return _listener is { Server.IsBound: true };
        }

        public void Dispose()
        {
            // stopping will free all resources
            StopInternal();
        }

        #region Start_Stop

        public void Start()
        {
            lock (_serverLock)
            {
                // check if were already running
                if (IsRunning())
                    throw new AlreadyStartedException();

                // if the listener exists (but IsRunning() failed meaning the socket isn't
                // bound) we need to stop first
                if (_listener != null)
                    Stop();

                // create a new listener
                _listener = new TcpListener(IPAddress.Parse(_config.Ip), _config.Port);
                _listener.Start();

                // create a cancelation token for the listener
                _listenCancellationToken = new CancellationTokenSource();

                // create a new background task to listen to new clients
                _ = ListenForClients(_listenCancellationToken!.Token);
            }
        }

        /// <summary>
        /// Stops the server from running
        /// </summary>
        public void Stop()
        {
            lock (_serverLock)
            {
                // make sure we're actually running
                RequireRunning();

                // actual stop call
                StopInternal();
            }
        }

        /// <summary>
        /// Stops the server from running, doesnt verify that we're running however
        /// This is used by Dispose
        /// </summary>
        private void StopInternal()
        {
            // stop the background task
            _listenCancellationToken?.Cancel();

            // stop listening
            _listener?.Stop();
            _listener?.Dispose();
            _listener = null;
        }

        #endregion Start_Stop

        #region Communication

        public async Task<bool> SendPacketToPeer(NetPeer peer, INetPacket packet)
        {
            lock (_serverLock)
            {
                RequireRunning();

                // convert the packet into a byte array
                var packetBytes = SerializePacketToBytes(packet);


                // todo

            }

            return true;
        }

        public async Task BroadcastPacket(INetPacket packet)
        {
            lock (_serverLock)
            {

                RequireRunning();

            }

            // todo
        }

        private async Task ListenForClients(CancellationToken token)
        {
            while (IsRunning() && !token.IsCancellationRequested)
            {
                // IsRunning() checks null
                var newPeerTcp = await _listener!.AcceptTcpClientAsync(token);

                // create a new peer
                var peer = new NetPeer(this, newPeerTcp);


                lock (_serverLock)
                {



                    // increment connection count
                    ConnectionCount += 1;
                }
            }
        }

        private void RequireRunning()
        {
            if (!IsRunning())
                throw new NeverStartedException();
        }

        #endregion Communication
    }
}