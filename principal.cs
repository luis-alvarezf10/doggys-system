using Guna.UI2.WinForms;
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
    public partial class principal : Form
    {
        public principal()
        {
            InitializeComponent();
        }
        public void abrirForm(object Interfaz)
        {
            if(this.container.Controls.Count > 0)
                this.container.Controls.RemoveAt(0);
            Form mostrar = Interfaz as Form;
            mostrar.TopLevel = false;
            mostrar.Dock = DockStyle.Fill;
            this.container.Controls.Add(mostrar);
            this.container.Tag = mostrar;
            mostrar.Show();

        }
        private void principal_Load(object sender, EventArgs e)
        {
            abrirForm(new inicio());
        }

        private void apagar()
        {
            InicioBtn.Checked = false;
            InventarioBtn.Checked = false;
            PagoBtn.Checked = false;  
            ReporteBtn.Checked = false;
        }
        private void container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SalirBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InicioBtn_Click(object sender, EventArgs e)
        {
            apagar();
            InicioBtn.Checked = true;
            abrirForm(new inicio());
        }

        private void InventarioBtn_Click(object sender, EventArgs e)
        {
            apagar();
            InventarioBtn.Checked = true;
            abrirForm(new inventario());
        }

        private void PagoBtn_Click(object sender, EventArgs e)
        { /*
            apagar();
            PagoBtn.Checked=true;
            abrirForm(new pago());
            */
        }

        private void ReporteBtn_Click(object sender, EventArgs e)
        {
            apagar();
            ReporteBtn.Checked=true;
            abrirForm(new reporte());
        }
    }
}
