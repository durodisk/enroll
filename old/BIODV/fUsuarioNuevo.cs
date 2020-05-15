using BIODV.Modelo;
using BIODV.Util;
using Datys.SIP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BIODV
{
	public class fUsuarioNuevo : Form
	{
		private Serializer ser = new Serializer();

		private IContainer components = null;

		private Panel panel1;

		private Button btncancelar;

		private ErrorProvider errorProvedor;

		private GroupBox groupControl3;

		private Label label3;

		private TextBox txtRepitePass;

		private TextBox txtPass;

		private Label label2;

		private GroupBox groupControl4;

		private Label label7;

		private TextBox txtIdentificacion;

		private Label label8;

		private TextBox txtPrimerNombre;

		private TextBox txtSegundoApellido;

		private Label label9;

		private TextBox txtPrimerApellido;

		private Label label12;

		private TextBox txtSegundoNombre;

		private Label label13;

		private Button button1;

		private TextBox txtComplemento;

		private Label label5;

		private ComboBox cmbUnidadPol;

		private Label label19;

		private CheckBox chkCambioPass;

		private Button button2;

		private TableLayoutPanel tableLayoutPanel11;

		private Label label1;

		private TableLayoutPanel tableLayoutPanel1;

		private PictureBox pictureBox1;

		private TableLayoutPanel tableLayoutPanel2;

		public fUsuarioNuevo()
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

		private void btncancelar_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "¿ Esta seguro de Salir ? ", true))
			{
				(new fPrincipal()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ValidaModeloDatosUsuario())
				{
					string vPassword = StringCipher.Encrypt(this.txtIdentificacion.Text.Trim(), this.txtPass.Text.Trim());
					string vPrimerApellido = ((string.IsNullOrWhiteSpace(this.txtPrimerApellido.Text) ? this.txtSegundoApellido.Text.Trim() : this.txtPrimerApellido.Text.Trim())).Replace(" ", "");
					string vPrimerNombre = this.txtPrimerNombre.Text.Trim();
					vPrimerNombre = vPrimerNombre.Replace(" ", "");
					string vUsuario = string.Concat(vPrimerNombre.ToLower(), ".", vPrimerApellido.ToLower());
					string vMensaje = string.Empty;
					Usuarios vUsuariosXML = this.CrearUsuario(ref vMensaje, this.txtIdentificacion.Text.Trim(), this.txtComplemento.Text.Trim(), this.txtPrimerNombre.Text.Trim().ToUpper(), this.txtSegundoNombre.Text.Trim().ToUpper(), this.txtPrimerApellido.Text.Trim().ToUpper(), this.txtSegundoApellido.Text.Trim().ToUpper(), vUsuario, vPassword, this.cmbUnidadPol.SelectedValue.ToString(), DateTime.Now, fLogin.usuario);
					List<string> pColResultado = vMensaje.Split(new char[] { '-' }).ToList<string>();
					if ((!pColResultado.Any<string>() ? true : pColResultado[0] != "1"))
					{
						this.Alerta("Mensaje", vMensaje, false);
					}
					else
					{
						string vNombreUsuario = pColResultado[1];
						this.ser.GeneraUsuariosXml(vMensaje, vUsuariosXML);
						this.Alerta("Mensaje", string.Concat("Se creó correctamente el usuario ", vNombreUsuario), false);
						(new fPrincipal()
						{
							MdiParent = base.ParentForm
						}).Show();
						base.Close();
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
		}

		private void button3_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void chkCambioPass_CheckedChanged(object sender, EventArgs e)
		{
			TextBox textBox = this.txtPass;
			TextBox textBox1 = this.txtRepitePass;
			bool @checked = this.chkCambioPass.Checked;
			bool flag = @checked;
			textBox1.Visible = @checked;
			textBox.Visible = flag;
			this.ValidaModeloDatosUsuario();
		}

		private void cmbUnidadPol_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private Usuarios CrearUsuario(ref string vMensaje, string pIdentificacion, string pCOmplemento, string pPrimerNombre, string pSegundoNombre, string pPrimerApellido, string pSegundoApellido, string vUsuario, string vPassword, string vUnidadPol, DateTime vFecha, string pUsuarioCreacion)
		{
			Usuarios _usuarios = (new Serializer()).Deserialize<Usuarios>(File.ReadAllText("user/usuarios.xml")) ?? new Usuarios();
			if (_usuarios.ListaUsuario == null)
			{
				_usuarios.ListaUsuario = new List<Usuario>();
			}
			List<Usuario> vColUsuario = (
				from cust in _usuarios.ListaUsuario
				where (cust.Identificacion != this.txtIdentificacion.Text.Trim() ? false : cust.Complemento == this.txtComplemento.Text.Trim())
				select cust).ToList<Usuario>();
			if ((vColUsuario == null ? false : vColUsuario.Count != 0))
			{
				vMensaje = "Usuario Existente con ese carnet y complemento";
			}
			else
			{
				vColUsuario = (
					from x in _usuarios.ListaUsuario
					where x.txtUsuario.Contains(vUsuario)
					select x).ToList<Usuario>();
				string vNro = (vColUsuario == null || vColUsuario.Count == 0 ? "" : vColUsuario.Count.ToString());
				_usuarios.ListaUsuario.Add(new Usuario()
				{
					Identificacion = pIdentificacion,
					Complemento = pCOmplemento,
					PrimerNombre = pPrimerNombre,
					SegundoNombre = pSegundoNombre,
					PrimerApellido = pPrimerApellido,
					SegundoApellido = pSegundoApellido,
					txtUsuario = string.Concat(vUsuario, vNro),
					Pass = vPassword,
					Unidad = vUnidadPol,
					UsuarioCreacion = pUsuarioCreacion,
					UsuarioModificacion = pUsuarioCreacion,
					FechaCreacion = vFecha,
					FechaModificacion = vFecha
				});
				vMensaje = string.Concat("1-", vUsuario, vNro);
			}
			return _usuarios;
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void fUsuarioNuevo_Load(object sender, EventArgs e)
		{
			List<Coder> vCoder = CargarControl.ObtenerLista("UNIDADESPOLICIA");
			CargarControl.Combo(this.cmbUnidadPol, vCoder, true);
			this.chkCambioPass.Checked = true;
			this.chkCambioPass.Visible = false;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(fUsuarioNuevo));
			this.panel1 = new Panel();
			this.button2 = new Button();
			this.button1 = new Button();
			this.btncancelar = new Button();
			this.errorProvedor = new ErrorProvider(this.components);
			this.groupControl3 = new GroupBox();
			this.chkCambioPass = new CheckBox();
			this.cmbUnidadPol = new ComboBox();
			this.label19 = new Label();
			this.label3 = new Label();
			this.txtRepitePass = new TextBox();
			this.txtPass = new TextBox();
			this.label2 = new Label();
			this.groupControl4 = new GroupBox();
			this.txtComplemento = new TextBox();
			this.label5 = new Label();
			this.label7 = new Label();
			this.txtIdentificacion = new TextBox();
			this.label8 = new Label();
			this.txtPrimerNombre = new TextBox();
			this.txtSegundoApellido = new TextBox();
			this.label9 = new Label();
			this.txtPrimerApellido = new TextBox();
			this.label12 = new Label();
			this.txtSegundoNombre = new TextBox();
			this.label13 = new Label();
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.label1 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.errorProvedor).BeginInit();
			this.groupControl3.SuspendLayout();
			this.groupControl4.SuspendLayout();
			this.tableLayoutPanel11.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.btncancelar);
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(337, 75);
			this.panel1.TabIndex = 8;
			this.button2.BackColor = Color.White;
			this.button2.Image = (Image)resources.GetObject("button2.Image");
			this.button2.ImageAlign = ContentAlignment.TopCenter;
			this.button2.Location = new Point(9, 18);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(105, 54);
			this.button2.TabIndex = 12;
			this.button2.Text = "Actualizar Usuarios";
			this.button2.TextAlign = ContentAlignment.BottomCenter;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Visible = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.BackColor = Color.White;
			this.button1.Image = (Image)resources.GetObject("button1.Image");
			this.button1.ImageAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(120, 18);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(84, 54);
			this.button1.TabIndex = 10;
			this.button1.Text = "Guardar y Salir";
			this.button1.TextAlign = ContentAlignment.BottomCenter;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(210, 18);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(75, 54);
			this.btncancelar.TabIndex = 11;
			this.btncancelar.Text = "Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.errorProvedor.ContainerControl = this;
			this.groupControl3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.groupControl3.Controls.Add(this.chkCambioPass);
			this.groupControl3.Controls.Add(this.cmbUnidadPol);
			this.groupControl3.Controls.Add(this.label19);
			this.groupControl3.Controls.Add(this.label3);
			this.groupControl3.Controls.Add(this.txtRepitePass);
			this.groupControl3.Controls.Add(this.txtPass);
			this.groupControl3.Controls.Add(this.label2);
			this.groupControl3.ForeColor = Color.White;
			this.groupControl3.Location = new Point(108, 188);
			this.groupControl3.Name = "groupControl3";
			this.groupControl3.Size = new System.Drawing.Size(835, 117);
			this.groupControl3.TabIndex = 6;
			this.groupControl3.TabStop = false;
			this.groupControl3.Text = "Datos Usuario";
			this.chkCambioPass.AutoSize = true;
			this.chkCambioPass.ForeColor = Color.White;
			this.chkCambioPass.Location = new Point(534, 28);
			this.chkCambioPass.Name = "chkCambioPass";
			this.chkCambioPass.Size = new System.Drawing.Size(156, 17);
			this.chkCambioPass.TabIndex = 43;
			this.chkCambioPass.Text = "Desea cambiar contraseña:";
			this.chkCambioPass.UseVisualStyleBackColor = true;
			this.chkCambioPass.CheckedChanged += new EventHandler(this.chkCambioPass_CheckedChanged);
			this.cmbUnidadPol.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.cmbUnidadPol.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cmbUnidadPol.FormattingEnabled = true;
			this.cmbUnidadPol.Location = new Point(134, 28);
			this.cmbUnidadPol.Name = "cmbUnidadPol";
			this.cmbUnidadPol.Size = new System.Drawing.Size(255, 21);
			this.cmbUnidadPol.TabIndex = 7;
			this.cmbUnidadPol.SelectedIndexChanged += new EventHandler(this.cmbUnidadPol_SelectedIndexChanged);
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label19.ForeColor = Color.White;
			this.label19.Location = new Point(26, 35);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(98, 14);
			this.label19.TabIndex = 42;
			this.label19.Text = "Unidad policial:";
			this.label19.TextAlign = ContentAlignment.TopRight;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label3.ForeColor = Color.White;
			this.label3.Location = new Point(400, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 14);
			this.label3.TabIndex = 18;
			this.label3.Text = "Repetir contraseña:";
			this.txtRepitePass.Location = new Point(534, 61);
			this.txtRepitePass.Name = "txtRepitePass";
			this.txtRepitePass.PasswordChar = '*';
			this.txtRepitePass.Size = new System.Drawing.Size(246, 20);
			this.txtRepitePass.TabIndex = 9;
			this.txtRepitePass.TextChanged += new EventHandler(this.txtRepitePass_TextChanged);
			this.txtPass.Location = new Point(134, 61);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.Size = new System.Drawing.Size(255, 20);
			this.txtPass.TabIndex = 8;
			this.txtPass.TextChanged += new EventHandler(this.txtPass_TextChanged);
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label2.ForeColor = Color.White;
			this.label2.Location = new Point(34, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 14);
			this.label2.TabIndex = 17;
			this.label2.Text = "Contraseña:";
			this.groupControl4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.groupControl4.Controls.Add(this.txtComplemento);
			this.groupControl4.Controls.Add(this.label5);
			this.groupControl4.Controls.Add(this.label7);
			this.groupControl4.Controls.Add(this.txtIdentificacion);
			this.groupControl4.Controls.Add(this.label8);
			this.groupControl4.Controls.Add(this.txtPrimerNombre);
			this.groupControl4.Controls.Add(this.txtSegundoApellido);
			this.groupControl4.Controls.Add(this.label9);
			this.groupControl4.Controls.Add(this.txtPrimerApellido);
			this.groupControl4.Controls.Add(this.label12);
			this.groupControl4.Controls.Add(this.txtSegundoNombre);
			this.groupControl4.Controls.Add(this.label13);
			this.groupControl4.ForeColor = Color.White;
			this.groupControl4.Location = new Point(108, 16);
			this.groupControl4.Name = "groupControl4";
			this.groupControl4.Size = new System.Drawing.Size(835, 132);
			this.groupControl4.TabIndex = 10;
			this.groupControl4.TabStop = false;
			this.groupControl4.Text = "Datos Basicos";
			this.txtComplemento.Location = new Point(540, 36);
			this.txtComplemento.MaxLength = 3;
			this.txtComplemento.Name = "txtComplemento";
			this.txtComplemento.Size = new System.Drawing.Size(62, 20);
			this.txtComplemento.TabIndex = 2;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label5.ForeColor = Color.White;
			this.label5.Location = new Point(418, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 14);
			this.label5.TabIndex = 16;
			this.label5.Text = "Complemento:";
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label7.ForeColor = Color.White;
			this.label7.Location = new Point(41, 38);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(94, 14);
			this.label7.TabIndex = 0;
			this.label7.Text = "Identificacion:";
			this.txtIdentificacion.Location = new Point(140, 35);
			this.txtIdentificacion.Name = "txtIdentificacion";
			this.txtIdentificacion.Size = new System.Drawing.Size(255, 20);
			this.txtIdentificacion.TabIndex = 1;
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label8.ForeColor = Color.White;
			this.label8.Location = new Point(35, 62);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(101, 14);
			this.label8.TabIndex = 2;
			this.label8.Text = "Primer Nombre:";
			this.txtPrimerNombre.Location = new Point(140, 62);
			this.txtPrimerNombre.Name = "txtPrimerNombre";
			this.txtPrimerNombre.Size = new System.Drawing.Size(255, 20);
			this.txtPrimerNombre.TabIndex = 3;
			this.txtPrimerNombre.TextChanged += new EventHandler(this.txtPrimerNombre_TextChanged);
			this.txtSegundoApellido.Location = new Point(540, 89);
			this.txtSegundoApellido.Name = "txtSegundoApellido";
			this.txtSegundoApellido.Size = new System.Drawing.Size(246, 20);
			this.txtSegundoApellido.TabIndex = 6;
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label9.ForeColor = Color.White;
			this.label9.Location = new Point(34, 89);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(103, 14);
			this.label9.TabIndex = 4;
			this.label9.Text = "Primer Apellido:";
			this.txtPrimerApellido.Location = new Point(140, 89);
			this.txtPrimerApellido.Name = "txtPrimerApellido";
			this.txtPrimerApellido.Size = new System.Drawing.Size(255, 20);
			this.txtPrimerApellido.TabIndex = 5;
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label12.ForeColor = Color.White;
			this.label12.Location = new Point(415, 89);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(119, 14);
			this.label12.TabIndex = 10;
			this.label12.Text = "Segundo Apellido:";
			this.txtSegundoNombre.Location = new Point(540, 62);
			this.txtSegundoNombre.Name = "txtSegundoNombre";
			this.txtSegundoNombre.Size = new System.Drawing.Size(246, 20);
			this.txtSegundoNombre.TabIndex = 4;
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label13.ForeColor = Color.White;
			this.label13.Location = new Point(418, 62);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(117, 14);
			this.label13.TabIndex = 8;
			this.label13.Text = "Segundo Nombre:";
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.ColumnCount = 1;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel11.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel11.Location = new Point(0, 117);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(1052, 29);
			this.tableLayoutPanel11.TabIndex = 45;
			this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(3, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1046, 24);
			this.label1.TabIndex = 26;
			this.label1.Text = "CREACION DE USUARIOS";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel1.Location = new Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1052, 94);
			this.tableLayoutPanel1.TabIndex = 46;
			this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(949, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 88);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			this.tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel2.Controls.Add(this.groupControl4, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupControl3, 1, 1);
			this.tableLayoutPanel2.Location = new Point(0, 152);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1052, 329);
			this.tableLayoutPanel2.TabIndex = 48;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(1052, 741);
			base.Controls.Add(this.tableLayoutPanel2);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.tableLayoutPanel11);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "fUsuarioNuevo";
			this.Text = "fUsuarioNuevo";
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.fUsuarioNuevo_Load);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.errorProvedor).EndInit();
			this.groupControl3.ResumeLayout(false);
			this.groupControl3.PerformLayout();
			this.groupControl4.ResumeLayout(false);
			this.groupControl4.PerformLayout();
			this.tableLayoutPanel11.ResumeLayout(false);
			this.tableLayoutPanel11.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void txtPass_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private void txtPrimerNombre_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private void txtRepitePass_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private bool ValidaModeloDatosUsuario()
		{
			return Validador.ValidarCampos(new BIODV.Modelo.DatosUsuario()
			{
				txtPrimerNombre = this.txtPrimerNombre.Text.Trim(),
				txtSegundoNombre = this.txtSegundoNombre.Text.Trim(),
				txtPrimerApellido = this.txtPrimerApellido.Text.Trim(),
				txtSegundoApellido = this.txtSegundoApellido.Text.Trim(),
				chkCambioPass = this.chkCambioPass.Checked,
				txtPass = this.txtPass.Text.Trim(),
				txtRepitePass = this.txtRepitePass.Text.Trim(),
				cmbUnidadPol = (this.cmbUnidadPol.SelectedValue == null ? "" : this.cmbUnidadPol.SelectedValue.ToString())
			}, this.errorProvedor, this);
		}
	}
}