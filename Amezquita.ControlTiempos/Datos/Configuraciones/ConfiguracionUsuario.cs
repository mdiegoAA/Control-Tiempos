// ----------------------------------------------------------------------------------------------
// <copyright file="ConfiguracionUsuario.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Amezquita.ControlTiempos.Dominio;

namespace Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones
{
    public class ConfiguracionUsuario : EntityTypeConfiguration<Usuario>
    {
        #region C'tors

        public ConfiguracionUsuario()
        {
            ToTable("Usuarios")
                .HasKey(t => t.Id);

            Property(t => t.AccesosFallidos)
                .HasColumnName("AccesosFallidos");

            Property(t => t.Bloqueado)
                .HasColumnName("Bloqueado");

            Property(t => t.Email)
                .HasColumnName("Email")
                .HasMaxLength(256);

            Property(t => t.EmailConfirmado)
                .HasColumnName("EmailConfirmado");

            Property(t => t.FechaBloqueo)
                .HasColumnName("FechaBloqueo");

            Property(t => t.UserName)
                .HasColumnName("NombreUsuario")
                .HasMaxLength(64);

            Property(t => t.Id)
                .HasColumnName("UsuarioId");

            Property(t => t.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(256);

            HasRequired(t => t.Cargo)
                .WithMany()
                .Map(fk => fk.MapKey("CargoId"))
                .WillCascadeOnDelete(false);

            HasMany(t => t.Roles)
                .WithMany()
                .Map(fk =>
                     {
                         fk.MapLeftKey("UsuarioId");
                         fk.MapRightKey("RolId");
                         fk.ToTable("RolesUsuario");
                     });

            HasOptional(t => t.ReglaCargue)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }

        #endregion
    }
}