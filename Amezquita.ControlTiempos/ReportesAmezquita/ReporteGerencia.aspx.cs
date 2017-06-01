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
    public partial class ReporteGerencia : System.Web.UI.Page
    {

        public static ReportesAmezquitaData reporteData = new ReportesAmezquitaData();
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = HttpContext.Current.User.Identity.Name.ToString();
            UsuarioNombre.Text = name.ToString();
        }

        [WebMethod]
        public static string traerReporte(string fecha) {

            string rSultL = reporteData.reporteGerente(fecha);
            return rSultL;


        }


        public string formatoFechas(string fecha)
        {

            int fechaEntera = Convert.ToInt32(fecha);
            int horas = fechaEntera / 60;
            int residuo = fechaEntera % 60;
            string result = "";

            if (residuo != 0)
            {

                result = horas + ":" + residuo;

            }
            else
            {

                result = horas.ToString();
            }

            return result;


        }

        protected string ResolverUrl(object path)
        {
            var url = path.ToString();
            return ResolveClientUrl(url);
        }

    }
}