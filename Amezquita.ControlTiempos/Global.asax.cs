// ----------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.WebJobs;
using Hangfire;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using MediatR;

namespace Amezquita.ControlTiempos
{
    public class Global : HttpApplication
    {
        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return Assembly.GetExecutingAssembly();
        }

        private void Application_Start(object sender, EventArgs e)
        {
            var hangContainer = new Container();

            hangContainer.Register(() =>
            {
                var dbContext = new ControlTiemposDbContext();

                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Configuration.ValidateOnSaveEnabled = false;

                return dbContext;
            });

            var assemblies = GetAssemblies();

            hangContainer.RegisterSingleton<IMediator, Mediator>();
            hangContainer.Register(typeof(IRequestHandler<,>), assemblies);
            hangContainer.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            hangContainer.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            hangContainer.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            hangContainer.RegisterSingleton(new SingleInstanceFactory(hangContainer.GetInstance));
            hangContainer.RegisterSingleton(new MultiInstanceFactory(hangContainer.GetAllInstances));

            HangfireBootstrapper.Instance.Start(hangContainer);

            TimeZoneInfo zoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            RecurringJob.AddOrUpdate<UsuariosSinRegistrarHaceDiasJob> (j => j.Procesar(), Cron.Daily(08, 00), zoneInfo);
            RecurringJob.AddOrUpdate<ProyectosAlMargenDeEjecucionJob> (j => j.Procesar(), Cron.Daily(08, 15), zoneInfo);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }
    }
}