using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Amezquita.ControlTiempos.ReportesAmezquita
{
    public class ReportesAmezquitaData:Conexion
    {

        public string proyectosXDirector(string idDirector, string fechaInicial, string fechaFinal)
        {

            fechaInicial = formatoFechas(fechaInicial);
            fechaInicial = fechaInicial + " 00:00:00";
            fechaFinal = formatoFechas(fechaFinal);
            fechaFinal = fechaFinal + " 23:59:00";

            SqlCommand cmd = new SqlCommand("sp_Amezquita_ProyectosDirector", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fechaInicial", fechaInicial);
            cmd.Parameters.AddWithValue("@fechaFinal", fechaFinal);
            cmd.Parameters.AddWithValue("@idDirector", idDirector);
            //     cmd.Parameters.AddWithValue("@IdUsuario", id);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


      
        public string obtenerUsuariosGrupoTarbajoXGerente(string idGerente)
        {


            SqlCommand cmd = new SqlCommand("obtenerUsuariosGrupoTarbajoXGerente", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idGerente", idGerente);

            //     cmd.Parameters.AddWithValue("@IdUsuario", id);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


        public string obtenerListadosGerentes()
        {
            

            SqlCommand cmd = new SqlCommand("sp_amezquita_ObtenerListadoGerentes", conn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            //     cmd.Parameters.AddWithValue("@IdUsuario", id);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


        public string obtenerProyectoGrupoTrabajo(string idUsuario)
        {

           
            SqlCommand cmd = new SqlCommand("sp_amezquita_obtenerProyectoGrupoTrabajo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

               cmd.Parameters.AddWithValue("@usuarioId", idUsuario);
            try
            {

              

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }

        public string ObtenerServicioProyecto(string idProyecto)
        {


            SqlCommand cmd = new SqlCommand("sp_amezquita_ObtenerServicioProyecto", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@proyecto", idProyecto);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }

        public string obtenerActividades()
        {


            SqlCommand cmd = new SqlCommand("sp_amezquita_obtenerActividades", conn);
            cmd.CommandType = CommandType.StoredProcedure;

           
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }



        public string reporteEjecutado()
        {


            SqlCommand cmd = new SqlCommand("Sp_Amezquita_ReporteTiempoEjecutadovsHoraTiempo", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


        public string obtenerActividadesXservicios(string servicios)
        {


            SqlCommand cmd = new SqlCommand("sp_amezquita_obtenerActividadesServicio", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idServicio", servicios);

            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


        public string obtenerListadosUsuarios(string usuario , string idGerente)
        {
            

            SqlCommand cmd = new SqlCommand("sp_amezquitaBuscarUsuario", conn);
            cmd.CommandType = CommandType.StoredProcedure;

              cmd.Parameters.AddWithValue("@nombreUsuario", usuario);
            cmd.Parameters.AddWithValue("@gerenteId", idGerente);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


        public string obtenerUusariosxProyecto(string idDirector,string año)
        {
            

            SqlCommand cmd = new SqlCommand("sp_Amezquita_ProyectosDirector", conn);
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@ano", año);
            cmd.Parameters.AddWithValue("@idDirector", idDirector);
            //     cmd.Parameters.AddWithValue("@IdUsuario", id);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }


        public string reporteGerente(string fecha)
        {


            DateTime fechaFormato = Convert.ToDateTime(fecha);

            string mes = fechaFormato.Month.ToString();
            string año = fechaFormato.Year.ToString(); 
            SqlCommand cmd = new SqlCommand("sp_amezquita_reporteGerenteHorasCargables", conn);
            cmd.CommandType = CommandType.StoredProcedure;
           
                 cmd.Parameters.AddWithValue("@mes", mes);
                 cmd.Parameters.AddWithValue("@año", año);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }

        public string reporteGerenteHorasNoCargables(string fecha)
        {


            DateTime fechaFormato = Convert.ToDateTime(fecha);

            string mes = fechaFormato.Month.ToString();
            string año = fechaFormato.Year.ToString();
            SqlCommand cmd = new SqlCommand("sp_amezquita_reporteGerenteHorasNoCargables", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@mes", mes);
            cmd.Parameters.AddWithValue("@año", año);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }

        public string proyectosXDirectorTotal(string idDirector)
        {





         

            SqlCommand cmd = new SqlCommand("sp_Amezquita_ProyectosDirectorTotal", conn);
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.AddWithValue("@idDirector", idDirector);
            //     cmd.Parameters.AddWithValue("@IdUsuario", id);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }
        public string ObtenerRegistroXMes_Tiempo(string Proyecto)
        {

            SqlCommand cmdArchivos = new SqlCommand("Sp_Amezquita_ObtenerRegistroXMes_Tiempo", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idProyecto", Proyecto);



            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception)
            {
                cerrarConexion();
                return "";
            }


        }

        public string ServicioxActividadesxTiempo(string Proyecto)
        {

            SqlCommand cmdArchivos = new SqlCommand("ServicioxActividadesxTiempo", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idProyecto", Proyecto);



            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception)
            {
                cerrarConexion();
                return "";
            }


        }

        public string agregarUsuario_grupoTrabajo(string idGerente , string idUsuario )
        {

            SqlCommand cmdArchivos = new SqlCommand("agregarUsuarioGrupoTrabajo", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idGerente", idGerente);
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idUsuario);



            try
            {


                abrirConexion();
                cmdArchivos.ExecuteNonQuery();              
                cerrarConexion();               
                return "bn";

            }

            catch (Exception)
            {
                cerrarConexion();
                return "";
            }


        }


        public static string formatoFechas(string fecha)
        {


            DateTime fechaDatatime = Convert.ToDateTime(fecha);
            int dayDatatime = fechaDatatime.Day;
            int mesDatatime = fechaDatatime.Month;
            int yearDatatime = fechaDatatime.Year;
            string dayReturn = "";
            string mesReturn = "";

            if (dayDatatime <= 9)
            {
                dayReturn = "0" + dayDatatime;

            }
            else
            {

                dayReturn = dayDatatime.ToString();

            }


            if (mesDatatime <= 9)
            {
                mesReturn = "0" + mesDatatime;
            }
            else
            {
                mesReturn = mesDatatime.ToString();
            }

            string fechaInicialBusqueda = dayReturn + "/" + mesReturn + "/" + yearDatatime;


            return fechaInicialBusqueda;

        }

        public string ObtenerReporteXcliente(string data,string mes , string año) {

            SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["ControlTiemposDbContext"].ConnectionString);
            SqlCommand cmds = new SqlCommand();
            SqlDataReader reader;

            string query = "with ReporteTiempo (proyectoId , nombreProyecto,dia , mes ,año , tiempo) as (";
            query = query + " select p.ProyectoId , SUBSTRING ( P.NombreProyecto ,CHARINDEX('-',P.NombreProyecto)+1,LEN(P.NombreProyecto)), DAY(ch.FechaInicio) as dia  , MONTH(ch.FechaInicio) as mes , year(ch.FechaInicio) as año, SUM((datediff(N,ch.FechaInicio,ch.FechaFin))) as Minutos";
            query = query + " from CarguesHoras ch , Proyectos p";
            query = query + " where ch.ProyectoId = p.ProyectoId";
            query = query + " and  ch.UsuarioId = '"+ data +"'";
            query = query + " and  MONTH(ch.FechaInicio) = "+mes+"";
            query = query + " and  year(ch.FechaInicio) = " + año + "";
            query = query + " group by p.ProyectoId , ch.ProyectoId , P.NombreProyecto , DAY(ch.FechaInicio) , MONTH(ch.FechaInicio), year(ch.FechaInicio) ) ";
            query = query + " select proyectoId , nombreProyecto ,isnull([1],0) as 'primero',isnull([2],0)as 'segundo',isnull([3],0)as 'tercero',isnull([4],0)as 'cuarto',isnull([5],0)as 'quinto',isnull([6],0)as 'sexto',isnull([7],0)as 'septimo',isnull([8],0)as 'octavo',isnull([9],0)as 'noveno',isnull([10],0)as 'decimo',isnull([11],0)as 'once',";
            query = query + " isnull([12],0)as 'doce',isnull([13],0)as 'trece',isnull([14],0)as 'catorce',isnull([15],0)as 'quince',isnull([16],0)as 'diesiceis',isnull([17],0)as 'diesiciete',isnull([18],0)as 'dieciocho',isnull([19],0)as 'diecinueve',isnull([20],0)as 'veinte',isnull([21],0)as 'veintiuno',isnull([22],0)as 'veintidos',";
            query = query + " isnull([23],0)as 'veintres',isnull([24],0)as'veintiCuatro',isnull([25],0)as 'veinticinco',isnull([26],0)as 'veintiSeis',isnull([27],0)as 'veintiSiete',isnull([28],0)as 'veintiOcho',isnull([29],0)as 'veintiNueve',isnull([30],0)as 'Trienta',isnull([31],0)as 'TrientaUno'";
            query = query + " from ReporteTiempo";
            query = query + " pivot (sum(tiempo) for dia in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) PVT";




            cmds.CommandText = query;
            cmds.CommandType = CommandType.Text;
            cmds.Connection = sql;

            try
            {

                sql.Open();
                cmds.ExecuteReader();
                sql.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmds);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string dataRsult = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return dataRsult;

            }
            catch (Exception e)
            {



                return "Problemas con la ecuacion";
            }






        }

        public string reporteTiempoMensual(string idProyecto)
        {

            SqlCommand cmdArchivos = new SqlCommand("reporteTiempoMensual", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idProyecto", idProyecto);



            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception)
            {
                cerrarConexion();
                return "";
            }


        }


        public string obtenerNombreUSuarioXid(string usuario) {
            string data = "";
            SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["ControlTiemposDbContext"].ConnectionString);
            SqlCommand cmds = new SqlCommand();
            SqlDataReader reader;
            string query = "select Nombre from Usuarios as usuario where NombreUsuario = '"+usuario+"'";

            cmds.CommandText = query;
            cmds.CommandType = CommandType.Text;
            cmds.Connection = sql;

            try
            {

                sql.Open();
                SqlDataReader rdr = cmds.ExecuteReader();
                rdr.Read();
                data = rdr["Nombre"].ToString();
                sql.Close();
              
              

                return data;
            }
            catch (Exception e)
            {
                return "Problemas con la ecuacion";
            }




        }

        public string obtenerUSuarioIDXnombre(string usuario)
        {
            string data = "";
            SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["ControlTiemposDbContext"].ConnectionString);
            SqlCommand cmds = new SqlCommand();
            SqlDataReader reader;
            string query = "select UsuarioId from Usuarios as usuario where NombreUsuario = '" + usuario + "'";

            cmds.CommandText = query;
            cmds.CommandType = CommandType.Text;
            cmds.Connection = sql;

            try
            {

                sql.Open();
                SqlDataReader rdr = cmds.ExecuteReader();
                rdr.Read();
                data = rdr["UsuarioId"].ToString();
                sql.Close();



                return data;
            }
            catch (Exception e)
            {
                return "Problemas con la ecuacion";
            }




        }


        public string UsuariosXServicioXproyecto(string idProyecto, string idSerivicio)
        {

            SqlCommand cmd = new SqlCommand("sp_amezquita_UsuariosXServicioXproyecto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProyectoId", idProyecto);
            cmd.Parameters.AddWithValue("@servicioId", idSerivicio);

            //     cmd.Parameters.AddWithValue("@IdUsuario", id);
            try
            {

                abrirConexion();
                cmd.ExecuteReader();

                cerrarConexion();
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable datasets = new DataTable();
                das.Fill(datasets);
                string data = JsonConvert.SerializeObject(datasets, Formatting.Indented);
                return data;


            }
            catch (Exception)
            {
                cerrarConexion();
                throw;
            }


        }

        public string obtenerListadoUsuariosXdirector(string idDirector, string ano) {

            SqlCommand cmdArchivos = new SqlCommand("Sp_Amezquita_ObtenerUsuariosxDirector", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idDirector", idDirector);
            cmdArchivos.Parameters.AddWithValue("@ano", ano);
            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "";
            }




        }

        public string ObtenerTiempoUsuarioDirector(string idDirector, string idUsuario , string año)

        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_ObtenerTiempoUsuarioDirector", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idDirector", idDirector);
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmdArchivos.Parameters.AddWithValue("@año", año);
            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "";
            }




        }

        public string Sp_Amezquita_ObtenerListadoUsuariosXgruposTrabajo(string idDirector,string año)
        {

            SqlCommand cmdArchivos = new SqlCommand("Sp_Amezquita_ObtenerListadoUsuariosXgruposTrabajo", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idDirector", idDirector);         
            cmdArchivos.Parameters.AddWithValue("@ano", año);
            try
            {
                
                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "";
            }




        }

        public string Sp_Amezquita_ObtenerListadoUsuariosXgrupos( string idGerente)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_obtnerlistadoUsuariosxGrupos", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idGerente", idGerente);

            try
            {

                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "";
            }




        }

        public string CambiarEstadoUsuario(string idGerente)
        {

            SqlCommand cmdArchivos = new SqlCommand("Sp_Amezquita_CambiarEstadoUsuario", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idGerente);

            try
            {

                abrirConexion();
                cmdArchivos.ExecuteNonQuery();
                cerrarConexion();
               
                return "true";

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "false";
            }




        }

        public string EditarTiempoUusario(string idUsuario, string idProyecto, int dia, int mes,int ano ) {
            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_EditarTiempoUsuario", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@proyectoId", idProyecto);
            cmdArchivos.Parameters.AddWithValue("@usuarioId", idUsuario);
            cmdArchivos.Parameters.AddWithValue("@dia", dia);
            cmdArchivos.Parameters.AddWithValue("@mes", mes);
            cmdArchivos.Parameters.AddWithValue("@ano", ano);
            try
            {

                abrirConexion();
                cmdArchivos.ExecuteNonQuery();
                cerrarConexion();
                return "";

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "";
            }




        }



        public string eliminarRegistroId(string idCargue)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_eliminarRegistroId", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idCargue", idCargue);

            try
            {

                abrirConexion();
                cmdArchivos.ExecuteNonQuery();
                cerrarConexion();

                return "true";

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "false";
            }




        }



        public string eliminarUsuariosxGrupos(string idGerente,string idUsuario)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_eliminarUsuariosxGrupos", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idGerente", idGerente);
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idUsuario);

            try
            {

                abrirConexion();
                cmdArchivos.ExecuteNonQuery();
                cerrarConexion();
                return "";

            }

            catch (Exception e)
            {
                cerrarConexion();
                return "";
            }




        }
        public string registroTiempo(string idUsuario, string observaciones , DateTime fechaInicio , DateTime fechaFin , string idActividad , string idProyecto , string idServicio)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_registroTiempo", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmdArchivos.Parameters.AddWithValue("@Observaciones", observaciones);
            cmdArchivos.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            cmdArchivos.Parameters.AddWithValue("@fechaFin", fechaFin);
            cmdArchivos.Parameters.AddWithValue("@idactividad", idActividad);
            cmdArchivos.Parameters.AddWithValue("@idproyecto", idProyecto);
            cmdArchivos.Parameters.AddWithValue("@idServicio", idServicio);


            try
            {


                abrirConexion();
                cmdArchivos.ExecuteNonQuery();
                cerrarConexion();
                return "bn";

            }

            catch (Exception)
            {
                cerrarConexion();
                return "";
            }


        }

        public string obtnerTotalHorasDia(string idUsuario,  string dia)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_ObtenerTotalHorasDia", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmdArchivos.Parameters.AddWithValue("@fecha", dia);
       


            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception)
            {
                cerrarConexion();
                return "";
            }


        }

        public string obtnerTotalLimitesDias(string idUsuario)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_amezquita_obtenerlimitesDiasUsuario", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@idUsuario", idUsuario);




            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception ex)
            {
                cerrarConexion();
                return "";
            }


        }

        public string obtenertiempoModificar(string idUsuario , string idProyecto , int dia , int mes , int ano , string idProyectoex,string proyectoO)
        {
            SqlCommand cmdArchivos;
            if (idProyecto == proyectoO)
            {
                cmdArchivos = new SqlCommand("sp_amezquita_obtenertiempoModificar", conn);
            }
            else {
                cmdArchivos = new SqlCommand("sp_amezquita_obtenertiempoModificarProyectoDiferente", conn);
            }

            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@usuarioId", idUsuario);
            cmdArchivos.Parameters.AddWithValue("@proyecto", idProyecto);
            cmdArchivos.Parameters.AddWithValue("@ano", ano);
            cmdArchivos.Parameters.AddWithValue("@mes", mes);
            cmdArchivos.Parameters.AddWithValue("@dia", dia);
            cmdArchivos.Parameters.AddWithValue("@proyectoex", idProyectoex);



            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception ex)
            {
                cerrarConexion();
                return "";
            }


        }

        public string obtenercarguesUsuarioXdia(string idUsuario, string idProyecto, int dia, int mes, int ano)
        {

            SqlCommand cmdArchivos = new SqlCommand("sp_Amezquita_obtenercarguesUsuarioXdia", conn);
            cmdArchivos.CommandType = CommandType.StoredProcedure;
            cmdArchivos.Parameters.AddWithValue("@usuarioId", idUsuario);
            cmdArchivos.Parameters.AddWithValue("@idProyecto", idProyecto);
            cmdArchivos.Parameters.AddWithValue("@ano", ano);
            cmdArchivos.Parameters.AddWithValue("@mes", mes);
            cmdArchivos.Parameters.AddWithValue("@dia", dia);




            try
            {


                abrirConexion();
                cmdArchivos.ExecuteReader();
                cerrarConexion();
                SqlDataAdapter da = new SqlDataAdapter(cmdArchivos);
                DataTable dataset = new DataTable();
                da.Fill(dataset);
                string data = JsonConvert.SerializeObject(dataset, Formatting.Indented);
                return data;

            }

            catch (Exception ex)
            {
                cerrarConexion();
                return "";
            }


        }
    }
}