using BIODV.Modelo;
using BIODV.Properties;
using BIODV.Util;
using Datys.Enroll.Core;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace BIODV
{
	public class fCapturaFacial : Form
	{
		private Serializer ser = new Serializer();

		private CapturedPerson PersonaCapturada = new CapturedPerson();

		private bool _selecting;

		private Image<Bgr, byte> imgInput;

		private Image<Bgr, byte> temp;

		private float escala;

		private float saldoX;

		private float saldoY;

		private Point StartLocation;

		private Point EndLcation;

		private Rectangle rect;

		public bool ForceClose = false;

		public bool nuevofrontal = false;

		private Bitmap BitmapRostroFrontalCropRegla;

		private Bitmap BitmapRostroFrontalCropReglaAux;

		private Bitmap BitmapRostroFrontal;

		private Bitmap BitmapRostroFrontalCrop;

		private int AuxNuevaImagen = 0;

		private Bitmap BitmapCuerpo;

		private Bitmap BitmapCuerpoCrop;

		private Bitmap BitmapCuerpoCropRegla;

		private Bitmap BitmapPerfilIzquierdo;

		private Bitmap BitmapPerfilIzquierdoCrop;

		private Bitmap BitmapPerfilIzquierdoCropRegla;

		private Bitmap BitmapPerfilDerecho;

		private Bitmap BitmapPerfilDerechoCrop;

		private Bitmap BitmapPerfilDerechoCropRegla;

		private IContainer components = null;

		private TabPage tabPage0;

		private Label label12;

		private NumericUpDown numEstatura;

		private Label label16;

		private TextBox txtid;

		private TabPage tabPage7;

		private Label label19;

		private Panel panel1;

		private Label label15;

		private TabPage tabPage1;

		private Button btnCargarFrontal;

		private PictureBox pbCapturaFrontal;

		private TabPage tabPage2;

		private TabPage tabPage4;

		private TabPage tabPage6;

		private Button btnAyuda;

		private Button btnguardar;

		private Button btnsiguiente;

		private Button btncancelar;

		private TabPage tabPage3;

		private PictureBox pbFrontalRegla;

		private ErrorProvider errorProvider1;

		private Button btnOriginalCuerpo;

		private Button btnCargarCuerpo;

		private PictureBox pbCuerpo;

		private TabPage tabPage5;

		private Button btnOriginalIzq;

		private Button btnCargarIzq;

		private PictureBox pbIzq;

		private Label label3;

		private Button btnOriginalDer;

		private Button btnCargarDer;

		private PictureBox pbDer;

		private Label label6;

		private GroupBox gbCuerpoEntero;

		private PictureBox pbResumenCuerpo;

		private GroupBox gbPerfilIzq;

		private PictureBox pbResumenIzq;

		private GroupBox gbFrontal;

		private PictureBox pbResumenFrontal;

		private GroupBox gbPerfilDer;

		private PictureBox pbResumenDer;

		private Label label7;

		private Label label8;

		private DataGridView dataGridView2;

		private DataGridViewTextBoxColumn Detalle2;

		private DataGridViewTextBoxColumn Valor2;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Detalle;

		private DataGridViewTextBoxColumn Valor;

		private PictureBox pbImagenRecorte;

		private Button btnFoto;

		private Button button1;

		private CheckBox chkCuerpo3;

		private CheckBox chkCuerpo2;

		private CheckBox chkCuerpo1;

		private PictureBox pbcheck1;

		private PictureBox pbcheck3;

		private PictureBox pbcheck2;

		private PictureBox pbcheck6;

		private PictureBox pbcheck5;

		private PictureBox pbcheck4;

		private CheckBox checkBox1;

		private CheckBox checkBox2;

		private CheckBox checkBox3;

		private Button button2;

		private PictureBox pbcheck9;

		private PictureBox pbcheck8;

		private PictureBox pbcheck7;

		private CheckBox checkBox4;

		private CheckBox checkBox5;

		private CheckBox checkBox6;

		private Button button3;

		private Button btnAnterior;

		private TabControl tabCapturaFacial;

		private TableLayoutPanel tableLayoutPanel2;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label1;

		private TableLayoutPanel tableLayoutPanel11;

		private TableLayoutPanel tableLayoutPanel4;

		private TableLayoutPanel tableLayoutPanel3;

		private Label label13;

		private TableLayoutPanel tableLayoutPanel5;

		private Label label2;

		private TableLayoutPanel tableLayoutPanel12;

		private TableLayoutPanel tableLayoutPanel6;

		private TableLayoutPanel tableLayoutPanel8;

		private TableLayoutPanel tableLayoutPanel7;

		private Label label4;

		private TableLayoutPanel tableLayoutPanel10;

		private TableLayoutPanel tableLayoutPanel9;

		private Label label5;

		private Panel panel2;

		private PictureBox pictureBox1;

		private Panel panel4;

		public fCapturaFacial()
		{
			this.InitializeComponent();
		}

		private void ActualizarRecordDataBodyFace()
		{
			try
			{
				if (this.PersonaCapturada == null)
				{
					this.PersonaCapturada = new CapturedPerson();
				}
				if (this.PersonaCapturada.BasicData == null)
				{
					this.PersonaCapturada.BasicData = new LightPersonBasicData();
				}
				if (this.PersonaCapturada.RecordData == null)
				{
					this.PersonaCapturada.RecordData = new RecordData();
				}
				if (this.PersonaCapturada.RecordData.Body == null)
				{
					this.PersonaCapturada.RecordData.Body = new FaceData();
				}
				if (this.PersonaCapturada.RecordData.FaceFrontal == null)
				{
					this.PersonaCapturada.RecordData.FaceFrontal = new FaceData();
				}
				if (this.BitmapRostroFrontal != null)
				{
					this.PersonaCapturada.RecordData.FaceFrontal.OriginalImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontal, 1000));
				}
				if (this.BitmapRostroFrontalCrop != null)
				{
					this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontalCrop, 1500));
					this.PersonaCapturada.BasicData.PhotoArr = this.ser.RedimensionarByteArray(this.ser.ImageToByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontalCrop, 500)), 48);
				}
				if (this.BitmapRostroFrontalCropRegla != null)
				{
					this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr = this.ser.ImagenToJPGByteArray((Bitmap)this.ser.CambiarTamanoImagen(this.BitmapRostroFrontalCropRegla, 1000));
				}
				fEnrolar.PersonaCapturada = this.PersonaCapturada;
				if (this.txtid.Text != this.PersonaCapturada.OfflinePerson.Identities[0].Identification)
				{
					this.PersonaCapturada.OfflinePerson.Identities[0].Identification = this.txtid.Text;
				}
				if (this.numEstatura.Value != Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize))
				{
					this.PersonaCapturada.RecordData.BodySize = Convert.ToInt32(this.numEstatura.Value);
				}
				(new Serializer()).SerializeEpd(this.PersonaCapturada, fPrincipal.RutaEpd);
			}
			catch (Exception exception)
			{
			}
		}

		private void ActualizarRecordDataBodyFace2(string newFolderName)
		{
			try
			{
				if (Directory.Exists(newFolderName))
				{
					if (this.PersonaCapturada == null)
					{
						this.PersonaCapturada = new CapturedPerson();
					}
					if (this.PersonaCapturada.BasicData == null)
					{
						this.PersonaCapturada.BasicData = new LightPersonBasicData();
					}
					if (this.PersonaCapturada.RecordData == null)
					{
						this.PersonaCapturada.RecordData = new RecordData();
					}
					string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
					string str = Path.DirectorySeparatorChar.ToString();
					char directorySeparatorChar = Path.DirectorySeparatorChar;
					string ruta = string.Concat(directoryName, str, "Rostro", directorySeparatorChar.ToString());
					if (this.PersonaCapturada.RecordData.Body == null)
					{
						this.PersonaCapturada.RecordData.Body = new FaceData();
					}
					if (File.Exists(string.Concat(ruta, "CuerpoOriginal.bmp")))
					{
						this.PersonaCapturada.RecordData.Body.OriginalImageArr = File.ReadAllBytes(string.Concat(ruta, "CuerpoOriginal.bmp"));
					}
					if (File.Exists(string.Concat(ruta, "CuerpoNormal.bmp")))
					{
						this.PersonaCapturada.RecordData.Body.NormalizedImageArr = File.ReadAllBytes(string.Concat(ruta, "CuerpoNormal.bmp"));
					}
					if (File.Exists(string.Concat(ruta, "CuerpoRegla.bmp")))
					{
						this.PersonaCapturada.RecordData.Body.RuledImageArr = File.ReadAllBytes(string.Concat(ruta, "CuerpoRegla.bmp"));
					}
					if (this.PersonaCapturada.RecordData.FaceFrontal == null)
					{
						this.PersonaCapturada.RecordData.FaceFrontal = new FaceData();
					}
					if (this.BitmapRostroFrontal != null)
					{
						this.PersonaCapturada.RecordData.FaceFrontal.OriginalImageArr = this.ser.ImagenToPNGByteArray(this.BitmapRostroFrontal);
					}
					if (this.BitmapRostroFrontalCrop != null)
					{
						this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr = this.ser.ImagenToPNGByteArray(this.BitmapRostroFrontalCrop);
						this.PersonaCapturada.BasicData.PhotoArr = this.ser.RedimensionarByteArray(this.ser.ImageToByteArray(this.BitmapRostroFrontalCrop), 48);
					}
					if (this.BitmapRostroFrontalCropRegla != null)
					{
						this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr = this.ser.ImagenToPNGByteArray(this.BitmapRostroFrontalCropRegla);
					}
					if (this.PersonaCapturada.RecordData.FaceRightProfile == null)
					{
						this.PersonaCapturada.RecordData.FaceRightProfile = new FaceData();
					}
					if (File.Exists(string.Concat(ruta, "PerfilDerOriginal.bmp")))
					{
						this.PersonaCapturada.RecordData.FaceRightProfile.OriginalImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilDerOriginal.bmp"));
					}
					if (File.Exists(string.Concat(ruta, "PerfilDerNormal.bmp")))
					{
						this.PersonaCapturada.RecordData.FaceRightProfile.NormalizedImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilDerNormal.bmp"));
					}
					if (File.Exists(string.Concat(ruta, "PerfilDerRegla.bmp")))
					{
						this.PersonaCapturada.RecordData.FaceRightProfile.RuledImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilDerRegla.bmp"));
					}
					if (this.PersonaCapturada.RecordData.FaceLeftProfile == null)
					{
						this.PersonaCapturada.RecordData.FaceLeftProfile = new FaceData();
					}
					if (File.Exists(string.Concat(ruta, "PerfilIzqOriginal.bmp")))
					{
						this.PersonaCapturada.RecordData.FaceLeftProfile.OriginalImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilIzqOriginal.bmp"));
					}
					if (File.Exists(string.Concat(ruta, "PerfilIzqNormal.bmp")))
					{
						this.PersonaCapturada.RecordData.FaceLeftProfile.NormalizedImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilIzqNormal.bmp"));
					}
					if (File.Exists(string.Concat(ruta, "PerfilIzqRegla.bmp")))
					{
						this.PersonaCapturada.RecordData.FaceLeftProfile.RuledImageArr = File.ReadAllBytes(string.Concat(ruta, "PerfilIzqRegla.bmp"));
					}
					fEnrolar.PersonaCapturada = this.PersonaCapturada;
					if (this.txtid.Text != this.PersonaCapturada.OfflinePerson.Identities[0].Identification)
					{
						this.PersonaCapturada.OfflinePerson.Identities[0].Identification = this.txtid.Text;
					}
					if (this.numEstatura.Value != Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize))
					{
						this.PersonaCapturada.RecordData.BodySize = Convert.ToInt32(this.numEstatura.Value);
					}
					(new Serializer()).SerializeEpd(this.PersonaCapturada, fPrincipal.RutaEpd);
				}
			}
			catch (Exception exception)
			{
			}
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

		private void btnAnterior_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tabCapturaFacial.SelectedIndex == 1)
				{
					this.btnAnterior.Enabled = false;
				}
				this.tabCapturaFacial.SelectTab(this.tabCapturaFacial.SelectedIndex - 1);
				this.btnsiguiente.Enabled = true;
				this.btnsiguiente.Text = "Siguiente";
			}
			catch (Exception exception)
			{
			}
		}

		private void btncancelar_Click(object sender, EventArgs e)
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

		private void btnCargarCuerpo_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.btnOriginalCuerpo.Enabled = false;
					this.BitmapCuerpo = new Bitmap(ofd.FileName);
					this.pbCuerpo.Image = this.BitmapCuerpo;
					this.pbCuerpo.Enabled = true;
					this.pbcheck1.Visible = true;
					this.pbcheck2.Visible = false;
					this.pbcheck3.Visible = false;
				}
				Button button = this.btnsiguiente;
				bool num = false;
				bool flag = (bool)num;
				this.btnOriginalCuerpo.Enabled = (bool)num;
				button.Enabled = flag;
			}
			catch
			{
			}
		}

		private void btnCargarDer_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.btnOriginalDer.Enabled = false;
					this.BitmapPerfilDerecho = null;
					this.BitmapPerfilDerechoCrop = null;
					this.BitmapPerfilDerechoCropRegla = null;
					this.BitmapPerfilDerecho = new Bitmap(ofd.FileName);
					this.pbDer.Image = this.BitmapPerfilDerecho;
					this.pbDer.Enabled = true;
					this.pbcheck7.Visible = true;
					this.pbcheck8.Visible = false;
					this.pbcheck9.Visible = false;
				}
				Button button = this.btnsiguiente;
				bool num = false;
				bool flag = (bool)num;
				this.btnOriginalDer.Enabled = (bool)num;
				button.Enabled = flag;
			}
			catch
			{
			}
		}

		private void btnCargarFrontal_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				{
					this.btnsiguiente.Enabled = false;
				}
				else
				{
					this.AuxNuevaImagen = 1;
					this.BitmapRostroFrontal = null;
					this.BitmapRostroFrontalCrop = null;
					this.BitmapRostroFrontalCropReglaAux = null;
					this.BitmapRostroFrontalCropRegla = null;
					this.pbCapturaFrontal.Image = null;
					this.pbImagenRecorte.Image = null;
					this.pbFrontalRegla.Image = null;
					this.BitmapRostroFrontal = new Bitmap(ofd.FileName);
					this.pbCapturaFrontal.Image = this.BitmapRostroFrontal;
					this.pbCapturaFrontal.Enabled = true;
					bool num = true;
					bool flag = (bool)num;
					this.btnsiguiente.Enabled = (bool)num;
					this.nuevofrontal = flag;
				}
			}
			catch
			{
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void btnCargarIzq_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.BitmapPerfilIzquierdo = null;
					this.BitmapPerfilIzquierdoCrop = null;
					this.BitmapPerfilIzquierdoCropRegla = null;
					this.BitmapPerfilIzquierdo = new Bitmap(ofd.FileName);
					this.pbIzq.Image = this.BitmapPerfilIzquierdo;
					this.pbIzq.Enabled = true;
					this.pbcheck4.Visible = true;
					this.pbcheck5.Visible = false;
					this.pbcheck6.Visible = false;
				}
				Button button = this.btnsiguiente;
				bool num = false;
				bool flag = (bool)num;
				this.btnOriginalIzq.Enabled = (bool)num;
				button.Enabled = flag;
			}
			catch
			{
			}
		}

		private void btnFoto_Click(object sender, EventArgs e)
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
					this.AuxNuevaImagen = 1;
					this.BitmapRostroFrontal = null;
					this.BitmapRostroFrontalCrop = null;
					this.BitmapRostroFrontalCropReglaAux = null;
					this.BitmapRostroFrontalCropRegla = null;
					this.pbCapturaFrontal.Image = null;
					this.pbImagenRecorte.Image = null;
					this.pbFrontalRegla.Image = null;
					this.BitmapRostroFrontal = Foto.Captura;
					this.pbCapturaFrontal.Image = this.BitmapRostroFrontal;
					bool num = true;
					bool flag = (bool)num;
					this.pbCapturaFrontal.Enabled = (bool)num;
					this.nuevofrontal = flag;
				}
				this.btnsiguiente.Enabled = this.pbCapturaFrontal.Image != null;
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
				this.ActualizarRecordDataBodyFace();
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
			}
			catch (Exception exception)
			{
			}
		}

		private void btnOriginalCuerpo_Click(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists("Rostro/CuerpoOriginal.bmp"))
				{
					this.imgInput = new Image<Bgr, byte>("Rostro/CuerpoOriginal.bmp");
				}
				else if (this.PersonaCapturada.RecordData.Body.OriginalImage != null)
				{
					this.imgInput = new Image<Bgr, byte>(this.PersonaCapturada.RecordData.Body.OriginalImage);
				}
				this.pbCuerpo.Image = this.imgInput.Bitmap;
				Button button = this.btnsiguiente;
				bool num = false;
				bool flag = (bool)num;
				this.btnOriginalCuerpo.Enabled = (bool)num;
				button.Enabled = flag;
			}
			catch (Exception exception)
			{
			}
		}

		private void btnOriginalDer_Click(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists("Rostro/PerfilDerOriginal.bmp"))
				{
					this.imgInput = new Image<Bgr, byte>("Rostro/PerfilDerOriginal.bmp");
				}
				else if (this.PersonaCapturada.RecordData.FaceRightProfile.OriginalImage != null)
				{
					this.imgInput = new Image<Bgr, byte>(this.PersonaCapturada.RecordData.FaceRightProfile.OriginalImage);
				}
				this.pbDer.Image = this.imgInput.Bitmap;
				Button button = this.btnsiguiente;
				bool num = false;
				bool flag = (bool)num;
				this.btnOriginalDer.Enabled = (bool)num;
				button.Enabled = flag;
			}
			catch (Exception exception)
			{
			}
		}

		private void btnOriginalIzq_Click(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists("Rostro/PerfilIzqOriginal.bmp"))
				{
					this.imgInput = new Image<Bgr, byte>("Rostro/PerfilIzqOriginal.bmp");
				}
				else if (this.PersonaCapturada.RecordData.FaceLeftProfile.OriginalImage != null)
				{
					this.imgInput = new Image<Bgr, byte>(this.PersonaCapturada.RecordData.FaceLeftProfile.OriginalImage);
				}
				this.pbIzq.Image = this.imgInput.Bitmap;
				Button button = this.btnsiguiente;
				bool num = false;
				bool flag = (bool)num;
				this.btnOriginalIzq.Enabled = (bool)num;
				button.Enabled = flag;
			}
			catch (Exception exception)
			{
			}
		}

		private void btnsiguiente_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				switch (this.tabCapturaFacial.SelectedIndex)
				{
					case 0:
					{
						if (this.ValidaModeloDatosGenerales())
						{
							this.btnAnterior.Enabled = true;
							this.tabCapturaFacial.SelectTab(1);
						}
						break;
					}
					case 1:
					{
						this.CargaPestanaCalidaFrontal();
						this.tabCapturaFacial.SelectTab(2);
						break;
					}
					case 2:
					{
						this.CargaPestanaReglaFrontal();
						this.btnsiguiente.Enabled = true;
						this.btnsiguiente.Text = "Finalizar";
						if (this.BitmapRostroFrontalCropRegla == null)
						{
							this.btnsiguiente.Enabled = false;
						}
						this.tabCapturaFacial.SelectTab(3);
						break;
					}
					case 3:
					{
						this.ActualizarRecordDataBodyFace();
						this.ForceClose = true;
						if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ? ", true))
						{
							(new fEnrolar()
							{
								MdiParent = base.ParentForm
							}).Show();
							base.Close();
						}
						break;
					}
					case 4:
					{
						this.tabCapturaFacial.SelectTab(5);
						this.CargaPestanaPerfilIzq();
						this.pbcheck4.Visible = false;
						this.pbcheck5.Visible = false;
						this.pbcheck6.Visible = false;
						this.pbcheck7.Visible = false;
						this.pbcheck8.Visible = false;
						this.pbcheck9.Visible = false;
						break;
					}
					case 5:
					{
						this.tabCapturaFacial.SelectTab(6);
						this.CargaPestanaPerfilDer();
						this.pbcheck7.Visible = false;
						this.pbcheck8.Visible = false;
						this.pbcheck9.Visible = false;
						break;
					}
					case 6:
					{
						this.tabCapturaFacial.SelectTab(7);
						this.CargaResumen();
						this.btnsiguiente.Text = "Finalizar";
						break;
					}
					case 7:
					{
						this.ActualizarRecordDataBodyFace();
						this.ForceClose = true;
						base.Close();
						break;
					}
				}
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch (Exception exception)
			{
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				Foto frm = new Foto();
				if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
				{
				}
				frm.Dispose();
				if (Foto.Captura == null)
				{
					this.btnsiguiente.Enabled = false;
				}
				else
				{
					this.btnOriginalCuerpo.Enabled = false;
					this.BitmapCuerpo = null;
					this.BitmapCuerpoCrop = null;
					this.BitmapCuerpoCropRegla = null;
					this.BitmapCuerpo = Foto.Captura;
					this.pbCuerpo.Image = this.BitmapCuerpo;
					this.pbCuerpo.Enabled = true;
					this.pbcheck1.Visible = true;
					this.pbcheck2.Visible = false;
					this.pbcheck3.Visible = false;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				Foto frm = new Foto();
				if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
				{
				}
				frm.Dispose();
				if (Foto.Captura == null)
				{
					Button button = this.btnsiguiente;
					bool num = false;
					bool flag = (bool)num;
					this.btnOriginalIzq.Enabled = (bool)num;
					button.Enabled = flag;
				}
				else
				{
					this.BitmapPerfilIzquierdo = null;
					this.BitmapPerfilIzquierdoCrop = null;
					this.BitmapPerfilIzquierdoCropRegla = null;
					this.btnOriginalIzq.Enabled = false;
					this.BitmapPerfilIzquierdo = Foto.Captura;
					this.pbIzq.Image = this.BitmapPerfilIzquierdo;
					this.pbIzq.Enabled = true;
					this.pbcheck4.Visible = true;
					this.pbcheck5.Visible = false;
					this.pbcheck6.Visible = false;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				Foto frm = new Foto();
				if (frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
				{
				}
				frm.Dispose();
				if (Foto.Captura == null)
				{
					Button button = this.btnsiguiente;
					bool num = false;
					bool flag = (bool)num;
					this.btnOriginalIzq.Enabled = (bool)num;
					button.Enabled = flag;
				}
				else
				{
					this.BitmapPerfilDerecho = null;
					this.BitmapPerfilDerechoCrop = null;
					this.BitmapPerfilDerechoCropRegla = null;
					this.btnOriginalDer.Enabled = false;
					this.BitmapPerfilDerecho = Foto.Captura;
					this.pbDer.Image = this.BitmapPerfilDerecho;
					this.pbDer.Enabled = true;
					this.pbcheck7.Visible = true;
					this.pbcheck8.Visible = false;
					this.pbcheck9.Visible = false;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void CargaPestana(Button btnOriginal, PictureBox pb, string pNombreArchivo, FaceData pFaceData)
		{
			try
			{
				bool num = false;
				bool flag = (bool)num;
				this.btnsiguiente.Enabled = (bool)num;
				btnOriginal.Enabled = flag;
				if (File.Exists(string.Concat("Rostro/", pNombreArchivo, "Regla.bmp")))
				{
					this.imgInput = new Image<Bgr, byte>(string.Concat("Rostro/", pNombreArchivo, "Regla.bmp"));
					pb.Image = this.imgInput.Bitmap;
					bool num1 = true;
					flag = (bool)num1;
					this.btnsiguiente.Enabled = (bool)num1;
					btnOriginal.Enabled = flag;
				}
				else if (File.Exists(string.Concat("Rostro/", pNombreArchivo, "Normal.bmp")))
				{
					this.imgInput = new Image<Bgr, byte>(string.Concat("Rostro/", pNombreArchivo, "Normal.bmp"));
					pb.Image = this.imgInput.Bitmap;
					btnOriginal.Enabled = true;
				}
				else if (File.Exists(string.Concat("Rostro/", pNombreArchivo, "Original.bmp")))
				{
					this.imgInput = new Image<Bgr, byte>(string.Concat("Rostro/", pNombreArchivo, "Original.bmp"));
					pb.Image = this.imgInput.Bitmap;
				}
				if ((pb.Image != null ? true : pFaceData == null))
				{
					if (pb.Image != null)
					{
						this.btnsiguiente.Enabled = true;
					}
				}
				else if (pFaceData.RuledImage != null)
				{
					this.imgInput = new Image<Bgr, byte>(pFaceData.RuledImage);
					pb.Image = this.imgInput.Bitmap;
					bool num2 = true;
					flag = (bool)num2;
					this.btnsiguiente.Enabled = (bool)num2;
					btnOriginal.Enabled = flag;
				}
				else if (pFaceData.NormalizedImage != null)
				{
					this.imgInput = new Image<Bgr, byte>(pFaceData.NormalizedImage);
					pb.Image = this.imgInput.Bitmap;
					btnOriginal.Enabled = true;
				}
				else if (pFaceData.OriginalImage != null)
				{
					this.imgInput = new Image<Bgr, byte>(pFaceData.OriginalImage);
					pb.Image = this.imgInput.Bitmap;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void CargaPestanaCalidaFrontal()
		{
			try
			{
				int ImageCalidadPorcentaje = 0;
				DataTable dtImg = new DataTable();
				Image imgp = null;
				Icao.ImagenRecorteIcao(this.BitmapRostroFrontal, ref ImageCalidadPorcentaje, ref dtImg, ref imgp);
				if (dtImg.Rows.Count == 0)
				{
					Icao.ImagenRecorteIcao(this.BitmapRostroFrontal, ref ImageCalidadPorcentaje, ref dtImg, ref imgp);
				}
				this.dataGridView1.Visible = false;
				this.dataGridView2.Visible = false;
				this.label8.Text = "";
				this.label7.Text = "";
				if (dtImg.Rows.Count <= 0)
				{
					this.BitmapRostroFrontalCrop = this.BitmapRostroFrontal;
					this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
					this.btnsiguiente.Enabled = true;
					this.label8.Text = "La imagen no cumple con lo requerido.\nSin embargo Ud. puede decidir continuar con el proceso.";
				}
				else if (ImageCalidadPorcentaje < 10)
				{
					this.BitmapRostroFrontalCrop = this.BitmapRostroFrontal;
					this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
					this.btnsiguiente.Enabled = true;
					this.label8.Text = "La imagen no cumple con lo requerido.\nSin embargo Ud. puede decidir continuar con el proceso.";
				}
				else
				{
					DataRow[] dr1 = dtImg.Select("Tipo = 'Rasgos'");
					DataRow[] dr2 = dtImg.Select("Tipo = 'Atributos'");
					DataTable dt1 = dr1.CopyToDataTable<DataRow>();
					DataTable dt2 = dr2.CopyToDataTable<DataRow>();
					string[] TobeDistinct = new string[] { "Detalle", "Valor" };
					DataTable dtRostro = fCapturaFacial.GetDistinctRecords(dt1, TobeDistinct);
					DataTable dtAtribtos = fCapturaFacial.GetDistinctRecords(dt2, TobeDistinct);
					this.dataGridView1.DataSource = dtRostro;
					this.dataGridView2.DataSource = dtAtribtos;
					this.dataGridView1.Visible = true;
					this.dataGridView2.Visible = true;
					this.label8.Text = "RASGOS FACIALES";
					this.label7.Text = "ATRIBUTOS FACIALES";
					PictureBox vPictureBox = new PictureBox();
					this.BitmapRostroFrontalCrop = (Bitmap)imgp;
					this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
					this.btnsiguiente.Enabled = true;
				}
			}
			catch (Exception exception)
			{
				this.BitmapRostroFrontalCrop = this.BitmapRostroFrontal;
				this.pbImagenRecorte.Image = this.BitmapRostroFrontalCrop;
				this.btnsiguiente.Enabled = true;
				this.label8.Text = "La imagen no cumple con lo requerido.\nSin embargo Ud. puede decidir continuar con el proceso.";
			}
		}

		private void CargaPestanaCuerpo()
		{
			try
			{
				FaceData faceData = null;
				if ((this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null ? false : this.PersonaCapturada.RecordData.Body != null))
				{
					faceData = this.PersonaCapturada.RecordData.Body;
				}
				this.CargaPestana(this.btnOriginalCuerpo, this.pbCuerpo, "Cuerpo", faceData);
				this.pbcheck1.Visible = false;
				this.pbcheck2.Visible = false;
				this.pbcheck3.Visible = false;
			}
			catch (Exception exception)
			{
			}
		}

		private void CargaPestanaFotoFrontal()
		{
			try
			{
				if (this.pbCapturaFrontal.Image != null)
				{
					this.btnsiguiente.Enabled = true;
				}
				else
				{
					this.btnsiguiente.Enabled = false;
				}
				if (this.PersonaCapturada != null)
				{
					if (this.PersonaCapturada.RecordData != null)
					{
						if (this.PersonaCapturada.RecordData.FaceFrontal != null)
						{
							if (this.PersonaCapturada.RecordData.FaceFrontal.OriginalImage != null)
							{
								this.BitmapRostroFrontal = this.PersonaCapturada.RecordData.FaceFrontal.OriginalImage;
								this.pbCapturaFrontal.Image = this.BitmapRostroFrontal;
								this.btnsiguiente.Enabled = true;
							}
						}
					}
				}
				else if (this.pbCapturaFrontal.Image != null)
				{
					this.btnsiguiente.Enabled = true;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void CargaPestanaPerfilDer()
		{
			try
			{
				FaceData faceData = null;
				if ((this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null ? false : this.PersonaCapturada.RecordData.FaceRightProfile != null))
				{
					faceData = this.PersonaCapturada.RecordData.FaceRightProfile;
				}
				this.CargaPestana(this.btnOriginalDer, this.pbDer, "PerfilDer", faceData);
			}
			catch (Exception exception)
			{
			}
		}

		private void CargaPestanaPerfilIzq()
		{
			try
			{
				FaceData faceData = null;
				if ((this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null ? false : this.PersonaCapturada.RecordData.FaceLeftProfile != null))
				{
					faceData = this.PersonaCapturada.RecordData.FaceLeftProfile;
				}
				this.CargaPestana(this.btnOriginalIzq, this.pbIzq, "PerfilIzq", faceData);
			}
			catch (Exception exception)
			{
			}
		}

		private void CargaPestanaReglaFrontal()
		{
			try
			{
				if (this.AuxNuevaImagen == 1)
				{
					if (this.BitmapRostroFrontalCropRegla == null)
					{
						this.pbFrontalRegla.Image = this.BitmapRostroFrontalCrop;
					}
					else
					{
						this.pbFrontalRegla.Image = this.BitmapRostroFrontalCropReglaAux;
					}
				}
				else if ((this.pbFrontalRegla.Image != null ? false : this.PersonaCapturada.RecordData != null))
				{
					if (this.PersonaCapturada.RecordData.FaceFrontal != null)
					{
						if (this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr != null)
						{
							this.pbFrontalRegla.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr);
							this.btnsiguiente.Enabled = true;
						}
						else if (this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr != null)
						{
							this.pbFrontalRegla.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceFrontal.NormalizedImageArr);
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void CargaResumen()
		{
			try
			{
				if (!File.Exists("Rostro/PerfilIzqRegla.bmp"))
				{
					this.pbResumenIzq.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceLeftProfile.RuledImageArr);
				}
				else
				{
					FileStream fs = new FileStream("Rostro/PerfilIzqRegla.bmp", FileMode.Open, FileAccess.Read);
					this.pbResumenIzq.Image = Image.FromStream(fs);
					fs.Close();
				}
				if (!File.Exists("Rostro/PerfilDerRegla.bmp"))
				{
					this.pbResumenDer.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceRightProfile.RuledImageArr);
				}
				else
				{
					FileStream fs = new FileStream("Rostro/PerfilDerRegla.bmp", FileMode.Open, FileAccess.Read);
					this.pbResumenDer.Image = Image.FromStream(fs);
					fs.Close();
				}
				if (!File.Exists("Rostro/FrontalRegla.bmp"))
				{
					this.pbResumenFrontal.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.FaceFrontal.RuledImageArr);
				}
				else
				{
					FileStream fs = new FileStream("Rostro/FrontalRegla.bmp", FileMode.Open, FileAccess.Read);
					this.pbResumenFrontal.Image = Image.FromStream(fs);
					fs.Close();
				}
				if (!File.Exists("Rostro/CuerpoRegla.bmp"))
				{
					this.pbResumenCuerpo.Image = this.ser.byteArrayToImage(this.PersonaCapturada.RecordData.Body.RuledImageArr);
				}
				else
				{
					FileStream fs = new FileStream("Rostro/CuerpoRegla.bmp", FileMode.Open, FileAccess.Read);
					this.pbResumenCuerpo.Image = Image.FromStream(fs);
					fs.Close();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Valor")
				{
					if (e.Value.ToString() == "0")
					{
						e.CellStyle.BackColor = Color.Red;
						e.Value = "No Detectado";
					}
					if (e.Value.ToString() == "1")
					{
						e.CellStyle.BackColor = Color.Yellow;
						e.Value = "Moderado";
					}
					if (e.Value.ToString() == "2")
					{
						e.CellStyle.BackColor = Color.GreenYellow;
						e.Value = "Confiable";
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				if (this.dataGridView2.Columns[e.ColumnIndex].Name == "Valor2")
				{
					string valorreal = e.Value.ToString();
					string valor_sub = valorreal.Substring(0, 1);
					string valor_grilla = valorreal.Substring(1);
					if (valor_sub == "E")
					{
						e.CellStyle.BackColor = Color.Red;
						e.Value = " Malo";
					}
					if (valor_sub == "0")
					{
						e.CellStyle.BackColor = Color.Red;
						e.Value = valor_grilla;
					}
					if (valor_sub == "1")
					{
						e.CellStyle.BackColor = Color.Yellow;
						e.Value = valor_grilla;
					}
					if (valor_sub == "2")
					{
						e.CellStyle.BackColor = Color.GreenYellow;
						e.Value = valor_grilla;
					}
					if (valor_sub == "9")
					{
						e.Value = valor_grilla;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void DibujaRegla(Bitmap bitmap, PictureBox pb, float y)
		{
			decimal value;
			string str;
			string str1;
			string str2;
			try
			{
				float tamanio = (float)bitmap.Height * 0.02f;
				this.escala = (float)bitmap.Height / (float)pb.Height;
				float pDitancia = (float)bitmap.Height / 21.7f;
				float pInicial = y * this.escala - pDitancia / 2f;
				if (this.saldoY <= 0f)
				{
					this.saldoY = 0f;
				}
				pInicial -= (float)this.saldoY;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					using (System.Drawing.Font arialFont = new System.Drawing.Font("Arial", tamanio, FontStyle.Regular))
					{
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - pDitancia));
						Graphics graphic = graphics;
						if (this.numEstatura.Value > new decimal(99))
						{
							value = this.numEstatura.Value;
							str = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value;
							str = string.Concat(value.ToString(), "______");
						}
						graphic.DrawString(str, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 4f * pDitancia));
						Graphics graphic1 = graphics;
						if ((this.numEstatura.Value - new decimal(10)) > new decimal(99))
						{
							value = this.numEstatura.Value - new decimal(10);
							str1 = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value - new decimal(10);
							str1 = string.Concat(value.ToString(), "______");
						}
						graphic1.DrawString(str1, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 5f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 5f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 6f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 7f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 8f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 9f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 6f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 7f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 8f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 9f * pDitancia));
						Graphics graphic2 = graphics;
						if ((this.numEstatura.Value - new decimal(20)) > new decimal(99))
						{
							value = this.numEstatura.Value - new decimal(20);
							str2 = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value - new decimal(20);
							str2 = string.Concat(value.ToString(), "______");
						}
						graphic2.DrawString(str2, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 10f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 10f * pDitancia));
						graphics.DrawString(this.txtid.Text, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), (float)(bitmap.Height / 21 * 20)));
					}
				}
				if (pb.Name != "pbFrontalRegla")
				{
					this.BitmapPerfilDerechoCropRegla = bitmap;
					this.pbcheck4.Visible = true;
					this.pbcheck5.Visible = true;
					this.pbcheck6.Visible = true;
				}
				else
				{
					this.BitmapRostroFrontalCropRegla = bitmap;
				}
				pb.Image = bitmap;
			}
			catch (Exception exception)
			{
			}
		}

		private void DibujaReglaCuerpo(Bitmap bitmap, PictureBox pb, float y)
		{
			decimal value;
			string str;
			try
			{
				float tamanio = (float)bitmap.Height * 0.02f;
				this.escala = (float)bitmap.Height / (float)pb.Height;
				float pDitancia = (float)bitmap.Height / 21.7f;
				float pInicial = y * this.escala - pDitancia / 2f;
				if (this.saldoY <= 0f)
				{
					this.saldoY = 0f;
				}
				pInicial -= (float)this.saldoY;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					using (System.Drawing.Font arialFont = new System.Drawing.Font("Arial", tamanio, FontStyle.Regular))
					{
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 5f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 6f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 7f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 8f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 9f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 10f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 5f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 6f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 7f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 8f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 9f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 10f * pDitancia));
						Graphics graphic = graphics;
						if (this.numEstatura.Value > new decimal(99))
						{
							value = this.numEstatura.Value;
							str = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value;
							str = string.Concat(value.ToString(), "______");
						}
						graphic.DrawString(str, arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial));
						graphics.DrawString(this.txtid.Text, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 4), (float)(bitmap.Height / 21 * 20)));
					}
				}
				pb.Image = bitmap;
				this.pbcheck1.Visible = true;
				this.pbcheck2.Visible = true;
				this.pbcheck3.Visible = true;
			}
			catch (Exception exception)
			{
			}
		}

		private void DibujaReglaDer(Bitmap bitmap, PictureBox pb, float y)
		{
			decimal value;
			string str;
			string str1;
			string str2;
			try
			{
				float tamanio = (float)bitmap.Height * 0.02f;
				this.escala = (float)bitmap.Height / (float)pb.Height;
				float pDitancia = (float)bitmap.Height / 21.7f;
				float pInicial = y * this.escala - pDitancia / 2f;
				if (this.saldoY <= 0f)
				{
					this.saldoY = 0f;
				}
				pInicial -= (float)this.saldoY;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					using (System.Drawing.Font arialFont = new System.Drawing.Font("Arial", tamanio, FontStyle.Regular))
					{
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial - pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial - pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial));
						Graphics graphic = graphics;
						if (this.numEstatura.Value > new decimal(99))
						{
							value = this.numEstatura.Value;
							str = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value;
							str = string.Concat(value.ToString(), "______");
						}
						graphic.DrawString(str, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 2f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 3f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 4f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 5f * pDitancia));
						Graphics graphic1 = graphics;
						if ((this.numEstatura.Value - new decimal(10)) > new decimal(99))
						{
							value = this.numEstatura.Value - new decimal(10);
							str1 = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value - new decimal(10);
							str1 = string.Concat(value.ToString(), "______");
						}
						graphic1.DrawString(str1, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 5f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 6f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 7f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 8f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 9f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 6f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 7f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 8f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 9f * pDitancia));
						graphics.DrawString("________", arialFont, new SolidBrush(Color.Black), new PointF(4f, pInicial + 10f * pDitancia));
						Graphics graphic2 = graphics;
						if ((this.numEstatura.Value - new decimal(20)) > new decimal(99))
						{
							value = this.numEstatura.Value - new decimal(20);
							str2 = string.Concat(value.ToString(), "_____");
						}
						else
						{
							value = this.numEstatura.Value - new decimal(20);
							str2 = string.Concat(value.ToString(), "______");
						}
						graphic2.DrawString(str2, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), pInicial + 10f * pDitancia));
						graphics.DrawString(this.txtid.Text, arialFont, new SolidBrush(Color.Black), new PointF((float)(bitmap.Width / 6 * 5), (float)(bitmap.Height / 21 * 20)));
					}
				}
				pb.Image = bitmap;
				this.pbcheck7.Visible = true;
				this.pbcheck8.Visible = true;
				this.pbcheck9.Visible = true;
			}
			catch (Exception exception)
			{
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

		private void fCapturaFacial_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void fCapturaFacial_Load(object sender, EventArgs e)
		{
			this.AuxNuevaImagen = 0;
			this.pbCapturaFrontal.Image = null;
			this.pbImagenRecorte.Image = null;
			this.pbFrontalRegla.Image = null;
			try
			{
				if (this.PersonaCapturada == null)
				{
					this.PersonaCapturada = new CapturedPerson();
				}
				if (this.PersonaCapturada.BasicData == null)
				{
					this.PersonaCapturada.BasicData = new LightPersonBasicData();
				}
				if (this.PersonaCapturada.RecordData == null)
				{
					this.PersonaCapturada.RecordData = new RecordData();
				}
				this.PersonaCapturada = fEnrolar.PersonaCapturada;
				string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
				char directorySeparatorChar = Path.DirectorySeparatorChar;
				string NewFolderName = string.Concat(directoryName, directorySeparatorChar.ToString(), "Rostro");
				if (!Directory.Exists(NewFolderName))
				{
					Directory.CreateDirectory(NewFolderName);
				}
				if ((this.PersonaCapturada == null ? false : this.PersonaCapturada.OfflinePerson != null))
				{
					this.txtid.Text = this.PersonaCapturada.OfflinePerson.Identities[0].Identification;
					this.numEstatura.Value = Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize);
				}
				this.btnsiguiente.Enabled = this.ValidaModeloDatosGenerales();
				this.CargaPestanaFotoFrontal();
				if ((this.PersonaCapturada.OfflinePerson.Identities[0].Identification == null ? true : Convert.ToDecimal(this.PersonaCapturada.RecordData.BodySize) <= new decimal(49)))
				{
					this.btnsiguiente.Enabled = false;
				}
				else
				{
					this.btnsiguiente.Enabled = true;
				}
			}
			catch
			{
			}
		}

		public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
		{
			DataTable table;
			try
			{
				DataTable dtUniqRecords = new DataTable();
				table = dt.DefaultView.ToTable(true, Columns);
			}
			catch (Exception exception)
			{
				table = null;
			}
			return table;
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
			try
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
			catch (Exception exception)
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(fCapturaFacial));
			this.tabPage0 = new TabPage();
			this.panel2 = new Panel();
			this.tableLayoutPanel12 = new TableLayoutPanel();
			this.txtid = new TextBox();
			this.numEstatura = new NumericUpDown();
			this.label16 = new Label();
			this.label12 = new Label();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.label2 = new Label();
			this.panel1 = new Panel();
			this.btnAnterior = new Button();
			this.btnAyuda = new Button();
			this.btnguardar = new Button();
			this.btnsiguiente = new Button();
			this.btncancelar = new Button();
			this.tabPage7 = new TabPage();
			this.gbCuerpoEntero = new GroupBox();
			this.pbResumenCuerpo = new PictureBox();
			this.gbPerfilIzq = new GroupBox();
			this.pbResumenIzq = new PictureBox();
			this.gbFrontal = new GroupBox();
			this.pbResumenFrontal = new PictureBox();
			this.gbPerfilDer = new GroupBox();
			this.pbResumenDer = new PictureBox();
			this.label19 = new Label();
			this.label15 = new Label();
			this.tabCapturaFacial = new TabControl();
			this.tabPage1 = new TabPage();
			this.tableLayoutPanel6 = new TableLayoutPanel();
			this.btnCargarFrontal = new Button();
			this.btnFoto = new Button();
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.pbCapturaFrontal = new PictureBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label13 = new Label();
			this.tabPage2 = new TabPage();
			this.tableLayoutPanel8 = new TableLayoutPanel();
			this.label8 = new Label();
			this.dataGridView1 = new DataGridView();
			this.Detalle = new DataGridViewTextBoxColumn();
			this.Valor = new DataGridViewTextBoxColumn();
			this.label7 = new Label();
			this.dataGridView2 = new DataGridView();
			this.Detalle2 = new DataGridViewTextBoxColumn();
			this.Valor2 = new DataGridViewTextBoxColumn();
			this.panel4 = new Panel();
			this.pbImagenRecorte = new PictureBox();
			this.tableLayoutPanel7 = new TableLayoutPanel();
			this.label4 = new Label();
			this.tabPage3 = new TabPage();
			this.tableLayoutPanel10 = new TableLayoutPanel();
			this.pbFrontalRegla = new PictureBox();
			this.tableLayoutPanel9 = new TableLayoutPanel();
			this.label5 = new Label();
			this.tabPage4 = new TabPage();
			this.pbcheck3 = new PictureBox();
			this.pbcheck2 = new PictureBox();
			this.pbcheck1 = new PictureBox();
			this.chkCuerpo3 = new CheckBox();
			this.chkCuerpo2 = new CheckBox();
			this.chkCuerpo1 = new CheckBox();
			this.button1 = new Button();
			this.btnOriginalCuerpo = new Button();
			this.btnCargarCuerpo = new Button();
			this.pbCuerpo = new PictureBox();
			this.tabPage5 = new TabPage();
			this.pbcheck6 = new PictureBox();
			this.pbcheck5 = new PictureBox();
			this.pbcheck4 = new PictureBox();
			this.checkBox1 = new CheckBox();
			this.checkBox2 = new CheckBox();
			this.checkBox3 = new CheckBox();
			this.button2 = new Button();
			this.btnOriginalIzq = new Button();
			this.btnCargarIzq = new Button();
			this.label3 = new Label();
			this.pbIzq = new PictureBox();
			this.tabPage6 = new TabPage();
			this.pbcheck9 = new PictureBox();
			this.pbcheck8 = new PictureBox();
			this.pbcheck7 = new PictureBox();
			this.checkBox4 = new CheckBox();
			this.checkBox5 = new CheckBox();
			this.checkBox6 = new CheckBox();
			this.button3 = new Button();
			this.btnOriginalDer = new Button();
			this.btnCargarDer = new Button();
			this.label6 = new Label();
			this.pbDer = new PictureBox();
			this.errorProvider1 = new ErrorProvider(this.components);
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label1 = new Label();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.tabPage0.SuspendLayout();
			this.tableLayoutPanel12.SuspendLayout();
			((ISupportInitialize)this.numEstatura).BeginInit();
			this.tableLayoutPanel5.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.gbCuerpoEntero.SuspendLayout();
			((ISupportInitialize)this.pbResumenCuerpo).BeginInit();
			this.gbPerfilIzq.SuspendLayout();
			((ISupportInitialize)this.pbResumenIzq).BeginInit();
			this.gbFrontal.SuspendLayout();
			((ISupportInitialize)this.pbResumenFrontal).BeginInit();
			this.gbPerfilDer.SuspendLayout();
			((ISupportInitialize)this.pbResumenDer).BeginInit();
			this.tabCapturaFacial.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((ISupportInitialize)this.pbCapturaFrontal).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.pbImagenRecorte).BeginInit();
			this.tableLayoutPanel7.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel10.SuspendLayout();
			((ISupportInitialize)this.pbFrontalRegla).BeginInit();
			this.tableLayoutPanel9.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((ISupportInitialize)this.pbcheck3).BeginInit();
			((ISupportInitialize)this.pbcheck2).BeginInit();
			((ISupportInitialize)this.pbcheck1).BeginInit();
			((ISupportInitialize)this.pbCuerpo).BeginInit();
			this.tabPage5.SuspendLayout();
			((ISupportInitialize)this.pbcheck6).BeginInit();
			((ISupportInitialize)this.pbcheck5).BeginInit();
			((ISupportInitialize)this.pbcheck4).BeginInit();
			((ISupportInitialize)this.pbIzq).BeginInit();
			this.tabPage6.SuspendLayout();
			((ISupportInitialize)this.pbcheck9).BeginInit();
			((ISupportInitialize)this.pbcheck8).BeginInit();
			((ISupportInitialize)this.pbcheck7).BeginInit();
			((ISupportInitialize)this.pbDer).BeginInit();
			((ISupportInitialize)this.errorProvider1).BeginInit();
			this.tableLayoutPanel11.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.tabPage0.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage0.Controls.Add(this.panel2);
			this.tabPage0.Controls.Add(this.tableLayoutPanel12);
			this.tabPage0.Controls.Add(this.tableLayoutPanel5);
			this.tabPage0.Location = new Point(4, 22);
			this.tabPage0.Name = "tabPage0";
			this.tabPage0.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage0.Size = new System.Drawing.Size(1181, 586);
			this.tabPage0.TabIndex = 7;
			this.panel2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.panel2.Location = new Point(13, 192);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1183, 25);
			this.panel2.TabIndex = 54;
			this.tableLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel12.ColumnCount = 4;
			this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.66721f));
			this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.93344f));
			this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.8447f));
			this.tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.55465f));
			this.tableLayoutPanel12.Controls.Add(this.txtid, 2, 0);
			this.tableLayoutPanel12.Controls.Add(this.numEstatura, 2, 1);
			this.tableLayoutPanel12.Controls.Add(this.label16, 1, 1);
			this.tableLayoutPanel12.Controls.Add(this.label12, 1, 0);
			this.tableLayoutPanel12.ForeColor = Color.White;
			this.tableLayoutPanel12.Location = new Point(231, 66);
			this.tableLayoutPanel12.Name = "tableLayoutPanel12";
			this.tableLayoutPanel12.RowCount = 2;
			this.tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel12.Size = new System.Drawing.Size(693, 85);
			this.tableLayoutPanel12.TabIndex = 53;
			this.txtid.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.txtid.Location = new Point(276, 11);
			this.txtid.Name = "txtid";
			this.txtid.Size = new System.Drawing.Size(235, 20);
			this.txtid.TabIndex = 44;
			this.numEstatura.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.numEstatura.Location = new Point(276, 53);
			this.numEstatura.Maximum = new decimal(new int[] { 250, 0, 0, 0 });
			this.numEstatura.Minimum = new decimal(new int[] { 49, 0, 0, 0 });
			this.numEstatura.Name = "numEstatura";
			this.numEstatura.Size = new System.Drawing.Size(235, 20);
			this.numEstatura.TabIndex = 49;
			this.numEstatura.TextAlign = HorizontalAlignment.Right;
			this.numEstatura.Value = new decimal(new int[] { 49, 0, 0, 0 });
			this.numEstatura.ValueChanged += new EventHandler(this.numEstatura_ValueChanged);
			this.label16.Anchor = AnchorStyles.Right;
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label16.Location = new Point(176, 56);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(94, 14);
			this.label16.TabIndex = 48;
			this.label16.Text = "Estatura (cm):";
			this.label12.Anchor = AnchorStyles.Right;
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label12.Location = new Point(176, 14);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(94, 14);
			this.label12.TabIndex = 50;
			this.label12.Text = "Identificación:";
			this.tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel5.Location = new Point(3, 6);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(1179, 29);
			this.tableLayoutPanel5.TabIndex = 52;
			this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label2.ForeColor = Color.White;
			this.label2.Location = new Point(3, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(1173, 18);
			this.label2.TabIndex = 26;
			this.label2.Text = "DATOS GENERALES";
			this.label2.TextAlign = ContentAlignment.MiddleCenter;
			this.panel1.BackColor = Color.FromArgb(48, 63, 105);
			this.panel1.Controls.Add(this.btnAnterior);
			this.panel1.Controls.Add(this.btnAyuda);
			this.panel1.Controls.Add(this.btnguardar);
			this.panel1.Controls.Add(this.btnsiguiente);
			this.panel1.Controls.Add(this.btncancelar);
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(339, 68);
			this.panel1.TabIndex = 15;
			this.btnAnterior.BackColor = Color.White;
			this.btnAnterior.Image = (Image)resources.GetObject("btnAnterior.Image");
			this.btnAnterior.ImageAlign = ContentAlignment.TopCenter;
			this.btnAnterior.Location = new Point(13, 3);
			this.btnAnterior.Name = "btnAnterior";
			this.btnAnterior.Size = new System.Drawing.Size(75, 60);
			this.btnAnterior.TabIndex = 20;
			this.btnAnterior.Text = "Anterior";
			this.btnAnterior.TextAlign = ContentAlignment.BottomCenter;
			this.btnAnterior.UseVisualStyleBackColor = false;
			this.btnAnterior.Click += new EventHandler(this.btnAnterior_Click);
			this.btnAyuda.BackColor = Color.White;
			this.btnAyuda.Location = new Point(853, 17);
			this.btnAyuda.Name = "btnAyuda";
			this.btnAyuda.Size = new System.Drawing.Size(75, 27);
			this.btnAyuda.TabIndex = 19;
			this.btnAyuda.Text = "Ayuda";
			this.btnAyuda.TextAlign = ContentAlignment.BottomCenter;
			this.btnAyuda.UseVisualStyleBackColor = false;
			this.btnAyuda.Visible = false;
			this.btnguardar.BackColor = Color.White;
			this.btnguardar.Image = (Image)resources.GetObject("btnguardar.Image");
			this.btnguardar.ImageAlign = ContentAlignment.TopCenter;
			this.btnguardar.Location = new Point(175, 3);
			this.btnguardar.Name = "btnguardar";
			this.btnguardar.Size = new System.Drawing.Size(75, 60);
			this.btnguardar.TabIndex = 18;
			this.btnguardar.Text = "Guardar";
			this.btnguardar.TextAlign = ContentAlignment.BottomCenter;
			this.btnguardar.UseVisualStyleBackColor = false;
			this.btnguardar.Click += new EventHandler(this.btnguardar_Click);
			this.btnsiguiente.BackColor = Color.White;
			this.btnsiguiente.Image = (Image)resources.GetObject("btnsiguiente.Image");
			this.btnsiguiente.ImageAlign = ContentAlignment.TopCenter;
			this.btnsiguiente.Location = new Point(94, 3);
			this.btnsiguiente.Name = "btnsiguiente";
			this.btnsiguiente.Size = new System.Drawing.Size(75, 60);
			this.btnsiguiente.TabIndex = 17;
			this.btnsiguiente.Text = "Siguiente";
			this.btnsiguiente.TextAlign = ContentAlignment.BottomCenter;
			this.btnsiguiente.UseVisualStyleBackColor = false;
			this.btnsiguiente.Click += new EventHandler(this.btnsiguiente_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(256, 3);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(75, 60);
			this.btncancelar.TabIndex = 16;
			this.btncancelar.Text = "Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.tabPage7.Controls.Add(this.gbCuerpoEntero);
			this.tabPage7.Controls.Add(this.gbPerfilIzq);
			this.tabPage7.Controls.Add(this.gbFrontal);
			this.tabPage7.Controls.Add(this.gbPerfilDer);
			this.tabPage7.Controls.Add(this.label19);
			this.tabPage7.Location = new Point(4, 22);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(1181, 586);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.UseVisualStyleBackColor = true;
			this.gbCuerpoEntero.Controls.Add(this.pbResumenCuerpo);
			this.gbCuerpoEntero.Location = new Point(642, 39);
			this.gbCuerpoEntero.Name = "gbCuerpoEntero";
			this.gbCuerpoEntero.Size = new System.Drawing.Size(250, 541);
			this.gbCuerpoEntero.TabIndex = 56;
			this.gbCuerpoEntero.TabStop = false;
			this.gbCuerpoEntero.Text = "Cuerpo entero";
			this.pbResumenCuerpo.Dock = DockStyle.Fill;
			this.pbResumenCuerpo.Location = new Point(3, 16);
			this.pbResumenCuerpo.Name = "pbResumenCuerpo";
			this.pbResumenCuerpo.Size = new System.Drawing.Size(244, 522);
			this.pbResumenCuerpo.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbResumenCuerpo.TabIndex = 0;
			this.pbResumenCuerpo.TabStop = false;
			this.gbPerfilIzq.Controls.Add(this.pbResumenIzq);
			this.gbPerfilIzq.Location = new Point(432, 39);
			this.gbPerfilIzq.Name = "gbPerfilIzq";
			this.gbPerfilIzq.Size = new System.Drawing.Size(200, 541);
			this.gbPerfilIzq.TabIndex = 55;
			this.gbPerfilIzq.TabStop = false;
			this.gbPerfilIzq.Text = "Perfil izquierdo";
			this.pbResumenIzq.Dock = DockStyle.Fill;
			this.pbResumenIzq.Location = new Point(3, 16);
			this.pbResumenIzq.Name = "pbResumenIzq";
			this.pbResumenIzq.Size = new System.Drawing.Size(194, 522);
			this.pbResumenIzq.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbResumenIzq.TabIndex = 0;
			this.pbResumenIzq.TabStop = false;
			this.gbFrontal.Controls.Add(this.pbResumenFrontal);
			this.gbFrontal.Location = new Point(222, 39);
			this.gbFrontal.Name = "gbFrontal";
			this.gbFrontal.Size = new System.Drawing.Size(200, 541);
			this.gbFrontal.TabIndex = 54;
			this.gbFrontal.TabStop = false;
			this.gbFrontal.Text = "Frontal";
			this.pbResumenFrontal.Dock = DockStyle.Fill;
			this.pbResumenFrontal.Location = new Point(3, 16);
			this.pbResumenFrontal.Name = "pbResumenFrontal";
			this.pbResumenFrontal.Size = new System.Drawing.Size(194, 522);
			this.pbResumenFrontal.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbResumenFrontal.TabIndex = 0;
			this.pbResumenFrontal.TabStop = false;
			this.gbPerfilDer.Controls.Add(this.pbResumenDer);
			this.gbPerfilDer.Location = new Point(12, 39);
			this.gbPerfilDer.Name = "gbPerfilDer";
			this.gbPerfilDer.Size = new System.Drawing.Size(200, 541);
			this.gbPerfilDer.TabIndex = 53;
			this.gbPerfilDer.TabStop = false;
			this.gbPerfilDer.Text = "Perfil derecho";
			this.pbResumenDer.Dock = DockStyle.Fill;
			this.pbResumenDer.Location = new Point(3, 16);
			this.pbResumenDer.Name = "pbResumenDer";
			this.pbResumenDer.Size = new System.Drawing.Size(194, 522);
			this.pbResumenDer.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbResumenDer.TabIndex = 0;
			this.pbResumenDer.TabStop = false;
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label19.ForeColor = Color.FromArgb(0, 64, 64);
			this.label19.Location = new Point(24, 16);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(145, 20);
			this.label19.TabIndex = 52;
			this.label19.Text = "Datos Generales";
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label15.ForeColor = Color.FromArgb(0, 64, 64);
			this.label15.Location = new Point(24, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(427, 20);
			this.label15.TabIndex = 52;
			this.label15.Text = "Normalizar y colocar estatura (Foto - Cuerpo entero)";
			this.tabCapturaFacial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tabCapturaFacial.Controls.Add(this.tabPage0);
			this.tabCapturaFacial.Controls.Add(this.tabPage1);
			this.tabCapturaFacial.Controls.Add(this.tabPage2);
			this.tabCapturaFacial.Controls.Add(this.tabPage3);
			this.tabCapturaFacial.Controls.Add(this.tabPage4);
			this.tabCapturaFacial.Controls.Add(this.tabPage5);
			this.tabCapturaFacial.Controls.Add(this.tabPage6);
			this.tabCapturaFacial.Controls.Add(this.tabPage7);
			this.tabCapturaFacial.ItemSize = new System.Drawing.Size(89, 0);
			this.tabCapturaFacial.Location = new Point(0, 0);
			this.tabCapturaFacial.Margin = new System.Windows.Forms.Padding(0);
			this.tabCapturaFacial.Multiline = true;
			this.tabCapturaFacial.Name = "tabCapturaFacial";
			this.tabCapturaFacial.SelectedIndex = 0;
			this.tabCapturaFacial.Size = new System.Drawing.Size(1189, 612);
			this.tabCapturaFacial.TabIndex = 14;
			this.tabPage1.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage1.Controls.Add(this.tableLayoutPanel6);
			this.tabPage1.Controls.Add(this.tableLayoutPanel4);
			this.tabPage1.Controls.Add(this.tableLayoutPanel3);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1181, 586);
			this.tabPage1.TabIndex = 0;
			this.tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel6.ColumnCount = 4;
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel6.Controls.Add(this.btnCargarFrontal, 2, 0);
			this.tableLayoutPanel6.Controls.Add(this.btnFoto, 1, 0);
			this.tableLayoutPanel6.Location = new Point(3, 484);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(1171, 58);
			this.tableLayoutPanel6.TabIndex = 56;
			this.btnCargarFrontal.Anchor = AnchorStyles.Left;
			this.btnCargarFrontal.BackColor = Color.White;
			this.btnCargarFrontal.Image = (Image)resources.GetObject("btnCargarFrontal.Image");
			this.btnCargarFrontal.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarFrontal.Location = new Point(587, 5);
			this.btnCargarFrontal.Name = "btnCargarFrontal";
			this.btnCargarFrontal.Size = new System.Drawing.Size(89, 48);
			this.btnCargarFrontal.TabIndex = 10;
			this.btnCargarFrontal.Text = "Cargar Imagen";
			this.btnCargarFrontal.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarFrontal.UseVisualStyleBackColor = false;
			this.btnCargarFrontal.Click += new EventHandler(this.btnCargarFrontal_Click);
			this.btnFoto.Anchor = AnchorStyles.Right;
			this.btnFoto.BackColor = Color.White;
			this.btnFoto.Image = (Image)resources.GetObject("btnFoto.Image");
			this.btnFoto.ImageAlign = ContentAlignment.TopCenter;
			this.btnFoto.Location = new Point(492, 5);
			this.btnFoto.Name = "btnFoto";
			this.btnFoto.Size = new System.Drawing.Size(89, 48);
			this.btnFoto.TabIndex = 53;
			this.btnFoto.Text = "Tomar Foto";
			this.btnFoto.TextAlign = ContentAlignment.BottomCenter;
			this.btnFoto.UseVisualStyleBackColor = false;
			this.btnFoto.Click += new EventHandler(this.btnFoto_Click);
			this.tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
			this.tableLayoutPanel4.Controls.Add(this.pbCapturaFrontal, 1, 0);
			this.tableLayoutPanel4.Location = new Point(6, 41);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(1168, 437);
			this.tableLayoutPanel4.TabIndex = 55;
			this.pbCapturaFrontal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.pbCapturaFrontal.BackColor = Color.Gainsboro;
			this.pbCapturaFrontal.BorderStyle = BorderStyle.Fixed3D;
			this.pbCapturaFrontal.Location = new Point(181, 6);
			this.pbCapturaFrontal.Margin = new System.Windows.Forms.Padding(6);
			this.pbCapturaFrontal.Name = "pbCapturaFrontal";
			this.pbCapturaFrontal.Size = new System.Drawing.Size(805, 425);
			this.pbCapturaFrontal.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbCapturaFrontal.TabIndex = 4;
			this.pbCapturaFrontal.TabStop = false;
			this.pbCapturaFrontal.Paint += new PaintEventHandler(this.pb_Paint);
			this.tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel3.Controls.Add(this.label13, 0, 0);
			this.tableLayoutPanel3.Location = new Point(6, 6);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(1168, 29);
			this.tableLayoutPanel3.TabIndex = 54;
			this.label13.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label13.ForeColor = Color.White;
			this.label13.Location = new Point(3, 5);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(1162, 18);
			this.label13.TabIndex = 26;
			this.label13.Text = "(FOTO FRONTAL)";
			this.label13.TextAlign = ContentAlignment.MiddleCenter;
			this.tabPage2.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage2.Controls.Add(this.tableLayoutPanel8);
			this.tabPage2.Controls.Add(this.tableLayoutPanel7);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(1181, 586);
			this.tabPage2.TabIndex = 5;
			this.tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel8.ColumnCount = 5;
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel8.Controls.Add(this.label8, 2, 0);
			this.tableLayoutPanel8.Controls.Add(this.dataGridView1, 2, 1);
			this.tableLayoutPanel8.Controls.Add(this.label7, 3, 0);
			this.tableLayoutPanel8.Controls.Add(this.dataGridView2, 3, 1);
			this.tableLayoutPanel8.Controls.Add(this.panel4, 1, 1);
			this.tableLayoutPanel8.Location = new Point(3, 38);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 2;
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 7.984791f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 92.01521f));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(1174, 526);
			this.tableLayoutPanel8.TabIndex = 63;
			this.label8.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.ForeColor = Color.White;
			this.label8.Location = new Point(354, 14);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(346, 13);
			this.label8.TabIndex = 58;
			this.label8.Text = "RASGOS FACIALES";
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
			this.dataGridView1.BorderStyle = BorderStyle.None;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Detalle, this.Valor });
			this.dataGridView1.GridColor = SystemColors.ButtonFace;
			this.dataGridView1.Location = new Point(354, 44);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.ScrollBars = ScrollBars.None;
			this.dataGridView1.Size = new System.Drawing.Size(346, 459);
			this.dataGridView1.TabIndex = 56;
			this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
			this.Detalle.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			this.Detalle.DataPropertyName = "Detalle";
			this.Detalle.HeaderText = "Detalle";
			this.Detalle.Name = "Detalle";
			this.Detalle.ReadOnly = true;
			this.Detalle.Width = 190;
			this.Valor.DataPropertyName = "Valor";
			this.Valor.HeaderText = "Valor";
			this.Valor.Name = "Valor";
			this.Valor.ReadOnly = true;
			this.label7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.ForeColor = Color.White;
			this.label7.Location = new Point(706, 14);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(346, 13);
			this.label7.TabIndex = 59;
			this.label7.Text = "ATRIBUTOS FACIALES";
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToDeleteRows = false;
			this.dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.dataGridView2.BackgroundColor = SystemColors.ButtonHighlight;
			this.dataGridView2.BorderStyle = BorderStyle.None;
			this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.ColumnHeadersVisible = false;
			this.dataGridView2.Columns.AddRange(new DataGridViewColumn[] { this.Detalle2, this.Valor2 });
			this.dataGridView2.GridColor = SystemColors.ButtonFace;
			this.dataGridView2.Location = new Point(706, 44);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.ReadOnly = true;
			this.dataGridView2.RowHeadersVisible = false;
			this.dataGridView2.ScrollBars = ScrollBars.None;
			this.dataGridView2.Size = new System.Drawing.Size(346, 459);
			this.dataGridView2.TabIndex = 57;
			this.dataGridView2.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
			this.Detalle2.DataPropertyName = "Detalle";
			this.Detalle2.HeaderText = "Detalle2";
			this.Detalle2.Name = "Detalle2";
			this.Detalle2.ReadOnly = true;
			this.Detalle2.Width = 125;
			this.Valor2.DataPropertyName = "Valor";
			this.Valor2.HeaderText = "Valor2";
			this.Valor2.Name = "Valor2";
			this.Valor2.ReadOnly = true;
			this.Valor2.Width = 190;
			this.panel4.Controls.Add(this.pbImagenRecorte);
			this.panel4.Location = new Point(120, 44);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(207, 365);
			this.panel4.TabIndex = 60;
			this.pbImagenRecorte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pbImagenRecorte.BackColor = Color.Gainsboro;
			this.pbImagenRecorte.BorderStyle = BorderStyle.Fixed3D;
			this.pbImagenRecorte.Location = new Point(-18, 3);
			this.pbImagenRecorte.Name = "pbImagenRecorte";
			this.pbImagenRecorte.Size = new System.Drawing.Size(222, 261);
			this.pbImagenRecorte.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbImagenRecorte.TabIndex = 55;
			this.pbImagenRecorte.TabStop = false;
			this.tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel7.ColumnCount = 1;
			this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel7.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel7.Location = new Point(3, 3);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(1174, 29);
			this.tableLayoutPanel7.TabIndex = 62;
			this.label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label4.ForeColor = Color.White;
			this.label4.Location = new Point(3, 5);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(1168, 18);
			this.label4.TabIndex = 26;
			this.label4.Text = "CALIDAD DE LA FOTO FRONTAL";
			this.label4.TextAlign = ContentAlignment.MiddleCenter;
			this.tabPage3.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage3.Controls.Add(this.tableLayoutPanel10);
			this.tabPage3.Controls.Add(this.tableLayoutPanel9);
			this.tabPage3.Location = new Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(1181, 586);
			this.tabPage3.TabIndex = 8;
			this.tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel10.ColumnCount = 3;
			this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
			this.tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel10.Controls.Add(this.pbFrontalRegla, 1, 0);
			this.tableLayoutPanel10.Location = new Point(3, 38);
			this.tableLayoutPanel10.Name = "tableLayoutPanel10";
			this.tableLayoutPanel10.RowCount = 1;
			this.tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel10.Size = new System.Drawing.Size(1172, 516);
			this.tableLayoutPanel10.TabIndex = 62;
			this.pbFrontalRegla.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.pbFrontalRegla.BackColor = Color.Gainsboro;
			this.pbFrontalRegla.BorderStyle = BorderStyle.Fixed3D;
			this.pbFrontalRegla.Location = new Point(240, 6);
			this.pbFrontalRegla.Margin = new System.Windows.Forms.Padding(6);
			this.pbFrontalRegla.Name = "pbFrontalRegla";
			this.pbFrontalRegla.Size = new System.Drawing.Size(691, 500);
			this.pbFrontalRegla.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbFrontalRegla.TabIndex = 60;
			this.pbFrontalRegla.TabStop = false;
			this.pbFrontalRegla.MouseDown += new MouseEventHandler(this.pbFrontalRegla_MouseDown);
			this.tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel9.ColumnCount = 1;
			this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel9.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel9.Location = new Point(3, 3);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 1;
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
			this.tableLayoutPanel9.Size = new System.Drawing.Size(1172, 29);
			this.tableLayoutPanel9.TabIndex = 61;
			this.label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label5.ForeColor = Color.White;
			this.label5.Location = new Point(3, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(1166, 18);
			this.label5.TabIndex = 26;
			this.label5.Text = "COLOCAR ESTATURA";
			this.label5.TextAlign = ContentAlignment.MiddleCenter;
			this.tabPage4.Controls.Add(this.pbcheck3);
			this.tabPage4.Controls.Add(this.pbcheck2);
			this.tabPage4.Controls.Add(this.pbcheck1);
			this.tabPage4.Controls.Add(this.chkCuerpo3);
			this.tabPage4.Controls.Add(this.chkCuerpo2);
			this.tabPage4.Controls.Add(this.chkCuerpo1);
			this.tabPage4.Controls.Add(this.button1);
			this.tabPage4.Controls.Add(this.btnOriginalCuerpo);
			this.tabPage4.Controls.Add(this.btnCargarCuerpo);
			this.tabPage4.Controls.Add(this.pbCuerpo);
			this.tabPage4.Controls.Add(this.label15);
			this.tabPage4.Location = new Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(1181, 586);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.UseVisualStyleBackColor = true;
			this.pbcheck3.Image = Resources.ok;
			this.pbcheck3.Location = new Point(344, 551);
			this.pbcheck3.Name = "pbcheck3";
			this.pbcheck3.Size = new System.Drawing.Size(35, 25);
			this.pbcheck3.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck3.TabIndex = 62;
			this.pbcheck3.TabStop = false;
			this.pbcheck2.Image = Resources.ok;
			this.pbcheck2.Location = new Point(186, 551);
			this.pbcheck2.Name = "pbcheck2";
			this.pbcheck2.Size = new System.Drawing.Size(35, 25);
			this.pbcheck2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck2.TabIndex = 61;
			this.pbcheck2.TabStop = false;
			this.pbcheck1.Image = Resources.ok;
			this.pbcheck1.Location = new Point(28, 551);
			this.pbcheck1.Name = "pbcheck1";
			this.pbcheck1.Size = new System.Drawing.Size(35, 25);
			this.pbcheck1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck1.TabIndex = 60;
			this.pbcheck1.TabStop = false;
			this.chkCuerpo3.AutoSize = true;
			this.chkCuerpo3.Enabled = false;
			this.chkCuerpo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chkCuerpo3.Location = new Point(361, 557);
			this.chkCuerpo3.Name = "chkCuerpo3";
			this.chkCuerpo3.Size = new System.Drawing.Size(160, 19);
			this.chkCuerpo3.TabIndex = 59;
			this.chkCuerpo3.Text = "Colocar Estatura (Regla)";
			this.chkCuerpo3.UseVisualStyleBackColor = true;
			this.chkCuerpo2.AutoSize = true;
			this.chkCuerpo2.Enabled = false;
			this.chkCuerpo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chkCuerpo2.Location = new Point(202, 556);
			this.chkCuerpo2.Name = "chkCuerpo2";
			this.chkCuerpo2.Size = new System.Drawing.Size(141, 19);
			this.chkCuerpo2.TabIndex = 58;
			this.chkCuerpo2.Text = "Normalizar (Recorte)";
			this.chkCuerpo2.UseVisualStyleBackColor = true;
			this.chkCuerpo1.AutoSize = true;
			this.chkCuerpo1.BackgroundImageLayout = ImageLayout.None;
			this.chkCuerpo1.Enabled = false;
			this.chkCuerpo1.FlatAppearance.BorderColor = Color.Black;
			this.chkCuerpo1.FlatAppearance.BorderSize = 4;
			this.chkCuerpo1.FlatAppearance.CheckedBackColor = Color.Blue;
			this.chkCuerpo1.FlatAppearance.MouseDownBackColor = Color.White;
			this.chkCuerpo1.FlatAppearance.MouseOverBackColor = Color.White;
			this.chkCuerpo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chkCuerpo1.Location = new Point(45, 555);
			this.chkCuerpo1.Margin = new System.Windows.Forms.Padding(5);
			this.chkCuerpo1.Name = "chkCuerpo1";
			this.chkCuerpo1.Size = new System.Drawing.Size(145, 19);
			this.chkCuerpo1.TabIndex = 57;
			this.chkCuerpo1.Text = "Foto o Cargar Imagen";
			this.chkCuerpo1.UseVisualStyleBackColor = true;
			this.button1.BackColor = Color.White;
			this.button1.Image = (Image)resources.GetObject("button1.Image");
			this.button1.ImageAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(560, 552);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 44);
			this.button1.TabIndex = 56;
			this.button1.Text = "Tomar Foto";
			this.button1.TextAlign = ContentAlignment.BottomCenter;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btnOriginalCuerpo.BackColor = Color.White;
			this.btnOriginalCuerpo.Image = (Image)resources.GetObject("btnOriginalCuerpo.Image");
			this.btnOriginalCuerpo.ImageAlign = ContentAlignment.TopCenter;
			this.btnOriginalCuerpo.Location = new Point(781, 553);
			this.btnOriginalCuerpo.Name = "btnOriginalCuerpo";
			this.btnOriginalCuerpo.Size = new System.Drawing.Size(97, 44);
			this.btnOriginalCuerpo.TabIndex = 55;
			this.btnOriginalCuerpo.Text = "Original";
			this.btnOriginalCuerpo.TextAlign = ContentAlignment.BottomCenter;
			this.btnOriginalCuerpo.UseVisualStyleBackColor = false;
			this.btnOriginalCuerpo.Click += new EventHandler(this.btnOriginalCuerpo_Click);
			this.btnCargarCuerpo.BackColor = Color.White;
			this.btnCargarCuerpo.Image = (Image)resources.GetObject("btnCargarCuerpo.Image");
			this.btnCargarCuerpo.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarCuerpo.Location = new Point(670, 553);
			this.btnCargarCuerpo.Name = "btnCargarCuerpo";
			this.btnCargarCuerpo.Size = new System.Drawing.Size(89, 44);
			this.btnCargarCuerpo.TabIndex = 54;
			this.btnCargarCuerpo.Text = "Cargar Imagen";
			this.btnCargarCuerpo.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarCuerpo.UseVisualStyleBackColor = false;
			this.btnCargarCuerpo.Click += new EventHandler(this.btnCargarCuerpo_Click);
			this.pbCuerpo.BackColor = Color.Gainsboro;
			this.pbCuerpo.BorderStyle = BorderStyle.Fixed3D;
			this.pbCuerpo.Cursor = Cursors.Cross;
			this.pbCuerpo.Location = new Point(28, 42);
			this.pbCuerpo.Margin = new System.Windows.Forms.Padding(6);
			this.pbCuerpo.Name = "pbCuerpo";
			this.pbCuerpo.Size = new System.Drawing.Size(850, 500);
			this.pbCuerpo.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbCuerpo.TabIndex = 53;
			this.pbCuerpo.TabStop = false;
			this.pbCuerpo.Paint += new PaintEventHandler(this.pb_Paint);
			this.pbCuerpo.MouseDown += new MouseEventHandler(this.pb_MouseDown);
			this.pbCuerpo.MouseMove += new MouseEventHandler(this.pb_MouseMove);
			this.pbCuerpo.MouseUp += new MouseEventHandler(this.pb_MouseUp);
			this.tabPage5.Controls.Add(this.pbcheck6);
			this.tabPage5.Controls.Add(this.pbcheck5);
			this.tabPage5.Controls.Add(this.pbcheck4);
			this.tabPage5.Controls.Add(this.checkBox1);
			this.tabPage5.Controls.Add(this.checkBox2);
			this.tabPage5.Controls.Add(this.checkBox3);
			this.tabPage5.Controls.Add(this.button2);
			this.tabPage5.Controls.Add(this.btnOriginalIzq);
			this.tabPage5.Controls.Add(this.btnCargarIzq);
			this.tabPage5.Controls.Add(this.label3);
			this.tabPage5.Controls.Add(this.pbIzq);
			this.tabPage5.Location = new Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(1181, 586);
			this.tabPage5.TabIndex = 9;
			this.tabPage5.UseVisualStyleBackColor = true;
			this.pbcheck6.Image = Resources.ok;
			this.pbcheck6.Location = new Point(343, 551);
			this.pbcheck6.Name = "pbcheck6";
			this.pbcheck6.Size = new System.Drawing.Size(35, 25);
			this.pbcheck6.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck6.TabIndex = 69;
			this.pbcheck6.TabStop = false;
			this.pbcheck5.Image = Resources.ok;
			this.pbcheck5.Location = new Point(185, 551);
			this.pbcheck5.Name = "pbcheck5";
			this.pbcheck5.Size = new System.Drawing.Size(35, 25);
			this.pbcheck5.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck5.TabIndex = 68;
			this.pbcheck5.TabStop = false;
			this.pbcheck4.Image = Resources.ok;
			this.pbcheck4.Location = new Point(27, 551);
			this.pbcheck4.Name = "pbcheck4";
			this.pbcheck4.Size = new System.Drawing.Size(35, 25);
			this.pbcheck4.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck4.TabIndex = 67;
			this.pbcheck4.TabStop = false;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Enabled = false;
			this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBox1.Location = new Point(360, 557);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(160, 19);
			this.checkBox1.TabIndex = 66;
			this.checkBox1.Text = "Colocar Estatura (Regla)";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox2.AutoSize = true;
			this.checkBox2.Enabled = false;
			this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBox2.Location = new Point(201, 556);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(141, 19);
			this.checkBox2.TabIndex = 65;
			this.checkBox2.Text = "Normalizar (Recorte)";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox3.AutoSize = true;
			this.checkBox3.BackgroundImageLayout = ImageLayout.None;
			this.checkBox3.Enabled = false;
			this.checkBox3.FlatAppearance.BorderColor = Color.Black;
			this.checkBox3.FlatAppearance.BorderSize = 4;
			this.checkBox3.FlatAppearance.CheckedBackColor = Color.Blue;
			this.checkBox3.FlatAppearance.MouseDownBackColor = Color.White;
			this.checkBox3.FlatAppearance.MouseOverBackColor = Color.White;
			this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBox3.Location = new Point(44, 555);
			this.checkBox3.Margin = new System.Windows.Forms.Padding(5);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(145, 19);
			this.checkBox3.TabIndex = 64;
			this.checkBox3.Text = "Foto o Cargar Imagen";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.button2.BackColor = Color.Gainsboro;
			this.button2.Image = (Image)resources.GetObject("button2.Image");
			this.button2.ImageAlign = ContentAlignment.TopCenter;
			this.button2.Location = new Point(559, 552);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(89, 44);
			this.button2.TabIndex = 63;
			this.button2.Text = "Tomar Foto";
			this.button2.TextAlign = ContentAlignment.BottomCenter;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.btnOriginalIzq.BackColor = Color.Gainsboro;
			this.btnOriginalIzq.Image = (Image)resources.GetObject("btnOriginalIzq.Image");
			this.btnOriginalIzq.ImageAlign = ContentAlignment.TopCenter;
			this.btnOriginalIzq.Location = new Point(781, 551);
			this.btnOriginalIzq.Name = "btnOriginalIzq";
			this.btnOriginalIzq.Size = new System.Drawing.Size(97, 44);
			this.btnOriginalIzq.TabIndex = 59;
			this.btnOriginalIzq.Text = "Original";
			this.btnOriginalIzq.TextAlign = ContentAlignment.BottomCenter;
			this.btnOriginalIzq.UseVisualStyleBackColor = false;
			this.btnOriginalIzq.Click += new EventHandler(this.btnOriginalIzq_Click);
			this.btnCargarIzq.BackColor = Color.Gainsboro;
			this.btnCargarIzq.Image = (Image)resources.GetObject("btnCargarIzq.Image");
			this.btnCargarIzq.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarIzq.Location = new Point(670, 552);
			this.btnCargarIzq.Name = "btnCargarIzq";
			this.btnCargarIzq.Size = new System.Drawing.Size(89, 44);
			this.btnCargarIzq.TabIndex = 58;
			this.btnCargarIzq.Text = "Cargar Imagen";
			this.btnCargarIzq.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarIzq.UseVisualStyleBackColor = false;
			this.btnCargarIzq.Click += new EventHandler(this.btnCargarIzq_Click);
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.ForeColor = Color.FromArgb(0, 64, 64);
			this.label3.Location = new Point(24, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(431, 20);
			this.label3.TabIndex = 56;
			this.label3.Text = "Normalizar y colocar estatura (Foto - Perfil izquierdo)";
			this.pbIzq.BackColor = Color.Gainsboro;
			this.pbIzq.BorderStyle = BorderStyle.Fixed3D;
			this.pbIzq.Location = new Point(28, 42);
			this.pbIzq.Margin = new System.Windows.Forms.Padding(6);
			this.pbIzq.Name = "pbIzq";
			this.pbIzq.Size = new System.Drawing.Size(850, 500);
			this.pbIzq.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbIzq.TabIndex = 57;
			this.pbIzq.TabStop = false;
			this.pbIzq.Paint += new PaintEventHandler(this.pb_Paint);
			this.pbIzq.MouseDown += new MouseEventHandler(this.pb_MouseDown);
			this.pbIzq.MouseMove += new MouseEventHandler(this.pb_MouseMove);
			this.pbIzq.MouseUp += new MouseEventHandler(this.pb_MouseUp);
			this.tabPage6.Controls.Add(this.pbcheck9);
			this.tabPage6.Controls.Add(this.pbcheck8);
			this.tabPage6.Controls.Add(this.pbcheck7);
			this.tabPage6.Controls.Add(this.checkBox4);
			this.tabPage6.Controls.Add(this.checkBox5);
			this.tabPage6.Controls.Add(this.checkBox6);
			this.tabPage6.Controls.Add(this.button3);
			this.tabPage6.Controls.Add(this.btnOriginalDer);
			this.tabPage6.Controls.Add(this.btnCargarDer);
			this.tabPage6.Controls.Add(this.label6);
			this.tabPage6.Controls.Add(this.pbDer);
			this.tabPage6.Location = new Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(1181, 586);
			this.tabPage6.TabIndex = 3;
			this.tabPage6.UseVisualStyleBackColor = true;
			this.pbcheck9.Image = Resources.ok;
			this.pbcheck9.Location = new Point(343, 550);
			this.pbcheck9.Name = "pbcheck9";
			this.pbcheck9.Size = new System.Drawing.Size(35, 25);
			this.pbcheck9.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck9.TabIndex = 76;
			this.pbcheck9.TabStop = false;
			this.pbcheck8.Image = Resources.ok;
			this.pbcheck8.Location = new Point(185, 550);
			this.pbcheck8.Name = "pbcheck8";
			this.pbcheck8.Size = new System.Drawing.Size(35, 25);
			this.pbcheck8.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck8.TabIndex = 75;
			this.pbcheck8.TabStop = false;
			this.pbcheck7.Image = Resources.ok;
			this.pbcheck7.Location = new Point(27, 550);
			this.pbcheck7.Name = "pbcheck7";
			this.pbcheck7.Size = new System.Drawing.Size(35, 25);
			this.pbcheck7.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbcheck7.TabIndex = 74;
			this.pbcheck7.TabStop = false;
			this.checkBox4.AutoSize = true;
			this.checkBox4.Enabled = false;
			this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBox4.Location = new Point(360, 556);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(160, 19);
			this.checkBox4.TabIndex = 73;
			this.checkBox4.Text = "Colocar Estatura (Regla)";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox5.AutoSize = true;
			this.checkBox5.Enabled = false;
			this.checkBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBox5.Location = new Point(201, 555);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(141, 19);
			this.checkBox5.TabIndex = 72;
			this.checkBox5.Text = "Normalizar (Recorte)";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox6.AutoSize = true;
			this.checkBox6.BackgroundImageLayout = ImageLayout.None;
			this.checkBox6.Enabled = false;
			this.checkBox6.FlatAppearance.BorderColor = Color.Black;
			this.checkBox6.FlatAppearance.BorderSize = 4;
			this.checkBox6.FlatAppearance.CheckedBackColor = Color.Blue;
			this.checkBox6.FlatAppearance.MouseDownBackColor = Color.White;
			this.checkBox6.FlatAppearance.MouseOverBackColor = Color.White;
			this.checkBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBox6.Location = new Point(44, 554);
			this.checkBox6.Margin = new System.Windows.Forms.Padding(5);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(145, 19);
			this.checkBox6.TabIndex = 71;
			this.checkBox6.Text = "Foto o Cargar Imagen";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.button3.BackColor = Color.Gainsboro;
			this.button3.Image = (Image)resources.GetObject("button3.Image");
			this.button3.ImageAlign = ContentAlignment.TopCenter;
			this.button3.Location = new Point(559, 551);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(89, 42);
			this.button3.TabIndex = 70;
			this.button3.Text = "Tomar Foto";
			this.button3.TextAlign = ContentAlignment.BottomCenter;
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.btnOriginalDer.BackColor = Color.Gainsboro;
			this.btnOriginalDer.Image = (Image)resources.GetObject("btnOriginalDer.Image");
			this.btnOriginalDer.ImageAlign = ContentAlignment.TopCenter;
			this.btnOriginalDer.Location = new Point(781, 551);
			this.btnOriginalDer.Name = "btnOriginalDer";
			this.btnOriginalDer.Size = new System.Drawing.Size(97, 42);
			this.btnOriginalDer.TabIndex = 63;
			this.btnOriginalDer.Text = "Original";
			this.btnOriginalDer.TextAlign = ContentAlignment.BottomCenter;
			this.btnOriginalDer.UseVisualStyleBackColor = false;
			this.btnOriginalDer.Click += new EventHandler(this.btnOriginalDer_Click);
			this.btnCargarDer.BackColor = Color.Gainsboro;
			this.btnCargarDer.Image = (Image)resources.GetObject("btnCargarDer.Image");
			this.btnCargarDer.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarDer.Location = new Point(670, 552);
			this.btnCargarDer.Name = "btnCargarDer";
			this.btnCargarDer.Size = new System.Drawing.Size(89, 42);
			this.btnCargarDer.TabIndex = 62;
			this.btnCargarDer.Text = "Cargar Imagen";
			this.btnCargarDer.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarDer.UseVisualStyleBackColor = false;
			this.btnCargarDer.Click += new EventHandler(this.btnCargarDer_Click);
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.ForeColor = Color.FromArgb(0, 64, 64);
			this.label6.Location = new Point(24, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(423, 20);
			this.label6.TabIndex = 60;
			this.label6.Text = "Normalizar y colocar estatura (Foto - Perfil derecho)";
			this.pbDer.BackColor = Color.Gainsboro;
			this.pbDer.BorderStyle = BorderStyle.Fixed3D;
			this.pbDer.Location = new Point(28, 42);
			this.pbDer.Margin = new System.Windows.Forms.Padding(6);
			this.pbDer.Name = "pbDer";
			this.pbDer.Size = new System.Drawing.Size(850, 500);
			this.pbDer.SizeMode = PictureBoxSizeMode.Zoom;
			this.pbDer.TabIndex = 61;
			this.pbDer.TabStop = false;
			this.pbDer.Paint += new PaintEventHandler(this.pb_Paint);
			this.pbDer.MouseDown += new MouseEventHandler(this.pb_MouseDown);
			this.pbDer.MouseMove += new MouseEventHandler(this.pb_MouseMove);
			this.pbDer.MouseUp += new MouseEventHandler(this.pb_MouseUp);
			this.errorProvider1.ContainerControl = this;
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.ColumnCount = 2;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel11.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel11.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel11.Location = new Point(32, 29);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(1189, 75);
			this.tableLayoutPanel11.TabIndex = 18;
			this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(1106, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(80, 69);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 55;
			this.pictureBox1.TabStop = false;
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Location = new Point(32, 101);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1189, 25);
			this.tableLayoutPanel1.TabIndex = 19;
			this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1183, 24);
			this.label1.TabIndex = 26;
			this.label1.Text = "INFORMACION FACIAL";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel2.Controls.Add(this.tabCapturaFacial, 0, 0);
			this.tableLayoutPanel2.Location = new Point(32, 106);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1189, 612);
			this.tableLayoutPanel2.TabIndex = 20;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(1248, 732);
			base.ControlBox = false;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.tableLayoutPanel11);
			base.Controls.Add(this.tableLayoutPanel2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.HelpButton = true;
			base.MinimizeBox = false;
			base.Name = "fCapturaFacial";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "fCapturaFacial";
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.fCapturaFacial_Load);
			this.tabPage0.ResumeLayout(false);
			this.tableLayoutPanel12.ResumeLayout(false);
			this.tableLayoutPanel12.PerformLayout();
			((ISupportInitialize)this.numEstatura).EndInit();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			this.tabPage7.PerformLayout();
			this.gbCuerpoEntero.ResumeLayout(false);
			((ISupportInitialize)this.pbResumenCuerpo).EndInit();
			this.gbPerfilIzq.ResumeLayout(false);
			((ISupportInitialize)this.pbResumenIzq).EndInit();
			this.gbFrontal.ResumeLayout(false);
			((ISupportInitialize)this.pbResumenFrontal).EndInit();
			this.gbPerfilDer.ResumeLayout(false);
			((ISupportInitialize)this.pbResumenDer).EndInit();
			this.tabCapturaFacial.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			((ISupportInitialize)this.pbCapturaFrontal).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.panel4.ResumeLayout(false);
			((ISupportInitialize)this.pbImagenRecorte).EndInit();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tableLayoutPanel10.ResumeLayout(false);
			((ISupportInitialize)this.pbFrontalRegla).EndInit();
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((ISupportInitialize)this.pbcheck3).EndInit();
			((ISupportInitialize)this.pbcheck2).EndInit();
			((ISupportInitialize)this.pbcheck1).EndInit();
			((ISupportInitialize)this.pbCuerpo).EndInit();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			((ISupportInitialize)this.pbcheck6).EndInit();
			((ISupportInitialize)this.pbcheck5).EndInit();
			((ISupportInitialize)this.pbcheck4).EndInit();
			((ISupportInitialize)this.pbIzq).EndInit();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			((ISupportInitialize)this.pbcheck9).EndInit();
			((ISupportInitialize)this.pbcheck8).EndInit();
			((ISupportInitialize)this.pbcheck7).EndInit();
			((ISupportInitialize)this.pbDer).EndInit();
			((ISupportInitialize)this.errorProvider1).EndInit();
			this.tableLayoutPanel11.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void label9_Click(object sender, EventArgs e)
		{
		}

		private void numEstatura_ValueChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosGenerales();
		}

		private Bitmap ObtieneImagen(Bitmap bitmap2, FaceData pFaceData)
		{
			Bitmap bitmap1;
			try
			{
				Bitmap bitmap = null;
				if (bitmap2 != null)
				{
					bitmap = bitmap2;
				}
				else if (pFaceData != null)
				{
					this.imgInput = new Image<Bgr, byte>(pFaceData.NormalizedImage);
					bitmap = this.imgInput.Bitmap;
				}
				bitmap1 = bitmap;
			}
			catch (Exception exception)
			{
				bitmap1 = null;
			}
			return bitmap1;
		}

		private Bitmap ObtieneImagen(string imageFilePath, FaceData pFaceData)
		{
			Bitmap bitmap1;
			try
			{
				Bitmap bitmap = null;
				if (this.BitmapRostroFrontalCrop != null)
				{
					bitmap = this.BitmapRostroFrontalCrop;
				}
				else if (pFaceData != null)
				{
					this.imgInput = new Image<Bgr, byte>(pFaceData.NormalizedImage);
					bitmap = this.imgInput.Bitmap;
				}
				bitmap1 = bitmap;
			}
			catch (Exception exception)
			{
				bitmap1 = null;
			}
			return bitmap1;
		}

		private void pb_MouseDown(object sender, MouseEventArgs e)
		{
			Point location;
			FaceData body;
			FaceData faceLeftProfile;
			FaceData faceRightProfile;
			PictureBox pb = (PictureBox)sender;
			string name = pb.Name;
			if (name != null)
			{
				if (name == "pbCuerpo")
				{
					if (!this.btnOriginalCuerpo.Enabled)
					{
						this._selecting = true;
						this.StartLocation = e.Location;
					}
					else
					{
						Bitmap bitmapCuerpoCrop = this.BitmapCuerpoCrop;
						if (this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null)
						{
							body = null;
						}
						else
						{
							body = this.PersonaCapturada.RecordData.Body;
						}
						Bitmap bitmap = this.ObtieneImagen(bitmapCuerpoCrop, body);
						this.BitmapCuerpoCropRegla = (Bitmap)bitmap.Clone();
						this.HallaEscalaSaldo(pb);
						Bitmap bitmapCuerpoCropRegla = this.BitmapCuerpoCropRegla;
						location = e.Location;
						this.DibujaReglaCuerpo(bitmapCuerpoCropRegla, pb, (float)location.Y);
						this.btnsiguiente.Enabled = true;
						this.BitmapCuerpo.Save("Rostro/CuerpoRegla1.bmp");
						this.BitmapCuerpoCrop.Save("Rostro/CuerpoRegla2.bmp");
						this.BitmapCuerpoCropRegla.Save("Rostro/CuerpoRegla3.bmp");
					}
					return;
				}
				else if (name == "pbIzq")
				{
					if (!this.btnOriginalIzq.Enabled)
					{
						this._selecting = true;
						this.StartLocation = e.Location;
					}
					else
					{
						Bitmap bitmapPerfilIzquierdoCrop = this.BitmapPerfilIzquierdoCrop;
						if (this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null)
						{
							faceLeftProfile = null;
						}
						else
						{
							faceLeftProfile = this.PersonaCapturada.RecordData.FaceLeftProfile;
						}
						Bitmap bitmap = this.ObtieneImagen(bitmapPerfilIzquierdoCrop, faceLeftProfile);
						this.BitmapPerfilIzquierdoCropRegla = (Bitmap)bitmap.Clone();
						this.HallaEscalaSaldo(pb);
						Bitmap bitmapPerfilIzquierdoCropRegla = this.BitmapPerfilIzquierdoCropRegla;
						location = e.Location;
						this.DibujaRegla(bitmapPerfilIzquierdoCropRegla, pb, (float)location.Y);
						this.btnsiguiente.Enabled = true;
						this.BitmapPerfilIzquierdo.Save("Rostro/BitmapPerfilIzquierdo1.bmp");
						this.BitmapPerfilIzquierdoCrop.Save("Rostro/BitmapPerfilIzquierdo2.bmp");
						this.BitmapPerfilIzquierdoCropRegla.Save("Rostro/BitmapPerfilIzquierdo3.bmp");
					}
					return;
				}
				else
				{
					if (name != "pbDer")
					{
						this._selecting = true;
						this.StartLocation = e.Location;
						return;
					}
					if (!this.btnOriginalDer.Enabled)
					{
						this._selecting = true;
						this.StartLocation = e.Location;
					}
					else
					{
						Bitmap bitmapPerfilDerechoCrop = this.BitmapPerfilDerechoCrop;
						if (this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null)
						{
							faceRightProfile = null;
						}
						else
						{
							faceRightProfile = this.PersonaCapturada.RecordData.FaceRightProfile;
						}
						Bitmap bitmap = this.ObtieneImagen(bitmapPerfilDerechoCrop, faceRightProfile);
						this.BitmapPerfilDerechoCropRegla = (Bitmap)bitmap.Clone();
						this.HallaEscalaSaldo(pb);
						Bitmap bitmapPerfilDerechoCropRegla = this.BitmapPerfilDerechoCropRegla;
						location = e.Location;
						this.DibujaReglaDer(bitmapPerfilDerechoCropRegla, pb, (float)location.Y);
						this.btnsiguiente.Enabled = true;
						this.BitmapPerfilDerecho.Save("Rostro/BitmapPerfilDerecho1.bmp");
						this.BitmapPerfilDerechoCrop.Save("Rostro/BitmapPerfilDerecho2.bmp");
						this.BitmapPerfilDerechoCropRegla.Save("Rostro/BitmapPerfilDerecho3.bmp");
					}
					return;
				}
			}
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
						string name = vPictureBox.Name;
						if (name != null)
						{
							if (name == "pbCuerpo")
							{
								this.btnOriginalCuerpo.Enabled = true;
								this.BitmapCuerpoCrop = this.temp.Bitmap;
								this.pbcheck1.Visible = true;
								this.pbcheck2.Visible = true;
								this.pbcheck3.Visible = false;
							}
							else if (name == "pbIzq")
							{
								this.btnOriginalIzq.Enabled = true;
								this.BitmapPerfilIzquierdoCrop = this.temp.Bitmap;
								this.pbcheck4.Visible = true;
								this.pbcheck5.Visible = true;
								this.pbcheck6.Visible = false;
							}
							else if (name == "pbDer")
							{
								this.btnOriginalDer.Enabled = true;
								this.BitmapPerfilDerechoCrop = this.temp.Bitmap;
								this.pbcheck7.Visible = true;
								this.pbcheck8.Visible = true;
								this.pbcheck9.Visible = false;
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

		private void pb_Paint(object sender, PaintEventArgs e)
		{
			if (this._selecting)
			{
				e.Graphics.DrawRectangle(Pens.Red, this.GetRectangle());
			}
		}

		private void pbFrontalRegla_MouseDown(object sender, MouseEventArgs e)
		{
			FaceData faceFrontal;
			try
			{
				PictureBox pb = (PictureBox)sender;
				Bitmap bitmapRostroFrontalCrop = this.BitmapRostroFrontalCrop;
				if (this.PersonaCapturada == null || this.PersonaCapturada.RecordData == null)
				{
					faceFrontal = null;
				}
				else
				{
					faceFrontal = this.PersonaCapturada.RecordData.FaceFrontal;
				}
				Bitmap bitmap = this.ObtieneImagen(bitmapRostroFrontalCrop, faceFrontal);
				this.BitmapRostroFrontalCropReglaAux = (Bitmap)bitmap.Clone();
				this.HallaEscalaSaldo(pb);
				this.DibujaRegla(this.BitmapRostroFrontalCropReglaAux, pb, (float)e.Location.Y);
				this.btnsiguiente.Enabled = true;
			}
			catch (Exception exception)
			{
			}
		}

		private bool ValidaModeloDatosGenerales()
		{
			bool enabled;
			try
			{
				DatosGenerales vRegistroPersona = new DatosGenerales()
				{
					numEstatura = this.numEstatura.Value
				};
				this.btnsiguiente.Enabled = Validador.ValidarCampos(vRegistroPersona, this.errorProvider1, this);
				enabled = this.btnsiguiente.Enabled;
			}
			catch (Exception exception)
			{
				enabled = false;
			}
			return enabled;
		}
	}
}