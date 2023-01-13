using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using ChatJs.Net;// quitar
using ChatJsMvcSample.Code;
using ChatJsMvcSample.Code.LongPolling;
using ChatJsMvcSample.Code.LongPolling.Chat;
using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Models; //Colocar
using System.Management;
using System.IO;
using System.Linq;
using System.Configuration;
//using Microsoft.Framework.Runtime;
using System.Threading.Tasks;
using System.IO;

namespace ChatJsMvcSample.Controllers
{
    /// <summary>
    /// ChatController
    /// THIS CONTROLLER IS ONLY USED FOR LONG-POLLING. IF YOU ARE NOT USING LONG POLLING, THIS CONTROLLER WILL
    /// NOT BE USED
    /// </summary>
    public class ChatController : Controller
    {
        /// <summary>
        /// This STUB. In a normal situation, there would be multiple rooms and the user room would have to be 
        /// determined by the user profile
        /// </summary>
        public const string ROOM_ID_STUB = "Zamba-Chat";

        /// <summary>
        /// Returns my user id
        /// </summary>
        /// <returns></returns>
        private decimal GetMyUserId(HttpRequestBase request)
        {

            // This would normally be done like this:
            //var userPrincipal = this.Context.User as AuthenticatedPrincipal;
            //if (userPrincipal == null)
            //    throw new NotAuthorizedException();

            //var userData = userPrincipal.Profile;
            //return userData.Id;

            // But for this example, it will get my user from the cookie
            return ChatHelper.GetChatUserFromCookie(request) == null ? 0 : 
                ChatHelper.GetChatUserFromCookie(request).Id;
        }

        private string GetMyRoomId()
        {
            // This would normally be done like this:
            //var userPrincipal = this.Context.User as AuthenticatedPrincipal;
            //if (userPrincipal == null)
            //    throw new NotAuthorizedException();

            //var userData = userPrincipal.Profile;
            //return userData.MyTenancyIdentifier;

            // But for this example, it will always return "chatjs-room", because we have only one room.
            return ROOM_ID_STUB;
        }

        /// <summary>
        /// Sets a user offline
        /// </summary>
        /// <remarks>
        /// ToDo: This action has a hole. As anyone can just call it to make a friend 
        /// off. The good side is that, if the person is really on, he/she will automatically be back on
        /// in a few seconds.
        /// </remarks>
        public void SetUserOffline(int userId)
        {
            var roomId = this.GetMyRoomId();

            if (ChatServer.RoomExists(roomId) && ChatServer.Rooms[roomId].UserExists(userId))
                ChatServer.Rooms[roomId].SetUserOffline(userId);
        }

        [HttpGet]
        public JsonResult GetUserInfo(int userId)
        {
            //var roomId = this.GetMyRoomId();
            //Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            // this will intentionally trigger an error in case the user doesn't exist.
            // the client must treat this scenario
            //  ChatHub chatHub = new ChatHub();
            var user = ChatHub.GetUserInfo(userId);

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
        public JsonResult SendMessage(int otherUserId, string message, string clientGuid)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (clientGuid == null) throw new ArgumentNullException("clientGuid");

            var roomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId(this.Request);

            Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            if (myUserId == otherUserId)
                throw new Exception("Cannot send a message to yourself");

            ChatServer.Rooms[roomId].SendMessage(myUserId, otherUserId, message, clientGuid);

            // you may want to persist messages here
            return null;
        }

        [HttpPost]
        public JsonResult SendMsgToUsers(List<int> otherUserId, string message, string clientGuid)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (clientGuid == null) throw new ArgumentNullException("clientGuid");

            var roomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId(this.Request);

            Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

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

            Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            if (myUserId == otherUserId)
                throw new Exception("Cannot send a typing signal to yourself");

            ChatServer.Rooms[roomId].SendTypingSignal(myUserId, otherUserId);

            return null;
        }

