namespace Amezquita.ControlTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actividades",
                c => new
                    {
                        ActividadId = c.Guid(nullable: false),
                        Codigo = c.String(maxLength: 8),
                        NombreActividad = c.String(maxLength: 128),
                        EsGenerica = c.Boolean(nullable: false),
                        NecesitaAprobacion = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ActividadId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Guid(nullable: false),
                        Codigo = c.String(maxLength: 8),
                        NombreArea = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AreaId);
            
            CreateTable(
                "dbo.AreasProyecto",
                c => new
                    {
                        AreaProyectoId = c.Guid(nullable: false),
                        ProyectoId = c.Guid(nullable: false),
                        AreaId = c.Guid(nullable: false),
                        Horas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaProyectoId)
                .ForeignKey("dbo.Areas", t => t.AreaId)
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId)
                .Index(t => t.ProyectoId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Proyectos",
                c => new
                    {
                        ProyectoId = c.Guid(nullable: false),
                        NombreProyecto = c.String(maxLength: 128),
                        Año = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        BolsaHoras = c.Decimal(precision: 8, scale: 2),
                        AuditorId = c.Guid(),
                        ClienteId = c.Guid(nullable: false),
                        DirectorId = c.Guid(nullable: false),
                        GerenteId = c.Guid(nullable: false),
                        SupervisorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProyectoId)
                .ForeignKey("dbo.Usuarios", t => t.AuditorId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .ForeignKey("dbo.Usuarios", t => t.DirectorId)
                .ForeignKey("dbo.Usuarios", t => t.GerenteId)
                .ForeignKey("dbo.Usuarios", t => t.SupervisorId)
                .Index(t => t.AuditorId)
                .Index(t => t.ClienteId)
                .Index(t => t.DirectorId)
                .Index(t => t.GerenteId)
                .Index(t => t.SupervisorId);
            
            CreateTable(
                "dbo.AlertasProyecto",
                c => new
                    {
                        ProyectoId = c.Guid(nullable: false),
                        DiasSinRegistrar = c.Int(nullable: false),
                        PorcentajeEjecutado = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProyectoId)
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId)
                .Index(t => t.ProyectoId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        NombreUsuario = c.String(maxLength: 64),
                        Nombre = c.String(maxLength: 256),
                        Email = c.String(maxLength: 256),
                        EmailConfirmado = c.Boolean(nullable: false),
                        AccesosFallidos = c.Int(nullable: false),
                        Bloqueado = c.Boolean(nullable: false),
                        FechaBloqueo = c.DateTime(),
                        CargoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Cargos", t => t.CargoId)
                .Index(t => t.CargoId);
            
            CreateTable(
                "dbo.Cargos",
                c => new
                    {
                        CargoId = c.Guid(nullable: false),
                        Codigo = c.String(maxLength: 8),
                        NombreCargo = c.String(maxLength: 128),
                        Tarifa = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CargoId);
            
            CreateTable(
                "dbo.ReglasCargue",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        LimiteDias = c.Int(nullable: false),
                        LimiteHoraFraccion = c.Decimal(nullable: false, precision: 4, scale: 2),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RolId = c.Guid(nullable: false),
                        NombreRol = c.String(),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Guid(nullable: false),
                        Codigo = c.String(maxLength: 8),
                        NIT = c.String(maxLength: 16),
                        NombreCliente = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Servicios",
                c => new
                    {
                        ServicioId = c.Guid(nullable: false),
                        Codigo = c.String(maxLength: 8),
                        EsGenerico = c.Boolean(nullable: false),
                        NombreServicio = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ServicioId);
            
            CreateTable(
                "dbo.CarguesHoras",
                c => new
                    {
                        CargueHorasId = c.Guid(nullable: false),
                        Observacion = c.String(maxLength: 512),
                        FechaFin = c.DateTime(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        Aprobada = c.Boolean(nullable: false),
                        FechaAprobacion = c.DateTime(),
                        ActividadId = c.Guid(nullable: false),
                        AprobadaPorId = c.Guid(),
                        ProyectoId = c.Guid(nullable: false),
                        ServicioId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CargueHorasId)
                .ForeignKey("dbo.Actividades", t => t.ActividadId)
                .ForeignKey("dbo.Usuarios", t => t.AprobadaPorId)
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId)
                .ForeignKey("dbo.Servicios", t => t.ServicioId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.ActividadId)
                .Index(t => t.AprobadaPorId)
                .Index(t => t.ProyectoId)
                .Index(t => t.ServicioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.DiasCalendario",
                c => new
                    {
                        DiaCalendarioId = c.Guid(nullable: false),
                        Dia = c.DateTime(nullable: false),
                        EsFestivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DiaCalendarioId);
            
            CreateTable(
                "dbo.ActividadesProyecto",
                c => new
                    {
                        ProyectoId = c.Guid(nullable: false),
                        ActividadId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProyectoId, t.ActividadId })
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId, cascadeDelete: true)
                .ForeignKey("dbo.Actividades", t => t.ActividadId, cascadeDelete: true)
                .Index(t => t.ProyectoId)
                .Index(t => t.ActividadId);
            
            CreateTable(
                "dbo.RolesUsuario",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        RolId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.RolId })
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RolId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.GruposTrabajo",
                c => new
                    {
                        ProyectoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProyectoId, t.UsuarioId })
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.ProyectoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.ServiciosProyecto",
                c => new
                    {
                        ProyectoId = c.Guid(nullable: false),
                        ServicioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProyectoId, t.ServicioId })
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId, cascadeDelete: true)
                .ForeignKey("dbo.Servicios", t => t.ServicioId, cascadeDelete: true)
                .Index(t => t.ProyectoId)
                .Index(t => t.ServicioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarguesHoras", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.CarguesHoras", "ServicioId", "dbo.Servicios");
            DropForeignKey("dbo.CarguesHoras", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.CarguesHoras", "AprobadaPorId", "dbo.Usuarios");
            DropForeignKey("dbo.CarguesHoras", "ActividadId", "dbo.Actividades");
            DropForeignKey("dbo.AreasProyecto", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.Proyectos", "SupervisorId", "dbo.Usuarios");
            DropForeignKey("dbo.ServiciosProyecto", "ServicioId", "dbo.Servicios");
            DropForeignKey("dbo.ServiciosProyecto", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.GruposTrabajo", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.GruposTrabajo", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.Proyectos", "GerenteId", "dbo.Usuarios");
            DropForeignKey("dbo.Proyectos", "DirectorId", "dbo.Usuarios");
            DropForeignKey("dbo.Proyectos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Proyectos", "AuditorId", "dbo.Usuarios");
            DropForeignKey("dbo.RolesUsuario", "RolId", "dbo.Roles");
            DropForeignKey("dbo.RolesUsuario", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ReglasCargue", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "CargoId", "dbo.Cargos");
            DropForeignKey("dbo.AlertasProyecto", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.ActividadesProyecto", "ActividadId", "dbo.Actividades");
            DropForeignKey("dbo.ActividadesProyecto", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.AreasProyecto", "AreaId", "dbo.Areas");
            DropIndex("dbo.ServiciosProyecto", new[] { "ServicioId" });
            DropIndex("dbo.ServiciosProyecto", new[] { "ProyectoId" });
            DropIndex("dbo.GruposTrabajo", new[] { "UsuarioId" });
            DropIndex("dbo.GruposTrabajo", new[] { "ProyectoId" });
            DropIndex("dbo.RolesUsuario", new[] { "RolId" });
            DropIndex("dbo.RolesUsuario", new[] { "UsuarioId" });
            DropIndex("dbo.ActividadesProyecto", new[] { "ActividadId" });
            DropIndex("dbo.ActividadesProyecto", new[] { "ProyectoId" });
            DropIndex("dbo.CarguesHoras", new[] { "UsuarioId" });
            DropIndex("dbo.CarguesHoras", new[] { "ServicioId" });
            DropIndex("dbo.CarguesHoras", new[] { "ProyectoId" });
            DropIndex("dbo.CarguesHoras", new[] { "AprobadaPorId" });
            DropIndex("dbo.CarguesHoras", new[] { "ActividadId" });
            DropIndex("dbo.ReglasCargue", new[] { "UsuarioId" });
            DropIndex("dbo.Usuarios", new[] { "CargoId" });
            DropIndex("dbo.AlertasProyecto", new[] { "ProyectoId" });
            DropIndex("dbo.Proyectos", new[] { "SupervisorId" });
            DropIndex("dbo.Proyectos", new[] { "GerenteId" });
            DropIndex("dbo.Proyectos", new[] { "DirectorId" });
            DropIndex("dbo.Proyectos", new[] { "ClienteId" });
            DropIndex("dbo.Proyectos", new[] { "AuditorId" });
            DropIndex("dbo.AreasProyecto", new[] { "AreaId" });
            DropIndex("dbo.AreasProyecto", new[] { "ProyectoId" });
            DropTable("dbo.ServiciosProyecto");
            DropTable("dbo.GruposTrabajo");
            DropTable("dbo.RolesUsuario");
            DropTable("dbo.ActividadesProyecto");
            DropTable("dbo.DiasCalendario");
            DropTable("dbo.CarguesHoras");
            DropTable("dbo.Servicios");
            DropTable("dbo.Clientes");
            DropTable("dbo.Roles");
            DropTable("dbo.ReglasCargue");
            DropTable("dbo.Cargos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.AlertasProyecto");
            DropTable("dbo.Proyectos");
            DropTable("dbo.AreasProyecto");
            DropTable("dbo.Areas");
            DropTable("dbo.Actividades");
        }
    }
}
