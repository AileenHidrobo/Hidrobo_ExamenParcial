using Hidrobo_ExamenParcial.Aplicacion;
using Hidrobo_ExamenParcial.Datos;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hidrobo_ExamenParcial.Persistencia.Inventario
{
    public partial class FRMEditarInventario : Form
    {
        private InventarioDTO inventarioActual;
        private ProductoService productoService = new ProductoService();
        private ProveedorService proveedorService = new ProveedorService();
        private InventarioService inventarioService = new InventarioService();

        public FRMEditarInventario(InventarioDTO inventario)
        {
            InitializeComponent();
            inventarioActual = inventario;
        }

        private void FRMEditarInventario_Load(object sender, EventArgs e)
        {
            CargarProductos();
            CargarProveedores();
            CargarDatosInventario();
        }

        private void CargarProductos()
        {
            List<ProductoDTO> productos = productoService.Todos();
            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProducto";
        }

        private void CargarProveedores()
        {
            List<ProveedorDTO> proveedores = proveedorService.Todos();
            cmbProveedor.DataSource = proveedores;
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "IdProveedor";
        }

        private void CargarDatosInventario()
        {
            cmbProducto.SelectedValue = inventarioActual.IdProducto;

            cmbProveedor.SelectedValue = inventarioActual.IdProveedor;

            nudCantidad.Value = inventarioActual.Cantidad;
            txtPrecio.Text = inventarioActual.Precio.ToString("F2");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            inventarioActual.IdProducto = (int)cmbProducto.SelectedValue;
            inventarioActual.IdProveedor = (int)cmbProveedor.SelectedValue;
            inventarioActual.Cantidad = (int)nudCantidad.Value;

            if (decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                inventarioActual.Precio = precio;
            }
            else
            {
                MessageBox.Show("Precio inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string resultado = inventarioService.Modificar(inventarioActual);

            if (resultado == "ok")
            {
                MessageBox.Show("Inventario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (cmbProducto.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbProveedor.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("Ingrese un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}