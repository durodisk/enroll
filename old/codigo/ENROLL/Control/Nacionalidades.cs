using ENROLL.Model;
using ENROLL.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Datys.SIP.Common;

namespace ENROLL.Control
{
  public class Nacionalidades : UserControl
  {
    private int rowIndex = 0;
    private BindingList<Coder> _paises = new BindingList<Coder>();
    private IContainer components = (IContainer) null;
    private Label label9;
    private Button btnmas;
    private DataGridView dataGridView1;
    private ContextMenuStrip contextMenuStrip1;
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

    private BindingList<Coder> ObtenerPaises()
    {
      BindingList<Coder> bindingList = new BindingList<Coder>();
      DataTable dataSource = (DataTable) this.dataGridView1.DataSource;
      if (dataSource != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataSource.Rows)
          bindingList.Add(new Coder()
          {
            CoderTypeId = "COUNTRY",
            Id = row.Field<string>("IdPais"),
            Value = row.Field<string>("Pais")
          });
      }
      return bindingList;
    }

    public Nacionalidades()
    {
      this.InitializeComponent();
      this.CargaPaises();
      this.ValidaModeloDatosNacionalidad();
    }

    private void Nacionalidades_Load(object sender, EventArgs e)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Pais");
      dataTable.Columns.Add("IdPais");
      foreach (Coder paise in (Collection<Coder>) this._paises)
      {
        DataRow row = dataTable.NewRow();
        row["Pais"] = (object) paise.Value;
        row["IdPais"] = (object) paise.Id;
        dataTable.Rows.Add(row);
      }
      this.dataGridView1.DataSource = (object) dataTable;
      this.dataGridView1.Columns["IdPais"].Visible = false;
      this.dataGridView1.Columns["Pais"].Width = 380;
    }

    private void btnmas_Click(object sender, EventArgs e)
    {
      DataTable dataTable = (DataTable) this.dataGridView1.DataSource ?? new DataTable();
      dataTable.Merge(this.ObtenerTablaPaises());
      this.dataGridView1.DataSource = (object) dataTable;
      this.dataGridView1.Columns["IdPais"].Visible = false;
      this.dataGridView1.Columns["Pais"].Width = 380;
      this.CargaPaises();
    }

    public void CargaPaises()
    {
      List<Coder> vCoderp = HelperLoadControl.ObtenerLista("COUNTRY");
      DataTable dataSource = (DataTable) this.dataGridView1.DataSource;
      List<Coder> coderList = this.RemuevePaisesSeleccionados(dataSource, vCoderp);
            HelperLoadControl.Combo(this.cmbNacionalidad, coderList, true);
      this.SeleccionaBolivia(dataSource, coderList);
    }

    private List<Coder> RemuevePaisesSeleccionados(DataTable dt, List<Coder> vCoderp)
    {
      if (dt != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
        {
          string vIdPais = row.Field<string>("IdPais");
          Coder coder = vCoderp.Single<Coder>((Func<Coder, bool>) (r => r.Id == vIdPais));
          vCoderp.Remove(coder);
        }
      }
      return vCoderp;
    }

    private void SeleccionaBolivia(DataTable pPaisesSelecionados, List<Coder> vCoderp)
    {
      DataRow dataRow = (DataRow) null;
      if (pPaisesSelecionados != null)
        dataRow = pPaisesSelecionados.AsEnumerable().Where<DataRow>((Func<DataRow, bool>) (myRow => myRow.Field<string>("IdPais") == "BOL")).FirstOrDefault<DataRow>();
      if (pPaisesSelecionados != null && dataRow != null)
        return;
      this.cmbNacionalidad.SelectedIndex = vCoderp.FindIndex((Predicate<Coder>) (c => c.Id == "BOL")) + 1;
    }

    public DataTable ObtenerTablaPaises()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Pais");
      dataTable.Columns.Add("IdPais");
      DataRow row = dataTable.NewRow();
      row["Pais"] = (object) this.cmbNacionalidad.SelectedItem.ToString();
      row["IdPais"] = (object) this.cmbNacionalidad.SelectedValue.ToString();
      dataTable.Rows.Add(row);
      return dataTable;
    }

    private void contextMenuStrip1_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.Rows[this.rowIndex].IsNewRow)
        return;
      this.dataGridView1.Rows.RemoveAt(this.rowIndex);
      this.CargaPaises();
    }

    private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      this.dataGridView1.Rows[e.RowIndex].Selected = true;
      this.rowIndex = e.RowIndex;
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
      this.contextMenuStrip1.Show((System.Windows.Forms.Control) this.dataGridView1, e.Location);
      this.contextMenuStrip1.Show(Cursor.Position);
    }

    private bool ValidaModeloDatosNacionalidad()
    {
      this.btnmas.Enabled = HelperValidatorField.ValidarCampos((object) new DatosNacionalidad()
      {
        cmbNacionalidad = (this.cmbNacionalidad.SelectedValue == null ? "" : this.cmbNacionalidad.SelectedValue.ToString())
      }, this.errorProvider1, (ContainerControl) this);
      return this.btnmas.Enabled;
    }

    private void cmbNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ValidaModeloDatosNacionalidad();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.label9 = new Label();
      this.btnmas = new Button();
      this.dataGridView1 = new DataGridView();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.eliminarToolStripMenuItem = new ToolStripMenuItem();
      this.errorProvider1 = new ErrorProvider(this.components);
      this.cmbNacionalidad = new ComboBox();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      ((ISupportInitialize) this.errorProvider1).BeginInit();
      this.SuspendLayout();
      this.label9.AutoSize = true;
      this.label9.Font = new Font("Tahoma", 9f, FontStyle.Bold);
      this.label9.Location = new Point(3, 79);
      this.label9.Name = "label9";
      this.label9.Size = new Size(100, 28);
      this.label9.TabIndex = 37;
      this.label9.Text = "Lista de \r\nnacionalidades:";
      this.btnmas.Location = new Point(331, 5);
      this.btnmas.Name = "btnmas";
      this.btnmas.Size = new Size(23, 23);
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
      this.dataGridView1.Size = new Size(250, 150);
      this.dataGridView1.TabIndex = 34;
      this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.eliminarToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(118, 26);
      this.contextMenuStrip1.Click += new EventHandler(this.contextMenuStrip1_Click);
      this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
      this.eliminarToolStripMenuItem.Size = new Size(117, 22);
      this.eliminarToolStripMenuItem.Text = "Eliminar";
      this.errorProvider1.ContainerControl = (ContainerControl) this;
      this.cmbNacionalidad.FormattingEnabled = true;
      this.cmbNacionalidad.Location = new Point(103, 6);
      this.cmbNacionalidad.Name = "cmbNacionalidad";
      this.cmbNacionalidad.Size = new Size(220, 21);
      this.cmbNacionalidad.TabIndex = 38;
      this.cmbNacionalidad.SelectedIndexChanged += new EventHandler(this.cmbNacionalidad_SelectedIndexChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((System.Windows.Forms.Control) this.cmbNacionalidad);
      this.Controls.Add((System.Windows.Forms.Control) this.label9);
      this.Controls.Add((System.Windows.Forms.Control) this.btnmas);
      this.Controls.Add((System.Windows.Forms.Control) this.dataGridView1);
      this.Name = "Nacionalidades";
      this.Size = new Size(358, 185);
      this.Load += new EventHandler(this.Nacionalidades_Load);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      ((ISupportInitialize) this.errorProvider1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
