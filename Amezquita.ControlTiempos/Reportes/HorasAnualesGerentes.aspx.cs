// ----------------------------------------------------------------------------------------------
// <copyright file="HorasAnualesGerentes.aspx.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Web.UI;
using Amezquita.ControlTiempos.Reportes.DataSets.HorasAnualesGerentesDataSetTableAdapters;
using Microsoft.Reporting.WebForms;

namespace Amezquita.ControlTiempos.Reportes
{
    public partial class HorasAnualesGerentes : Page
    {
        #region Event Handling

        protected void ActualizarReporte(object sender, EventArgs e)
        {
            using (var ds = new HoraGerenteTableAdapter())
            {
                var gerenteId = new Guid(GerentesDropDownList.SelectedValue);
                var directorId = new Guid(DirectoresDropDownList.SelectedValue);
                var fechaInicio = Convert.ToDateTime(FechaInicioTextBox.Text);
                var fechaFin = Convert.ToDateTime(FechaFinTextBox.Text);

                DataTable dataset = ds.GetData(gerenteId == Guid.Empty ? (Guid?) null : gerenteId, directorId == Guid.Empty ? (Guid?) null : directorId, fechaInicio, fechaFin);

                Report.Reset();
                Report.LocalReport.ReportPath = Server.MapPath("~/Reportes/HorasAnualesGerentesReport.rdlc");
                Report.LocalReport.DataSources.Clear();

                var reportDataSource = new ReportDataSource("HorasAnualesGerentesDataSet", dataset);

                Report.LocalReport.DataSources.Add(reportDataSource);
                Report.LocalReport.Refresh();
            }
        }

        #endregion
    }
}
