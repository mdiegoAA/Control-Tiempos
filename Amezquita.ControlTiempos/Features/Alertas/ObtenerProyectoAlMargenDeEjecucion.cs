using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Amezquita.ControlTiempos.Features.Alertas
{
    public class ObtenerProyectoAlMargenDeEjecucion
    {
        public class ProyectoAlMargeDeEjecucionDTO
        {
            public string Proyecto { get; set; }
            public string Email { get; set; }
            public decimal BolsaHoras { get; set; }
            public decimal PorcentajeEjecutado { get; set; }
            public int? TotalHoras { get; set; }
        }

        public class Query : IRequest<IEnumerable<ProyectoAlMargeDeEjecucionDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ProyectoAlMargeDeEjecucionDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public IEnumerable<ProyectoAlMargeDeEjecucionDTO> Handle(Query message)
            {
                var proyectos = _unidadDeTrabajo.CarguesHoras
                                                .AsNoTracking()
                                                .Select(ch => new
                                                {
                                                    Id = ch.Proyecto.Id,
                                                    Proyecto = ch.Proyecto.Nombre,
                                                    BolsaHoras = ch.Proyecto.BolsaHoras,
                                                    Email = ch.Proyecto.Gerente.Email,
                                                    PorcentajeEjecutado = ch.Proyecto.AlertasProyecto.PorcentajeEjecutado,
                                                    FechaInicio = ch.FechaInicio,
                                                    FechaFin = ch.FechaFin
                                                })
                                                .GroupBy(ch => new
                                                {
                                                    Id = ch.Id,
                                                    Proyecto = ch.Proyecto,
                                                    Email = ch.Email,
                                                    BolsaHoras = ch.BolsaHoras,
                                                    PorcentajeEjecutado = ch.PorcentajeEjecutado
                                                })
                                                .Select(g => new
                                                {
                                                    Proyecto = g.Key.Proyecto,
                                                    Email = g.Key.Email,
                                                    BolsaHoras = g.Key.BolsaHoras,
                                                    PorcentajeEjecutado = g.Key.PorcentajeEjecutado,
                                                    TotalHoras = g.Sum(ch => DbFunctions.DiffHours(ch.FechaInicio, ch.FechaFin)),
                                                })
                                                .Where(r => (r.TotalHoras / r.BolsaHoras) * 100.0M >= r.PorcentajeEjecutado)
                                                .ToList();

                return new List<ProyectoAlMargeDeEjecucionDTO>().InjectFrom(proyectos);
            }
        }
    }
}
