// ----------------------------------------------------------------------------------------------
// <copyright file="Paginado.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Infraestructura</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class Paginado<TEntidad>
    {
        public Paginado(IEnumerable<TEntidad> datos, int numeroPagina, int registrosPagina, int total)
        {
            if (numeroPagina < 1)
                throw new ArgumentOutOfRangeException(nameof(numeroPagina));

            if (registrosPagina < 1)
                throw new ArgumentOutOfRangeException(nameof(registrosPagina));

            Total = total;
            RegistrosPagina = registrosPagina;
            NumeroPagina = numeroPagina;
            TotalPaginas = Total > 0 ? (int) Math.Ceiling(Total / (double) RegistrosPagina) : 0;
            TienePaginaAnterior = NumeroPagina > 1;
            TienePaginaSiguiente = NumeroPagina < TotalPaginas;
            EsPrimeraPagina = NumeroPagina == 1;
            EsUltimaPagina = NumeroPagina >= TotalPaginas;

            Datos = datos.ToList();
        }

        public List<TEntidad> Datos { get; private set; }

        public bool EsPrimeraPagina { get; private set; }

        public bool EsUltimaPagina { get; private set; }

        public int NumeroPagina { get; private set; }

        public int RegistrosPagina { get; private set; }

        public bool TienePaginaAnterior { get; private set; }

        public bool TienePaginaSiguiente { get; private set; }

        public int Total { get; private set; }

        public int TotalPaginas { get; private set; }
    }
}