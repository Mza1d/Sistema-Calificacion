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
    /// Lógica de interacción para menuAlumno.xaml
    /// </summary>
    public partial class menuAlumno : Window
    {
        public menuAlumno(string v)
        {
            InitializeComponent();
            mensaje.Content = "BIENVENIDO " + v;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void BntEst_Click(object sender, RoutedEventArgs e)
        {
            CNotasEs Est = new CNotasEs();
            Est.Show();
        }
    }
}
