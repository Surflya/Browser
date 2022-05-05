using CefSharp;
using Surfly;
using Surfly.Properties;
using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Speech.Synthesis;
using System.Web;
using System.Windows.Forms;

public class MenuHandler : IContextMenuHandler
{
    public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
    {
        model.Clear();
        if (parameters.LinkUrl != "")
        {
            model.AddItem((CefMenuCommand)26515, "Open On New Tab");
            model.AddItem((CefMenuCommand)26516, "Open On New Window");
            model.AddItem((CefMenuCommand)26517, "Copy Link");
            model.AddSeparator();
        }
        if (parameters.HasImageContents)
        {
            model.AddItem((CefMenuCommand)26518, "Open Image");
            model.AddItem((CefMenuCommand)26519, "Open Image On New Tab");
            model.AddItem((CefMenuCommand)26520, "Open Image On New Window");
            model.AddItem((CefMenuCommand)26521, "Open Image On New Private Window");
            model.AddItem((CefMenuCommand)26522, "Download Image");
            model.AddItem((CefMenuCommand)26523, "Copy Image Address");
            model.AddItem((CefMenuCommand)26524, "Copy Image");
            model.AddItem((CefMenuCommand)26525, "Set Image As Desktop Wallpaper");
            model.AddItem((CefMenuCommand)26507, "Inspect Element");
        }
        else if (parameters.SelectionText == "")
        {
            model.AddItem((CefMenuCommand)26501, "Back");
            model.AddItem((CefMenuCommand)26502, "Forward");
            model.AddItem((CefMenuCommand)26503, "Refresh");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26505, "Print");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26506, "View Source Code");
            model.AddItem((CefMenuCommand)26507, "Inspect Element");
        }
        else if (parameters.SelectionText != "")
        {
            model.AddItem((CefMenuCommand)26508, "Copy");
            model.AddItem((CefMenuCommand)26509, "Copy Link With Highlight");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26510, "Search \"" + parameters.SelectionText + "\"");
            model.AddItem((CefMenuCommand)26511, "Define \"" + parameters.SelectionText + "\"");
            model.AddItem((CefMenuCommand)26514, "Translate \"" + parameters.SelectionText + "\"");
            model.AddItem((CefMenuCommand)26513, "Read Selection Aloud");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26507, "Inspect Element");
        }

    }

    public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
    {
        if (commandId == (CefMenuCommand)26501)
        {
            browser.Back();
            return true;
        }

        if (commandId == (CefMenuCommand)26502)
        {
            browser.Forward();
            return true;
        }

        if (commandId == (CefMenuCommand)26503)
        {
            browser.Reload();
            return true;
        }

        if (commandId == (CefMenuCommand)26505)
        {
            browser.GetHost().Print();
            return true;
        }

        if (commandId == (CefMenuCommand)26506)
        {
            browser.ViewSource();
            return true;
        }

        if (commandId == (CefMenuCommand)26507)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26508)
        {
            Clipboard.SetText(parameters.SelectionText);
            return true;
        }

        if (commandId == (CefMenuCommand)26509)
        {
            string link = parameters.PageUrl + "#:~:text=" + parameters.SelectionText;
            link = link.Replace(" ", "%20");
            Clipboard.SetText(link);
            return true;
        }

        if (commandId == (CefMenuCommand)26510)
        {
            Form1 form1 = new Form1(true);
            form1.NewTab(false, Settings.Default.SearchUrl + parameters.SelectionText);
            return true;
        }

        if (commandId == (CefMenuCommand)26511)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26513)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(parameters.SelectionText);
            return true;
        }

        if (commandId == (CefMenuCommand)26514)
        {
            var toLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower();
            var fromLanguage = "autodetect";
            string Translate()
            {
                string word = parameters.SelectionText;
                var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
                var webClient = new WebClient
                {
                    Encoding = System.Text.Encoding.UTF8
                };
                var result = webClient.DownloadString(url);
                try
                {
                    result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                    return result;
                }
                catch
                {
                    return "Error";
                }
            }
            Form form = new Form();
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            form.Size = new System.Drawing.Size(500, 500);
            GroupBox groupBox1 = new GroupBox();
            groupBox1.Size = new System.Drawing.Size(450, 250);
            groupBox1.Parent = form;
            groupBox1.Location = new System.Drawing.Point(15, 0);
            groupBox1.Text = "Original";
            groupBox1.Show();
            GroupBox groupBox2 = new GroupBox();
            groupBox2.Size = new System.Drawing.Size(450, 250);
            groupBox2.Parent = form;
            groupBox2.Location = new System.Drawing.Point(15, 250);
            groupBox2.Text = CultureInfo.CurrentCulture.DisplayName;
            groupBox2.Show();
            Label label1 = new Label();
            label1.Parent = groupBox1;
            label1.Location = new System.Drawing.Point(15, 20);
            label1.Text = parameters.SelectionText;
            label1.Font = new System.Drawing.Font("Arial Black", 18);
            label1.AutoSize = false;
            label1.Size = new Size(420, 220);
            label1.Show();
            Label label2 = new Label();
            label2.Parent = groupBox2;
            label2.Location = new System.Drawing.Point(15, 20);
            label2.Text = Translate();
            label2.Font = new System.Drawing.Font("Arial Black", 18);
            label2.AutoSize = false;
            label2.Size = new Size(420, 220);
            label2.Show();
            form.Show();
            return true;
        }

        if (commandId == (CefMenuCommand)26515)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26516)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26517)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26518)
        {
            browserControl.LoadUrl(parameters.SourceUrl);
            return true;
        }

        if (commandId == (CefMenuCommand)26519)
        {
            Form1 form1 = new Form1(true);
            form1.NewTab(false, parameters.SourceUrl);
            return true;
        }

        if (commandId == (CefMenuCommand)26520)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26521)
        {
            browser.GetHost().ShowDevTools(null, parameters.XCoord, parameters.YCoord);
            return true;
        }

        if (commandId == (CefMenuCommand)26522)
        {
            browser.GetHost().StartDownload(parameters.SourceUrl);
            return true;
        }

        if (commandId == (CefMenuCommand)26523)
        {

            return true;
        }

        if (commandId == (CefMenuCommand)26524)
        {
            browser.GetHost().StartDownload(parameters.SourceUrl);
            return true;
        }

        // Return false should ignore the selected option of the user !
        return false;
    }

    public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
    {

    }

    public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
    {
        return false;
    }
}