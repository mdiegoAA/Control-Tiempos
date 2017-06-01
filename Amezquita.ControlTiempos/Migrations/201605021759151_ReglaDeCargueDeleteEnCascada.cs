namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReglaDeCargueDeleteEnCascada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReglasCargue", "UsuarioId", "dbo.Usuarios");
            AddForeignKey("dbo.ReglasCargue", "UsuarioId", "dbo.Usuarios", "UsuarioId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReglasCargue", "UsuarioId", "dbo.Usuarios");
            AddForeignKey("dbo.ReglasCargue", "UsuarioId", "dbo.Usuarios", "UsuarioId");
        }
    }
}
