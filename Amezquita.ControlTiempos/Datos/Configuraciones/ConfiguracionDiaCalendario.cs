// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionDiaCalendario.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionDiaCalendario : EntityTypeConfiguration<DiaCalendario>
    {
        #region C'tors

        public ConfiguracionDiaCalendario()
        {
            ToTable("DiasCalendario")
                .HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("DiaCalendarioId");

            Property(t => t.Dia)
                .HasColumnName("Dia");

            Property(t => t.EsFestivo)
                .HasColumnName("EsFestivo");
        }

        #endregion
    }
}