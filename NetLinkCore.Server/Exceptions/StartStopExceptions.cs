using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLinkCore.Common.Extensions;

namespace NetLinkCore.Server.Exceptions
{
    public class AlreadyStartedException : Exception
    {
        public AlreadyStartedException() : base("The connection is already started!") { }
    }

    public class NeverStartedException : NetException
    {
        public NeverStartedException() : base("The connection has either stopped or never started!")
        {
        }
    }
}
