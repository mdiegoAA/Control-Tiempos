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
    public partial class HistoricoUsuario : System.Web.UI.Page
    {

        public static ReportesAmezquitaData reporteData = new ReportesAmezquitaData();
        public static string idUsuarioGlobal=""; 
        protected void Page_Load(object sender, EventArgs e)
        {

            bool rSult = HttpContext.Current.User.Identity.IsAuthenticated;

            string name = HttpContext.Current.User.Identity.Name.ToString();
            string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);
            UsuarioNombre.Text = name.ToString();
            nombreUsuario.InnerHtml = nombreUsuarioRsuLT;
        }

        protected string ResolverUrl(object path)
        {
            var url = path.ToString();
            return ResolveClientUrl(url);
        }



        [WebMethod]
        public static string ObtenerReporteXcliente(string Fecha) {

            string name = HttpContext.Current.User.Identity.Name.ToString();

            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
         
            var dateTime = DateTime.Parse(Fecha);
            var mes = dateTime.Month.ToString();
            var año = dateTime.Year.ToString();

            string rSult = reporteData.ObtenerReporteXcliente(idUsuario, mes,año);
            return rSult;

            
        }

        [WebMethod]
        public static string obtenerProyectoGrupoTrabajo()
        {

            string name = HttpContext.Current.User.Identity.Name.ToString();

            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
          

            string rSult = reporteData.obtenerProyectoGrupoTrabajo(idUsuario);
            return rSult;


        }

        [WebMethod]
        public static string eliminarRegistroId(string idCargue)
        {
            
            string rSult = reporteData.eliminarRegistroId(idCargue);
            return rSult;


        }

        [WebMethod]
        public static string validacionCargueNumeroDias(string fecha)
        {
            string name = HttpContext.Current.User.Identity.Name.ToString();

            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            DateTime fechaRegistro = Convert.ToDateTime(fecha);
            DateTime fechaActual = DateTime.Now;          
            TimeSpan ts = fechaActual - fechaRegistro;
            int differenceMinutes = ts.Hours;
       
            // Difference in days.
            int differenceInDays = ts.Days;
            string result ="true";
            string RestricionDias = reporteData.obtnerTotalLimitesDias(idUsuario);


            if ((differenceInDays == 0) && (differenceMinutes < 0)) {

                differenceInDays = -1;


            }


            DataTable tester = (DataTable)JsonConvert.DeserializeObject(RestricionDias, (typeof(DataTable)));
            string horasd = (tester.Rows[0].ItemArray[0].ToString());
            if ((Convert.ToInt32(horasd)  < differenceInDays)||(0 > differenceInDays)) {
               

                result = "False";

            }
            return result;


        }

        [WebMethod]
        public static string validacionCargueNumeroDiasModificar(string fecha , string idProyecto)
        {
            string name = HttpContext.Current.User.Identity.Name.ToString();

            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            DateTime fechaRegistro = Convert.ToDateTime(fecha);
            int ano = fechaRegistro.Year;
            int mes = fechaRegistro.Month;
            int dia = fechaRegistro.Day;
            DateTime fechaActual = DateTime.Now;
            TimeSpan ts = fechaActual - fechaRegistro;
            int differenceMinutes = ts.Hours;

            // Difference in days.
            int differenceInDays = ts.Days;
            string result = "true";
            string RestricionDias = reporteData.obtnerTotalLimitesDias(idUsuario);


            if ((differenceInDays == 0) && (differenceMinutes < 0))
            {

                differenceInDays = -1;


            }


            DataTable tester = (DataTable)JsonConvert.DeserializeObject(RestricionDias, (typeof(DataTable)));
            string horasd = (tester.Rows[0].ItemArray[0].ToString());
            if ((Convert.ToInt32(horasd) < differenceInDays) || (0 > differenceInDays))
            {


                result = "False";

            }

            if (result == "true") {


                string rSult = reporteData.obtenercarguesUsuarioXdia(idUsuario , idProyecto , dia,mes,ano);
                result = rSult;

            }


            return result;


        }



        [WebMethod]
        public static string ObtenerServicioProyecto(string idProyecto)
        {

            string rSult = reporteData.ObtenerServicioProyecto(idProyecto);
            return rSult;
            
        }

        [WebMethod]
        public static string obtenerActividades()
        {

            string rSult = reporteData.obtenerActividades();
            return rSult;

        }


        [WebMethod]
        public static string obtenerActividadesxServicio(string idActividad)
        {

            string rSult = reporteData.obtenerActividadesXservicios(idActividad);
            return rSult;

        }


        [WebMethod]
        public static string registrarTiempo(string Observaciones ,string ActividadId , string ServicioId , string diaCargar , string horas , string idproyecto) {
            string name = HttpContext.Current.User.Identity.Name.ToString();
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);

            int minutos = Convert.ToDateTime(horas).Minute;
            int horasEnteras = Convert.ToDateTime(horas).Hour;


            string validacionHoras = reporteData.obtnerTotalHorasDia(idUsuario , diaCargar);
           
            string horasd = "";
            if (validacionHoras == "")
            {
                horasd = "0";
            }
            else
            {
                DataTable tester = (DataTable)JsonConvert.DeserializeObject(validacionHoras, (typeof(DataTable)));
                horasd = (tester.Rows[0].ItemArray[0].ToString());
            }
            horasd = horasd.Replace(".",":");


            int index = horasd.IndexOf(":");
            int minutosTotalRegistrados = 0;
            if (index != -1)
            {

                int HorasRegistradosS = Convert.ToInt32(horasd.Substring(0, index));

                string formato = horasd.Substring(2);
                int minutosRegistradosS = Convert.ToInt32(horasd.Substring(index + 1));
                minutosTotalRegistrados = minutosRegistradosS + (HorasRegistradosS * 60);

            }
         

             
            int minutosTotal = minutos + (horasEnteras * 60);
          

            int totalTiempo = minutosTotal + minutosTotalRegistrados;

            int tiempoLimiteDia = 1440 - minutosTotalRegistrados;

            if (totalTiempo > 1440)
            {

                return "El tiempo limite es de " + (tiempoLimiteDia / 60 )  + " : " + (tiempoLimiteDia % 60);


            }
          

                DateTime fechaInicio = Convert.ToDateTime(diaCargar.ToString());
                DateTime fechaFin = fechaInicio.AddMinutes(minutosTotal);

                string rsult = reporteData.registroTiempo(idUsuario, Observaciones, fechaInicio, fechaFin, ActividadId, idproyecto, ServicioId);

                return "bn";
            

        }

        [WebMethod]
        public static string EditarTiempo(string Observaciones, string ActividadId, string ServicioId, string diaCargar, string horas, string idproyecto, string idCargueTiempo , string fecha ,string idProyectomodificar,string proyectoO)
        {

            DateTime fechaConvertida = Convert.ToDateTime(fecha);
            int dia = fechaConvertida.Day;
            int mes = fechaConvertida.Month;
            int ano = fechaConvertida.Year;
            string name = HttpContext.Current.User.Identity.Name.ToString();
            
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            string tiempoModificar = reporteData.obtenertiempoModificar(idUsuario,idproyecto,dia,mes,ano, idProyectomodificar, proyectoO);
            DataTable tester = (DataTable)JsonConvert.DeserializeObject(tiempoModificar, (typeof(DataTable)));

            string pPrueba = (tester.Rows[0].ItemArray[0].ToString());
            int valor = 0;
            if (pPrueba == "")
            {

                valor = 0;

            }
            else {

                valor = Convert.ToInt32(pPrueba);

            }

           

            int minutos = Convert.ToDateTime(horas).Minute;
            int horasEnteras = (Convert.ToDateTime(horas).Hour)*60 ;


            int rsultF = valor + minutos + horasEnteras;
            string rSultFV = "";
            if (rsultF > 1440) {

                int tiempo = 1440 - valor;

                rSultFV = "El tiempo de modificacion no puede ser mayor a " +(tiempo /60) + " : " +(tiempo % 60);

            }
            if (rsultF <= 1440) {

                string rSultModificar = reporteData.eliminarRegistroId(idProyectomodificar);


                string rSult = registrarTiempo(Observaciones, ActividadId, ServicioId, diaCargar, horas, idproyecto);
                rSultFV = "bn";
            }

            return rSultFV;

        }

        [WebMethod]
        public static string EliminarTiempo(string fecha, string idProyectomodificar)
        {

            DateTime fechaConvertida = Convert.ToDateTime(fecha);
            int dia = fechaConvertida.Day;
            int mes = fechaConvertida.Month;
            int ano = fechaConvertida.Year;
            string name = HttpContext.Current.User.Identity.Name.ToString();
            string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
            string rSult = reporteData.EditarTiempoUusario(idUsuario, idProyectomodificar, dia, mes, ano);
            return "bn";
        }

        
    }
}