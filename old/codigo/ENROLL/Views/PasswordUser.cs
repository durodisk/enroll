using ENROLL.Model;
using ENROLL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ENROLL
{
	public class vPasswordUser : Form
	{
		private IContainer components = null;

		private GroupBox groupControl3;

		private Label label19;

		private Label label3;

		private TextBox txtRepitePass;

		private TextBox txtPass;

		private Label label2;

		private ErrorProvider errorProvedor;

		private Panel panel1;

		private Button button1;

		private Button btncancelar;

		private TextBox txtPassOld;

		private TableLayoutPanel tableLayoutPanel11;

		private Label label5;

		private TableLayoutPanel tableLayoutPanel1;

		private PictureBox pictureBox1;

		private TableLayoutPanel tableLayoutPanel2;

		public vPasswordUser()
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
				(new vContainerMain()
				{
					MdiParent = base.ParentForm
				}).Show();
				base.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.ValidaModeloDatosUsuario())
			{
				HelperSerializer ser = new HelperSerializer();
				Usuarios _usuarios = ser.Deserialize<Usuarios>(File.ReadAllText("user/usuarios.xml"));
				List<Usuario> vColUsuarios = (
					from cust in _usuarios.ListaUsuario
					where cust.txtUsuario == vLogin.usuario
					select cust).ToList<Usuario>();
				if (vColUsuarios.Count != 1)
				{
					MessageBox.Show("Usuario inexistente");
				}
				else
				{
					string vPassword = vColUsuarios[0].Pass;
					if (StringCipher.Decrypt(vPassword, this.txtPassOld.Text.Trim()) != vColUsuarios[0].Identificacion)
					{
						this.Alerta("Mensaje", "Contraseña incorrecta", false);
					}
					else
					{
						string vMensaje = string.Empty;
						vPassword = StringCipher.Encrypt(vColUsuarios[0].Identificacion, this.txtPass.Text.Trim());
						_usuarios.ListaUsuario.First<Usuario>((Usuario x) => x.txtUsuario == vLogin.usuario).Pass = vPassword;
						_usuarios.ListaUsuario.First<Usuario>((Usuario x) => x.txtUsuario == vLogin.usuario).FechaModificacion = DateTime.Now;
						_usuarios.ListaUsuario.First<Usuario>((Usuario x) => x.txtUsuario == vLogin.usuario).UsuarioModificacion = vLogin.usuario;
						ser.GeneraUsuariosXml(vMensaje, _usuarios);
						this.Alerta("Mensaje", "Se actualizó de manera correcta la contraseña", false);
					}
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void fUsuarioPassword_Load(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(vPasswordUser));
			this.groupControl3 = new GroupBox();
			this.txtPassOld = new TextBox();
			this.label19 = new Label();
			this.label3 = new Label();
			this.txtRepitePass = new TextBox();
			this.txtPass = new TextBox();
			this.label2 = new Label();
			this.errorProvedor = new ErrorProvider(this.components);
			this.button1 = new Button();
			this.btncancelar = new Button();
			this.panel1 = new Panel();
			this.tableLayoutPanel11 = new TableLayoutPanel();
			this.label5 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.groupControl3.SuspendLayout();
			((ISupportInitialize)this.errorProvedor).BeginInit();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel11.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.groupControl3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.groupControl3.Controls.Add(this.txtPassOld);
			this.groupControl3.Controls.Add(this.label19);
			this.groupControl3.Controls.Add(this.label3);
			this.groupControl3.Controls.Add(this.txtRepitePass);
			this.groupControl3.Controls.Add(this.txtPass);
			this.groupControl3.Controls.Add(this.label2);
			this.groupControl3.ForeColor = Color.White;
			this.groupControl3.Location = new Point(101, 18);
			this.groupControl3.Name = "groupControl3";
			this.groupControl3.Size = new System.Drawing.Size(778, 102);
			this.groupControl3.TabIndex = 11;
			this.groupControl3.TabStop = false;
			this.groupControl3.Text = "Datos Usuario";
			this.txtPassOld.Location = new Point(174, 33);
			this.txtPassOld.Name = "txtPassOld";
			this.txtPassOld.PasswordChar = '*';
			this.txtPassOld.Size = new System.Drawing.Size(214, 20);
			this.txtPassOld.TabIndex = 1;
			this.txtPassOld.TextChanged += new EventHandler(this.txtPassOld_TextChanged);
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label19.ForeColor = Color.White;
			this.label19.Location = new Point(26, 35);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(132, 14);
			this.label19.TabIndex = 42;
			this.label19.Text = "Antigua contraseña:";
			this.label19.TextAlign = ContentAlignment.TopRight;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label3.ForeColor = Color.White;
			this.label3.Location = new Point(416, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 14);
			this.label3.TabIndex = 18;
			this.label3.Text = "Repetir contraseña:";
			this.txtRepitePass.Location = new Point(550, 59);
			this.txtRepitePass.Name = "txtRepitePass";
			this.txtRepitePass.PasswordChar = '*';
			this.txtRepitePass.Size = new System.Drawing.Size(246, 20);
			this.txtRepitePass.TabIndex = 3;
			this.txtRepitePass.TextChanged += new EventHandler(this.txtRepitePass_TextChanged);
			this.txtPass.Location = new Point(174, 59);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.Size = new System.Drawing.Size(214, 20);
			this.txtPass.TabIndex = 2;
			this.txtPass.TextChanged += new EventHandler(this.txtPass_TextChanged);
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label2.ForeColor = Color.White;
			this.label2.Location = new Point(38, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 14);
			this.label2.TabIndex = 17;
			this.label2.Text = "Nueva contraseña:";
			this.errorProvedor.ContainerControl = this;
			this.button1.BackColor = Color.White;
			this.button1.Image = (Image)resources.GetObject("button1.Image");
			this.button1.ImageAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(18, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(84, 54);
			this.button1.TabIndex = 4;
			this.button1.Text = "Guardar";
			this.button1.TextAlign = ContentAlignment.BottomCenter;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(108, 12);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(75, 54);
			this.btncancelar.TabIndex = 5;
			this.btncancelar.Text = "Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.btncancelar);
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(230, 69);
			this.panel1.TabIndex = 12;
			this.tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel11.ColumnCount = 1;
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel11.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel11.Location = new Point(1, 119);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(993, 29);
			this.tableLayoutPanel11.TabIndex = 45;
			this.label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label5.ForeColor = Color.White;
			this.label5.Location = new Point(3, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(987, 18);
			this.label5.TabIndex = 26;
			this.label5.Text = "CAMBIO DE CONTRASEÑA";
			this.label5.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Location = new Point(1, 29);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(993, 94);
			this.tableLayoutPanel1.TabIndex = 47;
			this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(890, 3);
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
			this.tableLayoutPanel2.Controls.Add(this.groupControl3, 1, 0);
			this.tableLayoutPanel2.Location = new Point(1, 170);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(980, 138);
			this.tableLayoutPanel2.TabIndex = 49;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(993, 741);
			base.Controls.Add(this.tableLayoutPanel2);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.tableLayoutPanel11);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "fUsuarioPassword";
			this.Text = "fUsuarioPassword";
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.fUsuarioPassword_Load);
			this.groupControl3.ResumeLayout(false);
			this.groupControl3.PerformLayout();
			((ISupportInitialize)this.errorProvedor).EndInit();
			this.panel1.ResumeLayout(false);
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

		private void txtPassOld_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private void txtRepitePass_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private bool ValidaModeloDatosUsuario()
		{
			return HelperValidatorField.ValidarCampos(new DatosPassword()
			{
				txtPassOld = this.txtPassOld.Text.Trim(),
				txtPass = this.txtPass.Text.Trim(),
				txtRepitePass = this.txtRepitePass.Text.Trim()
			}, this.errorProvedor, this);
		}
	}
}