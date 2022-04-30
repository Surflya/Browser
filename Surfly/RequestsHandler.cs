using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Core;
using CefSharp.Preferences;
using CefSharp.WinForms;
using Surfly.Properties;

namespace Surfly
{
    //Cancel loading of a Url
    public class RequestsHandler : CefSharp.Handler.RequestHandler
    {
        protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            if (request.Url.StartsWith("https://securepubads.g.doubleclick.net/pagead/adview") || request.Url.StartsWith("https://pagead2.googlesyndication.com/pagead"))
            {
                CountBlock();
                return true;
            }
            return base.OnBeforeBrowse(chromiumWebBrowser, browser, frame, request, userGesture, isRedirect);
        }

        void CountBlock() 
        {
            // Informa del bloqueo
            Console.WriteLine("Tracker Blocked");

            // Suma a trackers desactivados
            Settings.Default.BlockedTrackers++;
            Settings.Default.Save();
        }
    }
}
