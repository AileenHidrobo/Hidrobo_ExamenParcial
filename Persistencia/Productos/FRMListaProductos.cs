using Hidrobo_ExamenParcial.Aplicacion;
using Hidrobo_ExamenParcial.Datos;
using Hidrobo_ExamenParcial.Persistencia.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hidrobo_ExamenParcial.Persistencia.Productos
{
    public partial class FRMListaProducto : Form
    {
        private ProductoService productoService = new ProductoService();
        private List<ProductoDTO> listaProductos = new List<ProductoDTO>();

        public FRMListaProducto()
        {
            InitializeComponent();
            this.Load += FRMListaProducto_Load;
        }

        private void FRMListaProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            listaProductos = productoService.Todos();

            lstProducto.DataSource = null;
            lstProducto.DataSource = listaProductos;
            lstProducto.DisplayMember = "Nombre";
            lstProducto.ValueMember = "IdProducto";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FRMNuevoProducto frm = new FRMNuevoProducto();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProductoDTO seleccionado = (ProductoDTO)lstProducto.SelectedItem;
            FRMEditarProducto frm = new FRMEditarProducto(seleccionado);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProductoDTO seleccionado = (ProductoDTO)lstProducto.SelectedItem;
            DialogResult confirm = MessageBox.Show($"¿Está seguro de eliminar el producto '{seleccionado.Nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string resultado = productoService.Eliminar(seleccionado.IdProducto);
                if (resultado == "ok")
                {
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
