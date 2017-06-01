// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioProyectos.cs" company="SCI Software">
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
using Omu.ValueInjecter.Injections;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class ServicioProyectos : IServicioProyectos
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;
        private readonly IGeneradorIdentidad<Guid> _identidad;

        #endregion

        #region C'tors

        public ServicioProyectos(ControlTiemposDbContext unidadDeTrabajo, IGeneradorIdentidad<Guid> identidad)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _identidad = identidad;
        }

        #endregion

        #region Instance Methods

        public async Task CrearProyectoAsync(GuardarProyectoDTO dto)
        {
            Usuario supervisor = null;

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del proyecto es obligatorio.");
            }

            if (dto.ClienteId == Guid.Empty)
            {
                throw new ArgumentException("El id del cliente es obligatorio.");
            }

            if (dto.GerenteId == Guid.Empty)
            {
                throw new ArgumentException("El id del gerente es obligatorio.");
            }

            if (dto.DirectorId == Guid.Empty)
            {
                throw new ArgumentException("El id del director es obligatorio.");
            }

            if (dto.AuditorId.HasValue && dto.AuditorId == Guid.Empty)
            {
                throw new ArgumentException("El id del auditor no es valido.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del proyecto es obligatorio.");
            }

            if (dto.FechaInicio == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha inicio del proyecto no es válida.");
            }

            if (dto.FechaFinal == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha final del proyecto no es válida.");
            }

            if (dto.FechaFinal < dto.FechaInicio)
            {
                throw new ArgumentException("La fecha final no puede ser menor que la fecha inicial.");
            }

            if (dto.BolsaHoras <= 0.0m)
            {
                throw new ArgumentException("La cantidad de la bolsa de horas debe ser mayor que cero.");
            }

            var cliente = await _unidadDeTrabajo.Clientes.FindAsync(dto.ClienteId);

            if (cliente == null)
            {
                throw new ArgumentException($"El cliente con el id: {dto.ClienteId}, no existe o no se encuentra registrado");
            }

            var gerente = await _unidadDeTrabajo.Usuarios.FindAsync(dto.GerenteId);

            if (gerente == null)
            {
                throw new ArgumentException($"El gerente con el id: {dto.GerenteId}, no existe o no se encuentra registrado");
            }

            var director = await _unidadDeTrabajo.Usuarios.FindAsync(dto.DirectorId);

            if (director == null)
            {
                throw new ArgumentException($"El director con el id: {dto.DirectorId}, no existe o no se encuentra registrado");
            }
            
            if (dto.SupervisorId.HasValue)
            {
                supervisor = await _unidadDeTrabajo.Usuarios.FindAsync(dto.SupervisorId);

                if (supervisor == null)
                {
                    throw new ArgumentException($"El supervisor con el id: {dto.SupervisorId}, no existe o no se encuentra registrado");
                }
            }

            if (dto.AlertasProyectoDiasSinRegistrar <= 0)
            {
                throw new ArgumentException("Los días sin registrar deben ser mayor que cero.");
            }

            if (dto.AlertasProyectoPorcentajeEjecutado <= 0)
            {
                throw new ArgumentException("El porcentaje ejecutado debe ser mayor que cero.");
            }

            Usuario auditor = null;

            if (dto.AuditorId.HasValue)
            {
                auditor = await _unidadDeTrabajo.Usuarios.FindAsync(dto.AuditorId);

                if (auditor == null)
                {
                    throw new ArgumentException($"El auditor con el id: {dto.AuditorId}, no existe o no se encuentra registrado");
                }
            }

            
            var proyecto = new Proyecto(dto.Id, cliente, dto.Nombre, dto.Año, dto.FechaInicio, dto.FechaFinal, dto.BolsaHoras, gerente, director, supervisor, auditor);
            var alertas = new AlertasProyecto(dto.Id, dto.AlertasProyectoPorcentajeEjecutado, dto.AlertasProyectoDiasSinRegistrar);

            proyecto.AlertasProyecto = alertas;

            _unidadDeTrabajo.Proyectos.Add(proyecto);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarProyectoAsync(GuardarProyectoDTO dto)
        {
            Usuario supervisor = null;

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del proyecto es obligatorio.");
            }

            if (dto.ClienteId == Guid.Empty)
            {
                throw new ArgumentException("El id del cliente es obligatorio.");
            }

            if (dto.GerenteId == Guid.Empty)
            {
                throw new ArgumentException("El id del gerente es obligatorio.");
            }

            if (dto.DirectorId == Guid.Empty)
            {
                throw new ArgumentException("El id del director es obligatorio.");
            }

            if (dto.AuditorId.HasValue && dto.AuditorId == Guid.Empty)
            {
                throw new ArgumentException("El id del auditor es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del proyecto es obligatorio.");
            }

            if (dto.FechaInicio == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha inicio del proyecto no es válida.");
            }

            if (dto.FechaFinal == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha final del proyecto no es válida.");
            }

            if (dto.BolsaHoras <= 0.0m)
            {
                throw new ArgumentException("La cantidad de la bolsa de horas debe ser mayor que cero.");
            }

            var cliente = await _unidadDeTrabajo.Clientes.FindAsync(dto.ClienteId);

            if (cliente == null)
            {
                throw new ArgumentException($"El cliente con el id: {dto.ClienteId}, no existe o no se encuentra registrado");
            }

            var gerente = await _unidadDeTrabajo.Usuarios.FindAsync(dto.GerenteId);

            if (gerente == null)
            {
                throw new ArgumentException($"El gerente con el id: {dto.GerenteId}, no existe o no se encuentra registrado");
            }

            var director = await _unidadDeTrabajo.Usuarios.FindAsync(dto.DirectorId);

            if (director == null)
            {
                throw new ArgumentException($"El director con el id: {dto.DirectorId}, no existe o no se encuentra registrado");
            }

            if (dto.SupervisorId.HasValue)
            {
                supervisor = await _unidadDeTrabajo.Usuarios.FindAsync(dto.SupervisorId);

                if (supervisor == null)
                {
                    throw new ArgumentException($"El supervisor con el id: {dto.SupervisorId}, no existe o no se encuentra registrado");
                }
            }

            if (dto.AlertasProyectoDiasSinRegistrar <= 0)
            {
                throw new ArgumentException("Los días sin registrar deben ser mayor que cero.");
            }

            if (dto.AlertasProyectoPorcentajeEjecutado <= 0)
            {
                throw new ArgumentException("El porcentaje ejecutado debe ser mayor que cero.");
            }

            Usuario auditor = null;

            if (dto.AuditorId.HasValue)
            {
                auditor = await _unidadDeTrabajo.Usuarios.FindAsync(dto.AuditorId);

                if (auditor == null)
                {
                    throw new ArgumentException($"El auditor con el id: {dto.AuditorId}, no existe o no se encuentra registrado");
                }
            }

            var proyecto = await _unidadDeTrabajo.Proyectos
                                                 .Include(p => p.AlertasProyecto)
                                                 .Include(p => p.Cliente)
                                                 .Include(p => p.Gerente)
                                                 .Include(p => p.Director)
                                                 .Include(p => p.Supervisor)
                                                 .Include(p => p.Auditor)
                                                 .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (proyecto.AlertasProyecto == null)
            {
                var alertas = new AlertasProyecto(dto.Id, dto.AlertasProyectoPorcentajeEjecutado, dto.AlertasProyectoDiasSinRegistrar);

                proyecto.AlertasProyecto = alertas;
            }
            else
            {
                proyecto.AlertasProyecto.DiasSinRegistrar = dto.AlertasProyectoDiasSinRegistrar;
                proyecto.AlertasProyecto.PorcentajeEjecutado = dto.AlertasProyectoPorcentajeEjecutado;
            }

            proyecto.Auditor = auditor;
            proyecto.Año = dto.Año;
            proyecto.BolsaHoras = dto.BolsaHoras;
            proyecto.Cliente = cliente;
            proyecto.Director = director;
            proyecto.FechaFinal = dto.FechaFinal;
            proyecto.FechaInicio = dto.FechaInicio;
            proyecto.Gerente = gerente;
            proyecto.Nombre = dto.Nombre;
            proyecto.Supervisor = supervisor;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarProyectoAsync(EliminarProyectoDTO dto)
        {
            var proyecto = await _unidadDeTrabajo.Proyectos
                                                 .Include(s => s.AlertasProyecto)
                                                 .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (proyecto == null)
            {
                throw new InvalidOperationException("El proyecto a eliminar no existe o no encuentra registrado");
            }

            if (await _unidadDeTrabajo.Proyectos.Where(t => t.Id == dto.Id).SelectMany(t => t.Servicios).AnyAsync())
            {
                throw new InvalidOperationException("El proyecto no se puede eliminar porque tiene servicios asociados.");
            }

            if (await _unidadDeTrabajo.Proyectos.Where(t => t.Id == dto.Id).SelectMany(t => t.Actividades).AnyAsync())
            {
                throw new InvalidOperationException("El proyecto no se puede eliminar porque tiene actividades asociadas.");
            }

            if (await _unidadDeTrabajo.Proyectos.Where(t => t.Id == dto.Id).SelectMany(t => t.GrupoTrabajo).AnyAsync())
            {
                throw new InvalidOperationException("El proyecto no se puede eliminar porque tiene usuarios asociadas.");
            }

            _unidadDeTrabajo.Proyectos.Remove(proyecto);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task GuardarActividadesProyectoAsync(GuardarActividadesProyectoDTO dto)
        {
            using (var transaccion = _unidadDeTrabajo.Database.BeginTransaction())
            {
                try
                {
                    var proyecto = await _unidadDeTrabajo.Proyectos
                                                         .Include(p => p.Actividades)
                                                         .FirstOrDefaultAsync(p => p.Id == dto.ProyectoId);

                    var eliminados = proyecto.Actividades.Select(a => a.Id)
                                             .Except(dto.Actividades, a => a)
                                             .ToList();

                    var nuevas = dto.Actividades
                                    .Except(proyecto.Actividades.Select(a => a.Id).ToList(), a => a)
                                    .ToList();

                    proyecto.Actividades
                            .Where(a => eliminados.Contains(a.Id))
                            .ToList()
                            .ForEach(a => proyecto.Actividades.Remove(a));

                    foreach (var a in nuevas)
                    {
                        var nuevo = await _unidadDeTrabajo.Actividades.FindAsync(a);

                        proyecto.Actividades.Add(nuevo);
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

        public async Task GuardarAreasProyectoAsync(GuardarAreasProyectoDTO dto)
        {
            using (var transaccion = _unidadDeTrabajo.Database.BeginTransaction())
            {
                try
                {
                    var proyecto = await _unidadDeTrabajo.Proyectos
                                                         .Include(p => p.AreasProyecto)
                                                         .FirstOrDefaultAsync(p => p.Id == dto.ProyectoId);

                    var eliminadas = proyecto.AreasProyecto
                                             .Where(ap => !dto.Areas.Select(a => a.AreaId).Contains(ap.AreaId))
                                             .ToList();

                    var editadas = proyecto.AreasProyecto
                                           .Where(ap => dto.Areas.Select(a => a.AreaId).Contains(ap.AreaId))
                                           .ToList();

                    var nuevas = dto.Areas
                                    .Where(ap => !proyecto.AreasProyecto.Select(a => a.AreaId).Contains(ap.AreaId))
                                    .ToList();

                    eliminadas.ForEach(ap =>
                                       {
                                           _unidadDeTrabajo.AreasProyecto.Remove(ap);
                                       });

                    editadas.ForEach(ap =>
                                     {
                                         var editada = dto.Areas.Single(e => e.AreaId == ap.AreaId);
                                         ap.Horas = editada.Horas;
                                     });

                    nuevas.ForEach(ap =>
                                   {
                                       var nueva = new AreaProyecto(_identidad.GenerarId(), dto.ProyectoId, ap.AreaId, ap.Horas);
                                       proyecto.AreasProyecto.Add(nueva);
                                   });

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

        public async Task GuardarGrupoDeTrabajoProyectoAsync(GuardarGrupoDeTrabajoProyectoDTO dto)
        {
            using (var transaccion = _unidadDeTrabajo.Database.BeginTransaction())
            {
                try
                {
                    var proyecto = await _unidadDeTrabajo.Proyectos
                                                         .Include(p => p.GrupoTrabajo)
                                                         .FirstOrDefaultAsync(p => p.Id == dto.ProyectoId);

                    var eliminados = proyecto.GrupoTrabajo.Select(u => u.Id)
                                             .Except(dto.Usuarios, u => u)
                                             .ToList();

                    var nuevas = dto.Usuarios
                                    .Except(proyecto.GrupoTrabajo.Select(u => u.Id).ToList(), u => u)
                                    .ToList();

                    proyecto.GrupoTrabajo
                            .Where(u => eliminados.Contains(u.Id))
                            .ToList()
                            .ForEach(u => proyecto.GrupoTrabajo.Remove(u));

                    foreach (var u in nuevas)
                    {
                        var nuevo = await _unidadDeTrabajo.Usuarios.FindAsync(u);

                        proyecto.GrupoTrabajo.Add(nuevo);
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

        public async Task GuardarServiciosProyectoAsync(GuardarServiciosProyectoDTO dto)
        {
            using (var transaccion = _unidadDeTrabajo.Database.BeginTransaction())
            {
                try
                {
                    var proyecto = await _unidadDeTrabajo.Proyectos
                                                         .Include(p => p.Servicios)
                                                         .FirstOrDefaultAsync(p => p.Id == dto.ProyectoId);

                    var eliminados = proyecto.Servicios.Select(s => s.Id)
                                             .Except(dto.Servicios, s => s)
                                             .ToList();

                    var nuevas = dto.Servicios
                                    .Except(proyecto.Servicios.Select(s => s.Id).ToList(), s => s)
                                    .ToList();

                    proyecto.Servicios
                            .Where(s => eliminados.Contains(s.Id))
                            .ToList()
                            .ForEach(s => proyecto.Servicios.Remove(s));

                    foreach (var s in nuevas)
                    {
                        var nuevo = await _unidadDeTrabajo.Servicios.FindAsync(s);

                        proyecto.Servicios.Add(nuevo);
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

        public async Task<List<ActividadDTO>> ObtenerActvidadesAsync()
        {
            var actividades = await _unidadDeTrabajo.Actividades
                                                    .AsNoTracking()
                                                    .OrderBy(t => t.Nombre)
                                                    .ToListAsync();

            var actividadesDTO = new List<ActividadDTO>();

            actividadesDTO.InjectFrom(actividades);

            return actividadesDTO;
        }

        public async Task<List<ActividadDTO>> ObtenerActvidadesPorProyectoAsync(ObtenerActividadesPorProyectoDTO dto)
        {
            var actividades = await _unidadDeTrabajo.Proyectos
                                                    .Where(p => p.Id == dto.Id)
                                                    .SelectMany(p => p.Actividades)
                                                    .AsNoTracking()
                                                    .OrderBy(t => t.Nombre)
                                                    .ToListAsync();

            var actividadesDTO = new List<ActividadDTO>();

            actividadesDTO.InjectFrom(actividades);

            return actividadesDTO;
        }

        public async Task<List<AreaDTO>> ObtenerAreasAsync()
        {
            var areas = await _unidadDeTrabajo.Areas
                                              .AsNoTracking()
                                              .OrderBy(t => t.Nombre)
                                              .ToListAsync();

            var areasDTO = new List<AreaDTO>();

            areasDTO.InjectFrom(areas);

            return areasDTO;
        }

        public async Task<List<AreaProyectoDTO>> ObtenerAreasProyectoAsync(ObtenerAreasProyectoDTO dto)
        {
            var proyecto = await _unidadDeTrabajo.Proyectos
                                                 .Include(p => p.AreasProyecto)
                                                 .Include(p => p.AreasProyecto.Select(ap => ap.Area))
                                                 .AsNoTracking()
                                                 .OrderBy(t => t.Nombre)
                                                 .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (proyecto == null)
            {
                return null;
            }

            var areasProyectoDTO = new List<AreaProyectoDTO>();

            areasProyectoDTO.InjectFrom(proyecto.AreasProyecto, new FlatLoopInjection());

            return areasProyectoDTO;
        }

        public async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            var clientes = await _unidadDeTrabajo.Clientes
                                                 .AsNoTracking()
                                                 .OrderBy(t => t.Nombre)
                                                 .ToListAsync();

            var clientedDTO = new List<ClienteDTO>();

            clientedDTO.InjectFrom(clientes);

            return clientedDTO;
        }

        public async Task<List<UsuarioDTO>> ObtenerGrupoDeTrabajoPorProyectoAsync(ObtenerGrupoDeTrabajoPorProyectoDTO dto)
        {
            var usuarios = await _unidadDeTrabajo.Proyectos
                                                 .Where(p => p.Id == dto.Id)
                                                 .SelectMany(p => p.GrupoTrabajo)
                                                 .AsNoTracking()
                                                 .OrderBy(t => t.Nombre)
                                                 .ToListAsync();

            var usuariosDTO = new List<UsuarioDTO>();

            usuariosDTO.InjectFrom(usuarios);

            return usuariosDTO;
        }

        public async Task<ProyectoDTO> ObtenerProyectoPorIdAsync(ObtenerProyectoPorIdDTO dto)
        {
            var proyecto = await _unidadDeTrabajo.Proyectos
                                                 .Include(p => p.AlertasProyecto)
                                                 .Include(p => p.Cliente)
                                                 .Include(p => p.Gerente)
                                                 .Include(p => p.Director)
                                                 .Include(p => p.Supervisor)
                                                 .Include(p => p.Auditor)
                                                 .OrderBy(t => t.Nombre)
                                                 .FirstOrDefaultAsync(p => p.Id == dto.Id);

            var proyectoDTO = new ProyectoDTO();

            proyectoDTO.InjectFrom<FlatLoopInjection>(proyecto);

            return proyectoDTO;
        }

        public async Task<Paginado<ProyectoDTO>> ObtenerProyectosPorPaginaAsync(ObtenerProyectosPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Proyectos
                                           .Include(t => t.Cliente)
                                           .Include(t => t.Gerente)
                                           .OrderBy(t => t.Nombre)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Nombre.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Proyecto> actividades;

            if (paginaDTO.NumeroPagina == 1)
            {
                actividades = await consulta.Take(paginaDTO.RegistrosPagina)
                                            .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                actividades = await consulta.Skip(skip)
                                            .Take(paginaDTO.RegistrosPagina)
                                            .ToListAsync();
            }

            var proyectosDTO = new List<ProyectoDTO>();

            proyectosDTO.InjectFrom(actividades, new FlatLoopInjection());

            var paginadoDTO = new Paginado<ProyectoDTO>(proyectosDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        public async Task<List<ServicioDTO>> ObtenerServiciosAsync()
        {
            var servicios = await _unidadDeTrabajo.Servicios
                                                  .AsNoTracking()
                                                  .OrderBy(t => t.Nombre)
                                                  .ToListAsync();

            var serviciosDTO = new List<ServicioDTO>();

            serviciosDTO.InjectFrom(servicios);

            return serviciosDTO;
        }

        public async Task<List<ServicioDTO>> ObtenerServiciosPorProyectoAsync(ObtenerServiciosPorProyectoDTO dto)
        {
            var servicios = await _unidadDeTrabajo.Proyectos
                                                  .Where(p => p.Id == dto.Id)
                                                  .SelectMany(p => p.Servicios)
                                                  .AsNoTracking()
                                                  .OrderBy(t => t.Nombre)
                                                  .ToListAsync();

            var serviciosDTO = new List<ServicioDTO>();

            serviciosDTO.InjectFrom(servicios);

            return serviciosDTO;
        }

        public async Task<List<UsuarioDTO>> ObtenerUsuariosAsync()
        {
            var usuarios = await _unidadDeTrabajo.Usuarios
                                                 .AsNoTracking()
                                                 .OrderBy(t => t.Nombre)
                                                 .ToListAsync();

            var usuariosDTO = new List<UsuarioDTO>();

            usuariosDTO.InjectFrom(usuarios);

            return usuariosDTO;
        }

        #endregion
    }
}