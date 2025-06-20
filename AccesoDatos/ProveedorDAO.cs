using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hidrobo_ExamenParcial.Datos;
using MySql.Data.MySqlClient;

namespace Hidrobo_ExamenParcial.AccesoDatos
{
    class ProveedorDAO
    {
        private Conexion _conexion = new Conexion();

        public List<ProveedorDTO> Todos()
        {
            List<ProveedorDTO> listaProveedores = new List<ProveedorDTO>();
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "SELECT id_proveedor, nombre, correo, telefono FROM `proveedores`";
                using (MySqlCommand cmd = new MySqlCommand(consulta, cn))
                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        ProveedorDTO proveedor = new ProveedorDTO
                        {
                            IdProveedor = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Correo = lector.GetString(2),
                            Telefono = lector.GetString(3)
                        };
                        listaProveedores.Add(proveedor);
                    }
                }
            }
            return listaProveedores;
        }

        public string Insertar(ProveedorDTO proveedorDTO)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "INSERT INTO `proveedores` (nombre, correo, telefono) VALUES (@nombre, @correo, @telefono)";
                MySqlCommand cmd = new MySqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@nombre", proveedorDTO.Nombre);
                cmd.Parameters.AddWithValue("@correo", proveedorDTO.Correo);
                cmd.Parameters.AddWithValue("@telefono", proveedorDTO.Telefono);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }

        public string Modificar(ProveedorDTO proveedorDTO)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "UPDATE `proveedores` SET nombre = @nombre, correo = @correo, telefono = @telefono WHERE id_proveedor = @id_proveedor";
                MySqlCommand cmd = new MySqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@nombre", proveedorDTO.Nombre);
                cmd.Parameters.AddWithValue("@correo", proveedorDTO.Correo);
                cmd.Parameters.AddWithValue("@telefono", proveedorDTO.Telefono);
                cmd.Parameters.AddWithValue("@id_proveedor", proveedorDTO.IdProveedor);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }

        public string Eliminar(int idProveedor)
        {
            using (MySqlConnection cn = _conexion.AbrirConexion())
            {
                string consulta = "DELETE FROM `proveedores` WHERE id_proveedor = @id_proveedor";
                MySqlCommand cmd = new MySqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0 ? "ok" : "error";
            }
        }
    }
}