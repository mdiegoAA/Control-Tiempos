using Amezquita.ControlTiempos.Reportes.DataSets.HorasAnualesAreasDataSetTableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;

namespace Amezquita.ControlTiempos.Reportes
{
    public partial class HorasAnualesAreas : System.Web.UI.Page
    {
        protected void GenerarReporte(object sender, EventArgs e)
        {
            using (var adapter = new HoraAreaTableAdapter())
            {
                var clienteId = new Guid(ClientesDropDownList.SelectedValue);
                var directorId = new Guid(DirectoresDropDownList.SelectedValue);
                var fechaInicio = Convert.ToDateTime(FechaInicioTextBox.Text);
                var fechaFin = Convert.ToDateTime(FechaFinTextBox.Text);

                DataTable dataset = adapter.GetData(clienteId == Guid.Empty ? (Guid?)null :clienteId, directorId == Guid.Empty ? (Guid?)null :directorId, fechaInicio, fechaFin);

                Report.Reset();
                Report.LocalReport.ReportPath = Server.MapPath("~/Reportes/HorasAnualesAreasReport.rdlc");
                Report.LocalReport.DataSources.Clear();

                var source = new ReportDataSource("HorasAnualesAreasDataSet", dataset);

                Report.LocalReport.DataSources.Add(source);
                Report.LocalReport.Refresh();
            }
        }
    }
}