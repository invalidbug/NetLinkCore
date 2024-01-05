namespace NetLinkCore.Common.Packet
{
    public interface INetPacket
    {
        /// <summary>
        /// Get the unique packet id for this Packet
        /// </summary>
        /// <returns>The Packet ID</returns>
        public UInt64 GetPacketId();

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