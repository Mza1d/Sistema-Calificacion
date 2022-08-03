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
    /// Lógica de interacción para registroAlumno.xaml
    /// </summary>
    public partial class registroAlumno : Window
    {
        public registroAlumno()
        {
            InitializeComponent();
            mostrarAlumnos();
            cargarDatosC();
            cargarDatosS();
        }

        MySqlConnection conexion = ConexionDB.conexion();

        private void mostrarAlumnos()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM RegistrarAlumno", conexion);
            dt = new DataTable();
            da.Fill(dt);
            datosAlumno.ItemsSource = dt.DefaultView;
            txtCi.Focus();
        }

        private void limpiar()
        {
            txtId.Text = "";
            txtCi.Text = "";
            txtNombres.Text = "";
            txtApaterno.Text = "";
            txtAmaterno.Text = "";
            txtDireccion.Text = "";
            txtTelf.Text = "";
            txtEmail.Text = "";
            cbxSexo.Text = "";
            cbxCurso.ItemsSource = null;
            cbxCurso.Items.Clear();
            cbxSeccion.ItemsSource = null;
            cbxSeccion.Items.Clear();

            cargarDatosC();
            cargarDatosS();
        }

        public void cargarDatosC()
        {
            cbxCurso.ItemsSource = null;
            cbxCurso.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Curso";
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
        private void btnRegistroA_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombres.Text))
            {
                MessageBox.Show("INGRESE DATOS");
            }
            else
            {
                int idCurso = int.Parse(cbxCurso.SelectedValue.ToString());
                int idSeccion = int.Parse(cbxSeccion.SelectedValue.ToString());

                conexion.Open();
                string sql = "INSERT INTO Alumno(Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo, Id_Curso, Id_Seccion) VALUES('" + txtCi.Text + "', '" + txtNombres.Text + "', '" + txtApaterno.Text + "', '" + txtAmaterno.Text + "', '" + txtDireccion.Text + "', '" + txtTelf.Text + "', '" + txtEmail.Text + "', '" + cbxSexo.SelectionBoxItem + "', '" + idCurso + "', '" + idSeccion + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE GURADO CON ÉXITO");
                conexion.Close();
                limpiar();
                mostrarAlumnos();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            /*menuPrincipal inicio = new menuPrincipal();
            inicio.Show();*/
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string ci = txtCi.Text;
            MySqlDataReader reader = null;
            string sql = "SELECT Id, Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo, Id_Curso, Id_Seccion FROM Alumno WHERE Ci LIKE '" + ci + "' LIMIT 1";
            conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtCi.Text = reader.GetString(1);
                        txtNombres.Text = reader.GetString(2);
                        txtApaterno.Text = reader.GetString(3);
                        txtAmaterno.Text = reader.GetString(4);
                        txtDireccion.Text = reader.GetString(5);
                        txtTelf.Text = reader.GetString(6);
                        txtEmail.Text = reader.GetString(7);
                        cbxSexo.Text = reader.GetString(8);
                        cbxCurso.SelectedValue = reader.GetString(9);
                        cbxSeccion.SelectedValue = reader.GetString(10);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            conexion.Open();
            string sql = "UPDATE Alumno SET Ci='" + txtCi.Text + "', Nombre='" + txtNombres.Text + "', Apellido_paterno='" + txtApaterno.Text + "', Apellido_materno='" + txtAmaterno.Text + "', Direccion='" + txtDireccion.Text + "', Telefono='" + txtTelf.Text + "', Email='" + txtEmail.Text + "', Sexo='" + cbxSexo.SelectionBoxItem + "', Id_Curso='"+int.Parse(cbxCurso.SelectedValue.ToString())+"', Id_Curso='"+int.Parse(cbxSeccion.SelectedValue.ToString())+"' WHERE Id='" + txtId.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ACTUALIZO CON ÉXITO");
                limpiar();
                mostrarAlumnos();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al actualizar" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            conexion.Open();
            string sql = "DELETE FROM Alumno WHERE Ci='" + txtCi.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ELIMINO CON ÉXITO");
                limpiar();
                mostrarAlumnos();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
