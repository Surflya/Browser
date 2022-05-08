using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Surfly
{
    public partial class FormHistory : Form
    {
        string profileInternalName = "Default";
        StreamReader streamReader;
        int lineCount;

        public FormHistory(string profileInternalNameTR)
        {
            InitializeComponent();
            lineCount = File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his\his.csv").Count();
            streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his\his.csv");
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < lineCount; i++)
            {
                string[] dividers = { ";", ";" };
                string[] line = streamReader.ReadLine().Split(dividers, StringSplitOptions.None);
                ListViewItem listViewItem = new ListViewItem(line[1]);
                listViewItem.ToolTipText = line[0];
                ListViewGroup listViewGroup = new ListViewGroup();
                if (!listView1.Groups.Contains(listViewGroup))
                {
                    listView1.Groups.Add(listViewGroup);
                    listViewItem.Group = listViewGroup;
                }
                else
                {
                    foreach (ListViewGroup group in listView1.Groups)
                    {
                        if (group.ToString() == listViewGroup.ToString())
                        {
                            listViewItem.Group = group;
                        }
                    }
                }
                listView1.Items.Add(listViewItem);
            }
        }
    }
}
