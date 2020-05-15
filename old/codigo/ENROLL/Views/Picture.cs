using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace ENROLL
{
    public class vPicture : Form
    {
        private FilterInfoCollection filter;

        private VideoCaptureDevice device;

        private readonly static CascadeClassifier cascadeclassifier;

        private Bitmap bitmap2;

        public static Bitmap Captura;

        private IContainer components = null;

        private PictureBox pictureBox2;

        private Button button1;

        private PictureBox pictureBox1;

        private ComboBox comboBox1;

        private Button btndetect;

        private Button button2;

        private Panel panel1;

        static vPicture()
        {
            vPicture.cascadeclassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        }

        public vPicture()
        {
            this.InitializeComponent();
        }

        private void btndetect_Click(object sender, EventArgs e)
        {
            try
            {
                this.button1.Visible = true;
                this.pictureBox2.Visible = false;
                this.device = new VideoCaptureDevice(this.filter[this.comboBox1.SelectedIndex].MonikerString);
                this.device.NewFrame += new NewFrameEventHandler(this.Device_NewFrame);
                this.device.Start();
                ComboBox comboBox = this.comboBox1;
                bool num = false;
                bool flag = (bool)num;
                this.btndetect.Enabled = (bool)num;
                comboBox.Enabled = flag;
            }
            catch
            {
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (this.button1.Text != "Capturar")
            {
                this.button1.Text = "Capturar";
                this.pictureBox2.Visible = false;
            }
            else
            {
                this.button1.Text = "Nuevo";
                this.pictureBox2.Visible = true;
                vPicture.Captura = this.bitmap2;
                this.pictureBox2.Image = vPicture.Captura;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                this.bitmap2 = (Bitmap)eventArgs.Frame.Clone();
                Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
                CascadeClassifier cascadeClassifier = vPicture.cascadeclassifier;
                System.Drawing.Size size = new System.Drawing.Size();
                System.Drawing.Size size1 = size;
                size = new System.Drawing.Size();
                Rectangle[] rectangleArray = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 1, size1, size);
                for (int i = 0; i < (int)rectangleArray.Length; i++)
                {
                    Rectangle rectangle = rectangleArray[i];
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        using (Pen pen = new Pen(Color.Red, 2f))
                        {
                            graphics.DrawRectangle(pen, rectangle);
                        }
                    }
                }
                this.pictureBox1.Image = bitmap;
            }
            catch
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

        private void Foto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((this.device == null ? false : this.device.IsRunning))
            {
                this.device.Stop();
            }
        }

        private void Foto_Load(object sender, EventArgs e)
        {
            try
            {
                vPicture.Captura = null;
                this.pictureBox2.Visible = false;
                this.button1.Visible = false;
                this.filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo device in this.filter)
                {
                    this.comboBox1.Items.Add(device.Name);
                }
                if (this.comboBox1.Items.Count <= 0)
                {
                    base.Close();
                    MessageBox.Show("No existe alguna Camara conectada al equipo");
                }
                else
                {
                    this.comboBox1.SelectedIndex = 0;
                    this.device = new VideoCaptureDevice();
                    ComboBox comboBox = this.comboBox1;
                    bool num = true;
                    bool flag = (bool)num;
                    this.btndetect.Enabled = (bool)num;
                    comboBox.Enabled = flag;
                }
            }
            catch
            {
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(vPicture));
            this.button1 = new Button();
            this.comboBox1 = new ComboBox();
            this.btndetect = new Button();
            this.pictureBox2 = new PictureBox();
            this.pictureBox1 = new PictureBox();
            this.button2 = new Button();
            this.panel1 = new Panel();
            ((ISupportInitialize)this.pictureBox2).BeginInit();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.button1.BackColor = Color.White;
            this.button1.Image = (Image)resources.GetObject("button1.Image");
            this.button1.ImageAlign = ContentAlignment.TopCenter;
            this.button1.Location = new Point(189, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 56);
            this.button1.TabIndex = 8;
            this.button1.Text = "Capturar";
            this.button1.TextAlign = ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click_1);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(20, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(299, 21);
            this.comboBox1.TabIndex = 6;
            this.btndetect.BackColor = Color.White;
            this.btndetect.Image = (Image)resources.GetObject("btndetect.Image");
            this.btndetect.ImageAlign = ContentAlignment.TopCenter;
            this.btndetect.Location = new Point(329, 7);
            this.btndetect.Name = "btndetect";
            this.btndetect.Size = new System.Drawing.Size(70, 54);
            this.btndetect.TabIndex = 5;
            this.btndetect.Text = "Iniciar Camara";
            this.btndetect.TextAlign = ContentAlignment.BottomCenter;
            this.btndetect.UseVisualStyleBackColor = false;
            this.btndetect.Click += new EventHandler(this.btndetect_Click);
            this.pictureBox2.BackColor = Color.Gainsboro;
            this.pictureBox2.Location = new Point(20, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(460, 392);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox1.BackColor = Color.Gainsboro;
            this.pictureBox1.Location = new Point(20, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(460, 392);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.button2.BackColor = Color.White;
            this.button2.Image = (Image)resources.GetObject("button2.Image");
            this.button2.ImageAlign = ContentAlignment.TopCenter;
            this.button2.Location = new Point(405, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 54);
            this.button2.TabIndex = 10;
            this.button2.Text = "Salir";
            this.button2.TextAlign = ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.panel1.BackColor = Color.White;
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btndetect);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 528);
            this.panel1.TabIndex = 11;
            this.BackColor = Color.Teal;
            base.ClientSize = new System.Drawing.Size(520, 536);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Name = "Foto";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Fotografia";
            base.FormClosing += new FormClosingEventHandler(this.Foto_FormClosing);
            base.Load += new EventHandler(this.Foto_Load);
            ((ISupportInitialize)this.pictureBox2).EndInit();
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}