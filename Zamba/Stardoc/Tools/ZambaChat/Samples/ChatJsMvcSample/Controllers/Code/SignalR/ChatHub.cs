using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChatJs.Net;// quitar
using ChatJsMvcSample.Models; //Colocar
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using ChatJsMvcSample.Code;
using ChatJsMvcSample.Code.LongPolling;
using ChatJsMvcSample.Code.LongPolling.Chat;
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
using System.Data.Objects;


namespace ChatJsMvcSample.Code.SignalR
{
    public class ChatHub : Hub, IChatHub//revisar si lo saco
    {
      
       public static ChatEntities db = new ChatEntities();
        
        /// <summary>
        /// This STUB. In a normal situation, there would be multiple rooms and the user room would have to be 
        /// determined by the user profile
        /// </summary>
        public const string ROOM_ID_STUB = "Zamba-Chat";

        /// <summary>
        /// Current connections
        /// 1 room has many users that have many connections (2 open browsers from the same user represents 2 connections)
        /// </summary>
        private static readonly Dictionary<string, Dictionary<decimal, List<string>>> connections = new Dictionary<string, Dictionary<decimal, List<string>>>();

        /// <summary>
        /// This is STUB. This will SIMULATE a database of chat messages
        /// </summary>
        private static readonly List<ChatMessage> chatMessages = new List<ChatMessage>();

        /// <summary>
        /// This method is STUB. This will SIMULATE a database of users
        /// </summary>
        private static readonly List<ChatUser> chatUsers = new List<ChatUser>();
        
    //    private GetUserFromDB{LoadUsersFromDB();//
    
    //}
        /// <summary>
        /// This method is STUB. In a normal situation, the user info would come from the database so this method wouldn't be necessary.
        /// It's only necessary because this class is simulating the database
        /// </summary>
        /// <param name="newUser"></param>
        public static void RegisterNewUser(ChatUser newUser)
        {
            if (newUser == null) throw new ArgumentNullException("newUser");
            chatUsers.Add(newUser);
        }


        public List<List<ChatUser>> GetGroups(decimal myUserId)
        {          
            try
            {
                var chatsIds= new List<decimal>();
                var list = new List<List<ChatUser>>();
                var lDate = new List<DateTime>();
                using (ChatEntities db = new ChatEntities())
                {
                    var cp = db.ChatPeople.Where(x => x.UserId == myUserId);               
                    for (var i = 0; i <= cp.Count() - 1; i++)
                        chatsIds.Add(cp.ToList()[i].ChatId);
                }
           
                using (ChatEntities db = new ChatEntities())
                {                   
                    for (var i = 0; i <= chatsIds.Count()-1; i++)
                    {
                        decimal id = chatsIds.ToList()[i];
                        var groupCP = db.ChatPeople.Where(x => x.ChatId == id);
                        if (groupCP.ToList().Count() > 2)
                        {
                            var group = new List<ChatUser>();
                            for (var j = 0; j <= groupCP.ToList().Count()-1; j++)    
                            {
                                if(myUserId!=groupCP.ToList()[j].UserId)
                                    group.Add(GetUserInfo(groupCP.ToList()[j].UserId));         
                            }
                            var chatId=groupCP.ToList()[0].ChatId;
                            lDate.Add (db.Chat.Where(x => x.Id == chatId).FirstOrDefault().ChatHistory.OrderBy(c=>c.Id).LastOrDefault().Date);
                            list.Add(group);
                        }
                    }
                }

                var dateSort = new List<DateTime>(lDate).OrderByDescending(i => i).ToList();               
                var orderLst = new List<List<ChatUser>>();
                for (var i = 0; i <= lDate.ToList().Count() - 1; i++)
                {
                    for (var j = 0; j <= lDate.ToList().Count() - 1; j++)
                    {
                        if (dateSort[i] == lDate[j])
                        {
                            orderLst.Add(list[j]);
                            break;
                        }
                    }
                }                
                return orderLst.Take(10).ToList();               
            }

            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }


