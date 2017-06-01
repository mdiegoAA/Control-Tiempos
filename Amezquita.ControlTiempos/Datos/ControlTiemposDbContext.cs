// ----------------------------------------------------------------------------------------------
// <copyright file="ControlTiemposDbContext.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura.Datos</project>
// ----------------------------------------------------------------------------------------------

using System.Data.Entity;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura.Datos.Configuraciones;
using System.Threading.Tasks;
using System.Data;
using System;

namespace Amezquita.ControlTiempos.Infraestructura.Datos
{
    public class ControlTiemposDbContext : DbContext
    {
        private DbSet<Actividad> _actividades;
        private DbSet<Area> _areas;
        private DbSet<AreaProyecto> _areasProyecto;
        private DbSet<Cargo> _cargos;
        private DbSet<CargueHoras> _carguesHoras;
        private DbSet<Cliente> _clientes;
        private DbSet<DiaCalendario> _diasCalendario;
        private DbSet<Proyecto> _proyectos;
        private DbSet<Rol> _roles;
        private DbSet<Servicio> _servicios;
        private DbSet<Usuario> _usuarios;
        private DbSet<ReglaCargue> _reglasCargue;
        private DbContextTransaction _currentTransaction;

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public async Task CloseTransaction()
        {
            await CloseTransaction(exception: null);
        }

        public async Task CloseTransaction(Exception exception)
        {
            try
            {
                if (_currentTransaction != null && exception != null)
                {
                    _currentTransaction.Rollback();
                    return;
                }

                await SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch (Exception)
            {
                if (_currentTransaction?.UnderlyingTransaction.Connection != null)
                    _currentTransaction.Rollback();

                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public ControlTiemposDbContext() {}

        public DbSet<Actividad> Actividades => _actividades ?? (_actividades = Set<Actividad>());

        public DbSet<Area> Areas => _areas ?? (_areas = Set<Area>());

        public DbSet<AreaProyecto> AreasProyecto => _areasProyecto ?? (_areasProyecto = Set<AreaProyecto>());

        public DbSet<Cargo> Cargos => _cargos ?? (_cargos = Set<Cargo>());

        public DbSet<CargueHoras> CarguesHoras => _carguesHoras ?? (_carguesHoras = Set<CargueHoras>());

        public DbSet<Cliente> Clientes => _clientes ?? (_clientes = Set<Cliente>());

        public DbSet<DiaCalendario> DiasCalendario => _diasCalendario ?? (_diasCalendario = Set<DiaCalendario>());

        public DbSet<Proyecto> Proyectos => _proyectos ?? (_proyectos = Set<Proyecto>());

        public DbSet<Rol> Roles => _roles ?? (_roles = Set<Rol>());

        public DbSet<Servicio> Servicios => _servicios ?? (_servicios = Set<Servicio>());

        public DbSet<Usuario> Usuarios => _usuarios ?? (_usuarios = Set<Usuario>());
        public DbSet<ReglaCargue> ReglasCargue => _reglasCargue ?? (_reglasCargue = Set<ReglaCargue>());

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ConfiguracionArea());
            modelBuilder.Configurations.Add(new ConfiguracionAreaProyecto());
            modelBuilder.Configurations.Add(new ConfiguracionServicio());
            modelBuilder.Configurations.Add(new ConfiguracionActividad());
            modelBuilder.Configurations.Add(new ConfiguracionCargo());
            modelBuilder.Configurations.Add(new ConfiguracionCliente());
            modelBuilder.Configurations.Add(new ConfiguracionProyecto());
            modelBuilder.Configurations.Add(new ConfiguracionUsuario());
            modelBuilder.Configurations.Add(new ConfiguracionReglaCargue());
            modelBuilder.Configurations.Add(new ConfiguracionRol());
            modelBuilder.Configurations.Add(new ConfiguracionCargueHoras());
            modelBuilder.Configurations.Add(new ConfiguracionDiaCalendario());
            modelBuilder.Configurations.Add(new ConfiguracionAlertasProyecto());
        }
    }
}