// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionAreaProyecto.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionAreaProyecto : EntityTypeConfiguration<AreaProyecto>
    {
        #region C'tors

        public ConfiguracionAreaProyecto()
        {
            ToTable("AreasProyecto")
                .HasKey(t => t.Id);

            Property(t => t.Horas)
                .HasColumnName("Horas");

            Property(t => t.AreaId)
                .HasColumnName("AreaId");

            Property(t => t.ProyectoId)
                .HasColumnName("ProyectoId");

            Property(t => t.Id)
                .HasColumnName("AreaProyectoId");

            HasRequired(t => t.Area)
                .WithMany(t => t.AreasProyecto)
                .HasForeignKey(t => t.AreaId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Proyecto)
                .WithMany(t => t.AreasProyecto)
                .HasForeignKey(t => t.ProyectoId)
                .WillCascadeOnDelete(false);
        }

        #endregion
    }
}