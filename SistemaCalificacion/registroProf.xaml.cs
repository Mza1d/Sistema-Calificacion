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
    /// Lógica de interacción para registroProf.xaml
    /// </summary>
    public partial class registroProf : Window
    {
        public registroProf()
        {
            InitializeComponent();
            mostrarProf();
            cargarDatosC();
        }

        MySqlConnection conexion = ConexionDB.conexion();

        private void mostrarProf()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM registrarprof", conexion);
            dt = new DataTable();
            da.Fill(dt);
            datosProf.ItemsSource = dt.DefaultView;
            txtCi.Focus();
        }

        public void cargarDatosC()
        {
            cbxCurso.ItemsSource = null;
            cbxCurso.Items.Clear();
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
            cargarDatosC();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnRegistroProf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ci = int.Parse(txtCi.Text);
                string nombre = txtNombres.Text;
                string apellidoPaterno = txtApaterno.Text;
                string apellidoMaterno = txtAmaterno.Text;
                string direccion = txtDireccion.Text;
                int telefono = int.Parse(txtTelf.Text);
                string email = txtEmail.Text;
                string sexo = cbxSexo.Text;
                if (ci > 0 && nombre != "" && apellidoPaterno != "" && apellidoMaterno != "" && direccion != "" && telefono > 0 && email != "" && sexo != "")
                {
                    conexion.Open();
                    //string sql = "INSERT INTO profesor(Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo) VALUES('" + txtCi.Text + "', '" + txtNombres.Text + "', '" + txtApaterno.Text + "', '" + txtAmaterno.Text + "', '" + txtDireccion.Text + "', '" + txtTelf.Text + "', '" + txtEmail.Text + "', '" + cbxSexo.SelectionBoxItem + "')";
                    string sql = "INSERT INTO profesor(Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo, Id_Curso) VALUES('" + ci + "', '" + nombre + "', '" + apellidoPaterno + "', '" + apellidoMaterno + "', '" + direccion + "', '" + telefono + "', '" + email + "', '" + sexo + "','" + Convert.ToInt32(cbxCurso.SelectedValue) + "')";

                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(sql, conexion);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("SE GURADO CON ÉXITO");
                        limpiar();
                        mostrarProf();
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
            }catch(FormatException fex)
            {
                MessageBox.Show("Datos Incorrectos\n" + fex.Message);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string ci = txtCi.Text;
            MySqlDataReader reader = null;
            string sql = "SELECT Id, Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo, Id_Curso FROM Profesor WHERE Ci LIKE '"+ci+"' LIMIT 1";
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
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch(MySqlException ex)
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
            string sql = "UPDATE profesor SET Ci='"+txtCi.Text+"', Nombre='"+txtNombres.Text+ "', Apellido_paterno='"+txtApaterno.Text+ "', Apellido_materno='"+txtAmaterno.Text+ "', Direccion='"+txtDireccion.Text+ "', Telefono='"+txtTelf.Text+ "', Email='"+txtEmail.Text+ "', Sexo='"+cbxSexo.SelectionBoxItem+ "', Id_Curso='" + Convert.ToInt32(cbxCurso.SelectedValue) + "' WHERE Id='" + txtId.Text+"'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ACTUALIZO CON ÉXITO");
                limpiar();
                mostrarProf();
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
            string sql = "DELETE FROM profesor WHERE Ci='" + txtCi.Text + "'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("SE ELIMINO CON ÉXITO");
                limpiar();
                mostrarProf();
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
