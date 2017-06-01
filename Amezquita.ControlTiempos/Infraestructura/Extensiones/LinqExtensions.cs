// ----------------------------------------------------------------------------------------------
// <copyright file="LinqExtensions.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Amezquita.ControlTiempos.Infraestructura.Extensiones
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Except<T, TId>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TId> getId)
        {
            return from item in items
                   join otherItem in other on getId(item) equals getId(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;
        }
    }
}