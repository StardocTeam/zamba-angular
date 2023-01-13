using System;
using System.Web.Mvc;
using System.Linq;
using ChatJs.Net;// quitar
using ChatJsMvcSample.Code;
//using ChatJsMvcSample.Code.LongPolling;
//using ChatJsMvcSample.Code.LongPolling.Chat;
using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Models.ViewModels;
using ChatJsMvcSample.Models;
using System.Collections.Generic; //colocar
using System.Web.Http.Cors;

namespace ChatJsMvcSample.Controllers
{
    [EnableCors("*", "*", "*")]
    public class HomeController : Controller
    {
        [EnableCors("*", "*", "*")]
        [ValidateInput(false)]
        public ActionResult Index()
        {                     
            return View();
        }
        [ValidateInput(false)]
        public ActionResult ZLink()
        {
            return View();
        }

        /// <summary>
        /// Joins the chat
        /// </summary>
        [EnableCors("*", "*", "*")]
        public ActionResult JoinChat(decimal id)
        {       
            try
            {
                var user = new ChatUser();
                if (id == 0)
                {
                    user = new ChatUser()
                    {
                        Id = 0,
                        Avatar = "",
                        Name = "administrador",
                        Status = ChatUser.StatusType.Online,
                        LastActiveOn = DateTime.Now,
                        RoomId = ChatController.ROOM_ID_STUB,
                    };
                }
                else
                {
                    ChatHub ChatHub = new ChatHub();
                    user = ChatHub.GetUserInfo(id);
                }
                //ChatServer.SetupRoomIfNonexisting(ChatController.ROOM_ID_STUB);

                ChatHelper.CreateNewUserCookie(this.Response, user);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar al servidor/cargar usuario " + ex.Message);
            }
        }

        /// <summary>
        /// Leaves the chat
        /// </summary>
        /// 
        [EnableCors("*", "*", "*")]
        public ActionResult LeaveChat()//(string userName, string email)
        {
            ChatHelper.RemoveCookie(this.Response);
            return this.RedirectToAction("Index");
        }
    }
}