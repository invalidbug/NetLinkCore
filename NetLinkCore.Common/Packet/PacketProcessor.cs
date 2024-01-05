using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinkCore.Common.Packet
{
    public abstract class NetPacketProcessor
    {
        protected byte[] SerializePacketToBytes(INetPacket packet)
        {
            using MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(packet.GetPacketId());
            packet.Serialize(writer);
            return stream.ToArray();
        }
    }
}
