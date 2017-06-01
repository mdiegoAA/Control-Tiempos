using Amezquita.ControlTiempos.Reportes.DataSets.HorasAnualesOperadoresDataSetTableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;

namespace Amezquita.ControlTiempos.Reportes
{
    public partial class HorasAnualesOperadores : System.Web.UI.Page
    {
        protected void GenerarReporte(object sender, EventArgs e)
        {
            using (var adapter = new HoraOperadorTableAdapter())
            {
                var clienteId = new Guid(ClientesDropDownList.SelectedValue);
                var directorId = new Guid(DirectoresDropDownList.SelectedValue);
                var fechaInicio = Convert.ToDateTime(FechaInicioTextBox.Text);
                var fechaFin = Convert.ToDateTime(FechaFinTextBox.Text);

                DataTable dataset = adapter.GetData(clienteId == Guid.Empty ? (Guid?)null :clienteId, directorId == Guid.Empty ? (Guid?)null :directorId, fechaInicio, fechaFin);

                Report.Reset();
                Report.LocalReport.ReportPath = Server.MapPath("~/Reportes/HorasAnualesOperadoresReport.rdlc");
                Report.LocalReport.DataSources.Clear();

                var source = new ReportDataSource("HorasAnualesOperadoresDataSet", dataset);

                Report.LocalReport.DataSources.Add(source);
                Report.LocalReport.Refresh();
            }
        }
    }
}