        public JsonResult JoinChat(int userId)
        {
            if (userId==0){
                var userAdmin= new ChatUser(){
                   Id=0,
                   Avatar="",                  
                   Name="administrador",      
                   Status = ChatUser.StatusType.Online,
                   LastActiveOn = DateTime.Now,
                   RoomId = ChatController.ROOM_ID_STUB,  
                };

                ChatHelper.CreateNewUserCookie(this.Response, userAdmin);

                return this.Json(new
                {
                    User = userAdmin,
                }, JsonRequestBehavior.AllowGet);
            }  

            var roomId = this.GetMyRoomId();
            //   Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");          

            //COLOCAR
            //  var user = ChatHub.GetUserInfo(userId);

            var user = ChatHelper.GetChatUserFromCookie(this.Request) ;
            // ChatHelper.GetChatUserFromCookie(this.Request) ?? ChatHub.GetUserInfo(userId);
            if (user == null || user.Id != userId)
            {
                //if (user == null ||  user.Id != 0)
                //{ 
                user = ChatHub.GetUserInfo(userId);
                    if (user != null)
                        ChatHelper.CreateNewUserCookie(this.Response, user);
                //}
            }
            
            return this.Json(new
            {
                User = user,
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Returns the list of users in the current room
        /// </summary>
        public JsonResult GetUsersList()
        {
            var roomId = this.GetMyRoomId();
            Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

            var usersList = ChatServer.Rooms[roomId].UpdateStatusesAndGetUserList();

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

        public JsonResult CookieUserInfo()
        {
            return this.Json(new
            {
                User = ChatHub.GetUserInfo(this.GetMyUserId(this.Request)),
            }, JsonRequestBehavior.AllowGet);
        }

           public void CookieRemove()
        {
            var userId = this.GetMyUserId(this.Request);
            //Grabo usuario offline
            if (userId >=1)            
                LeaveChat(userId);
           
            ChatHelper.RemoveCookie(this.Response);
        }

           public JsonResult PathToBase64(string path)
           {
               ImageController ic = new ImageController();
               var base64 = ic.PathToBase64(path) ;
               return this.Json(new
               {
                   Base64 = base64,
               }, JsonRequestBehavior.AllowGet);
           }

        public JsonResult GetGroups()
        {
            ChatHub chatHub = new ChatHub();
            var groupList = chatHub.GetGroups(this.GetMyUserId(this.Request));

            return this.Json(new
            {
                GroupList = groupList,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersChat()
        {
            //var roomId = this.GetMyRoomId();
            //Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");
            ChatHub chatHub = new ChatHub();
            var users = chatHub.GetUsersChat();

            //var usersList = ChatServer.Rooms[roomId].UpdateStatusesAndGetUserList();

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

        public JsonResult GetMessageHistory(string usersId)
        {
            ChatHub chatHub = new ChatHub();
            var usersArray = usersId.Split(new Char[] { '/' });
            var users = new List<decimal>(Array.ConvertAll(usersArray, s => Convert.ToDecimal(s)));

            var history = chatHub.GetMessageHistory(users);

            return this.Json(new
            {
                History = history,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMoreMsgHistory(string usersId, int cant)
        {
            ChatHub chatHub = new ChatHub();
            var usersArray = usersId.Split(new Char[] { '/' });
            var users = new List<decimal>(Array.ConvertAll(usersArray, s => Convert.ToDecimal(s)));

            var history = chatHub.GetMoreMsgHistory(users, cant);

            return this.Json(new
            {
                History = history,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OpenGroupChat(string usersId)
        {
            ChatHub chatHub = new ChatHub();

            var usersArray = usersId.Split(new Char[] { ',' });
            var users = new List<int>(Array.ConvertAll(usersArray, s => Convert.ToInt32(s)));
            users.Sort();
            var groupChat = chatHub.OpenGroupChat(users);
            var clearChat = new Chat()
            {
                AdminId = groupChat.AdminId,
                ChatHistory = groupChat.ChatHistory,
                Id = groupChat.Id,
                ChatPeople = groupChat.ChatPeople
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

            ChatHelper.CreateNewUserCookie(this.Response, ChatHub.GetUserInfo(userId));
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
            ChatHub chatHub = new ChatHub();
            chatHub.ChangeAvatar(userId, avatar);
            return null;
        }

        public JsonResult ChangeStatus(decimal userId, int status)
        {
            //ChatHub chatHub = new ChatHub();
            //chatHub.ChangeStatus(userId, status);
            return null;
        }

        //[HttpPost]
        //public JsonResult ChangeStatus(int userId, int status)
        //{
        //    var roomId = this.GetMyRoomId();
        //    var myUserId = this.GetMyUserId(this.Request);

        //    Debug.Assert(ChatServer.RoomExists(roomId), "user room should be created when user joins the chat");

        //    //if (myUserId == otherUserId)
        //    //    throw new Exception("Cannot send a typing signal to yourself");

        //    ChatServer.Rooms[roomId].ChangeStatus(userId, status);

        //    return null;
        //}

        [HttpPost]
        public ContentResult UploadFiles(string idUsers)
        {
            var myUser = this.GetMyUserId(this.Request);
            ChatHub chatHub = new ChatHub();
            var usersArray = idUsers.Split(new Char[] { '/' });
            var users = new List<decimal>(Array.ConvertAll(usersArray, s => Convert.ToDecimal(s)));
            users.Add(myUser);
            var chatId=chatHub.GetChatId(users);
            var UrlFilesServer= ConfigurationManager.AppSettings["UrlFilesServer"];

            DirectoryInfo di = Directory.CreateDirectory(UrlFilesServer + chatId + "/" + myUser);

            var r = new List<UploadFilesResult>();
            string savedFileName="";
            string nameFile="";
            foreach (string file in Request.Files)
            {             
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                if (hpf.ContentLength>= int.Parse(ConfigurationManager.AppSettings["MaxSizeFilesServer"]) )
                    return Content("{\"name\":\"" + "Error"
                           + "\",\"type\":\"" + hpf.ContentLength + "\",\"size\":\"" + int.Parse(ConfigurationManager.AppSettings["MaxSizeFilesServer"]) + "\"}", "application/json");

                if (hpf.ContentLength == 0)
                    continue;
                nameFile = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + " - " + Path.GetFileName(hpf.FileName);
                savedFileName = Path.Combine(di.FullName, nameFile);
                hpf.SaveAs(savedFileName);

                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType,
                    Rute = savedFileName,
                    Users = idUsers
                });
            }
            return Content("{\"name\":\"" + di.FullName.Replace("\\", "-") + "-" + nameFile
                + "\",\"type\":\"" + idUsers + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" +
            //    string.Format("{0} bytes", r[0].Length) + "\",\"rute\":\"" + r[0].Rute + "\",\"users\":\"" + r[0].Users + "\"}", "application/json");
        }

        // [AcceptVerbs(HttpVerbs.Get)]
        public FileResult DownloadFile(string file)
        {
          //  file = AppDomain.CurrentDomain.BaseDirectory + "ChatJs/Images/add.png";
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
          
            //return new DownloadResult
            //{
            //    VirtualPath = GetVirtualPath(file),
            //    FileDownloadName = Path.GetFileName(file).Substring(22)
            //};
          //  return FileResult(file, "multipart/form-data", Path.GetFileName(file).Substring(22));

           // return File(AppDomain.CurrentDomain.BaseDirectory + "ChatJs/Images/AddUser.png", contentType, "add.png");
        }
        private string GetVirtualPath(string physicalPath)
                {
                    string rootpath = Server.MapPath("~/");

                    physicalPath = physicalPath.Replace(rootpath, "");
                    physicalPath = physicalPath.Replace("\\", "/");

                    return "~/" + physicalPath;
                }
    }        
}



public class UploadFilesResult
{
    public string Name { get; set; }
    public int Length { get; set; }
    public string Type { get; set; }
    public string Rute { get; set; }
    public string Users { get; set; }
}

public class DownloadResult : ActionResult
{

    public DownloadResult() { }

    public DownloadResult(string virtualPath)
    {
        this.VirtualPath = virtualPath;
    }

    public string VirtualPath
    {
        get;
        set;
    }

    public string FileDownloadName
    {
        get;
        set;
    }

    public override void ExecuteResult(ControllerContext context) {
        if (!String.IsNullOrEmpty(FileDownloadName)) {
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + this.FileDownloadName);
        }

        string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
        context.HttpContext.Response.TransmitFile(filePath);
    }
}