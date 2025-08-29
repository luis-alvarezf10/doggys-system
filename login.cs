using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static doggys_system.Program;

namespace doggys_system
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (UserTxtBox.Text == "" || PasswordTxtBox.Text == "")
            {
                MessageBox.Show("Error, uno o mas campos estan vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (UserTxtBox.Text == "admin" && PasswordTxtBox.Text == "admin")
            {
                this.DialogResult = DialogResult.OK;  // Marca el login como exitoso
                this.Close();  // Cierra el formulario de login
            }
            else
            {
                MessageBox.Show("Error, usuario no encontrado...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UserTxtBox.Text = "";
                PasswordTxtBox.Text = "";
            }
        }

        private void PasswordTxtBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void ShowPasswordBtn_Click(object sender, EventArgs e)
        {
            if (PasswordTxtBox.PasswordChar == '*')
            {
                ShowPasswordBtn.BackgroundImage = Image.FromFile(@"C:\Users\luisa\Documents\doggys_system\doggys_system\images\ojos-cruzados.png");
                PasswordTxtBox.PasswordChar = '\0';
            }
            else
            {
                ShowPasswordBtn.BackgroundImage = Image.FromFile(@"C:\Users\luisa\Documents\doggys_system\doggys_system\images\ojo.png");
                PasswordTxtBox.PasswordChar = '*';

            }
        }

        private async void login_Load(object sender, EventArgs e)
        {
            // this.DialogResult = DialogResult.OK;  // Marca el login como exitoso
            await CargarTasaDolarAlInicio();
            //this.Close();  // Cierra el formulario de login
        }
        private async Task CargarTasaDolarAlInicio()
        {
            TASA divisa = new TASA();
            await divisa.CargarValorDolar(); // Espera a que se complete la carga del valor del dólar
            ConfiguracionGlobal.TasaDolarBCV = Convert.ToSingle(divisa.ValorBCV); // Guarda la tasa de dólar en la configuración global
            ConfiguracionGlobal.TasaDolarIntermedio = Convert.ToSingle(divisa.Intermedio);
            ConfiguracionGlobal.TasaDolarDoggys = Convert.ToSingle(divisa.DolarDoggys);
        }
    }
}
