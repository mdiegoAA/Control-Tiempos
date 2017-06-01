// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioRoles.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Roles
{
    public interface IServicioRoles
    {
        #region Instance Methods

        Task CrearRolAsync(GuardarRolDTO dto);

        Task EditarRolAsync(GuardarRolDTO dto);

        Task EliminarRolAsync(EliminarRolDTO dto);

        Task<Paginado<RolDTO>> ObtenerRolesPorPaginaAsync(ObtenerRolesPorPaginaDTO paginaDTO);

        Task<RolDTO> ObtenerRolPorIdAsync(ObtenerRolPorIdDTO dto);

        #endregion
    }
}