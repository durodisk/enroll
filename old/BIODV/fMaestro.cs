using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace BIODV
{
	public class fMaestro : Form
	{
		public static string @var;

		private bool moviendoFormulario;

		private Point posicionActualPuntero;

		private IContainer components = null;

		private Panel panel2;

		private Label lbltituloform;

		private Button button2;

		private MenuStrip menuStrip1;

		private Button btnMinimizar;

		private Button btnnormal;

		private Button btnmaximizar;

		static fMaestro()
		{
			fMaestro.@var = "Sistema Integrado de Investigacion Criminal Enrolamiento";
		}

		public fMaestro()
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

		private void btnmaximizar_Click(object sender, EventArgs e)
		{
			this.btnnormal.Visible = true;
			this.btnmaximizar.Visible = false;
			base.WindowState = FormWindowState.Maximized;
		}

		private void btnMinimizar_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		private void btnnormal_Click(object sender, EventArgs e)
		{
			this.btnnormal.Visible = false;
			this.btnmaximizar.Visible = true;
			base.WindowState = FormWindowState.Normal;
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.Alerta("Mensaje", "Esta seguro de Salir del Sistema ?", true))
			{
				base.Close();
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

		private void fMaestro_Load(object sender, EventArgs e)
		{
			this.btnnormal.Visible = true;
			this.btnmaximizar.Visible = false;
			this.lbltituloform.Text = fMaestro.@var;
			base.Controls.OfType<System.Windows.Forms.MdiClient>().FirstOrDefault<System.Windows.Forms.MdiClient>().BackColor = Color.WhiteSmoke;
			if ((new fLogin()).ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
			{
				base.Close();
			}
			else
			{
				(new fPrincipal()
				{
					MdiParent = this
				}).Show();
			}
		}

		private void fMaestro_Paint(object sender, PaintEventArgs e)
		{
			int width = base.Width - 1;
			int height = base.Height - 1;
			Pen greenPen = new Pen(Color.FromArgb(255, 0, 255, 0), 5f);
			e.Graphics.DrawRectangle(greenPen, 0, 0, width, height);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(fMaestro));
			this.panel2 = new Panel();
			this.btnmaximizar = new Button();
			this.btnMinimizar = new Button();
			this.btnnormal = new Button();
			this.lbltituloform = new Label();
			this.button2 = new Button();
			this.menuStrip1 = new MenuStrip();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel2.BackColor = Color.FromArgb(47, 54, 82);
			this.panel2.Controls.Add(this.btnmaximizar);
			this.panel2.Controls.Add(this.btnMinimizar);
			this.panel2.Controls.Add(this.btnnormal);
			this.panel2.Controls.Add(this.lbltituloform);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Cursor = Cursors.Hand;
			this.panel2.Dock = DockStyle.Top;
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1249, 28);
			this.panel2.TabIndex = 23;
			this.panel2.DoubleClick += new EventHandler(this.panel2_DoubleClick);
			this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
			this.panel2.MouseMove += new MouseEventHandler(this.panel2_MouseMove);
			this.panel2.MouseUp += new MouseEventHandler(this.panel2_MouseUp);
			this.btnmaximizar.Anchor = AnchorStyles.Right;
			this.btnmaximizar.BackColor = Color.Silver;
			this.btnmaximizar.FlatAppearance.BorderSize = 0;
			this.btnmaximizar.FlatAppearance.MouseOverBackColor = Color.Blue;
			this.btnmaximizar.FlatStyle = FlatStyle.Flat;
			this.btnmaximizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnmaximizar.ForeColor = Color.Transparent;
			this.btnmaximizar.Image = (Image)resources.GetObject("btnmaximizar.Image");
			this.btnmaximizar.Location = new Point(1198, 4);
			this.btnmaximizar.Name = "btnmaximizar";
			this.btnmaximizar.Size = new System.Drawing.Size(18, 18);
			this.btnmaximizar.TabIndex = 16;
			this.btnmaximizar.UseVisualStyleBackColor = false;
			this.btnmaximizar.Click += new EventHandler(this.btnmaximizar_Click);
			this.btnMinimizar.Anchor = AnchorStyles.Right;
			this.btnMinimizar.BackColor = Color.Silver;
			this.btnMinimizar.FlatAppearance.BorderSize = 0;
			this.btnMinimizar.FlatAppearance.MouseOverBackColor = Color.Blue;
			this.btnMinimizar.FlatStyle = FlatStyle.Flat;
			this.btnMinimizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnMinimizar.ForeColor = Color.Transparent;
			this.btnMinimizar.Image = (Image)resources.GetObject("btnMinimizar.Image");
			this.btnMinimizar.Location = new Point(1176, 3);
			this.btnMinimizar.Name = "btnMinimizar";
			this.btnMinimizar.Size = new System.Drawing.Size(18, 18);
			this.btnMinimizar.TabIndex = 15;
			this.btnMinimizar.UseVisualStyleBackColor = false;
			this.btnMinimizar.Click += new EventHandler(this.btnMinimizar_Click);
			this.btnnormal.Anchor = AnchorStyles.Right;
			this.btnnormal.BackColor = Color.Silver;
			this.btnnormal.FlatAppearance.BorderSize = 0;
			this.btnnormal.FlatAppearance.MouseOverBackColor = Color.Blue;
			this.btnnormal.FlatStyle = FlatStyle.Flat;
			this.btnnormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnnormal.ForeColor = Color.Transparent;
			this.btnnormal.Image = (Image)resources.GetObject("btnnormal.Image");
			this.btnnormal.Location = new Point(1198, 4);
			this.btnnormal.Name = "btnnormal";
			this.btnnormal.Size = new System.Drawing.Size(18, 18);
			this.btnnormal.TabIndex = 14;
			this.btnnormal.UseVisualStyleBackColor = false;
			this.btnnormal.Click += new EventHandler(this.btnnormal_Click);
			this.lbltituloform.AutoSize = true;
			this.lbltituloform.ForeColor = SystemColors.ScrollBar;
			this.lbltituloform.Location = new Point(10, 4);
			this.lbltituloform.Name = "lbltituloform";
			this.lbltituloform.Size = new System.Drawing.Size(33, 13);
			this.lbltituloform.TabIndex = 13;
			this.lbltituloform.Text = "Titulo";
			this.button2.Anchor = AnchorStyles.Right;
			this.button2.BackColor = Color.Silver;
			this.button2.FlatAppearance.BorderSize = 0;
			this.button2.FlatAppearance.MouseOverBackColor = Color.Blue;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.button2.ForeColor = Color.Transparent;
			this.button2.Image = (Image)resources.GetObject("button2.Image");
			this.button2.Location = new Point(1222, 4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(18, 18);
			this.button2.TabIndex = 12;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1249, 24);
			this.menuStrip1.TabIndex = 25;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			this.BackgroundImageLayout = ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(1249, 687);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.menuStrip1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.IsMdiContainer = true;
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "fMaestro";
			base.TransparencyKey = Color.Maroon;
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.fMaestro_Load);
			base.Paint += new PaintEventHandler(this.fMaestro_Paint);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void panel2_DoubleClick(object sender, EventArgs e)
		{
			if (!this.btnnormal.Visible)
			{
				this.btnnormal.Visible = true;
				this.btnmaximizar.Visible = false;
				base.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.btnnormal.Visible = false;
				this.btnmaximizar.Visible = true;
				base.WindowState = FormWindowState.Normal;
			}
		}

		private void panel2_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left)
			{
				this.moviendoFormulario = false;
			}
			else
			{
				this.moviendoFormulario = true;
				this.posicionActualPuntero = new Point(e.X, e.Y);
			}
		}

		private void panel2_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.moviendoFormulario)
			{
				Point nuevoPunto = base.PointToScreen(new Point(e.X, e.Y));
				nuevoPunto.Offset(-this.posicionActualPuntero.X, -this.posicionActualPuntero.Y);
				base.Location = nuevoPunto;
			}
		}

		private void panel2_MouseUp(object sender, MouseEventArgs e)
		{
			this.moviendoFormulario = false;
		}
	}
}