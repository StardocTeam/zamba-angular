using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.Runtime.Serialization.Json;
using System.IO;
using Zamba.Core;
using System.Net;
using Zamba.PreLoad;

namespace Zamba.ZChromium
{
    public class InitZChromium
    {
        public string errorConsole;
        CefSettings settings = new CefSettings();
        private ChromiumWebBrowser browser;
        IChromiumZLink _container = null;
        string _url;
        private InitZChromium()
        {
        }
        public InitZChromium(IChromiumZLink container)
        {
            _container = container;
        }
        public InitZChromium(IChromiumZLink container, string url)
        {
            _url = url;
            _container = container;
        }

        public ChromiumWebBrowser Init()
        {
            if (!Cef.IsInitialized)
                CefSharp.Cef.Initialize(settings);
            string status = string.Empty;
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                DBBusiness.InitializeSystem(ObjectTypes.Cliente, null, false, ref status, new ErrorReportBusiness());
                try { 
                if ((bool.Parse(UserPreferences.getValueForMachine("UseMasterCredentials", UPSections.UserPreferences, false).ToString())))
                {
                    NetworkCredential MasterCredentials = new NetworkCredential();
                    MasterCredentials.Domain = ZOptBusiness.GetValueOrDefault("Domain", "pseguros.com");
                    MasterCredentials.UserName = ZOptBusiness.GetValueOrDefault("DomainUserName", "stardocservice");
                    MasterCredentials.Password = ZOptBusiness.GetValueOrDefault("DomainPasswordEncrypted", "sodio2017_nzt");
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
            }
            var cV = new WinFormJSCall.ConfValuesZC();
            browser = new ChromiumWebBrowser((_url == null || _url == string.Empty)?cV.homeWidgetURL:_url) { Dock = DockStyle.Fill };
            var wFJS = new WinFormJSCall(_container);
            browser.RegisterAsyncJsObject("winFormJSCall", wFJS);
            browser.ConsoleMessage += Browser_ConsoleMessage;
            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;
            return browser;
        }
        private void Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            ZClass.raiseerror(new Exception(string.Format("Line: {0}, Source: {1}, Message: {2}", e.Line, e.Source, e.Message)));
        }


        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
           // this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
          //  this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (args.Title == "The resource cannot be found.") {
                // _container.InvokeOnUiThreadIfRequired(() => _container.HideBrowser());
                _container.HideBrowser();
            }
           // this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
          //  this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }
    }

    public class WinFormJSCall
    {
        IChromiumZLink _container = null;
        private WinFormJSCall()
        {
        }
        public WinFormJSCall(IChromiumZLink container)
        {
            _container = container;
        }

        public void openTask(int docTypeId, int docId, int taskId, int stepId)
        {
            try
            {
                _container.SelectResult(docTypeId, docId, taskId, stepId);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public string confVal()
        {
            var js = new DataContractJsonSerializer(typeof(ConfValuesZC));
            var ms = new MemoryStream();
            var cVZC = new ConfValuesZC();
            js.WriteObject(ms, cVZC);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            return jsonString;
        }
        public class ConfValuesZC
        {
            public string homeWidgetURL;
            public string zambaWebRestApiURL;
            // public string zambaWebURL;// para carga de recursos chat
            public string chatWidgetURL;
            public string homeWidgetDomain;
            //  public string zURLService;
            public bool enableGlobalSearch;
            public bool enableChat;
            public long userIdGS;
            public ConfValuesZC()
            {
                try
                {
                    homeWidgetURL = ZOptBusiness.GetValue("HomeWidgetURL");
                    zambaWebRestApiURL = ZOptBusiness.GetValue("ZambaWebRestApiURL");
                    //  zambaWebURL = ZOptBusiness.GetValue("ZambaWebURL");
                    chatWidgetURL = ZOptBusiness.GetValue("ChatWidgetURL");
                    homeWidgetDomain = ZOptBusiness.GetValue("HomeWidgetDomain");
                    enableGlobalSearch = ZOptBusiness.GetValue("EnableGlobalSearchWidget").ToUpper() == "TRUE" ? true : false;
                    enableChat = ZOptBusiness.GetValue("EnableChatWidget").ToUpper() == "TRUE" ? true : false;
                    // zURLService = ZOptBusiness.GetValue("ZURLService"); Ver si es necesario para chat
                    userIdGS = Membership.MembershipHelper.CurrentUser.ID;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }
    }

    public class ZChromium
    {
        public enum ApplicationType
        {
            ZambaLink,
            GlobalSearch, 
            ZambaNews,
            SuggestedPeople
        }
    }
}
