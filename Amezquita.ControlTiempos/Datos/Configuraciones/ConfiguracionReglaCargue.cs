// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionReglaCargue.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionReglaCargue : EntityTypeConfiguration<ReglaCargue>
    {
        #region C'tors

        public ConfiguracionReglaCargue()
        {
            ToTable("ReglasCargue")
                .HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("UsuarioId");

            Property(t => t.LimiteDias)
                .HasColumnName("LimiteDias");

            Property(t => t.LimiteHoraFraccion)
                .HasColumnName("LimiteHoraFraccion")
                .HasPrecision(4, 2);
        }

        #endregion
    }
}