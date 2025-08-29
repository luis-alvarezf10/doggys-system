using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doggys_system
{
    public partial class venta : Form
    {
        public event Action ActData;
        public venta()
        {
            InitializeComponent();
            DataGridSelect.CellValueChanged += DataGridSelect_CellValueChanged;
            DataGridSelect.CurrentCellDirtyStateChanged += (s, e) => {
                if (DataGridSelect.IsCurrentCellDirty)
                    DataGridSelect.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void venta_Load(object sender, EventArgs e)
        {
            TxtBoxDolarBCV.Text = ConfiguracionGlobal.TasaDolarBCV.ToString("F2");
            TxtBoxDolarIntermedio.Text = ConfiguracionGlobal.TasaDolarIntermedio.ToString("F2");
            TxtBoxDolarDoggys.Text = ConfiguracionGlobal.TasaDolarDoggys.ToString("F2");
            CargarTotalDivisas();
            CargarTotalDivisasIntermedias();
            CargarTotalDivisasBCV();
            CargarTotalBolivares();
        }
        private void CargarTotalDivisas()
        {
            float total = 0;
            foreach(DataGridViewRow row in DataGridSelect.Rows)
            {
                total += Convert.ToSingle(row.Cells["totaldivisas"].Value);
            }
            TxtBoxTotalDivisas.Text = total.ToString("F2");
        }
        private void CargarTotalDivisasIntermedias()
        {
            float total = 0;
            foreach(DataGridViewRow row in DataGridSelect.Rows)
            {
                total += Convert.ToSingle(row.Cells["totalbolivares"].Value);
            }
            float TotalBolivares = total / ConfiguracionGlobal.TasaDolarIntermedio;
            TxtBoxTotalDivisasIntermedias.Text = TotalBolivares.ToString("F2");
        }
        private void CargarTotalDivisasBCV()
        {
            float total = 0;
            foreach (DataGridViewRow row in DataGridSelect.Rows)
            {
                total += Convert.ToSingle(row.Cells["totalbolivares"].Value);
            }
            float TotalBolivares = total / ConfiguracionGlobal.TasaDolarBCV;
            TxtBoxTotalDivisasBCV.Text = TotalBolivares.ToString("F2");
        }
        private void CargarTotalBolivares()
        {
            float total = 0;
            foreach (DataGridViewRow row in DataGridSelect.Rows)
            {
                total += Convert.ToSingle(row.Cells["totalbolivares"].Value);
            }
            TxtBoxTotalBolivares.Text = total.ToString("F2");
        }
        private void DataGridProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void TxtBoxTotaDivisas_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TxtBoxDolarBCV_TextChanged(object sender, EventArgs e)
        {

        }

        private void NumeroTxtBox_TextChanged(object sender, EventArgs e)
        {

            if(NumeroTxtBox.Text.Length == 11)
            {
                BuscarCliente(NumeroTxtBox.Text);
            }
        }
        bool clienteEncontrado = true;
        private async void BuscarCliente(string numero)
        {
            try
            {
                conexion Conecta = new conexion();
                MySqlConnection connection = Conecta.getConexion();
                Conecta.AbrirConexion();
                string query = "SELECT nombre FROM clientes WHERE numero = @numero";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Agregar el parámetro para la consulta
                    command.Parameters.AddWithValue("@numero", numero);

                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        NombreTxtBox.Text = result.ToString(); // Nombre encontrado
                    }
                    else
                    {
                        clienteEncontrado = false;
                        NombreTxtBox.Clear(); // Limpiar si no se encontró el cliente
                    }
                }

                Conecta.CerrarConexion(); // Asegúrate de cerrar la conexión
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el cliente: " + ex.Message);
            }

        }
        List<float> preciosCosto = new List<float>();
        public void CargarOrden(List<Producto> dpedido)
        {
            preciosCosto.Clear();
            foreach (var producto in dpedido)
            {
                DataGridSelect.Columns[3].ValueType = typeof(Image);
                int n = DataGridSelect.Rows.Add();
                DataGridSelect.Rows[n].Cells[0].Value = producto.id;
                DataGridSelect.Rows[n].Cells[1].Value = producto.codigo;
                DataGridSelect.Rows[n].Cells[2].Value = producto.descripcion;
                if (producto.imagen is byte[] imageBytes)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        DataGridSelect.Rows[n].Cells[3].Value = Image.FromStream(ms);
                    }
                }
                imagen.ImageLayout = DataGridViewImageCellLayout.Zoom;
                DataGridSelect.Rows[n].Cells[4].Value = producto.cantidad;
                DataGridSelect.Rows[n].Cells[5].Value = producto.pVenta;
                DataGridSelect.Rows[n].Cells[6].Value = ConfiguracionGlobal.TasaDolarDoggys * producto.pVenta;
                DataGridSelect.Rows[n].Cells[7].Value = Convert.ToInt32(DataGridSelect.Rows[n].Cells[4].Value) * Convert.ToSingle(DataGridSelect.Rows[n].Cells[5].Value);
                DataGridSelect.Rows[n].Cells[8].Value = Convert.ToSingle(DataGridSelect.Rows[n].Cells[7].Value) * ConfiguracionGlobal.TasaDolarDoggys;
                preciosCosto.Add(producto.pCosto);
                DataGridSelect.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DataGridSelect.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                DataGridSelect.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridSelect.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void DataGridSelect_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // columna cantidad
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            { 
                int nuevaCantidad;
                if (int.TryParse(DataGridSelect.Rows[e.RowIndex].Cells[4].Value?.ToString(), out nuevaCantidad))
                {
                    string codigo = DataGridSelect.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    conexion conecta = new conexion();
                    MySqlConnection connection = conecta.getConexion();
                    conecta.AbrirConexion();
                    try
                    {
                        string query = "SELECT cantidad FROM productos WHERE codigo = @codigo";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@codigo", codigo);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int cantidadRecogida = reader.GetInt32("cantidad");
                                    if (nuevaCantidad > cantidadRecogida)
                                    {
                                        MessageBox.Show("Error, no hay suficiente stock disponible de este articulo \n" +
                                            "Cantidad disponible: " + cantidadRecogida, "Introduce una cantidad menor");
                                        // pone el focus en celda 0 donde se encuentra el id.
                                        DataGridSelect.CurrentCell = DataGridSelect.Rows[e.RowIndex].Cells[0];
                                        nuevaCantidad = 1;
                                        DataGridSelect.Rows[e.RowIndex].Cells[4].Value = nuevaCantidad;
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error", ex.Message);
                    }
                    float precio = Convert.ToSingle(DataGridSelect.Rows[e.RowIndex].Cells[5].Value?.ToString());
                    DataGridSelect.Rows[e.RowIndex].Cells[6].Value = Convert.ToSingle(ConfiguracionGlobal.TasaDolarDoggys) * precio;
                    DataGridSelect.Rows[e.RowIndex].Cells[7].Value = nuevaCantidad * precio;
                    float preciototal = Convert.ToSingle(DataGridSelect.Rows[e.RowIndex].Cells[7].Value?.ToString());
                    DataGridSelect.Rows[e.RowIndex].Cells[8].Value = Convert.ToSingle(ConfiguracionGlobal.TasaDolarDoggys) * preciototal;
                    CargarTotalDivisas();
                    CargarTotalDivisasIntermedias();
                    CargarTotalDivisasBCV();
                    CargarTotalBolivares();
                }
            }
        }

        private void VenderBtn_Click(object sender, EventArgs e)
        {
            conexion Conecta = new conexion();
            MySqlConnection connection = Conecta.getConexion();
            Conecta.AbrirConexion();
            try
            {
                if (!clienteEncontrado)
                {
                    string queryCliente = "INSERT INTO clientes (nombre, numero) VALUES (@nombre, @numero)";
                    MySqlCommand cmdCliente = new MySqlCommand(queryCliente, connection);
                    cmdCliente.Parameters.AddWithValue("@nombre", NombreTxtBox.Text.Trim());
                    cmdCliente.Parameters.AddWithValue("@numero", NumeroTxtBox.Text.Trim());
                    cmdCliente.ExecuteNonQuery();
                }
                string queryVenta = "INSERT INTO ventas (telefono, cliente, fecha, totaldivisas, totalbolivares, metodopago, referencia) VALUES (@telefono, @cliente, @fecha, @totaldivisas, @totalbolivares, @metodopago, @referencia)";
                MySqlCommand cmdVenta = new MySqlCommand(queryVenta, connection);
                cmdVenta.Parameters.AddWithValue("@telefono", NumeroTxtBox.Text.Trim());
                cmdVenta.Parameters.AddWithValue("@cliente", NombreTxtBox.Text.Trim());
                cmdVenta.Parameters.AddWithValue("@fecha", DateTimePicker.Value);

                if (float.TryParse(TxtBoxTotalDivisas.Text.Trim(), out float totalDivisas))
                    cmdVenta.Parameters.AddWithValue("@totaldivisas", totalDivisas);
                else
                    throw new Exception("Valor de Total Divisas no es válido.");

                if (float.TryParse(TxtBoxTotalBolivares.Text.Trim(), out float totalBolivares))
                    cmdVenta.Parameters.AddWithValue("@totalBolivares", totalBolivares);
                else
                    throw new Exception("Valor de Total Bolivares no es válido.");

                cmdVenta.Parameters.AddWithValue("@metodopago", CmbBoxPayment.Text.Trim());
                cmdVenta.Parameters.AddWithValue("@referencia", referenciatxtbox.Text.Trim());
                cmdVenta.ExecuteNonQuery();
                int ventaId = Convert.ToInt32(cmdVenta.LastInsertedId);
                for (int fila = 0; fila < DataGridSelect.Rows.Count - 1; fila++)
                {
                    string queryProductoVenta = "INSERT INTO detallesventa (ventaid, codigoproducto, cantidad, pcostodivisas, pventabolivares, pventadivisas) " +
                        "VALUES (@ventaId, @codigoproducto, @cantidad, @preciocostodivisas, @precioventabolivares, @precioventadivisa)";
                    MySqlCommand cmdProductoVenta = new MySqlCommand(queryProductoVenta, connection);

                    cmdProductoVenta.Parameters.AddWithValue("@ventaId", ventaId); // Cambia este valor según corresponda
                    cmdProductoVenta.Parameters.AddWithValue("@codigoproducto", DataGridSelect.Rows[fila].Cells[1].Value.ToString());
                    cmdProductoVenta.Parameters.AddWithValue("@cantidad", int.Parse(DataGridSelect.Rows[fila].Cells[4].Value.ToString()));
                    cmdProductoVenta.Parameters.AddWithValue("@precioventadivisa", float.Parse(DataGridSelect.Rows[fila].Cells[7].Value.ToString()));
                    cmdProductoVenta.Parameters.AddWithValue("@precioventabolivares", float.Parse(DataGridSelect.Rows[fila].Cells[8].Value.ToString()));

                    // Calcula el totalCosto y lo asigna al parámetro de costo en divisas
                    float totalCosto = preciosCosto[fila] * int.Parse(DataGridSelect.Rows[fila].Cells[4].Value.ToString());
                    cmdProductoVenta.Parameters.AddWithValue("@preciocostodivisas", totalCosto);
                    cmdProductoVenta.ExecuteNonQuery(); 

                    // Ejecuta la consulta
                    string queryReducirInventario = "UPDATE productos SET cantidad = cantidad - @cantidad WHERE codigo = @codigoproducto";
                    MySqlCommand cmdReducirInventario = new MySqlCommand(queryReducirInventario, connection);
                    cmdReducirInventario.Parameters.AddWithValue("@cantidad", int.Parse(DataGridSelect.Rows[fila].Cells[4].Value.ToString()));
                    cmdReducirInventario.Parameters.AddWithValue("@codigoproducto", DataGridSelect.Rows[fila].Cells[1].Value.ToString());
                    cmdReducirInventario.ExecuteNonQuery();
                }
                Conecta.CerrarConexion();
                MessageBox.Show("Venta registrada. :)", "Notificacion Doggys Systems");
                ActData?.Invoke();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            
        }
    }
}
