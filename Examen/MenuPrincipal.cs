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
using System.Windows.Forms.VisualStyles;

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
            textBoxProducto.Enabled = false;
            textBoxTotalAPagar.Enabled = false;
           

        }
        //Tabla para el gridView
        DataTable dt = new DataTable(); 

      

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxPrecio.Clear();
            textBoxCantidad.Clear();
            textBoxEfectivo.Clear();
            textBoxCambio.Clear();
            textBoxSubtotal.Clear();
            textBoxIva.Clear();
            textBoxTotal.Clear();
            textBoxProducto.Clear();
            textBoxTotalAPagar.Clear();
            textBoxCantidad.Focus();
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
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
                ds = buscarProducto("SELECT producto_precio, producto_nombre FROM producto WHERE producto_nombre ='" + comboBoxProducto.Text + "'", "producto" );

                foreach (DataRow row in ds.Tables["producto"].Rows)
                {
                    textBoxPrecio.Text = row["producto_precio"].ToString();
                    textBoxProducto.Text = row["producto_nombre"].ToString();

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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
           
            
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;
                double efectivo = 0;
                double cambio = 0;
                double subtotal = 0;


                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);
                efectivo = Double.Parse(textBoxEfectivo.Text);

                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = efectivo - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            DataRow row = dt.NewRow(); //Creas un regístro.
            row["Producto"] =  textBoxProducto.Text; //Se añade un valor.
            row["Cantidad"] =  textBoxCantidad.Text;
            row["Precio"] =  textBoxPrecio.Text;
            row["Total"] =  textBoxTotal.Text;
            dt.Rows.Add(row); //Se añade el registro a la tabla.
            dataGridView1.DataSource = dt; //Se añade la tabla al datagrid.            
            dataGridView1.Update(); //Se actualiza.  

            double sumatoriaTotal = 0;
            int counter;

<<<<<<< HEAD
            for (counter = 0; counter < (dataGridView1.Rows.Count); counter++)
            {
                if (dataGridView1.Rows[counter].Cells["Total"].Value
                    != null)
                {
                    if (dataGridView1.Rows[counter].
                        Cells["Total"].Value.ToString().Length != 0)
                    {
                        sumatoriaTotal += double.Parse(dataGridView1.Rows[counter].
                            Cells["Total"].Value.ToString());
                    }
                }
            }

            textBoxTotalAPagar.Text = sumatoriaTotal.ToString();
=======

            textBoxPrecio.Clear();
>>>>>>> origin/master
            textBoxCantidad.Clear();
            textBoxEfectivo.Clear();
            textBoxCambio.Clear();
            textBoxSubtotal.Clear();
            textBoxIva.Clear();            
            textBoxProducto.Clear();
            textBoxCantidad.Focus();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Producto"); //Se crean las columnas.
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio");            
            //dt.Columns.Add("Subtotal");
            //dt.Columns.Add("IVA");
            dt.Columns.Add("Total");
            dataGridView1.DataSource = dt;
        }

        private void btn500_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;                
                double cambio = 0;
                double subtotal = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);
                
                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = 500 - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void btn200_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;
                double cambio = 0;
                double subtotal = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);

                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = 200 - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void btn100_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;
                double cambio = 0;
                double subtotal = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);

                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = 100 - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;
                double cambio = 0;
                double subtotal = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);

                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = 50 - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;
                double cambio = 0;
                double subtotal = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);

                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = 20 - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = 0;
                double num2 = 0;
                double total = 0;
                double cambio = 0;
                double subtotal = 0;

                num1 = Double.Parse(textBoxPrecio.Text);
                num2 = Double.Parse(textBoxCantidad.Text);

                subtotal = num1 * num2;
                total = subtotal * 1.16;
                cambio = 10 - total;

                textBoxIva.Text = Convert.ToDouble(subtotal * 0.16).ToString();
                textBoxSubtotal.Text = Convert.ToDouble(subtotal).ToString();
                textBoxTotal.Text = Convert.ToDouble(total).ToString();
                textBoxCambio.Text = Convert.ToDouble(cambio).ToString();
            }
            catch (Exception ex) { }
        }

        private void textBoxEfectivo_KeyPress(object sender, KeyPressEventArgs e)
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

    }

    }
