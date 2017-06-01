// ----------------------------------------------------------------------------------------------
// <copyright file="UsuarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class UsuarioDTO
    {
        #region Instance Properties

        public string CargoNombre { get; set; }

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        #endregion
    }
}