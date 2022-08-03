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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;

namespace SistemaCalificacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MySqlConnection conexion = ConexionDB.conexion();

        public void sesion(string usuarios, string contraseña)
        {
            try
            {
                
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Nombre,rol, contrasena FROM Usuario WHERE Nombre_usuario = @usuario AND Contrasena = @password ", conexion);
                cmd.Parameters.AddWithValue("usuario", usuarios);
                cmd.Parameters.AddWithValue("password", contraseña);
                utils.hashPassword(txtPassword.Password);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    if (dt.Rows[0][1].ToString() == "1") //Admin
                    {
                        new menuPrincipal(dt.Rows[0][0].ToString()).Show();
                        
                    }
                    else if (dt.Rows[0][1].ToString() == "0") //alumno
                    {
                        new menuAlumno(dt.Rows[0][0].ToString()).Show();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Incorrecto");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            sesion(this.txtUsuario.Text, this.txtPassword.Password);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void crear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CrearCuenta cuenta = new CrearCuenta();
            cuenta.Show();
        }
        public class utils
        {
            public static string hashPassword(string password)
            {
                SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

                byte[] password_bytes = Encoding.ASCII.GetBytes(password);
                byte[] encripted_bytes = sha1.ComputeHash(password_bytes);
                return Convert.ToBase64String(encripted_bytes);
            }
        }
    }
}
