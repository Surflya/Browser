using CefSharp;
using System;
using System.IO;
using Surfly.Properties;

namespace Surfly
{
    public class StaticFolderDownloadsHandler : IDownloadHandler
    {
        Form1 form1 = new Form1(true);

        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (downloadItem.IsValid)
            {
                Console.WriteLine("== File information ========================");
                Console.WriteLine(" File URL: {0}", downloadItem.Url);
                Console.WriteLine(" Suggested FileName: {0}", downloadItem.SuggestedFileName);
                Console.WriteLine(" MimeType: {0}", downloadItem.MimeType);
                Console.WriteLine(" Content Disposition: {0}", downloadItem.ContentDisposition);
                Console.WriteLine(" Total Size: {0}", downloadItem.TotalBytes);
                Console.WriteLine("============================================");

            }

            OnBeforeDownloadFired?.Invoke(this, downloadItem);

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    // Define the Downloads Directory Path
                    // You can use a different one, in this example we will hard-code it
                    string DownloadsDirectoryPath = "C:\\Users\\" + "fyrub" + "\\Downloads\\";

                    callback.Continue(
                        Path.Combine(
                            DownloadsDirectoryPath,
                            downloadItem.SuggestedFileName
                        ),
                        showDialog: false
                    );
                }
            }
        }

        /// https://cefsharp.github.io/api/51.0.0/html/T_CefSharp_DownloadItem.htm
        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            OnDownloadUpdatedFired?.Invoke(this, downloadItem);

            if (downloadItem.IsValid)
            {
                // Show progress of the download
                if (downloadItem.IsInProgress && (downloadItem.PercentComplete != 0))
                {
                    Console.WriteLine(
                        "Current Download Speed: {0} bytes ({1}%)",
                        downloadItem.CurrentSpeed,
                        downloadItem.PercentComplete
                    );
                }

                if (downloadItem.IsComplete)
                {
                    Console.WriteLine("The download has been finished !");
                }
            }
        }
    }
}
