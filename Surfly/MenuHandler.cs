using System;
using CefSharp;
using System.Windows.Forms;

public class MenuHandler : IContextMenuHandler
{
    public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
    {
        model.Clear();
        model.AddItem((CefMenuCommand)26501, "Back");
        model.AddItem((CefMenuCommand)26502, "Forward");
        model.AddItem((CefMenuCommand)26503, "Refresh");
        model.AddSeparator();
        model.AddItem((CefMenuCommand)26505, "Print");
        model.AddSeparator();
        model.AddItem((CefMenuCommand)26506, "View Source Code");
        model.AddItem((CefMenuCommand)26507, "Inspect Element");
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
            browser.GetHost().ShowDevTools();
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