using System;
using System.Data;
using System.Web.UI;
using Amezquita.ControlTiempos.Reportes.DataSets.HorasAnualesClientesDataSetTableAdapters;
using Microsoft.Reporting.WebForms;

namespace Amezquita.ControlTiempos.Reportes
{
    public partial class HorasAnualesClientes : Page
    {
        #region Event Handling

        protected void GenerarReporte(object sender, EventArgs e)
        {
            using (var adapter = new HoraClienteTableAdapter())
            {
                var clienteId = new Guid(ClientesDropDownList.SelectedValue);
                var directorId = new Guid(DirectoresDropDownList.SelectedValue);
                var fechaInicio = Convert.ToDateTime(FechaInicioTextBox.Text);
                var fechaFin = Convert.ToDateTime(FechaFinTextBox.Text);

                DataTable dataset = adapter.GetData(clienteId == Guid.Empty ? (Guid?) null : clienteId, directorId == Guid.Empty ? (Guid?) null : directorId, fechaInicio, fechaFin);

                Report.Reset();
                Report.LocalReport.ReportPath = Server.MapPath("~/Reportes/HorasAnualesClientesReport.rdlc");
                Report.LocalReport.DataSources.Clear();

                var source = new ReportDataSource("HorasAnualesClientesDataSet", dataset);

                Report.LocalReport.DataSources.Add(source);
                Report.LocalReport.Refresh();
            }
        }

        #endregion
    }
}