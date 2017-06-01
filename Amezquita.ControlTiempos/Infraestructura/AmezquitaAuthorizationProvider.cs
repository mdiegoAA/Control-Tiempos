using Amezquita.ControlTiempos.Dominio;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class AmezquitaAuthorizationProvider: OAuthAuthorizationServerProvider
    {
        private readonly UserManager<Usuario, Guid> _userManager;
        private readonly IDomainUserValidator _domainUserValidator;

        public AmezquitaAuthorizationProvider(UserManager<Usuario, Guid> userManager, IDomainUserValidator domainUserValidator)
        {
            _userManager= userManager;
            _domainUserValidator= domainUserValidator;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = await _userManager.FindByNameAsync(context.UserName);

            if (usuario != null && _domainUserValidator.IsValid(context.UserName, context.Password))
            {
                var identity = await _userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                var ticket = new AuthenticationTicket(identity, null);

                context.Validated(ticket);

                return;
            }

            context.Rejected();
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return Task.FromResult(context.Validated());
        }
    }
}