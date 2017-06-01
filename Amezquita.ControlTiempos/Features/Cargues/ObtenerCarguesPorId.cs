// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerCarguesPorId.cs" company="SCI Software">
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
    public class ObtenerCarguesPorId
    {
        public class CargueHorasDTO
        {
            #region Instance Properties

            public Guid ActividadId { get; set; }
            public DateTime FechaFin { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaRegistro { get; set; }
            public Guid Id { get; set; }
            public string Observaciones { get; set; }
            public Guid ProyectoId { get; set; }
            public Guid ServicioId { get; set; }

            #endregion
        }

        #region Nested type: Query

        public class Query : IAsyncRequest<CargueHorasDTO>
        {
            #region Instance Properties

            public Guid Id { get; set; }

            #endregion
        }

        #endregion

        #region Nested type: Handler

        public class Handler : IAsyncRequestHandler<Query, CargueHorasDTO>
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

            public async Task<CargueHorasDTO> Handle(Query message)
            {

                var cargueHorasEntity = await _unidadDeTrabajo.CarguesHoras
                    .Include(c => c.Actividad)
                    .Include(c => c.Proyecto)
                    .Include(c => c.Servicio)
                    .FirstOrDefaultAsync(p => p.Id == message.Id);

                return new CargueHorasDTO
                       {
                           FechaRegistro = cargueHorasEntity.FechaRegistro,
                           FechaInicio = cargueHorasEntity.FechaInicio,
                           Id = cargueHorasEntity.Id,
                           FechaFin = cargueHorasEntity.FechaFin,
                           Observaciones = cargueHorasEntity.Observacion,
                           ActividadId = cargueHorasEntity.Actividad.Id,
                           ProyectoId = cargueHorasEntity.Proyecto.Id,
                           ServicioId = cargueHorasEntity.Servicio.Id
                       };
            }

            #endregion
        }

        #endregion
    }
}