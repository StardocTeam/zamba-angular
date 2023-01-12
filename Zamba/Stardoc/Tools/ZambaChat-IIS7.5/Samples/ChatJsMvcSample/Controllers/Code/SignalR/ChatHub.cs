using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChatJs.Net;
using ChatJsMvcSample.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using ChatJsMvcSample.Code;
using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Models.ViewModels;
using ChatJsMvcSample.Controllers;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Cors;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChatJsMvcSample.Code.SignalR
{
    [HubName("chatHub")]
    [EnableCors("*", "*", "*")]
    public class ChatHub : Hub, IChatHub
    {
        public ChatHub()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Chat");
            }
            db = new ChatEntities(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

       
        public  ChatEntities db ;
        public static bool encrypt = bool.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["EncriptMessage"].ToString());
        /// <summary>
        /// This STUB. In a normal situation, there would be multiple rooms and the user room would have to be 
        /// determined by the user profile
        /// </summary>
        public const string ROOM_ID_STUB = "Zamba-Chat";
        private static List<ChatUser> chatUserInfo = new List<ChatUser>();
        public static List<ChatUser> ChatUserInfo
        {
            get
            {
                return chatUserInfo;
            }
            set
            {
            }
        }
        /// <summary>
        /// Current connections
        /// 1 room has many users that have many connections (2 open browsers from the same user represents 2 connections)
        /// </summary>
        private static readonly Dictionary<string, Dictionary<decimal, List<string>>> connections = new Dictionary<string, Dictionary<decimal, List<string>>>();         

        /// <summary>
        /// This method is STUB. This will SIMULATE a database of users
        /// </summary>
        private static readonly List<ChatUser> chatUsers = new List<ChatUser>();

        public DataTable GetUsersZambaInfo(decimal userId)
        {
            try
            {
                var QueryBuilder = new StringBuilder();
                QueryBuilder.Append("SELECT ID, NAME, PASSWORD, CRDATE, LUPDATE, STATE, DESCRIPTION, ");
                QueryBuilder.Append("ADDRESS_BOOK, EXPIRATIONTIME, EXPIRATIONDATE, ");
                QueryBuilder.Append("NOMBRES, APELLIDO, M.CORREO, TELEFONO, ");
                QueryBuilder.Append("PUESTO, FIRMA, FOTO, CONF_BASEMAIL, ");
                QueryBuilder.Append("CONF_MAILSERVER, CONF_MAILTYPE, SMTP ");
                QueryBuilder.Append("FROM usrtable u LEFT JOIN ZMAILCONFIG M ON u.ID = M.UserId WHERE id = ");
                QueryBuilder.Append(userId);
                return SQLEF.ExecuteSql(db, QueryBuilder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la consulta, " + ex.Message);
            }
        }

        public List<ChatGroupData> GetGroups(decimal myUserId)
        {
            try
            {
                var list = new List<ChatGroupData>();
                //using (var db = new ChatEntities())
                //{
                    var chatP = db.ChatPeople.Where(x => x.UserId == myUserId);
                    var chatsIds = chatP.Select(x => x.ChatId).ToList();
                    for (var i = 0; i <= chatsIds.Count() - 1; i++)
                    {
                        var id = chatsIds[i];
                        var chat = db.Chat.Where(x => x.Id == id).First();
                        if (chat.ChatType == ChatType.Group && chat.ChatHistory.Any())
                        {
                            var groupCP = chat.ChatPeople.ToList().Where(x => x.UserId != myUserId);
                            var group = new List<ChatUser>();
                            for (var j = 0; j <= groupCP.ToList().Count() - 1; j++)
                                group.Add(GetUserInfo(groupCP.ToList()[j].UserId));

                            var lastMsg = chat.ChatHistory.OrderBy(c => c.Id).LastOrDefault().Date;
                            list.Add(new ChatGroupData(group, lastMsg, chat.ChatName, id));
                        }
                    }
               // }
                return list.OrderByDescending(o => o.LastMessage).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        public List<ChatGroupDataForum> GetGroupsForum(decimal myUserId)
        {
            try
            {
                var list = new List<ChatGroupDataForum>();
                //using (var db = new ChatEntities())
                //{
                    var chatP = db.ChatPeople.Where(x => x.UserId == myUserId);
                    //var chatp2 = db.Chat.Where(x => x.AdminId == myUserId);
                    var chatsIds = chatP.Select(x => x.ChatId).ToList();
                    var DocId = db.Chat.Select(x => x.DocId).ToList();
                    for (var i = 0; i <= chatsIds.Count() - 1; i++)
                    {
                        var id = chatsIds[i];
                        var DocIds = DocId[i];
                        var chat = db.Chat.Where(x => x.Id == id).First();
                        if (chat.ChatType == ChatType.Group && chat.ChatHistory.Any())
                        {
                            var groupCP = chat.ChatPeople.ToList().Where(x => x.UserId != myUserId);
                            var group = new List<ChatUser>();
                            for (var j = 0; j <= groupCP.ToList().Count() - 1; j++)
                                group.Add(GetUserInfo(groupCP.ToList()[j].UserId));

                            var lastMsg = chat.ChatHistory.OrderBy(c => c.Id).LastOrDefault().Date;
                            list.Add(new ChatGroupDataForum(group, lastMsg, chat.ChatName, id, DocIds));
                        }
                    }
               // }
                return list.OrderByDescending(o => o.LastMessage).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }
        
        public void AddRemoveUsersGroupsForum(decimal chatId, decimal userId, bool add)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var chat = db.Chat.Where(x => x.Id == chatId).First();
                    var user = db.ChatUser.Where(x => x.Id == userId).First();
                    if (add)
                    {
                        chat.ChatPeople.Add(new ChatPeople() { ChatId = chatId, UserId = userId });
                    }
                    else
                    {
                        var cp = chat.ChatPeople.Where(x => x.ChatId == chatId && x.ChatId == userId).First();
                        chat.ChatPeople.Remove(cp);
                    }
                    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar/quitar usuario de grupo, " + ex.Message);
            }
        }

        public Chat OpenGroupChat(List<int> usersId)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var chat = new Chat();
                    var cp = db.ChatPeople.GroupBy(x => x.ChatId);
                    for (var i = 0; i <= cp.ToList().Count() - 1; i++)
                    {
                        List<decimal> cpUsers = cp.ToList()[i].Select(x => x.UserId).ToList();
                        cpUsers.Sort();
                        List<int> cpUsersInt = cpUsers.Select(s => Convert.ToInt32(s)).ToList();
                        if (cpUsersInt.SequenceEqual(usersId))
                        {
                            var chatId = cp.ToList()[i].Key;
                            chat = db.Chat.Where(x => x.Id == chatId).FirstOrDefault();
                            var ch = new List<ChatHistory>();
                            var chatHist = chat.ChatHistory.OrderBy(x => x.Id).Reverse().ToList();
                            for (var j = 0; j <= ((chatHist.Count <= 4) ? chatHist.Count - 1 : 4); j++)
                                ch.Add(chatHist[j]);

                            chat.ChatHistory.Clear();
                            ch.Reverse();
                            chat.ChatHistory = encrypt ? Encript.Decrypt(ch) : ch;
                            chat.ChatPeople = db.Chat.Where(x => x.Id == chatId).FirstOrDefault().ChatPeople;
                            break;
                        }
                    }
                    return chat;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }
        public Chat OpenGroupChat(decimal chatId)
        {
            try
            {
                //using (ChatEntities db = new ChatEntities())
                //{
                    var chat = new Chat();

                    chat = db.Chat.Where(x => x.Id == chatId).FirstOrDefault();
                    var ch = new List<ChatHistory>();
                    var chatHist = chat.ChatHistory.OrderBy(x => x.Id).Reverse().ToList();
                    for (var j = 0; j <= ((chatHist.Count <= 4) ? chatHist.Count - 1 : 4); j++)
                        ch.Add(chatHist[j]);

                    chat.ChatHistory.Clear();
                    ch.Reverse();
                    chat.ChatHistory = encrypt ? Encript.Decrypt(ch) : ch;
                    chat.ChatPeople = db.Chat.Where(x => x.Id == chatId).FirstOrDefault().ChatPeople;
                    return chat;
                //}
            }
            catch (FormatException ex)
            {
                throw new Exception("Problema al decodificar mensaje, compruebe si esta habilitada la codificacion, " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }
        public  decimal CreateEmptyChat(decimal myUserId, string groupName)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var chat = new Chat()
                    {
                        AdminId = myUserId,
                        LastMessage = DateTime.Now,
                        ChatType = ChatType.Group,
                        ChatName = groupName
                    };

                    db.Chat.Add(chat);
                    db.SaveChanges();

                    var cp = new ChatPeople()
                    {                      
                        ChatId = chat.Id,
                        UserId = myUserId
                    };
                    db.ChatPeople.Add(cp);
                    var msg = "Se creo el grupo " + groupName;
                    var chatHistory = new ChatHistory
                    {
                        ChatId = chat.Id,
                        UserId = myUserId,
                        Message = encrypt ? Encript.Encrypt(msg) : msg,
                        Date = DateTime.Now
                    };
                    db.ChatHistory.Add(chatHistory);
                    db.SaveChanges();
                    return chat.Id;
//                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear grupo, " + ex.Message);
            }
        }

        public  decimal CreateEmptyChatForum(decimal myUserId, string groupName, int? docId)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var chat = new Chat()
                    {
                        DocId = docId,
                        AdminId = myUserId,
                        LastMessage = DateTime.Now,
                        ChatType = ChatType.Group,
                        ChatName = groupName
                    };

                    db.Chat.Add(chat);
                    db.SaveChanges();

                    var cp = new ChatPeople()
                    {
                        ChatId = chat.Id,
                        UserId = myUserId,
                    
                    };
                    db.ChatPeople.Add(cp);
                    var msg = "Se creo el grupo " + groupName;
                    var chatHistory = new ChatHistory
                    {
                        ChatId = chat.Id,
                        UserId = myUserId,
                        Message = encrypt ? Encript.Encrypt(msg) : msg,
                        Date = DateTime.Now
                    };
                    db.ChatHistory.Add(chatHistory);
                    db.SaveChanges();
                    return chat.Id;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear grupo, " + ex.Message);
            }
        }

        public void AddRemoveUsersGroups(decimal chatId, decimal userId, bool add)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var chat = db.Chat.Where(x => x.Id == chatId).First();
                    var user = db.ChatUser.Where(x => x.Id == userId).First();
                    if (add)
                    {
                        chat.ChatPeople.Add(new ChatPeople() { ChatId = chatId, UserId = userId});
                    }
                    else
                    {
                        var cp = chat.ChatPeople.Where(x => x.ChatId == chatId && x.ChatId == userId).First();
                        chat.ChatPeople.Remove(cp);
                    }
                    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar/quitar usuario de grupo, " + ex.Message);
            }
        }

        ///Usar solo para SignalR agrega a cada usuario la fecha de la ultima conversacion del Chat
        public List<ChatUser> GetUsersChat(decimal myUserId)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var users = db.ChatUser.Where(x => x.Id != myUserId).ToList();
                    var c = db.ChatPeople.Where(x => x.UserId == myUserId).ToList();
                    var ccp = new List<ChatPeople>();
                    foreach (var cp in c)
                        ccp.AddRange(db.ChatPeople.Where(d => d.ChatId == cp.ChatId && d.UserId != myUserId).ToList());

                    foreach (var us in users)
                    {
                        var lM = new DateTime();
                        var eachUserCP = ccp.Where(x => x.UserId == us.Id);
                        foreach (ChatPeople cp in eachUserCP)
                        {
                            var chat = (db.Chat.Where(u => u.Id == cp.ChatId).First());
                            if (chat.ChatType==ChatType.Single&& lM < chat.LastMessage)
                                lM = chat.LastMessage;
                        }
                        us.LastMessage = lM;
                    }
                    return users;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        public List<ChatUser> GetUsersByChat(decimal chatId)
        {
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var c = db.ChatPeople.Where(x => x.ChatId == chatId).ToList();
                    var lcu = new List<ChatUser>();
                    foreach (ChatPeople cp in c)
                        lcu.Add(db.ChatUser.Where(d => d.Id == cp.UserId).First());
                    return lcu;
             //   }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios por id de chat, " + ex.Message);
            }
        }

        public List<Chat> GetNoReadUserHistory(decimal userId)
        {
            try
            {
                var chatsId = new List<decimal>();
                var thisUser = new ChatUser();
                var chatHistoryList = new List<ChatHistory>();

                //using (var db = new ChatEntities())
                //{
                    var chats = new List<ChatPeople>();
                    chats = db.ChatPeople.Where(u => u.UserId == userId).ToList();
                    chats.ForEach(cu => chatsId.Add(cu.ChatId));

                    thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    var history = new List<Chat>();

                    if (thisUser == null)
                        return history;

                    chatHistoryList = db.ChatHistory.Where(d => d.Date > thisUser.LastActiveOn).ToList();

                    var chatHistory = chatHistoryList.GroupBy(g => g.ChatId);

                    foreach (var chat in chatHistory)
                    {
                        if (chatsId.Contains(chat.Key))
                        {
                            Chat thisChat = db.Chat.Where(c => c.Id == chat.Key).FirstOrDefault();
                            Chat historyChat = new Chat()
                            {
                                AdminId = thisChat.AdminId,
                                ChatHistory = thisChat.ChatHistory.OrderByDescending(ch => ch.Id).Take(5).Reverse().ToList(),
                                ChatPeople = thisChat.ChatPeople,
                                Id = thisChat.Id
                            };
                            if (historyChat.ChatHistory.Reverse().FirstOrDefault().UserId != userId)
                                history.Add(encrypt ? Encript.Decrypt(historyChat) : historyChat);
                        }
                    }
                    LeaveChat(userId, false);// inicio sesion
                    return history;
//                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historial no leido, " + ex.Message);
            }
        }

        public void ChangeName(decimal userId, string name)
        {
            if (userId == 0) return;
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    if (thisUser != null)
                    {
                        thisUser.Name = name;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                    }
                //}

                var myRoomId = this.GetMyRoomId();
                if (connections.ContainsKey(myRoomId))
                {
                    foreach (var user in connections[myRoomId].Keys)
                    {
                        if (user == userId)
                            continue;

                        var userIdClusure = user;
                        var usersList = GetUserListChat().Where(u => u.Id != userIdClusure);
                        if (connections[myRoomId][user] != null)
                            foreach (var connectionId in connections[myRoomId][user])
                            {
                                string[] response = { userId.ToString(), name.ToString() };
                                this.Clients.Client(connectionId).changeName(response);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar nombre de usuario, " + ex.Message);
            }
        }

        public void ChangeGroupName(decimal userId, decimal chatId, string name)
        {
            if (chatId == 0) return;
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var thisChat = db.Chat.Where(u => u.Id == chatId).First();
                    thisChat.ChatName = name;
                    db.SaveChanges();
                //}
                var myRoomId = this.GetMyRoomId();
                if (connections.ContainsKey(myRoomId))
                {
                    foreach (var user in connections[myRoomId].Keys)
                    {
                        if (user == userId)
                            continue;

                        var userIdClusure = user;
                        var usersList = GetUserListChat().Where(u => u.Id != userIdClusure);
                        if (connections[myRoomId][user] != null)
                            foreach (var connectionId in connections[myRoomId][user])
                            {
                                string[] response = { userId.ToString(), chatId.ToString(), name.ToString() };
                                this.Clients.Client(connectionId).changeGroupName(response);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar nombre de grupo, " + ex.Message);
            }
        }
        // Para usar el mismo metodo se coloco opcional, Si True= Sale chat/ False ingresa al chat "Join"
        public void LeaveChat(decimal userId, bool leave = true)
        {
            if (userId == 0) return;

            try
            {
               // using (var db = new ChatEntities())
                //{
                    var thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    if (thisUser != null)
                    {
                        thisUser.Status = (leave == true) ? ChatUser.StatusType.Offline : ChatUser.StatusType.Online;
                        thisUser.LastActiveOn = DateTime.Now;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                    }
            //    }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        var error = String.Format("Property: {0} Error: {1}",
                                                  validationError.PropertyName,
                                                  validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        public void DisableChat(decimal userId, bool disable)
        {
            try
            {
              //  using (var db = new ChatEntities())
               // {
                    var thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    thisUser.Role = (disable) ? ChatUser.RoleType.Listener : ChatUser.RoleType.Active;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al deshabilitar/habilitar chat, " + ex.Message);
            }
        }

        public string ReduceBase64Img(string img)
        {
            try
            {
                var image = ImageController.ResizeBase64(ReplaceDataAttrImg(img));
                return image;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar nueva imagen, " + ex.Message);
            }
        }

        private string ReplaceDataAttrImg(string img)
        {
            var exts = new string[] { "jpg", "jpeg", "png", "bmp", "gif" };
            foreach (string ext in exts)
            {
                img = img.Replace("data:image/" + ext + ";base64,", string.Empty);
            }
            return img;
        }

        public  void ChangeAvatar(decimal userId, string avatar)
        {
            try
            {
                if (avatar == "default")
                {
                    using (var fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "ChatJs/images/defaultAvatar.png"))
                    {
                        using (var image = Image.FromStream(fs))
                        {
                            avatar = ImageController.ResizeImage(image);
                        }
                    }
                }
                else
                {
                    avatar = ImageController.ResizeBase64(avatar);
                }
//                using (var db = new ChatEntities())
  //              {
                    ChatUser thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    thisUser.Avatar = avatar;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
    //            }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string errEx = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    errEx += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errEx += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el nuevo avatar, " + ex.Message);
            }
        }

        public  void LeaveChatGroup(decimal chatId, decimal userId)
        {
            try
            {
      //          using (var db = new ChatEntities())
        //        {
                    var chat = db.Chat.Where(c => c.Id == chatId).First();
                    if (chat.ChatPeople.ToList().Count == 1 && chat.ChatPeople.First().UserId == userId)
                        db.Chat.Remove(chat);
                    else
                    {
                        var chatPeople = db.ChatPeople.Where(u => u.ChatId == chatId && u.UserId == userId);
                        if (chatPeople.Any())
                            db.ChatPeople.Remove(chatPeople.First());
                    }
                    db.SaveChanges();
          //      }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario del grupo, " + ex.Message);
            }
        }

        public void ChangeStatus(decimal userId, int status)
        {
            try
            {
            //    using (var db = new ChatEntities())
              //  {
                    ChatUser thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    switch (status)
                    {
                        case 0:
                            thisUser.Status = ChatUser.StatusType.Offline;
                            break;
                        case 1:
                            thisUser.Status = ChatUser.StatusType.Online;
                            break;
                        case 2:
                            thisUser.Status = ChatUser.StatusType.Busy;
                            break;
                        case 3:
                            thisUser.Status = ChatUser.StatusType.DontDisturb;
                            break;
                    }
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    var myRoomId = this.GetMyRoomId();
                    var myUserId = userId;
                    if (connections.ContainsKey(myRoomId))
                    {
                        foreach (var user in connections[myRoomId].Keys)
                        {
                            // we don't want to broadcast to the current user
                            if (user == myUserId)
                                continue;

                            var userIdClusure = user;

                            // creates a list of users that contains all users with the exception of the user to which 
                            // the list will be sent
                            // every user will receive a list of user that exclude him/hearself

                            var usersList = GetUserListChat().Where(u => u.Id != userIdClusure);
                            if (connections[myRoomId][user] != null)
                                foreach (var connectionId in connections[myRoomId][user])
                                {
                                    string[] response = { userId.ToString(), status.ToString(), (status == -1) ? thisUser.Avatar : "" };
                                    this.Clients.Client(connectionId).changeStatus(response);
                                }
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        public  void LoadUsersFromDB()
        {
            try
            {
                db.ChatUser.ToList().ForEach(cu => chatUsers.Add(cu));
            }
            catch (Exception ex)
            {
                throw new Exception("Message, " + ex.Message + ", InnerException: " + ex.InnerException + "StackTrace" + ex.StackTrace + "tostring" + ex.ToString());
            }
        }

        /// <summary>
        /// If the specified user is connected, return information about the user
        /// </summary>
        public  ChatUser GetUserInfo(decimal userId)
        {
            var userStored = ChatUserInfo == null ? null : ChatUserInfo.Where(x => x.Id == userId).FirstOrDefault();
            if (userStored != null) return userStored;
            try
            {
                //using (var db = new ChatEntities())
                //{
                    var user = db.ChatUser.Where(cu => cu.Id == userId).FirstOrDefault();
                    if (user == null)
                    {
                        //Obtengo datos de USRTABLE para generar nuevo ChatUser
                        var zambauser = db.Database.SqlQuery<ZambaUser>(
                      string.Format("SELECT * FROM usrtable where id = {0}", userId)).FirstOrDefault<ZambaUser>();

                        if (zambauser != null && zambauser.NAME.Length > 0)
                        {
                            var defPass = Encript.Encrypt("123456");
                            user = new ChatUser()
                            {
                                Id = userId,
                                Name = zambauser.NAME,
                                Avatar = ImageController.GetDefaultAvatarImageB64(),
                                LastActiveOn = DateTime.Now,
                                Status = ChatUser.StatusType.Online,
                                Password = defPass,
                                RetryPassword = defPass,
                         
                            };
                            db.ChatUser.Add(user);
                            db.SaveChanges();
                            //  ChatUserInfo.Add(user);
                            //return user;
                        }
                        else
                            return new ChatUser();
                    }
                    //ChatUserInfo.Add(user);
                    return user;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar session en Zamba.Link, " + ex.Message);
            }
        }






        private ChatUser GetUserById(int id)
        {
            var myRoomId = this.GetMyRoomId();
            var dbUser = chatUsers.First(u => u.Id == id);
            ChatUser.StatusType userStatus;
            lock (connections)
            {
                userStatus = connections.ContainsKey(myRoomId)
                                 ? (connections[myRoomId].ContainsKey(dbUser.Id)
                                        ? ChatUser.StatusType.Online
                                        : ChatUser.StatusType.Offline)
                                 : ChatUser.StatusType.Offline;
            }
            return new ChatUser()
            {
                Id = dbUser.Id,
                Name = dbUser.Name,
                Status = userStatus,
                Avatar = dbUser.Avatar,
            };
        }

        public string GetMyRoomId()
        {
            return ROOM_ID_STUB;
        }

        private void NotifyUsersChanged()
        {
            var myRoomId = this.GetMyRoomId();
            decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());
            if (connections.ContainsKey(myRoomId))
            {
                foreach (var userId in connections[myRoomId].Keys.ToList())
                {
                    // we don't want to broadcast to the current user
                    if (userId == myUserId)
                        continue;

                    var userIdClusure = userId;

                    //var usersList = GetUsersChat(myUserId).Where(u => u.Id != userIdClusure);                    
                    var usersList = GetUserListChat().Where(u => u.Id != userIdClusure);
                    if (connections[myRoomId][userId] != null)
                        foreach (var connectionId in connections[myRoomId][userId])
                            this.Clients.Client(connectionId).usersListChanged(usersList);
                }
            }

        }

        private Chat PersistMsgUsers(decimal userId, string message, ChatType chatType, string clientGuid)
        {
            var usersId = new List<decimal>();
            usersId.Add(userId);
            try
            {
                Chat chat;
        //        using (var db = new ChatEntities())
          //      {
                    decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());
                    var myUser = GetUserListChat().FirstOrDefault(u => u.Id == myUserId);
                    var usersids = usersId;
                    if (!(chatType == ChatType.Group && usersId.Count == 1 && usersId[0] == myUserId))
                        usersids.Add(myUserId);

                    var groupCP = db.ChatPeople.GroupBy(c => c.ChatId).Where(c => c.Count() == usersids.Count()).ToList();
                    decimal existChatId = ChatController.ExistChatByCP(groupCP, chatType, usersId);

                    if (existChatId != 0)
                    {
                        var chatContext = db.Chat.ToList().Where(c => c.Id == existChatId);
                        var thisChat = chatContext.ToList().FirstOrDefault();
                        chat = thisChat;

                        var chatHistory = new ChatHistory
                        {
                            ChatId = existChatId,
                            UserId = myUser.Id,
                            Message = encrypt ? Encript.Encrypt(message) : message,
                            Date = DateTime.Now
                        };
                        chat.LastMessage = DateTime.Now;
                        chat.ChatHistory.Add(chatHistory);
                        chat.ChatPeople.Add(thisChat.ChatPeople.FirstOrDefault());//sirve para que chatpeople no pierda valor
                        db.ChatHistory.Add(chatHistory);
                        db.SaveChanges();
                    }
                    else
                    {
                        string usName = "";
                        foreach (decimal uId in usersids)
                        {
                            usName = usName + db.ChatUser.Where(x => x.Id == uId).First().Name.Split('/')[0] + ",";
                        }

                        chat = new Chat()
                        {
                            AdminId = myUserId,
                            LastMessage = DateTime.Now,
                            ChatType = chatType,
                            ChatName = chatType == ChatType.Group ? usName.Remove(usName.Length - 1).Substring(0, 20) : string.Empty,
                        };

                        db.Chat.Add(chat);
                        db.SaveChanges();

                        var cp = new List<ChatPeople>();
                        foreach (int user in usersids)
                        {
                            cp.Add(new ChatPeople
                            {
                                ChatId = chat.Id,
                                UserId = user
                            });
                        }
                        cp.ForEach(c => db.ChatPeople.Add(c));

                        var chatHistory = new ChatHistory
                        {
                            ChatId = chat.Id,
                            UserId = myUser.Id,
                            Message = encrypt ? Encript.Encrypt(message) : message,
                            Date = DateTime.Now
                        };

                        db.ChatHistory.Add(chatHistory);
                        db.SaveChanges();
                    }
            //    }
                chat = encrypt ? Encript.Decrypt(chat) : chat;
                return chat;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar en la base de datos, " + ex.Message);
            }
        }

        private Chat PersistMsgUsers(decimal chatId, string message, ChatType chatType)
        {
            try
            {
                Chat chat;
           //     using (var db = new ChatEntities())
             //   {
                    decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());
                    var myUser = GetUserListChat().FirstOrDefault(u => u.Id == myUserId);
                    var chatContext = db.Chat.ToList().Where(c => c.Id == chatId);
                    var thisChat = chatContext.ToList().FirstOrDefault();
                    chat = thisChat;

                    var chatHistory = new ChatHistory
                    {
                        ChatId = chatId,
                        UserId = myUser.Id,
                        Message = encrypt ? Encript.Encrypt(message) : message,
                        Date = DateTime.Now
                    };
                    chat.LastMessage = DateTime.Now;
                    chat.ChatHistory.Add(chatHistory);
                    chat.ChatPeople.Add(thisChat.ChatPeople.FirstOrDefault());//sirve para que chatpeople no pierda valor
                    db.ChatHistory.Add(chatHistory);
                    db.SaveChanges();
               // }
                chat = encrypt ? Encript.Decrypt(chat) : chat;
                return chat;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar en la base de datos, " + ex.Message);
            }
        }

        public decimal GetChatId(List<decimal> usersId)
        {
       //     using (var db = new ChatEntities())
         //   {
                var groupCP = db.ChatPeople.GroupBy(c => c.ChatId).Where(c => c.Count() == 2).ToList();
                decimal existChatId = 0;
                foreach (var gcp in groupCP)
                {
                    var glist = gcp.ToList().Select(x => x.UserId).ToList();
                    List<int> usersIdInt = usersId.Select(s => Convert.ToInt32(s)).ToList();
                    List<int> glistInt = glist.Select(s => Convert.ToInt32(s)).ToList();
                    if (!glistInt.Except(usersIdInt).Any())
                    {
                        var chat = db.Chat.Where(x => x.Id == gcp.Key).First();
                        if (chat.ChatType == ChatType.Single)
                        {
                            existChatId = gcp.Key;
                            break;
                        }
                    }
                }
                if (existChatId == 0)//Creo un chat vacio
                {
                    var chat = new Chat()
                    {
                        AdminId = usersId[0],
                        LastMessage = DateTime.Now,
                        ChatType = ChatType.Single,
                        ChatName = string.Empty,
                    };
                    db.Chat.Add(chat);
                    db.SaveChanges();

                    var cp = new List<ChatPeople>();
                    foreach (int user in usersId)
                    {
                        cp.Add(new ChatPeople
                        {
                            ChatId = chat.Id,
                            UserId = user
                        });
                    }
                    cp.ForEach(c => db.ChatPeople.Add(c));
                    db.SaveChanges();
                    existChatId = chat.Id;
                }
                return existChatId;
           // }
        }

        #region IChatHub
        public List<ChatMessage> GetMoreMsgHistory(List<decimal> usersId, int cant, ChatType chatType = ChatType.Single)
        {
            int getMsgNum = 5;
            getMsgNum *= cant + 1;
            var msg = new List<ChatHistory>();
            try
            {
             //   using (var db = new ChatEntities())
               // {
                    var groupCP = db.ChatPeople.GroupBy(c => c.ChatId).Where(c => c.Count() == usersId.Count()).ToList();
                    decimal existChatId = 0;
                    foreach (var gcp in groupCP)
                    {
                        var glist = gcp.ToList().Select(x => x.UserId).ToList();

                        List<int> usersIdInt = usersId.Select(s => Convert.ToInt32(s)).ToList();
                        List<int> glistInt = glist.Select(s => Convert.ToInt32(s)).ToList();
                        //Estaba convertido en decimal por Ids de Oracle 'Except' funciona con Int
                        if (!glistInt.Except(usersIdInt).Any())
                        {
                            var chat = db.Chat.Where(x => x.Id == gcp.Key).First();
                            if (chat.ChatType == chatType)
                            {
                                existChatId = gcp.Key;
                                break;
                            }
                        }
                    }
                    if (existChatId != 0)
                        msg = db.ChatHistory.Where(c => c.ChatId == existChatId).OrderByDescending(d => d.Date).Take(getMsgNum).ToList();
               // }
                msg.Reverse();
                var cMsgList = new List<ChatMessage>();
                for (var i = 0; i < msg.Count(); i++)
                {
                    var cMsg = new ChatMessage
                    {
                        UserFromId = msg[i].UserId,
                        DateTime = msg[i].Date,
                        Message = msg[i].Message
                    };
                    cMsgList.Add(cMsg);
                }
                if (encrypt) cMsgList = Encript.Decrypt(cMsgList);
                return cMsgList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar obtener mensajes antiguos de la base de datos, " + ex.Message);
            }
        }

        public List<ChatMessage> GetMoreMsgHistory(List<decimal> usersId, decimal chatId, int cant)
        {
            int getMsgNum = 5;

            getMsgNum *= cant + 1;
            var msg = new List<ChatHistory>();
            try
            {
          //      using (var db = new ChatEntities())
            //    {
                    if (chatId == 0)
                    {
                        chatId = GetChatId(usersId);
                    }
                    if (chatId > 0)
                        msg = db.ChatHistory.Where(c => c.ChatId == chatId).OrderByDescending(d => d.Date).Take(getMsgNum).ToList();
              //  }
                msg.Reverse();
                var cMsgList = new List<ChatMessage>();
                for (var i = 0; i < msg.Count(); i++)
                {
                    var cMsg = new ChatMessage
                    {
                        UserFromId = msg[i].UserId,
                        DateTime = msg[i].Date,
                        Message = msg[i].Message
                    };
                    cMsgList.Add(cMsg);
                }
                if (encrypt) cMsgList = Encript.Decrypt(cMsgList);
                return cMsgList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar obtener mensajes antiguos de la base de datos, " + ex.Message);
            }
        }

        public List<ChatMessage> GetMessageHistory(List<decimal> usersId, ChatType chatType)//, int cant)
        {
            return GetMoreMsgHistory(usersId, 0, chatType);
        }

        [Obsolete("Use GetUsers instead")]
        public List<ChatUser> GetUsersList()
        {
            try
            {
                decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());

                var myRoomId = this.GetMyRoomId();

                var users = this.GetUsersChat(myUserId);//Ordenado, llamada por SignalR
                var roomUsers = users.Where(u => u.RoomId == myRoomId && u.Id != myUserId).OrderBy(u => u.Name).ToList();

                return roomUsers;
            }
            catch (Exception e)
            {

                throw e;
            }

         
        }

        public List<ChatUser> GetUserListChat()
        {
           // using (var db = new ChatEntities())
           // {
                return db.ChatUser.ToList();
           // }
        }

        public void SendMsgToUsers(decimal usersId, decimal chatId, string message, ChatType chatType, string clientGuid)
        {
            decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());
            //    var myUserId = this.GetMyUserId();
            var myRoomId = this.GetMyRoomId();

            var dbChatMessage = chatId > 0 ? this.PersistMsgUsers(chatId, message, chatType) : this.PersistMsgUsers(usersId, message, chatType, clientGuid);
            var connectionIds = new List<string>();
            lock (connections)
            {
                var conUsers = connections.Values.FirstOrDefault().Keys;
                foreach (var user in conUsers)
                {
                    if (connections[myRoomId].ContainsKey(user))
                        connectionIds.AddRange(connections[myRoomId][user]);
                }

                if (connections[myRoomId].ContainsKey(myUserId))
                    connectionIds.AddRange(connections[myRoomId][myUserId]);
            }
            foreach (var connectionId in connectionIds)
                this.Clients.Client(connectionId).sendMsgToUsers(dbChatMessage);
        }

        public void SendTypingSignal(int otherUserId)
        {
            decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());
            var myRoomId = this.GetMyRoomId();

            var connectionIds = new List<string>();
            lock (connections)
            {
                if (connections[myRoomId].ContainsKey(otherUserId))
                    connectionIds.AddRange(connections[myRoomId][otherUserId]);
            }
            foreach (var connectionId in connectionIds)
                this.Clients.Client(connectionId).sendTypingSignal(myUserId);
        }

        public override Task OnConnected()
        {
            decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());
            var myRoomId = this.GetMyRoomId();
            //ver de colocar esto para que los demas usuarios vean que estoy conectado, colocado en el joinchat
            LeaveChat(myUserId, false);
            lock (connections)
            {
                if (!connections.ContainsKey(myRoomId))
                    connections[myRoomId] = new Dictionary<decimal, List<string>>();

                if (!connections[myRoomId].ContainsKey(myUserId))
                {
                    // in this case, this is a NEW connection for the current user,
                    // not another browser window opening
                    connections[myRoomId][myUserId] = new List<string>();
                    this.NotifyUsersChanged();
                }
                connections[myRoomId][myUserId].Add(this.Context.ConnectionId);
            }

            return base.OnConnected();
        }

        //public void OnChangeStatus(int userId, int status)
        //{
        //    var myRoomId = this.GetMyRoomId();
        //    var myUserId = userId;

        //    lock (connections)
        //    {
        //        if (connections.ContainsKey(myRoomId))
        //            if (connections[myRoomId].ContainsKey(myUserId))
        //                if (connections[myRoomId][myUserId].Contains(this.Context.ConnectionId))
        //                {
        //                    connections[myRoomId][myUserId].Remove(this.Context.ConnectionId);
        //                    if (!connections[myRoomId][myUserId].Any())
        //                    {
        //                        connections[myRoomId].Remove(myUserId);
        //                        Task.Run(() =>
        //                        {
        //                            // this will run in separate thread.
        //                            // If the user is away for more than 10 seconds it will be removed from 
        //                            // the room.
        //                            // In a normal situation this wouldn't be done because normally the users in a
        //                            // chat room are fixed, like when you have 1 chat room for each tenancy
        //                            //cambie 10 segundos por 1 segundos.
        //                            Thread.Sleep(1000);
        //                            if (!connections[myRoomId].ContainsKey(myUserId))
        //                            {
        //                                var myDbUser = chatUsers.FirstOrDefault(u => u.Id == myUserId);
        //                                if (myDbUser != null)
        //                                {
        //                                    //chatUsers.Remove(myDbUser); aca eliminaba el usuario
        //                                    this.NotifyUsersChanged();
        //                                }
        //                            }
        //                        });
        //                    }
        //                }
        //    }
        //  //  return null;// base.OnChangeStatus()
        //}

        public override Task OnDisconnected(bool stopCalled)
        {
            var myRoomId = this.GetMyRoomId();
            decimal myUserId = decimal.Parse(this.Context.Request.QueryString["UserId"].ToString());

            //  LeaveChat(myUserId);
            lock (connections)
            {
                if (connections.ContainsKey(myRoomId))
                    if (connections[myRoomId].ContainsKey(myUserId))
                        if (connections[myRoomId][myUserId].Contains(this.Context.ConnectionId))
                        {
                            connections[myRoomId][myUserId].Remove(this.Context.ConnectionId);
                            if (!connections[myRoomId][myUserId].Any())
                            {
                                connections[myRoomId].Remove(myUserId);
                                Task.Factory.StartNew(() =>
                                Task.Run(() => 
                                {                                  
                                    if (!connections[myRoomId].ContainsKey(myUserId))
                                    {
                                        var myDbUser = chatUsers.FirstOrDefault(u => u.Id == myUserId);
                                        if (myDbUser != null)
                                        {                                            
                                            this.NotifyUsersChanged();
                                        }
                                    }
                                }));
                            }
                        }
            }

            return base.OnDisconnected(stopCalled);
        }
        #endregion
    }

    //internal class zambauser
    //{
    //    public Int64 Id { get; set; }
    //    public string Name { get; set; }
    //}
}