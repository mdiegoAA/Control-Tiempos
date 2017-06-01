// ----------------------------------------------------------------------------------------------
// <copyright file="IGeneradorIdentidad.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

namespace Amezquita.ControlTiempos.Infraestructura
{
    public interface IGeneradorIdentidad<T>
    {
        T GenerarId();
    }
}