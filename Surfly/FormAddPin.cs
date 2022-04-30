using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Surfly
{
    public partial class FormAddPin : Form
    {
        string name;
        string page;
        public FormAddPin(string namefo, string pagefo)
        {
            InitializeComponent();
            name = namefo;
            page = pagefo;
        }

        private void FormAddPin_Load(object sender, EventArgs e)
        {
            textBoxName.Text = name;
            textBoxAddress.Text = page;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            StreamReader streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\Default\usr_data\pins\pins.csv");
            string actualFavourites = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            name = textBoxName.Text;
            page = textBoxAddress.Text;
            StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\Default\usr_data\pins\pins.csv");
            streamWriter.Write(actualFavourites);
            streamWriter.WriteLine(page + ";" + name);
            streamWriter.Close();
            streamWriter.Dispose();
            Close();
        }
    }
}
