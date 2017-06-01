// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionCargueHoras.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionCargueHoras : EntityTypeConfiguration<CargueHoras>
    {
        #region C'tors

        public ConfiguracionCargueHoras()
        {
            ToTable("CarguesHoras")
                .HasKey(t => t.Id);

            Property(t => t.Aprobada)
                .HasColumnName("Aprobada");

            Property(t => t.EsNovedad)
                .HasColumnName("EsNovedad");

            Property(t => t.FechaAprobacion)
                .HasColumnName("FechaAprobacion");

            Property(t => t.FechaFin)
                .HasColumnName("FechaFin");

            Property(t => t.FechaInicio)
                .HasColumnName("FechaInicio");

            Property(t => t.FechaRegistro)
                .HasColumnName("FechaRegistro");

            Property(t => t.Id)
                .HasColumnName("CargueHorasId");

            Property(t => t.Observacion)
                .HasColumnName("Observacion")
                .HasMaxLength(512);

            HasRequired(t => t.Actividad)
                .WithMany()
                .Map(fk => fk.MapKey("ActividadId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Proyecto)
                .WithMany()
                .Map(fk => fk.MapKey("ProyectoId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Servicio)
                .WithMany()
                .Map(fk => fk.MapKey("ServicioId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Usuario)
                .WithMany()
                .Map(fk => fk.MapKey("UsuarioId"))
                .WillCascadeOnDelete(false);

            HasRequired(t => t.RegistradaPor)
                .WithMany()
                .Map(fk => fk.MapKey("RegistradaPorId"))
                .WillCascadeOnDelete(false);

            HasOptional(t => t.AprobadaPor)
                .WithMany()
                .Map(fk => fk.MapKey("AprobadaPorId"))
                .WillCascadeOnDelete(false);

            Ignore(t => t.HoraFraccion);
        }

        #endregion
    }
}