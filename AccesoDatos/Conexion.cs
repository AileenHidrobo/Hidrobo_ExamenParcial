using System;
using MySql.Data.MySqlClient;

namespace Hidrobo_ExamenParcial.AccesoDatos
{
    public class Conexion
    {
        private readonly string cadenaConexion =
            "server=localhost;database=inventario_productos;uid=root;pwd=;";
        private MySqlConnection conexion;

        public MySqlConnection AbrirConexion()
        {
            conexion = new MySqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }

        public void CerrarConexion()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }

    }
}
