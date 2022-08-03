using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaCalificacion
{
    class ConexionDB
    {
        public static MySqlConnection conexion()
        {
            /*string servidor = "sql10.freesqldatabase.com";
            string bd = "sql10510369";
            string usuario = "sql10510369";
            string password = "QVQlLppkpZ";*/
            string servidor = "localhost";
            string bd = "bd_sistemacalificacion";
            string usuario = "Mario";
            string password = "2021";

            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id= " + usuario + "; Password=" + password + "";
            //string cadenaConexion = "Data Source=DESKTOP-1OGT27K; Initial Catalog=SISTEMA; Integrated Security=True;";
            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);

                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
