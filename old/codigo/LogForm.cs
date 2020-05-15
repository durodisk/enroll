using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GBMSDemo
{
    public partial class LogForm : Form
    {
        public LogForm(String Caption)
        {
            InitializeComponent();

            Text = Caption;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void LogMessage(String Message)
        {
            txtLog.Text += Message + Environment.NewLine;
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
            Application.DoEvents();
        }

        public void EnableClose()
        {
            btnClose.Enabled = true;
        }
    }
}