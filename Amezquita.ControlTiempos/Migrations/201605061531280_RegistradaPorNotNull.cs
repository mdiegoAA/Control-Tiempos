namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistradaPorNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarguesHoras", "RegistradaPorId", c => c.Guid(nullable: false));
            CreateIndex("dbo.CarguesHoras", "RegistradaPorId");
        }

        public override void Down()
        {
            DropIndex("dbo.CarguesHoras", new[] { "RegistradaPorId" });
            AlterColumn("dbo.CarguesHoras", "RegistradaPorId", c => c.Guid(nullable: true));
        }
    }
}
