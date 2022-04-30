using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Core;
using CefSharp.Preferences;
using CefSharp.WinForms;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using Surfly.Properties;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Windows.Data;
using CefSharp.Handler;
using CefSharp.WinForms.Handler;
using Microsoft.Win32;

namespace Surfly
{
    public partial class Form1 : Form
    {
        string sitename;
        bool isPrivateSession;
        bool isFullscreen;
        bool previouslyMaximized;
        public string profileInternalName;
        TabPage tabContextMenuStripTab;
        int tabContextMenuStripTabIndex;


        public Form1(bool callIsFromOnFullscreenModeChange)
        {
            InitializeComponent();

            string[] args = Environment.GetCommandLineArgs();

            // Descarga la imagen de Bing del día

            DownloadBingImage();

            // Detecta si es una sesión privada
            if (args.Contains("private-session"))
            {
                isPrivateSession = true;
                MessageBox.Show("Now you are in private window.");
            }
            else
            {
                isPrivateSession = false;
            }
            Console.WriteLine(isPrivateSession);

            if (!isPrivateSession)
            {
                if (!args.Contains("profile-")) profileInternalName = "Default";
                else
                {
                    profileInternalName = args[0].Replace("not-private-session profile-", "");
                }
                Console.WriteLine(profileInternalName);

                // Crear directorios si estos no existen

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\cache");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\others");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\chromium_resources");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\chromium_locales");
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins\pins.csv")) File.Create(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\Default\usr_data\pins\pins.csv");
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his\his.csv")) File.Create(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\Default\usr_data\his\his.csv");
                if (Settings.Default.DefaultDownloadLocation == "") Settings.Default.DefaultDownloadLocation = KnownFolders.GetPath(KnownFolder.Downloads);

                // Se asegura, para evitar errores, que la llamada no es de OnFullscreenModeChange
                if (!callIsFromOnFullscreenModeChange) 
                {
                    // Cef Settings:

                    CefSettings settings = new CefSettings();
                    settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0) Gecko/20100101 Firefox/98.0";
                    settings.CefCommandLineArgs.Add("enable-reader-mode");
                    settings.CefCommandLineArgs.Add("enable-media-stream");
                    settings.Locale = System.Globalization.CultureInfo.CurrentCulture.DisplayName;
                    settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\cache";
                    settings.UserDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\others";
                    Cef.Initialize(settings);
                }

            }
            else
            {
                // Crear directorios si estos no existen

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\chromium_resources");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\chromium_locales");
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins\pins.Surflydata")) File.Create(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\Default\usr_data\pins\pins.Surflydata");

                if (Settings.Default.DefaultDownloadLocation == "") Settings.Default.DefaultDownloadLocation = KnownFolders.GetPath(KnownFolder.Downloads);

                VPNAction("Connect");

                // Cef Settings:

                CefSettings settings = new CefSettings();
                settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0) Gecko/20100101 Firefox/98.0";
                settings.CefCommandLineArgs.Add("enable-reader-mode");
                settings.CefCommandLineArgs.Add("enable-media-stream");
                settings.Locale = System.Globalization.CultureInfo.CurrentCulture.DisplayName;
                Cef.Initialize(settings);
            }

            // Hacer que el código funcione en algunas partes imposibles:

            CheckForIllegalCrossThreadCalls = false;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = Settings.Default.LastLocation;
            this.Size = Settings.Default.LastSize;
            if (Settings.Default.LastTimeWasMaximized) this.WindowState = FormWindowState.Maximized;
            // Carga "Al iniciar":

            NewTab(true);

            // Carga los pines

            int lines = File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins\pins.Surflydata").Count();
            StreamReader streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins\pins.Surflydata");
            streamReader.ReadToEnd();
            
            for (int i = 0; i < lines/2; i++)
            {
                Console.WriteLine("Item Added");
                toolStrip2.Items.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            streamReader.Dispose();
        }

        private void webBrowser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            // Administra los títulos de pestañas
            string tabTitle = e.Title;
            if (tabTitle.Length > 28)
            {
                tabTitle = tabTitle.Remove(27);
                tabTitle += "…";
            }
            else if (tabTitle.Length < 28)
            {
                var spaces = 32.0 - tabTitle.Length;
                spaces *= 1.2;
                for (int i = 0; i < spaces; i++)
                {
                    tabTitle += " ";
                }
            }
            tabControl.SelectedTab.Text = tabTitle;
            sitename = tabTitle;
        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            // Carga la página indicada en la barra de direcciones
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (!toolStripAddressBar.Text.Contains(" ") && toolStripAddressBar.Text.Contains(".")) 
            {
                web.LoadUrl(toolStripAddressBar.Text); 
            }
            else if (toolStripAddressBar.Text.Contains(" ") || !toolStripAddressBar.Text.Contains("."))
            {
                string search = toolStripAddressBar.Text;
                search.Replace(" ", "+");
                web.LoadUrl("https://www.google.com/search?q=" + search);
            }
        }

        private void toolStripButtonNewTab_Click(object sender, EventArgs e)
        {
            NewTab(false);
        }

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (web != null) 
            {
                if (web.CanGoBack)
                {
                    web.Back();
                }
            }
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (web != null)
            {
                if (web.CanGoForward)
                {
                    web.Forward();
                }
            }
        }

        private void webBrowser_Validated(object sender, EventArgs e)
        {
            
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            // Identifica el navegador de la pestaña seleccionada:
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (web != null)
            {
                // Actualiza la página:
                web.Reload();
            }
        }

        public void NewTab(bool isStart)
        {
            // Crea una pestaña con navegador incluido
            TabPage tab = new TabPage();
            tab.Text = "New Tab                              ";
            tabControl.Controls.Add(tab);
            tabControl.SelectTab(tabControl.TabCount - 1);
            ChromiumWebBrowser webTab = new ChromiumWebBrowser();
            // Establece los parámetros de las nuevas pestañas (pestaña):
            // Establece los parámetros de las nuevas pestañas (navegador):
            webTab.Parent = tab;
            webTab.Dock = DockStyle.Fill;
            if (Settings.Default.SelectDownloadLocationInEveryDownload)
            {
                webTab.DownloadHandler = new SelectedFolderDownloadsHandler();
            }
            else
            {
                webTab.DownloadHandler = new StaticFolderDownloadsHandler();
            }

            if (Settings.Default.BlockTracking) webTab.RequestHandler = new RequestsHandler();
            webTab.DisplayHandler = new WFullScreenDisplayHandler(); 

            // Carga la página de "Nueva pestaña":
            if (isStart)
            {
                webTab.LoadUrl(Settings.Default.AtStartPage);
            }
            else
            {
                string ntp = Environment.CurrentDirectory.Replace(@"\bin\Debug", "") + @"\Resources\InternalPages\NewTab\index.html";
                webTab.LoadUrl(ntp);
            }
            
            // Hace posibles los vacíos:
            webTab.Validated += webBrowser_Validated;
            webTab.TitleChanged += webBrowser_TitleChanged;
            webTab.AddressChanged += webBrowser_AddressChanged;
            webTab.StatusMessage += webBrowser_StatusMessage;
            webTab.KeyDown += Form1_KeyDown;        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab(false);
        }

        private void CloseTab()
        {
            tabControl.TabPages.Remove(tabControl.SelectedTab);
            if (tabControl.TabCount == 0)
            {
                Application.Exit();
            }
        }

        private void toolStripButtonCloseTab_Click(object sender, EventArgs e)
        {
            CloseTab();
        }

        private void webBrowser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            
        }

        private void toolStripStatusLabel_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void webBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            toolStripAddressBar.Text = e.Address.ToString();

            // Añadir al historial
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            StreamReader streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his\his.csv");
            var before = streamReader.ReadToEnd();
            streamReader.Dispose();
            streamReader.Close();
            StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\his\his.csv");
            streamWriter.Write(before + Environment.NewLine + web.Address + ";" + sitename + ";" + DateTime.Now);
            streamWriter.Close();
            streamWriter.Dispose();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            web.Print();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            string name = sitename;
            string page = web.Address;
            FormAddPin formAddPin = new FormAddPin(name, page);
            formAddPin.Show();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            StreamReader streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Surfly Browser\" + profileInternalName + @"\usr_data\pins\pins.Surflydata");
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath);
            System.Diagnostics.Process.Start(info);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.Show();
        }

        private void developerToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            SplitContainer split = new SplitContainer();
            web.Parent = split.Panel2;
            web.ShowDevToolsDocked(split.Panel1,null,DockStyle.Right);
            split.Show();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            Console.WriteLine("Key Down Detected");
            if (e.KeyCode == Keys.F5)
            {
                Console.WriteLine("F5 Key Detected");
                web.Reload();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void newPrivateWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Surfly.exe", "private-session");
        }

        private void toolStripButtonPrivacy_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            FormPrivacyInfo formPrivacyInfo = new FormPrivacyInfo(web.Address.ToString(), sitename, isPrivateSession);
            formPrivacyInfo.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isPrivateSession)
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "ipconfig /flushdns";
                process.StartInfo = startInfo;
                process.Start();
                DialogResult dialogResult = MessageBox.Show(Resources.Bang_All_your_data_now_is_lost, Resources.Private_Session_Ended_D);
            }
            else
            {
                Settings.Default.LastLocation = this.DesktopLocation;
                Settings.Default.LastTimeWasMaximized = this.WindowState == FormWindowState.Maximized;
                if (WindowState != FormWindowState.Maximized) Settings.Default.LastSize = this.Size;
                Settings.Default.Save();
            }
        }

        private static String imgDir = Environment.CurrentDirectory.Replace(@"\bin\Debug", "") + @"\Resources\InternalPages\NewTab\";

        static void DownloadBingImage()
        {
            String imageFileName;
            if (!Directory.Exists(imgDir))
                Directory.CreateDirectory(imgDir);
            {
                imageFileName = imgDir + "bg.png";
                    string response = null;
                    Connect(ref response);
                    ProcessXml(ref response);
                    using (WebClient client = new WebClient())
                        client.DownloadFile("http://www.bing.com" + response + "_1920x1080.jpg", imageFileName);
            }
            //SystemParametersInfo(20, 0, imageFileName, 0x01 | 0x02);
        }

        private static void Connect(ref string res)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://www.bing.com/hpimagearchive.aspx?format=xml&idx=0&n=1&mbl=1&mkt=en-ww");
            webrequest.KeepAlive = false;
            webrequest.Method = "GET";
            using (HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse())
            using (StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream()))
                res = loResponseStream.ReadToEnd();
        }

        private static void ProcessXml(ref string xmlString)
        {
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("urlBase");
                xmlString = reader.ReadElementContentAsString();
            }
        }

        private void shareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripAddressBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                toolStripButtonGo_Click(sender, e);
            }
        }

        public void MakeBrowserFullscreen(bool wholePage)
        {
            if (!isFullscreen)
            {
                ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
                if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                toolStrip1.Hide();
                toolStrip2.Hide();
                web.Parent = this;
                tabControl.Hide();
                isFullscreen = true; 
            }
            else
            {
                ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
                FormBorderStyle = FormBorderStyle.Sizable;
                if (previouslyMaximized)
                {
                    WindowState = FormWindowState.Normal;
                    WindowState = FormWindowState.Maximized;
                }
                else WindowState = FormWindowState.Normal;
                toolStrip1.Show();
                toolStrip2.Show();
                web.Parent = tabControl.SelectedTab;
                tabControl.Show();
                isFullscreen = false;
            }
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeBrowserFullscreen(true);
        }
        
        public void VPNAction(string action)
        {
            if (action == "Connect")
            {
                Console.WriteLine("VPN Connected");
                Process.Start("rasdial.exe", "fr1.vpnbook.com vpnbook 9ntas3c");
            }
            else if (action == "Disconnect")
            {
                Console.WriteLine("VPN Disconnected");
                Process.Start("rasdial.exe", "fr1.vpnbook.com /d");
            }
        }

        private void textBoxSearchOnPage_TextChanged(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (textBoxSearchOnPage.Text != "") web.Find(textBoxSearchOnPage.Text, true, false, false);
            else web.StopFinding(true);
            string source = web.GetSourceAsync().Result;
            Form form = new Form();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            web.Find(textBoxSearchOnPage.Text, true, false, true);
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser web = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            web.Find(textBoxSearchOnPage.Text, false, false, true);
        }

        private void searchOnPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelFind.Show();
        }

        private void buttonCloseSearchOnPage_Click(object sender, EventArgs e)
        {
            panelFind.Hide();
        }

        private void selectProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProfiles formProfiles = new FormProfiles();
            formProfiles.Show();
        }

        public void MakeDefaultBrowser()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\shell\\Associations\\UrlAssociations\\http\\UserChoice", true);
            string browser = regkey.GetValue("Progid").ToString();

            if (browser != "IE.HTTP")
            {
                regkey.SetValue("Progid", "IE.HTTP");
            }
        }

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var tabPage = this.tabControl.TabPages[e.Index];
                var tabRect = this.tabControl.GetTabRect(e.Index);
                // draw Close button to all other TabPages
                var closeImage = new Bitmap(Resources.close_black_24dp);
                e.Graphics.DrawImage(closeImage,
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int TCM_SETMINTABWIDTH = 0x1300 + 49;
        private void tabControl1_HandleCreated(object sender, EventArgs e)
        {
            SendMessage(this.tabControl.Handle, TCM_SETMINTABWIDTH, IntPtr.Zero, (IntPtr)16);
        }

        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("TabControl Clicked");
            // Process MouseDown event only till (tabControl.TabPages.Count - 1) excluding the last TabPage
            for (var i = 0; i < this.tabControl.TabPages.Count; i++)
            {
                var tabRect = this.tabControl.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                var closeImage = new Bitmap(Resources.close_black_24dp);
                var imageRect = new Rectangle(
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                    closeImage.Width,
                    closeImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    this.tabControl.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        private void tabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tabContextMenuStrip.Show(tabControl, e.Location);
                tabContextMenuStripTab = tabControl.SelectedTab;
                tabContextMenuStripTabIndex = tabControl.SelectedIndex;
            }
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Remove(tabContextMenuStripTab);
        }

        private void closeOtherTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int numberOfPages = tabControl.TabPages.Count;
            int nextForsAre1 = 0;
            for (int i = 0; i < numberOfPages; i++)
            {
                tabControl.SelectTab(nextForsAre1);
                if (i != tabContextMenuStripTabIndex)
                {
                    tabControl.TabPages.Remove(tabControl.TabPages[nextForsAre1]);
                }
                else
                {
                    nextForsAre1 = 1;
                }
            }
        }
    }
}