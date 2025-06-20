using System;
using System.Collections.Generic;
using Hidrobo_ExamenParcial.AccesoDatos;
using Hidrobo_ExamenParcial.Datos;

namespace Hidrobo_ExamenParcial.Aplicacion
{
    public class InventarioService
    {
        private readonly AccesoDatos.InventarioDAO _inventarioDAO = new AccesoDatos.InventarioDAO();

        public List<Datos.InventarioDTO> Todos()
        {
            return _inventarioDAO.Todos();
        }

        public string Insertar(Datos.InventarioDTO inventarioDTO)
        {
            return _inventarioDAO.Insertar(inventarioDTO);
        }

        public string Eliminar(int idInventario)
        {
            return _inventarioDAO.Eliminar(idInventario);
        }

        public string Modificar(Datos.InventarioDTO inventarioDTO)
        {
            return _inventarioDAO.Modificar(inventarioDTO);
        }
    }
}