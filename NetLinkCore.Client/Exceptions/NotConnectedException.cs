using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLinkCore.Common.Extensions;

namespace NetLinkCore.Client.Exceptions
{
    public class NotConnectedException : NetException
    {
        public NotConnectedException() : base("We are not connected to the server")
        {
        }
    }
}
