using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinkCore.Common.Extensions
{
    public class NetException : Exception
    {
        public NetException(string message) : base(message) { }
        public NetException(string message, Exception inner) : base(message, inner) { }
    }
}
