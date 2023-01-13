using System;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using Zamba.Core;
using Zamba.Core.Users;
//using ZambaLink;
using CefSharp.WinForms;
using CefSharp;
using System.Windows.Forms;
//using Zamba;


namespace Zamba.QuickSearch
{
    public class WinFormJSCall
    {
        IChromiumQuickSearch container = null;
        public WinFormJSCall(IChromiumQuickSearch _container)
        {
            container = _container;
        }
        public string confVal()
        {
            var js = new DataContractJsonSerializer(typeof(confValues));
            var ms = new MemoryStream();
            var cv = new confValues();
            js.WriteObject(ms, cv);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            return jsonString;
        }
        public class confValues
        {
            public string QuickSearchURL { get; set; }
            public string QuickSearchServer { get; set; }        
            public long QSuserId { get; set; }
            public confValues()
            {
                try
                {
                    QuickSearchURL = ZOptBusiness.GetValue("QuickSearchURL");
                    QuickSearchServer = ZOptBusiness.GetValue("QuickSearchServer");
                    QSuserId = Membership.MembershipHelper.CurrentUser.ID;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
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
    }
}
