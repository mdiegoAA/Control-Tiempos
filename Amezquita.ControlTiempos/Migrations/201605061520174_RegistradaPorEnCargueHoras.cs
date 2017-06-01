namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistradaPorEnCargueHoras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarguesHoras", "RegistradaPorId", c => c.Guid(nullable: true));
            AddForeignKey("dbo.CarguesHoras", "RegistradaPorId", "dbo.Usuarios", "UsuarioId");
            Sql("UPDATE dbo.CarguesHoras SET RegistradaPorId = UsuarioId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.CarguesHoras", "RegistradaPorId", "dbo.Usuarios");
            DropColumn("dbo.CarguesHoras", "RegistradaPorId");
        }
    }
}