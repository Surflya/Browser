using CefSharp;
using CefSharp.Enums;
using CefSharp.Structs;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Surfly
{
    public class WFullScreenDisplayHandler : IDisplayHandler
    {
        private Control parent;


        void IDisplayHandler.OnAddressChanged(IWebBrowser browserControl, AddressChangedEventArgs addressChangedArgs)
        {
        }
        void IDisplayHandler.OnTitleChanged(IWebBrowser browserControl, TitleChangedEventArgs titleChangedArgs)
        {
        }
        void IDisplayHandler.OnFaviconUrlChange(IWebBrowser browserControl, IBrowser browser, IList<string> urls)
        {
        }
        void IDisplayHandler.OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            if (fullscreen)
            {
                parent = chromiumWebBrowser.Parent;
                chromiumWebBrowser.FindForm().FormBorderStyle = FormBorderStyle.None;
                chromiumWebBrowser.FindForm().WindowState = FormWindowState.Maximized;
                chromiumWebBrowser.Parent = chromiumWebBrowser.FindForm();

                chromiumWebBrowser.BringToFront();
            }
            else
            {
                chromiumWebBrowser.FindForm().FormBorderStyle = FormBorderStyle.Sizable;
                chromiumWebBrowser.FindForm().WindowState = FormWindowState.Normal;
                chromiumWebBrowser.Parent = parent;
            }
        }
        void IDisplayHandler.OnStatusMessage(IWebBrowser browserControl, StatusMessageEventArgs statusMessageArgs)
        {
        }
        bool IDisplayHandler.OnConsoleMessage(IWebBrowser browserControl, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return false;
        }

        bool IDisplayHandler.OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, Size newSize)
        {
            return false;
        }

        bool IDisplayHandler.OnCursorChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr cursor, CursorType type, CursorInfo customCursorInfo)
        {
            return false;
        }

        void IDisplayHandler.OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {

        }

        bool IDisplayHandler.OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }
    }
}