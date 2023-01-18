using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using Zamba.Core;
using System;
using Microsoft.VisualBasic;
using static ZambaLink.Frm_ZLink;
using static Zamba.ZChromium.ZChromium;
using System.Net;
using Zamba.PreLoad;
using System.ComponentModel;

namespace Zamba.Link
{
    public class InitZLink
    {
        IChromiumZLink container = null;
        private ChromiumWebBrowser browser;
        public static string errorConsole;
        CefSettings settings = new CefSettings();
        public static ConfValues cV = new ConfValues();

        public InitZLink(IChromiumZLink _container)
        {
            container = _container;
        }

        public ChromiumWebBrowser Init(ApplicationType at)
        {
            var settings = new CefSettings();
            if (!Cef.IsInitialized)
                Cef.Initialize(settings);
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                string status = string.Empty;
                if (DBBusiness.InitializeSystem(ObjectTypes.Cliente, null, true, ref status, new ErrorReportBusiness()))
                {
                    try
                    {

                    if ((bool.Parse(UserPreferences.getValueForMachine("UseMasterCredentials", UPSections.UserPreferences, false).ToString())))
                    {
                        NetworkCredential MasterCredentials = new NetworkCredential();
                        MasterCredentials.Domain = ZOptBusiness.GetValueOrDefault("Domain", "pseguros.com");
                        MasterCredentials.UserName = ZOptBusiness.GetValueOrDefault("DomainUserName", "stardocservice");
                        MasterCredentials.Password = ZOptBusiness.GetValueOrDefault("DomainPassword", "sodio2017_nzt");
                        string ServerShare = ZOptBusiness.GetValueOrDefault("DomainServerShare", "\\\\svrimage\\zamba");
                        NetworkConnection NetworkConnection = new NetworkConnection(ServerShare, MasterCredentials);
                    }
                    }
                    catch (Win32Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                    return InitChromium(at);

                }
                else
                {
                    //Mostrar error de conexion con la base
                    return null;
                }
            }
            else
                return InitChromium(at);
        }
        private ChromiumWebBrowser InitChromium(ApplicationType at)
        {
            try
            {
                //  var cV = new ConfValues();
                var url = string.Empty;
                switch (at)
                {
                    case ApplicationType.ZambaLink:
                        url = cV.ZLinkURL;
                        break;
                    case ApplicationType.GlobalSearch:
                        url = cV.GlobalSearchURL;
                        break;
                    case ApplicationType.SuggestedPeople:
                        if (!(cV.ZLuserExtId == 0))
                        {
                            url = cV.ZLCollaborationServer + "/suggestedpeople?userId=" + cV.ZLuserExtId;
                        }
                        
                        //  url = "http://localhost/Zambachat7/suggestedpeople?userId=95";
                        break;
                }

                browser = new ChromiumWebBrowser(url) { Dock = DockStyle.Fill };
                var wFJS = new WinFormJSCall(container);
                //Para llamadas desde JS ej   winFormJSCall.action("shake");
                browser.RegisterAsyncJsObject("winFormJSCall", wFJS);
                browser.ConsoleMessage += Browser_ConsoleMessage;
                return browser;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        private void Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            errorConsole += (e.Message + Constants.vbCr).Replace("\n", Environment.NewLine);
        }
        public static string GetConsole()
        {
            return errorConsole;
        }

        //Llamar funciones de JavaScript
        public static void ExecuteJSFunction(ChromiumWebBrowser browser, string function, string value = "")
        {
            switch (function.ToLower())
            {
                case "maximize":
                    browser.ExecuteScriptAsync("fullScreenFromZlink();");
                    break;
                case "restore":
                    browser.ExecuteScriptAsync("restoreFromZlink();");
                    break;
                case "minimize":
                    browser.ExecuteScriptAsync("minimizeFromZlink();");
                    break;
                case "refresh":
                    browser.ExecuteScriptAsync("RefreshChatPage();");
                    break;
                case "js":
                    browser.ExecuteScriptAsync(value);
                    break;
            }
        }
    }
}
