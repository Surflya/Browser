using Surfly.Properties;
using System;
using System.Windows.Forms;

namespace Surfly
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) Settings.Default.SelectDownloadLocationInEveryDownload = true;
            else Settings.Default.SelectDownloadLocationInEveryDownload = false;
            Settings.Default.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            DialogResult dialogResult = MessageBox.Show("");
            if (dialogResult == DialogResult.OK) Application.Restart();
            else Close();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            comboBox1.Text = Settings.Default.DefaultSearchEngine;
        }

        public void ResetBrowser(object sender, EventArgs e)
        {

        }
    }
}
