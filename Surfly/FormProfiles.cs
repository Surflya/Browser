using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Surfly
{
    public partial class FormProfiles : Form
    {
        public FormProfiles()
        {
            InitializeComponent();
        }

        private void buttonCustomProfle_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "not-private-session profile-" + textBoxCustomProfile);
        }
    }
}
