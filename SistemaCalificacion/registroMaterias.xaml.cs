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
    /// Lógica de interacción para registroMaterias.xaml
    /// </summary>
    public partial class registroMaterias : Window
    {
        public registroMaterias()
        {
            InitializeComponent();
            mostrarMaterias();
            cargarDatosC();
            //cargarDatosP();
        }

        MySqlConnection conexion = ConexionDB.conexion();
        
        
        private void mostrarMaterias()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM RegistroProf", conexion);
            dt = new DataTable();
            da.Fill(dt);
            datosMaterias.ItemsSource = dt.DefaultView;
            txtCodigo.Focus();
        }

        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombres.Text = "";
            cbxCurso.ItemsSource = null;
            cbxCurso.Items.Clear();
            cbxProfesor.ItemsSource = null;
            cbxProfesor.Items.Clear();

            cargarDatosC();

            //cargarDatosP();
        }
        public void cargarDatosC()
        {
            cbxCurso.ItemsSource = null;
            cbxCurso.Items.Clear();
            cbxProfesor.ItemsSource = null;
            cbxProfesor.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Curso";
            //conexion.Open();
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

        /*public void cargarDatosP()
        {
            cbxProfesor.ItemsSource = null;
            cbxProfesor.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Profesor ORDER BY Nombre ASC";
            conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxProfesor.SelectedValuePath = "Id";
                cbxProfesor.DisplayMemberPath = "Nombre";
                cbxProfesor.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }*/


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //menuPrincipal inicios = new menuPrincipal();
        }

        private void btnRegistroProf_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("INGRESE DATOS");
            }
            else
            {
                try
                {
                    string codigo = txtCodigo.Text;
                    string nombre = txtNombres.Text;

                    int idCurso = int.Parse(cbxCurso.SelectedValue.ToString());
                    int idProfesor = int.Parse(cbxProfesor.SelectedValue.ToString());
                    if (nombre != "")
                    {
                        conexion.Open();
                        //string sql = "INSERT INTO profesor(Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo) VALUES('" + txtCi.Text + "', '" + txtNombres.Text + "', '" + txtApaterno.Text + "', '" + txtAmaterno.Text + "', '" + txtDireccion.Text + "', '" + txtTelf.Text + "', '" + txtEmail.Text + "', '" + cbxSexo.SelectionBoxItem + "')";
                        string sql = "INSERT INTO Materia(Codigo, Nombre, Id_Curso, Id_Profesor) VALUES('" + codigo + "','" + nombre + "', '" + idCurso + "', '" + idProfesor + "')";

                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(sql, conexion);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("SE GURADO CON ÉXITO");
                            limpiar();
                            mostrarMaterias();
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error al guardar" + ex.Message);
                        }
                        finally
                        {
                            conexion.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar todos lo campos\n");
                    }
                }
                catch (FormatException fex)
                {
                    MessageBox.Show("Datos Incorrectos\n" + fex.Message);
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //string codigo = txtCodigo.Text;
            MySqlDataReader reader = null;
            string sql = "SELECT Id, Codigo, Nombre, Id_Curso, Id_Profesor FROM Materia WHERE Codigo LIKE '" + txtCodigo.Text + "' LIMIT 1";
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
                        txtCodigo.Text = reader.GetString(1);
                        txtNombres.Text = reader.GetString(2);
                        //cbxCurso.SelectedValue = reader.GetString(3);
                        //cbxProfesor.SelectedValue = reader.GetString(4);
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
            string sql = "UPDATE materia SET codigo='" + txtCodigo.Text + "', Nombre='" + txtNombres.Text + "', Id_Curso='" + Convert.ToInt32(cbxCurso.SelectedValue) + "', Id_Profesor='" + int.Parse(cbxProfesor.SelectedValue.ToString()) + "' WHERE Id='" + txtId.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ACTUALIZO CON ÉXITO");
                limpiar();
                mostrarMaterias();
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
            string sql = "DELETE FROM materia  WHERE Codigo='" + txtCodigo.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ELIMINO CON ÉXITO");
                limpiar();
                mostrarMaterias();
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

        private void cbxCurso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxProfesor.ItemsSource = null;
            cbxProfesor.Items.Clear();
            //int idCurso = int.Parse(cbxCurso.SelectedValue.ToString());

            string sql = "SELECT Id, Nombre, Ci FROM Profesor WHERE Id_Curso = '" + Convert.ToInt32(cbxCurso.SelectedValue) + "' ORDER BY Ci ASC";
            //conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxProfesor.SelectedValuePath = "Id";
                cbxProfesor.DisplayMemberPath = "Ci";
                cbxProfesor.ItemsSource = dt.DefaultView;
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
    }
}
