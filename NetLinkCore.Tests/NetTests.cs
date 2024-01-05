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
using Xunit;

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
            Assert.True(server.ConnectionCount == 0);

            // can it connect?
            await client.ConnectAsync();
            Assert.True(client.IsConnected());
            Assert.True(server.ConnectionCount == 1);
        }

        // can it handle multiple clients?
        [Fact]
        public async Task MultipleConnectionTest()
        {
            // create our config
            var cfg = CreateTestConfig();

            // create the server
            using var server = CreateTestServer(cfg);

            // create a bunch of clients
            var clients = new List<NetClient>();
            for (int i = 0; i < 10; i++)
            {
                var client = CreateTestClient(cfg);
                await client.ConnectAsync();
                clients.Add(client);
                Assert.True(client.IsConnected(), "");
            }

            // it may take a bit for all the connections to connect fully.
            // todo: This shouldn't be the case, we should wait for a complete handshake
            //       before returning from ConnectAsync()
            await Task.Delay(100);

            // did they all connect?
            Assert.True(server.ConnectionCount == 10,
                $"Server does not have 10 clients, it has {server.ConnectionCount}!");

            // manually dispose all of the connections
            foreach (var client in clients)
                client.Dispose();
        }

        // can we start and stop the server?
        [Fact]
        public async Task StartAndStopAndStartTest()
        {
            // create our config
            var cfg = CreateTestConfig();

            // create the server
            using var server = CreateTestServer(cfg);

            // stop the server
            server.Stop();
            // start again
            server.Start();

            // try to connect to the server
            using var client = CreateTestClient(cfg);
            await client.ConnectAsync();

            Assert.True(server.IsRunning(), "Server failed to start and stop then start again");
            Assert.True(server.ConnectionCount == 1,
                $"Server did not have 1 connection, it had {server.ConnectionCount}");
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
            Assert.True(server.IsRunning());
            return server;
        }

        private NetClient CreateTestClient(NetConfig cfg)
        {
            var client = new NetClient(cfg);
            return client;
        }
    }
}