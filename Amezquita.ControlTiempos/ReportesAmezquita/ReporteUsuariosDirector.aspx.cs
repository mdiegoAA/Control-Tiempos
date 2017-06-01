using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amezquita.ControlTiempos.Features.Inicio;
using System.Web.Mvc;
using System.Web.Services;
using Amezquita.ControlTiempos.ReportesAmezquita;
using System.Web.Security;

namespace Amezquita.ControlTiempos.Reportes_Amezquita
{
    public partial class ReporteUsuariosDirector : System.Web.UI.Page
    {

      public static  ReportesAmezquitaData reporteData = new  ReportesAmezquitaData();
     static   string name = HttpContext.Current.User.Identity.Name.ToString();
      static   string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);
        protected void Page_Load(object sender, EventArgs e)
        {
            bool rSult = HttpContext.Current.User.Identity.IsAuthenticated;         
          
            string name = HttpContext.Current.User.Identity.Name.ToString();
           string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);          
            UsuarioNombre.Text = name.ToString();
           

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

        [WebMethod]
        public static string obtenerListadoUsuariosXdirector(string ano)
        {
           
            string Rsult = reporteData.obtenerListadoUsuariosXdirector(idUsuario, ano);
            return Rsult;

        }


        [WebMethod]
        public static string ObtenerTiempoUsuarioDirector(string idUsuarios , string año)
        {

            Page page = new Page();


             string name = HttpContext.Current.User.Identity.Name.ToString();
             string idUsuario = reporteData.obtenerUSuarioIDXnombre(name);

            string idDirector = idUsuario;


            string Rsult = reporteData.ObtenerTiempoUsuarioDirector(idDirector, idUsuarios ,año);
            return Rsult;
        }

        [WebMethod]
        public static string ObtenerListadoUsuariosXgruposTrabajo(string ano)
        {

            Page page = new Page();

            

            string name = HttpContext.Current.User.Identity.Name.ToString();
            string idUsuarios = reporteData.obtenerUSuarioIDXnombre(name);

            string idDirector = idUsuarios;


            string Rsult = reporteData.Sp_Amezquita_ObtenerListadoUsuariosXgruposTrabajo(idDirector, ano);
            return Rsult;
        }


    }
   
}