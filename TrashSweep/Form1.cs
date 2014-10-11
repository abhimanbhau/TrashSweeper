using System;
using System.IO;
using System.Windows.Forms;
using TrashSweep.Properties;

namespace TrashSweep
{
    public partial class Form1 : Form
    {
        private static readonly string ErrorLog = Path.GetTempFileName() + "_TSweeper.log";
        private long _bytesCleaned;
        private StreamWriter _logFile = new StreamWriter(ErrorLog);
        private long _totalJunkFiles;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPerformSweep_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Oh Sure!", "Select at least one catagory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkedListBox1.GetItemChecked(0))
            {
                TempCleaner.CleanTemp(ref _totalJunkFiles, ref _bytesCleaned, ref _logFile);
            }
            if (checkedListBox1.GetItemChecked(1))
            {
                
            }
            SaveSettings();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logFile.Flush();
            _logFile.Close();
        }

        private void SaveSettings()
        {
            Settings.Default.TotalJunkCleaned += (_bytesCleaned/1024.0);
            Settings.Default.TotalJunkFilesCleaned += _totalJunkFiles;
            Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really wanna exit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                Close();
            }
        }

        private void statsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Total Junk Size\t: " + (Settings.Default.TotalJunkCleaned/1024.0) + " MBytes\n" +
                            "Total Junk Files\t: " + Settings.Default.TotalJunkFilesCleaned + "\n",
                "Statistics",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}