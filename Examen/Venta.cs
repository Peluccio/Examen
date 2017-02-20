using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    class Venta
    {
        DataBase db = new DataBase();
        int res = -1;

        private int id;
        private String fecha_hora;
        private double subtotal;
        private double total;
        private int usuario_id;

        /*
         * Encuentra a una venta por su ID
         */
        public Boolean findById(int id)
        {
            try
            {
                SqlDataReader rows;
                string query = "SELECT * FROM venta WHERE venta_id = " + id;
                rows = db.execute(query);

                if (rows.Read())
                {
                    this.id = rows.GetInt32(0);
                    this.subtotal = rows.GetFloat(1);
                    this.total = rows.GetFloat(2);
                    this.usuario_id = rows.GetInt32(3);
                    this.fecha_hora = rows.GetString(4).ToString();
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

        public int findLast()
        {
            int last_id = 0;
            try
            {
                SqlDataReader rows;
                string query = "SELECT TOP 1 * FROM venta ORDER BY venta_id DESC";
                rows = db.execute(query);

                
                if (rows.Read())
                {
                    last_id = rows.GetInt32(0);
                }

                rows.Close();
                DataBase.conexion.Close();
                return last_id;
            }
            catch (Exception)
            {
                return last_id;
            }
        }

        /*
         * Retorna una lista de Ventas
         */
        public void findAll()
        {

        }

        /*
         * Crea un nuevo registro
         */
        public Boolean create()
        {
            try
            {
                string subtotal = this.subtotal.ToString().Replace(',', '.');
                string total = this.total.ToString().Replace(',', '.');

                string query = "INSERT INTO venta (venta_subtotal, venta_total, usuario_id, venta_fecha_hora) " +
                    "VALUES ("+subtotal+", "+total+", "+this.usuario_id+", '"+ System.DateTime.Now.ToShortDateString() +"')";

                res = db.executeNonQuery(query);

                if (res == 1) {
                    this.id = this.findLast();
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
         * Modifica una venta
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

        /*
         * Agrega el producto a la venta
         */
        public Boolean addProduct(int producto_id, double producto_cantidad)
        {
            try
            {
                string cantidad = producto_cantidad.ToString().Replace(',', '.');

                string query = "INSERT INTO venta_producto VALUES (" + this.id + ", "
                    + producto_id + ", "+cantidad+")";

                res = db.executeNonQuery(query);

                if (res == 1) return true;
                else return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                DataBase.conexion.Close();
            }
        }

        public int getId()
        {
            return this.id;
        }

        public String getFechaHora()
        {
            return this.fecha_hora;
        }

        public void setFecha(string fecha_hora)
        {
            this.fecha_hora = fecha_hora;
        }

        public double getSubtotal()
        {
            return this.subtotal;
        }

        public void setSubtotal(double subtotal)
        {
            this.subtotal = subtotal;
        }

        public double getTotal()
        {
            return this.total;
        }

        public void setTotal(double total)
        {
            this.total = total;
        }

        public int getUsuarioId()
        {
            return this.usuario_id;
        }

        public void setUsuarioId(int usuario_id)
        {
            this.usuario_id = usuario_id;
        }

        /*
         * ACTIVE RECORD
         */
         public DataSet productos()
        {
            DataSet list = new DataSet();
            list = db.getList("SELECT * FROM lista_productos WHERE venta_id = " + this.id, "lista_productos");
            return list;
        }



    }
}
