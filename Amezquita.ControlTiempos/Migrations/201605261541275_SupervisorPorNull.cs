namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201605261040160_SupervisorPorNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Proyectos", new[] { "SupervisorId" });
            AlterColumn("dbo.Proyectos", "SupervisorId", c => c.Guid());
            CreateIndex("dbo.Proyectos", "SupervisorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Proyectos", new[] { "SupervisorId" });
            AlterColumn("dbo.Proyectos", "SupervisorId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Proyectos", "SupervisorId");
        }
    }
}
