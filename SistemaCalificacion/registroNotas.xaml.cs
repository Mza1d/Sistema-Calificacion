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
    /// Lógica de interacción para registroNotas.xaml
    /// </summary>
    public partial class registroNotas : Window
    {
        public registroNotas()
        {
            InitializeComponent();
            mostrarNotas();
            //cargarDatosA();
            //cargarDatosCurso();
            cargarDatosC();
        }
        MySqlConnection conexion = ConexionDB.conexion();

        private void mostrarNotas()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM ingresoN", conexion);
            dt = new DataTable();
            da.Fill(dt);
            notasA.ItemsSource = dt.DefaultView;
            curso.Focus();
        }

        private void limpiar()
        {
            alumno.ItemsSource = null;
            alumno.Items.Clear();
            materia.ItemsSource = null;
            materia.Items.Clear();
            curso.ItemsSource = null;
            curso.Items.Clear();
            txtNota1.Text = "";
            txtNota2.Text = "";
            txtNota3.Text = "";
            txtDescrip.Text = "";
            txtId.Text = "";
            txtCi.Text = "";

            //cargarDatosA();
            cargarDatosC();

            //cargarDatosP();
        }
        

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void cargarDatosC()
        {
            curso.ItemsSource = null;
            curso.Items.Clear();
            alumno.ItemsSource = null;
            alumno.Items.Clear();
            materia.ItemsSource = null;
            materia.Items.Clear();

            string sql = "SELECT Id, Nombre FROM Curso";
            //conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                curso.SelectedValuePath = "Id";
                curso.DisplayMemberPath = "Nombre";
                curso.ItemsSource = dt.DefaultView;

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

        private void curso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alumno.ItemsSource = null;
            alumno.Items.Clear();

            string sql = "SELECT Id, Ci, Nombre FROM Alumno WHERE Id_Curso = '" + Convert.ToInt32(curso.SelectedValue) + "' ORDER BY Nombre ASC";
            //conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                alumno.SelectedValuePath = "Id";
                alumno.DisplayMemberPath = "Ci";
                alumno.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            materia.ItemsSource = null;
            materia.Items.Clear();

            string sql1 = "SELECT Id, Nombre FROM Materia WHERE Id_Curso = '" + Convert.ToInt32(curso.SelectedValue) + "' ORDER BY Nombre ASC";
            //conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql1, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                materia.SelectedValuePath = "Id";
                materia.DisplayMemberPath = "Nombre";
                materia.ItemsSource = dt.DefaultView;
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //string ci = txtCi.Text;
            MySqlDataReader reader = null;
            string sql = "SELECT Id, Id_Alumno, Id_Materia, Id_Curso, Nota1, Nota2, Nota3, Descripcion FROM Calificacion WHERE Id LIKE '" + txtCi.Text + "' LIMIT 1";
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
                        alumno.SelectedValue = reader.GetString(2);
                        //curso.SelectedValue = reader.GetString(1);
                        materia.SelectedValue = reader.GetString(3);
                        txtNota1.Text = reader.GetString(4);
                        txtNota2.Text = reader.GetString(5);
                        txtNota3.Text = reader.GetString(6);
                        txtDescrip.Text = reader.GetString(7);
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

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnIngresarN_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(alumno.Text))
            {
                MessageBox.Show("INGRESE DATOS");
            }
            else
            {
                try
                {
                    int idAlumno = int.Parse(alumno.SelectedValue.ToString());
                    int idMateria = int.Parse(materia.SelectedValue.ToString());
                    int idCurso = int.Parse(curso.SelectedValue.ToString());

                    conexion.Open();
                    string sql = "INSERT INTO Calificacion(Id_Alumno, Id_Materia, Id_Curso, Nota1, Nota2, Nota3, Descripcion) VALUES('" + Convert.ToInt32(alumno.SelectedValue) + "', '" + Convert.ToInt32(materia.SelectedValue) + "', '" + Convert.ToInt32(curso.SelectedValue) + "', '" + txtNota1.Text + "', '" + txtNota2.Text + "', '" + txtNota3.Text + "', '" + txtDescrip.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conexion);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("SE GURADO CON ÉXITO");
                    conexion.Close();
                    limpiar();
                    mostrarNotas();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int idMateria = int.Parse(materia.SelectedValue.ToString());
            conexion.Open();
            string sql = "UPDATE Calificacion SET Id_Alumno='" + Convert.ToInt32(curso.SelectedValue) + "', id_curso='" + Convert.ToInt32(alumno.SelectedValue) + "', Id_Materia='" + idMateria + "', Nota1='" + txtNota1.Text + "', Nota2='" + txtNota2.Text + "', Nota3='" + txtNota3.Text + "', Descripcion='" + txtDescrip.Text + "' WHERE Id='" + txtId.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ACTUALIZO CON ÉXITO");
                limpiar();
                mostrarNotas();
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
            string sql = "DELETE FROM Calificacion WHERE Id='" + txtCi.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ELIMINO CON ÉXITO");
                limpiar();
                mostrarNotas();
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
