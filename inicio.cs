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
    public partial class inicio : Form
    {
        public inicio()
        {
            InitializeComponent();
        }

        private void inicio_Load(object sender, EventArgs e)
        {
            reloj.Interval = 1000;
            reloj.Start();
            CargarFecha();
            ValorDolarBCV.Text = ConfiguracionGlobal.TasaDolarBCV.ToString("F2"); ;
        }
        private void CargarFecha()
        {
            string dia = DateTime.Now.ToString("dddd");
            string ndia = DateTime.Now.ToString("dd");
            string mes = DateTime.Now.ToString("MMMM");
            string ano = DateTime.Now.ToString("yyyy");
            Fecha.Text = Convert.ToString("" + dia + ", " + ndia + " de " + mes + " de " + ano);
        }

        private void reloj_Tick(object sender, EventArgs e)
        {
            Hora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void ValorDolarBCV_Click(object sender, EventArgs e)
        {

        }
    }
}
