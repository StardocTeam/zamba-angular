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
using ChatJsMvcSample.Models.ViewModels;
using System.Web.Configuration;
using System.Data;


namespace ChatJsMvcSample.Controllers
{
    /// <summary>
    /// ChatController
    /// THIS CONTROLLER IS ONLY USED FOR LONG-POLLING. IF YOU ARE NOT USING LONG POLLING, THIS CONTROLLER WILL
    /// NOT BE USED
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChatController : Controller
    {
        public ChatController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Chat");
            }
            db = new ChatEntities(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }


        public static ChatEntities db;
        public const string ROOM_ID_STUB = "Zamba-Chat";

        private decimal GetMyUserId(HttpRequestBase request)
        {
            //var a = decimal.Parse(request.QueryString["UserId"].ToString());            
            return ChatHelper.GetChatUserFromCookie(request) == null ? 0 :
                ChatHelper.GetChatUserFromCookie(request).Id;
        }

        private string GetMyRoomId()
        {
            return ROOM_ID_STUB;
        }
        //public void SetUserOffline(int userId)
        //{
        //    var roomId = this.GetMyRoomId();

        //    if (ChatServer.RoomExists(roomId) && ChatServer.Rooms[roomId].UserExists(userId))
        //        ChatServer.Rooms[roomId].SetUserOffline(userId);
        //}

        [HttpGet]
        public JsonResult GetUserInfo(int userId)
        {
            ChatHub ChatHub = new ChatHub();
            var user = ChatHub.GetUserInfo(userId);
            user.Password = user.RetryPassword = string.Empty;
            return this.Json(
                new
                {
                    User = user //ChatServer.Rooms[roomId].UserExists(userId) ? ChatServer.Rooms[roomId].UsersById[userId] : null
                },
                JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult GetMessageHistory(int otherUserId, long? timeStamp = null)
        //{
        //    var roomId = this.GetMyRoomId();
        //    var myUserId = this.GetMyUserId(this.Request);

        //    Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

        //    // Each UserFrom Id has a LIST of messages. Of course
        //    // all messages have the same UserTo, of course, myUserId.
        //    var messages = ChatServer.Rooms[roomId].GetMessagesBetween(myUserId, otherUserId, timeStamp);

        //    return this.Json(new
        //    {
        //        Messages = messages,
        //        Timestamp = DateTime.UtcNow.Ticks.ToString()
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetMsgHistoryGroup(List<int> users, long? timeStamp = null)
        //{
        //    var roomId = this.GetMyRoomId();
        //    var myUserId = this.GetMyUserId(this.Request);

        //    Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

        //    // Each UserFrom Id has a LIST of messages. Of course
        //    // all messages have the same UserTo, of course, myUserId.
        //    //var messages = ChatServer.Rooms[roomId].GetMessagesBetween(myUserId, otherUserId, timeStamp);
        //    var messages = "";
        //    return this.Json(new
        //    {
        //        Messages = messages,
        //        Timestamp = DateTime.UtcNow.Ticks.ToString()
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Obsolete]
        //Ver si se usa seguramente se maneja solo por SignalR y esto no es necesario
        public JsonResult SendMessage(int otherUserId, string message, string clientGuid)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (clientGuid == null) throw new ArgumentNullException("clientGuid");

            var roomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId(this.Request);

            //Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            if (myUserId == otherUserId)
                throw new Exception("Cannot send a message to yourself");

           // ChatServer.Rooms[roomId].SendMessage(myUserId, otherUserId, message, clientGuid);

            // you may want to persist messages here
            return null;
        }

        [HttpPost]
        [Obsolete]
        //Ver si se usa seguramente se maneja solo por SignalR y esto no es necesario
        public JsonResult SendMsgToUsers(List<int> otherUserId, string message, string clientGuid)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (clientGuid == null) throw new ArgumentNullException("clientGuid");

            var roomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId(this.Request);

            //Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            if (myUserId == otherUserId[99])
                throw new Exception("Cannot send a message to yourself");

            // ChatServer.Rooms[roomId].SendMsgToUsers(myUserId, otherUserId[99], message, clientGuid);

