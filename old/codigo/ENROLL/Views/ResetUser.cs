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
	public class vResetUser : Form
	{
		private IContainer components = null;

		private TextBox txtUsuarioResteo;

		private TextBox txtRepitePass;

		private TextBox txtPass;

		private ErrorProvider errorProvedor;

		private TextBox txtPassUsuarioActual;

		private Panel panel1;

		private Button button1;

		private Button btncancelar;

		private TableLayoutPanel tableLayoutPanel1;

		private PictureBox pictureBox1;

		private Label label4;

		private Label label41;

		private Label label7;

		private Label label1;

		private Label label6;

		private Label label8;

		private Label label5;

		private Label label2;

		private TextBox txtUsuario;

		private TableLayoutPanel tableLayoutPanel7;

		private TableLayoutPanel tableLayoutPanel8;

		private TableLayoutPanel tableLayoutPanel6;

		private TableLayoutPanel tableLayoutPanel5;

		private TableLayoutPanel tableLayoutPanel3;

		public vResetUser()
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
			base.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.ValidaModeloDatosUsuario())
			{
				HelperSerializer ser = new HelperSerializer();
				string vMensaje = string.Empty;
				string str = this.txtUsuario.Text.Trim();
				string vPassActual = this.txtPassUsuarioActual.Text.Trim();
				string now = this.txtUsuarioResteo.Text.Trim();
				this.txtPass.Text.Trim();
				this.txtRepitePass.Text.Trim();
				Usuarios _usuarios = ser.Deserialize<Usuarios>(File.ReadAllText("user/usuarios.xml"));
				if ((str == "admin.policia" ? false : str != "estanislao.castaya"))
				{
					this.Alerta("Mensaje", "Su usuario no puede cambiar la contraseña de otro usuario", false);
				}
				else
				{
					List<Usuario> vColUser = (
						from cust in _usuarios.ListaUsuario
						where cust.txtUsuario == str
						select cust).ToList<Usuario>();
					if (vColUser.Count == 1)
					{
						if (StringCipher.Decrypt(vColUser[0].Pass, vPassActual) != vColUser[0].Identificacion)
						{
							this.Alerta("Mensaje", "Contraseña incorrecta", false);
						}
						else
						{
							vColUser = (
								from cust in _usuarios.ListaUsuario
								where cust.txtUsuario == now
								select cust).ToList<Usuario>();
							if (vColUser.Count != 1)
							{
								this.Alerta("Mensaje", "Usuario inexitente", false);
							}
							else
							{
								string vPassword = StringCipher.Encrypt(vColUser[0].Identificacion, this.txtPass.Text.Trim());
								_usuarios.ListaUsuario.First<Usuario>((Usuario x) => x.txtUsuario == now).Pass = vPassword;
								_usuarios.ListaUsuario.First<Usuario>((Usuario x) => x.txtUsuario == now).FechaModificacion = DateTime.Now;
								_usuarios.ListaUsuario.First<Usuario>((Usuario x) => x.txtUsuario == now).UsuarioModificacion = str;
								ser.GeneraUsuariosXml(vMensaje, _usuarios);
								this.Alerta("Mensaje", "Se actualizó de manera correcta la contraseña", false);
								base.Close();
							}
						}
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

		private void fUsuarioReseteo_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(vLogin.usuario))
			{
				this.txtUsuario.Enabled = true;
				base.ActiveControl = this.txtUsuario;
			}
			else
			{
				this.txtUsuario.Text = vLogin.usuario;
				this.txtUsuario.Enabled = false;
				base.ActiveControl = this.txtPassUsuarioActual;
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(vResetUser));
			this.txtUsuarioResteo = new TextBox();
			this.txtRepitePass = new TextBox();
			this.txtPass = new TextBox();
			this.errorProvedor = new ErrorProvider(this.components);
			this.panel1 = new Panel();
			this.button1 = new Button();
			this.btncancelar = new Button();
			this.txtPassUsuarioActual = new TextBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.label4 = new Label();
			this.label41 = new Label();
			this.label7 = new Label();
			this.label2 = new Label();
			this.txtUsuario = new TextBox();
			this.label5 = new Label();
			this.label1 = new Label();
			this.label6 = new Label();
			this.label8 = new Label();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.tableLayoutPanel6 = new TableLayoutPanel();
			this.tableLayoutPanel7 = new TableLayoutPanel();
			this.tableLayoutPanel8 = new TableLayoutPanel();
			((ISupportInitialize)this.errorProvedor).BeginInit();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			base.SuspendLayout();
			this.txtUsuarioResteo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.txtUsuarioResteo.Location = new Point(436, 6);
			this.txtUsuarioResteo.Name = "txtUsuarioResteo";
			this.txtUsuarioResteo.Size = new System.Drawing.Size(254, 20);
			this.txtUsuarioResteo.TabIndex = 1;
			this.txtUsuarioResteo.TextChanged += new EventHandler(this.txtUsuarioResteo_TextChanged);
			this.txtRepitePass.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.txtRepitePass.Location = new Point(436, 71);
			this.txtRepitePass.Name = "txtRepitePass";
			this.txtRepitePass.PasswordChar = '*';
			this.txtRepitePass.Size = new System.Drawing.Size(254, 20);
			this.txtRepitePass.TabIndex = 3;
			this.txtRepitePass.TextChanged += new EventHandler(this.txtRepitePass_TextChanged);
			this.txtPass.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.txtPass.Location = new Point(436, 38);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.Size = new System.Drawing.Size(254, 20);
			this.txtPass.TabIndex = 2;
			this.txtPass.TextChanged += new EventHandler(this.txtPass_TextChanged);
			this.errorProvedor.ContainerControl = this;
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.btncancelar);
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(263, 71);
			this.panel1.TabIndex = 14;
			this.button1.BackColor = Color.White;
			this.button1.Image = (Image)resources.GetObject("button1.Image");
			this.button1.ImageAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(14, 14);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(84, 54);
			this.button1.TabIndex = 4;
			this.button1.Text = "Guardar y Salir";
			this.button1.TextAlign = ContentAlignment.BottomCenter;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btncancelar.BackColor = Color.White;
			this.btncancelar.Image = (Image)resources.GetObject("btncancelar.Image");
			this.btncancelar.ImageAlign = ContentAlignment.TopCenter;
			this.btncancelar.Location = new Point(104, 14);
			this.btncancelar.Name = "btncancelar";
			this.btncancelar.Size = new System.Drawing.Size(75, 54);
			this.btncancelar.TabIndex = 5;
			this.btncancelar.Text = "Salir";
			this.btncancelar.TextAlign = ContentAlignment.BottomCenter;
			this.btncancelar.UseVisualStyleBackColor = false;
			this.btncancelar.Click += new EventHandler(this.btncancelar_Click);
			this.txtPassUsuarioActual.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.txtPassUsuarioActual.Location = new Point(436, 38);
			this.txtPassUsuarioActual.Name = "txtPassUsuarioActual";
			this.txtPassUsuarioActual.PasswordChar = '*';
			this.txtPassUsuarioActual.Size = new System.Drawing.Size(254, 20);
			this.txtPassUsuarioActual.TabIndex = 2;
			this.txtPassUsuarioActual.TextChanged += new EventHandler(this.txtPassUsuarioActual_TextChanged);
			this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Location = new Point(12, 34);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(867, 77);
			this.tableLayoutPanel1.TabIndex = 45;
			this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(775, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(89, 71);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			this.label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.BackColor = Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 14.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.label4.ForeColor = Color.White;
			this.label4.Location = new Point(3, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(861, 23);
			this.label4.TabIndex = 48;
			this.label4.Text = "CAMBIO DE CONTRASEÑA AUTORIZADO";
			this.label4.TextAlign = ContentAlignment.TopCenter;
			this.label41.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label41.AutoSize = true;
			this.label41.BackColor = Color.Transparent;
			this.label41.BorderStyle = BorderStyle.Fixed3D;
			this.label41.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label41.ForeColor = Color.White;
			this.label41.Location = new Point(176, 6);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(514, 20);
			this.label41.TabIndex = 26;
			this.label41.Text = "VALIDACIÓN SUPERVISOR";
			this.label7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.BackColor = Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label7.ForeColor = Color.White;
			this.label7.Location = new Point(176, 41);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(254, 14);
			this.label7.TabIndex = 51;
			this.label7.Text = "Ingrese su contraseña:";
			this.label7.TextAlign = ContentAlignment.MiddleLeft;
			this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.BackColor = Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label2.ForeColor = Color.White;
			this.label2.Location = new Point(176, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(254, 14);
			this.label2.TabIndex = 53;
			this.label2.Text = "Ingrese su usuario:";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.txtUsuario.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.txtUsuario.Location = new Point(436, 6);
			this.txtUsuario.Name = "txtUsuario";
			this.txtUsuario.Size = new System.Drawing.Size(254, 20);
			this.txtUsuario.TabIndex = 1;
			this.label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.BackColor = Color.Transparent;
			this.label5.BorderStyle = BorderStyle.Fixed3D;
			this.label5.Font = new System.Drawing.Font("Tahoma", 11f, FontStyle.Bold | FontStyle.Underline);
			this.label5.ForeColor = Color.White;
			this.label5.Location = new Point(176, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(514, 20);
			this.label5.TabIndex = 26;
			this.label5.Text = "DATOS DEL USUARIO QUE RESETEARÁ LA CONTRASEÑA";
			this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.BackColor = Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(176, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(254, 14);
			this.label1.TabIndex = 52;
			this.label1.Text = "Nombre del usuario:";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.label6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.BackColor = Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label6.ForeColor = Color.White;
			this.label6.Location = new Point(176, 41);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(254, 14);
			this.label6.TabIndex = 53;
			this.label6.Text = "Ingrese la nueva contraseña:";
			this.label6.TextAlign = ContentAlignment.MiddleLeft;
			this.label8.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.BackColor = Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Bold);
			this.label8.ForeColor = Color.White;
			this.label8.Location = new Point(176, 74);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(254, 14);
			this.label8.TabIndex = 54;
			this.label8.Text = "Repita la nueva contraseña:";
			this.label8.TextAlign = ContentAlignment.MiddleLeft;
			this.tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel3.Location = new Point(12, 117);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 39f));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(867, 39);
			this.tableLayoutPanel3.TabIndex = 55;
			this.tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel5.Controls.Add(this.label41, 1, 0);
			this.tableLayoutPanel5.Location = new Point(12, 162);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(867, 33);
			this.tableLayoutPanel5.TabIndex = 56;
			this.tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel6.ColumnCount = 4;
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel6.Controls.Add(this.txtPassUsuarioActual, 2, 1);
			this.tableLayoutPanel6.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.label7, 1, 1);
			this.tableLayoutPanel6.Controls.Add(this.txtUsuario, 2, 0);
			this.tableLayoutPanel6.Location = new Point(12, 201);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 2;
			this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(868, 65);
			this.tableLayoutPanel6.TabIndex = 57;
			this.tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel7.ColumnCount = 4;
			this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel7.Controls.Add(this.txtRepitePass, 2, 2);
			this.tableLayoutPanel7.Controls.Add(this.txtPass, 2, 1);
			this.tableLayoutPanel7.Controls.Add(this.txtUsuarioResteo, 2, 0);
			this.tableLayoutPanel7.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel7.Controls.Add(this.label6, 1, 1);
			this.tableLayoutPanel7.Controls.Add(this.label8, 1, 2);
			this.tableLayoutPanel7.Location = new Point(12, 326);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 3;
			this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(868, 98);
			this.tableLayoutPanel7.TabIndex = 59;
			this.tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tableLayoutPanel8.ColumnCount = 3;
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel8.Controls.Add(this.label5, 1, 0);
			this.tableLayoutPanel8.Location = new Point(12, 287);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 1;
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(867, 33);
			this.tableLayoutPanel8.TabIndex = 58;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(48, 63, 105);
			base.ClientSize = new System.Drawing.Size(891, 683);
			base.Controls.Add(this.tableLayoutPanel7);
			base.Controls.Add(this.tableLayoutPanel8);
			base.Controls.Add(this.tableLayoutPanel6);
			base.Controls.Add(this.tableLayoutPanel5);
			base.Controls.Add(this.tableLayoutPanel3);
			base.Controls.Add(this.tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "fUsuarioReseteo";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "fUsuarioReseteo";
			base.Load += new EventHandler(this.fUsuarioReseteo_Load);
			((ISupportInitialize)this.errorProvedor).EndInit();
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			base.ResumeLayout(false);
		}

		private void txtPass_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private void txtPassUsuarioActual_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private void txtRepitePass_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private void txtUsuarioResteo_TextChanged(object sender, EventArgs e)
		{
			this.ValidaModeloDatosUsuario();
		}

		private bool ValidaModeloDatosUsuario()
		{
			return HelperValidatorField.ValidarCampos(new DatosPasswordReset()
			{
				txtPassUsuarioActual = this.txtPassUsuarioActual.Text.Trim(),
				txtUsuarioResteo = this.txtUsuarioResteo.Text.Trim(),
				txtPass = this.txtPass.Text.Trim(),
				txtRepitePass = this.txtRepitePass.Text.Trim()
			}, this.errorProvedor, this);
		}
	}
}