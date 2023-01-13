using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Models;
using ChatJsMvcSample.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Zamba.Collaboration.Code;
using Zamba.Collaboration.Models;

namespace Zamba.Collaboration.Controllers
{
    public class InvitationController : Controller
    {
        public InvitationController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Help");
            }
            db = new DBCollaboration(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private DBCollaboration db;
        public void SendMailInvitation(int userId, int invUserId)
        {
            try
            {                
                    var user = db.ChatUser.Where(x => x.Id == userId).FirstOrDefault();
                    var invUser = db.ChatUser.Where(x => x.Id == invUserId).FirstOrDefault();
                    if (user == null || invUser == null) return;

                    var body = new StringBuilder();
                    body.Append(user.Name + "("+ user.Email+")");
                    body.Append(" de ");
                    body.Append(user.Company);
                    body.Append(" te ha agregado a Zamba Collaboration.");
                    string title = "Nuevo contacto en Zamba Collaboration";
                    string subMsg = " Te invitamos a entablar una nueva conversacion.";
                    Mail.SendGenericMail(string.Empty, "Accede ahora", title, body.ToString(), subMsg,invUser);
                
            }
            catch (Exception ex)
            {

            }

        }
    }
}
