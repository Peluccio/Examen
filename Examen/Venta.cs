using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    class Venta
    {
        private int id;
        private String fecha;
        private float subtotal;
        private float total;
        private int usuario_id;

        /*
         * Encuentra a una venta por su ID
         */
        public Boolean findById(int id)
        {
            return false;
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
            return false;
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

        public int getId()
        {
            return this.id;
        }

        public String getFecha()
        {
            return this.fecha;
        }

        public void setFecha(string fecha)
        {
            this.fecha = fecha;
        }

        public float getSubtotal()
        {
            return this.subtotal;
        }

        public void setSubtotal(float subtotal)
        {
            this.subtotal = subtotal;
        }

        public float getTotal()
        {
            return this.total;
        }

        public void setTotal(float total)
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
         public void productos()
        {

        }
    }
}
