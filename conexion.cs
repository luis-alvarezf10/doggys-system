using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doggys_system
{
    internal class conexion
    {
        private MySqlConnection conex;
        private string server = "localhost";
        private string database = "doggys";
        private string user = "root";
        private string password = "";
        private string cadenaConexion;
        public conexion()
        {
            cadenaConexion = "Database =" + database + ";" +
                "DataSource =" + server + ";" +
                "User Id =" + user + ";" +
                "Password =" + password;
        }

        public MySqlConnection getConexion()
        {
            if (conex == null)
            {
                conex = new MySqlConnection(cadenaConexion);
                conex.Open();
            }
            return conex;
        }

        public void AbrirConexion()
        {
            if (conex.State == System.Data.ConnectionState.Closed)
            {
                conex.Open();
            }
        }

        public void CerrarConexion()
        {
            if (conex.State == System.Data.ConnectionState.Open)
            {
                conex.Close();
            }
        }
    }
}
