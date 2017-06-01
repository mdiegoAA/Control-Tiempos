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
    public partial class AgregarGrupoTrabajos : System.Web.UI.Page
    {

        public static ReportesAmezquitaData reporteData = new ReportesAmezquitaData();
        protected void Page_Load(object sender, EventArgs e)
        {

          
            string name = HttpContext.Current.User.Identity.Name.ToString();
            string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            string rSultL = reporteData.obtenerListadosGerentes();
            DataTable tester = (DataTable)JsonConvert.DeserializeObject(rSultL, (typeof(DataTable)));
            UsuarioNombre.Text = name.ToString();
            var temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th>#</th>";
            temp += "<th>Nombre Gerente</th>";          
            temp += "<th>Seleccionar</th>";
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";

            for (int i = 0; i <= tester.Rows.Count - 1; i++)
            {

                string idGerente = (tester.Rows[i].ItemArray[0].ToString());
                string nombreGerente = (tester.Rows[i].ItemArray[1].ToString());
                string  nombreParametro = nombreGerente.Replace(" ", "&");
                temp += "<tr>";
                temp += "<td colspna='2'>" + i + "</td>";
                temp += "<td colspna='2'>" + nombreGerente + "</td>";
              
                temp += "<td colspna='2'><button class='btn btn-primary btn-xs' value=" + idGerente + "#"+ nombreParametro + " onClick='AgregarUsuario(this.value)' type='submit'>Seleccionar</button></td>";
               
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

        [WebMethod]
        public static string obtenerListadoUsuario(string usuario , string idGerente) {

            string rSult = reporteData.obtenerListadosUsuarios(usuario, idGerente);
            return rSult;

        }
        [WebMethod]
        public static string agregarUsuarioGrupoTrabajo(string idGerente , string idUsuario)
        {

            string rSult = reporteData.agregarUsuario_grupoTrabajo(idGerente, idUsuario);
            return rSult;

        }
        [WebMethod]
        public static string eliminarUsuariosxGrupos(string idGerente, string idUsuario)
        {

            string rSult = reporteData.eliminarUsuariosxGrupos(idGerente, idUsuario);
            return rSult;

        }

        [WebMethod]
        public static string CambiarEstadoUsuario(string idUsuario)
        {

            string rSult = reporteData.CambiarEstadoUsuario(idUsuario);
            return rSult;

        }



        [WebMethod]
        public static string obtenerGrupoTrabajo(string idGerente)
        {

            string rSult = reporteData.Sp_Amezquita_ObtenerListadoUsuariosXgrupos(idGerente);
            return rSult;

        }



    }
}