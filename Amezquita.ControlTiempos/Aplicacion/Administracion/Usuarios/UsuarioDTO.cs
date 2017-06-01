// ----------------------------------------------------------------------------------------------
// <copyright file="UsuarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios
{
    public class UsuarioDTO
    {
        #region Instance Properties

        public int AccesosFallidos { get; set; }

        public bool Bloqueado { get; set; }

        public Guid CargoId { get; set; }

        public string CargoNombre { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmado { get; set; }

        public DateTime? FechaBloqueo { get; set; }

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string UserName { get; set; }

        public string Cedula { get; set; }

        public int ReglaCargueLimiteDias { get; set; }

        public decimal ReglaCargueLimiteHoraFraccion { get; set; }

        #endregion
    }
}