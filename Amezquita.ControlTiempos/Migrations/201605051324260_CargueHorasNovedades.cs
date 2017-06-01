namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CargueHorasNovedades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarguesHoras", "EsNovedad", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarguesHoras", "EsNovedad");
        }
    }
}
