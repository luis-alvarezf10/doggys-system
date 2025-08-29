using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;

namespace doggys_system
{
    public class TASA
    {
        private static int cont = 0;
        public decimal ValorBCV;
        public int Intermedio;
        public int DolarDoggys;
        public async Task<decimal> ObtenerValorDolarOficial()
        {
            string url = "https://www.bcv.org.ve/";
            using (HttpClient client = new HttpClient()){
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string pageContents = await response.Content.ReadAsStringAsync();

                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(pageContents);
                var dolarNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='dolar']/div[@class='field-content']/div[@class='row recuadrotsmc']/div[@class='col-sm-6 col-xs-6 centrado']/strong");
                if(dolarNode != null)
                {
                    string dolarText = dolarNode.InnerText.Trim();
                    if(decimal.TryParse(dolarText, out decimal valorDolar))
                    {
                        return valorDolar;
                    }
                    else
                    {
                        throw new Exception("Error al convertir el valor del dolar a decimal.");
                    }
                }
                else
                {
                    throw new Exception("No se pudo encontrar el valor del dolar en la pagina");
                }
            }
        }
        public async Task CargarValorDolar(int margen = 6)
        {
            try
            {
                // Cargar en segundo plano
                var valorDolarTask = ObtenerValorDolarOficial();
                decimal valorDolar = await valorDolarTask;
                ValorBCV = valorDolar;
                DolarDoggys = Convert.ToInt32(ValorBCV) + margen;
                Intermedio = Convert.ToInt32(ValorBCV + (margen / 2));
                cont = 0;
            }
            catch (Exception ex)
            {
                cont += 1;
                if(cont == 1)
                {
                    conexion Conecta = new conexion();
                    MySqlConnection connection = Conecta.getConexion();
                    Conecta.AbrirConexion();
                    string query = "SELECT dolardoggys FROM valordivisa LIMIT 1";
                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        object result = command.ExecuteScalar();
                        if(result != null)
                        {
                            DolarDoggys = Convert.ToInt32(result);
                        }
                    }
                    MessageBox.Show("Error al obtener el valor del dólar, se esta utilizando valor dolar predeterminado para doggys system, puede cambiarlo en configuracion..." +
                        "\n error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

    }
}
