// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioUsuarios.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
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
using Omu.ValueInjecter.Injections;
using System.Diagnostics;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioUsuarios(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearUsuarioAsync(GuardarUsuarioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del usuario es obligatorio.");
            }

            if (dto.CargoId == Guid.Empty)
            {
                throw new ArgumentException("El cargo del usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new ArgumentException("El email del usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.UserName))
            {
                throw new ArgumentException("El usuario es obligatorio.");
            }

            if (dto.ReglaCargueLimiteDias <= 0)
            {
                throw new ArgumentException("El límite de días debe ser mayor que cero.");
            }

            if (dto.ReglaCargueLimiteHoraFraccion <= 0)
            {
                throw new ArgumentException("El límite de hora fracción debe ser mayor que cero.");
            }

            var cargo = await _unidadDeTrabajo.Cargos.FindAsync(dto.CargoId);

            if (cargo == null)
            {
                throw new ArgumentException("El cargo no existe o no se encuentra registrado.");
            }

            var usuario = new Usuario(dto.Id, cargo, dto.UserName, dto.Nombre, dto.Email, dto.ReglaCargueLimiteDias, dto.ReglaCargueLimiteHoraFraccion ,dto.Cedula );

            _unidadDeTrabajo.Usuarios.Add(usuario);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarUsuarioAsync(GuardarUsuarioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del usuario es obligatorio.");
            }

            if (dto.CargoId == Guid.Empty)
            {
                throw new ArgumentException("El cargo del usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new ArgumentException("El email del usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.UserName))
            {
                throw new ArgumentException("El usuario es obligatorio.");
            }

            if (dto.ReglaCargueLimiteDias <= 0)
            {
                throw new ArgumentException("El límite de días debe ser mayor que cero.");
            }

            if (dto.ReglaCargueLimiteHoraFraccion <= 0)
            {
                throw new ArgumentException("El límite de hora fracción debe ser mayor que cero.");
            }

            var cargo = await _unidadDeTrabajo.Cargos.FindAsync(dto.CargoId);

            if (cargo == null)
            {
                throw new ArgumentException("El cargo no existe o no se encuentra registrado.");
            }

            var usuario = await _unidadDeTrabajo.Usuarios
                                                .Include(u => u.Cargo)
                                                .Include(u => u.ReglaCargue)
                                                .FirstOrDefaultAsync(t => t.Id == dto.Id);

            if (usuario == null)
            {
                throw new ArgumentException("El usuario a editar no existe o no se encuentra registrado.");
            }

            if (usuario.ReglaCargue == null)
            {
                var reglaCargue = new ReglaCargue(usuario.Id, dto.ReglaCargueLimiteDias, dto.ReglaCargueLimiteHoraFraccion);

                usuario.ReglaCargue = reglaCargue;
            }
            else
            {
                usuario.ReglaCargue.LimiteDias = dto.ReglaCargueLimiteDias;
                usuario.ReglaCargue.LimiteHoraFraccion = dto.ReglaCargueLimiteHoraFraccion;
            }

            usuario.AccesosFallidos = dto.AccesosFallidos;
            usuario.Bloqueado = dto.Bloqueado;
            usuario.Cargo = cargo;
            usuario.Email = dto.Email;
            usuario.Cedula = dto.Cedula;
            usuario.EmailConfirmado = dto.EmailConfirmado;
            usuario.FechaBloqueo = dto.FechaBloqueo;
            usuario.Nombre = dto.Nombre;
            usuario.UserName = dto.UserName;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(EliminarUsuarioDTO dto)
        {
            _unidadDeTrabajo.Database.Log = s => Debug.WriteLine(s);

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del usuario es obligatorio.");
            }

            var relacionado = _unidadDeTrabajo.Proyectos
                                              .AsNoTracking()
                                              .Any(t => t.Auditor.Id == dto.Id ||
                                                        t.Director.Id == dto.Id ||
                                                        t.Gerente.Id == dto.Id ||
                                                        t.Supervisor.Id == dto.Id ||
                                                        t.GrupoTrabajo.Any(a => a.Id == dto.Id));

            if (relacionado)
            {
                throw new InvalidOperationException("No es posible eliminar el usuario ya que se encuentra asociado a uno o más proyectos.");
            }

            var usuario = await _unidadDeTrabajo.Usuarios
                                                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (usuario == null)
            {
                throw new InvalidOperationException("El usuario a eliminar no existe o no se encuentra registrado.");
            }

            _unidadDeTrabajo.Usuarios.Remove(usuario);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task GuardarRolesUsuarioAsync(GuardarRolesUsuarioDTO dto)
        {
            using (var transaccion = _unidadDeTrabajo.Database.BeginTransaction())
            {
                try
                {
                    var usuario = await _unidadDeTrabajo.Usuarios
                                                        .Include(p => p.Roles)
                                                        .FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);

                    var eliminados = usuario.Roles.Select(r => r.Id)
                                            .Except(dto.Roles, r => r)
                                            .ToList();

                    var nuevas = dto.Roles
                                    .Except(usuario.Roles.Select(r => r.Id).ToList(), r => r)
                                    .ToList();

                    usuario.Roles
                           .Where(r => eliminados.Contains(r.Id))
                           .ToList()
                           .ForEach(r => usuario.Roles.Remove(r));

                    foreach (var r in nuevas)
                    {
                        var nuevo = await _unidadDeTrabajo.Roles.FindAsync(r);

                        usuario.Roles.Add(nuevo);
                    }

                    await _unidadDeTrabajo.SaveChangesAsync();

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();

                    throw;
                }
            }
        }

        public async Task<List<CargoDTO>> ObtenerCargosAsync()
        {
            var cargos = await _unidadDeTrabajo.Cargos
                                               .AsNoTracking()
                                               .ToListAsync();

            var cargosDTO = new List<CargoDTO>();

            cargosDTO.InjectFrom(cargos);

            return cargosDTO;
        }

        public async Task<List<RolDTO>> ObtenerRolesAsync()
        {
            var roles = await _unidadDeTrabajo.Roles
                                              .AsNoTracking()
                                              .ToListAsync();

            var rolesDTO = new List<RolDTO>();

            rolesDTO.InjectFrom(roles);

            return rolesDTO;
        }

        public async Task<List<RolDTO>> ObtenerRolesPorUsuarioAsync(ObteneRolesPorUsuarioDTO dto)
        {
            var roles = await _unidadDeTrabajo.Usuarios
                                              .Where(p => p.Id == dto.Id)
                                              .SelectMany(p => p.Roles)
                                              .AsNoTracking()
                                              .ToListAsync();

            var rolesDTO = new List<RolDTO>();

            rolesDTO.InjectFrom(roles);

            return rolesDTO;
        }

        public async Task<UsuarioDTO> ObtenerUsuarioPorIdAsync(ObtenerUsuarioPorIdDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del usuario es obligatorio.");
            }

            var usuario = await _unidadDeTrabajo.Usuarios
                                                .Include(t => t.Cargo)
                                                .Include(t => t.ReglaCargue)
                                                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (usuario == null)
            {
                return null;
            }

            var usuarioDTO = new UsuarioDTO();

            usuarioDTO.InjectFrom<FlatLoopInjection>(usuario);

            return usuarioDTO;
        }

        public async Task<Paginado<UsuarioDTO>> ObtenerUsuariosPorPaginaAsync(ObtenerUsuariosPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Usuarios
                                           .Include(t => t.Cargo)
                                           .OrderBy(t => t.Nombre)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Nombre.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Usuario> usuarios;

            if (paginaDTO.NumeroPagina == 1)
            {
                usuarios = await consulta.Take(paginaDTO.RegistrosPagina)
                                         .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                usuarios = await consulta.Skip(skip)
                                         .Take(paginaDTO.RegistrosPagina)
                                         .ToListAsync();
            }

            var usuariosDTO = new List<UsuarioDTO>();

            usuariosDTO.InjectFrom(usuarios, new FlatLoopInjection());

            var paginadoDTO = new Paginado<UsuarioDTO>(usuariosDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        #endregion
    }
}