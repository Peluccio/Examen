using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Examen
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            //
            Thread hilo = new Thread(mostrarSplash);
            hilo.Start();
            Thread.Sleep(2500);
            hilo.Abort();
        }

        public void mostrarSplash()
        {
            Splash sps = new Splash();
            sps.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
            Dispose();
        }
    }
}
