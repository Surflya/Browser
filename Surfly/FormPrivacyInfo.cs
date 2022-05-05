using Surfly.Properties;
using System;
using System.Windows.Forms;

namespace Surfly
{
    public partial class FormPrivacyInfo : Form
    {
        string siteAddress;
        string siteName;
        bool isPrivateSession;
        public FormPrivacyInfo(string siteAddressP, string siteNameP, bool isPrivateSessionP)
        {
            InitializeComponent();
            siteAddress = siteAddressP;
            siteName = siteNameP;
            isPrivateSession = isPrivateSessionP;
        }

        private void FormPrivacyInfo_Load(object sender, EventArgs e)
        {
            if (isPrivateSession) panel1.Show();
            else panel1.Hide();
            labelTrackers.Text = "Surfly has blocked " + Settings.Default.BlockedTrackers + " trackers from the install date.";
        }
    }
}
