using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Features.Usuarios
{
    [Authorize]
    public class UiController : Controller
    {
        private readonly UserManager<Usuario, Guid> _userManager;
        private readonly IDomainUserValidator _domainUserValidator;

        public UiController(UserManager<Usuario, Guid> userManager, IDomainUserValidator domainUserValidator)
        {
            _userManager = userManager;
            _domainUserValidator = domainUserValidator;
        }

        private IAuthenticationManager Autenticacion => HttpContext.GetOwinContext().Authentication;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarSesion()
        {
            Autenticacion.SignOut();
            return RedirectToAction("Index", "Inicio");
        }

        [AllowAnonymous]
        public ActionResult IniciarSesion(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IniciarSesion(IniciarSesion.Command modelo, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(modelo);

                var usuario = await _userManager.FindByNameAsync(modelo.Usuario);

                if (usuario != null && _domainUserValidator.IsValid(modelo.Usuario, modelo.Clave))
                {
                    var identity = await _userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);

                    Autenticacion.SignIn(identity);

                    return Redireccionar(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña no valida.");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(modelo);
        }

        private ActionResult Redireccionar(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Inicio");
        }
    }
}