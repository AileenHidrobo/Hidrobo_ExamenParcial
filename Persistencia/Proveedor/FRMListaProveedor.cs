using Hidrobo_ExamenParcial.AccesoDatos;
using Hidrobo_ExamenParcial.Aplicacion;
using Hidrobo_ExamenParcial.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hidrobo_ExamenParcial.Persistencia.Usuarios
{
    public partial class FRMListaProveedor : Form
    {
        private ProveedorService proveedorService = new ProveedorService();
        private List<ProveedorDTO> listaProveedores = new List<ProveedorDTO>();

        public FRMListaProveedor()
        {
            InitializeComponent();
        }

        private void FRMListaProveedor_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            listaProveedores = proveedorService.Todos();
            lstProveedor.DataSource = null;
            lstProveedor.DataSource = listaProveedores;
            lstProveedor.DisplayMember = "Nombre";    // Mostrar el nombre del proveedor
            lstProveedor.ValueMember = "IdProveedor"; // Usar su id como valor
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FRMNuevoProveedor frm = new FRMNuevoProveedor();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarProveedores();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstProveedor.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un proveedor para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProveedorDTO seleccionado = (ProveedorDTO)lstProveedor.SelectedItem;
            FRMEditarProveedor frm = new FRMEditarProveedor(seleccionado);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarProveedores();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstProveedor.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProveedorDTO seleccionado = (ProveedorDTO)lstProveedor.SelectedItem;
            DialogResult confirm = MessageBox.Show(
                $"¿Está seguro que quiere eliminar el proveedor '{seleccionado.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                string resultado = proveedorService.Eliminar(seleccionado.IdProveedor);
                if (resultado == "ok")
                {
                    MessageBox.Show("Proveedor eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProveedores();
                }
                else
                {
                    MessageBox.Show("Error al eliminar proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}