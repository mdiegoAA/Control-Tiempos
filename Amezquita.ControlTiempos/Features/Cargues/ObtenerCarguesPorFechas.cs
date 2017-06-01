// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerCarguesPorFechas.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using Omu.ValueInjecter.Injections;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class ObtenerCarguesPorFechas
    {
        #region Nested type: CargueHorasDTO

        public class CargueHorasDTO
        {
            #region Instance Properties

            public string ActividadNombre { get; set; }
            public bool Aprobada { get; set; }
            public bool EsNovedad { get; set; }
            public DateTime FechaFin { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaRegistro { get; set; }
            public Guid Id { get; set; }
            public string Observaciones { get; set; }
            public string ProyectoNombre { get; set; }
            public string ServicioNombre { get; set; }
            public string UsuarioNombre { get; set; }

            #endregion
        }

        #endregion

        #region Nested type: Handler

        public class Handler : IAsyncRequestHandler<Query, Paginado<CargueHorasDTO>>
        {
            #region Readonly & Static Fields

            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            #endregion

            #region C'tors

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            #endregion

            #region Instance Methods

            public async Task<Paginado<CargueHorasDTO>> Handle(Query message)
            {
                var fechaSistema = FechaSistema.Actual();
                var usuario = await _unidadDeTrabajo.ReglasCargue
                                                    .Where(r => r.Id == message.UsuarioId)
                                                    .FirstOrDefaultAsync();
                var fecha = fechaSistema.AddDays(-usuario.LimiteDias);

                message.FechaFin = message.FechaFin.AddHours(23).AddMinutes(59);

                var consulta = _unidadDeTrabajo.CarguesHoras
                                               .Include(ch => ch.Proyecto)
                                               .Include(ch => ch.Servicio)
                                               .Include(ch => ch.Actividad)
                                               .Include(ch => ch.Usuario)
                                               .Where(t => t.RegistradaPor.Id == message.UsuarioId)
                                               .Where(t => t.FechaRegistro >= fecha )
                                               .Where(t => t.FechaInicio >= message.FechaInicio && t.FechaInicio <= message.FechaFin)
                                               .OrderByDescending(t => t.FechaRegistro)
                                               .AsNoTracking()
                                               .AsQueryable();

                var total = await consulta.CountAsync();

                if (message.EsApp)
                {
                    return new Paginado<CargueHorasDTO>(new List<CargueHorasDTO>().InjectFrom(consulta, new FlatLoopInjection()),
                        message.NumeroPagina,
                        message.RegistrosPagina, total);
                }

                List<CargueHoras> carguesHoras;

                if (message.NumeroPagina == 1)
                {
                    carguesHoras = await consulta.Take(message.RegistrosPagina)
                                                 .ToListAsync();
                }
                else
                {
                    carguesHoras = await consulta.Skip((message.NumeroPagina - 1) * (message.RegistrosPagina))
                                                 .Take(message.RegistrosPagina)
                                                 .ToListAsync();
                }

                return new Paginado<CargueHorasDTO>(new List<CargueHorasDTO>().InjectFrom(carguesHoras, new FlatLoopInjection()),
                    message.NumeroPagina,
                    message.RegistrosPagina, total);
            }

            #endregion
        }

        #endregion

        #region Nested type: Query

        public class Query : IAsyncRequest<Paginado<CargueHorasDTO>>
        {
            #region Instance Properties

            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public bool EsApp { get; set; }
            public int NumeroPagina { get; set; }
            public int RegistrosPagina { get; set; }

            [PropertyModelBinder(typeof (UserIdModelBinder))]
            public Guid UsuarioId { get; set; }

            #endregion
        }

        #endregion
    }
}
