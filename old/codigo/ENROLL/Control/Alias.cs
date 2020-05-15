using ENROLL.Model;
using ENROLL.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ENROLL.Control
{
  public class Alias : UserControl
  {
    private int rowIndex = 0;
    private BindingList<string> _Alias = new BindingList<string>();
    private IContainer components = (IContainer) null;
    private TextBox txtAlias;
    private DataGridView dataGridView1;
    private Button btnmas;
    private Label label9;
    private ContextMenuStrip contextMenuStrip1;
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

    private BindingList<string> ObtenerAlias()
    {
      BindingList<string> bindingList = new BindingList<string>();
      DataTable dataSource = (DataTable) this.dataGridView1.DataSource;
      if (dataSource != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataSource.Rows)
          bindingList.Add(row.Field<string>(nameof (Alias)));
      }
      return bindingList;
    }

    public Alias()
    {
      this.InitializeComponent();
      this.ValidaModeloDatosAlias();
    }

    private void Alias_Load(object sender, EventArgs e)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add(nameof (Alias));
      foreach (string alia in (Collection<string>) this._Alias)
      {
        DataRow row = dataTable.NewRow();
        row[nameof (Alias)] = (object) alia;
        dataTable.Rows.Add(row);
      }
      this.dataGridView1.DataSource = (object) dataTable;
      this.dataGridView1.Columns[nameof (Alias)].Width = 380;
    }

    private void btnmas_Click(object sender, EventArgs e)
    {
      DataTable dataTable = (DataTable) this.dataGridView1.DataSource ?? new DataTable();
      dataTable.Merge(this.ObtenerTablaAlias());
      this.dataGridView1.DataSource = (object) dataTable;
      this.dataGridView1.Columns[nameof (Alias)].Width = 380;
      this.txtAlias.Text = "";
    }

    public DataTable ObtenerTablaAlias()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add(nameof (Alias));
      DataRow row = dataTable.NewRow();
      row[nameof (Alias)] = (object) this.txtAlias.Text;
      dataTable.Rows.Add(row);
      return dataTable;
    }

    private void contextMenuStrip1_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.Rows[this.rowIndex].IsNewRow)
        return;
      this.dataGridView1.Rows.RemoveAt(this.rowIndex);
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

    private bool ValidaModeloDatosAlias()
    {
      this.btnmas.Enabled = HelperValidatorField.ValidarCampos((object) new ModelAlias()
      {
        txtAlias = this.txtAlias.Text.Trim()
      }, this.errorProvider1, (ContainerControl) this);
      return this.btnmas.Enabled;
    }

    private void txtAlias_TextChanged(object sender, EventArgs e)
    {
      this.ValidaModeloDatosAlias();
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
      this.txtAlias = new TextBox();
      this.dataGridView1 = new DataGridView();
      this.btnmas = new Button();
      this.label9 = new Label();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.eliminarToolStripMenuItem = new ToolStripMenuItem();
      this.errorProvider1 = new ErrorProvider(this.components);
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      ((ISupportInitialize) this.errorProvider1).BeginInit();
      this.SuspendLayout();
      this.txtAlias.CharacterCasing = CharacterCasing.Upper;
      this.txtAlias.Location = new Point(98, 8);
      this.txtAlias.Name = "txtAlias";
      this.txtAlias.Size = new Size(220, 20);
      this.txtAlias.TabIndex = 0;
      this.txtAlias.TextChanged += new EventHandler(this.txtAlias_TextChanged);
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersVisible = false;
      this.dataGridView1.Location = new Point(98, 30);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.Size = new Size(250, 150);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
      this.btnmas.Location = new Point(325, 7);
      this.btnmas.Name = "btnmas";
      this.btnmas.Size = new Size(23, 23);
      this.btnmas.TabIndex = 32;
      this.btnmas.Text = "+";
      this.btnmas.UseVisualStyleBackColor = true;
      this.btnmas.Click += new EventHandler(this.btnmas_Click);
      this.label9.AutoSize = true;
      this.label9.Font = new Font("Tahoma", 9f, FontStyle.Bold);
      this.label9.Location = new Point(3, 96);
      this.label9.Name = "label9";
      this.label9.Size = new Size(89, 14);
      this.label9.TabIndex = 33;
      this.label9.Text = "Lista de alias:";
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((System.Windows.Forms.Control) this.label9);
      this.Controls.Add((System.Windows.Forms.Control) this.btnmas);
      this.Controls.Add((System.Windows.Forms.Control) this.dataGridView1);
      this.Controls.Add((System.Windows.Forms.Control) this.txtAlias);
      this.Name = "Alias";
      this.Size = new Size(354, 185);
      this.Load += new EventHandler(this.Alias_Load);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      ((ISupportInitialize) this.errorProvider1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
