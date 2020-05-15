using System;
using System.Windows.Forms;

namespace BIODV
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new fMaestro());
			}
			catch
			{
			}
		}
	}
}