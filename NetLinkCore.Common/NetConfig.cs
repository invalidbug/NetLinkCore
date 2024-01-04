using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinkCore.Common
{
    /// <summary>
    /// The configuration class that both the server and client share.
    /// This will include things such as pre-packet send/receive interfaces that'll
    /// later be responsible for things such as encryption
    /// </summary>
    public class NetConfig
    {
        /// <summary>
        /// How long the server/client wait when sending and receiving before considering
        /// the connection to have failed/disconnected
        /// </summary>
        public int TimeoutMs { get; set; } = 30000;

        /// <summary>
        /// How big the buffer is for both send and receive in bytes.
        /// </summary>
        public int BufferSize { get; set; } = 4096;

        /// <summary>
        /// The port the server will listen on and the client will connect to
        /// </summary>
        public int Port { get; set; } = 5032;


        /*
         * We use the builder pattern for everything in the config
         */
        /// <summary>
        /// Sets the TimeoutMs for both the client and server
        /// </summary>
        /// <param name="val">How long the timeout is, in milliseconds</param>
        /// <returns>The same config instance</returns>
        public NetConfig SetTimeout(int val)
        {
            TimeoutMs = val;
            return this;
        }

        /// <summary>
        /// Sets the connection port for both the client and server
        /// </summary>
        /// <param name="val">The port we will connect to</param>
        /// <returns>The same config instance</returns>
        public NetConfig SetConnectionPort(int val)
        {
            Port = val;
            return this;
        }

        public NetConfig SetBufferSize(int val)
        {
            BufferSize = val;
            return this;
        }
    }
}