using System.Windows.Forms;
using Zamba.Core;

namespace Zamba.QuickSearch
{
    public partial class frmQuickSearch : IChromiumQuickSearch
    {
        delegate void DDoAction(string action);
        public void DoAction(string action)
        {
            if (this.InvokeRequired)
            {
                var delegatedoaction = new DDoAction(DoAction);
                this.Invoke(delegatedoaction, new object[] { action });
            }
            else
            {
                switch (action)
                {
                    case "minimize":
                        this.WindowState = FormWindowState.Minimized;
                        break;
                    case "shake":
                        break;
                }
            }
        }

        delegate string DShowMessage(string title, string message);
        public string ShowMessage(string title, string message)
        {
            var winState = WindowState.ToString();
            if (this.InvokeRequired)
            {
                var delegateshowmessage = new DShowMessage(ShowMessage);
                this.Invoke(delegateshowmessage, new object[] { title, message });
            }
            else
            {
                notifyIcon.ShowBalloonTip(5, title, message, ToolTipIcon.Info);
            }
            return winState;
        }
    }
}

