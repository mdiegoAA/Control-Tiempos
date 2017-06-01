using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using Omu.ValueInjecter.Injections;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MvcSiteMapProvider.Linq;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class ObtenerCarguesPorUsuario
    {
        public class CargueHorasDTO
        {
            public string ActividadNombre { get; set; }
            public string UsuarioNombre { get; set; }
            public bool Aprobada { get; set; }
            public bool EsNovedad { get; set; }
            public DateTime FechaFin { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaRegistro { get; set; }
            public Guid Id { get; set; }
            public string Observacion { get; set; }
            public string ProyectoNombre { get; set; }
            public string ServicioNombre { get; set; }
        }

        public class Query : IAsyncRequest<Paginado<CargueHorasDTO>>
        {
            public string Buscar { get; set; }
            public int NumeroPagina { get; set; }
            public int RegistrosPagina { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid UsuarioId { get; set; }
            public bool EsApp { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, Paginado<CargueHorasDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<Paginado<CargueHorasDTO>> Handle(Query message)
            {
                var consulta = _unidadDeTrabajo.CarguesHoras
                                               .Include(ch => ch.Proyecto)
                                               .Include(ch => ch.Servicio)
                                               .Include(ch => ch.Actividad)
                                               .Include(ch => ch.Usuario)
                                               .Where(t => t.RegistradaPor.Id == message.UsuarioId)
                                               .OrderByDescending(t => t.FechaInicio)
                                               .AsNoTracking()
                                               .AsQueryable();

                var total = await consulta.CountAsync();

                if (message.EsApp)
                {
                    return new Paginado<CargueHorasDTO>(new List<CargueHorasDTO>().InjectFrom(consulta, new FlatLoopInjection()),
                                                    message.NumeroPagina,
                                                    message.RegistrosPagina, total);
                }

                if (!string.IsNullOrEmpty(message.Buscar))
                    consulta = consulta.Where(t => t.Proyecto.Nombre.Contains(message.Buscar));

                

                List<CargueHoras> carguesHoras;

                if (message.NumeroPagina == 1)
                    carguesHoras = await consulta.Take(message.RegistrosPagina)
                                                 .ToListAsync();
                else
                    carguesHoras = await consulta.Skip((message.NumeroPagina - 1) * (message.RegistrosPagina))
                                                 .Take(message.RegistrosPagina)
                                                 .ToListAsync();

                return new Paginado<CargueHorasDTO>(new List<CargueHorasDTO>().InjectFrom(carguesHoras, new FlatLoopInjection()),
                                                    message.NumeroPagina,
                                                    message.RegistrosPagina, total);
            }
        }
    }
}