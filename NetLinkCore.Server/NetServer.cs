using NetLinkCore.Common;

namespace NetLinkCore.Server
{
    public class NetServer
    {
        private readonly NetConfig _config;
        private readonly object _lock = new object();

        public NetServer(NetConfig config)
        {
            this._config = config;
        }

        public async Task Start()
        {

        }

    }
}
