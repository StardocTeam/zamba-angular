using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using Zamba.Core;
using System;
using Microsoft.VisualBasic;
using System.Net;
using Zamba.PreLoad;
using System.ComponentModel;

namespace Zamba.QuickSearch
{
    public class InitQuickSearch
    {
        IChromiumQuickSearch container = null;
        private ChromiumWebBrowser browser;
        public static string errorConsole;
        CefSettings settings = new CefSettings();

        public InitQuickSearch(IChromiumQuickSearch _container)
        {
            container = _container;
        }

        public ChromiumWebBrowser Init()
        {
            CefSettings settings = new CefSettings();
            CefSharp.Cef.Initialize(settings);
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
                    return InitChromium();

                }
                else
                {
                    //Mostrar error de conexion con la base
                    return null;
                }
            }
            else
                return InitChromium();
        }
        private ChromiumWebBrowser InitChromium()
        {
            try
            {
                var cV = new WinFormJSCall.confValues();
                browser = new ChromiumWebBrowser(cV.QuickSearchURL) { Dock = DockStyle.Fill };
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
            errorConsole += e.Message + Constants.vbCr + Constants.vbCr;
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
                case "dosearch":
                    browser.ExecuteScriptAsync("DoQuickSearch('" + value.Trim() + "');");
                    break;
            }
        }
    }
}
