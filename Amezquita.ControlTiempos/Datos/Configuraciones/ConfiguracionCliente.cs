// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionCliente.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionCliente : EntityTypeConfiguration<Cliente>
    {
        #region C'tors

        public ConfiguracionCliente()
        {
            ToTable("Clientes")
                .HasKey(t => t.Id);

            Property(t => t.Codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(8);

            Property(t => t.Id)
                .HasColumnName("ClienteId");

            Property(t => t.NIT)
                .HasColumnName("NIT")
                .HasMaxLength(16);

            Property(t => t.Nombre)
                .HasColumnName("NombreCliente")
                .HasMaxLength(128);
        }

        #endregion
    }
}