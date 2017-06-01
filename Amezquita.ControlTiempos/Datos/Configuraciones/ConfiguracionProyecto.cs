// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionProyecto.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionProyecto : EntityTypeConfiguration<Proyecto>
    {
        #region C'tors

        public ConfiguracionProyecto()
        {
            ToTable("Proyectos")
                .HasKey(t => t.Id);

            Property(t => t.Año)
                .HasColumnName("Año");

            Property(t => t.BolsaHoras)
                .HasColumnName("BolsaHoras")
                .HasPrecision(8, 2);

            Property(t => t.FechaFinal)
                .HasColumnName("FechaFinal");

            Property(t => t.FechaInicio)
                .HasColumnName("FechaInicio");

            Property(t => t.Id)
                .HasColumnName("ProyectoId");

            Property(t => t.Nombre)
                .HasColumnName("NombreProyecto")
                .HasMaxLength(128);

            HasOptional(t => t.Auditor)
                .WithMany()
                .Map(fk => fk.MapKey("AuditorId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Cliente)
                .WithMany()
                .Map(fk => fk.MapKey("ClienteId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Director)
                .WithMany()
                .Map(fk => fk.MapKey("DirectorId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Gerente)
                .WithMany()
                .Map(fk => fk.MapKey("GerenteId"))
                .WillCascadeOnDelete(false);

            HasOptional(t => t.Supervisor)
                .WithMany()
                .Map(fk => fk.MapKey("SupervisorId"))
                .WillCascadeOnDelete(false);

            HasMany(t => t.Actividades)
                .WithMany()
                .Map(fk =>
                     {
                         fk.ToTable("ActividadesProyecto");
                         fk.MapLeftKey("ProyectoId");
                         fk.MapRightKey("ActividadId");
                     });

            HasMany(t => t.GrupoTrabajo)
                .WithMany()
                .Map(fk =>
                     {
                         fk.ToTable("GruposTrabajo");
                         fk.MapLeftKey("ProyectoId");
                         fk.MapRightKey("UsuarioId");
                     });

            HasMany(t => t.Servicios)
                .WithMany()
                .Map(fk =>
                     {
                         fk.ToTable("ServiciosProyecto");
                         fk.MapLeftKey("ProyectoId");
                         fk.MapRightKey("ServicioId");
                     });

            HasOptional(t => t.AlertasProyecto)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }

        #endregion
    }
}