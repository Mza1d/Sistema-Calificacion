using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaCalificacion
{
    /// <summary>
    /// Lógica de interacción para ConsultaNotas.xaml
    /// </summary>
    public partial class ConsultaNotas : Window
    {
        public ConsultaNotas()
        {
            InitializeComponent();
            Notas();
            cargarDatosC();
            cargarDatosS();
        }

        MySqlConnection conexion = ConexionDB.conexion();

        private void Notas()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM Calificacion", conexion);
            dt = new DataTable();
            da.Fill(dt);
            consultaN.ItemsSource = dt.DefaultView;
        }
        public void cargarDatosC()
        {
            cbxCurso.ItemsSource = null;
            cbxCurso.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Curso";
            conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxCurso.SelectedValuePath = "Id";
                cbxCurso.DisplayMemberPath = "Nombre";
                cbxCurso.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public void cargarDatosS()
        {
            cbxSeccion.ItemsSource = null;
            cbxSeccion.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Seccion ORDER BY Nombre ASC";
            conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxSeccion.SelectedValuePath = "Id";
                cbxSeccion.DisplayMemberPath = "Nombre";
                cbxSeccion.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCon_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(cbxCurso.Text))
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
                        "WHERE curso.Id = '" + Convert.ToInt32(cbxCurso.SelectedValue) + "' AND seccion.Id = '" + Convert.ToInt32(cbxSeccion.SelectedValue) + "'", conexion);
                dt = new DataTable();
                da.Fill(dt);
                consultaCal.ItemsSource = dt.DefaultView;
            }
        }
    }
}
