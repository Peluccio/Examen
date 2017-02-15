using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            textBoxEfectivo.Focus();
            //textBoxPrecio.Enabled = false;
            textBoxSubtotal.Enabled = false;
            textBoxIva.Enabled = false;
            textBoxTotal.Enabled = false;
            textBoxCambio.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxPrecio.Clear();
            textBoxCantidad.Clear();
            textBoxEfectivo.Clear();
            textBoxCambio.Clear();
            textBoxSubtotal.Clear();
            textBoxIva.Clear();
            textBoxTotal.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBoxCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Enter ||
                (e.KeyChar == 46) || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Dato no válido. Sólo se permiten números", "Nota",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            Venta operaciones = new Venta();

            //operaciones.venta();
            //operaciones.setCatetoAdyacente(Convert.ToDouble(textBoxCatetoAdyacente1.Text.ToString()));

            double num1 = 0;
            double num2 = 0;
            double total = 0;

            num1 = Double.Parse(textBoxPrecio.Text);
            num2 = Double.Parse(textBoxCantidad.Text);

            total = num1 * num2;
            textBoxTotal.Text = Convert.ToDouble(total).ToString();
        }

        private void btn500_Click(object sender, EventArgs e)
        {
            double num1 = 0;
            double num2 = 0;
            double total = 0;
            double cambio = 0;

            num1 = Double.Parse(textBoxPrecio.Text);
            num2 = Double.Parse(textBoxCantidad.Text);

            total = num1 * num2;
            cambio = 500 - total;
            
            textBoxTotal.Text = Convert.ToDouble(total).ToString();
            textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
        }
    }
}
