using Hidrobo_ExamenParcial.Datos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Hidrobo_ExamenParcial.AccesoDatos
{
    class InventarioDAO
    {
        private Conexion _conexion = new Conexion();

        public List<InventarioDTO> Todos()
        {
            List<InventarioDTO> listaInventario = new List<InventarioDTO>();
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string cadena = "SELECT id_inventario, id_producto, cantidad, precio, id_proveedor FROM `inventarios`";
                using (MySqlCommand cmd = new MySqlCommand(cadena, cn))
                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        InventarioDTO inv = new InventarioDTO
                        {
                            IdInventario = lector.GetInt32(0),
                            IdProducto = lector.GetInt32(1),
                            Cantidad = lector.GetInt32(2),
                            Precio = lector.GetDecimal(3),
                            IdProveedor = lector.GetInt32(4)
                        };
                        listaInventario.Add(inv);
                    }
                }
            }
            return listaInventario;
        }

        public string Insertar(InventarioDTO inventarioDTO)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string cadena =
                    "INSERT INTO `inventarios`(`id_producto`, `cantidad`, `precio`, `id_proveedor`) " +
                    "VALUES (@id_producto, @cantidad, @precio, @id_proveedor)";
                MySqlCommand sqlCommand = new MySqlCommand(cadena, cn);
                sqlCommand.Parameters.AddWithValue("@id_producto", inventarioDTO.IdProducto);
                sqlCommand.Parameters.AddWithValue("@cantidad", inventarioDTO.Cantidad);
                sqlCommand.Parameters.AddWithValue("@precio", inventarioDTO.Precio);
                sqlCommand.Parameters.AddWithValue("@id_proveedor", inventarioDTO.IdProveedor);

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }

        public string Modificar(InventarioDTO inventarioDTO)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string cadena = "UPDATE `inventarios` " +
                "SET `id_producto`=@id_producto, `cantidad`=@cantidad, `precio`=@precio, `id_proveedor`=@id_proveedor " +
                "WHERE `id_inventario`=@id_inventario";
                MySqlCommand sqlCommand = new MySqlCommand(cadena, cn);
                sqlCommand.Parameters.AddWithValue("@id_producto", inventarioDTO.IdProducto);
                sqlCommand.Parameters.AddWithValue("@cantidad", inventarioDTO.Cantidad);
                sqlCommand.Parameters.AddWithValue("@precio", inventarioDTO.Precio);
                sqlCommand.Parameters.AddWithValue("@id_proveedor", inventarioDTO.IdProveedor);
                sqlCommand.Parameters.AddWithValue("@id_inventario", inventarioDTO.IdInventario);

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }

        public string Eliminar(int idInventario)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string cadena = "DELETE FROM `inventarios` WHERE `id_inventario`=@id_inventario";
                MySqlCommand sqlCommand = new MySqlCommand(cadena, cn);
                sqlCommand.Parameters.AddWithValue("@id_inventario", idInventario);

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }
    }
}