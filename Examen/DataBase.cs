using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Examen
{
    class DataBase
    {
        public static SqlConnection conexion = new SqlConnection("Initial Catalog=panaderia; Data Source=DESKTOP-N5AVN33\\SQLEXPRESS;Integrated Security=SSPI");

        public SqlDataReader execute(string query)
        {
            SqlCommand comando = new SqlCommand(query, conexion);
            conexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public int executeNonQuery(string query)
        {
            int respuesta = 1;
            SqlCommand comando = new SqlCommand(query, conexion);
            conexion.Open();
            respuesta = comando.ExecuteNonQuery();
            conexion.Close();
            return respuesta;
        }

        public DataSet getList(string query, string table)
        {
            DataSet list = new DataSet();
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            conexion.Open();
            adaptador.Fill(list, table);
            conexion.Close();
            return list;
        }
    }
}
