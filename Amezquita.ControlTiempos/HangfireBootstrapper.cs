using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SimpleInjector;
using Microsoft.Owin;
using SimpleInjector;
using System.Collections.Generic;
using System.Web.Hosting;

namespace Amezquita.ControlTiempos
{
    public class MyRestrictiveAuthorizationFilter : IAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            var context = new OwinContext(owinEnvironment);
            return context.Authentication.User.IsInRole("Administrador");
        }
    }

    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();
        private readonly object _lockObject = new object();
        private BackgroundJobServer _backgroundJobServer;
        private bool _started;

        private HangfireBootstrapper() {}

        public void Start(Container container)
        {
            lock (_lockObject)
            {
                if (_started)
                    return;

                _started = true;

                HostingEnvironment.RegisterObject(this);

                GlobalConfiguration.Configuration
                                   .UseSqlServerStorage("ControlTiemposDbContext")
                                   .UseActivator(new SimpleInjectorJobActivator(container));

                _backgroundJobServer = new BackgroundJobServer();
            }
        }

        public void Stop()
        {
            lock (_lockObject)
            {
                if (_backgroundJobServer != null)
                    _backgroundJobServer.Dispose();

                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}