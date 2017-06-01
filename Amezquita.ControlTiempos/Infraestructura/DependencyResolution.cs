using Amezquita.ControlTiempos.Aplicacion.Administracion.Areas;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Calendarios;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Clientes;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Roles;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Servicios;
using Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura.Cache;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public static class DependencyResolution
    {
        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return Assembly.GetExecutingAssembly();
        }

        public static void Config(Container container)
        {
            container.Register(() =>
            {
                var dbContext = new ControlTiemposDbContext();

                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Configuration.ValidateOnSaveEnabled = false;

                return dbContext;
            }, Lifestyle.Scoped);

            container.Register(() =>
            {
                var database = container.GetInstance<ControlTiemposDbContext>();
                var userStore = new AlmacenUsuarios(database);
                var userManager = new UserManager<Usuario, Guid>(userStore);

                userManager.UserValidator = new UserValidator<Usuario, Guid>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = false
                };

                userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false
                };

                userManager.UserLockoutEnabledByDefault = true;
                userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
                userManager.MaxFailedAccessAttemptsBeforeLockout = 3;

                return userManager;
            }, Lifestyle.Scoped);

            container.Register(() =>
            {
                var userManager = container.GetInstance<UserManager<Usuario, Guid>>();
                var owinContext = HttpContext.Current.GetOwinContext();

                return new SignInManager<Usuario, Guid>(userManager, owinContext.Authentication);
            }, Lifestyle.Scoped);

            container.Register<IManejadorDeCache, ManejadorDeCacheEnMemoria>();
            container.Register<IGeneradorIdentidad<Guid>>(() => new GuidSecuencial());
            container.Register<IDomainUserValidator, DomainUserValidator>();
            container.Register<IServicioAreas, ServicioAreas>();
            container.Register<IServicioServicios, ServicioServicios>();
            container.Register<IServicioCargos, ServicioCargos>();
            container.Register<IServicioClientes, ServicioClientes>();
            container.Register<IServicioProyectos, ServicioProyectos>();
            container.Register<IServicioUsuarios, ServicioUsuarios>();
            container.Register<IServicioRoles, ServicioRoles>();
            container.Register<IServicioCalendarios, ServicioCalendarios>();

            var assemblies = GetAssemblies();

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));

            container.RegisterCollection(typeof(IDomainEventHandler<>), assemblies);
            DomainEvents.Factory = t => container.GetAllInstances(t);
        }
    }
}