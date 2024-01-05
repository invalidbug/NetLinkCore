using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLinkCore.Common.Packet;

namespace NetLinkCore.Common
{
    /// <summary>
    /// Any class that has the ability to send a packet
    /// </summary>
    public interface INetConnection
    {
        // sends a packet to the destination
        public Task SendPacketAsync(INetPacket packet);
    }
}
