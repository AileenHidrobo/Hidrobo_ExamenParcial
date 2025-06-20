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

namespace Hidrobo_ExamenParcial.Persistencia.Usuarios
{
    public partial class FRMEditarProveedor : Form
    {
        private ProveedorDTO proveedorActual;
        private ProveedorService proveedorService = new ProveedorService();

        public FRMEditarProveedor(ProveedorDTO proveedor)
        {
            InitializeComponent();
            proveedorActual = proveedor;
        }

        private void FRMEditarProveedor_Load(object sender, EventArgs e)
        {
            CargarDatosProveedor();
        }

        private void CargarDatosProveedor()
        {
            txtNombre.Text = proveedorActual.Nombre;
            txtCorreo.Text = proveedorActual.Correo;
            txtTelefono.Text = proveedorActual.Telefono;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            proveedorActual.Nombre = txtNombre.Text.Trim();
            proveedorActual.Correo = txtCorreo.Text.Trim();
            proveedorActual.Telefono = txtTelefono.Text.Trim();

            string resultado = proveedorService.Modificar(proveedorActual);

            if (resultado == "ok")
            {
                MessageBox.Show("Proveedor actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Debe ingresar un correo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Debe ingresar un teléfono.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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