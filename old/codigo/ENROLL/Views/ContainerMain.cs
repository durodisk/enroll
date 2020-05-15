using Properties;
using ENROLL.Helpers;
using Datys.Enroll.Core;
using ENROLL.Helpers.ComplementoDatagrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace ENROLL
{
	public class vContainerMain : Form
	{

		private int rowIndex = 0;

		private int con_dgdat = 0;

		private int con_dgpen = 0;

		private int con_dgsub = 0;

		public static DataTable dt_enprogreso;

		public static DataTable dt_pendientes;

		public static DataTable dt_enviados;

		private static string link;

		private IContainer components = null;

		private Ribbon ribbon1;

		private RibbonTab ribbonTab1;

		private RibbonPanel ribbonPanel1;

		private RibbonButton ribtnActualizar;

		private RibbonButton ribtnNuevo;

		private RibbonButton ribtnEditar;

		private RibbonButton ribtnEliminar;

		private RibbonTab ribbonTab2;

		private PictureBox pictureBox1;

		private RibbonOrbOptionButton ribbonOrbOptionButton1;

		private DataGridViewProgressColumn dataGridViewProgressColumn1;

		private DataGridViewImageColumn dataGridViewImageColumn2;

		private RibbonTab ribbonTab3;

		private RibbonPanel ribbonPanel2;

		private RibbonButton ribbonEnviar;

		private RibbonTab ribbonTab4;

		private RibbonPanel ribbonPanel4;

		private RibbonButton ribbonButton1;

		private RibbonButton ribbonButton2;

		private RibbonButton ribbonButton3;

		private RibbonButton ribbonButton4;

		private DataGridView dgDatos;

		private DataGridView dgPendientes;

		private DataGridView dgSubidos;

		private GroupBox groupBox1;

		private LinkLabel linkSubidos;

		private LinkLabel linkpendientes;

		private LinkLabel linkProgreso;

		private SplitContainer splitContainer1;

		private RibbonOrbMenuItem ribbonOrbMenuItem1;

		private TableLayoutPanel tableLayoutPanel1;

		private TextBox textBox1;

		private Label label1;

		private RibbonButton ribtnArchivo;

		private RibbonButton ribbonButton6;

		private RibbonButtonList ribbonButtonList1;

		private RibbonPanel ribbonPanel5;

		private RibbonButton ribbonButton7;

		private PictureBox pictureBox2;

		private TableLayoutPanel tableLayoutPanel11;

		private Panel panel1;

		private RibbonPanel ribbonPanel6;

		private RibbonButton ribtnEditarAuto;

		private Button btnEnvio;

		private RibbonButton ribtnFisico;

		private DataGridViewImageColumn dataGridViewImageColumn1;

		private DataGridViewTextBoxColumn NomCompleto;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn FechaCreaciond;

		private DataGridViewTextBoxColumn Creadopord;

		private DataGridViewTextBoxColumn Modificadopord;

		private DataGridViewProgressColumn Progress;

		private DataGridViewTextBoxColumn ArchivoEpdN;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewImageColumn dataGridViewImageColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewProgressColumn dataGridViewProgressColumn2;

		private DataGridViewTextBoxColumn ArchivoEpdP;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewImageColumn dataGridViewImageColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewProgressColumn dataGridViewProgressColumn3;

		private DataGridViewTextBoxColumn ArchivoEpdE;

		private DataGridViewTextBoxColumn Estado;

		public static CapturedPerson PersonaCapturada
		{
			get;
			set;
		}

		public static string RutaEpd
		{
			get;
			set;
		}

		public static int UsuarioSeleccionado
		{
			get;
			set;
		}

		static vContainerMain()
		{
			vContainerMain.dt_enprogreso = null;
			vContainerMain.dt_pendientes = null;
			vContainerMain.dt_enviados = null;
			vContainerMain.link = "PRO";
		}

		public vContainerMain()
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			this.InitializeComponent();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void btnEnvio_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dgPendientes.CurrentCell.RowIndex > -1)
				{
					HelperSerializer ser = new HelperSerializer();
					DataGridViewRow vFila = this.dgPendientes.Rows[this.dgPendientes.CurrentCell.RowIndex];
					vContainerMain.RutaEpd = vFila.Cells[7].Value.ToString();
					string nombre = vFila.Cells[1].Value.ToString();
					string identificacion = vFila.Cells[2].Value.ToString();
					if (MessageBox.Show(string.Concat(new string[] { "Se sincronizara el registro:\n\nNombre Completo: ", nombre, "\nIdentificacion: ", identificacion, "\n\n¿Esta seguro de continuar?" }), "Mensaje", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						Sincronizacion.fPaquetes form = new Sincronizacion.fPaquetes();
						vContainerMain.PersonaCapturada = ser.DeserializeEpd(vContainerMain.RutaEpd);
						if (vContainerMain.PersonaCapturada.BasicData.UploadStatus.ToString() == "Pending")
						{
							form.Show();
							form.IniciarSincronizacionRegistro(vContainerMain.RutaEpd);
						}
						if (vContainerMain.PersonaCapturada.BasicData.UploadStatus.ToString() == "Done")
						{
							form.Show();
							form.IniciarSincronizacionUpdate(vContainerMain.RutaEpd);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		public Image byteArrayToImage(byte[] byteArrayIn)
		{
			return Image.FromStream(new MemoryStream(byteArrayIn));
		}

		private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				this.rowIndex = e.RowIndex;
				if (this.rowIndex > -1)
				{
					DataGridViewRow vFila = this.dgDatos.Rows[this.rowIndex];
					vContainerMain.RutaEpd = vFila.Cells["ArchivoEpdN"].Value.ToString();
					(new vEnroll()
					{
						MdiParent = base.ParentForm
					}).Show();
					base.Close();
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
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

		private void vContainerMain_Load(object sender, EventArgs e)
		{
			bool flag;
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				RibbonButton ribbonButton = this.ribbonButton6;
				bool num = false;
				//	bool flag1 = (bool)num;
				bool flag1 = num;
				this.ribtnEditarAuto.Visible = (bool)num;
				ribbonButton.Visible = flag1;
				RibbonButton ribbonButton1 = this.ribbonButton6;
				RibbonButton ribbonButton2 = this.ribtnEditarAuto;
				flag = (vLogin.usuario == "admin.policia" ? true : vLogin.usuario == "estanislao.castaya");
				flag1 = flag;
				ribbonButton2.Visible = flag;
				ribbonButton1.Visible = flag1;
				this.btnEnvio.Visible = false;
				vContainerMain.link = "PRO";
				this.dgDatos.Visible = true;
				this.dgPendientes.Visible = false;
				this.dgSubidos.Visible = false;
				this.LlenarGrilla();
				this.linkProgreso.LinkColor = Color.Blue;
				this.linkpendientes.LinkColor = Color.White;
				this.linkSubidos.LinkColor = Color.White;
				this.ribtnNuevo.Enabled = true;
				this.ribtnEditar.Enabled = true;
				this.ribtnEliminar.Enabled = true;
				if (this.con_dgdat == 0)
				{
					this.ribtnEditar.Enabled = false;
					this.ribtnEliminar.Enabled = false;
				}
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		public byte[] imageToByteArray(Image imageIn)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms, ImageFormat.Gif);
			return ms.ToArray();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(vContainerMain));
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
			this.ribbon1 = new Ribbon();
			this.ribbonTab1 = new RibbonTab();
			this.ribbonPanel1 = new RibbonPanel();
			this.ribtnActualizar = new RibbonButton();
			this.ribtnNuevo = new RibbonButton();
			this.ribtnEditar = new RibbonButton();
			this.ribtnEliminar = new RibbonButton();
			this.ribtnArchivo = new RibbonButton();
			this.ribbonPanel6 = new RibbonPanel();
			this.ribtnEditarAuto = new RibbonButton();
			this.ribbonTab4 = new RibbonTab();
			this.ribbonPanel4 = new RibbonPanel();
			this.ribbonButton3 = new RibbonButton();
			this.ribbonButton4 = new RibbonButton();
			this.ribbonButton6 = new RibbonButton();
			this.ribbonButtonList1 = new RibbonButtonList();
			this.ribbonTab3 = new RibbonTab();
			this.ribbonPanel2 = new RibbonPanel();
			this.ribbonEnviar = new RibbonButton();
			this.ribtnFisico = new RibbonButton();
			this.ribbonTab2 = new RibbonTab();
			this.ribbonPanel5 = new RibbonPanel();
			this.ribbonButton7 = new RibbonButton();
			this.ribbonOrbOptionButton1 = new RibbonOrbOptionButton();
			this.pictureBox1 = new PictureBox();
			this.dataGridViewProgressColumn1 = new DataGridViewProgressColumn();
			this.dataGridViewImageColumn2 = new DataGridViewImageColumn();
			this.ribbonButton1 = new RibbonButton();
			this.ribbonButton2 = new RibbonButton();
			this.dgDatos = new DataGridView();
			this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
			this.NomCompleto = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.FechaCreaciond = new DataGridViewTextBoxColumn();
			this.Creadopord = new DataGridViewTextBoxColumn();
			this.Modificadopord = new DataGridViewTextBoxColumn();
			this.Progress = new DataGridViewProgressColumn();
			this.ArchivoEpdN = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.dgPendientes = new DataGridView();
			this.dataGridViewImageColumn3 = new DataGridViewImageColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewProgressColumn2 = new DataGridViewProgressColumn();
			this.ArchivoEpdP = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.dgSubidos = new DataGridView();
			this.dataGridViewImageColumn4 = new DataGridViewImageColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.dataGridViewProgressColumn3 = new DataGridViewProgressColumn();
			this.ArchivoEpdE = new DataGridViewTextBoxColumn();
			this.Estado = new DataGridViewTextBoxColumn();
			this.groupBox1 = new GroupBox();
			this.linkSubidos = new LinkLabel();
			this.linkpendientes = new LinkLabel();
			this.linkProgreso = new LinkLabel();
			this.splitContainer1 = new SplitContainer();
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.panel1 = new Panel();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.btnEnvio = new Button();
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.ribbonOrbMenuItem1 = new RibbonOrbMenuItem();
			this.pictureBox2 = new PictureBox();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			((ISupportInitialize)this.dgDatos).BeginInit();
			((ISupportInitialize)this.dgPendientes).BeginInit();
			((ISupportInitialize)this.dgSubidos).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel11.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.ribbon1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.ribbon1.BackgroundImageLayout = ImageLayout.None;
			this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.ribbon1.Location = new Point(0, 0);
			this.ribbon1.Minimized = false;
			this.ribbon1.Name = "ribbon1";
			this.ribbon1.OrbDropDown.BackColor = Color.CadetBlue;
			this.ribbon1.OrbDropDown.BorderRoundness = 8;
			this.ribbon1.OrbDropDown.Location = new Point(0, 0);
			this.ribbon1.OrbDropDown.Name = "";
			this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
			this.ribbon1.OrbDropDown.TabIndex = 0;
			this.ribbon1.OrbStyle = RibbonOrbStyle.Office_2013;
			this.ribbon1.OrbText = "";
			this.ribbon1.OrbVisible = false;
			this.ribbon1.QuickAccessToolbar.Visible = false;
			this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9f);
			this.ribbon1.Size = new System.Drawing.Size(1375, 142);
			this.ribbon1.TabIndex = 0;
			this.ribbon1.Tabs.Add(this.ribbonTab1);
			this.ribbon1.Tabs.Add(this.ribbonTab4);
			this.ribbon1.Tabs.Add(this.ribbonTab3);
			this.ribbon1.Tabs.Add(this.ribbonTab2);
			this.ribbon1.TabSpacing = 4;
			this.ribbon1.Text = "ribbon1";
			this.ribbon1.ThemeColor = RibbonTheme.Blue;
			this.ribbonTab1.Name = "ribbonTab1";
			this.ribbonTab1.Panels.Add(this.ribbonPanel1);
			this.ribbonTab1.Panels.Add(this.ribbonPanel6);
			this.ribbonTab1.Text = "Acciones";
			this.ribbonPanel1.Items.Add(this.ribtnActualizar);
			this.ribbonPanel1.Items.Add(this.ribtnNuevo);
			this.ribbonPanel1.Items.Add(this.ribtnEditar);
			this.ribbonPanel1.Items.Add(this.ribtnEliminar);
			this.ribbonPanel1.Items.Add(this.ribtnArchivo);
			this.ribbonPanel1.Name = "ribbonPanel1";
			this.ribbonPanel1.Text = "";
			this.ribtnActualizar.Image = (Image)resources.GetObject("ribtnActualizar.Image");
			this.ribtnActualizar.LargeImage = (Image)resources.GetObject("ribtnActualizar.LargeImage");
			this.ribtnActualizar.Name = "ribtnActualizar";
			this.ribtnActualizar.SmallImage = (Image)resources.GetObject("ribtnActualizar.SmallImage");
			this.ribtnActualizar.Text = "Actualizar";
			this.ribtnActualizar.Click += new EventHandler(this.ribtnActualizar_Click);
			this.ribtnNuevo.Image = (Image)resources.GetObject("ribtnNuevo.Image");
			this.ribtnNuevo.LargeImage = (Image)resources.GetObject("ribtnNuevo.LargeImage");
			this.ribtnNuevo.Name = "ribtnNuevo";
			this.ribtnNuevo.SmallImage = (Image)resources.GetObject("ribtnNuevo.SmallImage");
			this.ribtnNuevo.Text = "Nuevo";
			this.ribtnNuevo.Click += new EventHandler(this.ribtnNuevo_Click);
			this.ribtnEditar.Image = (Image)resources.GetObject("ribtnEditar.Image");
			this.ribtnEditar.LargeImage = (Image)resources.GetObject("ribtnEditar.LargeImage");
			this.ribtnEditar.Name = "ribtnEditar";
			this.ribtnEditar.SmallImage = (Image)resources.GetObject("ribtnEditar.SmallImage");
			this.ribtnEditar.Text = "Editar";
			this.ribtnEditar.Click += new EventHandler(this.ribtnEditar_Click);
			this.ribtnEliminar.Image = (Image)resources.GetObject("ribtnEliminar.Image");
			this.ribtnEliminar.LargeImage = (Image)resources.GetObject("ribtnEliminar.LargeImage");
			this.ribtnEliminar.Name = "ribtnEliminar";
			this.ribtnEliminar.SmallImage = (Image)resources.GetObject("ribtnEliminar.SmallImage");
			this.ribtnEliminar.Text = "Eliminar";
			this.ribtnEliminar.Click += new EventHandler(this.ribtnEliminar_Click);
			this.ribtnArchivo.Image = (Image)resources.GetObject("ribtnArchivo.Image");
			this.ribtnArchivo.LargeImage = (Image)resources.GetObject("ribtnArchivo.LargeImage");
			this.ribtnArchivo.Name = "ribtnArchivo";
			this.ribtnArchivo.SmallImage = (Image)resources.GetObject("ribtnArchivo.SmallImage");
			this.ribtnArchivo.Text = "Archivo Fisico";
			this.ribtnArchivo.Click += new EventHandler(this.ribbonButton5_Click);
			this.ribbonPanel6.Items.Add(this.ribtnEditarAuto);
			this.ribbonPanel6.Name = "ribbonPanel6";
			this.ribbonPanel6.Text = "";
			this.ribtnEditarAuto.Image = (Image)resources.GetObject("ribtnEditarAuto.Image");
			this.ribtnEditarAuto.LargeImage = (Image)resources.GetObject("ribtnEditarAuto.LargeImage");
			this.ribtnEditarAuto.Name = "ribtnEditarAuto";
			this.ribtnEditarAuto.SmallImage = (Image)resources.GetObject("ribtnEditarAuto.SmallImage");
			this.ribtnEditarAuto.Text = "Autorizar Edicion";
			this.ribtnEditarAuto.Click += new EventHandler(this.ribbonButton8_Click);
			this.ribbonTab4.Name = "ribbonTab4";
			this.ribbonTab4.Panels.Add(this.ribbonPanel4);
			this.ribbonTab4.Text = "Usuarios";
			this.ribbonPanel4.Items.Add(this.ribbonButton3);
			this.ribbonPanel4.Items.Add(this.ribbonButton4);
			this.ribbonPanel4.Items.Add(this.ribbonButton6);
			this.ribbonPanel4.Name = "ribbonPanel4";
			this.ribbonPanel4.Text = "";
			this.ribbonButton3.Image = (Image)resources.GetObject("ribbonButton3.Image");
			this.ribbonButton3.LargeImage = (Image)resources.GetObject("ribbonButton3.LargeImage");
			this.ribbonButton3.Name = "ribbonButton3";
			this.ribbonButton3.SmallImage = (Image)resources.GetObject("ribbonButton3.SmallImage");
			this.ribbonButton3.Text = "Nuevo usuario";
			this.ribbonButton3.Click += new EventHandler(this.ribbonButton3_Click);
			this.ribbonButton4.Image = (Image)resources.GetObject("ribbonButton4.Image");
			this.ribbonButton4.LargeImage = (Image)resources.GetObject("ribbonButton4.LargeImage");
			this.ribbonButton4.Name = "ribbonButton4";
			this.ribbonButton4.SmallImage = (Image)resources.GetObject("ribbonButton4.SmallImage");
			this.ribbonButton4.Text = "Cambiar Contraseña";
			this.ribbonButton4.Click += new EventHandler(this.ribbonButton4_Click);
			this.ribbonButton6.DropDownItems.Add(this.ribbonButtonList1);
			this.ribbonButton6.Image = (Image)resources.GetObject("ribbonButton6.Image");
			this.ribbonButton6.LargeImage = (Image)resources.GetObject("ribbonButton6.LargeImage");
			this.ribbonButton6.Name = "ribbonButton6";
			this.ribbonButton6.SmallImage = (Image)resources.GetObject("ribbonButton6.SmallImage");
			this.ribbonButton6.Text = "Restaurar Contraseña";
			this.ribbonButton6.TextAlignment = RibbonItem.RibbonItemTextAlignment.Center;
			this.ribbonButton6.Click += new EventHandler(this.ribbonButton6_Click);
			this.ribbonButtonList1.ButtonsSizeMode = RibbonElementSizeMode.Large;
			this.ribbonButtonList1.FlowToBottom = false;
			this.ribbonButtonList1.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
			this.ribbonButtonList1.Name = "ribbonButtonList1";
			this.ribbonButtonList1.Text = "ribbonButtonList1";
			this.ribbonTab3.Name = "ribbonTab3";
			this.ribbonTab3.Panels.Add(this.ribbonPanel2);
			this.ribbonTab3.Text = "Sincronizacion";
			this.ribbonPanel2.Items.Add(this.ribbonEnviar);
			this.ribbonPanel2.Items.Add(this.ribtnFisico);
			this.ribbonPanel2.Name = "ribbonPanel2";
			this.ribbonPanel2.Text = "";
			this.ribbonEnviar.Image = (Image)resources.GetObject("ribbonEnviar.Image");
			this.ribbonEnviar.LargeImage = (Image)resources.GetObject("ribbonEnviar.LargeImage");
			this.ribbonEnviar.Name = "ribbonEnviar";
			this.ribbonEnviar.SmallImage = (Image)resources.GetObject("ribbonEnviar.SmallImage");
			this.ribbonEnviar.Text = "Enviar  (Sincronizacion)";
			this.ribbonEnviar.Click += new EventHandler(this.ribbonEnviar_Click);
			this.ribtnFisico.Image = (Image)resources.GetObject("ribtnFisico.Image");
			this.ribtnFisico.LargeImage = (Image)resources.GetObject("ribtnFisico.LargeImage");
			this.ribtnFisico.Name = "ribtnFisico";
			this.ribtnFisico.SmallImage = (Image)resources.GetObject("ribtnFisico.SmallImage");
			this.ribtnFisico.Text = "Ver";
			this.ribtnFisico.Visible = false;
			this.ribtnFisico.Click += new EventHandler(this.ribtnFisico_Click);
			this.ribbonTab2.Name = "ribbonTab2";
			this.ribbonTab2.Panels.Add(this.ribbonPanel5);
			this.ribbonTab2.Text = "Ayuda";
			this.ribbonPanel5.Items.Add(this.ribbonButton7);
			this.ribbonPanel5.Name = "ribbonPanel5";
			this.ribbonPanel5.Text = "--------------------";
			this.ribbonButton7.Image = (Image)resources.GetObject("ribbonButton7.Image");
			this.ribbonButton7.LargeImage = (Image)resources.GetObject("ribbonButton7.LargeImage");
			this.ribbonButton7.Name = "ribbonButton7";
			this.ribbonButton7.SmallImage = (Image)resources.GetObject("ribbonButton7.SmallImage");
			this.ribbonButton7.Style = RibbonButtonStyle.SplitDropDown;
			this.ribbonButton7.Text = "Manual de Usuario";
			this.ribbonButton7.Click += new EventHandler(this.ribbonButton7_Click);
			this.ribbonOrbOptionButton1.Image = (Image)resources.GetObject("ribbonOrbOptionButton1.Image");
			this.ribbonOrbOptionButton1.LargeImage = (Image)resources.GetObject("ribbonOrbOptionButton1.LargeImage");
			this.ribbonOrbOptionButton1.Name = "ribbonOrbOptionButton1";
			this.ribbonOrbOptionButton1.SmallImage = (Image)resources.GetObject("ribbonOrbOptionButton1.SmallImage");
			this.ribbonOrbOptionButton1.Text = "ribbonOrbOptionButton1";
			this.pictureBox1.Image = Resources._029_app;

			this.pictureBox1.Location = new Point(1061, 66);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle1.ForeColor = Color.FromArgb(0, 64, 0);
			dataGridViewCellStyle1.NullValue = null;
			this.dataGridViewProgressColumn1.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewProgressColumn1.HeaderText = "Completado [%]";
			this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
			this.dataGridViewProgressColumn1.ProgressBarColor = Color.Cyan;
			this.dataGridViewProgressColumn1.ReadOnly = true;
			this.dataGridViewImageColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.dataGridViewImageColumn2.DataPropertyName = "Foto";
			this.dataGridViewImageColumn2.FillWeight = 160f;
			this.dataGridViewImageColumn2.HeaderText = "Foto";
			this.dataGridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Stretch;
			this.dataGridViewImageColumn2.MinimumWidth = 2;
			this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
			this.ribbonButton1.Image = (Image)resources.GetObject("ribbonButton1.Image");
			this.ribbonButton1.LargeImage = (Image)resources.GetObject("ribbonButton1.LargeImage");
			this.ribbonButton1.Name = "ribbonButton1";
			this.ribbonButton1.SmallImage = (Image)resources.GetObject("ribbonButton1.SmallImage");
			this.ribbonButton1.Text = "Nuevo";
			this.ribbonButton2.Image = (Image)resources.GetObject("ribbonButton2.Image");
			this.ribbonButton2.LargeImage = (Image)resources.GetObject("ribbonButton2.LargeImage");
			this.ribbonButton2.Name = "ribbonButton2";
			this.ribbonButton2.SmallImage = (Image)resources.GetObject("ribbonButton2.SmallImage");
			this.ribbonButton2.Text = "Nuevo";
			this.dgDatos.AllowUserToAddRows = false;
			this.dgDatos.AllowUserToDeleteRows = false;
			this.dgDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgDatos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.dgDatos.BackgroundColor = Color.White;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dgDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgDatos.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewImageColumn1, this.NomCompleto, this.Column2, this.FechaCreaciond, this.Creadopord, this.Modificadopord, this.Progress, this.ArchivoEpdN, this.Column3 });
			this.dgDatos.Cursor = Cursors.Hand;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
			this.dgDatos.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgDatos.Dock = DockStyle.Fill;
			this.dgDatos.GridColor = Color.FromArgb(0, 192, 192);
			this.dgDatos.Location = new Point(0, 0);
			this.dgDatos.Name = "dgDatos";
			this.dgDatos.ReadOnly = true;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Control;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.Desktop;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			this.dgDatos.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dgDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgDatos.Size = new System.Drawing.Size(1168, 509);
			this.dgDatos.TabIndex = 2;
			this.dgDatos.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
			this.dataGridViewImageColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewImageColumn1.DataPropertyName = "Foto";
			this.dataGridViewImageColumn1.FillWeight = 6.59202f;
			this.dataGridViewImageColumn1.HeaderText = "Foto";
			this.dataGridViewImageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;
			this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
			this.dataGridViewImageColumn1.ReadOnly = true;
			this.dataGridViewImageColumn1.Resizable = DataGridViewTriState.False;
			this.dataGridViewImageColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
			this.NomCompleto.DataPropertyName = "Nombre";
			this.NomCompleto.FillWeight = 22.42674f;
			this.NomCompleto.HeaderText = "Nombre Completo";
			this.NomCompleto.Name = "NomCompleto";
			this.NomCompleto.ReadOnly = true;
			this.NomCompleto.Resizable = DataGridViewTriState.False;
			this.Column2.DataPropertyName = "Identificacion";
			this.Column2.FillWeight = 15f;
			this.Column2.HeaderText = "Identificacion";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.FechaCreaciond.DataPropertyName = "Fecha";
			this.FechaCreaciond.FillWeight = 15f;
			this.FechaCreaciond.HeaderText = "Fecha de Creacion";
			this.FechaCreaciond.Name = "FechaCreaciond";
			this.FechaCreaciond.ReadOnly = true;
			this.FechaCreaciond.Resizable = DataGridViewTriState.False;
			this.Creadopord.DataPropertyName = "Creado";
			this.Creadopord.FillWeight = 15f;
			this.Creadopord.HeaderText = "Creado Por";
			this.Creadopord.Name = "Creadopord";
			this.Creadopord.ReadOnly = true;
			this.Creadopord.Resizable = DataGridViewTriState.False;
			this.Modificadopord.DataPropertyName = "Modificado";
			this.Modificadopord.FillWeight = 15f;
			this.Modificadopord.HeaderText = "Modificado Por";
			this.Modificadopord.Name = "Modificadopord";
			this.Modificadopord.ReadOnly = true;
			this.Modificadopord.Resizable = DataGridViewTriState.False;
			this.Progress.DataPropertyName = "Completado";
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = Color.FromArgb(0, 64, 0);
			dataGridViewCellStyle3.NullValue = null;
			this.Progress.DefaultCellStyle = dataGridViewCellStyle3;
			this.Progress.FillWeight = 33.56382f;
			this.Progress.HeaderText = "Completado [%]";
			this.Progress.Name = "Progress";
			this.Progress.ProgressBarColor = Color.Cyan;
			this.Progress.ReadOnly = true;
			this.Progress.Resizable = DataGridViewTriState.False;
			this.ArchivoEpdN.DataPropertyName = "ArchivoEpd";
			this.ArchivoEpdN.HeaderText = "ArchivoEpd";
			this.ArchivoEpdN.Name = "ArchivoEpdN";
			this.ArchivoEpdN.ReadOnly = true;
			this.ArchivoEpdN.Visible = false;
			this.Column3.DataPropertyName = "Estado";
			this.Column3.FillWeight = 22.42674f;
			this.Column3.HeaderText = "Estado";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Visible = false;
			this.dgPendientes.AllowUserToAddRows = false;
			this.dgPendientes.AllowUserToDeleteRows = false;
			this.dgPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgPendientes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.dgPendientes.BackgroundColor = Color.White;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle6.SelectionForeColor = Color.Black;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgPendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgPendientes.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewImageColumn3, this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.dataGridViewProgressColumn2, this.ArchivoEpdP, this.Column4 });
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle8.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
			this.dgPendientes.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgPendientes.Dock = DockStyle.Fill;
			this.dgPendientes.GridColor = Color.FromArgb(0, 192, 192);
			this.dgPendientes.Location = new Point(0, 0);
			this.dgPendientes.Name = "dgPendientes";
			this.dgPendientes.ReadOnly = true;
			dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle9.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
			this.dgPendientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgPendientes.Size = new System.Drawing.Size(1168, 509);
			this.dgPendientes.TabIndex = 3;
			this.dataGridViewImageColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewImageColumn3.DataPropertyName = "Foto";
			this.dataGridViewImageColumn3.FillWeight = 6.59202f;
			this.dataGridViewImageColumn3.HeaderText = "Foto";
			this.dataGridViewImageColumn3.ImageLayout = DataGridViewImageCellLayout.Zoom;
			this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
			this.dataGridViewImageColumn3.ReadOnly = true;
			this.dataGridViewImageColumn3.Resizable = DataGridViewTriState.False;
			this.dataGridViewImageColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Nombre";
			this.dataGridViewTextBoxColumn1.FillWeight = 22.42674f;
			this.dataGridViewTextBoxColumn1.HeaderText = "Nombre Completo";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Identificacion";
			this.dataGridViewTextBoxColumn2.FillWeight = 15f;
			this.dataGridViewTextBoxColumn2.HeaderText = "Identificacion";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Fecha";
			this.dataGridViewTextBoxColumn3.FillWeight = 15f;
			this.dataGridViewTextBoxColumn3.HeaderText = "Fecha de Creacion";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Resizable = DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Creado";
			this.dataGridViewTextBoxColumn4.FillWeight = 15f;
			this.dataGridViewTextBoxColumn4.HeaderText = "Creado Por";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn5.DataPropertyName = "Modificado";
			this.dataGridViewTextBoxColumn5.FillWeight = 15f;
			this.dataGridViewTextBoxColumn5.HeaderText = "Modificado Por";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Resizable = DataGridViewTriState.False;
			this.dataGridViewProgressColumn2.DataPropertyName = "Completado";
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle7.ForeColor = Color.FromArgb(0, 64, 0);
			dataGridViewCellStyle7.NullValue = null;
			this.dataGridViewProgressColumn2.DefaultCellStyle = dataGridViewCellStyle7;
			this.dataGridViewProgressColumn2.FillWeight = 33.56382f;
			this.dataGridViewProgressColumn2.HeaderText = "Completado [%]";
			this.dataGridViewProgressColumn2.Name = "dataGridViewProgressColumn2";
			this.dataGridViewProgressColumn2.ProgressBarColor = Color.Cyan;
			this.dataGridViewProgressColumn2.ReadOnly = true;
			this.dataGridViewProgressColumn2.Resizable = DataGridViewTriState.False;
			this.ArchivoEpdP.DataPropertyName = "ArchivoEpd";
			this.ArchivoEpdP.HeaderText = "ArchivoEpd";
			this.ArchivoEpdP.Name = "ArchivoEpdP";
			this.ArchivoEpdP.ReadOnly = true;
			this.ArchivoEpdP.Visible = false;
			this.Column4.DataPropertyName = "Estado";
			this.Column4.HeaderText = "Estado";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.Column4.Visible = false;
			this.dgSubidos.AllowUserToAddRows = false;
			this.dgSubidos.AllowUserToDeleteRows = false;
			this.dgSubidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgSubidos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.dgSubidos.BackgroundColor = Color.White;
			dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = Color.FromArgb(192, 255, 255);
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle10.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
			this.dgSubidos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.dgSubidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgSubidos.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewImageColumn4, this.dataGridViewTextBoxColumn6, this.Column1, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn9, this.dataGridViewProgressColumn3, this.ArchivoEpdE, this.Estado });
			dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = SystemColors.Window;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle12.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle12.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle12.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle12.WrapMode = DataGridViewTriState.False;
			this.dgSubidos.DefaultCellStyle = dataGridViewCellStyle12;
			this.dgSubidos.Dock = DockStyle.Fill;
			this.dgSubidos.GridColor = Color.FromArgb(0, 192, 192);
			this.dgSubidos.Location = new Point(0, 0);
			this.dgSubidos.Name = "dgSubidos";
			this.dgSubidos.ReadOnly = true;
			dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle13.BackColor = SystemColors.Control;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle13.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle13.SelectionBackColor = Color.FromArgb(0, 192, 192);
			dataGridViewCellStyle13.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle13.WrapMode = DataGridViewTriState.True;
			this.dgSubidos.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
			this.dgSubidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgSubidos.Size = new System.Drawing.Size(1168, 509);
			this.dgSubidos.TabIndex = 4;
			this.dataGridViewImageColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewImageColumn4.DataPropertyName = "Foto";
			this.dataGridViewImageColumn4.FillWeight = 6.59202f;
			this.dataGridViewImageColumn4.HeaderText = "Foto";
			this.dataGridViewImageColumn4.ImageLayout = DataGridViewImageCellLayout.Zoom;
			this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
			this.dataGridViewImageColumn4.ReadOnly = true;
			this.dataGridViewImageColumn4.Resizable = DataGridViewTriState.False;
			this.dataGridViewImageColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
			this.dataGridViewTextBoxColumn6.DataPropertyName = "Nombre";
			this.dataGridViewTextBoxColumn6.FillWeight = 22.42674f;
			this.dataGridViewTextBoxColumn6.HeaderText = "Nombre Completo";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Resizable = DataGridViewTriState.False;
			this.Column1.DataPropertyName = "Identificacion";
			this.Column1.FillWeight = 15f;
			this.Column1.HeaderText = "Identificacion";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.DataPropertyName = "Fecha";
			this.dataGridViewTextBoxColumn7.FillWeight = 15f;
			this.dataGridViewTextBoxColumn7.HeaderText = "Fecha de Creacion";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Resizable = DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn8.DataPropertyName = "Creado";
			this.dataGridViewTextBoxColumn8.FillWeight = 15f;
			this.dataGridViewTextBoxColumn8.HeaderText = "Creado Por";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Resizable = DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn9.DataPropertyName = "Modificado";
			this.dataGridViewTextBoxColumn9.FillWeight = 15f;
			this.dataGridViewTextBoxColumn9.HeaderText = "Modificado Por";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Resizable = DataGridViewTriState.False;
			this.dataGridViewProgressColumn3.DataPropertyName = "Completado";
			dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle11.ForeColor = Color.FromArgb(0, 64, 0);
			dataGridViewCellStyle11.NullValue = null;
			this.dataGridViewProgressColumn3.DefaultCellStyle = dataGridViewCellStyle11;
			this.dataGridViewProgressColumn3.FillWeight = 25f;
			this.dataGridViewProgressColumn3.HeaderText = "Completado [%]";
			this.dataGridViewProgressColumn3.Name = "dataGridViewProgressColumn3";
			this.dataGridViewProgressColumn3.ProgressBarColor = Color.Cyan;
			this.dataGridViewProgressColumn3.ReadOnly = true;
			this.dataGridViewProgressColumn3.Resizable = DataGridViewTriState.False;
			this.ArchivoEpdE.DataPropertyName = "ArchivoEpd";
			this.ArchivoEpdE.HeaderText = "ArchivoEpd";
			this.ArchivoEpdE.Name = "ArchivoEpdE";
			this.ArchivoEpdE.ReadOnly = true;
			this.ArchivoEpdE.Visible = false;
			this.Estado.DataPropertyName = "Estado";
			this.Estado.FillWeight = 40f;
			this.Estado.HeaderText = "Estado ABIS";
			this.Estado.Name = "Estado";
			this.Estado.ReadOnly = true;
			this.groupBox1.AutoSize = true;
			this.groupBox1.BackColor = Color.FromArgb(48, 63, 105);
			this.groupBox1.Controls.Add(this.linkSubidos);
			this.groupBox1.Controls.Add(this.linkpendientes);
			this.groupBox1.Controls.Add(this.linkProgreso);
			this.groupBox1.Dock = DockStyle.Fill;
			this.groupBox1.FlatStyle = FlatStyle.Flat;
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188, 553);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ENROLAMIENTO";
			this.linkSubidos.ActiveLinkColor = Color.FromArgb(0, 192, 192);
			this.linkSubidos.AutoSize = true;
			this.linkSubidos.BackColor = Color.FromArgb(48, 63, 105);
			this.linkSubidos.Cursor = Cursors.WaitCursor;
			this.linkSubidos.DisabledLinkColor = Color.White;
			this.linkSubidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.linkSubidos.LinkColor = Color.White;
			this.linkSubidos.Location = new Point(1, 108);
			this.linkSubidos.Name = "linkSubidos";
			this.linkSubidos.Size = new System.Drawing.Size(67, 15);
			this.linkSubidos.TabIndex = 5;
			this.linkSubidos.TabStop = true;
			this.linkSubidos.Text = "ENVIADOS";
			this.linkSubidos.UseWaitCursor = true;
			this.linkSubidos.VisitedLinkColor = Color.FromArgb(0, 192, 192);
			this.linkSubidos.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkSubidos_LinkClicked);
			this.linkpendientes.ActiveLinkColor = Color.FromArgb(0, 192, 192);
			this.linkpendientes.AutoSize = true;
			this.linkpendientes.BackColor = Color.FromArgb(48, 63, 105);
			this.linkpendientes.Cursor = Cursors.WaitCursor;
			this.linkpendientes.DisabledLinkColor = Color.White;
			this.linkpendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.linkpendientes.LinkColor = Color.White;
			this.linkpendientes.Location = new Point(1, 68);
			this.linkpendientes.Name = "linkpendientes";
			this.linkpendientes.Size = new System.Drawing.Size(143, 15);
			this.linkpendientes.TabIndex = 4;
			this.linkpendientes.TabStop = true;
			this.linkpendientes.Text = "PENDIENTES DE ENVIO";
			this.linkpendientes.UseWaitCursor = true;
			this.linkpendientes.VisitedLinkColor = Color.FromArgb(0, 192, 192);
			this.linkpendientes.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkpendientes_LinkClicked);
			this.linkProgreso.ActiveLinkColor = Color.FromArgb(0, 192, 192);
			this.linkProgreso.AutoSize = true;
			this.linkProgreso.BackColor = Color.FromArgb(48, 63, 105);
			this.linkProgreso.Cursor = Cursors.WaitCursor;
			this.linkProgreso.DisabledLinkColor = Color.White;
			this.linkProgreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.linkProgreso.LinkColor = Color.White;
			this.linkProgreso.Location = new Point(1, 33);
			this.linkProgreso.Name = "linkProgreso";
			this.linkProgreso.Size = new System.Drawing.Size(96, 15);
			this.linkProgreso.TabIndex = 3;
			this.linkProgreso.TabStop = true;
			this.linkProgreso.Text = "EN PROGRESO";
			this.linkProgreso.UseWaitCursor = true;
			this.linkProgreso.VisitedLinkColor = Color.FromArgb(0, 192, 192);
			this.linkProgreso.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkProgreso_LinkClicked);
			this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.splitContainer1.FixedPanel = FixedPanel.Panel1;
			this.splitContainer1.Location = new Point(0, 131);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1MinSize = 130;
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel11);
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel2MinSize = 100;
			this.splitContainer1.Size = new System.Drawing.Size(1375, 553);
			this.splitContainer1.SplitterDistance = 188;
			this.splitContainer1.TabIndex = 3;
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.ColumnCount = 1;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel11.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel11.Location = new Point(6, 41);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(1174, 515);
			this.tableLayoutPanel11.TabIndex = 32;
			this.panel1.Controls.Add(this.dgDatos);
			this.panel1.Controls.Add(this.dgSubidos);
			this.panel1.Controls.Add(this.dgPendientes);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1168, 509);
			this.panel1.TabIndex = 0;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.09451f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.14871f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.75677f));
			this.tableLayoutPanel1.Controls.Add(this.btnEnvio, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Top;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1183, 38);
			this.tableLayoutPanel1.TabIndex = 5;
			this.btnEnvio.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnEnvio.BackColor = Color.FromArgb(16, 103, 242);
			this.btnEnvio.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnEnvio.FlatStyle = FlatStyle.Flat;
			this.btnEnvio.ForeColor = Color.White;
			this.btnEnvio.Location = new Point(1028, 5);
			this.btnEnvio.Name = "btnEnvio";
			this.btnEnvio.Size = new System.Drawing.Size(152, 30);
			this.btnEnvio.TabIndex = 6;
			this.btnEnvio.Text = "ENVIAR ENROLAMIENTO";
			this.btnEnvio.UseVisualStyleBackColor = false;
			this.btnEnvio.Click += new EventHandler(this.btnEnvio_Click);
			this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(3, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "BUSQUEDA POR:";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.textBox1.Location = new Point(134, 15);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(364, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
			this.ribbonOrbMenuItem1.DropDownArrowDirection = RibbonArrowDirection.Left;
			this.ribbonOrbMenuItem1.Image = (Image)resources.GetObject("ribbonOrbMenuItem1.Image");
			this.ribbonOrbMenuItem1.LargeImage = (Image)resources.GetObject("ribbonOrbMenuItem1.LargeImage");
			this.ribbonOrbMenuItem1.Name = "ribbonOrbMenuItem1";
			this.ribbonOrbMenuItem1.SmallImage = (Image)resources.GetObject("ribbonOrbMenuItem1.SmallImage");
			this.ribbonOrbMenuItem1.Text = "ribbonOrbMenuItem1";
			this.pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox2.BackColor = Color.White;
			this.pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new Point(1275, 30);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(100, 100);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 4;
			this.pictureBox2.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(1375, 721);
			base.Controls.Add(this.pictureBox2);
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.ribbon1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.Name = "vContainerMain";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.vContainerMain_Load);
			((ISupportInitialize)this.pictureBox1).EndInit();
			((ISupportInitialize)this.dgDatos).EndInit();
			((ISupportInitialize)this.dgPendientes).EndInit();
			((ISupportInitialize)this.dgSubidos).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel11.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void linkpendientes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				this.btnEnvio.Visible = true;
				this.dgDatos.Visible = false;
				this.dgPendientes.Visible = true;
				this.dgSubidos.Visible = false;
				vContainerMain.link = "PEN";
				this.LlenarGrilla();
				this.linkProgreso.LinkColor = Color.White;
				this.linkpendientes.LinkColor = Color.Blue;
				this.linkSubidos.LinkColor = Color.White;
				this.ribtnNuevo.Enabled = false;
				this.ribtnEditar.Enabled = false;
				this.ribtnEliminar.Enabled = false;
				this.ribtnEditarAuto.Enabled = true;
				if (this.con_dgpen == 0)
				{
					MessageBox.Show("No existen Registros");
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void linkProgreso_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				this.btnEnvio.Visible = false;
				vContainerMain.link = "PRO";
				this.dgDatos.Visible = true;
				this.dgPendientes.Visible = false;
				this.dgSubidos.Visible = false;
				this.LlenarGrilla();
				this.linkProgreso.LinkColor = Color.Blue;
				this.linkpendientes.LinkColor = Color.White;
				this.linkSubidos.LinkColor = Color.White;
				this.ribtnNuevo.Enabled = true;
				this.ribtnEditar.Enabled = true;
				this.ribtnEliminar.Enabled = true;
				this.ribtnEditarAuto.Enabled = false;
				if (this.con_dgdat == 0)
				{
					this.ribtnEditar.Enabled = false;
					this.ribtnEliminar.Enabled = false;
				}
				if (this.con_dgdat == 0)
				{
					MessageBox.Show("No existen Registros");
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void linkSubidos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				this.btnEnvio.Visible = false;
				this.dgDatos.Visible = false;
				this.dgPendientes.Visible = false;
				this.dgSubidos.Visible = true;
				vContainerMain.link = "ENV";
				this.LlenarGrilla();
				this.linkProgreso.LinkColor = Color.White;
				this.linkpendientes.LinkColor = Color.White;
				this.linkSubidos.LinkColor = Color.Blue;
				this.ribtnNuevo.Enabled = false;
				this.ribtnEditar.Enabled = false;
				this.ribtnEliminar.Enabled = false;
				this.ribtnEditarAuto.Enabled = true;
				if (this.con_dgsub == 0)
				{
					MessageBox.Show("No existen Registros");
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}

		}

		public static void Var_dump(object obj)
		{
			Console.WriteLine("{0,-18} {1}", "Name", "Value");
			string ln = @"-------------------------------------  
               ----------------------------";
			Console.WriteLine(ln);

			Type t = obj.GetType();
			PropertyInfo[] props = t.GetProperties();

			for (int i = 0; i < props.Length; i++)
			{
				try
				{
					Console.WriteLine("{0,-18} {1}",
						  props[i].Name, props[i].GetValue(obj, null));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			Console.WriteLine();
		}

		private void LlenarGrilla()
		{
			UploadStatus uploadStatus;
			bool str;
			bool flag;
			bool str1;
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				string[] vColArchivoEpd = Directory.GetFiles("Enrolls\\", "*.epd");
				DataTable vPersonas = new DataTable();
				vPersonas.Columns.Add("Foto", Type.GetType("System.Byte[]"));
				vPersonas.Columns.Add("Nombre", Type.GetType("System.String"));
				vPersonas.Columns.Add("Identificacion", Type.GetType("System.String"));
				vPersonas.Columns.Add("Fecha", Type.GetType("System.String"));
				vPersonas.Columns.Add("Creado", Type.GetType("System.String"));
				vPersonas.Columns.Add("Modificado", Type.GetType("System.String"));
				vPersonas.Columns.Add("Completado", Type.GetType("System.String"));
				vPersonas.Columns.Add("ArchivoEpd", Type.GetType("System.String"));
				vPersonas.Columns.Add("Estado", Type.GetType("System.String"));
				vContainerMain.dt_enprogreso = vPersonas.Clone();
				vContainerMain.dt_pendientes = vPersonas.Clone();
				vContainerMain.dt_enviados = vPersonas.Clone();
				this.con_dgdat = 0;
				this.con_dgpen = 0;
				this.con_dgsub = 0;
				string[] strArrays = vColArchivoEpd;
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string vArchivoEpd = strArrays[i];
					CapturedPerson PersonaCapturada = (new ENROLL.Helpers.HelperSerializer()).DeserializeEpd(vArchivoEpd);
					if (PersonaCapturada.BasicData.PhotoArr != null)
					{
						this.pictureBox1.Image = this.byteArrayToImage(PersonaCapturada.BasicData.PhotoArr);
					}
					else
					{
						this.pictureBox1.Image = Resources._029_app;
					}
					MemoryStream ms = new MemoryStream();
					this.pictureBox1.Image.Save(ms, this.pictureBox1.Image.RawFormat);
					byte[] img = ms.ToArray();
					float porcen1 = PersonaCapturada.BasicData.BiographicCompleted;
					float porcen2 = PersonaCapturada.BasicData.FaceCompleted;
					float porcen3 = PersonaCapturada.BasicData.FingerPalmCompleted;
					float porcen4 = PersonaCapturada.BasicData.TattooMarkCompleted;
					float porgral = (porcen1 + porcen2 + porcen3 + porcen4) / 4f;
					int GeneralCompleted = (int)(porgral * 100f);
					string status = PersonaCapturada.BasicData.StatusMessage;
					string identificacion = PersonaCapturada.OfflinePerson.Identities[0].Identification;
					if (PersonaCapturada.BasicData.UploadStatus.ToString() == "InProgress")
					{
						str = true;
					}
					else
					{
						uploadStatus = PersonaCapturada.BasicData.UploadStatus;
						str = uploadStatus.ToString() == "ManualVerification";
					}
					if (str)
					{
						this.con_dgdat++;
						Console.WriteLine(vContainerMain.link);
						if (vContainerMain.link == "PRO")
						{
							vContainerMain.dt_enprogreso.Rows.Add(new object[] { img, PersonaCapturada.BasicData.FullName, identificacion, PersonaCapturada.BasicData.Created, PersonaCapturada.BasicData.CreatedBy, PersonaCapturada.BasicData.ModifiedBy, GeneralCompleted, vArchivoEpd });
						}
					}
					if (PersonaCapturada.BasicData.UploadStatus.ToString() == "Pending")
					{
						flag = true;
					}
					else
					{
						uploadStatus = PersonaCapturada.BasicData.UploadStatus;
						flag = uploadStatus.ToString() == "Done";
					}
					if (flag)
					{
						this.con_dgpen++;
						if (vContainerMain.link == "PEN")
						{
							vContainerMain.dt_pendientes.Rows.Add(new object[] { img, PersonaCapturada.BasicData.FullName, identificacion, PersonaCapturada.BasicData.Created, PersonaCapturada.BasicData.CreatedBy, PersonaCapturada.BasicData.ModifiedBy, GeneralCompleted, vArchivoEpd });
						}
					}
					if (PersonaCapturada.BasicData.UploadStatus.ToString() == "UploadingBiometricData")
					{
						str1 = true;
					}
					else
					{
						uploadStatus = PersonaCapturada.BasicData.UploadStatus;
						str1 = uploadStatus.ToString() == "Failed";
					}
					if (str1)
					{
						this.con_dgsub++;
						if (vContainerMain.link == "ENV")
						{
							vContainerMain.dt_enviados.Rows.Add(new object[] { img, PersonaCapturada.BasicData.FullName, identificacion, PersonaCapturada.BasicData.Created, PersonaCapturada.BasicData.CreatedBy, PersonaCapturada.BasicData.ModifiedBy, GeneralCompleted, vArchivoEpd, PersonaCapturada.BasicData.StatusMessage });
						}
					}
				}
				this.linkProgreso.Text = string.Concat(" - EN PROGRESO  (", this.con_dgdat.ToString(), ")");
				this.linkpendientes.Text = string.Concat(" - PENDIENTES DE ENVIO  (", this.con_dgpen.ToString(), ")");
				this.linkSubidos.Text = string.Concat(" - ENVIADOS  (", this.con_dgsub.ToString(), ")");
				if ((!this.dgDatos.Visible || this.dgPendientes.Visible ? false : !this.dgSubidos.Visible))
				{
					this.dgDatos.DataSource = vContainerMain.dt_enprogreso;
					this.dgDatos.RowTemplate.Height = 30;
					this.dgDatos.Visible = true;
				}
				if ((this.dgDatos.Visible || !this.dgPendientes.Visible ? false : !this.dgSubidos.Visible))
				{
					this.dgPendientes.DataSource = vContainerMain.dt_pendientes;
					this.dgPendientes.RowTemplate.Height = 30;
					this.dgPendientes.Visible = true;
				}
				if ((this.dgDatos.Visible || this.dgPendientes.Visible ? false : this.dgSubidos.Visible))
				{
					this.dgSubidos.DataSource = vContainerMain.dt_enviados;
					this.dgSubidos.RowTemplate.Height = 30;
					this.dgSubidos.Visible = true;
				}
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void ribbonButton3_Click(object sender, EventArgs e)
		{
			try
			{
				(new vNewuser()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void ribbonButton4_Click(object sender, EventArgs e)
		{
			try
			{
				(new vPasswordUser()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void ribbonButton5_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			if (this.dgDatos.Visible)
			{
				if (this.dgDatos.CurrentCell.RowIndex > -1)
				{
					DataGridViewRow vFila = this.dgDatos.Rows[this.dgDatos.CurrentCell.RowIndex];
					vContainerMain.RutaEpd = ((DataGridViewTextBoxCell)vFila.Cells[7]).Value.ToString();
					vContainerMain.RutaEpd.Split(new char[] { '\\' });
					if (File.Exists(vContainerMain.RutaEpd))
					{
						vContainerMain.RutaEpd = Path.GetFullPath(vContainerMain.RutaEpd);
						Process.Start("explorer.exe", string.Format("/select,\"{0}\"", vContainerMain.RutaEpd));
					}
				}
			}
			if (this.dgPendientes.Visible)
			{
				if (this.dgPendientes.CurrentCell.RowIndex > -1)
				{
					DataGridViewRow vFila = this.dgPendientes.Rows[this.dgPendientes.CurrentCell.RowIndex];
					vContainerMain.RutaEpd = ((DataGridViewTextBoxCell)vFila.Cells[7]).Value.ToString();
					vContainerMain.RutaEpd.Split(new char[] { '\\' });
					if (File.Exists(vContainerMain.RutaEpd))
					{
						vContainerMain.RutaEpd = Path.GetFullPath(vContainerMain.RutaEpd);
						Process.Start("explorer.exe", string.Format("/select,\"{0}\"", vContainerMain.RutaEpd));
					}
				}
			}
			if (this.dgSubidos.Visible)
			{
				if (this.dgSubidos.CurrentCell.RowIndex > -1)
				{
					DataGridViewRow vFila = this.dgSubidos.Rows[this.dgSubidos.CurrentCell.RowIndex];
					vContainerMain.RutaEpd = ((DataGridViewTextBoxCell)vFila.Cells[7]).Value.ToString();
					vContainerMain.RutaEpd.Split(new char[] { '\\' });
					if (File.Exists(vContainerMain.RutaEpd))
					{
						vContainerMain.RutaEpd = Path.GetFullPath(vContainerMain.RutaEpd);
						Process.Start("explorer.exe", string.Format("/select,\"{0}\"", vContainerMain.RutaEpd));
					}
				}
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void ribbonButton6_Click(object sender, EventArgs e)
		{
			try
			{
				(new vResetUser()
				{
					MdiParent = base.ParentForm
				}).Show();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void ribbonButton7_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Filter = "pdf files (*.pdf) |*.pdf;"
			};
			dlg.ShowDialog();
			if (dlg.FileName != null)
			{
			}
		}

		private void ribbonButton8_Click(object sender, EventArgs e)
		{
			if ((!this.dgPendientes.Visible ? false : !this.dgSubidos.Visible))
			{
				try
				{
					if (this.dgPendientes.CurrentCell.RowIndex > -1)
					{
						DataGridViewRow vFila = this.dgPendientes.Rows[this.dgPendientes.CurrentCell.RowIndex];
						vContainerMain.RutaEpd = vFila.Cells[7].Value.ToString();
						string nombre = vFila.Cells[1].Value.ToString();
						string identificacion = vFila.Cells[2].Value.ToString();
						if (MessageBox.Show(string.Concat(new string[] { "Nombre : ", nombre, "\nIdentificacion: ", identificacion, "\n\n¿Esta seguro(a) de Autorizar la edicion del Registro?" }), "Mensaje", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
						{
							HelperSerializer ser = new HelperSerializer();
							vContainerMain.PersonaCapturada = ser.DeserializeEpd(vContainerMain.RutaEpd);
							if (vContainerMain.PersonaCapturada.BasicData.UploadStatus.ToString() == "Pending")
							{
								vContainerMain.PersonaCapturada.BasicData.UploadStatus = UploadStatus.InProgress;
							}
							if (vContainerMain.PersonaCapturada.BasicData.UploadStatus.ToString() == "Done")
							{
								vContainerMain.PersonaCapturada.BasicData.UploadStatus = UploadStatus.ManualVerification;
							}
							ser.SerializeEpd(vContainerMain.PersonaCapturada, vContainerMain.RutaEpd);
							this.LlenarGrilla();
						}
					}
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
				}
			}
			if ((this.dgPendientes.Visible ? false : this.dgSubidos.Visible))
			{
				try
				{
					if (this.dgSubidos.CurrentCell.RowIndex > -1)
					{
						DataGridViewRow vFila = this.dgSubidos.Rows[this.dgSubidos.CurrentCell.RowIndex];
						vContainerMain.RutaEpd = vFila.Cells[7].Value.ToString();
						HelperSerializer ser = new HelperSerializer();
						vContainerMain.PersonaCapturada = ser.DeserializeEpd(vContainerMain.RutaEpd);
						string nombre = vFila.Cells[1].Value.ToString();
						string identificacion = vFila.Cells[2].Value.ToString();
						if (MessageBox.Show(string.Concat(new string[] { "Nombre : ", nombre, "\nIdentificacion: ", identificacion, "\n\n¿Esta seguro(a) de Autorizar la edicion del Registro?" }), "Mensaje", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
						{
							if (vContainerMain.PersonaCapturada.BasicData.UploadStatus.ToString() != "UploadingBiometricData")
							{
								MessageBox.Show(string.Concat(new string[] { "El registro con:\n\nNombre: ", nombre, "\nIdentificacion: ", identificacion, "\n\nNo se puede Autorizar la Edicion, por que ya fue editado anteriormente." }), "Mensaje");
							}
							else
							{
								vContainerMain.PersonaCapturada.BasicData.UploadStatus = UploadStatus.ManualVerification;
								ser.SerializeEpd(vContainerMain.PersonaCapturada, vContainerMain.RutaEpd);
							}
						}
						this.LlenarGrilla();
					}
				}
				catch (Exception exception1)
				{
					Console.WriteLine(exception1);
				}
			}
		}

		private void ribbonEnviar_Click(object sender, EventArgs e)
		{
			try
			{
				(new Sincronizacion.fPaquetes()).Show();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void ribtnActualizar_Click(object sender, EventArgs e)
		{
			try
			{
				this.LlenarGrilla();
			}
			catch
			{
			}
		}

		private void ribtnEditar_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dgDatos.CurrentCell.RowIndex > -1)
				{
					DataGridViewRow vFila = this.dgDatos.Rows[this.dgDatos.CurrentCell.RowIndex];
					vContainerMain.RutaEpd = vFila.Cells["ArchivoEpdN"].Value.ToString();
					(new vEnroll()
					{
						MdiParent = base.ParentForm
					}).Show();
					base.Close();
				}
			}
			catch
			{
			}
		}

		private void ribtnEliminar_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("¿Esta seguro de que desea eliminar los elementos seleccionados?", "Enrolamiento", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				{
					if (this.dgDatos.CurrentCell.RowIndex > -1)
					{
						DataGridViewRow vFila = this.dgDatos.Rows[this.dgDatos.CurrentCell.RowIndex];
						vContainerMain.RutaEpd = vFila.Cells["ArchivoEpdN"].Value.ToString();
						if (File.Exists(vContainerMain.RutaEpd))
						{
							File.Delete(vContainerMain.RutaEpd);
						}
					}
					this.LlenarGrilla();
				}
			}
			catch
			{
			}
		}

		private void ribtnFisico_Click(object sender, EventArgs e)
		{
			try
			{
				(new vSynchronization()).Show();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private void ribtnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
				vContainerMain.RutaEpd = string.Empty;
				(new vEnroll()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
			catch
			{
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (vContainerMain.link == "PRO")
				{
					this.dgDatos.Visible = true;
					DataRow[] dr = vContainerMain.dt_enprogreso.Select(string.Concat("Nombre LIKE '%", this.textBox1.Text, "%'"));
					if (dr.Length == 0)
					{
						DataRow[] dr0 = vContainerMain.dt_enprogreso.Select(string.Concat("Identificacion LIKE '%", this.textBox1.Text, "%'"));
						if (dr0.Length == 0)
						{
							DataRow[] dr1 = vContainerMain.dt_enprogreso.Select(string.Concat("Fecha LIKE '%", this.textBox1.Text, "%'"));
							if (dr1.Length == 0)
							{
								DataRow[] dr2 = vContainerMain.dt_enprogreso.Select(string.Concat("Creado LIKE '%", this.textBox1.Text, "%'"));
								if (dr2.Length == 0)
								{
									this.dgDatos.Visible = false;
								}
								else
								{
									DataTable dtgRILLA = dr2.CopyToDataTable<DataRow>();
									this.dgDatos.DataSource = dtgRILLA;
								}
							}
							else
							{
								DataTable dtgRILLA = dr1.CopyToDataTable<DataRow>();
								this.dgDatos.DataSource = dtgRILLA;
							}
						}
						else
						{
							DataTable dtgRILLA = dr0.CopyToDataTable<DataRow>();
							this.dgDatos.DataSource = dtgRILLA;
						}
					}
					else
					{
						DataTable dtgRILLA = dr.CopyToDataTable<DataRow>();
						this.dgDatos.DataSource = dtgRILLA;
					}
				}
				else if (vContainerMain.link != "PEN")
				{
					this.dgSubidos.Visible = true;
					DataRow[] dr = vContainerMain.dt_enviados.Select(string.Concat("Nombre LIKE '%", this.textBox1.Text, "%'"));
					if (dr.Length == 0)
					{
						DataRow[] dr0 = vContainerMain.dt_enviados.Select(string.Concat("Identificacion LIKE '%", this.textBox1.Text, "%'"));
						if (dr0.Length == 0)
						{
							DataRow[] dr1 = vContainerMain.dt_enviados.Select(string.Concat("Fecha LIKE '%", this.textBox1.Text, "%'"));
							if (dr1.Length == 0)
							{
								DataRow[] dr2 = vContainerMain.dt_enviados.Select(string.Concat("Creado LIKE '%", this.textBox1.Text, "%'"));
								if (dr2.Length == 0)
								{
									this.dgSubidos.Visible = false;
								}
								else
								{
									DataTable dtgRILLA = dr2.CopyToDataTable<DataRow>();
									this.dgSubidos.DataSource = dtgRILLA;
								}
							}
							else
							{
								DataTable dtgRILLA = dr1.CopyToDataTable<DataRow>();
								this.dgSubidos.DataSource = dtgRILLA;
							}
						}
						else
						{
							DataTable dtgRILLA = dr0.CopyToDataTable<DataRow>();
							this.dgSubidos.DataSource = dtgRILLA;
						}
					}
					else
					{
						DataTable dtgRILLA = dr.CopyToDataTable<DataRow>();
						this.dgSubidos.DataSource = dtgRILLA;
					}
				}
				else
				{
					this.dgPendientes.Visible = true;
					DataRow[] dr = vContainerMain.dt_pendientes.Select(string.Concat("Nombre LIKE '%", this.textBox1.Text, "%'"));
					if (dr.Length == 0)
					{
						DataRow[] dr0 = vContainerMain.dt_pendientes.Select(string.Concat("Identificacion LIKE '%", this.textBox1.Text, "%'"));
						if (dr0.Length == 0)
						{
							DataRow[] dr1 = vContainerMain.dt_pendientes.Select(string.Concat("Fecha LIKE '%", this.textBox1.Text, "%'"));
							if (dr1.Length == 0)
							{
								DataRow[] dr2 = vContainerMain.dt_pendientes.Select(string.Concat("Creado LIKE '%", this.textBox1.Text, "%'"));
								if (dr2.Length == 0)
								{
									this.dgPendientes.Visible = false;
								}
								else
								{
									DataTable dtgRILLA = dr2.CopyToDataTable<DataRow>();
									this.dgPendientes.DataSource = dtgRILLA;
								}
							}
							else
							{
								DataTable dtgRILLA = dr1.CopyToDataTable<DataRow>();
								this.dgPendientes.DataSource = dtgRILLA;
							}
						}
						else
						{
							DataTable dtgRILLA = dr0.CopyToDataTable<DataRow>();
							this.dgPendientes.DataSource = dtgRILLA;
						}
					}
					else
					{
						DataTable dtgRILLA = dr.CopyToDataTable<DataRow>();
						this.dgPendientes.DataSource = dtgRILLA;
					}
				}
			}
			catch
			{
			}
		}
	}
}