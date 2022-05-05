// Multi-Language may add the project MLRuntime to your solution and add a reference
// to MLRuntime to your project. This supports language switching across multiple DLLs.
// In this case it will change "undef" to "define" in the following statement.
#undef USE_PROJECT_MLRUNTIME

using System;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace MultiLang
{
    public partial class SelectLanguage : Form
    {
        public SelectLanguage()
        {
            InitializeComponent();
        }

        //----------------------------------------------
        //Enums
        //----------------------------------------------
        public enum enumStartupMode
        {
            UseDefaultCulture = 0,
            UseSavedCulture = 1,
            ShowDialog = 2
        }

        private enum enumCultureMatch
        {
            None = 0,
            Language = 1,
            Neutral = 2,
            Region = 3
        }

        //----------------------------------------------
        //Member Variables
        //----------------------------------------------
        private enumStartupMode StartupMode;
        private CultureInfo SelectedCulture;

        // The array of supported cultures is updated automatically by Multi-Language for Visual Studio
        private static string[] SupportedCultures = { "en", "es" }; //MLHIDE

        //----------------------------------------------
        //Public Methods
        //----------------------------------------------
        public void LoadSettingsAndShow()
        {
            LoadSettingsAndShow(false);
        }

        public void LoadSettingsAndShow(Boolean ForceShow)
        {
            LoadSettings();

            if (ForceShow || (StartupMode == enumStartupMode.ShowDialog))
            {
                this.ShowDialog();
                SaveSettings();
            }

            if (StartupMode != enumStartupMode.UseDefaultCulture)
            {
                if (SelectedCulture != null)
                {
                    // Actually change the culture of the current thread.
                    Thread.CurrentThread.CurrentUICulture = SelectedCulture;

                    if (ForceShow)
                    {
#if USE_PROJECT_MLRUNTIME
            MLRuntime.MLRuntime.BroadcastLanguageChanged() ;
#else
                        // The code generated by VS.NET cannot be used to change the
                        // language of an active form. Show a message to this effect.
                        MessageBox.Show("The settings have been saved.\n" +
                                        "The language change will take full effect the next time you start the program.",
                                        "Select language",
                                        MessageBoxButtons.OK);
#endif
                    }
                }
            }
        }

        //----------------------------------------------
        //Private Methods
        //----------------------------------------------

        //
        // SaveSettings and LoadSettings use an XML file, saved in so called
        // Isolated Storage.
        //
        // I'm not convinced that this is really the best way or the best place
        // to store this information, but it's certainly a .NET way to do it.
        //
        private void LoadSettings()
        {
            // Set the defaults
            StartupMode = enumStartupMode.ShowDialog;
            SelectedCulture = Thread.CurrentThread.CurrentUICulture;

            // Create an IsolatedStorageFile object and get the store
            // for this application.
            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForDomain();

            // Check whether the file exists
            if (isoStorage.GetFileNames("CultureSettings.xml").Length > 0) //MLHIDE
            {
                // Create isoStorage StreamReader.
                StreamReader stmReader = new StreamReader
                                             (new IsolatedStorageFileStream
                                                   ("CultureSettings.xml",
                                                    FileMode.Open,
                                                    isoStorage)); //MLHIDE

                XmlTextReader xmlReader = new XmlTextReader(stmReader);

                // Loop through the XML file until all Nodes have been read and processed.
                while (xmlReader.Read())
                {
                    switch (xmlReader.Name)
                    {
                        case "StartupMode":                                         //MLHIDE
                            StartupMode = (enumStartupMode)int.Parse(xmlReader.ReadString());
                            break;
                        case "Culture":                                             //MLHIDE
                            String CultName = xmlReader.ReadString();
                            CultureInfo CultInfo = new CultureInfo(CultName);
                            SelectedCulture = CultInfo;
                            break;
                    }
                }

                // Close the reader
                xmlReader.Close();
                stmReader.Close();

            }

            isoStorage.Close();

        }

        private void SaveSettings()
        {

            // Get an isolated store for user, domain, and assembly and put it into
            // an IsolatedStorageFile object.
            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForDomain();

            // Create isoStorage StreamWriter and assign it to an XmlTextWriter variable.
            IsolatedStorageFileStream stmWriter = new IsolatedStorageFileStream("CultureSettings.xml", FileMode.Create, isoStorage); //MLHIDE
            XmlTextWriter writer = new XmlTextWriter(stmWriter, Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("CultureSettings");                     //MLHIDE
            writer.WriteStartElement("StartupMode");                         //MLHIDE
            writer.WriteString(((int)StartupMode).ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Culture");                             //MLHIDE
            writer.WriteString(SelectedCulture.Name);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();

            stmWriter.Close();
            isoStorage.Close();
        }

        private void SelectLanguage_Load(object sender, System.EventArgs e)
        {
            enumCultureMatch Match = enumCultureMatch.None;
            enumCultureMatch NewMatch = enumCultureMatch.None;

            // Version 1 detected which subdirectories are present.

            //      String AsmLocation             = Assembly.GetExecutingAssembly ( ).Location;
            //      String AsmPath                 = Path.GetDirectoryName ( AsmLocation );
            //      List<String> DirList           = new List<String> ( );
            //
            //      DirList.AddRange ( Directory.GetDirectories ( AsmPath, "??" ) );
            //      DirList.AddRange ( Directory.GetDirectories ( AsmPath, "??-??*" ) );
            //
            //      foreach ( String SubDirName in DirList )
            //      {
            //        try
            //        {
            //          String BaseName = Path.GetFileName ( SubDirName );
            //          CultureInfo Cult     = new CultureInfo ( BaseName );

            // Version 2 used the SupportedCultures array in MlString.h,
            // which is autoamatically updated by Multi-Language for Visual Studio
            //      foreach ( String IetfTag in ml.SupportedCultures )

            // Version 3 uses the SupportedCultures array in this file,
            // which is autoamatically updated by Multi-Language for Visual Studio
            foreach (String IetfTag in SupportedCultures)
            {
                try
                {
                    var Cult = new CultureInfo(IetfTag);
                    var CIWrap = new CIWrapper(Cult);

                    // Note: The property lstCultures.DisplayName is set to "NativeName" in order to
                    //       show language name in its own language.
                    lstCultures.Items.Add(CIWrap);

                    // The rest of this logic is just to find the nearest match to the
                    // current UI culture.
                    // How well does this culture match?
                    if (SelectedCulture.Equals(Cult))
                    {
                        NewMatch = enumCultureMatch.Region;
                    }
                    else if (Cult.TwoLetterISOLanguageName == SelectedCulture.TwoLetterISOLanguageName)
                    {
                        if (Cult.IsNeutralCulture)
                            NewMatch = enumCultureMatch.Neutral;
                        else
                            NewMatch = enumCultureMatch.Language;
                    }

                    // Is that better than the best match so far?
                    if (NewMatch > Match)
                    {
                        Match = NewMatch;
                        lstCultures.SelectedItem = CIWrap;
                    }
                }
                catch
                {
                }
            }

            switch (StartupMode)
            {
                case enumStartupMode.ShowDialog:
                    rbShow.Checked = true;
                    break;
                case enumStartupMode.UseDefaultCulture:
                    rbDefault.Checked = true;
                    break;
                case enumStartupMode.UseSavedCulture:
                    rbSelected.Checked = true;
                    break;
            }

        }

        private void btOK_Click(object sender, System.EventArgs e)
        {
            if (lstCultures.SelectedItem != null)
            {
                SelectedCulture = ((CIWrapper)lstCultures.SelectedItem).CInfo;
            }
            this.Close();
        }

        private void OnStartup_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbShow.Checked)
                StartupMode = enumStartupMode.ShowDialog;
            else if (rbSelected.Checked)
                StartupMode = enumStartupMode.UseSavedCulture;
            else if (rbDefault.Checked)
                StartupMode = enumStartupMode.UseDefaultCulture;
        }

    }

    public class CIWrapper
    {
        public CultureInfo CInfo { get; private set; }

        public CIWrapper(CultureInfo ci)
        {
            CInfo = ci;
        }

        public string NativeNameCapitalized => CInfo.TextInfo.ToTitleCase(CInfo.NativeName);

        //public static explicit operator CultureInfo ( CIWrapper w )
        //{
        //  return w.CInfo;
        //}
    }
}