using BIODV.Modelo;
using BIODV.Util;
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
	public class Nacionalidades : UserControl
	{
		private int rowIndex = 0;

		private BindingList<Coder> _paises = new BindingList<Coder>();

		private IContainer components = null;

		private Label label9;

		private Button btnmas;

		private DataGridView dataGridView1;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem eliminarToolStripMenuItem;

		private ErrorProvider errorProvider1;

		private ComboBox cmbNacionalidad;

		public BindingList<Coder> Paises
		{
			get
			{
				return this.ObtenerPaises();
			}
			set
			{
				this._paises = value;
			}
		}

		public Nacionalidades()
		{
			this.InitializeComponent();
			this.CargaPaises();
			this.ValidaModeloDatosNacionalidad();
		}

		private void btnmas_Click(object sender, EventArgs e)
		{
			DataTable dt = (DataTable)this.dataGridView1.DataSource ?? new DataTable();
			dt.Merge(this.ObtenerTablaPaises());
			this.dataGridView1.DataSource = dt;
			this.dataGridView1.Columns["IdPais"].Visible = false;
			this.dataGridView1.Columns["Pais"].Width = 380;
			this.CargaPaises();
		}

		public void CargaPaises()
		{
			List<Coder> vCoderp = CargarControl.ObtenerLista("COUNTRY");
			DataTable dt = (DataTable)this.dataGridView1.DataSource;
			vCoderp = this.RemuevePaisesSeleccionados(dt, vCoderp);
			CargarControl.Combo(this.cmbNacionalidad, vCoderp, true);
			this.SeleccionaBolivia(dt, vCoderp);
		}

		private void cmbNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosNacionalidad();
		}

		private void contextMenuStrip1_Click(object sender, EventArgs e)
		{
			if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
			{
				this.dataGridView1.Rows.RemoveAt(this.rowIndex);
				this.CargaPaises();
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
			this.label9 = new Label();
			this.btnmas = new Button();
			this.dataGridView1 = new DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem = new ToolStripMenuItem();
			this.errorProvider1 = new ErrorProvider(this.components);
			this.cmbNacionalidad = new ComboBox();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.errorProvider1).BeginInit();
			base.SuspendLayout();
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label9.Location = new Point(3, 79);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 28);
			this.label9.TabIndex = 37;
			this.label9.Text = "Lista de \r\nnacionalidades:";
			this.btnmas.Location = new Point(331, 5);
			this.btnmas.Name = "btnmas";
			this.btnmas.Size = new System.Drawing.Size(23, 23);
			this.btnmas.TabIndex = 36;
			this.btnmas.Text = "+";
			this.btnmas.UseVisualStyleBackColor = true;
			this.btnmas.Click += new EventHandler(this.btnmas_Click);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Location = new Point(103, 29);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(250, 150);
			this.dataGridView1.TabIndex = 34;
			this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
			this.contextMenuStrip1.Click += new EventHandler(this.contextMenuStrip1_Click);
			this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
			this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.eliminarToolStripMenuItem.Text = "Eliminar";
			this.errorProvider1.ContainerControl = this;
			this.cmbNacionalidad.FormattingEnabled = true;
			this.cmbNacionalidad.Location = new Point(103, 6);
			this.cmbNacionalidad.Name = "cmbNacionalidad";
			this.cmbNacionalidad.Size = new System.Drawing.Size(220, 21);
			this.cmbNacionalidad.TabIndex = 38;
			this.cmbNacionalidad.SelectedIndexChanged += new EventHandler(this.cmbNacionalidad_SelectedIndexChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.cmbNacionalidad);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.btnmas);
			base.Controls.Add(this.dataGridView1);
			base.Name = "Nacionalidades";
			base.Size = new System.Drawing.Size(358, 185);
			base.Load += new EventHandler(this.Nacionalidades_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.errorProvider1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void Nacionalidades_Load(object sender, EventArgs e)
		{
			DataTable table = new DataTable();
			table.Columns.Add("Pais");
			table.Columns.Add("IdPais");
			foreach (Coder vNombre in this._paises)
			{
				DataRow dr = table.NewRow();
				dr["Pais"] = vNombre.Value;
				dr["IdPais"] = vNombre.Id;
				table.Rows.Add(dr);
			}
			this.dataGridView1.DataSource = table;
			this.dataGridView1.Columns["IdPais"].Visible = false;
			this.dataGridView1.Columns["Pais"].Width = 380;
		}

		private BindingList<Coder> ObtenerPaises()
		{
			BindingList<Coder> vColPaises = new BindingList<Coder>();
			DataTable dtDirecciones = (DataTable)this.dataGridView1.DataSource;
			if (dtDirecciones != null)
			{
				foreach (DataRow vRow in dtDirecciones.Rows)
				{
					vColPaises.Add(new Coder()
					{
						CoderTypeId = "COUNTRY",
						Id = vRow.Field<string>("IdPais"),
						Value = vRow.Field<string>("Pais")
					});
				}
			}
			return vColPaises;
		}

		public DataTable ObtenerTablaPaises()
		{
			DataTable table = new DataTable();
			table.Columns.Add("Pais");
			table.Columns.Add("IdPais");
			DataRow dr = table.NewRow();
			dr["Pais"] = this.cmbNacionalidad.SelectedItem.ToString();
			dr["IdPais"] = this.cmbNacionalidad.SelectedValue.ToString();
			table.Rows.Add(dr);
			return table;
		}

		private List<Coder> RemuevePaisesSeleccionados(DataTable dt, List<Coder> vCoderp)
		{
			if (dt != null)
			{
				foreach (DataRow vPais in dt.Rows)
				{
					string str = vPais.Field<string>("IdPais");
					Coder vPaisRegistrado = vCoderp.Single<Coder>((Coder r) => r.Id == str);
					vCoderp.Remove(vPaisRegistrado);
				}
			}
			return vCoderp;
		}

		private void SeleccionaBolivia(DataTable pPaisesSelecionados, List<Coder> vCoderp)
		{
			DataRow vIdPaisBol = null;
			if (pPaisesSelecionados != null)
			{
				vIdPaisBol = pPaisesSelecionados.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("IdPais") == "BOL").FirstOrDefault<DataRow>();
			}
			if ((pPaisesSelecionados == null ? true : vIdPaisBol == null))
			{
				int indexBol = vCoderp.FindIndex((Coder c) => c.Id == "BOL");
				this.cmbNacionalidad.SelectedIndex = indexBol + 1;
			}
		}

		private bool ValidaModeloDatosNacionalidad()
		{
			DatosNacionalidad vNacion = new DatosNacionalidad()
			{
				cmbNacionalidad = (this.cmbNacionalidad.SelectedValue == null ? "" : this.cmbNacionalidad.SelectedValue.ToString())
			};
			this.btnmas.Enabled = Validador.ValidarCampos(vNacion, this.errorProvider1, this);
			return this.btnmas.Enabled;
		}
	}
}