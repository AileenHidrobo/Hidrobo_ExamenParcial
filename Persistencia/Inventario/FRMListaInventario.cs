using Hidrobo_ExamenParcial.AccesoDatos;
using Hidrobo_ExamenParcial.Aplicacion;
using Hidrobo_ExamenParcial.Datos;
using Hidrobo_ExamenParcial.Persistencia.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hidrobo_ExamenParcial.Persistencia.Inventario
{
    public partial class FRMListaInventario : Form
    {
        private InventarioService inventarioService = new InventarioService();
        private List<InventarioDTO> listaInventarios = new List<InventarioDTO>();

        public FRMListaInventario()
        {
            InitializeComponent();
        }

        private void FRMListaInventario_Load(object sender, EventArgs e)
        {
            CargarInventarios();
        }

        private void CargarInventarios()
        {
            listaInventarios = inventarioService.Todos();

            var productoService = new ProductoService();
            var listaProductos = productoService.Todos();

            var listaMostrar = listaInventarios.Select(inv => new
            {
                IdInventario = inv.IdInventario,
                NombreProducto = listaProductos.FirstOrDefault(p => p.IdProducto == inv.IdProducto)?.Nombre ?? "Desconocido"
            }).ToList();

            lstInventario.DataSource = null;
            lstInventario.DataSource = listaMostrar;
            lstInventario.DisplayMember = "NombreProducto";
            lstInventario.ValueMember = "IdInventario";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FRMNuevoInventario frm = new FRMNuevoInventario();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarInventarios();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstInventario.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un inventario para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InventarioDTO seleccionado = (InventarioDTO)lstInventario.SelectedItem;
            FRMEditarInventario frm = new FRMEditarInventario(seleccionado);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarInventarios();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstInventario.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un inventario para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InventarioDTO seleccionado = (InventarioDTO)lstInventario.SelectedItem;
            DialogResult confirm = MessageBox.Show($"¿Está seguro de eliminar el inventario con ID {seleccionado.IdInventario}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string resultado = inventarioService.Eliminar(seleccionado.IdInventario);
                if (resultado == "ok")
                {
                    MessageBox.Show("Inventario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInventarios();
                }
                else
                {
                    MessageBox.Show("Error al eliminar inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}