using Hidrobo_ExamenParcial.Datos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Hidrobo_ExamenParcial.AccesoDatos
{
    class ProductoDAO
    {
        private Conexion _conexion = new Conexion();

        public List<ProductoDTO> Todos()
        {
            List<ProductoDTO> listaProductos = new List<ProductoDTO>();
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "SELECT id_producto, nombre, precio, id_proveedor FROM productos";
                using (MySqlCommand cmd = new MySqlCommand(consulta, cn))
                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        ProductoDTO producto = new ProductoDTO
                        {
                            IdProducto = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Precio = lector.GetDecimal(2),
                            IdProveedor = lector.GetInt32(3)
                        };
                        listaProductos.Add(producto);
                    }
                }
            }
            return listaProductos;
        }

        public string Insertar(ProductoDTO productoDTO)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "INSERT INTO `productos` (nombre, precio, id_proveedor) VALUES (@nombre, @precio, @id_proveedor)";
                MySqlCommand cmd = new MySqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@nombre", productoDTO.Nombre);
                cmd.Parameters.AddWithValue("@precio", productoDTO.Precio);
                cmd.Parameters.AddWithValue("@id_proveedor", productoDTO.IdProveedor);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }

        public string Modificar(ProductoDTO productoDTO)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "UPDATE `productos` SET nombre = @nombre, precio = @precio, id_proveedor = @id_proveedor WHERE id_producto = @id_producto";
                MySqlCommand cmd = new MySqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@nombre", productoDTO.Nombre);
                cmd.Parameters.AddWithValue("@precio", productoDTO.Precio);
                cmd.Parameters.AddWithValue("@id_proveedor", productoDTO.IdProveedor);
                cmd.Parameters.AddWithValue("@id_producto", productoDTO.IdProducto);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }

        public string Eliminar(int idProducto)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "DELETE FROM `productos` WHERE id_producto = @id_producto";
                MySqlCommand cmd = new MySqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id_producto", idProducto);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }
    }
}