using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp.Internals;
using CefSharp;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Windows.Controls;
using Zamba;
using Zamba.Core;
using Zamba.AppBlock;
using System.Reflection;

namespace Zamba.ZChromium
{
    public partial class GraphViewer : ZControl
    {

        ZLabel errorConsole;

        CefSettings settings = new CefSettings();
        private ChromiumWebBrowser browser;
        IChromiumZLink _container = null;
        private GraphViewer()
        {
            InitializeComponent();
        }
        public GraphViewer(IChromiumZLink container)
        {
            Form ErrorsForm = new Form();
            errorConsole = new ZLabel();
            errorConsole.Dock = DockStyle.Fill;
            ErrorsForm.Controls.Add(errorConsole);
            ErrorsForm.Show();

            InitializeComponent();
            _container = container;
            Init();

            var assembly = Assembly.GetExecutingAssembly();

            var factory = (DefaultResourceHandlerFactory)
           (browser.ResourceHandlerFactory);
            if (factory == null) return;
            var response = ResourceHandler
                .FromStream(LoadResource("views.flot-charts.html"));
            factory.RegisterHandler("http://local/", response);


            // Get the list of resources
            var resourceNames = assembly.GetManifestResourceNames();

            // For each resource
            foreach (var resource in resourceNames)
            {
                // If it isn't in the "web" namespace, skip it.
                if (!resource.StartsWith("Zamba.ZChromium.web"))
                    continue;

                // Strip out the namespace that we don't need.
                var url = resource.Replace
                    ("Zamba.ZChromium.web.", "");

                // Function I made that turns the 
                // resource into a textStream
                var r = LoadResource(url);

                // Make the namespace look like a path
                url = url.Replace(".", "/");
                var lastSlash = url.LastIndexOf("/",
                    StringComparison.Ordinal);
                url = url.Substring(0, lastSlash) + "." +
                    url.Substring(lastSlash + 1);

                // Register the response with the URL
                factory.RegisterHandler("http://local/" + url,
                    ResourceHandler.FromStream(r));
            }

            browser
.RegisterJsObject("salesObject",
    salesObject);

            browser.Load("http://local/views/flot-charts.html");

        
            
    }

        private SalesObject salesObject = new SalesObject();
        private class SalesObject
        {
            private string salesData = "[[1196463600000, 340],        [1196550000000, 3430],        [1196636400000, 240],        [1196722800000, 577],        [1196809200000, 3636],        [1196895600000, 3575],        [1196982000000, 2736],        [1197068400000, 1086],        [1197154800000, 676]    ]";

            public string SalesData
            {
                get
                {
                    return salesData;
                }
                set
                {
                    salesData = value;
       
                }
            }
        }

        static Stream LoadResource(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var textStream = assembly
                .GetManifestResourceStream("Zamba.ZChromium.web."
                    + filename);
            return textStream;
        }

        public ChromiumWebBrowser Init()
        {



            if (!Cef.IsInitialized)
                CefSharp.Cef.Initialize(settings);
            string status = string.Empty;
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                DBBusiness.InitializeSystem(ObjectTypes.Cliente, null, false, ref status, new ErrorReportBusiness());
            }
            var cV = new GVWinFormJSCall.GVConfValuesZC();
            browser = new ChromiumWebBrowser(cV.GraphViewerURL) { Dock = DockStyle.Fill };



            //var wFJS = new GVWinFormJSCall(_container);
            //browser.RegisterAsyncJsObject("winFormJSCall", wFJS);
            browser.ConsoleMessage += Browser_ConsoleMessage;
            Controls.Add(browser);
            return browser;
        }
        private void Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            dLogError dl = new dLogError(LogError);
            this.Invoke(dl, new object[] { e.Message });
        }
        public delegate void dLogError(String Error);

        private void LogError(string Error)
        {
            errorConsole.Text += Error + " // ";
        }



        public void ShutDown()
        {
            Cef.Shutdown();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {

            salesObject.SalesData = "[[1196463600000, 16240],        [1196550000000, 13430],        [1196636400000, 11240],        [1196722800000, 11577],        [1196809200000, 3636],        [1196895600000, 113575],        [1196982000000, 112736],        [1197068400000, 111086],        [1197154800000, 11676]    ]";
            
            browser.ExecuteScriptAsync("angular.element(document.getElementById('flotSalesChart')).scope().salesChartData = '" + salesObject.SalesData + "';angular.element(document.getElementById('widget-grid')).scope().$digest();angular.element(document.getElementById('widget-grid')).scope().$apply();");


        }
    }

    public class GVWinFormJSCall
    {
        IChromiumZLink _container = null;
        private GVWinFormJSCall()
        {
        }
        public GVWinFormJSCall(IChromiumZLink container)
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

        public string GVconfVal()
        {
            var js = new DataContractJsonSerializer(typeof(GVConfValuesZC));
            var ms = new MemoryStream();
            var cVZC = new GVConfValuesZC();
            js.WriteObject(ms, cVZC);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            return jsonString;
        }

        public class GVConfValuesZC
        {
            public string GraphViewerURL;
            public long userIdGS;
            public GVConfValuesZC()
            {
                try
                {
                    GraphViewerURL = "http://www.google.com";
                    userIdGS = Membership.MembershipHelper.CurrentUser.ID;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }

    }
}
