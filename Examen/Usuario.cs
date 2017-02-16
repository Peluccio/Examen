using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    class Usuario
    {
        // Objeto para conexión
        DataBase db = new DataBase();

        private int id;
        private String nombre;
        private String apellidos;
        private string contrasena;
        private String rfc;
        private String direccion;
        private String ciudad;
        private String telefono;
        private String tipo;

        /*
         * Encuentra a un usuario por su ID
         */
        public Boolean findByIdPass(int id, string pass)
        {
            try
            {
                SqlDataReader rows;
                string query = "SELECT * FROM usuario WHERE usuario_id = " + id + " AND usuario_contrasena = " + pass;
                rows = db.execute(query);

                if (rows.Read())
                {
                    this.id = rows.GetInt32(0);
                    this.nombre = rows.GetString(1).ToString();
                    this.apellidos = rows.GetString(2).ToString();
                    this.rfc = rows.GetString(3).ToString();
                    this.direccion = rows.GetString(4).ToString();
                    this.ciudad = rows.GetString(5).ToString();
                    this.telefono = rows.GetString(6).ToString();
                    this.tipo = rows.GetString(7).ToString();
                    this.contrasena = rows.GetString(8).ToString();
                }

                rows.Close();
                DataBase.conexion.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
         * Retorna una lista de Usuarios
         */
        public void findAll()
        {

        }

        /*
         * Crea un nuevo registro
         */
        public Boolean create()
        {
            return false;
        }

        /*
         * Modifica un usuario
         */
        public Boolean update()
        {
            return false;
        }

        /*
         * Elimina registro con determinado ID
         */
        public Boolean delete(int id)
        {
            return false;
        }

        public int getId()
        {
            return this.id;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public String getApellidos()
        {
            return this.apellidos;
        }

        public void setApellidos(String apellidos)
        {
            this.apellidos = apellidos;
        }

        public String getRFC()
        {
            return this.rfc;
        }

        public void setRFC(String rfc)
        {
            this.rfc = rfc;
        }

        public String getDireccion()
        {
            return this.direccion;
        }

        public void setDireccion(String direccion)
        {
            this.direccion = direccion;
        }

        public String getCiudad()
        {
            return this.ciudad;
        }

        public void setCiudad(String ciudad)
        {
            this.ciudad = ciudad;
        }

        public String getTelefono()
        {
            return this.telefono;
        }

        public void setTelefono(String telefono)
        {
            this.telefono = telefono;
        }

        public String getTipo()
        {
            return this.tipo;
        }

        public String getContrasena()
        {
            return this.contrasena;
        }

        public void setContrasena()
        {
            this.contrasena = contrasena;
        }


        /*
         * ACTIVE RECORD
         */
         public void ventas()
        {

        }
    }
}
