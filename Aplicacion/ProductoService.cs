using Hidrobo_ExamenParcial.AccesoDatos;
using Hidrobo_ExamenParcial.Datos;
using System.Collections.Generic;

namespace Hidrobo_ExamenParcial.Aplicacion
{
    public class ProductoService
    {
        private readonly AccesoDatos.ProductoDAO _productoDAO = new AccesoDatos.ProductoDAO();

        public List<Datos.ProductoDTO> Todos()
        {
            return _productoDAO.Todos();
        }

        public string Insertar(Datos.ProductoDTO productoDTO)
        {
            return _productoDAO.Insertar(productoDTO);
        }

        public string Modificar(Datos.ProductoDTO productoDTO)
        {
            return _productoDAO.Modificar(productoDTO);
        }

        public string Eliminar(int idProducto)
        {
            return _productoDAO.Eliminar(idProducto);
        }
    }
}