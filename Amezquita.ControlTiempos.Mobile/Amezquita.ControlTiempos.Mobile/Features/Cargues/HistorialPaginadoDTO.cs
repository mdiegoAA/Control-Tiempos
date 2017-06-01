// ----------------------------------------------------------------------------------------------
// <copyright file="HistorialPaginadoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public class HistorialPaginadoDTO
    {
        public string Buscar { get; set; }
        public int NumeroPagina { get; set; }
        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int RegistrosPagina { get; set; }
        public bool EsPrimeraPagina { get; set; }
        public bool EsUltimaPagina { get; set; }
        public bool TienePaginaAnterior { get; set; }
        public bool TienePaginaSiguiente { get; set; }
        public List<HistorialDTO> Datos { get; set; }
    }
}