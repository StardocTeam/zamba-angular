using ChatJsMvcSample.Models;
using System.IO;
using System.Reflection;
using ChatJsMvcSample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Zamba.Collaboration.Models;
using Zamba.Collaboration.Code;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Threading;
using System.Web.Security;
using System.Web.Configuration;
using System.Drawing;
using CaptchaMvc.HtmlHelpers;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Text;
using Zamba.Membership;

namespace Zamba.Collaboration.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : Controller
    {
        public AccountController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Collaboration");
            }
            db = new DBCollaboration(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private static DBCollaboration db ;
        public ActionResult Index()
        {
            //var name = db.ChatUser.Where(x => x.Name.Contains("jav")).FirstOrDefault();
            //ViewBag.Name = name.Name;
            return RedirectToAction("Index", "Home");
        }

        [EnableCors("*", "*", "*")]
        [HttpPost]
        public JsonResult Invite(ChatInvitation ci)
        {
            var thisUser = SaveThisUser(ci);
            ci.Id = thisUser.Id;
            var invUser = SaveInvitedUser(ci);
            var groupCP = db.ChatPeople.GroupBy(c => c.ChatId).Where(c => c.Count() == 2).ToList();
            var lu = new List<decimal>();
            lu.Add(thisUser.Id);
            lu.Add(invUser.Id);
            var nCHat = ExistChatByCP(groupCP, ChatType.Single, lu);
            if (nCHat == 0)
            {
                var chat = new Chat()
                {
                    AdminId = thisUser.Id,
                    LastMessage = DateTime.Now,
                    ChatType = ChatType.Single,
                    ChatName = string.Empty,
                };
                db.Chat.Add(chat);
                db.SaveChanges();
                var cp = new List<ChatPeople>();
                foreach (int user in lu)
                {
                    cp.Add(new ChatPeople
                    {
                        ChatId = chat.Id,
                        UserId = user
                    });
                }
                cp.ForEach(c => db.ChatPeople.Add(c));
                db.SaveChanges();
            }
            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
            {
                var json = SerializeObj.Serialize(GetCreateAccountVM(ci, invUser.Id));
                var encJson = Encript.Encrypt(json);
                Mail.SendInvitation(thisUser, invUser, ci.MailCopy, encJson);
            });

            return Json(ci);
        }

        private CreateAccountVM GetCreateAccountVM(ChatInvitation ci, decimal invId)
        {
            var caVM = new CreateAccountVM()
            {
                Id = ci.Id,
                Email = ci.Email,
                InvitedEmail = ci.InvitedEmail,
                InvitedId = invId
            };
            return caVM;
        }
        public ActionResult CreateAccountEnc(string enc)
        {
            var iVM = new InvitationVM();
            try
            {
                var encJson = Encript.Decrypt(enc.Trim().Replace(" ", "+"));
                var caMV = SerializeObj.Deserialize<CreateAccountVM>(encJson);
                iVM.InvitedUser = db.ChatUser.Where(c => c.Id == caMV.InvitedId).FirstOrDefault();
                iVM.ThisUser = db.ChatUser.Where(c => c.Id == caMV.Id).FirstOrDefault();

                var UserList = db.ChatUser.ToList();
                if (UserList.Where(x => x.Email == iVM.InvitedUser.Email && iVM.InvitedUser.Password != "xxxxxx").FirstOrDefault() != null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["iVM"] = iVM;//.InvitedUser;
                    return RedirectToAction("CreateAccount");
                }
            }
            catch
            {
                ViewBag.Error = "Error al crear cuenta, verifique que el código ingresado sea el correcto";
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpGet]
        public ActionResult CreateAccount(InvitationVM iVM)
        {
            var editCU = new EditChatUser();
            try
            {
                if (iVM.ThisUser == null && iVM.InvitedUser == null)
                    iVM = TempData["iVM"] as InvitationVM;
                if (iVM == null || iVM.ThisUser == null || iVM.InvitedUser == null)
                {
                    ViewBag.Error = "Error al crear cuenta, usuario no registrado / inconsistencia de datos";
                    return View("~/Views/Shared/Error.cshtml");
                }
                editCU = new EditChatUser((ChatUser)iVM.InvitedUser);
                editCU.InvSenderId = iVM.ThisUser.Id;
            }
            catch (Exception ex )
            {
                
            }
            return View(editCU);
        }
        [HttpPost]
        [ActionName("CreateAccount")]
        [ValidateAntiForgeryToken]
        [CaptchaMvc.Attributes.CaptchaVerify("Captcha is not valid")]
        public ActionResult CreateAccount_Post(EditChatUser eCU)
        {
            //var err = "Error al validar sus datos. Por favor compruebe que los datos ingresados sean correctos";
            try
            {               
                TempData["Message"] = "";
                if (ModelState.IsValid)
                {
                    if (eCU.Id > 0)
                    {
                        ModelState.Clear();
                        var user = db.ChatUser.Where(x => x.Id == eCU.Id).FirstOrDefault();               
                        var password = Encript.Encrypt(eCU.Password);
                        user.Name = eCU.Name;
                        user.Status = ChatUser.StatusType.Offline;
                        user.Role = ChatUser.RoleType.Active;
                        user.Password = password;
                        user.RetryPassword = password;
                        user.LastActiveOn = DateTime.Now;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["user"] = user;
                        //Si el usuario invitado se registra correctamente me envia un email informandome
                        string assemblyFile = Path.GetDirectoryName((
                        new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
                        ).AbsolutePath).ToString();
                        var confirmEmailHTML = System.IO.File.ReadAllText(assemblyFile + "\\Mail\\MailConfirm.html")
                       .Replace("[*UserName]", user.Name)
                       .Replace("[*UserMail]", user.Email)
                       .Replace("[*InvitedUserName]", user.Name);

                        var invSender = db.ChatUser.Where(x => x.Id == eCU.InvSenderId).FirstOrDefault();
                        if (invSender !=null)
                            Mail.Send(invSender.Email, invSender.Name, "Invitacion a Zamba Collaboration", confirmEmailHTML);   
                        return RedirectToAction("ValidateAccount");
                    }
                }
                if (ModelState["CaptchaInputText"] != null && (ModelState["CaptchaInputText"]).Errors.Count > 0)
                {
                    TempData["Message"] = "*Captcha incorrecto.";
                }
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
                return View(eCU);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(eCU);
        }

        private ChatUser SaveThisUser(ChatInvitation cu)
        {
            //Compruebo si existe el usuario que invita        
            var thisUser = db.ChatUser.Where(c => c.Email == cu.Email).FirstOrDefault();
            if (thisUser == null)
            {
                var password = Encript.RandomString(8);
                var passEnc = Encript.Encrypt(password);
                thisUser = new ChatUser()
                {
                    Avatar = cu.Avatar,
                    Company = cu.Company,
                    Email = cu.Email,
                    LastActiveOn = DateTime.Now,
                    LastMessage = DateTime.Now,
                    Name = cu.Name,
                    Role = ChatUser.RoleType.Active,
                    RoomId = ChatController.ROOM_ID_STUB,
                    Status = ChatUser.StatusType.Online,
                    Password = passEnc
                };
                db.ChatUser.Add(thisUser);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                Mail.CreatedAccountNotification(thisUser, password);
            }
            return thisUser;
        }
        private ChatUser SaveInvitedUser(ChatInvitation cu)
        {
            //Compruebo si existe el usuario invitado
            var invUser = db.ChatUser.Where(c => c.Email == cu.InvitedEmail).FirstOrDefault();
            if (invUser == null)
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "bin/ChatJs/images/defaultAvatar.png";
                if (!System.IO.File.Exists(path)) path = AppDomain.CurrentDomain.BaseDirectory + "Content/images/defaultAvatar.png";
                var img = ImageController.PathToBase64(path);

                invUser = new ChatUser()
                {
                    Company = cu.InvitedCompany,
                    Email = cu.InvitedEmail,
                    LastActiveOn = DateTime.Now,
                    LastMessage = DateTime.Now,
                    Name = cu.InvitedUserName,
                    Role = ChatUser.RoleType.Blocked,
                    RoomId = ChatController.ROOM_ID_STUB,
                    Status = ChatUser.StatusType.Offline,
                    Avatar = img,
                    Password = "xxxxxx",
                    RetryPassword = "xxxxxx"
                };
                db.ChatUser.Add(invUser);
                db.SaveChanges();
            }
            return invUser;
        }
        public ActionResult ValidateAccount()
        {
            var user = TempData["user"] as ChatUser;
            return View(user);
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

        public ActionResult Join()
        {
            try
            {
                var userId = string.Empty;
                if (MembershipHelper.CurrentUser  != null && Session["userId"].ToString() != "0")
                {
                    userId = Session["userId"].ToString();
                }
                else
                {
                    var user = db.ChatUser.Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
                    if (user == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        userId = user.Id.ToString();
                    }
                }
                var id = Encript.Encrypt(userId);
                var url = WebConfigurationManager.AppSettings["ZLinkURL"] + "?enc=" + id;

                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ChatUser user)
        {
            var thisUser = UserFromCredentials(user.Email, user.Password);
            if (thisUser != null)
            {
                Session["userId"] = user.Id;
                FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public static ChatUser UserFromCredentials(string _userMail, string _password)
        {
            try
            {
                var thisUser = db.ChatUser.AsNoTracking().Where(x => x.Email == _userMail).FirstOrDefault();
                if (thisUser != null)
                {
                    db.Entry(thisUser).State = System.Data.Entity.EntityState.Detached;
                    thisUser = db.ChatUser.AsNoTracking().Where(x => x.Email == _userMail).FirstOrDefault();
                    if (Encript.Decrypt(thisUser.Password) == _password)
                        return thisUser;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public ActionResult Logout()
        {
            Session["userId"] = string.Empty;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SaveAvatarFile(decimal userId)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var img = Image.FromStream(file.InputStream, true, true);
                        var img64 = ImageController.GetBase64FromImage(img);
                        var user = db.ChatUser.Where(x => x.Id == userId).FirstOrDefault();
                        user.Avatar = img64;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Dismiss(int invitedUser, int thisUser)
        {
            try
            {
                var InvUser = db.ChatUser.Where(x => x.Id == invitedUser).FirstOrDefault();
                var ThisUser = db.ChatUser.Where(x => x.Id == thisUser).FirstOrDefault();
                var vm = new InvitationVM();
                vm.InvitedUser = InvUser;
                vm.ThisUser = ThisUser;

                return View(vm);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Dismiss(InvitationVM iVM)
        {
            try
            {
                var user = db.ChatUser.Where(x => x.Id == iVM.InvitedUser.Id).FirstOrDefault();
                db.ChatUser.Remove(user);
                db.SaveChanges();
                Mail.Dismiss(iVM.ThisUser, iVM.InvitedUser);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}