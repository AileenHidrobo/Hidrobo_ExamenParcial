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

namespace Hidrobo_ExamenParcial.Persistencia.Productos
{
    public partial class FRMEditarProducto : Form
    {
        private ProductoDTO productoActual;
        private ProductoService productoService = new ProductoService();
        private ProveedorService proveedorService = new ProveedorService();

        public FRMEditarProducto(ProductoDTO producto)
        {
            InitializeComponent();
            productoActual = producto;
        }

        private void FRMEditarProducto_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarDatosProducto();
        }

        private void CargarProveedores()
        {
            List<ProveedorDTO> proveedores = proveedorService.Todos();
            cmbProveedor.DataSource = proveedores;
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "IdProveedor";
        }

        private void CargarDatosProducto()
        {
            txtNombre.Text = productoActual.Nombre;
            txtPrecio.Text = productoActual.Precio.ToString("F2");
            cmbProveedor.SelectedValue = productoActual.IdProveedor;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            productoActual.Nombre = txtNombre.Text.Trim();

            if (decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                productoActual.Precio = precio;
            }
            else
            {
                MessageBox.Show("Precio inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            productoActual.IdProveedor = (int)cmbProveedor.SelectedValue;

            string resultado = productoService.Modificar(productoActual);

            if (resultado == "ok")
            {
                MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("Ingrese un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbProveedor.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}