// ----------------------------------------------------------------------------------------------
// <copyright file="ExtensionesValueInjecter.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;

namespace Amezquita.ControlTiempos.Infraestructura.Extensiones
{
    public static class ExtensionesValueInjecter
    {
        public static ICollection<TTo> InjectFrom<TFrom, TTo>(this ICollection<TTo> to, IEnumerable<TFrom> from) where TTo : new()
        {
            foreach (var source in from)
            {
                var target = new TTo();
                target.InjectFrom(source);
                to.Add(target);
            }

            return to;
        }

        public static ICollection<TTo> InjectFrom<TFrom, TTo>(this ICollection<TTo> to, IEnumerable<TFrom> from, ValueInjection injector) where TTo : new()
        {
            foreach (var source in from)
            {
                var target = new TTo();
                target.InjectFrom(injector, source);
                to.Add(target);
            }

            return to;
        }
    }
}