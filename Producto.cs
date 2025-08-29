using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doggys_system
{
    public class Producto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public float pCosto {  get; set; }
        public float pVenta { get; set; }
        public int cantidad { get; set; }
        public string tipo {  get; set; }
        public byte[] imagen { get; set; }
        public DateTime fecha { get; set; }
    }
}
