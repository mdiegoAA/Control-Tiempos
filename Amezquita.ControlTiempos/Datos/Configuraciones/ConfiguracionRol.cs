// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionRol.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionRol : EntityTypeConfiguration<Rol>
    {
        #region C'tors

        public ConfiguracionRol()
        {
            ToTable("Roles")
                .HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("RolId");

            Property(t => t.Name)
                .HasColumnName("NombreRol");
        }

        #endregion
    }
}