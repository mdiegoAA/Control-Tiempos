// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using Amezquita.ControlTiempos.Infraestructura;
using DataAnnotationsExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class GuardarProyectoDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "La cantidad de días sin registrar en el proyecto es obligatoria.")]
        [Integer(ErrorMessage = "Los días sin registrar deben ser un número entero valido.")]
        [Display(Name = "Días sin Registrar")]
        public int AlertasProyectoDiasSinRegistrar { get; set; }

        [Required(ErrorMessage = "El porcentaje ejecutado en el proyecto es obligatorio.")]
        [Numeric(ErrorMessage = "El porcentaje ejecutado debe ser un número decimal válido.")]
        [Display(Name = "Porcentaje Ejecutado")]
        public decimal AlertasProyectoPorcentajeEjecutado { get; set; }

        [Integer(ErrorMessage = "El año debe ser un número entero válido.")]
        [Required(ErrorMessage = "El año del proyecto es obligatorio.")]
        [Display(Name = "Año")]
        public int Año { get; set; }

        [Display(Name = "Auditor")]
        public Guid? AuditorId { get; set; }

        [Required(ErrorMessage = "El valor de la bolsa de horas es obligatorio.")]
        [Numeric(ErrorMessage = "La bolsa de horas debe ser un número decimal válido.")]
        [Display(Name = "Bolsa de Horas")]
        public decimal BolsaHoras { get; set; }

        [Required(ErrorMessage = "El cliente del proyecto es obligatorio.")]
        [Display(Name = "Cliente")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "El director del proyecto es obligatorio.")]
        [Display(Name = "Director")]
        public Guid DirectorId { get; set; }

        [Required(ErrorMessage = "La fecha final del proyecto es obligatoria.")]
        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date, ErrorMessage = "La fecha final debe ser una fecha válida.")]
        [IsDateAfter("FechaInicio", false, ErrorMessage = "La fecha final debe ser mayor a la fecha de incio.")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "La fecha inicial del proyecto es obligatoria.")]
        [Display(Name = "Fecha Inicial")]
        [DataType(DataType.Date, ErrorMessage = "La fecha inicial debe ser una fecha válida.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El gerente del proyecto es obligatorio.")]
        [Display(Name = "Gerente")]
        public Guid GerenteId { get; set; }

        [Required(ErrorMessage = "El id del proyecto es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Supervisor")]
        public Guid? SupervisorId { get; set; }

        #endregion
    }
}