using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using NetLinkCore.Common;
using NetLinkCore.Server.Exceptions;

namespace NetLinkCore.Server
{
    public class NetServer : IDisposable
    {
        public int ConnectionCount { get; set; } = 0;

        private readonly NetConfig _config;
        private readonly object _lock = new object();
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

        public void Start()
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

            // make sure we're actually running
            Debug.Assert(IsRunning());
        }

        public void Stop()
        {
            if (!IsRunning())
                throw new NeverStartedException();

            // internal may want to stop without knowing if we started
            StopInternal();
        }

        private void StopInternal()
        {
            // stop the background task
            _listenCancellationToken?.Cancel();

            // stop listening
            _listener?.Stop();
            _listener?.Dispose();
            _listener = null;
        }

        private async Task ListenForClients(CancellationToken token)
        {
            while (IsRunning() && !token.IsCancellationRequested)
            {
                // Is running checks null
                var newPeer = await _listener!.AcceptTcpClientAsync(token);

                lock (_lock)
                {
                    // increment connection count
                    ConnectionCount += 1;
                }
            }
        }






    }
}
