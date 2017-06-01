namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarPrecisionEnAlertasProyecto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AlertasProyecto", "PorcentajeEjecutado", c => c.Decimal(nullable: false, precision: 4, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlertasProyecto", "PorcentajeEjecutado", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
