// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioRoles.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using Omu.ValueInjecter;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Roles
{
    public class ServicioRoles : IServicioRoles
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioRoles(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearRolAsync(GuardarRolDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del rol es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("El nombre del rol es obligatorio.");
            }

            if (await _unidadDeTrabajo.Roles.AnyAsync(r => r.Name == dto.Name))
            {
                throw new ArgumentException("El rol ya se encuentra registrado.");
            }

            var rol = new Rol(dto.Id, dto.Name);

            rol.InjectFrom(dto);

            _unidadDeTrabajo.Roles.Add(rol);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarRolAsync(GuardarRolDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del rol es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("El nombre del rol es obligatorio.");
            }

            if (await _unidadDeTrabajo.Roles.AnyAsync(r => r.Id != dto.Id && r.Name == dto.Name))
            {
                throw new ArgumentException("El rol ya se encuentra registrado.");
            }

            var rol = await _unidadDeTrabajo.Roles.FindAsync(dto.Id);

            if (rol == null)
            {
                throw new ArgumentException("El rol que desea editar no existe o no se encuentra registrado");
            }

            rol.Name = dto.Name;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarRolAsync(EliminarRolDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del rol es obligatorio.");
            }

            var relacionado = _unidadDeTrabajo.Usuarios
                                              .Any(t => t.Roles.Any(a => a.Id == dto.Id));

            if (relacionado)
            {
                throw new ArgumentException("No es posible eliminar el ya que se encuentra asociada a uno o más usuarios.");
            }

            var rol = await _unidadDeTrabajo.Roles.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (rol == null)
            {
                throw new ArgumentException("El rol a eliminar no existe o no se encuentra registrada.");
            }

            _unidadDeTrabajo.Roles.Remove(rol);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<Paginado<RolDTO>> ObtenerRolesPorPaginaAsync(ObtenerRolesPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Roles
                                           .OrderBy(t => t.Name)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Name.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Rol> roles;

            if (paginaDTO.NumeroPagina == 1)
            {
                roles = await consulta.Take(paginaDTO.RegistrosPagina)
                                      .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                roles = await consulta.Skip(skip)
                                      .Take(paginaDTO.RegistrosPagina)
                                      .ToListAsync();
            }

            var rolesDTO = new List<RolDTO>();

            rolesDTO.InjectFrom(roles);

            var paginadoDTO = new Paginado<RolDTO>(rolesDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        public async Task<RolDTO> ObtenerRolPorIdAsync(ObtenerRolPorIdDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del roles obligatorio.");
            }

            var rol = await _unidadDeTrabajo.Roles.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (rol == null)
            {
                return null;
            }

            var rolDTO = new RolDTO();

            rolDTO.InjectFrom(rol);

            return rolDTO;
        }

        #endregion
    }
}