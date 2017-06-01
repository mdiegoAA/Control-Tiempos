// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionCargo.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionCargo : EntityTypeConfiguration<Cargo>
    {
        #region C'tors

        public ConfiguracionCargo()
        {
            ToTable("Cargos")
                .HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("CargoId");

            Property(t => t.Codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(8);

            Property(t => t.Nombre)
                .HasColumnName("NombreCargo")
                .HasMaxLength(128);

            Property(t => t.Tarifa)
                .HasColumnName("Tarifa");
        }

        #endregion
    }
}