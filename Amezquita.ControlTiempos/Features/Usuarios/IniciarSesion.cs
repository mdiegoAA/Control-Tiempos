using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Features.Usuarios
{
    public class IniciarSesion
    {
        public class Command : IRequest
        {
            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Clave { get; set; }

            [Display(Name = "¿Recordarme?")]
            public bool Recordar { get; set; }

            [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
            [Display(Name = "Usuario")]
            public string Usuario { get; set; }
        }
    }
}