using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    class Producto
    {
        // Objeto para conexión
        DataBase db = new DataBase();

        private int id;
        private String nombre;
        private String descripcion;
        private float precio;


        /*
         * Encuentra a un producto por su ID
         */
        public Boolean findById(int id)
        {
            try
            {
                SqlDataReader rows;
                string query = "SELECT * FROM producto WHERE producto_id = " + id;
                rows = db.execute(query);

                if(rows.Read())
                {
                    this.id = rows.GetInt32(0);
                    this.nombre = rows.GetString(1).ToString();
                    this.descripcion = rows.GetString(2).ToString();
                    this.precio = rows.GetFloat(3);
                }

                rows.Close();
                DataBase.conexion.Close();
                return true;
            } catch(Exception)
            {
                return false;
            }
        }

        /*
         * Retorna una lista de Productos
         */
        public DataSet findAll()
        {
            DataSet list = new DataSet();
            list = db.getList("SELECT * FROM producto", "producto");
            return list; 
        }
        

        /*
         * Crea un nuevo registro
         */
        public Boolean create()
        {
            try
            {
                SqlDataReader rows;
                string query = "INSERT INTO producto (producto_nombre, producto_descripcion, producto_precio) VALUES ("+this.nombre+", "+this.descripcion+", "+this.precio+")";
                rows = db.execute(query);
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
         * Modifica un producto
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

        public String getDescripcion()
        {
            return this.descripcion;
        }

        public void setDescripcion(String descripcion)
        {
            this.descripcion = descripcion;
        }

        public float getPrecio()
        {
            return this.precio;
        }

        public void setPrecio(float precio)
        {
            this.precio = precio;
        }
    }
}
