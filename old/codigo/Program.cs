using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace GBMSDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // prevent multiple instances
            if (System.Diagnostics.Process.GetProcessesByName("GBMSDemo").Length > 1)
                return;

            // Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoForm());
        }
    }
}