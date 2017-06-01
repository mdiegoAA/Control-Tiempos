// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionArea.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionArea : EntityTypeConfiguration<Area>
    {
        #region C'tors

        public ConfiguracionArea()
        {
            ToTable("Areas")
                .HasKey(t => t.Id);

            Property(t => t.Codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(8);

            Property(t => t.Id)
                .HasColumnName("AreaId");

            Property(t => t.Nombre)
                .HasColumnName("NombreArea")
                .HasMaxLength(128);
        }

        #endregion
    }
}