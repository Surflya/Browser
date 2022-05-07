using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;

namespace Surfly
{
    public partial class FormHistory : Form
    {
        string profileInternalName = "Default";
        StreamReader streamReader;
        CsvReader csvReader;

        public FormHistory(string profileInternalNameTR)
        {
            InitializeComponent();
            streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his\his.csv");
            csvReader = new CsvReader(streamReader);
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            
        }
    }
}
