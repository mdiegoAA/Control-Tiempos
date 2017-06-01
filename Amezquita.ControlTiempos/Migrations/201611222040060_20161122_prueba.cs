namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161122_prueba : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Cedula", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Cedula");
        }
    }
}
