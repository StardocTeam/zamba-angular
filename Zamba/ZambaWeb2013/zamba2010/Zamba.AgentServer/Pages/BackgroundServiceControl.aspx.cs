using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.AgentServer.WS;

namespace Zamba.AgentServer.Pages
{
    public partial class BackgroundServiceControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            UCMService.UCM = new WS.UCMService();
            UCMService.UCM.QuequeTasks();

        }
    }
}