using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amezquita.ControlTiempos.Features.Inicio;
using System.Web.Mvc;
using System.Web.Services;
using Amezquita.ControlTiempos.ReportesAmezquita;
using System.Web.Security;
using Newtonsoft.Json;

namespace Amezquita.ControlTiempos.ReportesAmezquita
{
    public partial class ClientesServicios : System.Web.UI.Page
    {
        

            public static ReportesAmezquitaData reporteData = new ReportesAmezquitaData();

            protected void Page_Load(object sender, EventArgs e)
            {
            bool rSult = HttpContext.Current.User.Identity.IsAuthenticated;

           string name = HttpContext.Current.User.Identity.Name.ToString();
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            //    string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);          
            UsuarioNombre.Text = name.ToString();
            string rSultado = reporteData.proyectosXDirectorTotal(idUsuario);
            DataTable tester = (DataTable)JsonConvert.DeserializeObject(rSultado, (typeof(DataTable)));
            string temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th colspna='2'>Proyectos</th>";
            temp += "<th colspna='2'>Servicio</th>";
            temp += "<th colspna='2'>Ejecutadas</th>";
            temp += "<th colspna='2'>Total Horas</th>";
            temp += "<th colspna='2'>Restante Horas</th>";
            temp += "<th colspna='2'>Seleccionar</th>";          
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";

            for (int i = 0; i<= tester.Rows.Count - 1; i++) {

                string idProyecto = (tester.Rows[i].ItemArray[0].ToString()) + "%" + i;
                string nombre = (tester.Rows[i].ItemArray[1].ToString());
                //  string AucnombrePro = nombre.Replace("","&");
                string nombreServicio = (tester.Rows[i].ItemArray[1].ToString());
                int horasValor = ((Convert.ToInt32(tester.Rows[i].ItemArray[2].ToString())*60) - Convert.ToInt32((tester.Rows[i].ItemArray[5].ToString())));
                temp += "<tr id='table_" + i + "'>";
                temp += "<td colspna='2'>" + nombre + "</td>";
                temp += "<td colspna='2'>" +  (tester.Rows[i].ItemArray[3].ToString()) + "</td>";
                temp += "<td colspna='2'>" + formatoFechas((tester.Rows[i].ItemArray[5].ToString())) + "</td>";
                temp += "<td colspna='2'>" + (tester.Rows[i].ItemArray[2].ToString()) + "</td>";
                temp += "<td colspna='2'>" + formatoFechas(horasValor.ToString()) + "</td>";
                temp += "<td colspna='2'><button class='btn btn-primary btn-xs' value="+ idProyecto + " onClick='calendario(this.value)' type='submit'>Seleccionar</button></td>";



            }

            tablesRsult.InnerHtml = temp;
            


        }



            public void pruebaclick()
            {



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

            [WebMethod]
            public static string obtenerListadoClientes(string fechaInicial, string fechaFinal)
            {

                Page page = new Page();


                string name = HttpContext.Current.User.Identity.Name.ToString();

                string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);

                string rSult = reporteData.proyectosXDirector(idUsuario, fechaInicial, fechaFinal);
                return "";



            }

            [WebMethod]
            public static string reporteTiempoMensual(string idArchivo)
            {

                string rSult = reporteData.reporteTiempoMensual(idArchivo);
                return rSult;

            }


        public string formatoFechas(string fecha) {

            int fechaEntera = Convert.ToInt32(fecha);
            int horas = fechaEntera / 60;
            int residuo = fechaEntera % 60;
            string result = "";

            if (residuo != 0)
            {

                result = horas + ":" + residuo;

            }
            else {

                 result = horas.ToString();
            }

            return result;


        }

            [WebMethod]
            public static string ObtenerRegistroXMes_Tiempo(string idArchivo)
            {

            string Rsult = reporteData.ObtenerRegistroXMes_Tiempo(idArchivo);

             return Rsult;

            }

       

            [WebMethod]
            public static string UsuariosXServicioXproyecto(string idProyecto, string idServicio)
            {

                string Rsult = reporteData.UsuariosXServicioXproyecto(idProyecto, idServicio);
                return Rsult;

            }






        }

    
}