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
    /// Lógica de interacción para CrearCuenta.xaml
    /// </summary>
    public partial class CrearCuenta : Window
    {
        public CrearCuenta()
        {
            InitializeComponent();
            txtNombre.Focus();
        }
        MySqlConnection conexion = ConexionDB.conexion();

        public void limpiar()
        {
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            txtPassword.Password = "";
            cbxTipo.Text = "";
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            txtNombre.Focus();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnIngresarN_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsuario.Text))
            {
                MessageBox.Show("INGRESE DATOS");
            }
            else
            {
                conexion.Open();
                string sql = "INSERT INTO Usuario(Nombre, Apellidos, Email, Nombre_Usuario, Contrasena, rol) VALUES('" + txtNombre.Text + "', '" + txtApellidos.Text + "', '" + txtEmail.Text + "', '" + txtUsuario.Text + "', '" + txtPassword.Password + "', '" + cbxTipo.SelectionBoxItem + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("GRACIAS POR REGISTRARSE");
                conexion.Close();
                limpiar();
            }
        }
    }
}
