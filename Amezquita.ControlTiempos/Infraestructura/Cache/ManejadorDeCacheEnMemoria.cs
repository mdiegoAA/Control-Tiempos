// ----------------------------------------------------------------------------------------------
// <copyright file="ManejadorDeCacheEnMemoria.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Runtime.Caching;

namespace Amezquita.ControlTiempos.Infraestructura.Cache
{
    public class ManejadorDeCacheEnMemoria : IManejadorDeCache
    {
        private bool _disposed;

        ~ManejadorDeCacheEnMemoria()
        {
            Dispose(false);
        }

        public bool Agregar(string llave, object valor)
        {
            ValidarLlave(llave);
            ValidarValor(valor);

            if (!MemoryCache.Default.Contains(llave))
                return MemoryCache.Default.Add(llave, valor, new CacheItemPolicy());

            MemoryCache.Default.Set(llave, valor, new CacheItemPolicy());

            return true;
        }

        public bool Agregar(string llave, object valor, DateTimeOffset fechaExpiracion)
        {
            ValidarLlave(llave);
            ValidarValor(valor);

            if (!MemoryCache.Default.Contains(llave))
                return MemoryCache.Default.Add(llave, valor, fechaExpiracion);

            MemoryCache.Default.Set(llave, valor, fechaExpiracion);

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Eliminar(string llave)
        {
            ValidarLlave(llave);

            if (MemoryCache.Default.Contains(llave))
                return MemoryCache.Default.Remove(llave) != null;

            return false;
        }

        public void Limpiar()
        {
            foreach (var elemento in MemoryCache.Default)
                MemoryCache.Default.Remove(elemento.Key);
        }

        public TValorCache Obtener<TValorCache>(string llave)
        {
            ValidarLlave(llave);

            var obj = MemoryCache.Default.Get(llave);

            if (obj != null)
                return (TValorCache) obj;

            return default(TValorCache);
        }

        protected virtual void Dispose(bool disposeManaged)
        {
            if (!_disposed)
            {
                if (disposeManaged)
                    MemoryCache.Default.Dispose();

                _disposed = true;
            }
        }

        private static void ValidarLlave(string llave)
        {
            if (string.IsNullOrEmpty(llave))
                throw new ArgumentNullException(nameof(llave));
        }

        private static void ValidarValor(object valor)
        {
            if (valor == null)
                throw new ArgumentNullException(nameof(valor));
        }
    }
}