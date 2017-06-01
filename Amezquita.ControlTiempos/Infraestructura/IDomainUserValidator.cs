using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public interface IDomainUserValidator
    {
        bool IsValid(string userName, string password);
    }

    partial class DomainUserValidator : IDomainUserValidator
    {
        public bool IsValid(string userName, string password)
        {
            var validar = Convert.ToBoolean(ConfigurationManager.AppSettings["DirectorioActivo:Validar"]);

            if (!validar)
                return true;

            var nombre = ConfigurationManager.AppSettings["DirectorioActivo:Nombre"];

            if (string.IsNullOrEmpty(userName))
                throw new ConfigurationErrorsException("El nombre del directorio activo no es valido.");

            using (var context = new PrincipalContext(ContextType.Domain, nombre))
                return context.ValidateCredentials(userName, password);
        }
    }
}
