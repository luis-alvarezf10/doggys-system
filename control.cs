using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doggys_system
{
    internal class control:conexion
    {
        private Producto dproducto = new Producto();
        public List<object> consulta(string dato)
        {
            MySqlDataReader reader;
            List<Object> lista = new List<Object>();
            string query;
            if (dato == null)
            {
                query = "SELECT id,codigo, descripcion, pcosto, pventa, cantidad, imagen, fecha";
            }
            else
            {
                query = "SELECT id, codigo, descripcion, pcosto, pventa, cantidad,imagen, fecha FROM productos WHERE codigo LIKE '%"+dato+"%'" + 
                    "OR descripcion  LIKE '%"+dato+ "%' OR pventa  LIKE '%"+dato+"%' ORDER BY id ASC" ;
            }
            conexion Conecta = new conexion();
            MySqlConnection connection = Conecta.getConexion();
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    dproducto.id = int.Parse(reader.GetString("id"));
                    dproducto.codigo = reader.GetString("codigo");
                    dproducto.descripcion = reader.GetString("descripcion");
                    dproducto.pCosto = float.Parse(reader.GetString("pcosto"));
                    dproducto.pVenta = float.Parse(reader.GetString("pventa"));
                    dproducto.cantidad = int.Parse(reader.GetString("cantidad"));
                    dproducto.imagen = (byte[])reader["imagen"];
                    lista.Add(dproducto);
                }
            } catch (MySqlException ex){
                Console.WriteLine(ex.Message.ToString());
            }
            return lista;



        }
    }
}
