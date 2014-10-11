using System;
using System.Windows.Forms;

namespace TrashSweep
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            if (!UacHelper.IsProcessElevated)
            {
                MessageBox.Show("Restart app with admin privilages", "Not running as admin",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}