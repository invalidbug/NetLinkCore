using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinkCore.Common.Packet
{

    /// <summary>
    /// A Layer in the Packet process that can be undone on the other side
    /// For example Compression, or Encryption
    /// </summary>
    public abstract class PacketTransformer
    {
        private readonly int _transformOrder;

        protected PacketTransformer(int order)
        {
            _transformOrder = order;
        }

        /// <summary>
        /// Called when we send a packet, do any transformations
        /// </summary>
        /// <param name="input">the data sent</param>
        /// <returns>the new data to send</returns>
        public abstract Task<byte[]> TransformAsync(byte[] input);

        // todo: maybe normalize is better
        /// <summary>
        /// Called when we receive a packet, detransform the packet back
        /// </summary>
        /// <param name="input">the data received</param>
        /// <returns>the received data converted back to normal data</returns>
        public abstract Task<byte[]> DetransformAsync(byte[] input);

        /// <summary>
        /// The order in which the transformer is called
        /// </summary>
        /// <returns>Higher = transformed last when sending</returns>
        public int GetOrder()
        {
            return _transformOrder;
        }

        /// <summary>
        /// Called when we are not done with the handshake and we received a response,
        /// we can use the connection to send another response.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public abstract Task OnHandshakePacketReceived(INetConnection connection,
            INetPacket packet);

        /// <summary>
        /// Each transformer may want to do its own handshake process with the
        /// connection. For example RSA would require multiple steps.
        /// While this returns false, the server will redirect all received
        /// packets to OnHandshakePacketReceived instead until all transformers are complete
        /// </summary>
        /// <returns>true if we're done with our handshake</returns>
        public virtual bool IsHandshakeComplete()
        {
            return true;
        }
    }
}
