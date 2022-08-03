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

namespace SistemaCalificacion
{
    /// <summary>
    /// Lógica de interacción para menuPrincipal.xaml
    /// </summary>
    public partial class menuPrincipal : Window
    {
        public menuPrincipal()
        {
        }

        public menuPrincipal(string v)
        {
            InitializeComponent();
            mensaje.Content = "Bienvenido " + v;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            registroAlumno alumno = new registroAlumno();
            alumno.Show();
        }

        private void btnRegistroM_Click(object sender, RoutedEventArgs e)
        {
            registroMaterias materias = new registroMaterias();
            materias.Show();
            //this.Close();
        }

        private void btnRegistrarP_Click(object sender, RoutedEventArgs e)
        {
            registroProf prof = new registroProf();
            prof.Show();
        }

        private void btnLista_Click(object sender, RoutedEventArgs e)
        {
            ListaAlumnos lista = new ListaAlumnos();
            lista.Show();
        }

        private void btnRegistroN_Click(object sender, RoutedEventArgs e)
        {
            registroNotas notas = new registroNotas();
            notas.Show();
        }

        private void btnCnotas_Click(object sender, RoutedEventArgs e)
        {
            ConsultaNotas cn = new ConsultaNotas();
            cn.Show();
        }
    }
}
