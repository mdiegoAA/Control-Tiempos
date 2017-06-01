// ----------------------------------------------------------------------------------------------
// <copyright file="AlmacenUsuarios.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura.Datos;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class AlmacenUsuarios : IUserStore<Usuario, Guid>, IUserLockoutStore<Usuario, Guid>, IUserPasswordStore<Usuario, Guid>, IUserTwoFactorStore<Usuario, Guid>, IUserRoleStore<Usuario, Guid>
    {
        private readonly ControlTiemposDbContext _dbContext;

        public AlmacenUsuarios(ControlTiemposDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToRoleAsync(Usuario user, string roleName)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (role == null)
                throw new ArgumentException("roleName");

            user.Roles.Add(role);

            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Usuario user)
        {
            _dbContext.Usuarios.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario user)
        {
            _dbContext.Usuarios.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Usuario> FindByIdAsync(Guid userId)
        {
            return await _dbContext.Usuarios
                                   .Include(u => u.Roles)
                                   .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Usuario> FindByNameAsync(string userName)
        {
            return await _dbContext.Usuarios
                                   .Include(u => u.Roles)
                                   .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<int> GetAccessFailedCountAsync(Usuario user)
        {
            return Task.FromResult(user.AccesosFallidos);
        }

        public Task<bool> GetLockoutEnabledAsync(Usuario user)
        {
            return Task.FromResult(user.Bloqueado);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(Usuario user)
        {
            DateTimeOffset dateTimeOffset;

            if (user.FechaBloqueo.HasValue)
            {
                var lockoutEndDateUtc = user.FechaBloqueo;
                dateTimeOffset = new DateTimeOffset(DateTime.SpecifyKind(lockoutEndDateUtc.Value, DateTimeKind.Utc));
            }
            else
            {
                dateTimeOffset = new DateTimeOffset();
            }

            return Task.FromResult(dateTimeOffset);
        }

        public Task<string> GetPasswordHashAsync(Usuario user)
        {
            return Task.FromResult(string.Empty);
        }

        public Task<IList<string>> GetRolesAsync(Usuario user)
        {
            return Task.FromResult<IList<string>>(user.Roles.Select(r => r.Name).ToList());
        }

        public Task<bool> GetTwoFactorEnabledAsync(Usuario user)
        {
            return Task.FromResult(false);
        }

        public Task<bool> HasPasswordAsync(Usuario user)
        {
            return Task.FromResult(false);
        }

        public Task<int> IncrementAccessFailedCountAsync(Usuario user)
        {
            user.AccesosFallidos++;

            return Task.FromResult(user.AccesosFallidos);
        }

        public Task<bool> IsInRoleAsync(Usuario user, string roleName)
        {
            return Task.FromResult(user.Roles.Any(r => r.Name == roleName));
        }

        public async Task RemoveFromRoleAsync(Usuario user, string roleName)
        {
            var role = user.Roles.SingleOrDefault(r => r.Name == roleName);

            if (role == null)
                throw new ArgumentException("roleName");

            user.Roles.Remove(role);

            await _dbContext.SaveChangesAsync();
        }

        public Task ResetAccessFailedCountAsync(Usuario user)
        {
            user.AccesosFallidos = 0;
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEnabledAsync(Usuario user, bool enabled)
        {
            user.Bloqueado = enabled;
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEndDateAsync(Usuario user, DateTimeOffset lockoutEnd)
        {
            var nullable = lockoutEnd == DateTimeOffset.MinValue ? null : new DateTime?(lockoutEnd.UtcDateTime);

            user.FechaBloqueo = nullable;

            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetTwoFactorEnabledAsync(Usuario user, bool enabled)
        {
            return Task.FromResult<object>(null);
        }

        public async Task UpdateAsync(Usuario user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                _dbContext?.Dispose();
        }
    }
}