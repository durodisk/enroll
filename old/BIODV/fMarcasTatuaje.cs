using BIODV.Modelo;
using BIODV.Util;
using Datys.Enroll.Core;
using Datys.SIP.Common;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BIODV
{
	public class fMarcasTatuaje : Form
	{
		private Serializer ser = new Serializer();

		private Image<Bgr, byte> imgInput;

		private Rectangle rect;

		private Point StartLocation;

		private Point EndLcation;

		private int rowIndex = 0;

		private int cantidad = 0;

		private string _nombrePB;

		private DataTable dt;

		private bool _seleccionado = false;

		private bool _selecting;

		private float escala;

		private float saldoX;

		private float saldoY;

		private Image<Bgr, byte> temp;

		private IContainer components = null;

		private TabControl tabControl1;

		private TabPage tabPage2;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem eliminarToolStripMenuItem;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem eliminarToolStripMenuItem1;

		private ErrorProvider errorProvider1;

		private Panel panel3;

		private Button btnAnterior;

		private Button btnguardar;

		private Button btnsiguiente;

		private Button btncancelar;

		private TabPage tabPage1;

		private Panel panel1;

		private Label label9;

		private Button btnañadirM;

		private Label label3;

		private Label label2;

		private ComboBox cmbAreaCuerpo;

		private ComboBox cmbTipoMarca;

		private DataGridView dataGridView1;

		private Button btnCargarMarcas;

		private PictureBox pbImagenMarca;

		private Label label12;

		private ComboBox cmbColor;

		private Label label7;

		private Label label10;

		private Panel panel2;

		private Label label5;

		private Button btnañadirT;

		private ComboBox cmbAreaCuerpoT;

		private ComboBox cmbMotivo;

		private DataGridView dataGridView2;

		private Label label6;

		private Button btnCargarTatuajes;

		private PictureBox pbImagenTatuaje;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;

		private ToolStripMenuItem eliminarToolStripMenuItem3;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;

		private ToolStripMenuItem eliminarToolStripMenuItem2;

		private Button btnfotoM;

		private Button btnFoto2;

		private Label label11;

		private TextBox txtNombreT;

		private ToolStripMenuItem editarToolStripMenuItem;

		private TableLayoutPanel tableLayoutPanel2;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label13;

		private TableLayoutPanel tableLayoutPanel11;

		private PictureBox pictureBox1;

		private CheckBox chkNuevo;

		private CheckBox chkanadir;

		private Button button1;

		public BindingList<MarkData> Marcas
		{
			get;
			set;
		}

		public BindingList<TattooData> Tatuajes
		{
			get;
			set;
		}

		public fMarcasTatuaje()
		{
			this.InitializeComponent();
		}

		private void AgregaFilaGrillaMarcas()
		{
			this.dt = (DataTable)this.dataGridView1.DataSource;
			if (this.dt == null)
			{
				this.dt = new DataTable();
			}
			this.dt.Merge(this.ObtenerTablaMarca());
			this.dataGridView1.DataSource = this.dt;
			this.dataGridView1.Columns["IdTipoMarca"].Visible = false;
			this.dataGridView1.Columns["IdAreaCuerpo"].Visible = false;
			this.dataGridView1.Columns["TipoMarca"].Width = 170;
			this.dataGridView1.Columns["AreaCuerpo"].Width = 170;
			this.dataGridView1.Columns["Cantidad"].Width = 120;
		}

		private void AgregaFilaGrillaTatuajes()
		{
			this.dt = (DataTable)this.dataGridView2.DataSource;
			if (this.dt == null)
			{
				this.dt = new DataTable();
			}
			this.dt.Merge(this.ObtenerTablaTatuajes());
			this.dataGridView2.DataSource = this.dt;
			this.dataGridView2.Columns["IdMotivo"].Visible = false;
			this.dataGridView2.Columns["IdAreaCuerpo"].Visible = false;
			this.dataGridView2.Columns["IdColor"].Visible = false;
			this.dataGridView2.Columns["Nombre"].Width = 145;
			this.dataGridView2.Columns["Motivo"].Width = 145;
			this.dataGridView2.Columns["AreaCuerpo"].Width = 145;
			this.dataGridView2.Columns["Color"].Width = 145;
			this.dataGridView2.Columns["Cantidad"].Width = 110;
		}

		private bool Alerta(string titulo, string mensaje, bool confirmacion)
		{
			bool accion = false;
			if (!confirmacion)
			{
				MessageBox.Show(mensaje, titulo);
			}
			else
			{
				accion = (MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo).ToString() != "Yes" ? false : true);
			}
			return accion;
		}

		private void btnanterior_Click(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedIndex == 1)
			{
				this.btnAnterior.Enabled = false;
			}
			this.tabControl1.SelectTab(this.tabControl1.SelectedIndex - 1);
			this.btnsiguiente.Enabled = true;
			this.btnsiguiente.Text = "Siguiente";
		}

		private void btnañadirM_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			if (this.ValidaModeloDatosMarcas())
			{
				this.AgregaFilaGrillaMarcas();
				this.LlenaMarcas();
				this.cmbAreaCuerpo.SelectedIndex = 0;
				this.cmbTipoMarca.SelectedIndex = 0;
				this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
				this.rowIndex = this.dt.Rows.Count - 1;
				this.CargaImagenesMarcas();
				this._seleccionado = true;
				this.cantidad = 0;
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void btnañadirT_Click(object sender, EventArgs e)
		{
			if (this.ValidaModeloDatosTatuajes())
			{
				this.AgregaFilaGrillaTatuajes();
				this.LlenaTatuajes();
				this.txtNombreT.Text = "";
				this.cmbMotivo.SelectedIndex = 0;
				this.cmbAreaCuerpoT.SelectedIndex = 0;
				this.cmbColor.SelectedIndex = 0;
				this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
				this.rowIndex = this.dt.Rows.Count - 1;
				this.CargaImagenesTatuajes();
			}
		}

		private void btncancelar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "Se guardaran los datos y se saldra del formulario,\nEsta seguro de realizar esta operacion ? ", true))
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				fEnrolar.PersonaCapturada.RecordData.MarkDatas = this.Marcas;
				fEnrolar.PersonaCapturada.RecordData.TattooDatas = this.Tatuajes;
				(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				(new fEnrolar()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void btnCargarMarcas_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			try
			{
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.imgInput = new Image<Bgr, byte>(ofd.FileName);
					this.pbImagenMarca.Image = this.imgInput.Bitmap;
					this.pbImagenMarca.Enabled = true;
					this.CargaCarril();
				}
			}
			catch
			{
			}
		}

		private void btnCargarTatuajes_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.imgInput = new Image<Bgr, byte>(ofd.FileName);
					this.pbImagenTatuaje.Image = this.imgInput.Bitmap;
					this.pbImagenTatuaje.Enabled = true;
				}
			}
			catch
			{
			}
		}

		private void btnFoto2_Click(object sender, EventArgs e)
		{
			try
			{
				Foto frm = new Foto();
				if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
				{
				}
				frm.Dispose();
				if (Foto.Captura != null)
				{
					this.imgInput = new Image<Bgr, byte>(Foto.Captura);
					this.pbImagenTatuaje.Image = this.imgInput.Bitmap;
					this.pbImagenTatuaje.Enabled = true;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnfotoM_Click(object sender, EventArgs e)
		{
			try
			{
				Foto frm = new Foto();
				if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
				{
				}
				frm.Dispose();
				if (Foto.Captura != null)
				{
					this.imgInput = new Image<Bgr, byte>(Foto.Captura);
					this.pbImagenMarca.Image = this.imgInput.Bitmap;
					this.pbImagenMarca.Enabled = true;
					this.CargaCarril();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnguardar_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				fEnrolar.PersonaCapturada.RecordData.MarkDatas = this.Marcas;
				fEnrolar.PersonaCapturada.RecordData.TattooDatas = this.Tatuajes;
				(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
			}
			catch
			{
			}
		}

		private void btnsiguiente_Click(object sender, EventArgs e)
		{
			try
			{
				int selectedIndex = this.tabControl1.SelectedIndex;
				if (selectedIndex == 0)
				{
					this.btnAnterior.Enabled = true;
					this.tabControl1.SelectTab(1);
					this.CargaPestanaTatuajes();
					this.btnsiguiente.Text = "Finalizar";
					this.btnsiguiente.Enabled = this.ValidaGrilla((DataTable)this.dataGridView2.DataSource);
				}
				else if (selectedIndex == 1)
				{
					fEnrolar.PersonaCapturada.RecordData.MarkDatas = this.Marcas;
					fEnrolar.PersonaCapturada.RecordData.TattooDatas = this.Tatuajes;
					base.Close();
				}
				this.ser.SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch
			{
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ? , puede que exista datos sin ser guardados", true))
			{
				(new fEnrolar()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		public void CargaAreaCuerpo()
		{
			List<Coder> vCoderp = CargarControl.ObtenerLista("BODYAREA");
			CargarControl.Combo(this.cmbAreaCuerpo, vCoderp, true);
			CargarControl.Combo(this.cmbAreaCuerpoT, vCoderp, true);
		}

		private void CargaCarril()
		{
			try
			{
				PictureBox p = new PictureBox()
				{
					Name = string.Concat("pb", this.cantidad.ToString()),
					Image = this.imgInput.Bitmap,
					Location = new Point(20 + this.cantidad * 100, 20),
					Size = new System.Drawing.Size(90, 90),
					SizeMode = PictureBoxSizeMode.Zoom,
					BorderStyle = BorderStyle.FixedSingle
				};
				p.MouseClick += new MouseEventHandler(this.Pb_MouseClick);
				this.dt = (DataTable)this.dataGridView1.DataSource;
				if (this.dt == null)
				{
					this.panel1.Controls.Add(p);
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
					this.cantidad++;
					this.chkanadir.Checked = true;
				}
				else if (this.chkanadir.Checked)
				{
					this.cantidad = Convert.ToInt32(this.dt.Rows[this.rowIndex]["Cantidad"]);
					this.panel1.Controls.Add(p);
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
					this.cantidad++;
					this.dt = (DataTable)this.dataGridView1.DataSource;
					this.dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
					this.dataGridView1.DataSource = this.dt;
					this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
					if (this._seleccionado)
					{
						this.Marcas[this.rowIndex].ListImages.Add(new ImageData()
						{
							NormalizedImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.imgInput.Bitmap, 800))
						});
					}
					this.pbImagenMarca.Enabled = false;
					this.CargaImagenesMarcas();
				}
				else if (!this.chkNuevo.Checked)
				{
					this.panel1.Controls.Add(p);
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
					this.cantidad++;
					this.dt = (DataTable)this.dataGridView1.DataSource;
					this.dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
					this.dataGridView1.DataSource = this.dt;
					this.btnsiguiente.Enabled = this.ValidaGrilla(this.dt);
					if (this._seleccionado)
					{
						this.Marcas[this.rowIndex].ListImages.Add(new ImageData()
						{
							NormalizedImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.imgInput.Bitmap, 1500))
						});
					}
					this.pbImagenMarca.Enabled = false;
				}
				else
				{
					if (this.cantidad == 0)
					{
						this.dataGridView1.ClearSelection();
						this.panel1.Controls.Clear();
						this.pbImagenMarca.Image = null;
						this.cantidad = 0;
						this._seleccionado = false;
					}
					this._seleccionado = false;
					this.panel1.Controls.Add(p);
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
					this.cantidad++;
				}
			}
			catch (Exception exception)
			{
			}
		}

		public void CargaColor()
		{
			List<Coder> vCoderp = CargarControl.ObtenerLista("TATOOCOLOR");
			CargarControl.Combo(this.cmbColor, vCoderp, true);
		}

		private void CargaImagenesMarcas()
		{
			this.cantidad = 0;
			this.pbImagenMarca.Image = null;
			this.panel1.Controls.Clear();
			if ((this.Marcas == null ? false : this.Marcas.Any<MarkData>()))
			{
				MarkData vMarca = this.Marcas[this.rowIndex];
				if ((vMarca.ListImages == null ? false : vMarca.ListImages.Any<ImageData>()))
				{
					foreach (ImageData img in vMarca.ListImages)
					{
						PictureBox p = new PictureBox()
						{
							Name = string.Concat("pb", this.cantidad.ToString()),
							Image = this.ser.byteArrayToImage(img.NormalizedImageArr),
							Location = new Point(20 + this.cantidad * 100, 20),
							Size = new System.Drawing.Size(90, 90),
							SizeMode = PictureBoxSizeMode.Zoom,
							BorderStyle = BorderStyle.FixedSingle
						};
						p.MouseClick += new MouseEventHandler(this.Pb_MouseClick);
						this.panel1.Controls.Add(p);
						this.cantidad++;
					}
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
				}
				int c = 0;
				foreach (DataGridViewRow row in (IEnumerable)this.dataGridView1.Rows)
				{
					row.Selected = c == this.rowIndex;
					c++;
				}
			}
		}

		private void CargaImagenesTatuajes()
		{
			this.cantidad = 0;
			this.pbImagenTatuaje.Image = null;
			this.panel2.Controls.Clear();
			if ((this.Tatuajes == null ? true : !this.Tatuajes.Any<TattooData>()))
			{
				this.btnCargarTatuajes.Enabled = false;
			}
			else
			{
				TattooData vTatuajes = this.Tatuajes[this.rowIndex];
				if ((vTatuajes.ListImages == null ? false : vTatuajes.ListImages.Any<ImageData>()))
				{
					foreach (ImageData img in vTatuajes.ListImages)
					{
						PictureBox p = new PictureBox()
						{
							Name = string.Concat("pbt", this.cantidad.ToString()),
							Image = this.ser.byteArrayToImage(img.NormalizedImageArr),
							Location = new Point(20 + this.cantidad * 100, 20),
							Size = new System.Drawing.Size(90, 90),
							SizeMode = PictureBoxSizeMode.Zoom,
							BorderStyle = BorderStyle.FixedSingle
						};
						p.MouseClick += new MouseEventHandler(this.Pbt_MouseClick);
						this.panel2.Controls.Add(p);
						this.cantidad++;
					}
					this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
				}
				int c = 0;
				foreach (DataGridViewRow row in (IEnumerable)this.dataGridView2.Rows)
				{
					row.Selected = c == this.rowIndex;
					c++;
				}
				this.btnCargarTatuajes.Enabled = true;
			}
		}

		public void CargaMotivo()
		{
			List<Coder> vCoderp = CargarControl.ObtenerLista("TATOOMOTIVE");
			CargarControl.Combo(this.cmbMotivo, vCoderp, true);
		}

		private void CargaPestanaMarcas()
		{
			this.pbImagenMarca.Enabled = false;
			this.cmbTipoMarca.SelectedIndex = 0;
			this.cmbAreaCuerpo.SelectedIndex = 0;
			DataTable dt = this.ObtenerDataTableMarcas();
			this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
			if (dt.Rows.Count > 0)
			{
				this.dataGridView1.DataSource = dt;
				this.dataGridView1.Columns["IdTipoMarca"].Visible = false;
				this.dataGridView1.Columns["IdAreaCuerpo"].Visible = false;
				this.dataGridView1.Columns["TipoMarca"].Width = 170;
				this.dataGridView1.Columns["AreaCuerpo"].Width = 170;
				this.dataGridView1.Columns["Cantidad"].Width = 120;
				this.rowIndex = dt.Rows.Count - 1;
				this.CargaImagenesMarcas();
				this._seleccionado = true;
			}
		}

		private void CargaPestanaTatuajes()
		{
			this.btnCargarTatuajes.Enabled = false;
			this.pbImagenTatuaje.Enabled = false;
			this.txtNombreT.Text = "";
			this.cmbMotivo.SelectedIndex = 0;
			this.cmbAreaCuerpoT.SelectedIndex = 0;
			this.cmbColor.SelectedIndex = 0;
			DataTable dt = this.ObtenerDataTableTatuajes();
			if (dt.Rows.Count > 0)
			{
				this.dataGridView2.DataSource = dt;
				this.dataGridView2.Columns["IdMotivo"].Visible = false;
				this.dataGridView2.Columns["IdAreaCuerpo"].Visible = false;
				this.dataGridView2.Columns["IdColor"].Visible = false;
				this.dataGridView2.Columns["Nombre"].Width = 145;
				this.dataGridView2.Columns["Motivo"].Width = 145;
				this.dataGridView2.Columns["AreaCuerpo"].Width = 145;
				this.dataGridView2.Columns["Color"].Width = 145;
				this.dataGridView2.Columns["Cantidad"].Width = 110;
				this.rowIndex = dt.Rows.Count - 1;
				this.CargaImagenesTatuajes();
			}
		}

		public void CargaTipoMarca()
		{
			List<Coder> vCoderp = CargarControl.ObtenerLista("MARKTYPE");
			CargarControl.Combo(this.cmbTipoMarca, vCoderp, true);
		}

		private void chkanadir_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkanadir.Checked)
			{
				this.rowIndex = -1;
				this.chkNuevo.Checked = true;
			}
			else
			{
				this.chkNuevo.Checked = false;
			}
		}

		private void chkNuevo_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkNuevo.Checked)
			{
				this.chkanadir.Checked = true;
			}
			else
			{
				this.rowIndex = -1;
				this.chkanadir.Checked = false;
				this.dataGridView1.ClearSelection();
				this.panel1.Controls.Clear();
				this.pbImagenMarca.Image = null;
				this.cantidad = 0;
				this._seleccionado = false;
			}
		}

		private void cmbAreaCuerpo_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
			if (this._seleccionado)
			{
				this.dataGridView1.ClearSelection();
				this.panel1.Controls.Clear();
				this.pbImagenMarca.Image = null;
				this.cantidad = 0;
				this._seleccionado = false;
			}
		}

		private void cmbAreaCuerpoT_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
		}

		private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
		}

		private void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
		}

		private void cmbTipoMarca_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
			if (this._seleccionado)
			{
				this.dataGridView1.ClearSelection();
				this.panel1.Controls.Clear();
				this.pbImagenMarca.Image = null;
				this.cantidad = 0;
				this._seleccionado = false;
			}
			Coder vCoder = (Coder)this.cmbTipoMarca.SelectedItem;
			if ((vCoder.Id == "BODYMARKTYPE" || vCoder.Id == "LEFTPROFILEMARKTYPE" ? false : vCoder.Id != "RIGHTPROFILEMARKTYPE"))
			{
				this.cmbAreaCuerpo.Enabled = true;
			}
			else
			{
				if (vCoder.Id == "BODYMARKTYPE")
				{
					this.cmbAreaCuerpo.SelectedValue = "BODYAREA";
				}
				if ((vCoder.Id == "LEFTPROFILEMARKTYPE" ? true : vCoder.Id == "RIGHTPROFILEMARKTYPE"))
				{
					this.cmbAreaCuerpo.SelectedValue = "FACEAREA";
				}
				this.cmbAreaCuerpo.Enabled = false;
			}
		}

		private void contextMenuStrip2_Click(object sender, EventArgs e)
		{
			try
			{
				this.cantidad = 0;
				int itemeliminado = 0;
				foreach (PictureBox item in this.panel1.Controls.OfType<PictureBox>())
				{
					if (item.Name != this._nombrePB)
					{
						this.cantidad++;
					}
					else
					{
						this.panel1.Controls.Remove(item);
						itemeliminado = this.cantidad;
						break;
					}
				}
				this.pbImagenMarca.Image = null;
				int contador = 0;
				foreach (PictureBox item in this.panel1.Controls.OfType<PictureBox>())
				{
					if (contador >= itemeliminado)
					{
						Point location = item.Location;
						int x = location.X - 100;
						location = item.Location;
						item.Location = new Point(x, location.Y);
						item.Name = string.Concat("pb", contador.ToString());
					}
					contador++;
				}
				if (this.rowIndex != -1)
				{
					this.Marcas[this.rowIndex].ListImages.RemoveAt(this.cantidad);
					DataTable dt = (DataTable)this.dataGridView1.DataSource;
					dt.Rows[this.rowIndex]["Cantidad"] = contador;
					this.dataGridView1.DataSource = dt;
					this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
				}
				this.cantidad = contador;
				if (this.cantidad > itemeliminado)
				{
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
				}
				else if (itemeliminado - 1 >= 0)
				{
					this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
				}
			}
			catch
			{
			}
		}

		private void contextMenuStrip3_Click(object sender, EventArgs e)
		{
			if (!this.dataGridView2.Rows[this.rowIndex].IsNewRow)
			{
				this.dataGridView2.Rows.RemoveAt(this.rowIndex);
				this.Tatuajes.RemoveAt(this.rowIndex);
				this.rowIndex = this.Tatuajes.Count - 1;
				this.CargaImagenesTatuajes();
			}
		}

		private void contextMenuStrip4_Click(object sender, EventArgs e)
		{
			this.cantidad = 0;
			int itemeliminado = 0;
			foreach (PictureBox item in this.panel2.Controls.OfType<PictureBox>())
			{
				if (item.Name != this._nombrePB)
				{
					this.cantidad++;
				}
				else
				{
					this.panel2.Controls.Remove(item);
					itemeliminado = this.cantidad;
					break;
				}
			}
			this.pbImagenTatuaje.Image = null;
			int contador = 0;
			foreach (PictureBox item in this.panel2.Controls.OfType<PictureBox>())
			{
				if (contador >= itemeliminado)
				{
					Point location = item.Location;
					int x = location.X - 100;
					location = item.Location;
					item.Location = new Point(x, location.Y);
					item.Name = string.Concat("pbt", contador.ToString());
				}
				contador++;
			}
			this.Tatuajes[this.rowIndex].ListImages.RemoveAt(this.cantidad);
			this.cantidad = contador;
			DataTable dt = (DataTable)this.dataGridView2.DataSource;
			dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
			this.dataGridView2.DataSource = dt;
			this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
			if (this.cantidad > itemeliminado)
			{
				this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
			}
			else if (itemeliminado - 1 >= 0)
			{
				this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(itemeliminado - 1), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
			}
		}

		private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.rowIndex = e.RowIndex;
			this.cmbAreaCuerpo.SelectedIndex = 0;
			this.cmbTipoMarca.SelectedIndex = 0;
			this._seleccionado = true;
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.dataGridView1.Rows[e.RowIndex].Selected = true;
				this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
				this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
				this.contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
			}
			this.CargaImagenesMarcas();
			this.chkanadir.Checked = true;
		}

		private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.rowIndex = e.RowIndex;
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.dataGridView2.Rows[e.RowIndex].Selected = true;
				this.dataGridView2.CurrentCell = this.dataGridView2.Rows[e.RowIndex].Cells[0];
				this.contextMenuStrip3.Show(this.dataGridView2, e.Location);
				this.contextMenuStrip3.Show(System.Windows.Forms.Cursor.Position);
			}
			this.CargaImagenesTatuajes();
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void editarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
				{
					this._seleccionado = false;
					MarkData vMarca = this.Marcas[this.rowIndex];
					this.cmbTipoMarca.SelectedValue = vMarca.Type.Id;
					this.cmbAreaCuerpo.SelectedValue = vMarca.BodyArea.Id;
					this.dataGridView1.Rows.RemoveAt(this.rowIndex);
					this.Marcas.RemoveAt(this.rowIndex);
					this.rowIndex = -1;
					this.dataGridView1.ClearSelection();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
				{
					this.dataGridView1.Rows.RemoveAt(this.rowIndex);
					this.Marcas.RemoveAt(this.rowIndex);
					this.rowIndex = this.Marcas.Count - 1;
					this.CargaImagenesMarcas();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void fMarcasTatuaje_Load(object sender, EventArgs e)
		{
			this.rowIndex = -1;
			if (fEnrolar.PersonaCapturada == null)
			{
				fEnrolar.PersonaCapturada = new CapturedPerson();
			}
			if (fEnrolar.PersonaCapturada.RecordData == null)
			{
				fEnrolar.PersonaCapturada.RecordData = new RecordData();
			}
			this.Marcas = fEnrolar.PersonaCapturada.RecordData.MarkDatas;
			this.Tatuajes = fEnrolar.PersonaCapturada.RecordData.TattooDatas;
			this.CargaTipoMarca();
			this.CargaAreaCuerpo();
			this.CargaMotivo();
			this.CargaColor();
			this.CargaPestanaMarcas();
			this.chkNuevo_CheckedChanged(sender, e);
		}

		private Rectangle GetRectangle()
		{
			this.rect = new Rectangle()
			{
				X = Math.Min(this.StartLocation.X, this.EndLcation.X),
				Y = Math.Min(this.StartLocation.Y, this.EndLcation.Y),
				Width = Math.Abs(this.StartLocation.X - this.EndLcation.X),
				Height = Math.Abs(this.StartLocation.Y - this.EndLcation.Y)
			};
			return this.rect;
		}

		public void HallaEscalaSaldo(PictureBox pbCaptura)
		{
			this.imgInput = new Image<Bgr, byte>((Bitmap)pbCaptura.Image);
			System.Drawing.Size size = pbCaptura.Size;
			this.escala = (float)size.Height / (float)this.imgInput.Height;
			float nuevamedidax = (float)this.imgInput.Width * this.escala;
			float nuevamediday = (float)this.imgInput.Height * this.escala;
			if (nuevamedidax > (float)pbCaptura.Size.Width)
			{
				size = pbCaptura.Size;
				this.escala = (float)size.Width / (float)this.imgInput.Width;
				nuevamedidax = (float)this.imgInput.Height * this.escala;
				size = pbCaptura.Size;
				this.saldoY = ((float)size.Height - nuevamedidax) / 2f;
				this.saldoX = 0f;
			}
			else
			{
				size = pbCaptura.Size;
				this.saldoX = ((float)size.Width - nuevamedidax) / 2f;
				this.saldoY = 0f;
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(fMarcasTatuaje));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.chkanadir = new CheckBox();
			this.chkNuevo = new CheckBox();
			this.btnfotoM = new Button();
			this.panel1 = new Panel();
			this.label9 = new Label();
			this.btnañadirM = new Button();
			this.label3 = new Label();
			this.label2 = new Label();
			this.cmbAreaCuerpo = new ComboBox();
			this.cmbTipoMarca = new ComboBox();
			this.dataGridView1 = new DataGridView();
			this.btnCargarMarcas = new Button();
			this.pbImagenMarca = new PictureBox();
			this.tabPage2 = new TabPage();
			this.label11 = new Label();
			this.txtNombreT = new TextBox();
			this.btnFoto2 = new Button();
			this.label12 = new Label();
			this.cmbColor = new ComboBox();
			this.label7 = new Label();
			this.label10 = new Label();
			this.panel2 = new Panel();
			this.label5 = new Label();
			this.btnañadirT = new Button();
			this.cmbAreaCuerpoT = new ComboBox();
			this.cmbMotivo = new ComboBox();
			this.dataGridView2 = new DataGridView();
			this.label6 = new Label();
			this.btnCargarTatuajes = new Button();
			this.pbImagenTatuaje = new PictureBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem = new ToolStripMenuItem();
			this.editarToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem1 = new ToolStripMenuItem();
			this.errorProvider1 = new ErrorProvider(this.components);
			this.panel3 = new Panel();
			this.button1 = new Button();
			this.btnAnterior = new Button();
			this.btnguardar = new Button();
			this.btncancelar = new Button();
			this.btnsiguiente = new Button();
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem3 = new ToolStripMenuItem();
			this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.eliminarToolStripMenuItem2 = new ToolStripMenuItem();
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label13 = new Label();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			((ISupportInitialize)this.pbImagenMarca).BeginInit();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			((ISupportInitialize)this.pbImagenTatuaje).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			((ISupportInitialize)this.errorProvider1).BeginInit();
			this.panel3.SuspendLayout();
			this.contextMenuStrip3.SuspendLayout();
			this.contextMenuStrip4.SuspendLayout();
			this.tableLayoutPanel11.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.ItemSize = new System.Drawing.Size(47, 10);
			this.tabControl1.Location = new Point(123, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(988, 579);
			this.tabControl1.TabIndex = 12;
			this.tabPage1.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage1.Controls.Add(this.chkanadir);
			this.tabPage1.Controls.Add(this.chkNuevo);
			this.tabPage1.Controls.Add(this.btnfotoM);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.btnañadirM);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.cmbAreaCuerpo);
			this.tabPage1.Controls.Add(this.cmbTipoMarca);
			this.tabPage1.Controls.Add(this.dataGridView1);
			this.tabPage1.Controls.Add(this.btnCargarMarcas);
			this.tabPage1.Controls.Add(this.pbImagenMarca);
			this.tabPage1.Location = new Point(4, 14);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(980, 561);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Marcas";
			this.chkanadir.AutoSize = true;
			this.chkanadir.FlatAppearance.BorderColor = Color.Aqua;
			this.chkanadir.FlatAppearance.BorderSize = 3;
			this.chkanadir.FlatStyle = FlatStyle.Flat;
			this.chkanadir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.chkanadir.ForeColor = Color.White;
			this.chkanadir.Location = new Point(28, 393);
			this.chkanadir.Name = "chkanadir";
			this.chkanadir.Size = new System.Drawing.Size(130, 19);
			this.chkanadir.TabIndex = 65;
			this.chkanadir.Text = "AÑADIR IMAGEN";
			this.chkanadir.UseVisualStyleBackColor = true;
			this.chkanadir.CheckedChanged += new EventHandler(this.chkanadir_CheckedChanged);
			this.chkNuevo.AutoSize = true;
			this.chkNuevo.Checked = true;
			this.chkNuevo.CheckState = CheckState.Checked;
			this.chkNuevo.FlatStyle = FlatStyle.Flat;
			this.chkNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.chkNuevo.ForeColor = Color.White;
			this.chkNuevo.Location = new Point(375, 18);
			this.chkNuevo.Name = "chkNuevo";
			this.chkNuevo.Size = new System.Drawing.Size(144, 19);
			this.chkNuevo.TabIndex = 64;
			this.chkNuevo.Text = "NUEVO REGISTRO";
			this.chkNuevo.UseVisualStyleBackColor = true;
			this.chkNuevo.CheckedChanged += new EventHandler(this.chkNuevo_CheckedChanged);
			this.btnfotoM.BackColor = Color.White;
			this.btnfotoM.Image = (Image)resources.GetObject("btnfotoM.Image");
			this.btnfotoM.ImageAlign = ContentAlignment.TopCenter;
			this.btnfotoM.Location = new Point(171, 358);
			this.btnfotoM.Name = "btnfotoM";
			this.btnfotoM.Size = new System.Drawing.Size(91, 53);
			this.btnfotoM.TabIndex = 63;
			this.btnfotoM.Text = "Tomar Foto";
			this.btnfotoM.TextAlign = ContentAlignment.BottomCenter;
			this.btnfotoM.UseVisualStyleBackColor = false;
			this.btnfotoM.Click += new EventHandler(this.btnfotoM_Click);
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = Color.White;
			this.panel1.Location = new Point(28, 417);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(933, 134);
			this.panel1.TabIndex = 62;
			this.label9.AutoSize = true;
			this.label9.ForeColor = Color.White;
			this.label9.Location = new Point(865, 61);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(53, 13);
			this.label9.TabIndex = 61;
			this.label9.Text = "Imágenes";
			this.btnañadirM.BackColor = Color.White;
			this.btnañadirM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnañadirM.Location = new Point(833, 56);
			this.btnañadirM.Name = "btnañadirM";
			this.btnañadirM.Size = new System.Drawing.Size(31, 23);
			this.btnañadirM.TabIndex = 60;
			this.btnañadirM.Text = "+";
			this.btnañadirM.UseVisualStyleBackColor = false;
			this.btnañadirM.Click += new EventHandler(this.btnañadirM_Click);
			this.label3.AutoSize = true;
			this.label3.ForeColor = SystemColors.ButtonFace;
			this.label3.Location = new Point(616, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 59;
			this.label3.Text = "Area del Cuerpo";
			this.label2.AutoSize = true;
			this.label2.ForeColor = SystemColors.ButtonFace;
			this.label2.Location = new Point(372, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 13);
			this.label2.TabIndex = 58;
			this.label2.Text = "Tipo de Adjunto";
			this.cmbAreaCuerpo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.cmbAreaCuerpo.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cmbAreaCuerpo.FormattingEnabled = true;
			this.cmbAreaCuerpo.Location = new Point(613, 58);
			this.cmbAreaCuerpo.Name = "cmbAreaCuerpo";
			this.cmbAreaCuerpo.Size = new System.Drawing.Size(214, 21);
			this.cmbAreaCuerpo.TabIndex = 56;
			this.cmbAreaCuerpo.SelectedIndexChanged += new EventHandler(this.cmbAreaCuerpo_SelectedIndexChanged);
			this.cmbTipoMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.cmbTipoMarca.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cmbTipoMarca.FormattingEnabled = true;
			this.cmbTipoMarca.Location = new Point(369, 58);
			this.cmbTipoMarca.Name = "cmbTipoMarca";
			this.cmbTipoMarca.Size = new System.Drawing.Size(238, 21);
			this.cmbTipoMarca.TabIndex = 55;
			this.cmbTipoMarca.SelectedIndexChanged += new EventHandler(this.cmbTipoMarca_SelectedIndexChanged);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = Color.White;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Location = new Point(369, 85);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(593, 326);
			this.dataGridView1.TabIndex = 53;
			this.dataGridView1.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
			this.btnCargarMarcas.BackColor = Color.White;
			this.btnCargarMarcas.Image = (Image)resources.GetObject("btnCargarMarcas.Image");
			this.btnCargarMarcas.Location = new Point(268, 358);
			this.btnCargarMarcas.Name = "btnCargarMarcas";
			this.btnCargarMarcas.Size = new System.Drawing.Size(91, 53);
			this.btnCargarMarcas.TabIndex = 10;
			this.btnCargarMarcas.Text = "Cargar Imagen";
			this.btnCargarMarcas.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarMarcas.UseVisualStyleBackColor = false;
			this.btnCargarMarcas.Click += new EventHandler(this.btnCargarMarcas_Click);
			this.pbImagenMarca.BackColor = Color.White;
			this.pbImagenMarca.BorderStyle = BorderStyle.Fixed3D;
			this.pbImagenMarca.Location = new Point(28, 8);
			this.pbImagenMarca.Margin = new System.Windows.Forms.Padding(6);
			this.pbImagenMarca.Name = "pbImagenMarca";
			this.pbImagenMarca.Size = new System.Drawing.Size(331, 346);
			this.pbImagenMarca.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbImagenMarca.TabIndex = 4;
			this.pbImagenMarca.TabStop = false;
			this.tabPage2.BackColor = Color.White;
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.txtNombreT);
			this.tabPage2.Controls.Add(this.btnFoto2);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.cmbColor);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label10);
			this.tabPage2.Controls.Add(this.panel2);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.btnañadirT);
			this.tabPage2.Controls.Add(this.cmbAreaCuerpoT);
			this.tabPage2.Controls.Add(this.cmbMotivo);
			this.tabPage2.Controls.Add(this.dataGridView2);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.btnCargarTatuajes);
			this.tabPage2.Controls.Add(this.pbImagenTatuaje);
			this.tabPage2.Location = new Point(4, 14);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(980, 561);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Tatuajes";
			this.label11.AutoSize = true;
			this.label11.Location = new Point(371, 51);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(44, 13);
			this.label11.TabIndex = 80;
			this.label11.Text = "Nombre";
			this.txtNombreT.Location = new Point(368, 66);
			this.txtNombreT.Name = "txtNombreT";
			this.txtNombreT.Size = new System.Drawing.Size(115, 20);
			this.txtNombreT.TabIndex = 79;
			this.txtNombreT.TextChanged += new EventHandler(this.txtNombreT_TextChanged);
			this.btnFoto2.BackColor = Color.Gainsboro;
			this.btnFoto2.Image = (Image)resources.GetObject("btnFoto2.Image");
			this.btnFoto2.ImageAlign = ContentAlignment.TopCenter;
			this.btnFoto2.Location = new Point(177, 407);
			this.btnFoto2.Name = "btnFoto2";
			this.btnFoto2.Size = new System.Drawing.Size(75, 50);
			this.btnFoto2.TabIndex = 78;
			this.btnFoto2.Text = "Tomar Foto";
			this.btnFoto2.TextAlign = ContentAlignment.BottomCenter;
			this.btnFoto2.UseVisualStyleBackColor = false;
			this.btnFoto2.Click += new EventHandler(this.btnFoto2_Click);
			this.label12.AutoSize = true;
			this.label12.Location = new Point(734, 50);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(31, 13);
			this.label12.TabIndex = 77;
			this.label12.Text = "Color";
			this.cmbColor.FormattingEnabled = true;
			this.cmbColor.Location = new Point(731, 65);
			this.cmbColor.Name = "cmbColor";
			this.cmbColor.Size = new System.Drawing.Size(115, 21);
			this.cmbColor.TabIndex = 76;
			this.cmbColor.SelectedIndexChanged += new EventHandler(this.cmbColor_SelectedIndexChanged);
			this.label7.AutoSize = true;
			this.label7.Location = new Point(613, 50);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(83, 13);
			this.label7.TabIndex = 75;
			this.label7.Text = "Area del Cuerpo";
			this.label10.AutoSize = true;
			this.label10.Location = new Point(492, 51);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(39, 13);
			this.label10.TabIndex = 74;
			this.label10.Text = "Motivo";
			this.panel2.AutoScroll = true;
			this.panel2.BackColor = Color.Gray;
			this.panel2.Location = new Point(28, 474);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(933, 120);
			this.panel2.TabIndex = 72;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(846, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 13);
			this.label5.TabIndex = 71;
			this.label5.Text = "Cantidad de imágenes";
			this.btnañadirT.BackColor = Color.White;
			this.btnañadirT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnañadirT.Location = new Point(930, 42);
			this.btnañadirT.Name = "btnañadirT";
			this.btnañadirT.Size = new System.Drawing.Size(31, 23);
			this.btnañadirT.TabIndex = 70;
			this.btnañadirT.Text = "+";
			this.btnañadirT.UseVisualStyleBackColor = false;
			this.btnañadirT.Click += new EventHandler(this.btnañadirT_Click);
			this.cmbAreaCuerpoT.FormattingEnabled = true;
			this.cmbAreaCuerpoT.Location = new Point(610, 65);
			this.cmbAreaCuerpoT.Name = "cmbAreaCuerpoT";
			this.cmbAreaCuerpoT.Size = new System.Drawing.Size(115, 21);
			this.cmbAreaCuerpoT.TabIndex = 69;
			this.cmbAreaCuerpoT.SelectedIndexChanged += new EventHandler(this.cmbAreaCuerpoT_SelectedIndexChanged);
			this.cmbMotivo.FormattingEnabled = true;
			this.cmbMotivo.Location = new Point(489, 66);
			this.cmbMotivo.Name = "cmbMotivo";
			this.cmbMotivo.Size = new System.Drawing.Size(115, 21);
			this.cmbMotivo.TabIndex = 68;
			this.cmbMotivo.SelectedIndexChanged += new EventHandler(this.cmbMotivo_SelectedIndexChanged);
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToDeleteRows = false;
			this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView2.BackgroundColor = Color.White;
			this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.ColumnHeadersVisible = false;
			this.dataGridView2.Location = new Point(368, 88);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.ReadOnly = true;
			this.dataGridView2.RowHeadersVisible = false;
			this.dataGridView2.Size = new System.Drawing.Size(593, 338);
			this.dataGridView2.TabIndex = 66;
			this.dataGridView2.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseUp);
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.ForeColor = Color.FromArgb(0, 64, 64);
			this.label6.Location = new Point(24, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(187, 20);
			this.label6.TabIndex = 65;
			this.label6.Text = "Imágenes de Tatuajes";
			this.btnCargarTatuajes.BackColor = Color.Gainsboro;
			this.btnCargarTatuajes.Image = (Image)resources.GetObject("btnCargarTatuajes.Image");
			this.btnCargarTatuajes.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarTatuajes.Location = new Point(274, 406);
			this.btnCargarTatuajes.Name = "btnCargarTatuajes";
			this.btnCargarTatuajes.Size = new System.Drawing.Size(85, 51);
			this.btnCargarTatuajes.TabIndex = 64;
			this.btnCargarTatuajes.Text = "Cargar Imagen";
			this.btnCargarTatuajes.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarTatuajes.UseVisualStyleBackColor = false;
			this.btnCargarTatuajes.Click += new EventHandler(this.btnCargarTatuajes_Click);
			this.pbImagenTatuaje.BackColor = Color.Gainsboro;
			this.pbImagenTatuaje.BorderStyle = BorderStyle.Fixed3D;
			this.pbImagenTatuaje.Location = new Point(28, 51);
			this.pbImagenTatuaje.Margin = new System.Windows.Forms.Padding(6);
			this.pbImagenTatuaje.Name = "pbImagenTatuaje";
			this.pbImagenTatuaje.Size = new System.Drawing.Size(331, 346);
			this.pbImagenTatuaje.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbImagenTatuaje.TabIndex = 63;
			this.pbImagenTatuaje.TabStop = false;
			this.pbImagenTatuaje.Paint += new PaintEventHandler(this.pbImagenTatuaje_Paint);
			this.pbImagenTatuaje.MouseDown += new MouseEventHandler(this.pb_MouseDown);
			this.pbImagenTatuaje.MouseMove += new MouseEventHandler(this.pb_MouseMove);
			this.pbImagenTatuaje.MouseUp += new MouseEventHandler(this.pb_MouseUp);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem, this.editarToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
			this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
			this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.eliminarToolStripMenuItem.Text = "Eliminar";
			this.eliminarToolStripMenuItem.Click += new EventHandler(this.eliminarToolStripMenuItem_Click);
			this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
			this.editarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.editarToolStripMenuItem.Text = "Editar";
			this.editarToolStripMenuItem.Click += new EventHandler(this.editarToolStripMenuItem_Click);
			this.contextMenuStrip2.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem1 });
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(118, 26);
			this.contextMenuStrip2.Click += new EventHandler(this.contextMenuStrip2_Click);
			this.eliminarToolStripMenuItem1.Name = "eliminarToolStripMenuItem1";
			this.eliminarToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
			this.eliminarToolStripMenuItem1.Text = "Eliminar";
			this.errorProvider1.ContainerControl = this;
			this.panel3.BackColor = Color.FromArgb(48, 63, 105);
			this.panel3.Controls.Add(this.button1);
			this.panel3.Controls.Add(this.btnAnterior);
			this.panel3.Controls.Add(this.btnguardar);
			this.panel3.Controls.Add(this.btncancelar);
			this.panel3.Controls.Add(this.btnsiguiente);
			this.panel3.Location = new Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(294, 75);
			this.panel3.TabIndex = 25;
			this.button1.BackColor = Color.White;
			this.button1.Image = (Image)resources.GetObject("button1.Image");
			this.button1.ImageAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(203, 11);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 60);
			this.button1.TabIndex = 33;
			this.button1.Text = "Salir";
			this.button1.TextAlign = ContentAlignment.BottomCenter;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btnAnterior.BackColor = Color.White;
			this.btnAnterior.Enabled = false;
			this.btnAnterior.Image = (Image)resources.GetObject("btnAnterior.Image");
			this.btnAnterior.ImageAlign = ContentAlignment.TopCenter;
			this.btnAnterior.Location = new Point(347, 11);
			this.btnAnterior.Name = "btnAnterior";
			this.btnAnterior.Size = new System.Drawing.Size(75, 42);
			this.btnAnterior.TabIndex = 1;
			this.btnAnterior.Text = "Anterior";
			this.btnAnterior.TextAlign = ContentAlignment.BottomCenter;
			this.btnAnterior.UseVisualStyleBackColor = false;
			this.btnAnterior.Visible = false;
			this.btnAnterior.Click += new EventHandler(this.btnanterior_Click);
			this.btnguardar.BackColor = Color.White;
			this.btnguardar.Image = (Image)resources.GetObject("btnguardar.Image");
			this.btnguardar.ImageAlign = ContentAlignment.TopCenter;
			this.btnguardar.Location = new Point(10, 11);
			this.btnguardar.Name = "btnguardar";
			this.btnguardar.Size = new System.Drawing.Size(80, 60);
			this.btnguardar.TabIndex = 28;
			this.btnguardar.Text = "Guardar";
			this.btnguardar.TextAlign = ContentAlignment.BottomCenter;
			this.btnguardar.UseVisualStyleBackColor = false;
			this.btnguardar.Click += new EventHandler(this.btnguardar_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(105, 11);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(92, 60);
			this.btncancelar.TabIndex = 26;
			this.btncancelar.Text = "Guardar y Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.btnsiguiente.BackColor = Color.White;
			this.btnsiguiente.Image = (Image)resources.GetObject("btnsiguiente.Image");
			this.btnsiguiente.ImageAlign = ContentAlignment.TopCenter;
			this.btnsiguiente.Location = new Point(428, 11);
			this.btnsiguiente.Name = "btnsiguiente";
			this.btnsiguiente.Size = new System.Drawing.Size(75, 42);
			this.btnsiguiente.TabIndex = 27;
			this.btnsiguiente.Text = "Siguiente";
			this.btnsiguiente.TextAlign = ContentAlignment.BottomCenter;
			this.btnsiguiente.UseVisualStyleBackColor = false;
			this.btnsiguiente.Visible = false;
			this.btnsiguiente.Click += new EventHandler(this.btnsiguiente_Click);
			this.contextMenuStrip3.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem3 });
			this.contextMenuStrip3.Name = "contextMenuStrip3";
			this.contextMenuStrip3.Size = new System.Drawing.Size(118, 26);
			this.contextMenuStrip3.Click += new EventHandler(this.contextMenuStrip3_Click);
			this.eliminarToolStripMenuItem3.Name = "eliminarToolStripMenuItem3";
			this.eliminarToolStripMenuItem3.Size = new System.Drawing.Size(117, 22);
			this.eliminarToolStripMenuItem3.Text = "Eliminar";
			this.contextMenuStrip4.Items.AddRange(new ToolStripItem[] { this.eliminarToolStripMenuItem2 });
			this.contextMenuStrip4.Name = "contextMenuStrip4";
			this.contextMenuStrip4.Size = new System.Drawing.Size(118, 26);
			this.contextMenuStrip4.Click += new EventHandler(this.contextMenuStrip4_Click);
			this.eliminarToolStripMenuItem2.Name = "eliminarToolStripMenuItem2";
			this.eliminarToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
			this.eliminarToolStripMenuItem2.Text = "Eliminar";
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel11.ColumnCount = 2;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel11.Controls.Add(this.panel3, 0, 0);
			this.tableLayoutPanel11.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel11.Location = new Point(-1, 25);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(1228, 92);
			this.tableLayoutPanel11.TabIndex = 18;
			this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(1125, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 86);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 26;
			this.pictureBox1.TabStop = false;
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Controls.Add(this.label13, 0, 0);
			this.tableLayoutPanel1.Location = new Point(-1, 98);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1236, 36);
			this.tableLayoutPanel1.TabIndex = 19;
			this.label13.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label13.AutoSize = true;
			this.label13.BackColor = Color.Transparent;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.label13.ForeColor = Color.White;
			this.label13.Location = new Point(3, 6);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(1230, 24);
			this.label13.TabIndex = 26;
			this.label13.Text = "ARCHIVOS ADJUNTOS";
			this.label13.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel2.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel2.Controls.Add(this.tabControl1, 1, 0);
			this.tableLayoutPanel2.Location = new Point(-1, 121);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1236, 585);
			this.tableLayoutPanel2.TabIndex = 20;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(1227, 718);
			base.ControlBox = false;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.tableLayoutPanel2);
			base.Controls.Add(this.tableLayoutPanel11);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MinimizeBox = false;
			base.Name = "fMarcasTatuaje";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Registro de Marcas y/o Tatuajes";
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.fMarcasTatuaje_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			((ISupportInitialize)this.pbImagenMarca).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((ISupportInitialize)this.dataGridView2).EndInit();
			((ISupportInitialize)this.pbImagenTatuaje).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			((ISupportInitialize)this.errorProvider1).EndInit();
			this.panel3.ResumeLayout(false);
			this.contextMenuStrip3.ResumeLayout(false);
			this.contextMenuStrip4.ResumeLayout(false);
			this.tableLayoutPanel11.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LlenaMarcas()
		{
			Coder vTipoMarca = CargarControl.ObtenerCoder(this.cmbTipoMarca, "MARKTYPE");
			Coder vAreaCuerpo = CargarControl.ObtenerCoder(this.cmbAreaCuerpo, "BODYAREA");
			if (this.Marcas == null)
			{
				this.Marcas = new BindingList<MarkData>();
			}
			this.Marcas.Add(new MarkData()
			{
				BodyArea = vAreaCuerpo,
				Type = vTipoMarca,
				Name = string.Concat("Marca - ", vAreaCuerpo.Value, " - ", vTipoMarca.Value),
				ListImages = new BindingList<ImageData>()
			});
			foreach (PictureBox pb in this.panel1.Controls.OfType<PictureBox>())
			{
				this.Marcas[this.Marcas.Count - 1].ListImages.Add(new ImageData()
				{
					NormalizedImageArr = this.ser.ImageToByteArray(pb.Image)
				});
				Thread.Sleep(500);
			}
		}

		private void LlenaTatuajes()
		{
			Coder vMotivo = CargarControl.ObtenerCoder(this.cmbMotivo, "TATOOMOTIVE");
			Coder vAreaCuerpo = CargarControl.ObtenerCoder(this.cmbAreaCuerpoT, "BODYAREA");
			Coder vColor = CargarControl.ObtenerCoder(this.cmbColor, "TATOOCOLOR");
			if (this.Tatuajes == null)
			{
				this.Tatuajes = new BindingList<TattooData>();
			}
			this.Tatuajes.Add(new TattooData()
			{
				Motive = vMotivo,
				BodyArea = vAreaCuerpo,
				Color = vColor,
				Name = this.txtNombreT.Text.Trim(),
				ListImages = new BindingList<ImageData>()
			});
		}

		private DataTable ObtenerDataTableMarcas()
		{
			DataTable table = new DataTable();
			table.Columns.Add("TipoMarca");
			table.Columns.Add("IdTipoMarca");
			table.Columns.Add("AreaCuerpo");
			table.Columns.Add("IdAreaCuerpo");
			table.Columns.Add("Cantidad");
			foreach (MarkData vItem in this.Marcas)
			{
				DataRow dr = table.NewRow();
				dr["TipoMarca"] = vItem.Type.Value;
				dr["IdTipoMarca"] = vItem.Type.Id;
				dr["AreaCuerpo"] = vItem.BodyArea.Value;
				dr["IdAreaCuerpo"] = vItem.BodyArea.Id;
				dr["Cantidad"] = vItem.ListImages.Count;
				table.Rows.Add(dr);
			}
			return table;
		}

		private DataTable ObtenerDataTableTatuajes()
		{
			DataTable table = new DataTable();
			table.Columns.Add("Nombre");
			table.Columns.Add("Motivo");
			table.Columns.Add("IdMotivo");
			table.Columns.Add("AreaCuerpo");
			table.Columns.Add("IdAreaCuerpo");
			table.Columns.Add("Color");
			table.Columns.Add("IdColor");
			table.Columns.Add("Cantidad");
			foreach (TattooData vItem in this.Tatuajes)
			{
				DataRow dr = table.NewRow();
				dr["Nombre"] = vItem.Name;
				dr["Motivo"] = vItem.Motive.Value;
				dr["IdMotivo"] = vItem.Motive.Id;
				dr["AreaCuerpo"] = vItem.BodyArea.Value;
				dr["IdAreaCuerpo"] = vItem.BodyArea.Id;
				dr["Color"] = vItem.Color.Value;
				dr["IdColor"] = vItem.Color.Id;
				dr["Cantidad"] = vItem.ListImages.Count;
				table.Rows.Add(dr);
			}
			return table;
		}

		public DataTable ObtenerTablaMarca()
		{
			DataTable table = new DataTable();
			table.Columns.Add("TipoMarca");
			table.Columns.Add("IdTipoMarca");
			table.Columns.Add("AreaCuerpo");
			table.Columns.Add("IdAreaCuerpo");
			table.Columns.Add("Cantidad");
			DataRow dr = table.NewRow();
			dr["TipoMarca"] = this.cmbTipoMarca.SelectedItem.ToString();
			dr["IdTipoMarca"] = this.cmbTipoMarca.SelectedValue.ToString();
			dr["AreaCuerpo"] = this.cmbAreaCuerpo.SelectedItem.ToString();
			dr["IdAreaCuerpo"] = this.cmbAreaCuerpo.SelectedValue.ToString();
			dr["Cantidad"] = this.panel1.Controls.OfType<PictureBox>().Count<PictureBox>();
			table.Rows.Add(dr);
			if (Convert.ToInt32(table.Rows[0]["Cantidad"]) == 0)
			{
				this.chkanadir.Checked = true;
			}
			return table;
		}

		public DataTable ObtenerTablaTatuajes()
		{
			DataTable table = new DataTable();
			table.Columns.Add("Nombre");
			table.Columns.Add("Motivo");
			table.Columns.Add("IdMotivo");
			table.Columns.Add("AreaCuerpo");
			table.Columns.Add("IdAreaCuerpo");
			table.Columns.Add("Color");
			table.Columns.Add("IdColor");
			table.Columns.Add("Cantidad");
			DataRow dr = table.NewRow();
			dr["Nombre"] = this.txtNombreT.Text;
			dr["Motivo"] = this.cmbMotivo.SelectedItem.ToString();
			dr["IdMotivo"] = this.cmbMotivo.SelectedValue.ToString();
			dr["AreaCuerpo"] = this.cmbAreaCuerpoT.SelectedItem.ToString();
			dr["IdAreaCuerpo"] = this.cmbAreaCuerpoT.SelectedValue.ToString();
			dr["Color"] = this.cmbColor.SelectedItem.ToString();
			dr["IdColor"] = this.cmbColor.SelectedValue.ToString();
			dr["Cantidad"] = 0;
			table.Rows.Add(dr);
			return table;
		}

		private void Pb_MouseClick(object sender, MouseEventArgs e)
		{
			PictureBox vPictureBox = (PictureBox)sender;
			this._nombrePB = vPictureBox.Name;
			try
			{
				foreach (PictureBox item in this.panel1.Controls.OfType<PictureBox>())
				{
					item.BackColor = Color.Transparent;
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (vPictureBox.Image != null)
					{
						this.pbImagenMarca.Image = vPictureBox.Image;
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbImagenMarca.Enabled = false;
						vPictureBox.BackColor = Color.Red;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.pbImagenMarca.Image = vPictureBox.Image;
					this.pbImagenMarca.Enabled = false;
					vPictureBox.BackColor = Color.Red;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pb_MouseDown(object sender, MouseEventArgs e)
		{
			this._selecting = true;
			this.StartLocation = e.Location;
		}

		private void pb_MouseMove(object sender, MouseEventArgs e)
		{
			if (this._selecting)
			{
				this.EndLcation = e.Location;
				((PictureBox)sender).Invalidate();
			}
		}

		private void pb_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if ((e.Button != System.Windows.Forms.MouseButtons.Left ? false : this._selecting))
				{
					this.EndLcation = e.Location;
					this._selecting = false;
					Rectangle rectangle = this.rect;
					if (false)
					{
						return;
					}
					else if ((this.rect.Width <= 0 ? false : this.rect.Height > 0))
					{
						PictureBox vPictureBox = (PictureBox)sender;
						this.HallaEscalaSaldo(vPictureBox);
						float nuevoX = (float)this.rect.X - this.saldoX;
						float nuevoY = (float)this.rect.Y - this.saldoY;
						nuevoX /= this.escala;
						nuevoY /= this.escala;
						this.rect.X = Convert.ToInt32(Math.Round((double)nuevoX, 0));
						this.rect.Y = Convert.ToInt32(Math.Round((double)nuevoY, 0));
						this.rect.Width = Convert.ToInt32(Math.Round((double)((float)this.rect.Width / this.escala), 0));
						this.rect.Height = Convert.ToInt32(Math.Round((double)((float)this.rect.Height / this.escala), 0));
						this.imgInput.ROI = this.rect;
						this.temp = this.imgInput.Clone();
						vPictureBox.Image = this.temp.Bitmap;
						PictureBox p = new PictureBox();
						DataTable dt = new DataTable();
						string name = vPictureBox.Name;
						if (name != null)
						{
							if (name == "pbImagenMarca")
							{
								p.Name = string.Concat("pb", this.cantidad.ToString());
								p.Image = this.imgInput.Bitmap;
								p.Location = new Point(20 + this.cantidad * 100, 20);
								p.Size = new System.Drawing.Size(90, 90);
								p.SizeMode = PictureBoxSizeMode.Zoom;
								p.BorderStyle = BorderStyle.FixedSingle;
								p.MouseClick += new MouseEventHandler(this.Pb_MouseClick);
								this.panel1.Controls.Add(p);
								this.Pb_MouseClick(this.panel1.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
								this.cantidad++;
								dt = (DataTable)this.dataGridView1.DataSource;
								dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
								this.dataGridView1.DataSource = dt;
								this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
								this.Marcas[this.rowIndex].ListImages.Add(new ImageData()
								{
									NormalizedImageArr = this.ser.ImageToByteArray(this.imgInput.Bitmap)
								});
								this.pbImagenMarca.Enabled = false;
							}
							else if (name == "pbImagenTatuaje")
							{
								p.Name = string.Concat("pbt", this.cantidad.ToString());
								p.Image = this.imgInput.Bitmap;
								p.Location = new Point(20 + this.cantidad * 100, 20);
								p.Size = new System.Drawing.Size(90, 90);
								p.SizeMode = PictureBoxSizeMode.Zoom;
								p.BorderStyle = BorderStyle.FixedSingle;
								p.MouseClick += new MouseEventHandler(this.Pbt_MouseClick);
								this.panel2.Controls.Add(p);
								this.Pbt_MouseClick(this.panel2.Controls.OfType<PictureBox>().ElementAt<PictureBox>(this.cantidad), new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
								this.cantidad++;
								dt = (DataTable)this.dataGridView2.DataSource;
								dt.Rows[this.rowIndex]["Cantidad"] = this.cantidad;
								this.dataGridView2.DataSource = dt;
								this.btnsiguiente.Enabled = this.ValidaGrilla(dt);
								this.Tatuajes[this.rowIndex].ListImages.Add(new ImageData()
								{
									NormalizedImageArr = this.ser.ImageToByteArray(this.imgInput.Bitmap)
								});
								this.pbImagenTatuaje.Enabled = false;
							}
						}
					}
					else
					{
						return;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void pbImagenMarca_Paint(object sender, PaintEventArgs e)
		{
			if (this._selecting)
			{
				e.Graphics.DrawRectangle(Pens.Red, this.GetRectangle());
			}
		}

		private void pbImagenTatuaje_Paint(object sender, PaintEventArgs e)
		{
			if (this._selecting)
			{
				e.Graphics.DrawRectangle(Pens.Red, this.GetRectangle());
			}
		}

		private void Pbt_MouseClick(object sender, MouseEventArgs e)
		{
			PictureBox vPictureBox = (PictureBox)sender;
			this._nombrePB = vPictureBox.Name;
			try
			{
				foreach (PictureBox item in this.panel2.Controls.OfType<PictureBox>())
				{
					item.BackColor = Color.Transparent;
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (vPictureBox.Image != null)
					{
						this.pbImagenTatuaje.Image = vPictureBox.Image;
						this.contextMenuStrip4.Show(System.Windows.Forms.Cursor.Position);
						this.pbImagenTatuaje.Enabled = false;
						vPictureBox.BackColor = Color.Red;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.pbImagenTatuaje.Image = vPictureBox.Image;
					this.pbImagenTatuaje.Enabled = false;
					vPictureBox.BackColor = Color.Red;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void txtNombre_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
		}

		private void txtNombreT_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosMarcas();
		}

		public bool ValidaGrilla(DataTable pDT)
		{
			bool flag;
			bool vEsValido = true;
			foreach (DataRow vFila in pDT.Rows)
			{
				if (Convert.ToInt32(vFila["Cantidad"]) == 0)
				{
					flag = false;
					return flag;
				}
			}
			flag = vEsValido;
			return flag;
		}

		private bool ValidaModeloDatosMarcas()
		{
			return Validador.ValidarCampos(new DatosMarcas()
			{
				cmbTipoMarca = (this.cmbTipoMarca.SelectedValue == null ? "" : this.cmbTipoMarca.SelectedValue.ToString()),
				cmbAreaCuerpo = (this.cmbAreaCuerpo.SelectedValue == null ? "" : this.cmbAreaCuerpo.SelectedValue.ToString())
			}, this.errorProvider1, this);
		}

		private bool ValidaModeloDatosTatuajes()
		{
			return Validador.ValidarCampos(new DatosTatuajes()
			{
				txtNombreT = this.txtNombreT.Text,
				cmbMotivo = (this.cmbMotivo.SelectedValue == null ? "" : this.cmbMotivo.SelectedValue.ToString()),
				cmbAreaCuerpo = (this.cmbAreaCuerpoT.SelectedValue == null ? "" : this.cmbAreaCuerpoT.SelectedValue.ToString()),
				cmbColor = (this.cmbColor.SelectedValue == null ? "" : this.cmbColor.SelectedValue.ToString())
			}, this.errorProvider1, this);
		}
	}
}