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
using System.IO;

namespace Surfly
{
    //Cancel loading of a Url
    public class RequestsHandler : CefSharp.Handler.RequestHandler
    {
        protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            Form1 form1 = new Form1(true);
            Console.WriteLine(SimplifyUrl(request.Url));
            try
            {
                if (form1.trackers_blocklist.Contains(SimplifyUrl(request.Url)))
                {
                    CountBlock();
                    return true;
                }
            }
            catch (Exception)
            {
                
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

        public static String SimplifyUrl(String url)
        {
            return url;
        }
    }
}
