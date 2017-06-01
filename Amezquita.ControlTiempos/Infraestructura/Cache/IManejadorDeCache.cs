// ----------------------------------------------------------------------------------------------
// <copyright file="IManejadorDeCache.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Infraestructura.Cache
{
    public interface IManejadorDeCache : IDisposable
    {
        bool Agregar(string llave, object valor);
        bool Agregar(string llave, object valor, DateTimeOffset fechaExpiracion);
        bool Eliminar(string llave);
        void Limpiar();
        TValorCache Obtener<TValorCache>(string llave);
    }
}