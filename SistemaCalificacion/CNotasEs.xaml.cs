using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SistemaCalificacion
{
    /// <summary>
    /// Lógica de interacción para CNotasEs.xaml
    /// </summary>
    public partial class CNotasEs : Window
    {
        public CNotasEs()
        {
            InitializeComponent();
        }
        MySqlConnection conexion = ConexionDB.conexion();

        private void BtnCon_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCi.Text))
            {
                MessageBox.Show("INGRESE DATOS");
            }
            else
            {
                MySqlDataAdapter da;
                DataTable dt;

                da = new MySqlDataAdapter("SELECT alumno.Ci AS 'Nro CARNET', alumno.Apellido_paterno AS 'APELLIDOS PATERNO', alumno.Apellido_materno AS 'APELLIDOS MATERNO', alumno.Nombre AS NOMBRES," +
                        "materia.Nombre AS MATERIA," +
                        "curso.Nombre AS CURSO, seccion.Nombre AS SECCION, calificacion.Nota1 AS 'NOTA 1', calificacion.Nota2 AS 'NOTA 2', calificacion.Nota3 AS 'NOTA 3', round((calificacion.Nota1 + calificacion.Nota2 + calificacion.Nota3) / 3, 2) AS `PROMEDIO`, calificacion.descripcion AS `OBSERVACIONES` " +
                        "FROM calificacion " +
                        "INNER join alumno on calificacion.Id_Alumno = alumno.Id " +
                        "INNER JOIN materia ON calificacion.Id_Materia = materia.Id " +
                        "INNER join curso on calificacion.Id_Curso = curso.Id " +
                        "INNER JOIN seccion ON alumno.Id_Seccion = seccion.Id " +
                        "WHERE alumno.Ci = '" + Convert.ToInt32(txtCi.Text) + "'", conexion);
                dt = new DataTable();
                da.Fill(dt);
                consultaCal.ItemsSource = dt.DefaultView;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
