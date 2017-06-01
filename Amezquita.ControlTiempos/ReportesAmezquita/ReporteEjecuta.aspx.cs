using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amezquita.ControlTiempos.Features.Inicio;
using System.Web.Mvc;
using System.Web.Services;
using System.Data;
using Amezquita.ControlTiempos.ReportesAmezquita;
using System.Web.Security;
using Newtonsoft.Json;
namespace Amezquita.ControlTiempos.ReportesAmezquita
{
    public partial class ReporteEjecuta : System.Web.UI.Page
    {
        public static ReportesAmezquitaData reporteData = new ReportesAmezquitaData();


        protected void Page_Load(object sender, EventArgs e)
        {


            string name = HttpContext.Current.User.Identity.Name.ToString();
            string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            string rSultL = reporteData.reporteEjecutado();
            DataTable tester = (DataTable)JsonConvert.DeserializeObject(rSultL, (typeof(DataTable)));
            UsuarioNombre.Text = name.ToString();
            var temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th> Proyecto </th>";
            temp += "<th> Gerente </th>";
            temp += "<th>Revisoria Fiscal</th>";
            temp += "<th>Horas Legales</th>";
            temp += "<th>Horas Impuestos</th>";
            temp += "<th>Horas TI</th>";
            temp += "</tr>";
            temp += "</thead>";

            temp += "<tbody>";

            for (int i = 0; i <= tester.Rows.Count - 1; i++)
            {
                string gerente = (tester.Rows[i].ItemArray[18].ToString());
                string clienteSinteg = (tester.Rows[i].ItemArray[1].ToString());
                string ClienteControlTiempos = (tester.Rows[i].ItemArray[2].ToString());
                string RevisoriaFiscalSinteg = (tester.Rows[i].ItemArray[7].ToString());
                string legalSinteg = (tester.Rows[i].ItemArray[9].ToString());
                string legalControlTiempos = (tester.Rows[i].ItemArray[11].ToString());
                string Impuestos = (tester.Rows[i].ItemArray[16].ToString());
                string ImpuestosSinteg = (tester.Rows[i].ItemArray[8].ToString());
                string TI = (tester.Rows[i].ItemArray[14].ToString());
                string tIsSinteg = (tester.Rows[i].ItemArray[10].ToString());
                string RevisoriaFiscalControlTiempos =( (Convert.ToDecimal( (tester.Rows[i].ItemArray[12].ToString()))) + (Convert.ToDecimal((tester.Rows[i].ItemArray[13].ToString())))+ (Convert.ToDecimal((tester.Rows[i].ItemArray[15].ToString()))) + (Convert.ToDecimal((tester.Rows[i].ItemArray[17].ToString())))).ToString();
                //   string nombreParametro = nombreGerente.Replace(" ", "&");
                temp += "<tr>";
                temp += "<tr><td>" + clienteSinteg + "</td><td>" + gerente + "</td><td>" + RevisoriaFiscalSinteg + "</td><td>"+ legalSinteg + "</td><td>" + ImpuestosSinteg + "</td><td>" + tIsSinteg + "</td></tr>";
                temp += "<tr><td>" + ClienteControlTiempos + "</td><td>" + gerente + "</td><td>" + RevisoriaFiscalControlTiempos + "</td><td>"+ legalControlTiempos + "</td><td>" + Impuestos + "</td><td>" + TI + "</td></tr>";
                temp += "<tr><td colspan='5'></td></tr>";

                temp += "</tr>";
            }

            temp += "</tbody>";
            temp += "</table>";
            tablesRsult.InnerHtml = temp;




        }
        protected string ResolverUrl(object path)
        {
            var url = path.ToString();
            return ResolveClientUrl(url);
        }
    }
}