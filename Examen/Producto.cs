using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    class Producto
    {
        private int id;
        private String nombre;
        private String descripcion;
        private float precio;

        /*
         * Encuentra a un producto por su ID
         */
        public Boolean findById(int id)
        {
            return false;
        }

        /*
         * Retorna una lista de Productos
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
