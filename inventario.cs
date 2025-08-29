using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Guna.UI2.WinForms.Suite;
using MySql.Data.MySqlClient;

namespace doggys_system
{
    public partial class inventario : Form
    {
        public inventario()
        {
            InitializeComponent();
        }

        private void Añadir_btn_Click(object sender, EventArgs e)
        {
            ConfiguracionGlobal.valida = 1;
            registrarPoducto mostrar = new registrarPoducto();
            mostrar.OnProductAdded += CargarDatos;
            mostrar.ShowDialog();
        }
        private void CargarDatos()
        {
            try
            {
                conexion Conecta = new conexion();
                MySqlConnection connection = Conecta.getConexion();
                Conecta.AbrirConexion();
                string query = "SELECT * FROM productos";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    acomodarTabla(dt);
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
        private void main_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void acomodarTabla(DataTable dt)
        {

            DataGrid.DataSource = dt;

            // Configuraciones de la DataGrid
            DataGrid.Columns["id"].Width = 50;
            DataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            DataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid.Columns["codigo"].Width = 80;
            DataGrid.Columns["descripcion"].Width = 100;
            DataGrid.Columns["pcosto"].Width = 80;
            DataGrid.Columns["pventa"].Width = 80;
            DataGrid.Columns["cantidad"].Width = 80;
            DataGrid.Columns["imagen"].Width = 150;
            DataGrid.Columns["tipo"].Width = 120;
            DataGrid.Columns["fecha"].Width = 120;
        }
        private  void BuscarProducto(string descripcion)
        {
            try {
                conexion Conecta = new conexion();
                MySqlConnection connection = Conecta.getConexion();
                Conecta.AbrirConexion();
                string query = "SELECT * FROM productos WHERE descripcion LIKE @descripcion";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@descripcion", "%" + descripcion + "%");
                    //command.Parameters.AddWithValue("@pageSize", pageSize);
                    //command.Parameters.AddWithValue("@offset", offset);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    acomodarTabla(dt);
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

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if(SearchBox.Text.Length > 0)
            {
                BuscarProducto(SearchBox.Text);
            }
            else
            {
                CargarDatos();
            }
        }

        private void TopicComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TopicComboBox.SelectedIndex > 0) 
            {
                try
                {
                    conexion Conecta = new conexion();
                    MySqlConnection connection = Conecta.getConexion();
                    Conecta.AbrirConexion();
                    string tipoSelecionado = TopicComboBox.SelectedItem.ToString();
                    string query = "SELECT * FROM productos WHERE tipo LIKE @tipo";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tipo", "%" + tipoSelecionado + "%");
                       // command.Parameters.AddWithValue("@pageSize", pageSize);
                       // command.Parameters.AddWithValue("@offset", offset);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        acomodarTabla(dt);
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
            else
            {
                CargarDatos();
            }
        }

        private void VentaBtn_Click(object sender, EventArgs e)
        {
            venta dventa = new venta();
            List<Producto> productosSeleccionados = new List<Producto>();
            foreach (DataGridViewRow selectedRow in DataGrid.Rows)
            {
                Producto dpedido = new Producto();
                if (selectedRow.Selected)
                {
                    if (Convert.ToInt32(selectedRow.Cells["cantidad"].Value) >= 1)
                    {
                        dpedido.id = Convert.ToInt32(selectedRow.Cells["id"].Value);
                        dpedido.codigo = selectedRow.Cells["codigo"].Value.ToString();
                        dpedido.descripcion = selectedRow.Cells["descripcion"].Value.ToString();
                        dpedido.cantidad = 1;
                        dpedido.imagen = (byte[])selectedRow.Cells["imagen"].Value;
                        dpedido.pCosto = Convert.ToSingle(selectedRow.Cells["pcosto"].Value);
                        dpedido.pVenta = Convert.ToSingle(selectedRow.Cells["pventa"].Value);

                        productosSeleccionados.Add(dpedido);
                    }
                    else
                    {
                        MessageBox.Show("El stock de uno de los productos esta en cero... ");
                        return;
                    }
                }
            }
            dventa.CargarOrden(productosSeleccionados);
            dventa.ActData += CargarDatos;
            dventa.ShowDialog();
        }

        private void EliminarBtn_Click(object sender, EventArgs e)
        {
            string CodigoSelecionado = null;
            string descripcionProducto = null;
            if (DataGrid.SelectedRows.Count != 1)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            conexion conecta = new conexion();
            MySqlConnection conection = conecta.getConexion();
            conecta.AbrirConexion();
            foreach (DataGridViewRow selectedRow in DataGrid.Rows)
            {
                if(selectedRow.Selected)
                {
                    descripcionProducto = selectedRow.Cells["descripcion"].Value.ToString();
                    DialogResult result = MessageBox.Show("Estas seguro de borrar este producto. " + descripcionProducto.ToString(), "confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(result == DialogResult.OK)
                    {
                        CodigoSelecionado = selectedRow.Cells["codigo"].Value.ToString();
                        string query = "DELETE FROM productos WHERE codigo = @codigo";
                        using (MySqlCommand command = new MySqlCommand(query, conection))
                        {
                            command.Parameters.AddWithValue("@codigo", CodigoSelecionado);
                            command.ExecuteNonQuery();
                        }
                        conecta.CerrarConexion();
                        MessageBox.Show("Este arcticulo ya fue eliminado: ", "Confirmacion eliminada");
                        CargarDatos();
                    }
                    else
                    {
                        descripcionProducto = null;
                    }

                }
            }
        }

        private void ModificarBtn_Click(object sender, EventArgs e)
        {
            int idSeleccionado = 0;
            foreach (DataGridViewRow row in DataGrid.Rows)
            {
                if (row.Selected)
                {
                    idSeleccionado = Convert.ToInt32(row.Cells["id"].Value.ToString());
                    registrarPoducto modifica = new registrarPoducto();
                    modifica.ModificarProducto(idSeleccionado);
                    modifica.ShowDialog();
                }
            }
            CargarDatos();
        }
    }
}
