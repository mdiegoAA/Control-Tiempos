// ----------------------------------------------------------------------------------------------
// <copyright file="RegisterClientValidationExtensions.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using Amezquita.ControlTiempos;
using DataAnnotationsExtensions.ClientValidation;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(RegisterClientValidationExtensions), "Start")]

namespace Amezquita.ControlTiempos
{
    public static class RegisterClientValidationExtensions
    {
        public static void Start() => DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();
    }
}