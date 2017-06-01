using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Amezquita.ControlTiempos
{
    public class Conexion
    {

        public SqlConnection conn = new SqlConnection();

        public  Conexion()
        {
           
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ControlTiemposDbContext"].ConnectionString);


        }

        public void abrirConexion()
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            {

                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {

                    abrirConexion();
                }

            }


        }

        public void cerrarConexion()
        {

            if (conn.State == ConnectionState.Open)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }


        }

    }
}