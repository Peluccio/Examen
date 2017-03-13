using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    public class Usuario
    {
        // Objeto para conexión
        DataBase db = new DataBase();
        int res = -1;

        private int id;
        private String nombre;
        private String apellidos;
        private String contrasena;
        private String rfc;
        private String direccion;
        private String ciudad;
        private String telefono;
        private String tipo;
        private int activo;

        /*
         * Obtener la instancia del usuario que ingresó 
         */
         public  Usuario getInstance()
        {
            return this;
        }

        /*
         * Encuentra a un usuario por su ID
         */
        public Boolean findByIdPass(int id, string pass)
        {
            try
            {
                SqlDataReader rows;
                string query = "SELECT * FROM usuario WHERE usuario_id = " + id + " AND usuario_contrasena = '" + pass + "'";
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
                    //this.activo = rows.Get(9).ToString();
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
         * Guarda al usuario en la base de datos
         */
        public Boolean save(string type)
        {
            try
            {
                string query = "";

                if(type == "insert")
                {
                    query = "INSERT INTO usuario (usuario_nombre, usuario_apellidos, usuario_rfc, usuario_direccion, usuario_ciudad, usuario_telefono, usuario_tipo, usuario_contrasena, usuario_activo) " +
                    "VALUES ('" + this.nombre + "', '" + this.apellidos + "', '" + this.rfc + "', '" + this.direccion + "', '"+this.ciudad+"', '"+this.telefono+"', '"+this.tipo+"', '"+this.contrasena+"', '"+1+"')";
                } else
                {
                    query = "UPDATE usuario SET " +
                        "usuario_nombre = '" + this.nombre + "', " +
                        "usuario_apellidos = '" + this.apellidos + "', " +
                        "usuario_rfc = '" + this.rfc + "', " +
                        "usuario_direccion = '" + this.direccion + "', " +
                        "usuario_ciudad = '" + this.ciudad + "'," +
                        "usuario_telefono = '" + this.telefono + "', " +
                        "usuario_tipo = '" + this.tipo + "', " +
                        "usuario_contrasena = '" + this.contrasena + "', " +
                        "usuario_activo = '" + this.activo + "' WHERE usuario_id = "+this.id;
                }

                res = db.executeNonQuery(query);

                if (res == 1)
                {
                    return true;
                }
                else return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
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

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }

        public String getContrasena()
        {
            return this.contrasena;
        }

        public void setContrasena(string contrasena)
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
