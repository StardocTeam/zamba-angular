using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using ChatJsMvcSample.Code;
//using ChatJsMvcSample.Code.LongPolling;
//using ChatJsMvcSample.Code.LongPolling.Chat;
using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Models;
using System.Management;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using ChatJs.Net;
using ChatJsMvcSample.Models.ViewModels;

namespace ChatJsMvcSample.Controllers
{
    [EnableCors("*", "*", "*")]
    public class RestCollaborationController : Controller
    {
        public RestCollaborationController()
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
        public void GetMyUserId(string mail)
        {
            //
        }
        [EnableCors("*", "*", "*")]
        public List<ChatUser> GetUsersListColl(decimal myUserId)
        {
            try
            {
                var chatsIds = new List<decimal>();

                var lcu = new List<ChatUser>();
               
                    var cp = db.ChatPeople.AsNoTracking().Where(x => x.UserId == myUserId);
                    for (var i = 0; i <= cp.Count() - 1; i++)
                        chatsIds.Add(cp.ToList()[i].ChatId);

                    for (var i = 0; i <= chatsIds.Count() - 1; i++)
                    {
                        decimal id = chatsIds.ToList()[i];
                        var chat = db.Chat.AsNoTracking().Where(x => x.Id == id).First();
                        var groupCP = chat.ChatPeople.ToList().Where(x => x.UserId != myUserId);
                    ChatHub ChatHub = new ChatHub();
                    for (var j = 0; j <= groupCP.ToList().Count() - 1; j++)
                        {
                            var usId = groupCP.ToList()[j].UserId;
                            if (!lcu.Any(x => x.Id == usId))
                                lcu.Add(ChatHub.GetUserInfo(usId));
                        }
                    }
               
                return lcu.OrderByDescending(o => o.LastMessage).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }
        [EnableCors("*", "*", "*")]
        public JsonResult GetGroups(decimal myUserId)
        {
            try
            {
                var list = new List<ChatExternalData>();
               
                    var chatsIds = db.ChatPeople.AsNoTracking().Where(x => x.UserId == myUserId).Select(x => x.ChatId).ToList();

                    for (var i = 0; i <= chatsIds.Count() - 1; i++)
                    {
                        decimal id = chatsIds[i];
                        var chat = db.Chat.AsNoTracking().Where(x => x.Id == id).First();
                        //if (chat.ChatType == ChatType.Group)
                        //{
                        var groupCP = chat.ChatPeople.ToList().Where(x => x.UserId != myUserId);
                        var group = new List<ChatUser>();
                    ChatHub ChatHub = new ChatHub();
                    for (var j = 0; j <= groupCP.ToList().Count() - 1; j++)
                        {
                            var user = ChatHub.GetUserInfo(groupCP.ToList()[j].UserId);
                            if (user.Id > 0 && user.Role != ChatUser.RoleType.Blocked)
                                group.Add(user);
                        }
                        if (group.Count > 0)
                        {
                            var lastMsg = chat.ChatHistory.Count > 0 ? chat.ChatHistory.OrderBy(c => c.Id).LastOrDefault().Date : DateTime.Now;
                            list.Add(new ChatExternalData(group, lastMsg, chat.ChatName, id, chat.ChatType));
                        }
                        // }
                    }
              
                list = list.OrderByDescending(o => o.LastMessage).ToList();
                return this.Json(
               new
               {
                   Groups = list
               },
               JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }


        public JsonResult GetGroupsForum(decimal myUserId)
        {
            try
            {
                var list = new List<Models.ViewModels.ChatExternalDataForum>();
                
                    var chatsIds = db.ChatPeople.AsNoTracking().Where(x => x.UserId == myUserId).Select(x => x.ChatId).ToList();

                    for (var i = 0; i <= chatsIds.Count() - 1; i++)
                    {
                        decimal id = chatsIds[i];
                        var chat = db.Chat.AsNoTracking().Where(x => x.Id == id).First();
                        //if (chat.ChatType == ChatType.Group)
                        //{
                        var groupCP = chat.ChatPeople.ToList().Where(x => x.UserId != myUserId);
                        var group = new List<ChatUser>();
                    ChatHub ChatHub = new ChatHub();
                    for (var j = 0; j <= groupCP.ToList().Count() - 1; j++)
                        {
                            var user = ChatHub.GetUserInfo(groupCP.ToList()[j].UserId);
                            if (user.Id > 0 && user.Role != ChatUser.RoleType.Blocked)
                                group.Add(user);
                        }
                        if (group.Count > 0)
                        {
                            var lastMsg = chat.ChatHistory.Count > 0 ? chat.ChatHistory.OrderBy(c => c.Id).LastOrDefault().Date : DateTime.Now;
                            list.Add(new ChatExternalDataForum(group, lastMsg, chat.ChatName,id,chat.ChatType,chat.DocId));
                        }
                        // }
                    }
                
                list = list.OrderByDescending(o => o.LastMessage).ToList();
                return this.Json(
               new
               {
                   Groups = list
               },
               JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        [EnableCors("*", "*", "*")]
        public JsonResult GetMyUserInfo(string mail)
        {
            try
            {
                var user = db.ChatUser.AsNoTracking().Where(x => x.Email == mail).FirstOrDefault();
                user.Password = user.RetryPassword =  string.Empty;
                return this.Json(
                new
                {
                    User = user //ChatServer.Rooms[roomId].UserExists(userId) ? ChatServer.Rooms[roomId].UsersById[userId] : null
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}