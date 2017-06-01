using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Actividades
{
    public class ObtenerActividadesPorPagina
    {
        public class Query : IAsyncRequest<Paginado<ActividadDTO>>
        {
            public string Buscar { get; set; }
            public int NumeroPagina { get; set; }
            public int RegistrosPagina { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, Paginado<ActividadDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<Paginado<ActividadDTO>> Handle(Query message)
            {
                var consulta = _unidadDeTrabajo.Actividades
                                               .OrderBy(t => t.Nombre)
                                               .AsNoTracking()
                                               .AsQueryable();

                if (!string.IsNullOrEmpty(message.Buscar))
                    consulta = consulta.Where(t => t.Nombre.Contains(message.Buscar));

                var total = await consulta.CountAsync();

                List<Actividad> actividades;

                if (message.NumeroPagina == 1)
                    actividades = await consulta.Take(message.RegistrosPagina)
                                                .ToListAsync();
                else
                    actividades = await consulta.Skip((message.NumeroPagina - 1) * (message.RegistrosPagina))
                                                .Take(message.RegistrosPagina)
                                                .ToListAsync();

                return new Paginado<ActividadDTO>(new List<ActividadDTO>().InjectFrom(actividades),
                                                  message.NumeroPagina,
                                                  message.RegistrosPagina, total);
            }
        }
    }
}