

namespace NetLinkCore
{
    public interface INetPacket
    {
        public void Serialize(BinaryWriter writer);
        public void Deserialize(BinaryReader reader);

    }
}
