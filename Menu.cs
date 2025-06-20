using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hidrobo_ExamenParcial
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var frmListaProveedor = new Hidrobo_ExamenParcial.Persistencia.Usuarios.FRMListaProveedor();
            frmListaProveedor.Show();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            var frmListaInventario = new Hidrobo_ExamenParcial.Persistencia.Inventario.FRMListaInventario();
            frmListaInventario.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var frmListaProducto = new Hidrobo_ExamenParcial.Persistencia.Productos.FRMListaProducto();
            frmListaProducto.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}