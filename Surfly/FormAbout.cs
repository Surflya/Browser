using System.Windows.Forms;
using System.IO;
using System;

namespace Surfly
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, System.EventArgs e)
        {
            StreamReader sr = new StreamReader(Environment.CurrentDirectory + @"\AppVersion.txt");
            label1.Text.Replace("[version]", sr.ReadLine());
            sr.Close();
            sr.Dispose();
        }
    }
}
