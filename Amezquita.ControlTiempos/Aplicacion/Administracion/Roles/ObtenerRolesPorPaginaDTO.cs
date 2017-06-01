// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerRolesPorPaginaDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Roles
{
    public class ObtenerRolesPorPaginaDTO
    {
        #region Instance Properties

        public string Buscar { get; set; }

        public int NumeroPagina { get; set; }

        public int RegistrosPagina { get; set; }

        #endregion
    }
}