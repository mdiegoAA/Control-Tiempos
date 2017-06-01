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
    public partial class ReportesAmezquita : System.Web.UI.Page
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
            string rSultL = reporteData.proyectosXDirector(idUsuario, fechaInicial, fechaFinal);
            DataTable tester = (DataTable)JsonConvert.DeserializeObject(rSultL, (typeof(DataTable)));
            UsuarioNombre.Text = name.ToString();
            var temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th>Proyectos</th>";
            temp += "<th>Servicio</th>";
            temp += "<th>Ejecutadas</th>";
            temp += "<th>Total Horas</th>";
            temp += "<th>Restante Horas</th>";
            temp += "<th>Seleccionar</th>";
            temp += "<th>Generar Reporte</th>";
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";

            for (int i = 0; i <= tester.Rows.Count - 1; i++)
            {

                string idProyecto = (tester.Rows[i].ItemArray[0].ToString()) + "%" + i;
                string nombre = (tester.Rows[i].ItemArray[1].ToString());
                 string AucnombrePro = nombre.Replace(" ","&");
                string nombreServicio = (tester.Rows[i].ItemArray[1].ToString());
                string servicioss = nombreServicio.Replace(" ", "&");
                int horasValor = ((Convert.ToInt32(tester.Rows[i].ItemArray[2].ToString()) * 60) - Convert.ToInt32((tester.Rows[i].ItemArray[5].ToString())));
                temp += "<tr id='table_" + i + "'>";
                temp += "<td colspna='2'>" + nombre + "</td>";
                temp += "<td colspna='2'>" + (tester.Rows[i].ItemArray[3].ToString()) + "</td>";
                temp += "<td colspna='2'>" + formatoFechas((tester.Rows[i].ItemArray[5].ToString())) + "</td>";
                temp += "<td colspna='2'>" + (tester.Rows[i].ItemArray[2].ToString()) + "</td>";
                temp += "<td colspna='2'>" + formatoFechas(horasValor.ToString()) + "</td>";
                temp += "<td colspna='2'><button class='btn btn-primary btn-xs' value=" + idProyecto+ '%' + AucnombrePro + '!' + i + " onClick='calendario(this.value)' type='submit'>Seleccionar</button></td>";
                temp += "<td><button type='button' value=" + idProyecto + '#' + AucnombrePro + '%' + servicioss + " onclick='reporteTiempoMensual(this.value)' class='btn btn-default btn-xs'><span class='fa fa-file-excel-o' aria-hidden='true'></span> Generar Excel</button></td>";
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
        public static string obtenerListadoClientes(string fechaInicial, string fechaFinal) {

            Page page = new Page();


            string name = HttpContext.Current.User.Identity.Name.ToString();

            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);

            string rSult = reporteData.proyectosXDirector(idUsuario, fechaInicial, fechaFinal);
            return rSult;

            

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