        public Chat OpenGroupChat(List<int> usersId)
        {       
            try
            {
                using (ChatEntities db = new ChatEntities())
                {           
                    var chat = new Chat();
                    var cp = db.ChatPeople.GroupBy(x=>x.ChatId); 
                    for (var i = 0; i <= cp.ToList().Count() - 1; i++)
                    {
                        List<decimal> cpUsers = cp.ToList()[i].Select(x => x.UserId).ToList();
                        cpUsers.Sort();
                        List<int> cpUsersInt = cpUsers.Select(s => Convert.ToInt32(s)).ToList();
                        if (cpUsersInt.SequenceEqual(usersId))
                        {
                            var chatId = cp.ToList()[i].Key;                          
                            chat = db.Chat.Where(x => x.Id== chatId).FirstOrDefault();                        
                            var ch = new List<ChatHistory>();                          
                            var chatHist = chat.ChatHistory.OrderBy(x => x.Id).Reverse().ToList();
                            for (var j = 0; j <= ((chatHist.Count <= 4) ? chatHist.Count -1 : 4); j++)                          
                                ch.Add(chatHist[j]);                               
                          
                         chat.ChatHistory.Clear();
                         ch.Reverse();
                         chat.ChatHistory = ch;
                         chat.ChatPeople = db.Chat.Where(x => x.Id == chatId).FirstOrDefault().ChatPeople;
                         break;
                        }
                    }
                    return chat;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        public List<ChatUser> GetUsersChat()
        {
          //  var myUserId = GetMyUserId();
            try
            {
                using (ChatEntities db = new ChatEntities())
                {
                    return db.ChatUser.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }


        public List<Chat> GetNoReadUserHistory(decimal userId)
        {
            try
            {
                List<decimal> chatsId = new List<decimal>();
                ChatUser thisUser = new ChatUser();
                List<ChatHistory> chatHistoryList = new List<ChatHistory>();

                using (ChatEntities db = new ChatEntities())
                {                  
                    List<ChatPeople> chats = new List<ChatPeople>();
                    chats = db.ChatPeople.Where(u => u.UserId == userId).ToList();
                    chats.ForEach(cu => chatsId.Add(cu.ChatId));

                    thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    List<Chat> history = new List<Chat>();

                    if (thisUser == null)
                        return history;

                    var chatHistory = chatHistoryList.GroupBy(g => g.ChatId);

                    foreach (var chat in chatHistory)
                    {
                        if (chatsId.Contains(chat.Key))
                        {
                            Chat thisChat = db.Chat.Where(c => c.Id == chat.Key).FirstOrDefault();
                            Chat historyChat = new Chat();
                            historyChat.AdminId = thisChat.AdminId;
                            historyChat.ChatHistory = thisChat.ChatHistory.OrderByDescending(ch => ch.Id).Take(5).Reverse().ToList();
                            historyChat.ChatPeople = thisChat.ChatPeople;
                            historyChat.Id = thisChat.Id;
                         if (historyChat.ChatHistory.Reverse().FirstOrDefault().UserId!=userId)
                            history.Add(historyChat);                         
                        }
                    }
                    LeaveChat(userId, false);// inicio sesion
                    return history;               
                }
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
                using (ChatEntities db = new ChatEntities())
                {
                    ChatUser thisUser = db.ChatUser.Where(u => u.Id == userId).First();
                    thisUser.Name = name;                 
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar nombre de usuario, " + ex.Message);
            }
        }

        // Para usar el mismo metodo se coloco opcional, Si True= Sale chat/ False ingresa al chat "Join"
        public void LeaveChat(decimal userId, bool leave = true)
        {
            if (userId == 0) return;

            try
            {
                using (ChatEntities db = new ChatEntities())
                {

                    ChatUser thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    thisUser.Status = (leave == true) ? ChatUser.StatusType.Offline : ChatUser.StatusType.Online;
                    thisUser.LastActiveOn = DateTime.Now;
                    db.SaveChanges();
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
                using (ChatEntities db = new ChatEntities())
                {
                    ChatUser thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    thisUser.Role = (disable) ? ChatUser.RoleType.Listener : ChatUser.RoleType.Active;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al deshabilitar/habilitar chat, " + ex.Message);
            }
        }


        public void ChangeAvatar(decimal userId, string avatar)
        {
            try
            {
                var ic = new ImageController();
                if (avatar == "default")
                {
                    using (var fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "ChatJs/images/defaultAvatar.jpg"))
                    {
                        using (var image = Image.FromStream(fs))
                        {
                            avatar = ic.ResizeImage(image);
                        }
                    }
                }
                else
                {
                   avatar = ic.ResizeBase64(avatar);
                    //byte[] imageBytes = Convert.FromBase64String(avatar);
                    //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    //ms.Write(imageBytes, 0, imageBytes.Length);
                    //avatar = ic.ResizeImage(Image.FromStream(ms, true));

                }
                using (ChatEntities db = new ChatEntities())
                {
                    ChatUser thisUser = db.ChatUser.Where(u => u.Id == userId).FirstOrDefault();
                    thisUser.Avatar = avatar;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el nuevo avatar, " + ex.Message);
            }
        }


        public void ChangeStatus(decimal userId, int status)
        {
            try
            {
                using (ChatEntities db = new ChatEntities())
                {
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
                    db.SaveChanges();
                    //OnChangeStatus(userId, status);
                    // NotifyUsersChanged();                   

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
                            var usersList = GetUsersChat().Where(u => u.Id != userIdClusure);

                            if (connections[myRoomId][user] != null)
                                foreach (var connectionId in connections[myRoomId][user]) {
                                    string[] response = { userId.ToString(), status.ToString(), (status==-1)?thisUser.Avatar:"" };
                                    this.Clients.Client(connectionId).changeStatus(response);
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        public static void LoadUsersFromDB()
        {  
            try
            {                              
                //if (db.ChatUser.Count() >= 1)                   
                db.ChatUser.ToList().ForEach(cu => chatUsers.Add(cu));              
            }
            catch(Exception ex)
            {
                throw new Exception("Error al cargar usuarios, " + ex.Message);
            }
        }

        /// <summary>
        /// This method is STUB. Returns if a user is registered in the FAKE DB.
        /// Normally this wouldn't be necessary.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsUserRegistered(ChatUser user)
        {
            return chatUsers.Any(u => u.Id == user.Id);
        }

        /// <summary>
        /// Tries to find a user with the provided e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static ChatUser FindUserById(decimal id)
        {
          //  ChatUser s = this.GetUserInfo(id);
           //  return GetUserInfo(id);
            return string.IsNullOrEmpty(id.ToString()) ? null : GetUserInfo(id);//.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// If the specified user is connected, return information about the user
        /// </summary>
        public static ChatUser GetUserInfo(decimal userId)
        {
            try
            {
                using (ChatEntities db = new ChatEntities())
                {
                    return db.ChatUser.Where(cu => cu.Id == userId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de usuarios, " + ex.Message);
            }
        }

        private ChatUser GetUserById(int id)
        {
            var myRoomId = this.GetMyRoomId();

            // this is STUB. Normally you would go to the database get the real user
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
               Avatar=dbUser.Avatar,
              // Avatar "",//ProfilePictureUrl = GravatarHelper.GetGravatarUrl(GravatarHelper.GetGravatarHash(dbUser.Email), GravatarHelper.Size.s32)
            };
        }

        /// <summary>
        /// Returns my user id
        /// </summary>
        /// <returns></returns>
        private decimal GetMyUserId()
        {
       
            return ChatHelper.GetChatUserFromCookie(this.Context.Request).Id;
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
        /// Broadcasts to all users in the same room the new users list
        /// </summary>
        /// <param name="myUserId">
        /// user Id that has to be excluded in the broadcast. That is, all users
        /// should receive the message, except this.
        /// </param>
        private void NotifyUsersChanged()
        {
            var myRoomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId();


            if (connections.ContainsKey(myRoomId))
            {
                foreach (var userId in connections[myRoomId].Keys.ToList())
                {
                    // we don't want to broadcast to the current user
                    if (userId == myUserId)
                        continue;

                    var userIdClusure = userId;

                    // creates a list of users that contains all users with the exception of the user to which 
                    // the list will be sent
                    // every user will receive a list of user that exclude him/hearself
                    var usersList = GetUsersChat().Where(u => u.Id != userIdClusure);

                    if (connections[myRoomId][userId] != null)
                        foreach (var connectionId in connections[myRoomId][userId])
                            this.Clients.Client(connectionId).usersListChanged(usersList);
                }
            }

        }

        private ChatMessage PersistMessage(int otherUserId, string message, string clientGuid)
        {
            var myUserId = this.GetMyUserId();

            // this is STUB. Normally you would go to the real database to get the my user and the other user
            var myUser = chatUsers.FirstOrDefault(u => u.Id == myUserId);
            var otherUser = chatUsers.FirstOrDefault(u => u.Id == otherUserId);

            if (myUser == null || otherUser == null)
                return null;

            var chatMessage = new ChatMessage
            {
                DateTime = DateTime.Now,
               // Date = DateTime.Now, colocar
                Message = message,
                ClientGuid = clientGuid, // quitado
                UserFromId = myUserId,
               // UserId = myUserId, colocar
                UserToId = otherUserId,// quitado
            };

            // this is STUB. Normally you would add the dbMessage to the real database
            chatMessages.Add(chatMessage);

            // normally you would save the database changes
            //this.db.SaveChanges();

            return chatMessage;
        }

        private Chat PersistMsgUsers(List<decimal> usersId, string message, string clientGuid)
        {
            try
            {
                Chat chat;
                using (ChatEntities db = new ChatEntities())
                {
                    var myUserId = this.GetMyUserId();
                    var myUser = GetUsersChat().FirstOrDefault(u => u.Id == myUserId);
                    // var intList = usersId.Select(s => Convert.ToInt32(s)).ToList();
                    var usersids = usersId;
                    usersids.Add(myUserId);

                    var groupCP = db.ChatPeople.GroupBy(c => c.ChatId).Where(c => c.Count() == usersids.Count()).ToList();
                    decimal existChatId = 0;
                    foreach (var gcp in groupCP)
                    {
                        var glist = gcp.ToList().Select(x => x.UserId).ToList();

                        List<int> usersIdInt = usersId.Select(s => Convert.ToInt32(s)).ToList();
                        List<int> glistInt = glist.Select(s => Convert.ToInt32(s)).ToList();

                        if (!glistInt.Except(usersIdInt).Any())
                        {
                            existChatId = gcp.Key;
                            break;
                        }
                    }

                    if (existChatId != 0)
                    {
                        var chatContext = db.Chat.ToList().Where(c => c.Id == existChatId);
                        var thisChat = chatContext.ToList().FirstOrDefault();
                        chat = thisChat;

                        ChatHistory chatHistory = new ChatHistory
                        {
                            ChatId = existChatId,
                            UserId = myUser.Id,
                            Message = message,
                            Date = DateTime.Now
                        };
                        chat.ChatHistory.Add(chatHistory);
                        chat.ChatPeople.Add(thisChat.ChatPeople.FirstOrDefault());//sirve para que chatpeople no pierda valor
                        db.ChatHistory.Add(chatHistory);
                        db.SaveChanges();
                    }
                    else
                    {
                        chat = new Chat()
                        {
                            AdminId = myUserId
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

                        ChatHistory chatHistory = new ChatHistory
                        {
                            ChatId = chat.Id,
                            UserId = myUser.Id,
                            Message = message,
                            Date = DateTime.Now
                        };

                        db.ChatHistory.Add(chatHistory);
                        db.SaveChanges();
                    }
                }
                return chat;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar en la base de datos, " + ex.Message);
            }
        }

        public decimal GetChatId(List<decimal> usersId)
        {
            using (ChatEntities db = new ChatEntities())
            {
                var groupCP = db.ChatPeople.GroupBy(c => c.ChatId).Where(c => c.Count() == usersId.Count()).ToList();
                decimal existChatId = 0;
                foreach (var gcp in groupCP)
                {
                    var glist = gcp.ToList().Select(x => x.UserId).ToList();
                    List<int> usersIdInt = usersId.Select(s => Convert.ToInt32(s)).ToList();
                    List<int> glistInt = glist.Select(s => Convert.ToInt32(s)).ToList();                 
                    if (!glistInt.Except(usersIdInt).Any())
                    {
                        existChatId = gcp.Key;
                        break;
                    }                   
                }
                  return existChatId;
            }
        }
        
        #region IChatHub

        public List<ChatMessage> GetMoreMsgHistory( List<decimal> usersId, int cant=0)
        {
            int getMsgNum = 5;

            getMsgNum *= cant + 1;
            List<ChatHistory> msg = new List<ChatHistory>();
            try
            {
                using (ChatEntities db = new ChatEntities())
                {
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
                            existChatId = gcp.Key;
                            break;
                        }
                    }
                    if (existChatId != 0)
                        msg = db.ChatHistory.Where(c => c.ChatId == existChatId).OrderByDescending(d => d.Date).Take(getMsgNum).ToList();
                }
                msg.Reverse();
                List<ChatMessage> cMsgList = new List<ChatMessage>();
                for (var i = 0; i < msg.Count(); i++)
                {
                    ChatMessage cMsg = new ChatMessage
                    {
                        UserFromId = msg[i].UserId,
                        DateTime = msg[i].Date,
                        Message = msg[i].Message
                    };
                    cMsgList.Add(cMsg);
                }
                return cMsgList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar obtener mensajes antiguos de la base de datos, " + ex.Message);
            }               
        }
        /// <summary>
        /// Returns the message history
        /// </summary>
        public List<ChatMessage> GetMessageHistory( List<decimal> usersId)//, int cant)
        {
            return GetMoreMsgHistory(usersId);
        }

        //public List<ChatMessage> GetMsgHistoryGroup(List<int> usersIds)
        //{
        //    var myUserId = this.GetMyUserId();
        //    // this is STUB. Normally you would go to the real database to get the messages
        //    //var messages = chatMessages
        //    //    .Where(
        //    //                       m =>
        //    //                       (m.UserToId == myUserId && m.UserFromId == otherUserId) ||
        //    //                       (m.UserToId == otherUserId && m.UserFromId == myUserId))
        //    //                   .OrderByDescending(m => m.Timestamp).Take(30).ToList();
        //    //.Where(
        //    //    m =>
        //    //                 (m.UserToId == otherUserId || m.UserToId == myUserId )&&
        //    //    (m.UserToId == myUserId && m.UserToId == otherUserId ))

        //    //    //(m.UserId == otherUserId) || //m.UserToId == myUserId && colocar
        //    //    //(m.UserId == myUserId)) //m.UserToId == otherUserId && colocar
        //    //.OrderByDescending(m => m.Timestamp).Take(30).ToList(); // ordenado por .Timestamp Date
        //    List<ChatMessage> messages = new List<ChatMessage>();
        //    messages.Reverse();
        //    return messages;
        //}
     
        public List<ChatUser> GetUsersList()
        {
            var myUserId = this.GetMyUserId();
            var myRoomId = this.GetMyRoomId();
            
            var users= this.GetUsersChat();
            var roomUsers = users.Where(u => u.RoomId == myRoomId && u.Id != myUserId).OrderBy(u => u.Name).ToList();
                  
            return roomUsers;
            
        }

        /// <summary>
        /// Sends a message to a particular user
        /// </summary>
        public void SendMessage(int otherUserId, string message, string clientGuid)
        {
            var myUserId = this.GetMyUserId();
            var myRoomId = this.GetMyRoomId();

            var dbChatMessage = this.PersistMessage(otherUserId, message, clientGuid);
            var connectionIds = new List<string>();
            lock (connections)
            {
                if (connections[myRoomId].ContainsKey(otherUserId))
                    connectionIds.AddRange(connections[myRoomId][otherUserId]);
                if (connections[myRoomId].ContainsKey(myUserId))
                    connectionIds.AddRange(connections[myRoomId][myUserId]);
            }
            foreach (var connectionId in connectionIds)
                this.Clients.Client(connectionId).sendMessage(dbChatMessage);
        }

        public void SendMsgToUsers(List<decimal> usersId, string message, string clientGuid)
        {
            var myUserId = this.GetMyUserId();
            var myRoomId = this.GetMyRoomId();

            var dbChatMessage = this.PersistMsgUsers(usersId, message, clientGuid);
            var connectionIds = new List<string>();
            lock (connections)
            {
                var conUsers = connections.Values.FirstOrDefault().Keys;
               // -		connections.Values.FirstOrDefault().FirstOrDefault().Value	
                foreach (var user in conUsers)
                {
                    if (connections[myRoomId].ContainsKey(user))
                        connectionIds.AddRange(connections[myRoomId][user]);
                }
               
                if (connections[myRoomId].ContainsKey(myUserId))
                    connectionIds.AddRange(connections[myRoomId][myUserId]);            
            }
            //var chatToSend= new object;
            //chatToSend{}
            foreach (var connectionId in connectionIds)
                this.Clients.Client(connectionId).sendMsgToUsers(dbChatMessage);
        }

        /// <summary>
        /// Sends a typing signal to a particular user
        /// </summary>
        public void SendTypingSignal(int otherUserId)
        {
            var myUserId = this.GetMyUserId();
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

        /// <summary>
        /// Triggered when the user opens a new browser window
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            var myRoomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId();
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

        /// <summary>
        /// Triggered when the user closes the browser window
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            var myRoomId = this.GetMyRoomId();
            var myUserId = this.GetMyUserId();
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
                              // Task.Run(() => //(No anda en .NET 4.5)
                                    {
                                        // this will run in separate thread.
                                        // If the user is away for more than 10 seconds it will be removed from 
                                        // the room.
                                        // In a normal situation this wouldn't be done because normally the users in a
                                        // chat room are fixed, like when you have 1 chat room for each tenancy
                                        //cambie 10 segundos por 1 segundos.                                   
                                        Thread.Sleep(1000);

                                        if (!connections[myRoomId].ContainsKey(myUserId))
                                        {
                                            var myDbUser = chatUsers.FirstOrDefault(u => u.Id == myUserId);
                                            if (myDbUser != null)
                                            {
                                                //chatUsers.Remove(myDbUser); aca eliminaba el usuario
                                                this.NotifyUsersChanged();
                                            }
                                        }
                                    });
                            }
                        }
            }

            return base.OnDisconnected();
        }
        #endregion
    }
}