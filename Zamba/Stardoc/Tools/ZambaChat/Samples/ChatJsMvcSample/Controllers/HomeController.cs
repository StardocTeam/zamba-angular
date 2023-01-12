using System;
using System.Web.Mvc;
using System.Linq;
using ChatJs.Net;// quitar
using ChatJsMvcSample.Code;
using ChatJsMvcSample.Code.LongPolling;
using ChatJsMvcSample.Code.LongPolling.Chat;
using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Models.ViewModels;
using ChatJsMvcSample.Models;
using System.Collections.Generic; //colocar


namespace ChatJsMvcSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

          //  return View();
          var existingUser = ChatHelper.GetChatUserFromCookie(this.Request);
            ChatViewModel chatViewModel = null;

            //if (existingUser == null)
            //{
            //    return View();
            //    ChatUser user = new ChatUser()
            //       {
            //           Name = "test user",                   
            //           Avatar = "",
            //           Id = 22242,                     
            //           Status = ChatUser.StatusType.Online,
            //           RoomId = ChatController.ROOM_ID_STUB
            //       };
            //    existingUser = user;
            //    ChatHub.RegisterNewUser(user);
            //}
     
            if (existingUser != null)
            {
                // in this case the authentication cookie is valid and we must render the chat
                if (existingUser.Id != 0) { 
                    ChatEntities db = new ChatEntities();
                    var thisUser = db.ChatUser.Where(u => u.Id == existingUser.Id).FirstOrDefault();
                    thisUser.Status = ChatUser.StatusType.Online;
                    db.SaveChanges();
                }

                chatViewModel = new ChatViewModel()
                {
                        IsUserAuthenticated = true,
                        UserId = existingUser.Id,
                        UserName = existingUser.Name,
                        Avatar = existingUser.Avatar,
                    };
            }
            return this.View(chatViewModel);
        }

        /// <summary>
        /// Joins the chat
        /// </summary>
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
                    user = ChatHub.FindUserById(id);             

                ChatServer.SetupRoomIfNonexisting(ChatController.ROOM_ID_STUB);

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
        public ActionResult LeaveChat()//(string userName, string email)
        {
            ChatHelper.RemoveCookie(this.Response);
            return this.RedirectToAction("Index");
        }
    }
}