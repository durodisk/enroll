using AutoScan;
using BIODV.Properties;
using BIODV.Util;
using Datys.Enroll.Core;
using Datys.SIP.Common.Biometric;
using Emgu.CV;
using Emgu.CV.Structure;
using SampleSegmentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BIODV
{
	public class fCropB : Form
	{
		private Serializer ser = new Serializer();

		private string SeleccionAnverso = "";

		private string QuitarAnverso = "";

		private Rectangle rect;

		private Point StartLocation;

		private Point EndLcation;

		private bool IsMouseDown3 = false;

		private string MenuAnverso1 = "Datos Biograficos";

		private string MenuAnverso2 = "Dactilares Izquierda";

		private string MenuAnverso3 = "Dactilares Derecha";

		private string MenuAnverso4 = "Simultaneas Izquierda";

		private string MenuAnverso5 = "Simultaneas Derecha";

		private string MenuAnverso6 = "Pulgares";

		private string MenuReverso1 = "Palma Izquierda";

		private string MenuReverso2 = "Palma Derecha";

		private double escalaX;

		private double escalaY;

		private Image<Bgr, byte> imgInput;

		private Image<Bgr, byte> imgInputDB;

		private Image<Bgr, byte> imgInputRI;

		private Image<Bgr, byte> imgInputRD;

		private Image<Bgr, byte> imgInputSI;

		private Image<Bgr, byte> imgInputSD;

		private Image<Bgr, byte> imgInputPU;

		private Image<Bgr, byte> imgInputPI;

		private Image<Bgr, byte> imgInputPD;

		private string SeleccionReverso = "";

		private string QuitarReverso = "";

		private double escalaXR;

		private double escalaYR;

		private Image<Bgr, byte> imgInputR;

		private Rectangle rectR;

		private Point StartLocationR;

		private Point EndLcationR;

		private bool IsMouseDown3R = false;

		private int segmentar = 0;

		private Image<Bgr, byte> imgInputDI;

		private Image<Bgr, byte> imgInputDD;

		private double escalaXDI;

		private double escalaYDI;

		private double escalaXDD;

		private double escalaYDD;

		private Rectangle rectDI;

		private Point StartLocationDI;

		private Point EndLcationDI;

		private bool IsMouseDownDI = false;

		private string SeleccionDI = "";

		private Dictionary<string, string> respuesta = new Dictionary<string, string>();

		private Rectangle rectDD;

		private Point StartLocationDD;

		private Point EndLcationDD;

		private bool IsMouseDownDD = false;

		private string SeleccionDD = "";

		private string QuitarRolado = "";

		private IContainer components = null;

		private Panel panel100;

		private TableLayoutPanel tableLayoutPanel2;

		private TableLayoutPanel tableLayoutPanel11;

		private Label label10;

		private TableLayoutPanel tableLayoutPanel1;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TableLayoutPanel tableLayoutPanel4;

		private Panel panel14;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private Label label1;

		private TabPage tabPage2;

		private TableLayoutPanel tableLayoutPanel6;

		private Panel panel15;

		private Label label8;

		private Label label7;

		private TabPage tabPage3;

		private TableLayoutPanel tableLayoutPanel3;

		private PictureBox pictureBox1;

		private PictureBox pbanverso;

		private Button btnCargarA;

		private Button btnscaner;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem DatosBiograficosToolStripMenuItem;

		private ToolStripMenuItem RodadasIzquierdaToolStripMenuItem;

		private ToolStripMenuItem RodadasDerechaToolStripMenuItem;

		private ToolStripMenuItem palmaIzquierdaToolStripMenuItem;

		private ToolStripMenuItem palmaDerechaToolStripMenuItem;

		private ToolStripMenuItem pulgaresToolStripMenuItem;

		private PictureBox pbbiografia;

		private PictureBox pbDactilarDer;

		private PictureBox pbDactilarIzq;

		private PictureBox pbSimultaneaIzq;

		private PictureBox pbPulgares;

		private PictureBox pbSimultaneaDer;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem quitarToolStripMenuItem;

		private PictureBox pbpalmaizquierda;

		private PictureBox pbpalmaderecha;

		private PictureBox pbreverso;

		private Button btnescan2;

		private Button btnCargarR;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;

		private ToolStripMenuItem QuitarcontextMenuStrip4;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;

		private ToolStripMenuItem palmaIzquierdaToolStripMenuItem1;

		private ToolStripMenuItem palmaDerechaToolStripMenuItem1;

		private Button btnAnterior;

		private Button btnSiguiente;

		private Button btnguardar;

		private Button btncancelar;

		private PictureBox pbDactilarIzquierdo;

		private PictureBox pbDactilarDerecho;

		private TableLayoutPanel tableLayoutPanel5;

		private Panel panel200;

		private PictureBox pictureBox4;

		private Panel panel11;

		private PictureBox pictureBoxD11;

		private Panel panel12;

		private PictureBox pictureBoxD12;

		private Panel panel5;

		private PictureBox pictureBoxD5;

		private Panel panel4;

		private PictureBox pictureBoxD4;

		private Panel panel3;

		private PictureBox pictureBoxD3;

		private Panel panel2;

		private PictureBox pictureBoxD2;

		private Panel panel1;

		private PictureBox pictureBoxD1;

		private Panel panel10;

		private PictureBox pictureBoxD10;

		private Panel panel9;

		private PictureBox pictureBoxD9;

		private Panel panel8;

		private PictureBox pictureBoxD8;

		private Panel panel7;

		private PictureBox pictureBoxD7;

		private Panel panel6;

		private PictureBox pictureBoxD6;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip5;

		private ToolStripMenuItem amputadoToolStripMenuItem;

		private ToolStripMenuItem descartadoToolStripMenuItem;

		private Button button1;

		public PalmForm FormularioPalma
		{
			get;
			set;
		}

		public fCropB()
		{
			this.InitializeComponent();
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

		private void amputadoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataImageCore dataImageCore;
			try
			{
				string quitarRolado = this.QuitarRolado;
				if (quitarRolado != null)
				{
					switch (quitarRolado)
					{
						case "D1":
						{
							this.pictureBoxD1.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(1);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right Thumb").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D2":
						{
							this.pictureBoxD2.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(2);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right IndexFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D3":
						{
							this.pictureBoxD3.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(3);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right MiddleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D4":
						{
							this.pictureBoxD4.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(4);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right RingFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D5":
						{
							this.pictureBoxD5.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(5);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right LittleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D6":
						{
							this.pictureBoxD6.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(6);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left Thumb").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D7":
						{
							this.pictureBoxD7.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(7);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left IndexFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D8":
						{
							this.pictureBoxD8.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(8);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left MiddleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D9":
						{
							this.pictureBoxD9.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(9);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left RingFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "D10":
						{
							this.pictureBoxD10.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(10);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left LittleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "P21":
						{
							this.pbpalmaderecha.Image = Resources.amputado;
							this.pictureBoxD12.Image = Resources.amputado;
							this.FormularioPalma.AmputeePalms.Add(21);
							DataImageCore?[] palmsPrints = this.FormularioPalma.PalmsPrints;
							dataImageCore = new DataImageCore()
							{
								Bir = new BIR()
								{
									BDB = this.ser.ImageToByteArray(Resources.amputado),
									BDBInfo = new BIRBDBInfo()
									{
										Subtype = "PalmsPrintsRight"
									}
								},
								RegionType = RegionType.Segmentado
							};
							palmsPrints[0] = new DataImageCore?(dataImageCore);
							this.pictureBoxD1.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(1);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right Thumb").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD2.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(2);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right IndexFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD3.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(3);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right MiddleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD4.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(4);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right RingFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD5.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(5);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right LittleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
						case "P23":
						{
							this.pbpalmaizquierda.Image = Resources.amputado;
							this.pictureBoxD11.Image = Resources.amputado;
							this.FormularioPalma.AmputeePalms.Add(23);
							DataImageCore?[] nullable = this.FormularioPalma.PalmsPrints;
							dataImageCore = new DataImageCore()
							{
								Bir = new BIR()
								{
									BDB = this.ser.ImageToByteArray(Resources.amputado),
									BDBInfo = new BIRBDBInfo()
									{
										Subtype = "PalmsPrintsLeft"
									}
								},
								RegionType = RegionType.Segmentado
							};
							nullable[1] = new DataImageCore?(dataImageCore);
							this.pictureBoxD6.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(6);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left Thumb").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD7.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(7);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left IndexFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD8.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(8);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left MiddleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD9.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(9);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left RingFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							this.pictureBoxD10.Image = Resources.amputado;
							this.FormularioPalma.AmputeeRolledFingers.Add(10);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left LittleFinger").BDB = this.ser.ImageToByteArray(Resources.amputado);
							break;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAnterior_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				this.btnSiguiente.Text = "Siguiente";
				int selectedIndex = this.tabControl1.SelectedIndex;
				if (selectedIndex == 1)
				{
					this.btnAnterior.Enabled = false;
				}
				else if (selectedIndex == 2)
				{
					this.btnSiguiente.Text = "Segmentar";
				}
				this.tabControl1.SelectTab(this.tabControl1.SelectedIndex - 1);
				this.btnSiguiente.Enabled = true;
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch
			{
			}
		}

		private void btncancelar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Alerta", "Se guardaran los datos y se saldra del formulario,\nEsta seguro de realizar esta operacion?", true))
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				fEnrolar.PersonaCapturada.PalmForm = this.FormularioPalma;
				(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				(new fEnrolar()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void btnCargarA_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.pbanverso.Image = null;
					this.imgInput = new Image<Bgr, byte>(ofd.FileName);
					this.pbanverso.Image = this.imgInput.Bitmap;
					this.pbanverso.Enabled = true;
					try
					{
						this.FormularioPalma.Form = this.ser.ImageToByteArray(this.imgInput.Bitmap);
						this.FormularioPalma.ListModels = null;
						this.FormularioPalma.RolledPrints = new DataImageCore?[2];
						this.FormularioPalma.SimultaneousPrints = new DataImageCore?[3];
					}
					catch
					{
					}
					this.segmentar = 1;
					this.pbbiografia.Image = null;
					this.pbDactilarDer.Image = null;
					this.pbDactilarIzq.Image = null;
					this.pbSimultaneaDer.Image = null;
					this.pbSimultaneaIzq.Image = null;
					this.pbPulgares.Image = null;
					this.pbDactilarIzquierdo.Image = null;
					this.pbDactilarDerecho.Image = null;
					this.pictureBoxD1.Image = null;
					this.pictureBoxD2.Image = null;
					this.pictureBoxD3.Image = null;
					this.pictureBoxD4.Image = null;
					this.pictureBoxD5.Image = null;
					this.pictureBoxD6.Image = null;
					this.pictureBoxD7.Image = null;
					this.pictureBoxD8.Image = null;
					this.pictureBoxD9.Image = null;
					this.pictureBoxD10.Image = null;
					this.pbanverso.Enabled = true;
					this.pbbiografia.Enabled = true;
					this.pbDactilarDer.Enabled = true;
					this.pbDactilarIzq.Enabled = true;
					this.pbSimultaneaDer.Enabled = true;
					this.pbSimultaneaIzq.Enabled = true;
					this.pbPulgares.Enabled = true;
				}
				if (this.imgInput != null)
				{
					double width = (double)this.imgInput.Width;
					System.Drawing.Size size = this.pbanverso.Size;
					this.escalaX = width / (double)size.Width;
					double height = (double)this.imgInput.Height;
					size = this.pbanverso.Size;
					this.escalaY = height / (double)size.Height;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void btnCargarR_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.pbreverso.Image = null;
					this.imgInputR = new Image<Bgr, byte>(ofd.FileName);
					this.pbreverso.Image = this.imgInputR.Bitmap;
					this.pbreverso.Enabled = true;
					this.FormularioPalma.BackPage = this.ser.ImageToByteArray(this.imgInputR.Bitmap);
					this.FormularioPalma.PalmsPrints = new DataImageCore?[2];
					this.pbpalmaizquierda.Image = null;
					this.pbpalmaderecha.Image = null;
					this.pictureBoxD11.Image = null;
					this.pictureBoxD12.Image = null;
					this.pbpalmaizquierda.Enabled = true;
					this.pbpalmaderecha.Enabled = true;
				}
				if (this.imgInputR != null)
				{
					double width = (double)this.imgInputR.Width;
					System.Drawing.Size size = this.pbreverso.Size;
					this.escalaXR = width / (double)size.Width;
					double height = (double)this.imgInputR.Height;
					size = this.pbreverso.Size;
					this.escalaYR = height / (double)size.Height;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void btnescan2_Click(object sender, EventArgs e)
		{
			try
			{
				frmAutoScan.imageOut = null;
				if ((new frmAutoScan()).ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
				{
				}
				if (frmAutoScan.imageOut != null)
				{
					this.imgInputR = new Image<Bgr, byte>((Bitmap)frmAutoScan.imageOut);
					this.pbreverso.Image = this.imgInputR.Bitmap;
					this.pbreverso.Enabled = true;
					this.FormularioPalma.BackPage = this.ser.ImageToByteArray(this.imgInputR.Bitmap);
					this.FormularioPalma.PalmsPrints = new DataImageCore?[2];
					this.pbpalmaizquierda.Image = null;
					this.pbpalmaderecha.Image = null;
					this.pictureBoxD11.Image = null;
					this.pictureBoxD12.Image = null;
					this.pbpalmaizquierda.Enabled = true;
					this.pbpalmaderecha.Enabled = true;
				}
				if (this.imgInputR != null)
				{
					double width = (double)this.imgInputR.Width;
					System.Drawing.Size size = this.pbreverso.Size;
					this.escalaXR = width / (double)size.Width;
					double height = (double)this.imgInputR.Height;
					size = this.pbreverso.Size;
					this.escalaYR = height / (double)size.Height;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void btnguardar_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				fEnrolar.PersonaCapturada.PalmForm = this.FormularioPalma;
				(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
				this.Alerta("Mensaje", "Se guardaron los datos satisfactoriamente", false);
			}
			catch
			{
			}
		}

		private void btnscaner_Click(object sender, EventArgs e)
		{
			frmAutoScan.imageOut = null;
			frmAutoScan form = new frmAutoScan();
			if (form.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
			{
			}
			if (frmAutoScan.imageOut != null)
			{
				this.imgInput = new Image<Bgr, byte>(new Bitmap(frmAutoScan.imageOut));
				this.pbanverso.Image = this.imgInput.Bitmap;
				this.pbanverso.Enabled = true;
				try
				{
					this.FormularioPalma.Form = this.ser.ImageToByteArray(this.imgInput.Bitmap);
					this.FormularioPalma.ListModels = null;
					this.FormularioPalma.RolledPrints = new DataImageCore?[2];
					this.FormularioPalma.SimultaneousPrints = new DataImageCore?[3];
				}
				catch
				{
				}
				this.segmentar = 1;
				this.pbbiografia.Image = null;
				this.pbDactilarDer.Image = null;
				this.pbDactilarIzq.Image = null;
				this.pbSimultaneaDer.Image = null;
				this.pbSimultaneaIzq.Image = null;
				this.pbPulgares.Image = null;
				this.pbDactilarIzquierdo.Image = null;
				this.pbDactilarDerecho.Image = null;
				this.pictureBoxD1.Image = null;
				this.pictureBoxD2.Image = null;
				this.pictureBoxD3.Image = null;
				this.pictureBoxD4.Image = null;
				this.pictureBoxD5.Image = null;
				this.pictureBoxD6.Image = null;
				this.pictureBoxD7.Image = null;
				this.pictureBoxD8.Image = null;
				this.pictureBoxD9.Image = null;
				this.pictureBoxD10.Image = null;
				this.pbanverso.Enabled = true;
				this.pbbiografia.Enabled = true;
				this.pbDactilarDer.Enabled = true;
				this.pbDactilarIzq.Enabled = true;
				this.pbSimultaneaDer.Enabled = true;
				this.pbSimultaneaIzq.Enabled = true;
				this.pbPulgares.Enabled = true;
			}
			if (this.imgInput != null)
			{
				double width = (double)this.imgInput.Width;
				System.Drawing.Size size = this.pbanverso.Size;
				this.escalaX = width / (double)size.Width;
				double height = (double)this.imgInput.Height;
				size = this.pbanverso.Size;
				this.escalaY = height / (double)size.Height;
			}
			form.Dispose();
		}

		private void btnSiguiente_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				switch (this.tabControl1.SelectedIndex)
				{
					case 0:
					{
						if (this.validarAnverso())
						{
							if (!this.validarAnverso())
							{
								MessageBox.Show("Debe completar el recorte del formulario Anverso.");
							}
							else
							{
								this.btnAnterior.Enabled = true;
								this.btnSiguiente.Text = "Segmentar";
								this.btnSiguiente.Enabled = this.validarReverso();
								fEnrolar.PersonaCapturada.PalmForm = this.FormularioPalma;
								(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
								this.tabControl1.SelectedTab = this.tabPage2;
							}
						}
						break;
					}
					case 1:
					{
						fEnrolar.PersonaCapturada.PalmForm = this.FormularioPalma;
						(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
						this.btnAnterior.Enabled = true;
						this.SegmentarAnversoReverso();
						this.Calidad((Bitmap)this.pictureBoxD1.Image, this.panel1);
						this.Calidad((Bitmap)this.pictureBoxD2.Image, this.panel2);
						this.Calidad((Bitmap)this.pictureBoxD3.Image, this.panel3);
						this.Calidad((Bitmap)this.pictureBoxD4.Image, this.panel4);
						this.Calidad((Bitmap)this.pictureBoxD5.Image, this.panel5);
						this.Calidad((Bitmap)this.pictureBoxD6.Image, this.panel6);
						this.Calidad((Bitmap)this.pictureBoxD7.Image, this.panel7);
						this.Calidad((Bitmap)this.pictureBoxD8.Image, this.panel8);
						this.Calidad((Bitmap)this.pictureBoxD9.Image, this.panel9);
						this.Calidad((Bitmap)this.pictureBoxD10.Image, this.panel10);
						this.btnSiguiente.Text = "Finalizar";
						this.tabControl1.SelectedTab = this.tabPage3;
						break;
					}
					case 2:
					{
						this.btnguardar_Click(this, new EventArgs());
						fEnrolar.PersonaCapturada.PalmForm = this.FormularioPalma;
						(new Serializer()).SerializeEpd(fEnrolar.PersonaCapturada, fPrincipal.RutaEpd);
						this.btnSiguiente.Text = "Finalizar";
						(new fEnrolar()
						{
							MdiParent = base.ParentForm
						}).Show();
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

		private void Calidad(Bitmap pDedoCrop, Panel pPanel)
		{
			SegmentacionRod form = new SegmentacionRod();
			DataTable dtResultado = new DataTable();
			this.respuesta = form.enviarUno(pDedoCrop, ref dtResultado);
			DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
			if (results == null)
			{
				this.panelcolor(pPanel, 0);
			}
			else
			{
				this.panelcolor(pPanel, Convert.ToInt32(results.Field<string>("Q")));
			}
		}

		private void CargaImagenes()
		{
			System.Drawing.Size size;
			Image image;
			fEnrolar.PersonaCapturada.BasicData.UploadStatus.ToString();
			if (this.FormularioPalma != null)
			{
				if (this.FormularioPalma.Form != null)
				{
					this.pbanverso.Image = this.ser.byteArrayToImage(this.FormularioPalma.Form);
					this.imgInput = new Image<Bgr, byte>(new Bitmap(this.pbanverso.Image));
					this.pbanverso.Enabled = true;
					this.pbbiografia.Enabled = true;
					this.pbDactilarDer.Enabled = true;
					this.pbDactilarIzq.Enabled = true;
					this.pbSimultaneaDer.Enabled = true;
					this.pbSimultaneaIzq.Enabled = true;
					this.pbPulgares.Enabled = true;
					double width = (double)this.imgInput.Width;
					size = this.pbanverso.Size;
					this.escalaX = width / (double)size.Width;
					double height = (double)this.imgInput.Height;
					size = this.pbanverso.Size;
					this.escalaY = height / (double)size.Height;
				}
				if (this.FormularioPalma.BackPage != null)
				{
					this.pbreverso.Image = this.ser.byteArrayToImage(this.FormularioPalma.BackPage);
					this.imgInputR = new Image<Bgr, byte>(new Bitmap(this.pbreverso.Image));
					this.pbreverso.Enabled = true;
					this.pbpalmaizquierda.Enabled = true;
					this.pbpalmaderecha.Enabled = true;
					double num = (double)this.imgInputR.Width;
					size = this.pbreverso.Size;
					this.escalaXR = num / (double)size.Width;
					double height1 = (double)this.imgInputR.Height;
					size = this.pbreverso.Size;
					this.escalaYR = height1 / (double)size.Height;
				}
				if (this.FormularioPalma.ListModels != null)
				{
					this.pbbiografia.Image = this.ser.byteArrayToImage(this.FormularioPalma.ListModels);
				}
				if (this.FormularioPalma.RolledPrints.Any<DataImageCore?>())
				{
					for (int i = 0; i < this.FormularioPalma.RolledPrints.Count<DataImageCore?>(); i++)
					{
						if (this.FormularioPalma.RolledPrints[i].HasValue)
						{
							DataImageCore VDataImageCore = this.FormularioPalma.RolledPrints[i].Value;
							string subtype = VDataImageCore.Bir.BDBInfo.Subtype;
							if (subtype != null)
							{
								if (subtype == "RolledPrintsRight")
								{
									PictureBox pictureBox = this.pbDactilarDerecho;
									PictureBox pictureBox1 = this.pbDactilarDer;
									Image image1 = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
									image = image1;
									pictureBox1.Image = image1;
									pictureBox.Image = image;
									if (this.pbDactilarDerecho.Image != null)
									{
										this.imgInputRD = new Image<Bgr, byte>(new Bitmap(this.pbDactilarDerecho.Image));
										double width1 = (double)this.imgInputRD.Width;
										size = this.pbDactilarDerecho.Size;
										this.escalaXDD = width1 / (double)size.Width;
										double num1 = (double)this.imgInputRD.Height;
										size = this.pbDactilarDerecho.Size;
										this.escalaYDD = num1 / (double)size.Height;
									}
								}
								else if (subtype == "RolledPrintsLeft")
								{
									PictureBox pictureBox2 = this.pbDactilarIzquierdo;
									PictureBox pictureBox3 = this.pbDactilarIzq;
									Image image2 = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
									image = image2;
									pictureBox3.Image = image2;
									pictureBox2.Image = image;
									if (this.pbDactilarIzquierdo.Image != null)
									{
										this.imgInputRI = new Image<Bgr, byte>(new Bitmap(this.pbDactilarIzquierdo.Image));
										double width2 = (double)this.imgInputRI.Width;
										size = this.pbDactilarIzquierdo.Size;
										this.escalaXDI = width2 / (double)size.Width;
										double height2 = (double)this.imgInputRI.Height;
										size = this.pbDactilarIzquierdo.Size;
										this.escalaYDI = height2 / (double)size.Height;
									}
								}
							}
						}
					}
				}
				if (this.FormularioPalma.SimultaneousPrints.Any<DataImageCore?>())
				{
					for (int i = 0; i < this.FormularioPalma.SimultaneousPrints.Count<DataImageCore?>(); i++)
					{
						if (this.FormularioPalma.SimultaneousPrints[i].HasValue)
						{
							DataImageCore VDataImageCore = this.FormularioPalma.SimultaneousPrints[i].Value;
							string str = VDataImageCore.Bir.BDBInfo.Subtype;
							if (str != null)
							{
								if (str == "SimultaneousPrintsLeft")
								{
									this.pbSimultaneaIzq.Image = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
								}
								else if (str == "SimultaneousPrintsThumb")
								{
									this.pbPulgares.Image = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
								}
								else if (str == "SimultaneousPrintsRight")
								{
									this.pbSimultaneaDer.Image = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
								}
							}
						}
					}
				}
				if (this.FormularioPalma.PalmsPrints.Any<DataImageCore?>())
				{
					for (int i = 0; i < this.FormularioPalma.PalmsPrints.Count<DataImageCore?>(); i++)
					{
						if (this.FormularioPalma.PalmsPrints[i].HasValue)
						{
							DataImageCore VDataImageCore = this.FormularioPalma.PalmsPrints[i].Value;
							string subtype1 = VDataImageCore.Bir.BDBInfo.Subtype;
							if (subtype1 != null)
							{
								if (subtype1 == "PalmsPrintsRight")
								{
									PictureBox pictureBox4 = this.pictureBoxD12;
									PictureBox pictureBox5 = this.pbpalmaderecha;
									Image image3 = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
									image = image3;
									pictureBox5.Image = image3;
									pictureBox4.Image = image;
								}
								else if (subtype1 == "PalmsPrintsLeft")
								{
									PictureBox pictureBox6 = this.pictureBoxD11;
									PictureBox pictureBox7 = this.pbpalmaizquierda;
									Image image4 = this.ser.byteArrayToImage(VDataImageCore.Bir.BDB);
									image = image4;
									pictureBox7.Image = image4;
									pictureBox6.Image = image;
								}
							}
						}
					}
				}
				if (this.FormularioPalma.RolledPrintsBIR.Any<BIR>())
				{
					foreach (BIR vBir in this.FormularioPalma.RolledPrintsBIR)
					{
						if (vBir.BDB != null)
						{
							string str1 = vBir.BDBInfo.Subtype;
							if (str1 != null)
							{
								switch (str1)
								{
									case "Right Thumb":
									{
										this.pictureBoxD1.Image = this.ser.byteArrayToImage(vBir.BDB);
										this.segmentar = 0;
										break;
									}
									case "Right IndexFinger":
									{
										Bitmap dedo2 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD2.Image = dedo2;
										this.segmentar = 0;
										break;
									}
									case "Right MiddleFinger":
									{
										Bitmap dedo3 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD3.Image = dedo3;
										this.segmentar = 0;
										break;
									}
									case "Right RingFinger":
									{
										Bitmap dedo4 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD4.Image = dedo4;
										this.segmentar = 0;
										break;
									}
									case "Right LittleFinger":
									{
										Bitmap dedo5 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD5.Image = dedo5;
										this.segmentar = 0;
										break;
									}
									case "Left Thumb":
									{
										Bitmap dedo6 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD6.Image = dedo6;
										this.segmentar = 0;
										break;
									}
									case "Left IndexFinger":
									{
										Bitmap dedo7 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD7.Image = dedo7;
										this.segmentar = 0;
										break;
									}
									case "Left MiddleFinger":
									{
										Bitmap dedo8 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD8.Image = dedo8;
										this.segmentar = 0;
										break;
									}
									case "Left RingFinger":
									{
										Bitmap dedo9 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD9.Image = dedo9;
										this.segmentar = 0;
										break;
									}
									case "Left LittleFinger":
									{
										Bitmap dedo10 = (Bitmap)this.ser.byteArrayToImage(vBir.BDB);
										this.pictureBoxD10.Image = dedo10;
										this.segmentar = 0;
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		public Bitmap CropImage(Bitmap source, Rectangle section)
		{
			Bitmap bitmap1;
			try
			{
				Bitmap bitmap = new Bitmap(section.Width, section.Height);
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					try
					{
						g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
						bitmap1 = bitmap;
					}
					catch
					{
						bitmap1 = null;
					}
				}
			}
			catch
			{
				bitmap1 = null;
			}
			return bitmap1;
		}

		private void DatosBiograficosToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void descartadoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataImageCore dataImageCore;
			try
			{
				string quitarRolado = this.QuitarRolado;
				if (quitarRolado != null)
				{
					switch (quitarRolado)
					{
						case "D1":
						{
							this.pictureBoxD1.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(1);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right Thumb").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D2":
						{
							this.pictureBoxD2.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(2);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right IndexFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D3":
						{
							this.pictureBoxD3.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(3);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right MiddleFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D4":
						{
							this.pictureBoxD4.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(4);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right RingFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D5":
						{
							this.pictureBoxD5.Image = Resources.descartado;
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right LittleFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D6":
						{
							this.pictureBoxD6.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(6);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left Thumb").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D7":
						{
							this.pictureBoxD7.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(7);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left IndexFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D8":
						{
							this.pictureBoxD8.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(8);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left MiddleFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D9":
						{
							this.pictureBoxD9.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(9);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left RingFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "D10":
						{
							this.pictureBoxD10.Image = Resources.descartado;
							this.FormularioPalma.DiscardRolledFingers.Add(10);
							this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left LittleFinger").BDB = this.ser.ImageToByteArray(Resources.descartado);
							break;
						}
						case "P21":
						{
							this.pbpalmaderecha.Image = Resources.descartado;
							this.pictureBoxD12.Image = Resources.descartado;
							this.FormularioPalma.AmputeePalms.Add(21);
							DataImageCore?[] palmsPrints = this.FormularioPalma.PalmsPrints;
							dataImageCore = new DataImageCore()
							{
								Bir = new BIR()
								{
									BDB = this.ser.ImageToByteArray(Resources.descartado),
									BDBInfo = new BIRBDBInfo()
									{
										Subtype = "PalmsPrintsRight"
									}
								},
								RegionType = RegionType.Segmentado
							};
							palmsPrints[0] = new DataImageCore?(dataImageCore);
							break;
						}
						case "P23":
						{
							this.pbpalmaizquierda.Image = Resources.descartado;
							this.pictureBoxD11.Image = Resources.descartado;
							this.FormularioPalma.AmputeePalms.Add(23);
							DataImageCore?[] nullable = this.FormularioPalma.PalmsPrints;
							dataImageCore = new DataImageCore()
							{
								Bir = new BIR()
								{
									BDB = this.ser.ImageToByteArray(Resources.descartado),
									BDBInfo = new BIRBDBInfo()
									{
										Subtype = "PalmsPrintsLeft"
									}
								},
								RegionType = RegionType.Segmentado
							};
							nullable[1] = new DataImageCore?(dataImageCore);
							break;
						}
					}
				}
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

		private void fCrop_Load(object sender, EventArgs e)
		{
			DataImageCore dataImageCore;
			try
			{
				this.contextMenuStrip1.Items[0].Text = this.MenuAnverso1;
				this.contextMenuStrip1.Items[1].Text = this.MenuAnverso2;
				this.contextMenuStrip1.Items[2].Text = this.MenuAnverso3;
				this.contextMenuStrip1.Items[3].Text = this.MenuAnverso4;
				this.contextMenuStrip1.Items[4].Text = this.MenuAnverso5;
				this.contextMenuStrip1.Items[5].Text = this.MenuAnverso6;
				this.contextMenuStrip3.Items[0].Text = this.MenuReverso1;
				this.contextMenuStrip3.Items[1].Text = this.MenuReverso2;
				this.pbanverso.Enabled = false;
				this.pbbiografia.Enabled = false;
				this.pbDactilarDer.Enabled = false;
				this.pbDactilarIzq.Enabled = false;
				this.pbSimultaneaDer.Enabled = false;
				this.pbSimultaneaIzq.Enabled = false;
				this.pbPulgares.Enabled = false;
				this.pbreverso.Enabled = false;
				this.pbpalmaizquierda.Enabled = false;
				this.pbpalmaderecha.Enabled = false;
				if (fEnrolar.PersonaCapturada == null)
				{
					fEnrolar.PersonaCapturada = new CapturedPerson();
				}
				if (fEnrolar.PersonaCapturada.PalmForm == null)
				{
					this.FormularioPalma = new PalmForm();
					this.segmentar = 1;
				}
				else
				{
					this.FormularioPalma = fEnrolar.PersonaCapturada.PalmForm;
					if ((this.FormularioPalma.RolledPrints == null ? true : !this.FormularioPalma.RolledPrints.Any<DataImageCore?>()))
					{
						DataImageCore?[] vRoladas = new DataImageCore?[2];
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "RolledPrintsRight"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vRoladas[0] = new DataImageCore?(dataImageCore);
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "RolledPrintsLeft"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vRoladas[1] = new DataImageCore?(dataImageCore);
						this.FormularioPalma.RolledPrints = vRoladas;
					}
					if ((this.FormularioPalma.SimultaneousPrints == null ? true : !this.FormularioPalma.SimultaneousPrints.Any<DataImageCore?>()))
					{
						DataImageCore?[] vSimultaneas = new DataImageCore?[3];
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "SimultaneousPrintsLeft"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vSimultaneas[0] = new DataImageCore?(dataImageCore);
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "SimultaneousPrintsThumb"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vSimultaneas[1] = new DataImageCore?(dataImageCore);
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "SimultaneousPrintsRight"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vSimultaneas[2] = new DataImageCore?(dataImageCore);
						this.FormularioPalma.SimultaneousPrints = vSimultaneas;
					}
					if ((this.FormularioPalma.PalmsPrints == null ? true : !this.FormularioPalma.PalmsPrints.Any<DataImageCore?>()))
					{
						DataImageCore?[] vPalmas = new DataImageCore?[2];
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "PalmsPrintsRight"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vPalmas[0] = new DataImageCore?(dataImageCore);
						dataImageCore = new DataImageCore()
						{
							Bir = new BIR()
							{
								BDB = null,
								BDBInfo = new BIRBDBInfo()
								{
									Subtype = "PalmsPrintsLeft"
								}
							},
							RegionType = RegionType.Segmentado
						};
						vPalmas[1] = new DataImageCore?(dataImageCore);
						this.FormularioPalma.PalmsPrints = vPalmas;
					}
					if ((this.FormularioPalma.RolledPrintsBIR == null ? true : !this.FormularioPalma.RolledPrintsBIR.Any<BIR>()))
					{
						List<BIR> vHuellas = new List<BIR>()
						{
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right Thumb"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right IndexFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right MiddleFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right RingFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right LittleFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left Thumb"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left IndexFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left MiddleFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left RingFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left LittleFinger"
								}
							}
						};
						this.FormularioPalma.RolledPrintsBIR = vHuellas;
					}
					if ((this.FormularioPalma.PalmsPrintsBIR == null ? true : !this.FormularioPalma.PalmsPrintsBIR.Any<BIR>()))
					{
						List<BIR> vPalmas = new List<BIR>()
						{
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right Palm"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left Palm"
								}
							}
						};
						this.FormularioPalma.PalmsPrintsBIR = vPalmas;
					}
					this.CargaImagenes();
					this.btnSiguiente.Enabled = this.validarAnverso();
				}
			}
			catch (Exception exception)
			{
			}
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

		private Rectangle GetRectangleDD()
		{
			this.rectDD = new Rectangle()
			{
				X = Math.Min(this.StartLocationDD.X, this.EndLcationDD.X),
				Y = Math.Min(this.StartLocationDD.Y, this.EndLcationDD.Y),
				Width = Math.Abs(this.StartLocationDD.X - this.EndLcationDD.X),
				Height = Math.Abs(this.StartLocationDD.Y - this.EndLcationDD.Y)
			};
			return this.rectDD;
		}

		private Rectangle GetRectangleDI()
		{
			this.rectDI = new Rectangle()
			{
				X = Math.Min(this.StartLocationDI.X, this.EndLcationDI.X),
				Y = Math.Min(this.StartLocationDI.Y, this.EndLcationDI.Y),
				Width = Math.Abs(this.StartLocationDI.X - this.EndLcationDI.X),
				Height = Math.Abs(this.StartLocationDI.Y - this.EndLcationDI.Y)
			};
			return this.rectDI;
		}

		private Rectangle GetRectangleR()
		{
			this.rectR = new Rectangle()
			{
				X = Math.Min(this.StartLocationR.X, this.EndLcationR.X),
				Y = Math.Min(this.StartLocationR.Y, this.EndLcationR.Y),
				Width = Math.Abs(this.StartLocationR.X - this.EndLcationR.X),
				Height = Math.Abs(this.StartLocationR.Y - this.EndLcationR.Y)
			};
			return this.rectR;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(fCropB));
			this.panel100 = new Panel();
			this.button1 = new Button();
			this.btnAnterior = new Button();
			this.btnSiguiente = new Button();
			this.btnguardar = new Button();
			this.btncancelar = new Button();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.label10 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.panel14 = new Panel();
			this.pbPulgares = new PictureBox();
			this.pbSimultaneaDer = new PictureBox();
			this.pbSimultaneaIzq = new PictureBox();
			this.pbDactilarIzq = new PictureBox();
			this.pbDactilarDer = new PictureBox();
			this.pbbiografia = new PictureBox();
			this.btnscaner = new Button();
			this.btnCargarA = new Button();
			this.pbanverso = new PictureBox();
			this.label6 = new Label();
			this.label5 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.tabPage2 = new TabPage();
			this.tableLayoutPanel6 = new TableLayoutPanel();
			this.panel15 = new Panel();
			this.btnescan2 = new Button();
			this.btnCargarR = new Button();
			this.pbreverso = new PictureBox();
			this.pbpalmaizquierda = new PictureBox();
			this.pbpalmaderecha = new PictureBox();
			this.label8 = new Label();
			this.label7 = new Label();
			this.tabPage3 = new TabPage();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.panel200 = new Panel();
			this.panel5 = new Panel();
			this.pictureBoxD5 = new PictureBox();
			this.panel4 = new Panel();
			this.pictureBoxD4 = new PictureBox();
			this.panel3 = new Panel();
			this.pictureBoxD3 = new PictureBox();
			this.panel2 = new Panel();
			this.pictureBoxD2 = new PictureBox();
			this.panel1 = new Panel();
			this.pictureBoxD1 = new PictureBox();
			this.panel10 = new Panel();
			this.pictureBoxD10 = new PictureBox();
			this.panel9 = new Panel();
			this.pictureBoxD9 = new PictureBox();
			this.panel8 = new Panel();
			this.pictureBoxD8 = new PictureBox();
			this.panel7 = new Panel();
			this.pictureBoxD7 = new PictureBox();
			this.panel6 = new Panel();
			this.pictureBoxD6 = new PictureBox();
			this.panel12 = new Panel();
			this.pictureBoxD12 = new PictureBox();
			this.panel11 = new Panel();
			this.pictureBoxD11 = new PictureBox();
			this.pictureBox4 = new PictureBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.pbDactilarDerecho = new PictureBox();
			this.pbDactilarIzquierdo = new PictureBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DatosBiograficosToolStripMenuItem = new ToolStripMenuItem();
			this.RodadasIzquierdaToolStripMenuItem = new ToolStripMenuItem();
			this.RodadasDerechaToolStripMenuItem = new ToolStripMenuItem();
			this.palmaIzquierdaToolStripMenuItem = new ToolStripMenuItem();
			this.palmaDerechaToolStripMenuItem = new ToolStripMenuItem();
			this.pulgaresToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.quitarToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.QuitarcontextMenuStrip4 = new ToolStripMenuItem();
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.palmaIzquierdaToolStripMenuItem1 = new ToolStripMenuItem();
			this.palmaDerechaToolStripMenuItem1 = new ToolStripMenuItem();
			this.contextMenuStrip5 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.amputadoToolStripMenuItem = new ToolStripMenuItem();
			this.descartadoToolStripMenuItem = new ToolStripMenuItem();
			this.panel100.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel11.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.panel14.SuspendLayout();
			((ISupportInitialize)this.pbPulgares).BeginInit();
			((ISupportInitialize)this.pbSimultaneaDer).BeginInit();
			((ISupportInitialize)this.pbSimultaneaIzq).BeginInit();
			((ISupportInitialize)this.pbDactilarIzq).BeginInit();
			((ISupportInitialize)this.pbDactilarDer).BeginInit();
			((ISupportInitialize)this.pbbiografia).BeginInit();
			((ISupportInitialize)this.pbanverso).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.panel15.SuspendLayout();
			((ISupportInitialize)this.pbreverso).BeginInit();
			((ISupportInitialize)this.pbpalmaizquierda).BeginInit();
			((ISupportInitialize)this.pbpalmaderecha).BeginInit();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.panel200.SuspendLayout();
			this.panel5.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD5).BeginInit();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD4).BeginInit();
			this.panel3.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD3).BeginInit();
			this.panel2.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD2).BeginInit();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD1).BeginInit();
			this.panel10.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD10).BeginInit();
			this.panel9.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD9).BeginInit();
			this.panel8.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD8).BeginInit();
			this.panel7.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD7).BeginInit();
			this.panel6.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD6).BeginInit();
			this.panel12.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD12).BeginInit();
			this.panel11.SuspendLayout();
			((ISupportInitialize)this.pictureBoxD11).BeginInit();
			((ISupportInitialize)this.pictureBox4).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			((ISupportInitialize)this.pbDactilarDerecho).BeginInit();
			((ISupportInitialize)this.pbDactilarIzquierdo).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.contextMenuStrip4.SuspendLayout();
			this.contextMenuStrip3.SuspendLayout();
			this.contextMenuStrip5.SuspendLayout();
			base.SuspendLayout();
			this.panel100.BackColor = Color.FromArgb(48, 63, 105);
			this.panel100.Controls.Add(this.button1);
			this.panel100.Controls.Add(this.btnAnterior);
			this.panel100.Controls.Add(this.btnSiguiente);
			this.panel100.Controls.Add(this.btnguardar);
			this.panel100.Controls.Add(this.btncancelar);
			this.panel100.Location = new Point(3, 3);
			this.panel100.Name = "panel100";
			this.panel100.Size = new System.Drawing.Size(461, 60);
			this.panel100.TabIndex = 29;
			this.button1.BackColor = Color.White;
			this.button1.Image = (Image)resources.GetObject("button1.Image");
			this.button1.ImageAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(346, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 54);
			this.button1.TabIndex = 32;
			this.button1.Text = "Salir";
			this.button1.TextAlign = ContentAlignment.BottomCenter;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btnAnterior.BackColor = Color.White;
			this.btnAnterior.Enabled = false;
			this.btnAnterior.Image = (Image)resources.GetObject("btnAnterior.Image");
			this.btnAnterior.ImageAlign = ContentAlignment.TopCenter;
			this.btnAnterior.Location = new Point(6, 3);
			this.btnAnterior.Name = "btnAnterior";
			this.btnAnterior.Size = new System.Drawing.Size(75, 54);
			this.btnAnterior.TabIndex = 28;
			this.btnAnterior.Text = "Anterior";
			this.btnAnterior.TextAlign = ContentAlignment.BottomCenter;
			this.btnAnterior.UseVisualStyleBackColor = false;
			this.btnAnterior.Click += new EventHandler(this.btnAnterior_Click);
			this.btnSiguiente.BackColor = Color.White;
			this.btnSiguiente.Image = (Image)resources.GetObject("btnSiguiente.Image");
			this.btnSiguiente.ImageAlign = ContentAlignment.TopCenter;
			this.btnSiguiente.Location = new Point(87, 3);
			this.btnSiguiente.Name = "btnSiguiente";
			this.btnSiguiente.Size = new System.Drawing.Size(75, 54);
			this.btnSiguiente.TabIndex = 29;
			this.btnSiguiente.Text = "Siguiente";
			this.btnSiguiente.TextAlign = ContentAlignment.BottomCenter;
			this.btnSiguiente.UseVisualStyleBackColor = false;
			this.btnSiguiente.Click += new EventHandler(this.btnSiguiente_Click);
			this.btnguardar.BackColor = Color.White;
			this.btnguardar.Image = (Image)resources.GetObject("btnguardar.Image");
			this.btnguardar.ImageAlign = ContentAlignment.TopCenter;
			this.btnguardar.Location = new Point(168, 3);
			this.btnguardar.Name = "btnguardar";
			this.btnguardar.Size = new System.Drawing.Size(75, 54);
			this.btnguardar.TabIndex = 30;
			this.btnguardar.Text = "Guardar";
			this.btnguardar.TextAlign = ContentAlignment.BottomCenter;
			this.btnguardar.UseVisualStyleBackColor = false;
			this.btnguardar.Click += new EventHandler(this.btnguardar_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(249, 3);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(91, 54);
			this.btncancelar.TabIndex = 31;
			this.btncancelar.Text = "Guardar y Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.panel100, 0, 0);
			this.tableLayoutPanel2.Location = new Point(29, 28);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1256, 80);
			this.tableLayoutPanel2.TabIndex = 30;
			this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(1173, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(80, 74);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 56;
			this.pictureBox1.TabStop = false;
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.ColumnCount = 1;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel11.Controls.Add(this.label10, 0, 0);
			this.tableLayoutPanel11.Location = new Point(12, 94);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(1297, 25);
			this.tableLayoutPanel11.TabIndex = 31;
			this.label10.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label10.ForeColor = Color.White;
			this.label10.Location = new Point(3, 3);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(1291, 18);
			this.label10.TabIndex = 26;
			this.label10.Text = "SEGMENTACION (TARJETA UNICA DE FILIACION)";
			this.label10.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 94.73684f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
			this.tableLayoutPanel1.Location = new Point(29, 94);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1256, 643);
			this.tableLayoutPanel1.TabIndex = 32;
			this.tabControl1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.HotTrack = true;
			this.tabControl1.ItemSize = new System.Drawing.Size(51, 20);
			this.tabControl1.Location = new Point(0, 3);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1256, 637);
			this.tabControl1.TabIndex = 12;
			this.tabPage1.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage1.Controls.Add(this.tableLayoutPanel4);
			this.tabPage1.Location = new Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1248, 609);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Anverso";
			this.tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel4.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel4.Controls.Add(this.panel14, 1, 0);
			this.tableLayoutPanel4.Location = new Point(6, 6);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(1236, 603);
			this.tableLayoutPanel4.TabIndex = 28;
			this.panel14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.panel14.Controls.Add(this.pbPulgares);
			this.panel14.Controls.Add(this.pbSimultaneaDer);
			this.panel14.Controls.Add(this.pbSimultaneaIzq);
			this.panel14.Controls.Add(this.pbDactilarIzq);
			this.panel14.Controls.Add(this.pbDactilarDer);
			this.panel14.Controls.Add(this.pbbiografia);
			this.panel14.Controls.Add(this.btnscaner);
			this.panel14.Controls.Add(this.btnCargarA);
			this.panel14.Controls.Add(this.pbanverso);
			this.panel14.Controls.Add(this.label6);
			this.panel14.Controls.Add(this.label5);
			this.panel14.Controls.Add(this.label4);
			this.panel14.Controls.Add(this.label3);
			this.panel14.Controls.Add(this.label2);
			this.panel14.Controls.Add(this.label1);
			this.panel14.Location = new Point(126, 3);
			this.panel14.Name = "panel14";
			this.panel14.Size = new System.Drawing.Size(982, 597);
			this.panel14.TabIndex = 0;
			this.pbPulgares.BackColor = Color.White;
			this.pbPulgares.Location = new Point(599, 384);
			this.pbPulgares.Name = "pbPulgares";
			this.pbPulgares.Size = new System.Drawing.Size(134, 165);
			this.pbPulgares.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbPulgares.TabIndex = 34;
			this.pbPulgares.TabStop = false;
			this.pbPulgares.MouseClick += new MouseEventHandler(this.pbPulgares_MouseClick);
			this.pbSimultaneaDer.BackColor = Color.White;
			this.pbSimultaneaDer.Location = new Point(739, 384);
			this.pbSimultaneaDer.Name = "pbSimultaneaDer";
			this.pbSimultaneaDer.Size = new System.Drawing.Size(190, 165);
			this.pbSimultaneaDer.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbSimultaneaDer.TabIndex = 35;
			this.pbSimultaneaDer.TabStop = false;
			this.pbSimultaneaDer.MouseClick += new MouseEventHandler(this.pbPalmaD_MouseClick);
			this.pbSimultaneaIzq.BackColor = Color.White;
			this.pbSimultaneaIzq.Location = new Point(400, 385);
			this.pbSimultaneaIzq.Name = "pbSimultaneaIzq";
			this.pbSimultaneaIzq.Size = new System.Drawing.Size(190, 165);
			this.pbSimultaneaIzq.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbSimultaneaIzq.TabIndex = 33;
			this.pbSimultaneaIzq.TabStop = false;
			this.pbSimultaneaIzq.MouseClick += new MouseEventHandler(this.pbPalmaI_MouseClick);
			this.pbDactilarIzq.BackColor = Color.White;
			this.pbDactilarIzq.Location = new Point(400, 296);
			this.pbDactilarIzq.Name = "pbDactilarIzq";
			this.pbDactilarIzq.Size = new System.Drawing.Size(529, 70);
			this.pbDactilarIzq.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbDactilarIzq.TabIndex = 32;
			this.pbDactilarIzq.TabStop = false;
			this.pbDactilarIzq.MouseClick += new MouseEventHandler(this.pbrodadasI_MouseClick);
			this.pbDactilarDer.BackColor = Color.White;
			this.pbDactilarDer.Location = new Point(400, 208);
			this.pbDactilarDer.Name = "pbDactilarDer";
			this.pbDactilarDer.Size = new System.Drawing.Size(529, 70);
			this.pbDactilarDer.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbDactilarDer.TabIndex = 31;
			this.pbDactilarDer.TabStop = false;
			this.pbDactilarDer.MouseClick += new MouseEventHandler(this.pbrodadasD_MouseClick);
			this.pbbiografia.BackColor = Color.White;
			this.pbbiografia.Location = new Point(400, 22);
			this.pbbiografia.Name = "pbbiografia";
			this.pbbiografia.Size = new System.Drawing.Size(257, 168);
			this.pbbiografia.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbbiografia.TabIndex = 30;
			this.pbbiografia.TabStop = false;
			this.pbbiografia.MouseClick += new MouseEventHandler(this.pbbiografia_MouseClick);
			this.btnscaner.BackColor = Color.White;
			this.btnscaner.Image = (Image)resources.GetObject("btnscaner.Image");
			this.btnscaner.ImageAlign = ContentAlignment.TopCenter;
			this.btnscaner.Location = new Point(98, 552);
			this.btnscaner.Name = "btnscaner";
			this.btnscaner.Size = new System.Drawing.Size(89, 42);
			this.btnscaner.TabIndex = 29;
			this.btnscaner.Text = "Escanear";
			this.btnscaner.TextAlign = ContentAlignment.BottomCenter;
			this.btnscaner.UseVisualStyleBackColor = false;
			this.btnscaner.Click += new EventHandler(this.btnscaner_Click);
			this.btnCargarA.BackColor = Color.White;
			this.btnCargarA.Image = (Image)resources.GetObject("btnCargarA.Image");
			this.btnCargarA.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarA.Location = new Point(3, 552);
			this.btnCargarA.Name = "btnCargarA";
			this.btnCargarA.Size = new System.Drawing.Size(89, 42);
			this.btnCargarA.TabIndex = 28;
			this.btnCargarA.Text = "Cargar Imagen";
			this.btnCargarA.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarA.UseVisualStyleBackColor = false;
			this.btnCargarA.Click += new EventHandler(this.btnCargarA_Click);
			this.pbanverso.BackColor = Color.White;
			this.pbanverso.BorderStyle = BorderStyle.Fixed3D;
			this.pbanverso.Cursor = Cursors.Cross;
			this.pbanverso.Location = new Point(0, 6);
			this.pbanverso.Margin = new System.Windows.Forms.Padding(6);
			this.pbanverso.Name = "pbanverso";
			this.pbanverso.Size = new System.Drawing.Size(350, 543);
			this.pbanverso.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbanverso.TabIndex = 27;
			this.pbanverso.TabStop = false;
			this.pbanverso.Paint += new PaintEventHandler(this.pbanverso_Paint);
			this.pbanverso.MouseClick += new MouseEventHandler(this.pbanverso_MouseClick);
			this.pbanverso.MouseDown += new MouseEventHandler(this.pbanverso_MouseDown);
			this.pbanverso.MouseMove += new MouseEventHandler(this.pbanverso_MouseMove);
			this.pbanverso.MouseUp += new MouseEventHandler(this.pbanverso_MouseUp);
			this.label6.AutoSize = true;
			this.label6.ForeColor = Color.White;
			this.label6.Location = new Point(596, 369);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(108, 13);
			this.label6.TabIndex = 26;
			this.label6.Text = "Simultaneos Pulgares";
			this.label5.AutoSize = true;
			this.label5.ForeColor = Color.White;
			this.label5.Location = new Point(736, 369);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(108, 13);
			this.label5.TabIndex = 25;
			this.label5.Text = "Simultaneos Derecha";
			this.label4.AutoSize = true;
			this.label4.ForeColor = Color.White;
			this.label4.Location = new Point(397, 369);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110, 13);
			this.label4.TabIndex = 24;
			this.label4.Text = "Simultaneos Izquierda";
			this.label3.AutoSize = true;
			this.label3.ForeColor = Color.White;
			this.label3.Location = new Point(397, 193);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Dactilares derechos";
			this.label2.AutoSize = true;
			this.label2.ForeColor = Color.White;
			this.label2.Location = new Point(397, 281);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Dactilares Izquierdos";
			this.label1.AutoSize = true;
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(397, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Datos Biograficos";
			this.tabPage2.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage2.Controls.Add(this.tableLayoutPanel6);
			this.tabPage2.Location = new Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1248, 609);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Reverso";
			this.tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel6.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel6.ColumnCount = 3;
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel6.Controls.Add(this.panel15, 1, 0);
			this.tableLayoutPanel6.Location = new Point(6, 9);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(1236, 597);
			this.tableLayoutPanel6.TabIndex = 31;
			this.panel15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.panel15.Controls.Add(this.btnescan2);
			this.panel15.Controls.Add(this.btnCargarR);
			this.panel15.Controls.Add(this.pbreverso);
			this.panel15.Controls.Add(this.pbpalmaizquierda);
			this.panel15.Controls.Add(this.pbpalmaderecha);
			this.panel15.Controls.Add(this.label8);
			this.panel15.Controls.Add(this.label7);
			this.panel15.Location = new Point(250, 3);
			this.panel15.Name = "panel15";
			this.panel15.Size = new System.Drawing.Size(735, 591);
			this.panel15.TabIndex = 0;
			this.btnescan2.BackColor = Color.White;
			this.btnescan2.Image = (Image)resources.GetObject("btnescan2.Image");
			this.btnescan2.ImageAlign = ContentAlignment.TopCenter;
			this.btnescan2.Location = new Point(98, 546);
			this.btnescan2.Name = "btnescan2";
			this.btnescan2.Size = new System.Drawing.Size(89, 43);
			this.btnescan2.TabIndex = 30;
			this.btnescan2.Text = "Escanear";
			this.btnescan2.TextAlign = ContentAlignment.BottomCenter;
			this.btnescan2.UseVisualStyleBackColor = false;
			this.btnescan2.Click += new EventHandler(this.btnescan2_Click);
			this.btnCargarR.BackColor = Color.White;
			this.btnCargarR.Image = (Image)resources.GetObject("btnCargarR.Image");
			this.btnCargarR.ImageAlign = ContentAlignment.TopCenter;
			this.btnCargarR.Location = new Point(3, 546);
			this.btnCargarR.Name = "btnCargarR";
			this.btnCargarR.Size = new System.Drawing.Size(89, 43);
			this.btnCargarR.TabIndex = 29;
			this.btnCargarR.Text = "Cargar Imagen";
			this.btnCargarR.TextAlign = ContentAlignment.BottomCenter;
			this.btnCargarR.UseVisualStyleBackColor = false;
			this.btnCargarR.Click += new EventHandler(this.btnCargarR_Click);
			this.pbreverso.BackColor = Color.White;
			this.pbreverso.Cursor = Cursors.Cross;
			this.pbreverso.Location = new Point(3, 4);
			this.pbreverso.Name = "pbreverso";
			this.pbreverso.Size = new System.Drawing.Size(350, 536);
			this.pbreverso.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbreverso.TabIndex = 19;
			this.pbreverso.TabStop = false;
			this.pbreverso.Paint += new PaintEventHandler(this.pbreverso_Paint);
			this.pbreverso.MouseClick += new MouseEventHandler(this.pbreverso_MouseClick);
			this.pbreverso.MouseDown += new MouseEventHandler(this.pbreverso_MouseDown);
			this.pbreverso.MouseMove += new MouseEventHandler(this.pbreverso_MouseMove);
			this.pbreverso.MouseUp += new MouseEventHandler(this.pbreverso_MouseUp);
			this.pbpalmaizquierda.BackColor = Color.White;
			this.pbpalmaizquierda.Location = new Point(377, 19);
			this.pbpalmaizquierda.Name = "pbpalmaizquierda";
			this.pbpalmaizquierda.Size = new System.Drawing.Size(338, 245);
			this.pbpalmaizquierda.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbpalmaizquierda.TabIndex = 17;
			this.pbpalmaizquierda.TabStop = false;
			this.pbpalmaizquierda.MouseClick += new MouseEventHandler(this.pbpalmaizquierda_MouseClick);
			this.pbpalmaderecha.BackColor = Color.White;
			this.pbpalmaderecha.Location = new Point(375, 284);
			this.pbpalmaderecha.Name = "pbpalmaderecha";
			this.pbpalmaderecha.Size = new System.Drawing.Size(338, 245);
			this.pbpalmaderecha.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbpalmaderecha.TabIndex = 18;
			this.pbpalmaderecha.TabStop = false;
			this.pbpalmaderecha.MouseClick += new MouseEventHandler(this.pbpalmaderecha_MouseClick);
			this.label8.AutoSize = true;
			this.label8.ForeColor = Color.White;
			this.label8.Location = new Point(374, 267);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 13);
			this.label8.TabIndex = 16;
			this.label8.Text = "Palma Derecha";
			this.label7.AutoSize = true;
			this.label7.ForeColor = Color.White;
			this.label7.Location = new Point(376, 2);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(82, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "Palma Izquierda";
			this.tabPage3.BackColor = Color.FromArgb(48, 63, 105);
			this.tabPage3.Controls.Add(this.tableLayoutPanel5);
			this.tabPage3.Controls.Add(this.tableLayoutPanel3);
			this.tabPage3.Location = new Point(4, 24);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(1248, 609);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Segmentacion Impresiones Dactilares";
			this.tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel5.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel5.Controls.Add(this.panel200, 1, 0);
			this.tableLayoutPanel5.Location = new Point(3, 125);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(1242, 481);
			this.tableLayoutPanel5.TabIndex = 59;
			this.panel200.Anchor = AnchorStyles.Top;
			this.panel200.Controls.Add(this.panel5);
			this.panel200.Controls.Add(this.panel4);
			this.panel200.Controls.Add(this.panel3);
			this.panel200.Controls.Add(this.panel2);
			this.panel200.Controls.Add(this.panel1);
			this.panel200.Controls.Add(this.panel10);
			this.panel200.Controls.Add(this.panel9);
			this.panel200.Controls.Add(this.panel8);
			this.panel200.Controls.Add(this.panel7);
			this.panel200.Controls.Add(this.panel6);
			this.panel200.Controls.Add(this.panel12);
			this.panel200.Controls.Add(this.panel11);
			this.panel200.Controls.Add(this.pictureBox4);
			this.panel200.Location = new Point(165, 3);
			this.panel200.Name = "panel200";
			this.panel200.Size = new System.Drawing.Size(911, 475);
			this.panel200.TabIndex = 0;
			this.panel5.Anchor = AnchorStyles.None;
			this.panel5.BackColor = Color.White;
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.pictureBoxD5);
			this.panel5.Location = new Point(780, 128);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(70, 95);
			this.panel5.TabIndex = 68;
			this.pictureBoxD5.BackColor = SystemColors.ControlLightLight;
			this.pictureBoxD5.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD5.Location = new Point(2, 3);
			this.pictureBoxD5.Name = "pictureBoxD5";
			this.pictureBoxD5.Size = new System.Drawing.Size(63, 87);
			this.pictureBoxD5.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD5.TabIndex = 17;
			this.pictureBoxD5.TabStop = false;
			this.pictureBoxD5.MouseClick += new MouseEventHandler(this.pictureBoxD5_MouseClick);
			this.panel4.Anchor = AnchorStyles.None;
			this.panel4.BackColor = Color.White;
			this.panel4.BorderStyle = BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.pictureBoxD4);
			this.panel4.Location = new Point(710, 45);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(70, 95);
			this.panel4.TabIndex = 67;
			this.pictureBoxD4.BackColor = SystemColors.ControlLightLight;
			this.pictureBoxD4.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD4.Location = new Point(2, 3);
			this.pictureBoxD4.Name = "pictureBoxD4";
			this.pictureBoxD4.Size = new System.Drawing.Size(63, 87);
			this.pictureBoxD4.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD4.TabIndex = 16;
			this.pictureBoxD4.TabStop = false;
			this.pictureBoxD4.MouseClick += new MouseEventHandler(this.pictureBoxD4_MouseClick);
			this.panel3.Anchor = AnchorStyles.None;
			this.panel3.BackColor = Color.White;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.pictureBoxD3);
			this.panel3.Location = new Point(640, 25);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(70, 95);
			this.panel3.TabIndex = 66;
			this.pictureBoxD3.BackColor = SystemColors.ControlLightLight;
			this.pictureBoxD3.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD3.Location = new Point(2, 3);
			this.pictureBoxD3.Name = "pictureBoxD3";
			this.pictureBoxD3.Size = new System.Drawing.Size(61, 87);
			this.pictureBoxD3.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD3.TabIndex = 15;
			this.pictureBoxD3.TabStop = false;
			this.pictureBoxD3.MouseClick += new MouseEventHandler(this.pictureBoxD3_MouseClick);
			this.panel2.Anchor = AnchorStyles.None;
			this.panel2.BackColor = Color.White;
			this.panel2.BorderStyle = BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.pictureBoxD2);
			this.panel2.Location = new Point(567, 41);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(70, 95);
			this.panel2.TabIndex = 65;
			this.pictureBoxD2.BackColor = SystemColors.ControlLightLight;
			this.pictureBoxD2.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD2.Location = new Point(3, 5);
			this.pictureBoxD2.Name = "pictureBoxD2";
			this.pictureBoxD2.Size = new System.Drawing.Size(62, 84);
			this.pictureBoxD2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD2.TabIndex = 14;
			this.pictureBoxD2.TabStop = false;
			this.pictureBoxD2.MouseClick += new MouseEventHandler(this.pictureBoxD2_MouseClick);
			this.panel1.Anchor = AnchorStyles.None;
			this.panel1.BackColor = Color.White;
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.pictureBoxD1);
			this.panel1.Location = new Point(502, 167);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(70, 95);
			this.panel1.TabIndex = 64;
			this.pictureBoxD1.BackColor = SystemColors.ControlLightLight;
			this.pictureBoxD1.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD1.ErrorImage = null;
			this.pictureBoxD1.Location = new Point(3, 5);
			this.pictureBoxD1.Name = "pictureBoxD1";
			this.pictureBoxD1.Size = new System.Drawing.Size(61, 85);
			this.pictureBoxD1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD1.TabIndex = 13;
			this.pictureBoxD1.TabStop = false;
			this.pictureBoxD1.MouseClick += new MouseEventHandler(this.pictureBoxD1_MouseClick);
			this.panel10.Anchor = AnchorStyles.None;
			this.panel10.BackColor = Color.White;
			this.panel10.BorderStyle = BorderStyle.FixedSingle;
			this.panel10.Controls.Add(this.pictureBoxD10);
			this.panel10.Location = new Point(71, 129);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(70, 95);
			this.panel10.TabIndex = 63;
			this.pictureBoxD10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.pictureBoxD10.BackColor = Color.White;
			this.pictureBoxD10.Location = new Point(3, 4);
			this.pictureBoxD10.Name = "pictureBoxD10";
			this.pictureBoxD10.Size = new System.Drawing.Size(62, 85);
			this.pictureBoxD10.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD10.TabIndex = 12;
			this.pictureBoxD10.TabStop = false;
			this.pictureBoxD10.MouseClick += new MouseEventHandler(this.pictureBoxD10_MouseClick);
			this.panel9.Anchor = AnchorStyles.None;
			this.panel9.BackColor = Color.White;
			this.panel9.BorderStyle = BorderStyle.FixedSingle;
			this.panel9.Controls.Add(this.pictureBoxD9);
			this.panel9.Location = new Point(140, 52);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(70, 95);
			this.panel9.TabIndex = 62;
			this.pictureBoxD9.BackColor = Color.White;
			this.pictureBoxD9.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD9.Location = new Point(2, 4);
			this.pictureBoxD9.Name = "pictureBoxD9";
			this.pictureBoxD9.Size = new System.Drawing.Size(63, 87);
			this.pictureBoxD9.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD9.TabIndex = 11;
			this.pictureBoxD9.TabStop = false;
			this.pictureBoxD9.MouseClick += new MouseEventHandler(this.pictureBoxD9_MouseClick);
			this.panel8.Anchor = AnchorStyles.None;
			this.panel8.BackColor = Color.White;
			this.panel8.BorderStyle = BorderStyle.FixedSingle;
			this.panel8.Controls.Add(this.pictureBoxD8);
			this.panel8.Location = new Point(211, 32);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(70, 95);
			this.panel8.TabIndex = 61;
			this.pictureBoxD8.BackColor = Color.White;
			this.pictureBoxD8.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD8.Location = new Point(3, 3);
			this.pictureBoxD8.Name = "pictureBoxD8";
			this.pictureBoxD8.Size = new System.Drawing.Size(62, 87);
			this.pictureBoxD8.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD8.TabIndex = 10;
			this.pictureBoxD8.TabStop = false;
			this.pictureBoxD8.MouseClick += new MouseEventHandler(this.pictureBoxD8_MouseClick);
			this.panel7.Anchor = AnchorStyles.None;
			this.panel7.BackColor = Color.White;
			this.panel7.BorderStyle = BorderStyle.FixedSingle;
			this.panel7.Controls.Add(this.pictureBoxD7);
			this.panel7.Location = new Point(283, 41);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(70, 95);
			this.panel7.TabIndex = 60;
			this.pictureBoxD7.BackColor = Color.White;
			this.pictureBoxD7.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD7.Location = new Point(3, 3);
			this.pictureBoxD7.Name = "pictureBoxD7";
			this.pictureBoxD7.Size = new System.Drawing.Size(62, 88);
			this.pictureBoxD7.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD7.TabIndex = 9;
			this.pictureBoxD7.TabStop = false;
			this.pictureBoxD7.MouseClick += new MouseEventHandler(this.pictureBoxD7_MouseClick);
			this.panel6.Anchor = AnchorStyles.None;
			this.panel6.BackColor = Color.White;
			this.panel6.BorderStyle = BorderStyle.FixedSingle;
			this.panel6.Controls.Add(this.pictureBoxD6);
			this.panel6.Location = new Point(352, 167);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(70, 95);
			this.panel6.TabIndex = 59;
			this.pictureBoxD6.BackColor = Color.White;
			this.pictureBoxD6.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBoxD6.Location = new Point(3, 5);
			this.pictureBoxD6.Name = "pictureBoxD6";
			this.pictureBoxD6.Size = new System.Drawing.Size(61, 85);
			this.pictureBoxD6.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD6.TabIndex = 8;
			this.pictureBoxD6.TabStop = false;
			this.pictureBoxD6.MouseClick += new MouseEventHandler(this.pictureBoxD6_MouseClick);
			this.panel12.Anchor = AnchorStyles.None;
			this.panel12.BackColor = Color.White;
			this.panel12.BorderStyle = BorderStyle.FixedSingle;
			this.panel12.Controls.Add(this.pictureBoxD12);
			this.panel12.Location = new Point(576, 242);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(234, 179);
			this.panel12.TabIndex = 58;
			this.pictureBoxD12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.pictureBoxD12.BackColor = Color.White;
			this.pictureBoxD12.Location = new Point(3, 4);
			this.pictureBoxD12.Name = "pictureBoxD12";
			this.pictureBoxD12.Size = new System.Drawing.Size(226, 169);
			this.pictureBoxD12.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD12.TabIndex = 12;
			this.pictureBoxD12.TabStop = false;
			this.pictureBoxD12.MouseClick += new MouseEventHandler(this.pictureBoxD12_MouseClick);
			this.panel11.Anchor = AnchorStyles.None;
			this.panel11.BackColor = Color.White;
			this.panel11.BorderStyle = BorderStyle.FixedSingle;
			this.panel11.Controls.Add(this.pictureBoxD11);
			this.panel11.Location = new Point(107, 237);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(242, 184);
			this.panel11.TabIndex = 53;
			this.pictureBoxD11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.pictureBoxD11.BackColor = Color.White;
			this.pictureBoxD11.Location = new Point(3, 4);
			this.pictureBoxD11.Name = "pictureBoxD11";
			this.pictureBoxD11.Size = new System.Drawing.Size(234, 174);
			this.pictureBoxD11.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxD11.TabIndex = 12;
			this.pictureBoxD11.TabStop = false;
			this.pictureBoxD11.MouseClick += new MouseEventHandler(this.pictureBoxD11_MouseClick);
			this.pictureBox4.Anchor = AnchorStyles.None;
			this.pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
			this.pictureBox4.Location = new Point(-44, 3);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(998, 456);
			this.pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox4.TabIndex = 8;
			this.pictureBox4.TabStop = false;
			this.tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel3.BackColor = Color.FromArgb(48, 63, 105);
			this.tableLayoutPanel3.ColumnCount = 4;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel3.Controls.Add(this.pbDactilarDerecho, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.pbDactilarIzquierdo, 1, 0);
			this.tableLayoutPanel3.Location = new Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(1242, 116);
			this.tableLayoutPanel3.TabIndex = 58;
			this.pbDactilarDerecho.Anchor = AnchorStyles.Left;
			this.pbDactilarDerecho.BackColor = Color.White;
			this.pbDactilarDerecho.BorderStyle = BorderStyle.Fixed3D;
			this.pbDactilarDerecho.Cursor = Cursors.Cross;
			this.pbDactilarDerecho.Location = new Point(623, 3);
			this.pbDactilarDerecho.Name = "pbDactilarDerecho";
			this.pbDactilarDerecho.Size = new System.Drawing.Size(490, 110);
			this.pbDactilarDerecho.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbDactilarDerecho.TabIndex = 20;
			this.pbDactilarDerecho.TabStop = false;
			this.pbDactilarDerecho.Paint += new PaintEventHandler(this.pbDactilarDerecho_Paint);
			this.pbDactilarDerecho.MouseDown += new MouseEventHandler(this.pbDactilarDerecho_MouseDown);
			this.pbDactilarDerecho.MouseMove += new MouseEventHandler(this.pbDactilarDerecho_MouseMove);
			this.pbDactilarDerecho.MouseUp += new MouseEventHandler(this.pbDactilarDerecho_MouseUp);
			this.pbDactilarIzquierdo.Anchor = AnchorStyles.Right;
			this.pbDactilarIzquierdo.BackColor = Color.White;
			this.pbDactilarIzquierdo.BorderStyle = BorderStyle.Fixed3D;
			this.pbDactilarIzquierdo.Cursor = Cursors.Cross;
			this.pbDactilarIzquierdo.Location = new Point(127, 3);
			this.pbDactilarIzquierdo.Name = "pbDactilarIzquierdo";
			this.pbDactilarIzquierdo.Size = new System.Drawing.Size(490, 110);
			this.pbDactilarIzquierdo.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pbDactilarIzquierdo.TabIndex = 19;
			this.pbDactilarIzquierdo.TabStop = false;
			this.pbDactilarIzquierdo.Paint += new PaintEventHandler(this.pbDactilarIzquierdo_Paint);
			this.pbDactilarIzquierdo.MouseDown += new MouseEventHandler(this.pbDactilarIzquierdo_MouseDown);
			this.pbDactilarIzquierdo.MouseMove += new MouseEventHandler(this.pbDactilarIzquierdo_MouseMove);
			this.pbDactilarIzquierdo.MouseUp += new MouseEventHandler(this.pbDactilarIzquierdo_MouseUp);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.DatosBiograficosToolStripMenuItem, this.RodadasIzquierdaToolStripMenuItem, this.RodadasDerechaToolStripMenuItem, this.palmaIzquierdaToolStripMenuItem, this.palmaDerechaToolStripMenuItem, this.pulgaresToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(171, 136);
			this.DatosBiograficosToolStripMenuItem.Name = "DatosBiograficosToolStripMenuItem";
			this.DatosBiograficosToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.DatosBiograficosToolStripMenuItem.Text = "Datos Biograficos";
			this.RodadasIzquierdaToolStripMenuItem.Name = "RodadasIzquierdaToolStripMenuItem";
			this.RodadasIzquierdaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.RodadasIzquierdaToolStripMenuItem.Text = "Rodadas Izquierda";
			this.RodadasDerechaToolStripMenuItem.Name = "RodadasDerechaToolStripMenuItem";
			this.RodadasDerechaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.RodadasDerechaToolStripMenuItem.Text = "Rodadas Derecha";
			this.palmaIzquierdaToolStripMenuItem.Name = "palmaIzquierdaToolStripMenuItem";
			this.palmaIzquierdaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.palmaIzquierdaToolStripMenuItem.Text = "Palma Izquierda";
			this.palmaDerechaToolStripMenuItem.Name = "palmaDerechaToolStripMenuItem";
			this.palmaDerechaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.palmaDerechaToolStripMenuItem.Text = "Palma Derecha";
			this.pulgaresToolStripMenuItem.Name = "pulgaresToolStripMenuItem";
			this.pulgaresToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.pulgaresToolStripMenuItem.Text = "Pulgares";
			this.contextMenuStrip2.Items.AddRange(new ToolStripItem[] { this.quitarToolStripMenuItem });
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(108, 26);
			this.quitarToolStripMenuItem.Name = "quitarToolStripMenuItem";
			this.quitarToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.quitarToolStripMenuItem.Text = "Quitar";
			this.quitarToolStripMenuItem.Click += new EventHandler(this.quitarToolStripMenuItem_Click);
			this.contextMenuStrip4.Items.AddRange(new ToolStripItem[] { this.QuitarcontextMenuStrip4 });
			this.contextMenuStrip4.Name = "contextMenuStrip4";
			this.contextMenuStrip4.Size = new System.Drawing.Size(108, 26);
			this.QuitarcontextMenuStrip4.Name = "QuitarcontextMenuStrip4";
			this.QuitarcontextMenuStrip4.Size = new System.Drawing.Size(107, 22);
			this.QuitarcontextMenuStrip4.Text = "Quitar";
			this.QuitarcontextMenuStrip4.Click += new EventHandler(this.QuitarcontextMenuStrip4_Click);
			this.contextMenuStrip3.Items.AddRange(new ToolStripItem[] { this.palmaIzquierdaToolStripMenuItem1, this.palmaDerechaToolStripMenuItem1 });
			this.contextMenuStrip3.Name = "contextMenuStrip3";
			this.contextMenuStrip3.Size = new System.Drawing.Size(159, 48);
			this.palmaIzquierdaToolStripMenuItem1.Name = "palmaIzquierdaToolStripMenuItem1";
			this.palmaIzquierdaToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
			this.palmaIzquierdaToolStripMenuItem1.Text = "Palma Izquierda";
			this.palmaDerechaToolStripMenuItem1.Name = "palmaDerechaToolStripMenuItem1";
			this.palmaDerechaToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
			this.palmaDerechaToolStripMenuItem1.Text = "Palma Derecha";
			this.contextMenuStrip5.Items.AddRange(new ToolStripItem[] { this.amputadoToolStripMenuItem, this.descartadoToolStripMenuItem });
			this.contextMenuStrip5.Name = "contextMenuStrip5";
			this.contextMenuStrip5.Size = new System.Drawing.Size(134, 48);
			this.amputadoToolStripMenuItem.Name = "amputadoToolStripMenuItem";
			this.amputadoToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.amputadoToolStripMenuItem.Text = "Amputado";
			this.amputadoToolStripMenuItem.Click += new EventHandler(this.amputadoToolStripMenuItem_Click);
			this.descartadoToolStripMenuItem.Name = "descartadoToolStripMenuItem";
			this.descartadoToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.descartadoToolStripMenuItem.Text = "Descartado";
			this.descartadoToolStripMenuItem.Click += new EventHandler(this.descartadoToolStripMenuItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(1321, 782);
			base.ControlBox = false;
			base.Controls.Add(this.tableLayoutPanel11);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.tableLayoutPanel2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.MinimizeBox = false;
			base.Name = "fCropB";
			base.ShowIcon = false;
			this.Text = "Segmentacion de Formulario";
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.fCrop_Load);
			this.panel100.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel11.ResumeLayout(false);
			this.tableLayoutPanel11.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.panel14.ResumeLayout(false);
			this.panel14.PerformLayout();
			((ISupportInitialize)this.pbPulgares).EndInit();
			((ISupportInitialize)this.pbSimultaneaDer).EndInit();
			((ISupportInitialize)this.pbSimultaneaIzq).EndInit();
			((ISupportInitialize)this.pbDactilarIzq).EndInit();
			((ISupportInitialize)this.pbDactilarDer).EndInit();
			((ISupportInitialize)this.pbbiografia).EndInit();
			((ISupportInitialize)this.pbanverso).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.panel15.ResumeLayout(false);
			this.panel15.PerformLayout();
			((ISupportInitialize)this.pbreverso).EndInit();
			((ISupportInitialize)this.pbpalmaizquierda).EndInit();
			((ISupportInitialize)this.pbpalmaderecha).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.panel200.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD5).EndInit();
			this.panel4.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD4).EndInit();
			this.panel3.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD3).EndInit();
			this.panel2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD2).EndInit();
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD1).EndInit();
			this.panel10.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD10).EndInit();
			this.panel9.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD9).EndInit();
			this.panel8.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD8).EndInit();
			this.panel7.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD7).EndInit();
			this.panel6.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD6).EndInit();
			this.panel12.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD12).EndInit();
			this.panel11.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxD11).EndInit();
			((ISupportInitialize)this.pictureBox4).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			((ISupportInitialize)this.pbDactilarDerecho).EndInit();
			((ISupportInitialize)this.pbDactilarIzquierdo).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.contextMenuStrip4.ResumeLayout(false);
			this.contextMenuStrip3.ResumeLayout(false);
			this.contextMenuStrip5.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void palmaDerechaToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void palmaDerechaToolStripMenuItem1_Click(object sender, EventArgs e)
		{
		}

		private void palmaIzquierdaToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void palmaIzquierdaToolStripMenuItem1_Click(object sender, EventArgs e)
		{
		}

		private void panelcolor(Panel panel, int calidad)
		{
			if (calidad >= 70)
			{
				panel.BackColor = Color.Green;
			}
			if ((calidad < 50 ? false : calidad < 70))
			{
				panel.BackColor = Color.Yellow;
			}
			if (calidad < 50)
			{
				panel.BackColor = Color.Red;
			}
		}

		private void pbanverso_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					this.contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.pbanverso.Image == null)
					{
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbanverso_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if ((this.pbbiografia.Image == null || this.pbDactilarDer.Image == null || this.pbDactilarIzq.Image == null || this.pbSimultaneaDer.Image == null || this.pbSimultaneaIzq.Image == null ? false : this.pbPulgares.Image != null))
				{
					MessageBox.Show("No se puede realizar el recorte, antes debe quitar y seleccionar el area.");
				}
				else
				{
					this.IsMouseDown3 = true;
					this.StartLocation = e.Location;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbanverso_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if ((this.pbbiografia.Image == null || this.pbDactilarDer.Image == null || this.pbDactilarIzq.Image == null || this.pbSimultaneaDer.Image == null || this.pbSimultaneaIzq.Image == null ? true : this.pbPulgares.Image == null))
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						if (this.IsMouseDown3)
						{
							this.EndLcation = e.Location;
							this.pbanverso.Invalidate();
						}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbanverso_MouseUp(object sender, MouseEventArgs e)
		{
			DataImageCore dataImageCore;
			System.Drawing.Size size;
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.IsMouseDown3)
					{
						this.EndLcation = e.Location;
						this.IsMouseDown3 = false;
						Rectangle rectangle = this.rect;
						if (true)
						{
							this.rect.X = Convert.ToInt16(Math.Round((double)this.rect.X * this.escalaX, 0));
							this.rect.Y = Convert.ToInt16(Math.Round((double)this.rect.Y * this.escalaY, 0));
							this.rect.Width = Convert.ToInt16(Math.Round((double)this.rect.Width * this.escalaX, 0));
							this.rect.Height = Convert.ToInt16(Math.Round((double)this.rect.Height * this.escalaY, 0));
							this.imgInput.ROI = this.rect;
							if (this.SeleccionAnverso == "DB")
							{
								if (this.pbbiografia.Image == null)
								{
									this.imgInputDB = this.imgInput.Clone();
									this.pbbiografia.Image = this.imgInputDB.Bitmap;
									this.FormularioPalma.ListModels = this.ser.ImageToByteArray(this.pbbiografia.Image);
								}
								else
								{
									MessageBox.Show(string.Concat("Ya selecciono ", this.MenuAnverso1));
									return;
								}
							}
							if (this.SeleccionAnverso == "RI")
							{
								if (this.pbDactilarIzq.Image == null)
								{
									this.imgInputRI = this.imgInput.Clone();
									this.pbDactilarIzq.Image = this.imgInputRI.Bitmap;
									this.pbDactilarIzquierdo.Image = this.imgInputRI.Bitmap;
									DataImageCore?[] rolledPrints = this.FormularioPalma.RolledPrints;
									dataImageCore = new DataImageCore()
									{
										Bir = new BIR()
										{
											BDB = this.ser.ImageToByteArray(this.imgInputRI.Bitmap),
											BDBInfo = new BIRBDBInfo()
											{
												Subtype = "RolledPrintsLeft"
											}
										},
										RegionType = RegionType.Segmentado
									};
									rolledPrints[1] = new DataImageCore?(dataImageCore);
									if (this.pbDactilarIzquierdo.Image != null)
									{
										double width = (double)this.imgInputRI.Width;
										size = this.pbDactilarIzquierdo.Size;
										this.escalaXDI = width / (double)size.Width;
										double height = (double)this.imgInputRI.Height;
										size = this.pbDactilarIzquierdo.Size;
										this.escalaYDI = height / (double)size.Height;
									}
									this.contextMenuStrip1.Items[1].Text = string.Concat(this.MenuAnverso2, "  (OK)");
								}
								else
								{
									MessageBox.Show(string.Concat("Ya selecciono ", this.MenuAnverso2));
									return;
								}
							}
							if (this.SeleccionAnverso == "RD")
							{
								if (this.pbDactilarDer.Image == null)
								{
									this.imgInputRD = this.imgInput.Clone();
									this.pbDactilarDer.Image = this.imgInputRD.Bitmap;
									this.pbDactilarDerecho.Image = this.imgInputRD.Bitmap;
									DataImageCore?[] nullable = this.FormularioPalma.RolledPrints;
									dataImageCore = new DataImageCore()
									{
										Bir = new BIR()
										{
											BDB = this.ser.ImageToByteArray(this.imgInputRD.Bitmap),
											BDBInfo = new BIRBDBInfo()
											{
												Subtype = "RolledPrintsRight"
											}
										},
										RegionType = RegionType.Segmentado
									};
									nullable[0] = new DataImageCore?(dataImageCore);
									if (this.pbDactilarDerecho.Image != null)
									{
										double num = (double)this.imgInputRD.Width;
										size = this.pbDactilarDerecho.Size;
										this.escalaXDD = num / (double)size.Width;
										double height1 = (double)this.imgInputRD.Height;
										size = this.pbDactilarDerecho.Size;
										this.escalaYDD = height1 / (double)size.Height;
									}
									this.contextMenuStrip1.Items[2].Text = string.Concat(this.MenuAnverso3, "  (OK)");
								}
								else
								{
									MessageBox.Show(string.Concat("Ya selecciono ", this.MenuAnverso3));
									return;
								}
							}
							if (this.SeleccionAnverso == "PI")
							{
								if (this.pbSimultaneaIzq.Image == null)
								{
									this.imgInputSI = this.imgInput.Clone();
									this.pbSimultaneaIzq.Image = this.imgInputSI.Bitmap;
									DataImageCore?[] simultaneousPrints = this.FormularioPalma.SimultaneousPrints;
									dataImageCore = new DataImageCore()
									{
										Bir = new BIR()
										{
											BDB = this.ser.ImageToByteArray(this.imgInputSI.Bitmap),
											BDBInfo = new BIRBDBInfo()
											{
												Subtype = "SimultaneousPrintsLeft"
											}
										},
										RegionType = RegionType.Segmentado
									};
									simultaneousPrints[0] = new DataImageCore?(dataImageCore);
									this.contextMenuStrip1.Items[3].Text = string.Concat(this.MenuAnverso4, "  (OK)");
								}
								else
								{
									MessageBox.Show(string.Concat("Ya selecciono ", this.MenuAnverso4));
									return;
								}
							}
							if (this.SeleccionAnverso == "PD")
							{
								if (this.pbSimultaneaDer.Image == null)
								{
									this.imgInputSD = this.imgInput.Clone();
									this.pbSimultaneaDer.Image = this.imgInputSD.Bitmap;
									DataImageCore?[] nullableArray = this.FormularioPalma.SimultaneousPrints;
									dataImageCore = new DataImageCore()
									{
										Bir = new BIR()
										{
											BDB = this.ser.ImageToByteArray(this.imgInputSD.Bitmap),
											BDBInfo = new BIRBDBInfo()
											{
												Subtype = "SimultaneousPrintsRight"
											}
										},
										RegionType = RegionType.Segmentado
									};
									nullableArray[2] = new DataImageCore?(dataImageCore);
									this.contextMenuStrip1.Items[4].Text = string.Concat(this.MenuAnverso5, "  (OK)");
								}
								else
								{
									MessageBox.Show(string.Concat("Ya selecciono ", this.MenuAnverso5));
									return;
								}
							}
							if (this.SeleccionAnverso == "PU")
							{
								if (this.pbPulgares.Image == null)
								{
									this.imgInputPU = this.imgInput.Clone();
									this.pbPulgares.Image = this.imgInputPU.Bitmap;
									DataImageCore?[] simultaneousPrints1 = this.FormularioPalma.SimultaneousPrints;
									dataImageCore = new DataImageCore()
									{
										Bir = new BIR()
										{
											BDB = this.ser.ImageToByteArray(this.imgInputPU.Bitmap),
											BDBInfo = new BIRBDBInfo()
											{
												Subtype = "SimultaneousPrintsThumb"
											}
										},
										RegionType = RegionType.Segmentado
									};
									simultaneousPrints1[1] = new DataImageCore?(dataImageCore);
									this.contextMenuStrip1.Items[5].Text = string.Concat(this.MenuAnverso6, "  (OK)");
								}
								else
								{
									MessageBox.Show(string.Concat("Ya selecciono ", this.MenuAnverso6));
									return;
								}
							}
							this.btnSiguiente.Enabled = this.validarAnverso();
						}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbanverso_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				if ((this.pbbiografia.Image == null || this.pbDactilarDer.Image == null || this.pbDactilarIzq.Image == null || this.pbSimultaneaDer.Image == null || this.pbSimultaneaIzq.Image == null ? true : this.pbPulgares.Image == null))
				{
					Rectangle rectangle = this.rect;
					if (true)
					{
						Pen ColorBorde = new Pen(Color.Blue, 2f);
						e.Graphics.DrawRectangle(ColorBorde, this.GetRectangle());
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbbiografia_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (this.pbbiografia.Image != null)
					{
						this.QuitarAnverso = "DB";
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbbiografia.BackColor = Color.Gainsboro;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.SeleccionAnverso = "DB";
					this.pbbiografia.BackColor = Color.Blue;
					this.pbbiografia.Padding = new System.Windows.Forms.Padding(2);
					this.pbDactilarIzq.BackColor = Color.Gainsboro;
					this.pbDactilarDer.BackColor = Color.Gainsboro;
					this.pbSimultaneaIzq.BackColor = Color.Gainsboro;
					this.pbSimultaneaDer.BackColor = Color.Gainsboro;
					this.pbPulgares.BackColor = Color.Gainsboro;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbDactilarDerecho_MouseDown(object sender, MouseEventArgs e)
		{
			this.IsMouseDownDD = true;
			this.StartLocationDD = e.Location;
		}

		private void pbDactilarDerecho_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (this.IsMouseDownDD)
				{
					this.EndLcationDD = e.Location;
					this.pbDactilarDerecho.Invalidate();
				}
			}
		}

		private void pbDactilarDerecho_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.IsMouseDownDD)
					{
						this.EndLcationDD = e.Location;
						this.IsMouseDownDD = false;
						Rectangle rectangle = this.rectDI;
						if (false)
						{
							return;
						}
						else if ((this.rectDD.Width <= 0 ? false : this.rectDD.Height > 0))
						{
							this.rectDD.X = Convert.ToInt16(Math.Round((double)this.rectDD.X * this.escalaXDD, 0));
							this.rectDD.Y = Convert.ToInt16(Math.Round((double)this.rectDD.Y * this.escalaYDD, 0));
							this.rectDD.Width = Convert.ToInt16(Math.Round((double)this.rectDD.Width * this.escalaXDD, 0));
							this.rectDD.Height = Convert.ToInt16(Math.Round((double)this.rectDD.Height * this.escalaYDD, 0));
							if (this.SeleccionDD == "DD1")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarDerecho.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDD);
								this.pictureBoxD1.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel1);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right Thumb").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(1);
								this.FormularioPalma.DiscardRolledFingers.Remove(1);
							}
							if (this.SeleccionDD == "DD2")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarDerecho.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDD);
								this.pictureBoxD2.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel2);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right IndexFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(2);
								this.FormularioPalma.DiscardRolledFingers.Remove(2);
							}
							if (this.SeleccionDD == "DD3")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarDerecho.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDD);
								this.pictureBoxD3.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel3);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right MiddleFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(3);
								this.FormularioPalma.DiscardRolledFingers.Remove(3);
							}
							if (this.SeleccionDD == "DD4")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarDerecho.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDD);
								this.pictureBoxD4.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel4);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right RingFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(4);
								this.FormularioPalma.DiscardRolledFingers.Remove(4);
							}
							if (this.SeleccionDD == "DD5")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarDerecho.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDD);
								this.pictureBoxD5.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel5);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right LittleFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(5);
								this.FormularioPalma.DiscardRolledFingers.Remove(5);
							}
						}
						else
						{
							return;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void pbDactilarDerecho_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rectangle = this.rectDD;
			if (true)
			{
				Pen ColorBorde = new Pen(Color.Blue, 2f);
				e.Graphics.DrawRectangle(ColorBorde, this.GetRectangleDD());
			}
		}

		private void pbDactilarIzq_Click(object sender, EventArgs e)
		{
		}

		private void pbDactilarIzquierdo_MouseDown(object sender, MouseEventArgs e)
		{
			this.IsMouseDownDI = true;
			this.StartLocationDI = e.Location;
		}

		private void pbDactilarIzquierdo_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (this.IsMouseDownDI)
				{
					this.EndLcationDI = e.Location;
					this.pbDactilarIzquierdo.Invalidate();
				}
			}
		}

		private void pbDactilarIzquierdo_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.IsMouseDownDI)
					{
						this.EndLcationDI = e.Location;
						this.IsMouseDownDI = false;
						Rectangle rectangle = this.rectDI;
						if (false)
						{
							return;
						}
						else if ((this.rectDI.Width <= 0 ? false : this.rectDI.Height > 0))
						{
							this.rectDI.X = Convert.ToInt16(Math.Round((double)this.rectDI.X * this.escalaXDI, 0));
							this.rectDI.Y = Convert.ToInt16(Math.Round((double)this.rectDI.Y * this.escalaYDI, 0));
							this.rectDI.Width = Convert.ToInt16(Math.Round((double)this.rectDI.Width * this.escalaXDI, 0));
							this.rectDI.Height = Convert.ToInt16(Math.Round((double)this.rectDI.Height * this.escalaYDI, 0));
							if (this.SeleccionDI == "DI6")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarIzquierdo.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDI);
								this.Calidad(BmDedoCrop, this.panel6);
								this.pictureBoxD6.Image = BmDedoCrop;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left Thumb").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(6);
								this.FormularioPalma.DiscardRolledFingers.Remove(6);
							}
							if (this.SeleccionDI == "DI7")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarIzquierdo.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDI);
								this.pictureBoxD7.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel7);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left IndexFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(7);
								this.FormularioPalma.DiscardRolledFingers.Remove(7);
							}
							if (this.SeleccionDI == "DI8")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarIzquierdo.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDI);
								this.pictureBoxD8.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel8);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left MiddleFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(8);
								this.FormularioPalma.DiscardRolledFingers.Remove(8);
							}
							if (this.SeleccionDI == "DI9")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarIzquierdo.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDI);
								this.pictureBoxD9.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel9);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left RingFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(9);
								this.FormularioPalma.DiscardRolledFingers.Remove(9);
							}
							if (this.SeleccionDI == "DI10")
							{
								Bitmap BmDedo = (Bitmap)this.pbDactilarIzquierdo.Image.Clone();
								Bitmap BmDedoCrop = this.CropImage(BmDedo, this.rectDI);
								this.pictureBoxD10.Image = BmDedoCrop;
								this.Calidad(BmDedoCrop, this.panel10);
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left LittleFinger").BDB = this.ser.ImageToByteArray(BmDedoCrop);
								this.FormularioPalma.AmputeeRolledFingers.Remove(10);
								this.FormularioPalma.DiscardRolledFingers.Remove(10);
							}
						}
						else
						{
							return;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void pbDactilarIzquierdo_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rectangle = this.rectDI;
			if (true)
			{
				Pen ColorBorde = new Pen(Color.Blue, 2f);
				e.Graphics.DrawRectangle(ColorBorde, this.GetRectangleDI());
			}
		}

		private void pbPalmaD_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (this.pbSimultaneaDer.Image != null)
					{
						this.QuitarAnverso = "PD";
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbSimultaneaDer.BackColor = Color.Gainsboro;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.SeleccionAnverso = "PD";
					this.pbbiografia.BackColor = Color.Gainsboro;
					this.pbDactilarIzq.BackColor = Color.Gainsboro;
					this.pbDactilarDer.BackColor = Color.Gainsboro;
					this.pbSimultaneaIzq.BackColor = Color.Gainsboro;
					this.pbSimultaneaDer.BackColor = Color.Blue;
					this.pbSimultaneaDer.Padding = new System.Windows.Forms.Padding(2);
					this.pbPulgares.BackColor = Color.Gainsboro;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbpalmaderecha_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (this.pbpalmaderecha.Image != null)
				{
					this.QuitarReverso = "PD";
					this.contextMenuStrip4.Show(System.Windows.Forms.Cursor.Position);
					this.pbpalmaizquierda.BackColor = Color.Gainsboro;
				}
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionReverso = "PD";
				this.pbpalmaizquierda.BackColor = Color.Gainsboro;
				this.pbpalmaderecha.BackColor = Color.Blue;
				this.pbpalmaderecha.Padding = new System.Windows.Forms.Padding(2);
			}
		}

		private void pbPalmaI_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (this.pbSimultaneaIzq.Image != null)
					{
						this.QuitarAnverso = "PI";
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbSimultaneaIzq.BackColor = Color.Gainsboro;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.SeleccionAnverso = "PI";
					this.pbbiografia.BackColor = Color.Gainsboro;
					this.pbDactilarIzq.BackColor = Color.Gainsboro;
					this.pbDactilarDer.BackColor = Color.Gainsboro;
					this.pbSimultaneaIzq.BackColor = Color.Blue;
					this.pbSimultaneaIzq.Padding = new System.Windows.Forms.Padding(2);
					this.pbSimultaneaDer.BackColor = Color.Gainsboro;
					this.pbPulgares.BackColor = Color.Gainsboro;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbpalmaizquierda_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (this.pbpalmaizquierda.Image != null)
				{
					this.QuitarReverso = "PI";
					this.contextMenuStrip4.Show(System.Windows.Forms.Cursor.Position);
					this.pbpalmaizquierda.BackColor = Color.Gainsboro;
				}
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionReverso = "PI";
				this.pbpalmaizquierda.BackColor = Color.Blue;
				this.pbpalmaizquierda.Padding = new System.Windows.Forms.Padding(2);
				this.pbpalmaderecha.BackColor = Color.Gainsboro;
			}
		}

		private void pbPulgares_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (this.pbPulgares.Image != null)
					{
						this.QuitarAnverso = "PU";
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbPulgares.BackColor = Color.Gainsboro;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.SeleccionAnverso = "PU";
					this.pbbiografia.BackColor = Color.Gainsboro;
					this.pbDactilarIzq.BackColor = Color.Gainsboro;
					this.pbDactilarDer.BackColor = Color.Gainsboro;
					this.pbSimultaneaIzq.BackColor = Color.Gainsboro;
					this.pbSimultaneaDer.BackColor = Color.Gainsboro;
					this.pbPulgares.BackColor = Color.Blue;
					this.pbPulgares.Padding = new System.Windows.Forms.Padding(2);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbreverso_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					this.contextMenuStrip3.Show(System.Windows.Forms.Cursor.Position);
				}
			}
			catch
			{
			}
		}

		private void pbreverso_MouseDown(object sender, MouseEventArgs e)
		{
			if ((this.pbpalmaizquierda.Image == null ? false : this.pbpalmaderecha.Image != null))
			{
				MessageBox.Show("No se puede realizar el recorte, antes debe quitar y seleccionar el area.");
			}
			else
			{
				this.IsMouseDown3R = true;
				this.StartLocationR = e.Location;
			}
		}

		private void pbreverso_MouseMove(object sender, MouseEventArgs e)
		{
			if ((this.pbpalmaizquierda.Image == null ? true : this.pbpalmaderecha.Image == null))
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.IsMouseDown3R)
					{
						this.EndLcationR = e.Location;
						this.pbreverso.Invalidate();
					}
				}
			}
		}

		private void pbreverso_MouseUp(object sender, MouseEventArgs e)
		{
			DataImageCore dataImageCore;
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.IsMouseDown3R)
					{
						this.EndLcationR = e.Location;
						this.IsMouseDown3R = false;
						Rectangle rectangle = this.rectR;
						if (true)
						{
							this.rectR.X = Convert.ToInt16(Math.Round((double)this.rectR.X * this.escalaXR, 0));
							this.rectR.Y = Convert.ToInt16(Math.Round((double)this.rectR.Y * this.escalaYR, 0));
							this.rectR.Width = Convert.ToInt16(Math.Round((double)this.rectR.Width * this.escalaXR, 0));
							this.rectR.Height = Convert.ToInt16(Math.Round((double)this.rectR.Height * this.escalaYR, 0));
							this.imgInputR.ROI = this.rectR;
							this.rectR = Rectangle.Empty;
							string seleccionReverso = this.SeleccionReverso;
							if (seleccionReverso != null)
							{
								if (seleccionReverso != "PI")
								{
									if (seleccionReverso == "PD")
									{
										if (this.pbpalmaderecha.Image == null)
										{
											this.imgInputPD = this.imgInputR.Clone();
											this.pbpalmaderecha.Image = this.imgInputPD.Bitmap;
											Bitmap BPD = (Bitmap)this.imgInputPD.Bitmap.Clone();
											BPD.RotateFlip(RotateFlipType.Rotate270FlipNone);
											this.pictureBoxD12.Image = BPD;
											this.Calidad(BPD, this.panel12);
											DataImageCore?[] palmsPrints = this.FormularioPalma.PalmsPrints;
											dataImageCore = new DataImageCore()
											{
												Bir = new BIR()
												{
													BDB = this.ser.ImageToByteArray(BPD),
													BDBInfo = new BIRBDBInfo()
													{
														Subtype = "PalmsPrintsRight"
													}
												},
												RegionType = RegionType.Segmentado
											};
											palmsPrints[0] = new DataImageCore?(dataImageCore);
											this.contextMenuStrip3.Items[1].Text = string.Concat(this.MenuReverso2, "  (OK)");
										}
										else
										{
											MessageBox.Show("Ya selecciono Palma Derecha");
											return;
										}
									}
								}
								else if (this.pbpalmaizquierda.Image == null)
								{
									this.imgInputPI = this.imgInputR.Clone();
									this.pbpalmaizquierda.Image = this.imgInputPI.Bitmap;
									Bitmap BPI = (Bitmap)this.imgInputPI.Bitmap.Clone();
									BPI.RotateFlip(RotateFlipType.Rotate270FlipNone);
									this.pictureBoxD11.Image = BPI;
									this.Calidad(BPI, this.panel11);
									DataImageCore?[] nullable = this.FormularioPalma.PalmsPrints;
									dataImageCore = new DataImageCore()
									{
										Bir = new BIR()
										{
											BDB = this.ser.ImageToByteArray(BPI),
											BDBInfo = new BIRBDBInfo()
											{
												Subtype = "PalmsPrintsLeft"
											}
										},
										RegionType = RegionType.Segmentado
									};
									nullable[1] = new DataImageCore?(dataImageCore);
									this.contextMenuStrip3.Items[0].Text = string.Concat(this.MenuReverso1, "  (OK)");
								}
								else
								{
									MessageBox.Show("Ya selecciono Palma Izquierda");
									return;
								}
							}
							this.btnSiguiente.Enabled = this.validarReverso();
						}
					}
				}
			}
			catch
			{
			}
		}

		private void pbreverso_Paint(object sender, PaintEventArgs e)
		{
			if ((this.pbpalmaderecha.Image == null ? true : this.pbpalmaizquierda.Image == null))
			{
				Rectangle rectangle = this.rectR;
				if (true)
				{
					Pen ColorBorde = new Pen(Color.Blue, 2f);
					e.Graphics.DrawRectangle(ColorBorde, this.GetRectangleR());
				}
			}
		}

		private void pbrodadasD_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (this.pbDactilarDer.Image != null)
					{
						this.QuitarAnverso = "RD";
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbDactilarDer.BackColor = Color.Gainsboro;
						this.segmentar = 1;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.SeleccionAnverso = "RD";
					this.pbbiografia.BackColor = Color.Gainsboro;
					this.pbDactilarIzq.BackColor = Color.Gainsboro;
					this.pbDactilarDer.BackColor = Color.Blue;
					this.pbDactilarDer.Padding = new System.Windows.Forms.Padding(2);
					this.pbSimultaneaIzq.BackColor = Color.Gainsboro;
					this.pbSimultaneaDer.BackColor = Color.Gainsboro;
					this.pbPulgares.BackColor = Color.Gainsboro;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pbrodadasI_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (this.pbDactilarIzq.Image != null)
					{
						this.QuitarAnverso = "RI";
						this.contextMenuStrip2.Show(System.Windows.Forms.Cursor.Position);
						this.pbDactilarIzq.BackColor = Color.Gainsboro;
						this.segmentar = 1;
					}
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					this.SeleccionAnverso = "RI";
					this.pbbiografia.BackColor = Color.Gainsboro;
					this.pbDactilarIzq.BackColor = Color.Blue;
					this.pbDactilarIzq.Padding = new System.Windows.Forms.Padding(2);
					this.pbDactilarDer.BackColor = Color.Gainsboro;
					this.pbSimultaneaIzq.BackColor = Color.Gainsboro;
					this.pbSimultaneaDer.BackColor = Color.Gainsboro;
					this.pbPulgares.BackColor = Color.Gainsboro;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pictureBoxD1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDD = "DD1";
				this.pictureBoxD1.BackColor = Color.Blue;
				this.pictureBoxD1.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD2.BackColor = Color.Gainsboro;
				this.pictureBoxD3.BackColor = Color.Gainsboro;
				this.pictureBoxD4.BackColor = Color.Gainsboro;
				this.pictureBoxD5.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D1";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD1.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD10_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDI = "DI10";
				this.pictureBoxD10.BackColor = Color.Blue;
				this.pictureBoxD10.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD9.BackColor = Color.Gainsboro;
				this.pictureBoxD8.BackColor = Color.Gainsboro;
				this.pictureBoxD7.BackColor = Color.Gainsboro;
				this.pictureBoxD6.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D10";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD10.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD11_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDD = "P23";
				this.pictureBoxD11.BackColor = Color.Blue;
				this.pictureBoxD11.Padding = new System.Windows.Forms.Padding(2);
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "P23";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD11.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD12_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "P21";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD12.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD2_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDD = "DD2";
				this.pictureBoxD2.BackColor = Color.Blue;
				this.pictureBoxD2.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD1.BackColor = Color.Gainsboro;
				this.pictureBoxD3.BackColor = Color.Gainsboro;
				this.pictureBoxD4.BackColor = Color.Gainsboro;
				this.pictureBoxD5.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D2";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD2.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD3_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDD = "DD3";
				this.pictureBoxD3.BackColor = Color.Blue;
				this.pictureBoxD3.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD1.BackColor = Color.Gainsboro;
				this.pictureBoxD2.BackColor = Color.Gainsboro;
				this.pictureBoxD4.BackColor = Color.Gainsboro;
				this.pictureBoxD5.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D3";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD3.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD4_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDD = "DD4";
				this.pictureBoxD4.BackColor = Color.Blue;
				this.pictureBoxD4.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD1.BackColor = Color.Gainsboro;
				this.pictureBoxD2.BackColor = Color.Gainsboro;
				this.pictureBoxD3.BackColor = Color.Gainsboro;
				this.pictureBoxD5.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D4";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD4.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD5_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDD = "DD5";
				this.pictureBoxD5.BackColor = Color.Blue;
				this.pictureBoxD5.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD1.BackColor = Color.Gainsboro;
				this.pictureBoxD2.BackColor = Color.Gainsboro;
				this.pictureBoxD3.BackColor = Color.Gainsboro;
				this.pictureBoxD4.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D5";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD5.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD6_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDI = "DI6";
				this.pictureBoxD6.BackColor = Color.Blue;
				this.pictureBoxD6.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD10.BackColor = Color.Gainsboro;
				this.pictureBoxD9.BackColor = Color.Gainsboro;
				this.pictureBoxD8.BackColor = Color.Gainsboro;
				this.pictureBoxD7.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D6";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD6.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD7_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDI = "DI7";
				this.pictureBoxD7.BackColor = Color.Blue;
				this.pictureBoxD7.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD10.BackColor = Color.Gainsboro;
				this.pictureBoxD9.BackColor = Color.Gainsboro;
				this.pictureBoxD8.BackColor = Color.Gainsboro;
				this.pictureBoxD6.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D7";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD7.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD8_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDI = "DI8";
				this.pictureBoxD8.BackColor = Color.Blue;
				this.pictureBoxD8.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD10.BackColor = Color.Gainsboro;
				this.pictureBoxD9.BackColor = Color.Gainsboro;
				this.pictureBoxD7.BackColor = Color.Gainsboro;
				this.pictureBoxD6.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D8";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD8.BackColor = Color.Gainsboro;
			}
		}

		private void pictureBoxD9_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.SeleccionDI = "DI9";
				this.pictureBoxD9.BackColor = Color.Blue;
				this.pictureBoxD9.Padding = new System.Windows.Forms.Padding(2);
				this.pictureBoxD10.BackColor = Color.Gainsboro;
				this.pictureBoxD8.BackColor = Color.Gainsboro;
				this.pictureBoxD7.BackColor = Color.Gainsboro;
				this.pictureBoxD6.BackColor = Color.Gainsboro;
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.QuitarRolado = "D9";
				this.contextMenuStrip5.Show(System.Windows.Forms.Cursor.Position);
				this.pictureBoxD9.BackColor = Color.Gainsboro;
			}
		}

		private void pulgaresToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void QuitarcontextMenuStrip4_Click(object sender, EventArgs e)
		{
			string quitarReverso = this.QuitarReverso;
			if (quitarReverso != null)
			{
				if (quitarReverso == "PI")
				{
					this.pbpalmaizquierda.Image = null;
					this.contextMenuStrip3.Items[0].Text = this.MenuReverso1;
				}
				else if (quitarReverso == "PD")
				{
					this.pbpalmaderecha.Image = null;
					this.contextMenuStrip3.Items[1].Text = this.MenuReverso2;
				}
			}
		}

		private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string quitarAnverso = this.QuitarAnverso;
				if (quitarAnverso != null)
				{
					if (quitarAnverso == "DB")
					{
						this.pbbiografia.Image = null;
						this.contextMenuStrip1.Items[0].Text = this.MenuAnverso1;
					}
					else if (quitarAnverso == "RI")
					{
						this.pbDactilarIzq.Image = null;
						this.contextMenuStrip1.Items[1].Text = this.MenuAnverso2;
					}
					else if (quitarAnverso == "RD")
					{
						this.pbDactilarDer.Image = null;
						this.contextMenuStrip1.Items[2].Text = this.MenuAnverso3;
					}
					else if (quitarAnverso == "PI")
					{
						this.pbSimultaneaIzq.Image = null;
						this.contextMenuStrip1.Items[3].Text = this.MenuAnverso4;
					}
					else if (quitarAnverso == "PD")
					{
						this.pbSimultaneaDer.Image = null;
						this.contextMenuStrip1.Items[4].Text = this.MenuAnverso5;
					}
					else if (quitarAnverso == "PU")
					{
						this.pbPulgares.Image = null;
						this.contextMenuStrip1.Items[5].Text = this.MenuAnverso6;
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void RodadasDerechaToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void RodadasIzquierdaToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void SegmentarAnversoReverso()
		{
			Rectangle section;
			Bitmap CroppedImage;
			try
			{
				if ((!this.validarAnverso() ? false : this.validarReverso()))
				{
					Bitmap sourceI = null;
					Bitmap sourceD = null;
					string fileName = "";
					int ancho = 0;
					int alto = 0;
					int div = 0;
					this.segmentar++;
					if ((this.FormularioPalma.RolledPrintsBIR == null ? true : !this.FormularioPalma.RolledPrintsBIR.Any<BIR>()))
					{
						List<BIR> vHuellas = new List<BIR>()
						{
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right Thumb"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right IndexFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right MiddleFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right RingFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Right LittleFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left Thumb"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left IndexFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left MiddleFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left RingFinger"
								}
							},
							new BIR()
							{
								BDBInfo = new BIRBDBInfo()
								{
									Type = "Finger",
									Subtype = "Left LittleFinger"
								}
							}
						};
						this.FormularioPalma.RolledPrintsBIR = vHuellas;
					}
					if (this.pbDactilarDerecho.Image != null)
					{
						sourceD = (Bitmap)this.pbDactilarDerecho.Image.Clone();
						ancho = sourceD.Width;
						alto = sourceD.Height;
						div = ancho / 5;
						int div_aux = 1;
						for (int dd = 1; dd <= 5; dd++)
						{
							SegmentacionRod form = new SegmentacionRod();
							DataTable dtResultado = new DataTable();
							section = new Rectangle(new Point(div_aux, 1), new System.Drawing.Size(div, alto));
							CroppedImage = this.CropImage(sourceD, section);
							fileName = string.Concat("dedo", dd.ToString(), "pbDactilarDer.bmp");
							div_aux = div * dd;
							if (dd == 1)
							{
								this.pictureBoxD1.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right Thumb").BDB = this.ser.ImageToByteArray(this.pictureBoxD1.Image);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel1, 0);
								}
								else
								{
									this.panelcolor(this.panel1, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (dd == 2)
							{
								this.pictureBoxD2.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right IndexFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel2, 0);
								}
								else
								{
									this.panelcolor(this.panel2, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (dd == 3)
							{
								this.pictureBoxD3.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right MiddleFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel3, 0);
								}
								else
								{
									this.panelcolor(this.panel3, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (dd == 4)
							{
								this.pictureBoxD4.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right RingFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel4, 0);
								}
								else
								{
									this.panelcolor(this.panel4, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (dd == 5)
							{
								this.pictureBoxD5.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Right LittleFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel5, 0);
								}
								else
								{
									this.panelcolor(this.panel5, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
						}
					}
					if (this.pbDactilarIzquierdo.Image != null)
					{
						sourceI = (Bitmap)this.pbDactilarIzquierdo.Image.Clone();
						ancho = sourceI.Width;
						alto = sourceI.Height;
						div = ancho / 5;
						int div_aux = 1;
						int d = 1;
						for (int id = 6; id <= 10; id++)
						{
							SegmentacionRod form = new SegmentacionRod();
							DataTable dtResultado = new DataTable();
							section = new Rectangle(new Point(div_aux, 1), new System.Drawing.Size(div, alto));
							CroppedImage = this.CropImage(sourceI, section);
							fileName = string.Concat("dedo", id.ToString(), "pbDactilarIzq.bmp");
							div_aux = div * d;
							d++;
							if (id == 6)
							{
								this.pictureBoxD6.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left Thumb").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel6, 0);
								}
								else
								{
									this.panelcolor(this.panel6, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (id == 7)
							{
								this.pictureBoxD7.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left IndexFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel7, 0);
								}
								else
								{
									this.panelcolor(this.panel7, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (id == 8)
							{
								this.pictureBoxD8.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left MiddleFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel8, 0);
								}
								else
								{
									this.panelcolor(this.panel8, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (id == 9)
							{
								this.pictureBoxD9.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left RingFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel9, 0);
								}
								else
								{
									this.panelcolor(this.panel9, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
							if (id == 10)
							{
								this.pictureBoxD10.Image = CroppedImage;
								this.FormularioPalma.RolledPrintsBIR.First<BIR>((BIR x) => x.BDBInfo.Subtype == "Left LittleFinger").BDB = this.ser.ImageToByteArray(CroppedImage);
								this.respuesta = form.enviarUno(CroppedImage, ref dtResultado);
								DataRow results = dtResultado.AsEnumerable().Where<DataRow>((DataRow myRow) => myRow.Field<string>("Posicion") == "11").FirstOrDefault<DataRow>();
								if (results == null)
								{
									this.panelcolor(this.panel10, 0);
								}
								else
								{
									this.panelcolor(this.panel10, Convert.ToInt32(results.Field<string>("Q")));
								}
							}
						}
					}
				}
				else
				{
					this.tabControl1.SelectedTab = this.tabPage2;
					MessageBox.Show("Debe completar el recorte del formulario Anverso.");
				}
			}
			catch (Exception exception)
			{
			}
		}

		private bool validarAnverso()
		{
			bool flag;
			bool var = true;
			if (this.pbanverso.Image == null)
			{
				flag = false;
			}
			else if (this.pbbiografia.Image == null)
			{
				flag = false;
			}
			else if (this.pbSimultaneaIzq.Image == null)
			{
				flag = false;
			}
			else if (this.pbSimultaneaDer.Image == null)
			{
				flag = false;
			}
			else if (this.pbDactilarIzq.Image == null)
			{
				flag = false;
			}
			else if (this.pbDactilarDer.Image != null)
			{
				flag = (this.pbPulgares.Image != null ? var : false);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private bool validarReverso()
		{
			bool flag;
			bool var = true;
			if (this.pbreverso.Image == null)
			{
				flag = false;
			}
			else if (this.pbpalmaizquierda.Image != null)
			{
				flag = (this.pbpalmaderecha.Image != null ? var : false);
			}
			else
			{
				flag = false;
			}
			return flag;
		}
	}
}