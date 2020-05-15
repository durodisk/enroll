using BIODV.Modelo;
using BIODV.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BIODV.Control
{
	public class Alias : UserControl
	{
		private int rowIndex = 0;

		private BindingList<string> _Alias = new BindingList<string>();

		private IContainer components = null;

		private TextBox txtAlias;

		private DataGridView dataGridView1;

		private Button btnmas;

		private Label label9;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem eliminarToolStripMenuItem;

		private ErrorProvider errorProvider1;

		public BindingList<string> Nombres
		{
			get
			{
				return this.ObtenerAlias();
			}
			set
			{
				this._Alias = value;
			}
		}

		public Alias()
		{
			this.InitializeComponent();
			this.ValidaModeloDatosAlias();
		}

		private void Alias_Load(object sender, EventArgs e)
		{
			DataTable table = new DataTable();
			table.Columns.Add("Alias");
			foreach (string vNombre in this._Alias)
			{
				DataRow dr = table.NewRow();
				dr["Alias"] = vNombre;
				table.Rows.Add(dr);
			}
			this.dataGridView1.DataSource = table;
			this.dataGridView1.Columns["Alias"].Width = 380;
		}

		private void btnmas_Click(object sender, EventArgs e)
		{
			DataTable dt = (DataTable)this.dataGridView1.DataSource ?? new DataTable();
			dt.Merge(this.ObtenerTablaAlias());
			this.dataGridView1.DataSource = dt;
			this.dataGridView1.Columns["Alias"].Width = 380;
			this.txtAlias.Text = "";
		}

		private void contextMenuStrip1_Click(object sender, EventArgs e)
		{
			if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
			{
				this.dataGridView1.Rows.RemoveAt(this.rowIndex);
			}
		}

		private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.dataGridView1.Rows[e.RowIndex].Selected = true;
				this.rowIndex = e.RowIndex;
				this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
				this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
				this.contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
			}
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
			this.txtAlias = new TextBox();
			this.dataGridView1 = new DataGridView();
			this.btnmas = new Button();
			this.label9 = new Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem = new ToolStripMenuItem();
			this.errorProvider1 = new ErrorProvider(this.components);
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.errorProvider1).BeginInit();
			base.SuspendLayout();
			this.txtAlias.CharacterCasing = CharacterCasing.Upper;
			this.txtAlias.Location = new Point(98, 8);
			this.txtAlias.Name = "txtAlias";
			this.txtAlias.Size = new System.Drawing.Size(220, 20);
			this.txtAlias.TabIndex = 0;
			this.txtAlias.TextChanged += new EventHandler(this.txtAlias_TextChanged);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Location = new Point(98, 30);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(250, 150);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
			this.btnmas.Location = new Point(325, 7);
			this.btnmas.Name = "btnmas";
			this.btnmas.Size = new System.Drawing.Size(23, 23);
			this.btnmas.TabIndex = 32;
			this.btnmas.Text = "+";
			this.btnmas.UseVisualStyleBackColor = true;
			this.btnmas.Click += new EventHandler(this.btnmas_Click);
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label9.Location = new Point(3, 96);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(89, 14);
			this.label9.TabIndex = 33;
			this.label9.Text = "Lista de alias:";
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
			this.contextMenuStrip1.Click += new EventHandler(this.contextMenuStrip1_Click);
			this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
			this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.eliminarToolStripMenuItem.Text = "Eliminar";
			this.errorProvider1.ContainerControl = this;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.label9);
			base.Controls.Add(this.btnmas);
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.txtAlias);
			base.Name = "Alias";
			base.Size = new System.Drawing.Size(354, 185);
			base.Load += new EventHandler(this.Alias_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.errorProvider1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private BindingList<string> ObtenerAlias()
		{
			BindingList<string> vColDirecciones = new BindingList<string>();
			DataTable dtDirecciones = (DataTable)this.dataGridView1.DataSource;
			if (dtDirecciones != null)
			{
				foreach (DataRow vRow in dtDirecciones.Rows)
				{
					vColDirecciones.Add(vRow.Field<string>("Alias"));
				}
			}
			return vColDirecciones;
		}

		public DataTable ObtenerTablaAlias()
		{
			DataTable table = new DataTable();
			table.Columns.Add("Alias");
			DataRow dr = table.NewRow();
			dr["Alias"] = this.txtAlias.Text;
			table.Rows.Add(dr);
			return table;
		}

		private void txtAlias_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosAlias();
		}

		private bool ValidaModeloDatosAlias()
		{
			DatosAlias vDireccion = new DatosAlias()
			{
				txtAlias = this.txtAlias.Text.Trim()
			};
			this.btnmas.Enabled = Validador.ValidarCampos(vDireccion, this.errorProvider1, this);
			return this.btnmas.Enabled;
		}
	}
}