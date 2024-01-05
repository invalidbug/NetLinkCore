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
            using var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(packet.GetPacketId());
            packet.Serialize(writer);
            return stream.ToArray();
        }

        // todo: deserialize

    }
}
