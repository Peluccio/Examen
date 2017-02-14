using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen
{
    class Usuario
    {
        private int id;
        private String nombre;
        private String apellidos;
        private String rfc;
        private String direccion;
        private String ciudad;
        private String telefono;
        private String tipo;

        /*
         * Encuentra a un usuario por su ID
         */
        public Boolean findById(int id)
        {
            return false;
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


        /*
         * ACTIVE RECORD
         */
         public void ventas()
        {

        }
    }
}
