namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustesEnProyecto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlertasProyecto", "ProyectoId", "dbo.Proyectos");
            AlterColumn("dbo.Proyectos", "BolsaHoras", c => c.Decimal(nullable: false, precision: 8, scale: 2));
            AlterColumn("dbo.AlertasProyecto", "PorcentajeEjecutado", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddForeignKey("dbo.AlertasProyecto", "ProyectoId", "dbo.Proyectos", "ProyectoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlertasProyecto", "ProyectoId", "dbo.Proyectos");
            AlterColumn("dbo.AlertasProyecto", "PorcentajeEjecutado", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Proyectos", "BolsaHoras", c => c.Decimal(precision: 8, scale: 2));
            AddForeignKey("dbo.AlertasProyecto", "ProyectoId", "dbo.Proyectos", "ProyectoId");
        }
    }
}
