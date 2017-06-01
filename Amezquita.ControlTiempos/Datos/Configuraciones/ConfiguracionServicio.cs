// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionServicio.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionServicio : EntityTypeConfiguration<Servicio>
    {
        #region C'tors

        public ConfiguracionServicio()
        {
            ToTable("Servicios")
                .HasKey(t => t.Id);

            Property(t => t.Codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(8);

            Property(t => t.Id)
                .HasColumnName("ServicioId");

            Property(t => t.Nombre)
                .HasColumnName("NombreServicio")
                .HasMaxLength(128);
        }

        #endregion
    }
}