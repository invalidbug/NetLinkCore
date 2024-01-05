using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLinkCore;
using NetLinkCore.Client;
using NetLinkCore.Common;
using NetLinkCore.Server;

namespace NetLinkCore.Tests
{

    public class NetTests
    {

        // the inline data will cause it to run twice.
        // this is to make sure that when the server is disposed in code,
        // it can start back up again cleanly
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task SimpleConnectionTest(bool junk)
        {
            // create our config
            var cfg = CreateTestConfig();

            // create the server and the client. They implement IDisposable so we
            // want to make sure they get disposed
            using var server = CreateTestServer(cfg);
            using var client = CreateTestClient(cfg);

            // make sure theres no connections already
            Debug.Assert(server.ConnectionCount == 0);

            // can it connect?
            await client.ConnectAsync();
            Debug.Assert(client.IsConnected());
            Debug.Assert(server.ConnectionCount == 1);
        }

        private NetConfig CreateTestConfig()
        {
            var config = new NetConfig();

            return config;
        }

        private NetServer CreateTestServer(NetConfig cfg)
        {
            var server = new NetServer(cfg);
            server.Start();
            Debug.Assert(server.IsRunning());
            return server;
        }

        private NetClient CreateTestClient(NetConfig cfg)
        {
            var client = new NetClient(cfg);
            return client;
        }
    }
}