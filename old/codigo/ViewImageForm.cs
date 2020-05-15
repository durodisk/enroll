using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GBMSDemo
{
    public partial class ViewImageForm : Form
    {
        public Image bmpImage;

        public ViewImageForm()
        {
            InitializeComponent();
        }

        private void ViewImageForm_Load(object sender, EventArgs e)
        {
            ImagePictureBox.Image = bmpImage;

            // if possible, make image appear in original dimension
            if ((bmpImage.Width < Screen.PrimaryScreen.WorkingArea.Width) &&
                (bmpImage.Height < Screen.PrimaryScreen.WorkingArea.Height))
            {
                this.Width = bmpImage.Width + this.Width - ImagePictureBox.Width;
                this.Height = bmpImage.Height + this.Height - ImagePictureBox.Height;

                // center the window
                this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            }
            else
            {
                // make window full screen
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}