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
    /// Lógica de interacción para ListaAlumnos.xaml
    /// </summary>
    public partial class ListaAlumnos : Window
    {
        public ListaAlumnos()
        {
            InitializeComponent();
            cargarLista();
            cargarDatosC();
            cargarDatosS();
        }

        MySqlConnection conexion = ConexionDB.conexion();

        public void cargarLista()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM ListaAlumnos", conexion);
            dt = new DataTable();
            da.Fill(dt);
            listaA.ItemsSource = dt.DefaultView;
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
                MessageBox.Show("Error al cargar: " + ex.Message);
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
            string sql = "SELECT Id, Nombre FROM Seccion";
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
                MessageBox.Show("Error al cargar: " + ex.Message);
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

        private void btnLista_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(cbxCurso.Text))
            {
                MessageBox.Show("INGRESE DATOS");
            }
            else
            {
                try
                {
                    MySqlDataAdapter da;
                    DataTable dt;
                    conexion.Open();

                    da = new MySqlDataAdapter("SELECT alumno.Ci AS 'Nro CARNET', alumno.Apellido_paterno AS 'APELLIDOS PATERNO', alumno.Apellido_materno AS 'APELLIDOS MATERNO', alumno.Nombre AS 'NOMBRE', alumno.Direccion AS 'DIRECCION', alumno.Telefono AS 'CELULAR', alumno.Email AS 'CORREO ELECTRONICO', alumno.Sexo AS 'GENERO', curso.Nombre AS 'CURSO', seccion.Nombre AS 'PARALELO'" +
                            "FROM alumno " +

                            "INNER JOIN curso ON alumno.Id_Curso = curso.Id " +
                            "INNER JOIN seccion ON alumno.Id_Seccion = seccion.Id " +
                            "WHERE curso.Id = '" + Convert.ToInt32(cbxCurso.SelectedValue) + "' AND seccion.Id = '" + Convert.ToInt32(cbxSeccion.SelectedValue) + "' ORDER BY alumno.Apellido_paterno ASC ", conexion);
                    dt = new DataTable();
                    da.Fill(dt);
                    verLista.ItemsSource = dt.DefaultView;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: \n" + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            registroAlumno alumno = new registroAlumno();
            alumno.Show();
        }
    }
}
