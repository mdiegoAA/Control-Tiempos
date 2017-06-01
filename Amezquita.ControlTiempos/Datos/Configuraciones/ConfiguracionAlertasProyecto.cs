// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionAlertaProyecto.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionAlertasProyecto : EntityTypeConfiguration<AlertasProyecto>
    {
        #region C'tors

        public ConfiguracionAlertasProyecto()
        {
            ToTable("AlertasProyecto")
                .HasKey(t => t.Id);

            Property(t => t.DiasSinRegistrar)
                .HasColumnName("DiasSinRegistrar");

            Property(t => t.Id)
                .HasColumnName("ProyectoId");

            Property(t => t.PorcentajeEjecutado)
                .HasColumnName("PorcentajeEjecutado")
                .HasPrecision(4, 2);
        }

        #endregion
    }
}