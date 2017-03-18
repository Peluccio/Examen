using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Examen
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal(Usuario u)
        {
            InitializeComponent();
            textBoxEfectivo.Focus();
            textBoxPrecio.Enabled = false;
            checkBoxTicket.Checked = true;

            textBoxCantidad.Text = "1";
            textBoxEfectivo.Text = "0";
            lblFecha.Text = System.DateTime.Now.ToShortDateString();

            // Encontrar id de última venta
            Venta venta = new Venta();
            int folio = venta.findLast() + 1;
            lblFolio.Text = folio.ToString();

            // Set usuario
            this.usuario = u;
            lblNomUs.Text = this.usuario.getNombre() + " " + usuario.getApellidos();
            lblPuestoUs.Text = this.usuario.getTipo();
            if (this.usuario.getTipo() == "vendedor")
            {
                tabControl1.Controls.Remove(tabPage1);
                tabControl1.Controls.Remove(tabPage2);
            }

            // Llenar el combobox de registro de usuarios y de estado
            cmbTipo.Items.Add("vendedor");
            cmbTipo.Items.Add("administrador");
            cmbTipo.SelectedIndex = 0;

            cmbEstado.Items.Add("activo");
            cmbEstado.Items.Add("inactivo");
            cmbEstado.SelectedIndex = 0;
        }

        //Tabla para el gridView de productos
        DataTable dt = new DataTable();

        //Tabla para el gridView de usuarios
        DataTable dtUsuarios = new DataTable();

        // Total de la venta
        double totalVenta = 0;
        double subtotalVenta = 0;
        Venta ventaPrint = new Venta();
        Usuario usuario = new Usuario();

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Está seguro de que desea cerrar su sesión? Si no ha cobrado la venta, la información de la misma se perderá.", "Está a punto de cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                Dispose();
                Login ventana = new Login();
                ventana.ShowDialog();
            }
        }

        private void comboBoxProducto_Click(object sender, EventArgs e)
        {
            Producto operaciones = new Producto();
            DataBase db = new DataBase();
            comboBoxProducto.Items.Clear();
            btnAgregar.Enabled = true;

            if (comboBoxProducto.Text == "") btnAgregar.Enabled = false;
            else btnAgregar.Enabled = true;

            try
            {
                Producto producto = new Producto();
                DataSet listaProductos = new DataSet();
                listaProductos = producto.findAll();

                foreach (DataRow row in listaProductos.Tables["producto"].Rows)
                {
                    comboBoxProducto.Items.Add(row["producto_nombre"].ToString());
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
                if (comboBoxProducto.Text == "") btnAgregar.Enabled = false;
                else btnAgregar.Enabled = true;

                Producto producto = new Producto();
                bool productExists = producto.findByName(comboBoxProducto.Text);
                if(productExists)
                {
                    textBoxPrecio.Text = producto.getPrecio().ToString();
                    textBoxCodigo.Text = producto.getId().ToString();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DataBase.conexion.Close();
                textBoxCantidad.Focus();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.actualizarRegistro();

            dataGridView1.DataSource = dt; //Se añade la tabla al datagrid.            
            dataGridView1.Update(); //Se actualiza.  

            textBoxCantidad.Focus();
            this.actualizarTicket(false);
        }

        public void actualizarRegistro()
        {
            if (dt.Rows.Count > 0)
            {
                bool exists = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Producto"].ToString() == comboBoxProducto.Text)
                    {
                        exists = true;
                        // Ya existe el producto en la lista, sumar la cantidad
                        double rowCantidad = 0; // Convertir cantidad en la fila
                        Double.TryParse(row["Cantidad"].ToString(), out rowCantidad);

                        double txtCantidad = 0; // Convertir cantidad del textbox
                        Double.TryParse(textBoxCantidad.Text, out txtCantidad);

                        double rowPrecio = 0; // Convertir el total de la fila
                        Double.TryParse(row["Precio"].ToString(), out rowPrecio);

                        //Actualizar la cantidad en la lista
                        row["Cantidad"] = rowCantidad + txtCantidad;
                        row["Total"] = (rowCantidad + txtCantidad) * rowPrecio;

                        break;
                    } 
                }
                if(!exists)
                {
                    this.agregarNuevoRegistro();
                }
            } else
            {
                this.agregarNuevoRegistro();
            }
            
        }

        public void agregarNuevoRegistro()
        {
            // Calcular precio * cantidad
            double precio = 0, cantidad = 0;
            Double.TryParse(textBoxPrecio.Text, out precio);
            Double.TryParse(textBoxCantidad.Text, out cantidad);

            DataRow r = dt.NewRow(); //Creas un regístro.
            r["Producto"] = comboBoxProducto.Text; //Se añade un valor.
            r["Cantidad"] = textBoxCantidad.Text;
            r["Precio"] = textBoxPrecio.Text;
            r["Total"] = cantidad * precio;
            r["Codigo"] = textBoxCodigo.Text;
            dt.Rows.Add(r); //Se añade el registro a la tabla.
        }

        public void actualizarTicket(bool nuevaVenta)
        {
            double st = 0; // Variable para acumular el subtotal de la venta
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string sbt = row["Total"].ToString();
                    double s = 0;
                    Double.TryParse(sbt, out s);
                    this.subtotalVenta = s;
                    st += s;
                } catch(Exception ex)
                {
                    // Atrapa la excepción al eliminar una fila
                }
                
            }

            // Calcular IVA
            double iva = st * 0.16;

            // Llenar los label del ticket
            lblSubtotal.Text = "$" + Math.Round(st, 2, MidpointRounding.AwayFromZero).ToString();
            lblIva.Text = "$" + Math.Round(iva, 2, MidpointRounding.AwayFromZero).ToString();
            lblTotal.Text = "$" + Math.Round(st + iva, 2, MidpointRounding.AwayFromZero).ToString();

            if(this.subtotalVenta + iva > 0) btnCobrar.Enabled = true;
            else btnCobrar.Enabled = false;
            this.subtotalVenta = st;
            this.totalVenta = st + iva;
            this.calcularCambio();

            if (nuevaVenta)
            {
                // Encontrar id de última venta
                Venta venta = new Venta();
                int folio = venta.findLast() + 1;
                lblFolio.Text = folio.ToString();
            }
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Producto"); //Se crean las columnas.
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio", typeof(double));
            dt.Columns.Add("Total");
            dt.Columns.Add("Codigo");
            dataGridView1.DataSource = dt;
            timerHora.Start();

            // Crear columnas para DataGrid de usuarios
            dtUsuarios.Columns.Add("Código (ID)");
            dtUsuarios.Columns.Add("Nombre");
            dtUsuarios.Columns.Add("Apellidos");
            dtUsuarios.Columns.Add("RFC");
            dtUsuarios.Columns.Add("Dirección");
            dtUsuarios.Columns.Add("Ciudad");
            dtUsuarios.Columns.Add("Teléfono");
            dtUsuarios.Columns.Add("Tipo");
            dtUsuarios.Columns.Add("Contraseña");
            dtUsuarios.Columns.Add("Estado");
            dgUsuarios.DataSource = dtUsuarios;
        }

        private void btn500_Click(object sender, EventArgs e)
        {
            textBoxEfectivo.Text = "500";
        }

        private void btn200_Click(object sender, EventArgs e)
        {
            textBoxEfectivo.Text = "200";
        }

        private void btn100_Click(object sender, EventArgs e)
        {
            textBoxEfectivo.Text = "100";
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            textBoxEfectivo.Text = "50";
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            textBoxEfectivo.Text = "20";
        }

        private void btn1000_Click(object sender, EventArgs e)
        {
            textBoxEfectivo.Text = "1000";
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

        private void textBoxEfectivo_TextChanged(object sender, EventArgs e)
        {
            this.calcularCambio();
        }

        private void calcularCambio()
        {
            try
            {
                double efRecib = 0;

                Double.TryParse(textBoxEfectivo.Text, System.Globalization.NumberStyles.Any,CultureInfo.GetCultureInfo("en-US"), out efRecib);
                Console.WriteLine(efRecib);

                double cambio = efRecib - this.totalVenta;

                if(cambio < 0)
                {
                    lblEfRecib.Text = "No recibido";
                    lblCambio.Text = "$0.00";
                } else
                {
                    lblEfRecib.Text = "$" + textBoxEfectivo.Text;
                    lblCambio.Text = "$" + Math.Round(cambio, 2, MidpointRounding.ToEven);
                }
                
            }
            catch (Exception)
            {

            }
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            string hora = "";
            if (System.DateTime.Now.Hour < 10) hora += "0" + System.DateTime.Now.Hour.ToString();
            else hora += System.DateTime.Now.Hour.ToString();
            hora += ":";
            if (System.DateTime.Now.Minute < 10) hora += "0" + System.DateTime.Now.Minute.ToString();
            else hora += System.DateTime.Now.Minute.ToString();

            //lblHora.Text = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString();
            lblHora.Text = hora;
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            double efectivo = 0;
            Double.TryParse(textBoxEfectivo.Text, out efectivo);

            if(efectivo - this.totalVenta > 0)
            {
                // Crear objeto de venta y guardarlo
                Venta venta = new Venta();
                venta.setSubtotal(this.subtotalVenta);
                venta.setTotal(this.totalVenta);
                venta.setUsuarioId(this.usuario.getId());
                bool result = venta.create();

                Console.WriteLine(result);
                if(result)
                {
                    // Agregar productos a la venta
                    foreach(DataRow row in dt.Rows)
                    {
                        string idString = row["Codigo"].ToString();
                        string cantString = row["Cantidad"].ToString();

                        bool res = venta.addProduct(Convert.ToInt32(idString), Convert.ToDouble(cantString));
                        Console.WriteLine(res);
                    }

                    MessageBox.Show("Venta registrada con éxito", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
                    if(checkBoxTicket.Checked)
                    {
                        this.ventaPrint = venta;
                        // Generar ticket
                        printDocument.Print();
                    }

                    this.clearAll();
                }
            } else
            {
                MessageBox.Show("La cantidad en efectivo no es suficiente para pagar", "Pago insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font consolas = new Font("Consolas", 10, FontStyle.Regular);
            Font consolasBold = new Font("Consolas", 10, FontStyle.Bold);
            Font consolasGrande = new Font("Consolas", 14, FontStyle.Regular);

            string logo = Environment.CurrentDirectory + "\\images\\logo_panaderia12.png";
            // x y width height

            // Dibujar logo
            e.Graphics.DrawImage(Image.FromFile(logo), 50, 50, 120, 120);

            // Escribir dirección del negocio
            e.Graphics.DrawString("Av. Chapultepec No. 4387", consolas, Brushes.Black, 180, 70);
            e.Graphics.DrawString("Colonia Americana", consolas, Brushes.Black, 180, 90);
            e.Graphics.DrawString("Guadalajara, Jalisco", consolas, Brushes.Black, 180, 110);
            e.Graphics.DrawString("Tel: (33) 3614 9800", consolas, Brushes.Black, 180, 130);
            e.Graphics.DrawString("www.panvinadelmar.com", consolas, Brushes.Black, 180, 150);

            // Información del Ticket
            e.Graphics.DrawString("Folio: " + ventaPrint.getId().ToString(), consolasGrande, Brushes.Black, 650, 70);
            e.Graphics.DrawString(System.DateTime.Now.ToShortDateString(), consolas, Brushes.Black, 650, 90);
            e.Graphics.DrawString("Cantidad", consolasBold, Brushes.Black, 50, 200);
            e.Graphics.DrawString("Producto", consolasBold, Brushes.Black, 130, 200);
            e.Graphics.DrawString("Precio unitario", consolasBold, Brushes.Black, 450, 200);
            e.Graphics.DrawString("Total", consolasBold, Brushes.Black, 600, 200);

            // Obtener productos ligados
            DataSet list = new DataSet();

            list = ventaPrint.productos();

            /*LISTA DE PRODUCTOS*/
            int y = 230;
            foreach(DataRow row in list.Tables["lista_productos"].Rows)
            {
                // Procesar cadena de precio
                string precio1 = row["producto_precio"].ToString().Replace(',', '.');
                double precio2 = 0;
                Double.TryParse(precio1, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out precio2);

                double cantidad = 0;
                Double.TryParse(row["cantidad"].ToString(), out cantidad);

                double total = cantidad * precio2;

                e.Graphics.DrawString(row["cantidad"].ToString(), consolas, Brushes.Black, 50, y);
                e.Graphics.DrawString(row["producto_nombre"].ToString(), consolas, Brushes.Black, 130, y);
                e.Graphics.DrawString("$" + precio2.ToString(), consolas, Brushes.Black, 450, y);
                e.Graphics.DrawString("$" + total, consolas, Brushes.Black, 600, y);
                y += 20;
            }

            e.Graphics.DrawString("Subtotal: ", consolasBold, Brushes.Black, 500, y + 30);
            e.Graphics.DrawString("$" + Math.Round(this.subtotalVenta, 2).ToString(), consolas, Brushes.Black, 600, y + 30);

            e.Graphics.DrawString("IVA: ", consolasBold, Brushes.Black, 500, y + 50);
            double iva = this.subtotalVenta * 0.16;
            e.Graphics.DrawString("$" + Math.Round(iva, 2).ToString(), consolas, Brushes.Black, 600, y + 50);

            e.Graphics.DrawString("Total: ", consolasBold, Brushes.Black, 500, y + 70);
            e.Graphics.DrawString("$" + Math.Round(this.totalVenta, 2).ToString(), consolas, Brushes.Black, 600, y + 70);

            e.Graphics.DrawString("Ef. recibido: ", consolasBold, Brushes.Black, 500, y + 90);
            e.Graphics.DrawString("$" + textBoxEfectivo.Text, consolas, Brushes.Black, 600, y + 90);

            e.Graphics.DrawString("Su cambio: ", consolasBold, Brushes.Black, 500, y + 110);
            e.Graphics.DrawString(lblCambio.Text, consolas, Brushes.Black, 600, y + 110);

            e.Graphics.DrawString("Gracias por su compra", consolasGrande, Brushes.Black, 300, y + 180);
            e.Graphics.DrawString("Lo atendió " + this.usuario.getNombre() + " " + this.usuario.getApellidos(), consolas, Brushes.Black, 300, y + 200);
        }

        /*
         * Limpiar todo
         */
        public void clearAll()
        {
            dt.Clear();
            textBoxEfectivo.Text = "0";
            this.subtotalVenta = 0;
            this.totalVenta = 0;
            this.actualizarTicket(true);
        }

        private void btnReporte1_Click(object sender, EventArgs e)
        {
            Producto obj = new Producto();
            dsProducto1 dsProducto = new dsProducto1();

            try
            {
                DataSet ds, ds2 = new DataSet();
                ds = obj.consultar("SELECT * FROM producto ORDER BY producto_precio DESC", "producto");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show
                        ("No se encontró...", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (DataRow row in ds.Tables["producto"].Rows)
                    {
                        dsProducto.dtProducto1.Rows.Add(
                            row["producto_id"].ToString(),
                            row["producto_nombre"].ToString(),
                            row["producto_descripcion"].ToString(),
                            row["producto_precio"].ToString(),
                            row["producto_activo"].ToString()
                            );
                    }
  
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
            Reporte rep = new Reporte(dsProducto.dtProducto1);
            rep.ShowDialog();
            

        }

        private void btnReporte2_Click(object sender, EventArgs e)
        {
            Venta obj = new Venta();
            dsProducto2 dsVenta = new dsProducto2();
            
            try
            {
                DataSet ds, ds2 = new DataSet();
                ds = obj.consultarVenta("SELECT * FROM venta", "venta");
                ds2 = obj.consultarVenta("SELECT AVG(venta_total) AS venta_total FROM venta AS v", "venta");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show
                        ("No se encontró...", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (DataRow row in ds.Tables["venta"].Rows)
                    {
                        dsVenta.dtProductoPromedio.Rows.Add(
                            row["venta_id"].ToString(),
                            "$" + row["venta_subtotal"].ToString(),
                            "$" + row["venta_total"].ToString(),
                            row["usuario_id"].ToString(),
                            row["venta_fecha_hora"].ToString()
                            );
                    }
                    foreach (DataRow row in ds2.Tables["venta"].Rows)
                    {
                        dsVenta.dtProductoPromedio.Rows.Add(
                            //row["venta_id"].ToString(),
                            //row["venta_subtotal"].ToString(),
                            "Promedio "  + " $" + row["venta_total"].ToString()
                            //row["usuario_id"].ToString(),
                            //row["venta_fecha_hora"].ToString()
                            );
                    }

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
            ReportePromedio rep = new ReportePromedio(dsVenta.dtProductoPromedio);
            rep.ShowDialog();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guarda el resultado de insertar o modificar valores
            bool result = false;

            // Checar que los campos estén llenos y que las contraseñas sean las mismas
            if (this.checkFormValues() && this.checkPasswords())
            {
                Usuario u = new Usuario();
                u.setNombre(txtNombre.Text);
                u.setApellidos(txtApellidos.Text);
                u.setRFC(txtRFC.Text);
                u.setDireccion(txtDireccion.Text);
                u.setCiudad(txtCiudad.Text);
                u.setTelefono(txtTel.Text);
                u.setContrasena(txtContrasena1.Text);
                u.setTipo(cmbTipo.Text);

                if (cmbEstado.Text == "activo") u.setActivo(1);
                else u.setActivo(0);

                // Si el código está vacío, es un nuevo usuario
                if (txtCodigo.Text == "")
                {
                    result = u.save("insert");

                    // Valida la respuesta SQL
                    if (result)
                    {
                        MessageBox.Show("El código generado para el usuario registrado es: " + u.findLast().ToString(), "Guardado con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearForm();
                        this.buscar();
                    }
                    else MessageBox.Show("No fue posible registrar los datos, por favor contacte al administrador.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    result = u.save("update", Int32.Parse(txtCodigo.Text));

                    // Valida la respuesta SQL
                    if (result) MessageBox.Show("Los datos del usuario se modificaron correctamente", "Guardado con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else MessageBox.Show("No fue posible guardar los cambios, por favor contacte al administrador.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Para actualizar DataGrid
                this.buscar();
            }
            
        }

        /*
         * Valida que los campos del formulario estén llenos
         */ 
        private bool checkFormValues()
        {
            if (txtNombre.Text == "" ||
               txtApellidos.Text == "" ||
               txtRFC.Text == "" ||
               txtDireccion.Text == "" ||
               txtCiudad.Text == "" ||
               txtTel.Text == "" ||
               txtContrasena1.Text == "" ||
               txtContrasena2.Text == "")
            {
                MessageBox.Show("Asegúrese de llenar todos los campos.", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else return true;
        }

        private bool checkPasswords()
        {
            if(txtContrasena1.Text == txtContrasena2.Text)
            {
                return true;
            } else
            {
                txtContrasena2.Text = "";
                txtContrasena2.Focus();
                MessageBox.Show("Asegúrese que las contraseñas sean las mismas.", "Las contraseñas no son iguales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void buscar()
        {
            if(txtBuscar.Text != "")
            {
                // Limpiar DataTable
                dtUsuarios.Clear();

                // Crear objeto y buscar usuarios
                Usuario listU = new Usuario();
                DataSet dsUsuarios = listU.findByApellido(txtBuscar.Text);

                foreach (DataRow r in dsUsuarios.Tables["usuario"].Rows)
                {
                    // Agregar fila a la tabla
                    DataRow r2 = dtUsuarios.NewRow();
                    r2["Código (ID)"] = r["usuario_id"].ToString();
                    r2["Nombre"] = r["usuario_nombre"].ToString();
                    r2["Apellidos"] = r["usuario_apellidos"].ToString();
                    r2["RFC"] = r["usuario_rfc"].ToString();
                    r2["Dirección"] = r["usuario_direccion"].ToString();
                    r2["Ciudad"] = r["usuario_ciudad"].ToString();
                    r2["Teléfono"] = r["usuario_telefono"].ToString();
                    r2["Tipo"] = r["usuario_tipo"].ToString();
                    r2["Contraseña"] = r["usuario_contrasena"].ToString();

                    if (r["usuario_activo"].ToString() == "1") r2["Estado"] = "Activo";
                    else r2["Estado"] = "Inactivo";

                    dtUsuarios.Rows.Add(r2);
                }
                dgUsuarios.Update();
            }

        }

        private void clearForm()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtCiudad.Clear();
            txtDireccion.Clear();
            txtTel.Clear();
            txtRFC.Clear();
            txtContrasena1.Clear();
            txtContrasena2.Clear();
            cmbTipo.Text = "vendedor";
        }

        private void dgUsuarios_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // LLenar formulario con usuario seleccionado
            txtCodigo.Text = dtUsuarios.Rows[e.RowIndex][0].ToString();
            txtNombre.Text = dtUsuarios.Rows[e.RowIndex][1].ToString();
            txtApellidos.Text = dtUsuarios.Rows[e.RowIndex][2].ToString();
            txtRFC.Text = dtUsuarios.Rows[e.RowIndex][3].ToString();
            txtDireccion.Text = dtUsuarios.Rows[e.RowIndex][4].ToString();
            txtCiudad.Text = dtUsuarios.Rows[e.RowIndex][5].ToString();
            txtTel.Text = dtUsuarios.Rows[e.RowIndex][6].ToString();
            cmbTipo.Text = dtUsuarios.Rows[e.RowIndex][7].ToString();
            txtContrasena1.Text = dtUsuarios.Rows[e.RowIndex][8].ToString();
            txtContrasena2.Text = dtUsuarios.Rows[e.RowIndex][8].ToString();

            if (dtUsuarios.Rows[e.RowIndex][9].ToString() == "Activo") cmbEstado.Text = "activo";
            else cmbEstado.Text = "inactivo";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.clearForm();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.actualizarTicket(false);
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            // Limpiar DataTable
            dtUsuarios.Clear();

            // Crear objeto y buscar usuarios
            Usuario listU = new Usuario();
            DataSet dsUsuarios = listU.findAll();

            foreach (DataRow r in dsUsuarios.Tables["usuario"].Rows)
            {
                // Agregar fila a la tabla
                DataRow r2 = dtUsuarios.NewRow();
                r2["Código (ID)"] = r["usuario_id"].ToString();
                r2["Nombre"] = r["usuario_nombre"].ToString();
                r2["Apellidos"] = r["usuario_apellidos"].ToString();
                r2["RFC"] = r["usuario_rfc"].ToString();
                r2["Dirección"] = r["usuario_direccion"].ToString();
                r2["Ciudad"] = r["usuario_ciudad"].ToString();
                r2["Teléfono"] = r["usuario_telefono"].ToString();
                r2["Tipo"] = r["usuario_tipo"].ToString();
                r2["Contraseña"] = r["usuario_contrasena"].ToString();

                if (r["usuario_activo"].ToString() == "1") r2["Estado"] = "Activo";
                else r2["Estado"] = "Inactivo";

                dtUsuarios.Rows.Add(r2);
            }
            dgUsuarios.Update();
        }
    }

}