            // you may want to persist messages here
            return null;
        }


        [HttpPost]
        public JsonResult SendTypingSignal(int otherUserId)
        {
            var roomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId(this.Request);

          //  Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            if (myUserId == otherUserId)
                throw new Exception("Cannot send a typing signal to yourself");

           // ChatServer.Rooms[roomId].SendTypingSignal(myUserId, otherUserId);

            return null;
        }

        public JsonResult JoinChat(int userId)
        {
            ChatHub ChatHub = new ChatHub();
            var user = ChatHub.GetUserInfo(userId);
            if (user != null)
            {
                Session["ChatUserId"] = user.Id;
                return this.Json(new
                {
                    User = user,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new ArgumentException("UserId");
            }
        }

        public JsonResult GetUsersList()
        {
            var usersList = db.ChatUser.AsNoTracking().ToList();
            usersList.ForEach(x => x.Password = string.Empty);
            return this.Json(new
            {
                Users = usersList,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CookieUserId()
        {
            return this.Json(new
            {
                Id = this.GetMyUserId(this.Request),
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult CookieUserInfo()
        //{
        //    return this.Json(new
        //    {
        //        User = ChatHub.GetUserInfo(this.GetMyUserId(this.Request)),
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //public void CookieRemove()
        //{
        //    var userId = this.GetMyUserId(this.Request);
        //    //Grabo usuario offline
        //    if (userId >= 1)
        //        LeaveChat(userId);

        //    ChatHelper.RemoveCookie(this.Response);
        //}
        public string GetGroupsForum(decimal myUserId)
        {
            var chatHub = new ChatHub();
            var groupList = chatHub.GetGroupsForum(myUserId).OrderByDescending(x => x.LastMessage).ToList();//this.GetMyUserId(this.Request)
            var groups = Newtonsoft.Json.JsonConvert.SerializeObject(groupList);
            return groups;
        }

        public string GetGroups(decimal myUserId)
        {
            var chatHub = new ChatHub();
            var groupList = chatHub.GetGroups(myUserId).OrderByDescending(x => x.LastMessage).ToList();//this.GetMyUserId(this.Request)
            var groups = Newtonsoft.Json.JsonConvert.SerializeObject(groupList);
            return groups;
        }
        public string CreateEmptyChat(decimal userId, string groupName)
        {
            ChatHub ChatHub = new ChatHub();
            var createChat = ChatHub.CreateEmptyChat(userId, groupName);
            var chatId = Newtonsoft.Json.JsonConvert.SerializeObject(createChat);

            return chatId;
        }


        public string CreateEmptyChatForum(decimal userId, string groupName, int? docId)
        {
            ChatHub ChatHub = new ChatHub();
            var createChat = ChatHub.CreateEmptyChatForum(userId, groupName, docId);
            var chatId = Newtonsoft.Json.JsonConvert.SerializeObject(createChat);

            return chatId;
        }

        public string AddRemoveUsersGroupsForum(decimal chatId, decimal userId, bool add, decimal docId)
        {
            var chatHub = new ChatHub();
            chatHub.AddRemoveUsersGroupsForum(chatId, userId, add);
            return "ok";
        }

        public string AddRemoveUsersGroups(decimal chatId, decimal userId, bool add)
        {
            var chatHub = new ChatHub();
            chatHub.AddRemoveUsersGroups(chatId, userId, add);
            return "ok";
        }

        public JsonResult GetAllUsersChat()
        {
            var chatHub = new ChatHub();
            return this.Json(new
            {
                User = chatHub.GetUserListChat(),
            }, JsonRequestBehavior.AllowGet);
        }
        //Se reemplazo este por el de abajo, no quitar hasta estar seguro que no es necesario
        //[EnableCors("*", "*", "*")]
        //public JsonResult GetUsersChat(HttpRequestBase request)
        //{
        //    //var roomId = this.GetMyRoomId();
        //    //Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");
        //    ChatHub chatHub = new ChatHub();
        //    var users = chatHub.GetUsersChat(this.GetMyUserId(request));
        //    //var usersList = ChatServer.Rooms[roomId].UpdateStatusesAndGetUserList();
        //    return this.Json(new
        //    {
        //        Users = users,
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetUsersChatByUserId(decimal myUserId)
        {
            ChatHub chatHub = new ChatHub();
            var users = chatHub.GetUserListChat().Where(x => x.Id != myUserId);
            return this.Json(new
            {
                Users = users,
            }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUsersByChat(decimal chatId)
        {
            var chatHub = new ChatHub();
            var users = chatHub.GetUsersByChat(chatId);
            return this.Json(new
            {
                Users = users,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNoReadUserHistory(decimal userId)
        {
            //var roomId = this.GetMyRoomId();
            //Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");
            ChatHub chatHub = new ChatHub();
            var history = chatHub.GetNoReadUserHistory(userId);
            //var usersList = ChatServer.Rooms[roomId].UpdateStatusesAndGetUserList();
            return this.Json(new
            {
                History = history,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReduceBase64Img(string img)
        {
            ChatHub chatHub = new ChatHub();
            var reduceImg = chatHub.ReduceBase64Img(img);

            return this.Json(new
            {
                Img = reduceImg,
            }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetMessageHistory(string usersId, ChatType chatType)
        {
            ChatHub chatHub = new ChatHub();
            var users = new List<decimal>();
            var usersArray = usersId.Split(new Char[] { '/' });
            if (usersArray.ToList().Count == 2 && !IsNumeric(usersArray.ToList()[1]))
            {
                users.Add(decimal.Parse(usersArray[0]));
            }
            else
                users = new List<decimal>(Array.ConvertAll(usersArray, s => Convert.ToDecimal(s)));

            var history = chatHub.GetMessageHistory(users, chatType);

            return this.Json(new
            {
                History = history,
            }, JsonRequestBehavior.AllowGet);
        }

        //[EnableCors("*", "*", "*")]
        //public JsonResult GetMoreMsgHistory(string usersId, int cant)
        //{
        //    ChatHub chatHub = new ChatHub();
        //    var usersArray = usersId.Split(new Char[] { '/' });
        //    var users = new List<decimal>(Array.ConvertAll(usersArray, s => Convert.ToDecimal(s)));

        //    var history = chatHub.GetMoreMsgHistory(users, cant);

        //    return this.Json(new
        //    {
        //        History = history,
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetMoreMsgHistory(string usersId, decimal chatId, int cant)
        {
            var chatHub = new ChatHub();
            var usersArray = usersId.Split(new Char[] { '/' });
            var users = new List<decimal>(Array.ConvertAll(usersArray, s => Convert.ToDecimal(s)));
            var history = chatHub.GetMoreMsgHistory(users, chatId, cant);

            return this.Json(new
            {
                History = history,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult OpenGroupChat(string usersId)
        {
            ChatHub chatHub = new ChatHub();
            var users = new List<int>();
            var usersArray = usersId.Split(new Char[] { ',' });
            if (usersArray.ToList().Count == 2 && !IsNumeric(usersArray.ToList()[0]))
            {
                users.Add(Int32.Parse(usersArray[1]));
            }
            else
            {
                users = new List<int>(Array.ConvertAll(usersArray, s => Convert.ToInt32(s)));
                users.Sort();
            }
            var groupChat = chatHub.OpenGroupChat(users);
            var clearChat = new Chat()
            {
                AdminId = groupChat.AdminId,
                ChatHistory = groupChat.ChatHistory,
                Id = groupChat.Id,
                ChatPeople = groupChat.ChatPeople,
                ChatType = groupChat.ChatType,
                ChatName = groupChat.ChatName
            };
            return this.Json(new
            {
                GroupChat = clearChat,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult OpenGroupChatById(decimal chatId)
        {
            
           var chatHub = new ChatHub();
           var groupChat = chatHub.OpenGroupChat(chatId);

           var clearChat = new Chat()
           {
               AdminId = groupChat.AdminId,
               ChatHistory = groupChat.ChatHistory,
               Id = groupChat.Id,
               ChatPeople = groupChat.ChatPeople,
               ChatType = groupChat.ChatType,
                ChatName = groupChat.ChatName
           };

           return this.Json(new
           {
              GroupChat = clearChat,
           }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LeaveChat(decimal userId)
        {
            ChatHub chatHub = new ChatHub();
            chatHub.LeaveChat(userId);
            return null;
        }

        public JsonResult ChangeName(decimal userId, string name)
        {
            ChatHub chatHub = new ChatHub();
            chatHub.ChangeName(userId, name);

            //  ChatHelper.CreateNewUserCookie(this.Response, ChatHub.GetUserInfo(userId));
            return null;
        }

        public JsonResult ChangeGroupName(decimal userId, decimal chatId, string name)
        {
            ChatHub chatHub = new ChatHub();
            chatHub.ChangeGroupName(userId, chatId, name);
            return null;
        }


        public JsonResult DisableChat(decimal userId, bool disable)
        {
            ChatHub chatHub = new ChatHub();
            chatHub.DisableChat(userId, disable);
            return null;
        }
        public JsonResult ChangeAvatar(decimal userId, string avatar)
        {
            ChatHub ChatHub = new ChatHub();
            ChatHub.ChangeAvatar(userId, avatar);
            return null;
        }

        public JsonResult LeaveChatGroup(decimal chatId, decimal userId)
        {
            ChatHub ChatHub = new ChatHub();
            ChatHub.LeaveChatGroup(chatId, userId);
            return null;
        }

        public JsonResult ChangeStatus(decimal userId, int status)
        {
            //ChatHub chatHub = new ChatHub();
            //chatHub.ChangeStatus(userId, status);
            return null;
        }
        public int CreateChatUser(decimal id, string name, string mail)
        {
            try
            {
                var defAv = ImageController.GetDefaultAvatarImageB64();
                DateTime time = DateTime.Now;              // Use current time
                string format = "yyyyMMdd HH:mm:ss.fff";
                var q = new System.Text.StringBuilder();
                q.Append("INSERT INTO CHATUSERS (ID,NAME, AVATAR,STATUS,LASTACTIVEON,ROOMID,ROLE,EMAIL,PASSWORD) VALUES (");
                q.Append(id + ",");
                q.Append("'" + name + "',");
                q.Append("'" + defAv + "',");
                q.Append((int)ChatUser.StatusType.Online + ",");
                q.Append("'" + time.ToString(format) + "',");
                q.Append("'" + ROOM_ID_STUB + "',");
                q.Append((int)ChatUser.RoleType.Active + ",");
                q.Append(mail == string.Empty ? "'user" + id + "@zambalink.com'," : "'" + mail + "',");
                q.Append("'123456')");

                return db.Database.ExecuteSqlCommand(q.ToString());

                #region No funciona con EF por error de identity
                //var cu = new ChatUser()
                //{
                //    Id = id,
                //    Name = name,
                //    Avatar = ImageController.GetDefaultAvatarImageB64(),
                //    Status = ChatUser.StatusType.Online,
                //    LastActiveOn = DateTime.Now,
                //    RoomId = ROOM_ID_STUB,
                //    Role = ChatUser.RoleType.Active,
                //    Email = mail == string.Empty ? "user" + id + "@zambalink.com" : mail,
                //    Password = "123456",
                //    RetryPassword = "123456"
                //};
                //db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[ChatUsers] ON");     
                //db.ChatUser.Add(cu);                
                //db.SaveChanges();
                //db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[ChatUsers] OFF");
                #endregion

            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        [HttpPost]
        public ContentResult UploadFiles(string id, decimal myUserId, bool useExternal)
        {
            //id debe tener ser chatidNumero
            var myUser = myUserId;
            var chatHub = new ChatHub();
            decimal chatId;
            if (id.IndexOf("extchatid") > -1)
            {
                chatId = int.Parse(id.Replace("extchatid", string.Empty));
            }
            else if (id.IndexOf("chatid") > -1)
            {
                chatId = int.Parse(id.Replace("chatid", string.Empty));
            }
            else
            {
                var ld = new List<decimal>();
                ld.Add(decimal.Parse(id));
                ld.Add(myUserId);
                chatId = chatHub.GetChatId(ld);
            }

            var UrlFilesServer = useExternal ? ConfigurationManager.AppSettings["UrlFilesServerExt"] :
                ConfigurationManager.AppSettings["UrlFilesServer"];

            DirectoryInfo di = Directory.CreateDirectory(UrlFilesServer + chatId + "/" + myUser);

            var r = new List<UploadFilesResult>();
            string savedFileName = "";
            string nameFile = "";
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                if (hpf.ContentLength >= int.Parse(ConfigurationManager.AppSettings["MaxSizeFilesServer"]))
                    return Content("{\"name\":\"" + "Error"
                           + "\",\"type\":\"" + hpf.ContentLength + "\",\"size\":\"" + int.Parse(ConfigurationManager.AppSettings["MaxSizeFilesServer"]) + "\"}", "application/json");

                //if (hpf.ContentLength == 0)
                //    continue;
                nameFile = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + " - " + Path.GetFileName(hpf.FileName);
                savedFileName = Path.Combine(di.FullName, nameFile);
                hpf.SaveAs(savedFileName);

                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType,
                    Rute = savedFileName,
                    Users = id
                });
            }

            var urlPath = (useExternal ? ConfigurationManager.AppSettings["UrlFilesServerExtHTTP"] :
                ConfigurationManager.AppSettings["UrlFilesServerHTTP"]) + chatId + "/" + myUserId + "/";
            return Content("{\"name\":\"" + urlPath + nameFile
                + "\",\"type\":\"" + chatId + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
        }
        [Obsolete]
        [EnableCors("*", "*", "*")]
        public FileResult DownloadFile(string file)
        {
            var reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Path.GetExtension(file).ToLower());
            string contentType = "application/unknown";
            if (reg != null)
            {
                string registryContentType = reg.GetValue("Content Type") as string;
                if (!String.IsNullOrWhiteSpace(registryContentType))
                    contentType = reg.GetValue("Content Type") as string;
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(@file);
            return File(fileBytes, contentType, Path.GetFileName(file).Substring(22));//sin fecha
        }

        private string GetVirtualPath(string physicalPath)
        {
            string rootpath = Server.MapPath("~/");

            physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");

            return "~/" + physicalPath;
        }
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }


        public string GetUsersZambaInfo(decimal id)
        {
            var chatHub = new ChatHub();
            var user = chatHub.GetUsersZambaInfo(id);
            if (user.Rows.Count == 0)
                return string.Empty;
            else
            {
                var userJson = DataTableToJson.Convert(user);
                return userJson.Replace("\\", "/");
            }
        }

        public string PathToBase64(string path)
        {
            var base64 = ImageController.PathToBase64(path);
            return base64;
        }
        public static decimal ExistChatByCP(List<IGrouping<decimal, ChatPeople>> groupCP, ChatType chatType, List<decimal> usersId)
        {
            decimal existChatId = 0;
            foreach (var gcp in groupCP)
            {
                var glist = gcp.ToList().Select(x => x.UserId).ToList();

                List<int> usersIdInt = usersId.Select(s => Convert.ToInt32(s)).ToList();
                List<int> glistInt = glist.Select(s => Convert.ToInt32(s)).ToList();

                if (!glistInt.Except(usersIdInt).Any())
                {
                    var thisChat = db.Chat.Where(x => x.Id == gcp.Key).First();
                    if (thisChat.ChatType == chatType)
                    {
                        existChatId = gcp.Key;
                        break;
                    }
                }
            }
            return existChatId;
        }

        //[HttpPost]
        //public int InsertChatForum(int IdChat,int DocTypeId,int DocId)
        //{
        //    try
        //    {
        //        var str = new System.Text.StringBuilder();
        //        str.Append("INSERT INTO Zchat_forum VALUES ( '" + IdChat + "','" + DocTypeId + "','" + DocId + "' )");

        //        return db.Database.ExecuteSqlCommand(str.ToString());
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
           
        //}


    }
}