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
using System.Net.Http;
using System.Net.Http.Headers;


namespace ChatJsMvcSample.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SuggestedPeopleController : Controller
    {
        public SuggestedPeopleController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Chat");
            }
            db = new ChatEntities(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }


        public static ChatEntities db;
        public static List<ChatUserVM> GetSuggested(int userId)
        {
            var lCU = new List<ChatUserVM>();
           
            try
            {
                if (WebConfigurationManager.AppSettings["isCS"] == "false") return lCU;
                var cP = GetConnectedPeople(userId);
                var companiesUser = cP.GroupBy(x => x.Company).ToList();
                var companies = GetCompaniesConfig();
                foreach (var c in companiesUser)
                {
                    var conf = companies.Where(x => x.Company == c.Key).FirstOrDefault();
                    if (conf != null)
                    {
                        db.ChangeDatabase(conf.InitialCatalog, conf.DataSource, conf.UserId, conf.Password);
                        SQLEF.RefreshAll(db);
                        var users = db.ChatUser.AsNoTracking().Take(10);
                        foreach (var u in users)
                        {
                            lCU.Add(new ChatUserVM()
                            {
                                Id = u.Id,
                                Avatar = u.Avatar,
                                Email = u.Email,
                                Company = u.Company,
                                Name = u.Name
                            });
                        }
                    }
                }
            }
            finally
            {
                db.ChangeDatabase(configConnectionStringName: "ChatContext");
            }
            return lCU;
        }
        private static List<ChatUser> GetConnectedPeople(int userId)
        {
            var lChatUser = new List<ChatUser>();
            try
            {
               
                var cId = db.ChatPeople.AsNoTracking().Where(x => x.UserId == userId).ToList().Select(x => x.ChatId).ToList();

                foreach (var id in cId)
                {
                    var usersId = db.ChatPeople.AsNoTracking().Where(x => x.ChatId == id && x.UserId != userId).ToList()
                        .Select(x => x.UserId).ToList();
                    foreach (var uId in usersId)
                    {
                        var user = db.ChatUser.AsNoTracking().Where(x => x.Id == uId).FirstOrDefault();
                        db.Entry<ChatUser>(user).Reload();
                        if (!lChatUser.Contains(user)) lChatUser.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lChatUser.OrderBy(x => x.Company).ToList();
        }
        private static IList<CompanyDBConn> GetCompaniesConfig()
        {
            var cns = new List<CompanyDBConn>();
            try
            {
                
                var dt = SQLEF.ExecuteSql(db, "select * from CompanyDBConnection");
                cns = dt.AsEnumerable().Select(row =>
                                 new CompanyDBConn
                                 {
                                     Company = row.Field<string>("Company"),
                                     DataSource = row.Field<string>("DataSource"),
                                     InitialCatalog = row.Field<string>("InitialCatalog"),
                                     UserId = row.Field<string>("UserId"),
                                     Password = row.Field<string>("Password")

                                 }).ToList();
                dt.Dispose();
            }
            catch (Exception ex) { }
            return cns;
        }
        public ActionResult Index(int userId)
        {
            var suggested = GetSuggested(userId);
            return View(suggested);
        }
        public void SendUserInvitation(ChatUserVMId cuVM)
        {
            try
            {
             
                    var invUser = db.ChatUser.Where(x => x.Email == cuVM.InvitedUser.Email).FirstOrDefault();
                    var ch = new ChatHub();
                    var password = Encript.Encrypt("123456");
                    //Crea usuario invitado
                    if (invUser == null)
                    {
                        invUser = new ChatUser
                        {
                            Name = cuVM.InvitedUser.Name,
                            Avatar = cuVM.InvitedUser.Avatar,
                            Company = cuVM.InvitedUser.Company,
                            Email = cuVM.InvitedUser.Email,
                            RoomId = ch.GetMyRoomId(),
                            Password = password,
                            RetryPassword = password,
                            //Role = ChatUser.RoleType.Blocked,
                            LastActiveOn = DateTime.Now,
                            LastMessage = DateTime.Now,
                        };
                        db.ChatUser.Add(invUser);
                        db.SaveChanges();
                    }
                    CreateNewChat(cuVM, invUser);
                
            }
            catch (Exception ex) { }
        }
        private void CreateNewChat(ChatUserVMId cuVM, ChatUser invUser)
        {
            if (!ExistChat(cuVM, invUser))
            {
                try
                {
                   
                        var chat = new Chat()
                        {
                            AdminId = cuVM.Id,
                            LastMessage = DateTime.Now,
                            ChatType = ChatType.Single,
                            ChatName = string.Empty,
                        };
                        db.Chat.Add(chat);
                        db.SaveChanges();
                        var cp = new List<ChatPeople>();
                        cp.Add(new ChatPeople
                        {
                            ChatId = chat.Id,
                            UserId = cuVM.Id,
                        });
                        cp.Add(new ChatPeople
                        {
                            ChatId = chat.Id,
                            UserId = invUser.Id,
                        });
                        cp.ForEach(c => db.ChatPeople.Add(c));
                        db.SaveChanges();

                        SendMailNotification(cuVM.Id, invUser.Id);
                   
                }
                catch (Exception ex) { }
            }
        }
        private bool ExistChat(ChatUserVMId cuVM, ChatUser invUser)
        {
            try
            {
               
                    var cpId = db.ChatPeople.AsNoTracking().Where(x => x.UserId == cuVM.Id).Select(x => x.ChatId).ToList();
                    foreach (var i in cpId)
                    {
                        var cpFilter = db.ChatPeople.AsNoTracking().Where(x => x.ChatId == i).ToList();
                        if (cpFilter.Count == 2 && cpFilter.Where(x => x.UserId == invUser.Id).ToList().Count == 1)                        
                            return true;                        
                    }
                    return false;
               
            }
            catch { return false; }
        }
        public void SendMailNotification(decimal userId, decimal invUserId)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ZColl"] + "/invitation/SendMailInvitation");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("?userId=" + userId + "&invUserId=" + invUserId).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;           
            }
        }
    }
}