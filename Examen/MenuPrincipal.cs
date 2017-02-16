using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            textBoxPrecio.Enabled = false;
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
            textBoxCantidad.Focus();

            /*DataSet datos = new DataSet();
            ConsultaTabla("producto", "producto_id");
            dataGridView1.DataSource = datos.Tables["producto"];*/
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
            try
            {
                /*Venta operaciones = new Venta();
                Producto operaciones = new Producto();

                //operaciones.venta();
                //operaciones.setCatetoAdyacente(Convert.ToDouble(textBoxCatetoAdyacente1.Text.ToString()));

                double num1 = 0;
                double num2 = 0;
                double total = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);

                total = num1 * num2;
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
               
                //operaciones.setPrecio(Convert.ToDouble(textBoxPrecio.Text.ToString())); */
            }
            catch (Exception ex) { }
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

        public DataSet buscarProducto(string consulta, string tabla)
        {
            DataBase db = new DataBase();
            DataSet conjuntoDatos = new DataSet();
            SqlCommand command = new SqlCommand(consulta, DataBase.conexion);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataBase.conexion.Open();
            adapter.Fill(conjuntoDatos, tabla);
            DataBase.conexion.Close();

            return conjuntoDatos;
        }

        private void comboBoxProducto_Click(object sender, EventArgs e)
        {
            Producto operaciones = new Producto();
            DataBase db = new DataBase();
            comboBoxProducto.Items.Clear();

            try
            {
                DataSet ds = new DataSet();
                ds = buscarProducto("SELECT producto_nombre FROM producto ", "producto");

                foreach (DataRow fila in ds.Tables["producto"].Rows)
                {
                    comboBoxProducto.Items.Add(fila["producto_nombre"].ToString());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DataBase.conexion.Close();
            }
        }

        private void comboBoxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = buscarProducto("SELECT producto_precio FROM producto WHERE producto_nombre ='" + comboBoxProducto.Text + "'", "producto" );

                foreach (DataRow row in ds.Tables["producto"].Rows)
                {
                    textBoxPrecio.Text = row["producto_precio"].ToString();
             
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DataBase.conexion.Close();
            }
        }



        /*Método que ejecutará la consulta de los registros de una tabla.
        public DataSet ConsultaTabla(string cliente, string codigo_cli)
        {
            DataSet dS = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string consulta = "SELECT * FROM " + cliente + " ORDER BY " + codigo_cli + " ASC";
            
            SqlCommand command = new SqlCommand(consulta, DataBase.conexion);
            adapter.SelectCommand = command;
            DataBase.conexion.Open();
            adapter.Fill(dS, cliente);
            DataBase.conexion.Close();
            
            return dS;
        } */


    }
}
