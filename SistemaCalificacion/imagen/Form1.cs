using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaCalificacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mostrarAlumnos();
            cargarDatosC();
            cargarDatosS();
            txtCi.Focus();
        }

        MySqlConnection conexion = ConexionDB.conexion();

        private void mostrarAlumnos()
        {
            MySqlDataAdapter da;
            DataTable dt;

            da = new MySqlDataAdapter("SELECT *FROM Alumno", conexion);
            dt = new DataTable();
            da.Fill(dt);
            datosAlumno.DataSource = dt.DefaultView;
            txtCi.Focus();
        }

        private void limpiar()
        {
            txtCi.Text = "";
            txtNombre.Text = "";
            txtApaterno.Text = "";
            txtAmaterno.Text = "";
            txtDireccion.Text = "";
            txtTelf.Text = "";
            txtEmail.Text = "";
            cbxSexo.Text = "";
            cbxCurso.DataSource = null;
            cbxCurso.Items.Clear();
            cbxSeccion.DataSource = null;
            cbxSeccion.Items.Clear();

            cargarDatosC();
            cargarDatosS();
        }

        public void cargarDatosS()
        {
            cbxSeccion.DataSource = null;
            cbxSeccion.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Seccion ORDER BY Nombre ASC";
            conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxSeccion.ValueMember = "Id";
                cbxSeccion.DisplayMember = "Nombre";
                cbxSeccion.DataSource = dt;
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

        public void cargarDatosC()
        {
            cbxCurso.DataSource = null;
            cbxCurso.Items.Clear();
            string sql = "SELECT Id, Nombre FROM Curso ORDER BY Nombre ASC";
            conexion.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxCurso.ValueMember = "Id";
                cbxCurso.DisplayMember = "Nombre";
                cbxCurso.DataSource = dt;
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnRegistrarA_Click(object sender, EventArgs e)
        {
            int idCurso = int.Parse(cbxCurso.SelectedValue.ToString());
            int idSeccion = int.Parse(cbxSeccion.SelectedValue.ToString());

            conexion.Open();
            string sql = "INSERT INTO Alumno(Ci, Nombre, Apellido_paterno, Apellido_materno, Direccion, Telefono, Email, Sexo, Id_Curso, Id_Seccion) VALUES('" + txtCi.Text + "', '" + txtNombre.Text + "', '" + txtApaterno.Text + "', '" + txtAmaterno.Text + "', '" + txtDireccion.Text + "', '" + txtTelf.Text + "', '" + txtEmail.Text + "', '" + cbxSexo.SelectedItem + "', '" + idCurso + "', '" + idSeccion + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conexion);
            cmd.ExecuteNonQuery();
            MessageBox.Show("SE GURADO CON ÉXITO");
            conexion.Close();
            limpiar();
            mostrarAlumnos();
        }
    }
}
