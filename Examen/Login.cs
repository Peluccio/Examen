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
            this.textBoxUsuario.Focus();
            
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
            this.validarIngreso();
        }

        public void setErrorProviders()
        {
            if (textBoxUsuario.Text == "")
                errorUsuario.SetError(textBoxUsuario, "Debe ingresar con un ID de usuario");

            if (textBoxContrasenia.Text == "")
                errorContrasena.SetError(textBoxContrasenia, "Debe ingresar con su contraseña de usuario");

        }

        private void textBoxUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == (char)Keys.Enter) textBoxContrasenia.Focus();
        }

        private void textBoxContrasenia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter) this.validarIngreso();
        }

        private void validarIngreso()
        {
            errorUsuario.Clear();
            errorContrasena.Clear();

            if (textBoxUsuario.Text != "" || textBoxContrasenia.Text != "")
            {
                try
                {
                    int id = -1;
                    Int32.TryParse(textBoxUsuario.Text, out id);

                    Usuario usuario = new Usuario();
                    usuario.findByIdPass(id, textBoxContrasenia.Text);
                    if (usuario.getId() > 0)
                    {
                        this.Hide();
                        MenuPrincipal ventana = new MenuPrincipal(usuario);
                        ventana.ShowDialog();
                        Dispose();
                    }
                    else
                    {
                        if (textBoxUsuario.Text == "" || textBoxContrasenia.Text == "") this.setErrorProviders();
                        else MessageBox.Show("Usuario o contraseña incorrectos", "No existe el usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } catch (FormatException)
                {
                    //MessageBox.Show("Usuario o contraseña incorrectos", "No existe el usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                this.setErrorProviders();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBoxUsuario.Focus();
        }
    }
}
