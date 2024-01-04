namespace NetLinkCore.Common
{
    public interface INetPacket
    {
        /// <summary>
        /// Serializes a packet
        /// </summary>
        /// <param name="writer">The Binary writer to write the packet to the stream</param>
        public void Serialize(BinaryWriter writer);

        /// <summary>
        /// Deserializes a packet
        /// </summary>
        /// <param name="reader">The Binary reader to read from the stream</param>
        public void Deserialize(BinaryReader reader);
    }
}