using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace BIODV
{
	public class Test : Form
	{
		private IContainer components = null;

		private Button button1;

		private DataGridView dataGridView1;

		private PictureBox pictureBox1;

		private DataGridViewImageColumn Image;

		private DataGridViewTextBoxColumn id;

		private ListBox listBox1;

		public Test()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
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

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Test));
			this.button1 = new Button();
			this.dataGridView1 = new DataGridView();
			this.Image = new DataGridViewImageColumn();
			this.id = new DataGridViewTextBoxColumn();
			this.pictureBox1 = new PictureBox();
			this.listBox1 = new ListBox();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.button1.Location = new Point(328, 116);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Image, this.id });
			this.dataGridView1.Location = new Point(12, 155);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(175, 283);
			this.dataGridView1.TabIndex = 2;
			this.Image.DataPropertyName = "Image";
			this.Image.FillWeight = 10f;
			this.Image.HeaderText = "Image";
			this.Image.ImageLayout = DataGridViewImageCellLayout.Stretch;
			this.Image.MinimumWidth = 2;
			this.Image.Name = "Image";
			this.Image.ReadOnly = true;
			this.Image.Width = 10;
			this.id.HeaderText = "Column1";
			this.id.Name = "id";
			this.id.ReadOnly = true;
			this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(84, 45);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new Point(337, 155);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(372, 173);
			this.listBox1.TabIndex = 4;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(800, 450);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.button1);
			base.Name = "Test";
			this.Text = "Test";
			base.Load += new EventHandler(this.Test_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}

		private void Test_Load(object sender, EventArgs e)
		{
			Bitmap img1 = new Bitmap("C:\\Imgs\\1.jpg");
			this.pictureBox1.Image = img1;
			this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.RowTemplate.Height = 80;
			this.dataGridView1.AllowUserToAddRows = false;
		}
	}
}