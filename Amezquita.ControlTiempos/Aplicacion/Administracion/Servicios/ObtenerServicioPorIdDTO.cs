using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Servicios
{
    public class ObtenerServicioPorIdDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del servicio es obligatorio.")]
        public Guid Id { get; set; }

        #endregion
    }
}