// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerCargosPorpaginaDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos
{
    public class ObtenerCargosPorPaginaDTO
    {
        #region Instance Properties

        public string Buscar { get; set; }

        public int NumeroPagina { get; set; }

        public int RegistrosPagina { get; set; }

        #endregion
    }
}