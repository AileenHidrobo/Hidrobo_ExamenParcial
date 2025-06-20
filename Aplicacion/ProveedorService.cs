using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidrobo_ExamenParcial.Aplicacion
{
    public class ProveedorService
    {
        private readonly AccesoDatos.ProveedorDAO _proveedorDAO = new AccesoDatos.ProveedorDAO();

        public List<Datos.ProveedorDTO> Todos()
        {
            return _proveedorDAO.Todos();
        }

        public string Insertar(Datos.ProveedorDTO proveedorDTO)
        {
            return _proveedorDAO.Insertar(proveedorDTO);
        }

        public string Modificar(Datos.ProveedorDTO proveedorDTO)
        {
            return _proveedorDAO.Modificar(proveedorDTO);
        }

        public string Eliminar(int idProveedor)
        {
            return _proveedorDAO.Eliminar(idProveedor);
        }
    }
}
