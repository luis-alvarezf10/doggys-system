using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doggys_system
{
    public partial class reporte : Form
    {
        public reporte()
        {
            InitializeComponent();
        }

        private void reporte_Load(object sender, EventArgs e)
        {
            RangoCmbBox.SelectedIndex = 0;

        }
        private void CargarDatos()
        {
            try
            {
                conexion Conecta = new conexion();
                MySqlConnection connection = Conecta.getConexion();
                Conecta.AbrirConexion();
                string query = "SELECT * FROM ventas ORDER BY fecha ASC";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Datagrid.DataSource = dt;
                }

                Conecta.CerrarConexion();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en la consulta SQL: " + ex.Message);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
                this.Close();
            }
        }

        private void CargarBtn_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = DateTimeInicio.Value.Date;
            DateTime fechaFin = DateTimeFin.Value.Date;
            if(fechaInicio > fechaFin)
            {
                MessageBox.Show("Error, rango de fecha invalido...");
                return;
            }
            try
            {
                conexion Conecta = new conexion();
                MySqlConnection connection = Conecta.getConexion();
                Conecta.AbrirConexion();

                // Consulta SQL para obtener las ventas entre las fechas especificadas
                string query = "SELECT * FROM ventas WHERE fecha BETWEEN @fechaInicio AND @fechaFin ORDER BY fecha ASC";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Pasa los parámetros de fecha a la consulta
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", fechaFin);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Datagrid.DataSource = dt;
                }

                Conecta.CerrarConexion();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en la consulta SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }

        private void Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RangoCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dias = 0;
            switch (RangoCmbBox.SelectedIndex)
            {
                case 0:
                    dias = 7;
                    break;
                case 1:
                    dias = 30;
                    break;
                case 2:
                    dias = 90;
                    break;
            }
            DateTime fechaInicio = DateTime.Today.AddDays(-dias);
            DateTime fechaFin = DateTime.Today;

            // Realiza la consulta usando los parámetros de fecha
            try
            {
                conexion Conecta = new conexion();
                MySqlConnection connection = Conecta.getConexion();
                Conecta.AbrirConexion();

                // Modifica la consulta para incluir el rango de fechas
                string query = "SELECT * FROM ventas WHERE fecha BETWEEN @fechaInicio AND @fechaFin ORDER BY fecha DESC";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", fechaFin);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Datagrid.DataSource = dt;
                }

                Conecta.CerrarConexion();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en la consulta SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }
    }
}
