using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ENROLL
{
	public class vValidateFingers : Form
	{
		public static string[] propHuella;

		private IContainer components = null;

		private ListBox listBox1;

		public vValidateFingers()
		{
			this.InitializeComponent();
		}

		public static void cargarlistbox(string dedo, string resultado)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void fValidarHuellar_Load(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.listBox1 = new ListBox();
			base.SuspendLayout();
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new Point(116, 66);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(504, 251);
			this.listBox1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(800, 450);
			base.Controls.Add(this.listBox1);
			base.Name = "fValidarHuellar";
			this.Text = "fValidarHuellar";
			base.Load += new EventHandler(this.fValidarHuellar_Load);
			base.ResumeLayout(false);
		}
	}
}