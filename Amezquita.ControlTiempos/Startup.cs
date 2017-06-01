using Amezquita.ControlTiempos;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Migrations;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]

namespace Amezquita.ControlTiempos
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ControlTiemposDbContext, Configuration>());

            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            DependencyResolution.Config(container);

            container.Register<IControllerFactory, ControllerFactory>();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();

            app.UseCors(CorsOptions.AllowAll);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                        {
                                            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                            LoginPath = new PathString("/Usuarios/IniciarSesion"),
                                            Provider = new CookieAuthenticationProvider()
                                        });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Tokens"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(8),
                Provider = new AmezquitaAuthorizationProvider(container.GetInstance<UserManager<Usuario, Guid>>(), 
                                                              container.GetInstance<IDomainUserValidator>())
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseHangfireDashboard("/Jobs", new DashboardOptions { AuthorizationFilters = new[] { new MyRestrictiveAuthorizationFilter() } });
        }
    }
}