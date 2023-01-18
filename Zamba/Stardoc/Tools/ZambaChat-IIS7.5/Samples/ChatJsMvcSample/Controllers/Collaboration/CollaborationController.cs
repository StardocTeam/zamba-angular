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
using System.Web.Configuration;

namespace ChatJsMvcSample.Controllers
{
    [EnableCors("*", "*", "*")]

    public class CollaborationController : Controller
    {
        public CollaborationController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Chat");
            }
            db = new ChatEntities(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }


        public static ChatEntities db;

        [EnableCors("*", "*", "*")]
        public ActionResult Index()
        {
            return View();
        }
        [EnableCors("*", "*", "*")]
        public ActionResult Login()
        {
            return View();
        }

        public decimal GetUserId()
        {
            try
            {
                var userId = Encript.Decrypt(Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString());
                return decimal.Parse(userId);
            }
            catch { }
            return 0;
        }
        [EnableCors("*", "*", "*")]
        public bool IsCollaborationServer()
        {
            try
            {
                var isColServer = WebConfigurationManager.AppSettings["isCS"];
                return bool.Parse(isColServer);
            }
            catch { }
            return false;
        }

        public string GetUserByCredentials(string email, string password)
        {
            try
            {
                var user = db.ChatUser.AsNoTracking().Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    var currPass = Encript.Decrypt(user.Password);
                    if (currPass == password.Trim())
                    {
                        return Encript.Encrypt(user.Id.ToString());
                    }
                    else
                    {
                        return "Error: Contraseña incorrecta";
                    }
                }
                return "Error: Usuario no registrado - mail incorrecto";
            }
            catch
            {
                return "Error: Se produjo un error al validar sus datos";
            }
        }

        public decimal GetUserIdByEnc(string enc)
        {
            try
            {
                var userId = Encript.Decrypt(enc.Trim().Replace(" ", "+"));
                return decimal.Parse(userId);
            }
            catch { }
            return 0;
        }
        public string EncToString(string enc)
        {
            try
            {
                var str = Encript.Decrypt(enc.Trim().Replace(" ", "+"));
                return str;
            }
            catch { }
            return "Codigo incorrecto";
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

        [ValidateInput(false)]
        public ActionResult Join(string enc)
        {
            try
            {
                Session["userId"] = enc;
            }
            catch { }
            return View();
        }

        [ValidateInput(false)]
        public ActionResult JoinZLink(string enc)
        {
            try
            {
                Session["userId"] = enc;
            }
            catch { }
            return View();
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