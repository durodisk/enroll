using BIODV.Modelo;
using BIODV.Util;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BIODV.Control
{
	public class Direccion : UserControl
	{
		private int rowIndex = 0;

		private BindingList<Address> _Direcciones = new BindingList<Address>();

		private IContainer components = null;

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

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

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

		public Direccion()
		{
			this.InitializeComponent();
		}

		private void btnmas_Click(object sender, EventArgs e)
		{
			DataTable dt = (DataTable)this.dataGridView1.DataSource ?? new DataTable();
			dt.Merge(this.ObtenerTablaDireccion());
			this.dataGridView1.DataSource = dt;
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

		public void CargaDepartamento()
		{
			List<Coder> vCoder = CargarControl.ObtenerLista("PROV");
			CargarControl.Combo(this.cmbDireccionDpto, vCoder, true);
		}

		private void CargaTipoDireccion()
		{
			DataTable dt = (DataTable)this.dataGridView1.DataSource;
			List<Coder> vCoder = CargarControl.ObtenerLista("ADDRESSTYPE");
			string str = (dt == null || dt.Rows.Count == 0 ? "PERM" : "TRAN");
			vCoder = (
				from cust in vCoder
				where cust.Id == str
				select cust).ToList<Coder>();
			CargarControl.Combo(this.cmbTipoDireccion, vCoder, false);
		}

		private void cmbDireccionDpto_SelectedIndexChanged(object sender, EventArgs e)
		{
			string vDepartamento = (string)((ComboBox)sender).SelectedValue;
			List<Coder> items = new List<Coder>();
			if (!string.IsNullOrWhiteSpace(vDepartamento))
			{
				items = CargarControl.ObtenerSubLista("PROV", vDepartamento);
			}
			CargarControl.Combo(this.cmbDireccionProv, items, true);
			this.ValidaModeloDatosDireccion();
		}

		private void cmbDireccionProv_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDireccion();
		}

		private void contextMenuStrip1_Click(object sender, EventArgs e)
		{
			if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
			{
				this.dataGridView1.Rows.RemoveAt(this.rowIndex);
				this.CargaTipoDireccion();
				this.CargaDepartamento();
				this.txtCallePrincipal.Text = "";
			}
		}

		private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.dataGridView1.Rows[e.RowIndex].Selected = true;
				this.rowIndex = e.RowIndex;
				this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
				if (this.rowIndex == this.dataGridView1.Rows.Count - 1)
				{
					this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
					this.contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
				}
			}
		}

		private void Direccion_Load(object sender, EventArgs e)
		{
			DataTable table = new DataTable();
			table.Columns.Add("TipoDireccion");
			table.Columns.Add("CallePrincipal");
			table.Columns.Add("Departamento");
			table.Columns.Add("Provincia");
			table.Columns.Add("IdTipoDireccion");
			table.Columns.Add("IdDepartamento");
			table.Columns.Add("IdProvincia");
			foreach (Address vDir in this._Direcciones)
			{
				DataRow dr = table.NewRow();
				dr["TipoDireccion"] = vDir.Type.Value;
				dr["CallePrincipal"] = vDir.Street;
				dr["Departamento"] = vDir.State.Value;
				dr["Provincia"] = vDir.District.Value;
				dr["IdTipoDireccion"] = vDir.Type.Id;
				dr["IdDepartamento"] = vDir.State.Value;
				dr["IdProvincia"] = vDir.District.Value;
				table.Rows.Add(dr);
			}
			this.dataGridView1.DataSource = table;
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

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
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
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem = new ToolStripMenuItem();
			this.errorProvider1 = new ErrorProvider(this.components);
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.errorProvider1).BeginInit();
			base.SuspendLayout();
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
			this.dataGridView1.Size = new System.Drawing.Size(776, 96);
			this.dataGridView1.TabIndex = 32;
			this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
			this.txtCallePrincipal.CharacterCasing = CharacterCasing.Upper;
			this.txtCallePrincipal.Location = new Point(144, 20);
			this.txtCallePrincipal.Name = "txtCallePrincipal";
			this.txtCallePrincipal.Size = new System.Drawing.Size(229, 20);
			this.txtCallePrincipal.TabIndex = 28;
			this.txtCallePrincipal.TextChanged += new EventHandler(this.txtCallePrincipal_TextChanged);
			this.cmbDireccionProv.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDireccionProv.FormattingEnabled = true;
			this.cmbDireccionProv.Location = new Point(562, 20);
			this.cmbDireccionProv.Name = "cmbDireccionProv";
			this.cmbDireccionProv.Size = new System.Drawing.Size(187, 21);
			this.cmbDireccionProv.TabIndex = 30;
			this.cmbDireccionProv.SelectedIndexChanged += new EventHandler(this.cmbDireccionProv_SelectedIndexChanged);
			this.cmbDireccionDpto.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDireccionDpto.FormattingEnabled = true;
			this.cmbDireccionDpto.Location = new Point(379, 20);
			this.cmbDireccionDpto.Name = "cmbDireccionDpto";
			this.cmbDireccionDpto.Size = new System.Drawing.Size(177, 21);
			this.cmbDireccionDpto.TabIndex = 29;
			this.cmbDireccionDpto.SelectedIndexChanged += new EventHandler(this.cmbDireccionDpto_SelectedIndexChanged);
			this.cmbTipoDireccion.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbTipoDireccion.FormattingEnabled = true;
			this.cmbTipoDireccion.Location = new Point(3, 20);
			this.cmbTipoDireccion.Name = "cmbTipoDireccion";
			this.cmbTipoDireccion.Size = new System.Drawing.Size(135, 21);
			this.cmbTipoDireccion.TabIndex = 27;
			this.label42.AutoSize = true;
			this.label42.BackColor = Color.Transparent;
			this.label42.ForeColor = Color.White;
			this.label42.Location = new Point(565, 4);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(51, 13);
			this.label42.TabIndex = 26;
			this.label42.Text = "Provincia";
			this.label41.AutoSize = true;
			this.label41.BackColor = Color.Transparent;
			this.label41.ForeColor = Color.White;
			this.label41.Location = new Point(382, 4);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(74, 13);
			this.label41.TabIndex = 25;
			this.label41.Text = "Departamento";
			this.label35.AutoSize = true;
			this.label35.BackColor = Color.Transparent;
			this.label35.ForeColor = Color.White;
			this.label35.Location = new Point(147, 4);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(73, 13);
			this.label35.TabIndex = 24;
			this.label35.Text = "Calle Principal";
			this.label6.AutoSize = true;
			this.label6.BackColor = Color.Transparent;
			this.label6.ForeColor = Color.White;
			this.label6.Location = new Point(7, 4);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(91, 13);
			this.label6.TabIndex = 23;
			this.label6.Text = "Tipo de Dirección";
			this.btnmas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnmas.Location = new Point(755, 17);
			this.btnmas.Name = "btnmas";
			this.btnmas.Size = new System.Drawing.Size(23, 23);
			this.btnmas.TabIndex = 31;
			this.btnmas.Text = "+";
			this.btnmas.UseVisualStyleBackColor = true;
			this.btnmas.Click += new EventHandler(this.btnmas_Click);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
			this.contextMenuStrip1.Click += new EventHandler(this.contextMenuStrip1_Click);
			this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
			this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.eliminarToolStripMenuItem.Text = "Eliminar";
			this.errorProvider1.ContainerControl = this;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.Controls.Add(this.btnmas);
			base.Controls.Add(this.txtCallePrincipal);
			base.Controls.Add(this.cmbDireccionProv);
			base.Controls.Add(this.cmbDireccionDpto);
			base.Controls.Add(this.cmbTipoDireccion);
			base.Controls.Add(this.label42);
			base.Controls.Add(this.label41);
			base.Controls.Add(this.label35);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.dataGridView1);
			base.Name = "Direccion";
			base.Size = new System.Drawing.Size(782, 144);
			base.Load += new EventHandler(this.Direccion_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.errorProvider1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private BindingList<Address> ObtenerDirecciones()
		{
			BindingList<Address> vColDirecciones = new BindingList<Address>();
			DataTable dtDirecciones = (DataTable)this.dataGridView1.DataSource;
			if (dtDirecciones != null)
			{
				foreach (DataRow vRow in dtDirecciones.Rows)
				{
					vColDirecciones.Add(new Address()
					{
						Type = new Coder()
						{
							CoderTypeId = "ADDRESSTYPE",
							Id = vRow.Field<string>("IdTipoDireccion"),
							Value = vRow.Field<string>("TipoDireccion")
						},
						Street = vRow.Field<string>("CallePrincipal"),
						State = new Coder()
						{
							CoderTypeId = "PROV",
							Id = vRow.Field<string>("IdDepartamento"),
							Value = vRow.Field<string>("Departamento")
						},
						District = new Coder()
						{
							CoderTypeId = "MUN",
							Id = vRow.Field<string>("IdProvincia"),
							Value = vRow.Field<string>("Provincia")
						}
					});
				}
			}
			return vColDirecciones;
		}

		public DataTable ObtenerTablaDireccion()
		{
			DataTable table = new DataTable();
			table.Columns.Add("TipoDireccion");
			table.Columns.Add("CallePrincipal");
			table.Columns.Add("Departamento");
			table.Columns.Add("Provincia");
			table.Columns.Add("IdTipoDireccion");
			table.Columns.Add("IdDepartamento");
			table.Columns.Add("IdProvincia");
			DataRow dr = table.NewRow();
			dr["TipoDireccion"] = this.cmbTipoDireccion.SelectedItem.ToString();
			dr["CallePrincipal"] = this.txtCallePrincipal.Text;
			dr["Departamento"] = this.cmbDireccionDpto.SelectedItem.ToString();
			dr["Provincia"] = this.cmbDireccionProv.SelectedItem.ToString();
			dr["IdTipoDireccion"] = this.cmbTipoDireccion.SelectedValue;
			dr["IdDepartamento"] = this.cmbDireccionDpto.SelectedValue;
			dr["IdProvincia"] = this.cmbDireccionProv.SelectedValue;
			table.Rows.Add(dr);
			return table;
		}

		private void txtCallePrincipal_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosDireccion();
		}

		private bool ValidaModeloDatosDireccion()
		{
			DatosDireccion vDireccion = new DatosDireccion()
			{
				cmbDireccionDpto = (this.cmbDireccionDpto.SelectedValue == null ? "" : this.cmbDireccionDpto.SelectedValue.ToString()),
				cmbDireccionProv = (this.cmbDireccionProv.SelectedValue == null ? "" : this.cmbDireccionProv.SelectedValue.ToString()),
				cmbTipoDireccion = (this.cmbTipoDireccion.SelectedValue == null ? "" : this.cmbTipoDireccion.SelectedValue.ToString()),
				txtCallePrincipal = this.txtCallePrincipal.Text.Trim()
			};
			this.btnmas.Enabled = Validador.ValidarCampos(vDireccion, this.errorProvider1, this);
			return this.btnmas.Enabled;
		}
	}
}