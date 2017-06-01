// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarUsuarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios
{
    public class GuardarUsuarioDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "La cantidad de accesos fallidos es obligatoria.")]
        [Display(Name = "Accesos fallidos")]
        public int AccesosFallidos { get; set; }

        [Display(Name = "Bloqueado")]
        public bool Bloqueado { get; set; }

        [Required(ErrorMessage = "El cargo del usuario es obligatorio.")]
        [Display(Name = "Cargo")]
        public Guid CargoId { get; set; }

        [Required(ErrorMessage = "El email del usuario es obligatorio.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El email del usuario debe se una dirección de correo válida.")]
        public string Email { get; set; }

        [Display(Name = "Email confirmado")]
        public bool EmailConfirmado { get; set; }

        [Display(Name = "Fecha bloqueo")]
        public DateTime? FechaBloqueo { get; set; }

        [Required(ErrorMessage = "El id del usuario es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        public string Cedula { get; set;}

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El límite de días del usuario es obligatorio.")]
        [Display(Name = "Límite Días")]
        [Integer(ErrorMessage = "El límite de días debe ser un número entero válido.")]
        public int ReglaCargueLimiteDias { get; set; }

        [Required(ErrorMessage = "El límite de hora fracción del usuario es obligatorio.")]
        [Display(Name = "Límite Hora Fracción")]
        [Numeric(ErrorMessage = "La hora fracción debe ser un número decimal válido.")]
        public decimal ReglaCargueLimiteHoraFraccion { get; set; }

        #endregion
    }
}