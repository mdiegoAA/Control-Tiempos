namespace Amezquita.ControlTiempos.Migrations
{
    using Dominio;
    using Infraestructura.Datos;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ControlTiemposDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ControlTiemposDbContext context)
        {
            var cargoAdministrador = new Cargo(new Guid("D744C934-BF3D-4103-9885-A7D2FFC1AED7"), "01", "ADMINISTRADOR", 0);
            context.Cargos.AddOrUpdate(cargoAdministrador);

            var rolAdministrador = new Rol(new Guid("A8292C53-F75A-11E5-80BC-00155D050500"), "Administrador");
            context.Roles.AddOrUpdate(rolAdministrador);

            var rolRH = new Rol(new Guid("A8292C53-F75A-11E5-80BC-00155D050501"), "Recursos Humanos");
            context.Roles.AddOrUpdate(rolAdministrador);

            var rolOperativo = new Rol(new Guid("A8292C53-F75A-11E5-80BC-00155D050502"), "Operativo");
            context.Roles.AddOrUpdate(rolOperativo);

            var rolDirector = new Rol(new Guid("A8292C53-F75A-11E5-80BC-00155D050503"), "Director");
            context.Roles.AddOrUpdate(rolDirector);

            context.SaveChanges();

            var sci = new Usuario(new Guid("8C40EF35-694A-4C6D-BA78-D7178217C687"), cargoAdministrador, "sci", "JHONNYS LÓPEZ", "jhonnys.lopez@softwaresci.com", 15, 24,"121210");
            sci.Roles.Add(rolAdministrador);
            context.Usuarios.AddOrUpdate(sci);

            var vcarlos = new Usuario(new Guid("8C40EF35-694A-4C6D-BA78-D7178217C688"), cargoAdministrador, "vcarlos", "CARLOS VARGAS", "vcarlos@amezquita.com.co", 15, 24,"4545454");
            vcarlos.Roles.Add(rolAdministrador);
            context.Usuarios.AddOrUpdate(vcarlos);

            var gedinson = new Usuario(new Guid("8C40EF35-694A-4C6D-BA78-D7178217C689"), cargoAdministrador, "gedison", "EDISON GIRÓN", "gedison@amezquita.com.co", 15, 24,"545454");
            gedinson.Roles.Add(rolAdministrador);
            sci.Roles.Add(rolRH);
            context.Usuarios.AddOrUpdate(gedinson);
        }
    }
}