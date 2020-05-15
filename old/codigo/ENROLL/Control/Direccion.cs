using ENROLL.Model;
using ENROLL.Helpers;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENROLL.Control
{
    public class Direccion : UserControl
    {
        private int rowIndex = 0;
        public BindingList<Address> _Direcciones = new BindingList<Address>();
        private IContainer components = (IContainer)null;
        private DataGridView dataGridView1;
        private TextBox txtCallePrincipal;
        private ComboBox cmbDireccionProv;
        private ComboBox cmbDireccionDpto;
        private ComboBox cmbTipoDireccion;
        private Label label42;
        private Label label41;
        private Label label35;
        private Label label6;
        private Button btnmas;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem eliminarToolStripMenuItem;
        private ErrorProvider errorProvider1;

        public BindingList<Address> Direcciones
        {
            get
            {
                return this.ObtenerDirecciones();
            }
            set
            {
                this._Direcciones = value;
            }
        }

        private BindingList<Address> ObtenerDirecciones()
        {
            BindingList<Address> bindingList = new BindingList<Address>();
            DataTable dataSource = (DataTable)this.dataGridView1.DataSource;
            if (dataSource != null)
            {
                foreach (DataRow row in (InternalDataCollectionBase)dataSource.Rows)
                    bindingList.Add(new Address()
                    {
                        Type = new Coder()
                        {
                            CoderTypeId = "ADDRESSTYPE",
                            Id = row.Field<string>("IdTipoDireccion"),
                            Value = row.Field<string>("TipoDireccion")
                        },
                        Street = row.Field<string>("CallePrincipal"),
                        State = new Coder()
                        {
                            CoderTypeId = "PROV",
                            Id = row.Field<string>("IdDepartamento"),
                            Value = row.Field<string>("Departamento")
                        },
                        District = new Coder()
                        {
                            CoderTypeId = "MUN",
                            Id = row.Field<string>("IdProvincia"),
                            Value = row.Field<string>("Provincia")
                        }
                    });
            }
            return bindingList;
        }

        public Direccion()
        {
            this.InitializeComponent();
        }

        private void Direccion_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("TipoDireccion");
            dataTable.Columns.Add("CallePrincipal");
            dataTable.Columns.Add("Departamento");
            dataTable.Columns.Add("Provincia");
            dataTable.Columns.Add("IdTipoDireccion");
            dataTable.Columns.Add("IdDepartamento");
            dataTable.Columns.Add("IdProvincia");
            foreach (Address direccione in (Collection<Address>) this._Direcciones)
            {
                DataRow row = dataTable.NewRow();
                row["TipoDireccion"] = (object)direccione.Type.Value;
                row["CallePrincipal"] = (object)direccione.Street;
                row["Departamento"] = (object)direccione.State.Value;
                row["Provincia"] = (object)direccione.District.Value;
                row["IdTipoDireccion"] = (object)direccione.Type.Id;
                row["IdDepartamento"] = (object)direccione.State.Value;
                row["IdProvincia"] = (object)direccione.District.Value;
                dataTable.Rows.Add(row);
            }
            this.dataGridView1.DataSource = (object)dataTable;
            this.dataGridView1.Columns["IdTipoDireccion"].Visible = false;
            this.dataGridView1.Columns["IdDepartamento"].Visible = false;
            this.dataGridView1.Columns["IdProvincia"].Visible = false;
            this.dataGridView1.Columns["TipoDireccion"].Width = 135;
            this.dataGridView1.Columns["CallePrincipal"].Width = 232;
            this.dataGridView1.Columns["Departamento"].Width = 195;
            this.dataGridView1.Columns["Provincia"].Width = 195;
            this.CargaTipoDireccion();
            this.CargaDepartamento();
            this.ValidaModeloDatosDireccion();
        }

        private void CargaTipoDireccion()
        {
            DataTable dataSource = (DataTable)this.dataGridView1.DataSource;
            List<Coder> source = HelperLoadControl.ObtenerLista("ADDRESSTYPE");
            string vTipoDir = dataSource == null || dataSource.Rows.Count == 0 ? "PERM" : "TRAN";
            HelperLoadControl.Combo(this.cmbTipoDireccion, source.Where<Coder>((Func<Coder, bool>)(cust => cust.Id == vTipoDir)).ToList<Coder>(), false);
        }

        public void CargaDepartamento()
        {
            HelperLoadControl.Combo(this.cmbDireccionDpto, HelperLoadControl.ObtenerLista("PROV"), true);
        }

        public DataTable ObtenerTablaDireccion()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("TipoDireccion");
            dataTable.Columns.Add("CallePrincipal");
            dataTable.Columns.Add("Departamento");
            dataTable.Columns.Add("Provincia");
            dataTable.Columns.Add("IdTipoDireccion");
            dataTable.Columns.Add("IdDepartamento");
            dataTable.Columns.Add("IdProvincia");
            DataRow row = dataTable.NewRow();
            row["TipoDireccion"] = (object)this.cmbTipoDireccion.SelectedItem.ToString();
            row["CallePrincipal"] = (object)this.txtCallePrincipal.Text;
            row["Departamento"] = (object)this.cmbDireccionDpto.SelectedItem.ToString();
            row["Provincia"] = (object)this.cmbDireccionProv.SelectedItem.ToString();
            row["IdTipoDireccion"] = this.cmbTipoDireccion.SelectedValue;
            row["IdDepartamento"] = this.cmbDireccionDpto.SelectedValue;
            row["IdProvincia"] = this.cmbDireccionProv.SelectedValue;
            dataTable.Rows.Add(row);
            return dataTable;
        }

        private void btnmas_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)this.dataGridView1.DataSource ?? new DataTable();
            dataTable.Merge(this.ObtenerTablaDireccion());
            this.dataGridView1.DataSource = (object)dataTable;
            this.dataGridView1.Columns["IdTipoDireccion"].Visible = false;
            this.dataGridView1.Columns["IdDepartamento"].Visible = false;
            this.dataGridView1.Columns["IdProvincia"].Visible = false;
            this.dataGridView1.Columns["TipoDireccion"].Width = 135;
            this.dataGridView1.Columns["CallePrincipal"].Width = 232;
            this.dataGridView1.Columns["Departamento"].Width = 195;
            this.dataGridView1.Columns["Provincia"].Width = 195;
            this.CargaTipoDireccion();
            this.CargaDepartamento();
            this.txtCallePrincipal.Text = "";
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            this.dataGridView1.Rows[e.RowIndex].Selected = true;
            this.rowIndex = e.RowIndex;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
            if (this.rowIndex == this.dataGridView1.Rows.Count - 1)
            {
                this.contextMenuStrip1.Show((System.Windows.Forms.Control)this.dataGridView1, e.Location);
                this.contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows[this.rowIndex].IsNewRow)
                return;
            this.dataGridView1.Rows.RemoveAt(this.rowIndex);
            this.CargaTipoDireccion();
            this.CargaDepartamento();
            this.txtCallePrincipal.Text = "";
        }

        private void txtCallePrincipal_TextChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDireccion();
        }

        private void cmbDireccionDpto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = (string)((ListControl)sender).SelectedValue;
            List<Coder> pColCoder = new List<Coder>();
            if (!string.IsNullOrWhiteSpace(selectedValue))
                pColCoder = HelperLoadControl.ObtenerSubLista("PROV", selectedValue);
            HelperLoadControl.Combo(this.cmbDireccionProv, pColCoder, true);
            this.ValidaModeloDatosDireccion();
        }

        private void cmbDireccionProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaModeloDatosDireccion();
        }

        private bool ValidaModeloDatosDireccion()
        {
            this.btnmas.Enabled = HelperValidatorField.ValidarCampos((object)new DatosDireccion()
            {
                cmbDireccionDpto = (this.cmbDireccionDpto.SelectedValue == null ? "" : this.cmbDireccionDpto.SelectedValue.ToString()),
                cmbDireccionProv = (this.cmbDireccionProv.SelectedValue == null ? "" : this.cmbDireccionProv.SelectedValue.ToString()),
                cmbTipoDireccion = (this.cmbTipoDireccion.SelectedValue == null ? "" : this.cmbTipoDireccion.SelectedValue.ToString()),
                txtCallePrincipal = this.txtCallePrincipal.Text.Trim()
            }, this.errorProvider1, (ContainerControl)this);
            return this.btnmas.Enabled;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            this.dataGridView1 = new DataGridView();
            this.txtCallePrincipal = new TextBox();
            this.cmbDireccionProv = new ComboBox();
            this.cmbDireccionDpto = new ComboBox();
            this.cmbTipoDireccion = new ComboBox();
            this.label42 = new Label();
            this.label41 = new Label();
            this.label35 = new Label();
            this.label6 = new Label();
            this.btnmas = new Button();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new ToolStripMenuItem();
            this.errorProvider1 = new ErrorProvider(this.components);
            ((ISupportInitialize)this.dataGridView1).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((ISupportInitialize)this.errorProvider1).BeginInit();
            this.SuspendLayout();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new Point(3, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new Size(776, 96);
            this.dataGridView1.TabIndex = 32;
            this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            this.txtCallePrincipal.CharacterCasing = CharacterCasing.Upper;
            this.txtCallePrincipal.Location = new Point(144, 20);
            this.txtCallePrincipal.Name = "txtCallePrincipal";
            this.txtCallePrincipal.Size = new Size(229, 20);
            this.txtCallePrincipal.TabIndex = 28;
            this.txtCallePrincipal.TextChanged += new EventHandler(this.txtCallePrincipal_TextChanged);
            this.cmbDireccionProv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDireccionProv.FormattingEnabled = true;
            this.cmbDireccionProv.Location = new Point(562, 20);
            this.cmbDireccionProv.Name = "cmbDireccionProv";
            this.cmbDireccionProv.Size = new Size(187, 21);
            this.cmbDireccionProv.TabIndex = 30;
            this.cmbDireccionProv.SelectedIndexChanged += new EventHandler(this.cmbDireccionProv_SelectedIndexChanged);
            this.cmbDireccionDpto.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDireccionDpto.FormattingEnabled = true;
            this.cmbDireccionDpto.Location = new Point(379, 20);
            this.cmbDireccionDpto.Name = "cmbDireccionDpto";
            this.cmbDireccionDpto.Size = new Size(177, 21);
            this.cmbDireccionDpto.TabIndex = 29;
            this.cmbDireccionDpto.SelectedIndexChanged += new EventHandler(this.cmbDireccionDpto_SelectedIndexChanged);
            this.cmbTipoDireccion.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoDireccion.FormattingEnabled = true;
            this.cmbTipoDireccion.Location = new Point(3, 20);
            this.cmbTipoDireccion.Name = "cmbTipoDireccion";
            this.cmbTipoDireccion.Size = new Size(135, 21);
            this.cmbTipoDireccion.TabIndex = 27;
            this.label42.AutoSize = true;
            this.label42.BackColor = Color.Transparent;
            this.label42.ForeColor = Color.White;
            this.label42.Location = new Point(565, 4);
            this.label42.Name = "label42";
            this.label42.Size = new Size(51, 13);
            this.label42.TabIndex = 26;
            this.label42.Text = "Provincia";
            this.label41.AutoSize = true;
            this.label41.BackColor = Color.Transparent;
            this.label41.ForeColor = Color.White;
            this.label41.Location = new Point(382, 4);
            this.label41.Name = "label41";
            this.label41.Size = new Size(74, 13);
            this.label41.TabIndex = 25;
            this.label41.Text = "Departamento";
            this.label35.AutoSize = true;
            this.label35.BackColor = Color.Transparent;
            this.label35.ForeColor = Color.White;
            this.label35.Location = new Point(147, 4);
            this.label35.Name = "label35";
            this.label35.Size = new Size(73, 13);
            this.label35.TabIndex = 24;
            this.label35.Text = "Calle Principal";
            this.label6.AutoSize = true;
            this.label6.BackColor = Color.Transparent;
            this.label6.ForeColor = Color.White;
            this.label6.Location = new Point(7, 4);
            this.label6.Name = "label6";
            this.label6.Size = new Size(91, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Tipo de Dirección";
            this.btnmas.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.btnmas.Location = new Point(755, 17);
            this.btnmas.Name = "btnmas";
            this.btnmas.Size = new Size(23, 23);
            this.btnmas.TabIndex = 31;
            this.btnmas.Text = "+";
            this.btnmas.UseVisualStyleBackColor = true;
            this.btnmas.Click += new EventHandler(this.btnmas_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
            {
        (ToolStripItem) this.eliminarToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(181, 48);
            this.contextMenuStrip1.Click += new EventHandler(this.contextMenuStrip1_Click);
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new Size(180, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.errorProvider1.ContainerControl = (ContainerControl)this;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(48, 63, 105);
            this.Controls.Add((System.Windows.Forms.Control)this.btnmas);
            this.Controls.Add((System.Windows.Forms.Control)this.txtCallePrincipal);
            this.Controls.Add((System.Windows.Forms.Control)this.cmbDireccionProv);
            this.Controls.Add((System.Windows.Forms.Control)this.cmbDireccionDpto);
            this.Controls.Add((System.Windows.Forms.Control)this.cmbTipoDireccion);
            this.Controls.Add((System.Windows.Forms.Control)this.label42);
            this.Controls.Add((System.Windows.Forms.Control)this.label41);
            this.Controls.Add((System.Windows.Forms.Control)this.label35);
            this.Controls.Add((System.Windows.Forms.Control)this.label6);
            this.Controls.Add((System.Windows.Forms.Control)this.dataGridView1);
            this.Name = "Direccion";
            this.Size = new Size(782, 144);
            this.Load += new EventHandler(this.Direccion_Load);
            ((ISupportInitialize)this.dataGridView1).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize)this.errorProvider1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
