// ----------------------------------------------------------------------------------------------
// <copyright file="GuidSecuencial.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class GuidSecuencial : IGeneradorIdentidad<Guid>
    {
        public Guid GenerarId()
        {
            const int rpcSOk = 0;
            Guid guid;

            var hr = UuidCreateSequential(out guid);

            if (hr != rpcSOk)
                throw new ApplicationException("UuidCreateSequential failed: " + hr);

            return guid;
        }

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);
    }
}