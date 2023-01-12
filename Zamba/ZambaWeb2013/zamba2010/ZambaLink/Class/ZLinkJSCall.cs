using System;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using Zamba.Core;
using Zamba.Core.Users;
using ZambaLink;
using CefSharp.WinForms;
using CefSharp;
using System.Windows.Forms;
//using Zamba;

namespace Zamba.Link
{
    public class WinFormJSCall
    {
        IChromiumZLink container = null;
        public WinFormJSCall(IChromiumZLink _container)
        {
            container = _container;
        }
        public string confVal()
        {
            var js = new DataContractJsonSerializer(typeof(ConfValues));
            var ms = new MemoryStream();    
            js.WriteObject(ms, InitZLink.cV);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            return jsonString;
        }
      
        public void action(string action)
        {
            try
            {
                container.DoAction(action);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        public string showMessage(string title, string message)
        {
            string winState = "";
            try
            {
                winState = container.ShowMessage(title, message);

                return winState;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return winState;
            }
        }
        public void downloadFile(string file)
        {
            try
            {
                container.DownloadFile(file);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        public void enableSuggestions()
        {
            try
            {
                container.EnableSuggestions();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

}
