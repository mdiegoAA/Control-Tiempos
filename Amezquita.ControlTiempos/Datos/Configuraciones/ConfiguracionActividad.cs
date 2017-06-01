// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionActividad.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionActividad : EntityTypeConfiguration<Actividad>
    {
        #region C'tors

        public ConfiguracionActividad()
        {
            ToTable("Actividades")
                .HasKey(t => t.Id);

            Property(t => t.Codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(8);

            Property(t => t.Id)
                .HasColumnName("ActividadId");

            Property(t => t.EsGenerica)
                .HasColumnName("EsGenerica");

            Property(t => t.NecesitaAprobacion)
                .HasColumnName("NecesitaAprobacion");

            Property(t => t.Nombre)
                .HasColumnName("NombreActividad")
                .HasMaxLength(128);
        }

        #endregion
    }
}