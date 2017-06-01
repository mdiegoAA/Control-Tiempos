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

namespace Amezquita.ControlTiempos.Reportes_Amezquita
{
    public partial class ReporteGrupoTrabajo : System.Web.UI.Page
    {

      public static  ReportesAmezquitaData reporteData = new  ReportesAmezquitaData();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            bool rSult = HttpContext.Current.User.Identity.IsAuthenticated;

            string fechaInicial = "01/01/2001";
            string fechaFinal = "01/01/2001";
            string name = HttpContext.Current.User.Identity.Name.ToString();
            string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            string rSultL = reporteData.obtenerUsuariosGrupoTarbajoXGerente(idUsuario);
            DataTable tester = (DataTable)JsonConvert.DeserializeObject(rSultL, (typeof(DataTable)));
            UsuarioNombre.Text = name.ToString();
            var temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th  colspna='2'>Usuario</th>";
            temp += "<th  colspna='2'>Dias sin cargar</th>";
            temp += "<th>Opcion</th>";   
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";

            for (int i = 0; i <= tester.Rows.Count - 1; i++)
            {

                string idProyecto = (tester.Rows[i].ItemArray[0].ToString());
                string nombre = (tester.Rows[i].ItemArray[1].ToString());
                string dias = (tester.Rows[i].ItemArray[2].ToString());
                string estado = (tester.Rows[i].ItemArray[3].ToString());
                string nmbreM= nombre.Replace(" ", "&");

                
                temp += "<tr id='table_" + i + "'>";

                if (estado == "1")
                {

                    temp += "<td colspna='2'>" + nombre + "</td>";
                }
                if (estado == "0")
                {

                    temp += "<td colspna='2' style='color:#a6a6a6'><u>" + nombre + "</u></td>";
                }


                int diasEnteros = Convert.ToInt32(dias);

                if (diasEnteros > 8)
                {

                    temp += "<td colspna='2' style='color:red'>" + dias + "</td>";

                }
                else
                {

                    temp += "<td colspna='2'>" + dias + "</td>";
                }
                temp += "<td><button type='button' value=" + idProyecto + "$"+ nmbreM + " onclick='reporteTiempoMensual(this.value)' class='btn btn-primary btn-xs'>Seleccionar</button></td>";
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
        public static string obtenerListadoClientes(string fechaInicial, string fechaFinal)
        {

            Page page = new Page();


            string name = HttpContext.Current.User.Identity.Name.ToString();

            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);

            string rSult = reporteData.proyectosXDirector(idUsuario, fechaInicial, fechaFinal);
            return rSult;



        }



        [WebMethod]
        public static string ObtenerReporteXclienteGrupoTrabajo( string usuario, string Fecha)
        {

            string name = HttpContext.Current.User.Identity.Name.ToString();

        
            var dateTime = DateTime.Parse(Fecha);
            var mes = dateTime.Month.ToString();
            var año = dateTime.Year.ToString();

            string rSult = reporteData.ObtenerReporteXcliente(usuario, mes, año);
            return rSult;


        }


        [WebMethod]
        public static string ServicioxActividadesxTiempo(string idArchivo)
        {
            string Rsult = reporteData.ServicioxActividadesxTiempo(idArchivo);

            return Rsult;
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


      

        [WebMethod]
        public static string reporteTiempoMensual(string idArchivo)
        {

            string rSult = reporteData.reporteTiempoMensual(idArchivo);
            return rSult;

        }

        [WebMethod]
        public static string UsuariosXServicioXproyecto(string idProyecto, string idServicio)
        {

            string Rsult = reporteData.UsuariosXServicioXproyecto(idProyecto, idServicio);
            return Rsult;

        }






    }
   
}