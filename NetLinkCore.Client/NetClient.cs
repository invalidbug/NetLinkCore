﻿using NetLinkCore.Common;

namespace NetLinkCore.Client
{
    public class NetClient : INetConnection
    {
        private readonly NetConfig config;

        public NetClient(NetConfig config)
        {
            this.config = config;
        }


        /// <summary>
        /// Send a packet to the server
        /// </summary>
        /// <param name="packet">The packet we'd want to send</param>
        public async Task SendPacket(INetPacket packet)
        {
            // todo:


        }
    }
}
