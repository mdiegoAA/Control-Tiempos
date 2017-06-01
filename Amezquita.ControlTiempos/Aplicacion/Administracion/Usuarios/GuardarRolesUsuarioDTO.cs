// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarRolesUsuarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios
{
    public class GuardarRolesUsuarioDTO
    {
        #region Instance Properties

        public IEnumerable<Guid> Roles { get; set; }

        public Guid UsuarioId { get; set; }

        #endregion
    }
}