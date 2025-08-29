using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace doggys_system
{
    public partial class registrarPoducto : Form
    {
        private Producto dproducto = new Producto();
        public event Action OnProductAdded;
        public static int numero;
        public registrarPoducto()
        {
            InitializeComponent();
            codeTextBox.KeyDown += new KeyEventHandler(codeTextBox_KeyDown);
        }
        private void PBproduct_Click(object sender, EventArgs e)
        {

        }
        private bool CodigoProductoExiste(string codigo)
        {
            try
            {
                conexion Conecta = new conexion();
                using (MySqlConnection connection = Conecta.getConexion())
                {
                    string query = "SELECT COUNT(*) FROM productos WHERE codigo = @codigo";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codigo", codigo);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // Si el count es mayor que 0, significa que el código ya existe
                        return count > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en la consulta SQL: " + ex.Message);
                return true; // Si hay un error en la consulta, consideramos que el producto ya existe por seguridad.
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            conexion Conecta = new conexion();
            MySqlConnection connection = Conecta.getConexion();
            string codigo = codeTextBox.Text.Trim();
            if (ValidarDatos())
            {
                CargarDatosProducto();
                try
                {
                    string query = "INSERT INTO productos (codigo, descripcion, pcosto, pventa, cantidad, imagen, tipo, fecha) " +
                           "VALUES (@codigo, @descripcion, @costo, @venta, @cantidad, @imagen, @tipo, @fecha)";

                    MySqlCommand comando = new MySqlCommand(query, connection);
                    comando.Parameters.AddWithValue("@codigo", dproducto.codigo);
                    comando.Parameters.AddWithValue("@descripcion", dproducto.descripcion);
                    comando.Parameters.AddWithValue("@costo", dproducto.pCosto);
                    comando.Parameters.AddWithValue("@venta", dproducto.pVenta);
                    comando.Parameters.AddWithValue("@cantidad", dproducto.cantidad);
                    comando.Parameters.AddWithValue("@imagen", dproducto.imagen);
                    comando.Parameters.AddWithValue("@tipo", dproducto.tipo);
                    comando.Parameters.AddWithValue("@fecha", dproducto.fecha);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Guardado...");
                    OnProductAdded?.Invoke();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error, al hacer el registro" + ex.Message);
                }
            }
        }
        private bool ValidarDatos()
        {
            if (codeTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Falta el codigo del producto");
                return false;
            }
            if (codeTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Falta el nombre del producto");
                return false;
            }
            if (!float.TryParse(prizeCoastTxtBox.Text.Trim(), out float pCosto))
            {
                MessageBox.Show("Falta el precio costo del producto");
                return false;
            }
            if (!float.TryParse(prizeSellTxtBox.Text.Trim(), out float pVenta))
            {
                MessageBox.Show("Falta el precio venta del producto");
                return false;
            }
            if (!int.TryParse(QuantityTxtBox.Text.Trim(), out int cantidad) || int.Parse(QuantityTxtBox.Text.Trim()) < 1)
            {
                MessageBox.Show("Falta la cantidad solicitada del producto...");
                return false;
            }
            if (CmbBoxType.SelectedIndex == -1)
            {
                MessageBox.Show("Falta el tipo de artículo del producto");
                return false;
            }
            return true;
        }
        private Image RedimensionarImagen(Image imgOriginal, int ancho, int alto)
        {
            var nuevaImagen = new Bitmap(ancho, alto);
            using (var g = Graphics.FromImage(nuevaImagen))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgOriginal, 0, 0, ancho, alto);
            }
            return nuevaImagen;
        }

        private void LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagenes|*.jpg; *.png";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // puede ser mypcitures
            ofd.Title = "Seleccionar imagen";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image imgOriginal = Image.FromFile(ofd.FileName);
                PBproduct.Image = RedimensionarImagen(imgOriginal, 150, 150);
            }
        }
        private void CargarDatosProducto()
        {
            dproducto.codigo = codeTextBox.Text.Trim();
            dproducto.descripcion = productTextBox.Text.Trim();
            dproducto.pCosto = float.Parse(prizeCoastTxtBox.Text.Trim());
            dproducto.pVenta = float.Parse(prizeSellTxtBox.Text.Trim());
            dproducto.cantidad = int.Parse(QuantityTxtBox.Text.Trim());
            dproducto.tipo = CmbBoxType.Text.Trim();
            dproducto.imagen = ImageToByteArray(PBproduct.Image);
            dproducto.fecha = DateTime.Now;
        }
        private byte[] ImageToByteArray(Image image)
        {
           if (image == null)
           {
               return null;
           }

           using (MemoryStream dMemmoryStream = new MemoryStream())
           {
               image.Save(dMemmoryStream, ImageFormat.Png);
               return dMemmoryStream.ToArray();
           }
        }
        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            codeTextBox.Text = codeTextBox.Text.ToUpper();
            codeTextBox.SelectionStart = codeTextBox.Text.Length;
            conexion Conecta = new conexion();
            MySqlConnection connection = Conecta.getConexion();
            Conecta.AbrirConexion();
            string codigo = codeTextBox.Text.Trim();
            if (codeTextBox.Text.Length > 0 && ConfiguracionGlobal.valida != 0)
            {
                if (CodigoProductoExiste(codigo))
                {
                    SaveBtn.Visible = false;   
                    ModBtn.Visible = false;
                    AlmacenarBtn.Visible = true;
                    MessageBox.Show("El producto ya se encuentra registrado, se habilitará unicamente la casilla 'Cantidad'...", "Informacion", MessageBoxButtons.OK);
                    codeTextBox.Text = codigo;
                    codeTextBox.Enabled = false;
                    try
                    {
                        string queryBuscar = "SELECT descripcion, pcosto, pventa, imagen, tipo FROM productos WHERE codigo = @codigo";
                        using (MySqlCommand commandBuscar = new MySqlCommand(queryBuscar, connection))
                        {
                            commandBuscar.Parameters.AddWithValue("@codigo", codigo);
                            using (MySqlDataReader reader = commandBuscar.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    productTextBox.Text = reader.GetString("descripcion");
                                    prizeCoastTxtBox.Text = reader.GetFloat("pcosto").ToString();
                                    prizeSellTxtBox.Text = reader.GetFloat("pventa").ToString();
                                    QuantityTxtBox.Text = " ";
                                    if (reader.HasRows)
                                    {
                                        byte[] imageBytes = (byte[])reader["imagen"];
                                        using (MemoryStream ms = new MemoryStream(imageBytes))
                                        {
                                            PBproduct.Image = Image.FromStream(ms);
                                        }
                                    }
                                    CmbBoxType.Text = reader.GetString("tipo");
                                }
                            }
                            codeTextBox.Enabled = false;
                            productTextBox.Enabled = false;
                            prizeCoastTxtBox.Enabled = false;
                            prizeSellTxtBox.Enabled = false;
                            CmbBoxType.Enabled = false;
                            LoadImage.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void codeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.G) // Detecta Alt + G
            {
                e.SuppressKeyPress = true; // Previene el sonido de "error" al presionar Alt + G
                string nuevoCodigo = GenerarCodigo();
                codeTextBox.Text = nuevoCodigo; // Asigna el nuevo código al TextBox
                codeTextBox.SelectionStart = codeTextBox.Text.Length; // Coloca el cursor al final
            }
        }

        private string GenerarCodigo()
        {
            Random random = new Random();
            int nuevoCodigo;

            do
            {
                nuevoCodigo = random.Next(1, 1000); // Cambia 1000 al límite superior que desees
            }
            while (CodigoProductoExiste($"DS-{nuevoCodigo:D3}")); // Verifica si el código ya existe

            return $"DS-{nuevoCodigo:D3}"; // Retornar el nuevo código en el formato DS-###
        }
        private void productTextBox_TextChanged(object sender, EventArgs e)
        {
            productTextBox.Text = productTextBox.Text.ToUpper();
            productTextBox.SelectionStart = productTextBox.Text.Length;
        }

        private void registrarPoducto_Load(object sender, EventArgs e)
        {

        }
        static int IdProducto;
        string codigoProducto;
        public void ModificarProducto(int idSeleccionado)
        {
            ConfiguracionGlobal.valida = 0;
            this.Text = "Modificar Producto";
            Title.Text = "Modificar Producto";
            ModBtn.Visible = true;
            SaveBtn.Visible = false;
            IdProducto = idSeleccionado;
            conexion conecta = new conexion();
            MySqlConnection connection = conecta.getConexion();
            conecta.AbrirConexion();
            string query = "SELECT codigo, descripcion, pcosto, pventa, cantidad, tipo, imagen " +
                "FROM productos WHERE id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", int.Parse(idSeleccionado.ToString()));
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            codigoProducto = reader.GetString("codigo");
                            codeTextBox.Text = reader.GetString("codigo");
                            productTextBox.Text = reader.GetString("descripcion");
                            prizeCoastTxtBox.Text = reader.GetFloat("pcosto").ToString();
                            prizeSellTxtBox.Text = reader.GetFloat("pventa").ToString();
                            QuantityTxtBox.Text = reader.GetInt32("cantidad").ToString();
                            CmbBoxType.Text = reader.GetString("tipo");
                            if (reader.HasRows)
                            {
                                byte[] imageBytes = (byte[])reader["imagen"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    PBproduct.Image = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                    conecta.CerrarConexion();
                }
                catch
                {
                    MessageBox.Show("No se pudo conseguir el producto seleccionado... ");
                }
            }
        }

        private void ModBtn_Click(object sender, EventArgs e)
        {
            if(codigoProducto != codeTextBox.Text)
            {
                while (CodigoProductoExiste(codeTextBox.Text))
                {
                    MessageBox.Show("Ya existe este codigo en el inventario...");
                    codeTextBox.Focus();
                    return;
                }
            }
            if (ValidarDatos())
            {
                CargarDatosProducto();
                conexion conecta = new conexion();
                MySqlConnection connection = conecta.getConexion();
                conecta.AbrirConexion();
                try
                {
                    string query = "UPDATE productos SET codigo = @codigo, descripcion = @descripcion, pcosto = @costo, pventa = @venta, cantidad = @cantidad, imagen = @imagen, tipo = @tipo, fecha = @fecha " +
                        "WHERE id = @id";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codigo", dproducto.codigo);
                        command.Parameters.AddWithValue("@descripcion", dproducto.descripcion);
                        command.Parameters.AddWithValue("@costo", dproducto.pCosto);
                        command.Parameters.AddWithValue("@venta", dproducto.pVenta);
                        command.Parameters.AddWithValue("@cantidad", dproducto.cantidad);
                        command.Parameters.AddWithValue("@imagen", dproducto.imagen);
                        command.Parameters.AddWithValue("@tipo", dproducto.tipo);
                        command.Parameters.AddWithValue("@fecha", dproducto.fecha);
                        command.Parameters.AddWithValue("@id", IdProducto);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Inventario actualizado...");
                    this.Close();
                    OnProductAdded?.Invoke();
                }
                catch (Exception ex)
                { 
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AlmacenarBtn_Click(object sender, EventArgs e)
        {
            conexion Conecta = new conexion();
            MySqlConnection connection = Conecta.getConexion();
            Conecta.AbrirConexion();
            string codigo = codeTextBox.Text.Trim();
            int cantidad = Convert.ToInt32(QuantityTxtBox.Text.Trim());
            CargarDatosProducto();
            try
            {
                string queryAumenta = "UPDATE productos SET cantidad = cantidad + @cantidad WHERE codigo = @codigo";
                using (MySqlCommand command = new MySqlCommand(queryAumenta, connection))
                {
                    command.Parameters.AddWithValue("@cantidad", cantidad);
                    command.Parameters.AddWithValue("@codigo", codigo);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Stock aumentado...");
                OnProductAdded?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
        }
    }

}
