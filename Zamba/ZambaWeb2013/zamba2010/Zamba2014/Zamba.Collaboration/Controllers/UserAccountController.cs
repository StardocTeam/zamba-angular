using ChatJsMvcSample.Models;
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
using System.IO;
using System.Web.Security;
using System.Web.Configuration;
using System.Drawing;
using CaptchaMvc.HtmlHelpers;
using Zamba.Membership;

namespace Zamba.Collaboration.Controllers
{
    public class UserAccountController : Controller
    {
        public UserAccountController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Chat");
            }
            db = new DBCollaboration(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }
                
        private static DBCollaboration db ;
        // GET: UserAccount
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit()
        {
            ChatUser user;
            if (MembershipHelper.CurrentUser  != null && int.Parse(Session["userId"].ToString()) > 0)
            {
                user = db.ChatUser.Where(x => x.Id == decimal.Parse(Session["userId"].ToString())).FirstOrDefault();
            }
            else
            {
                user = db.ChatUser.Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            }
            if (user == null)
            {
                ViewBag.Error = "Error al acceder a sus datos para modificarlos, por favor vuelva a iniciar sesion";
                return View("~/Views/Shared/Error.cshtml");
            }
            var eCU = new EditChatUser();
            var js = JsonConvert.SerializeObject(user);
            var eUser = JsonConvert.DeserializeObject<EditChatUser>(js);
            return View(eUser);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Post(EditChatUser cu)
        {
            try
            {
                var user = db.ChatUser.Where(x => x.Id == cu.Id).FirstOrDefault();
                var password = string.Empty;
                ViewBag.PasswordError = string.Empty;
                if (Encript.Decrypt(user.Password) == cu.OldPassword && cu.Password.Length > 5 && cu.Password == cu.RetryPassword)
                    password = Encript.Encrypt(cu.Password);
                else
                {
                    if (!string.IsNullOrEmpty(cu.OldPassword) && !string.IsNullOrEmpty(cu.Password) && !string.IsNullOrEmpty(cu.OldPassword))
                    {
                        cu.Company = user.Company;
                        cu.Email = user.Email;
                        cu.Avatar = user.Avatar;
                        ViewBag.PasswordError = "Por favor verifique la actual y nueva contraseña";
                        return View(cu);
                    }
                    else
                        password = user.Password;
                }

                user.Name = cu.Name;
                user.Password = password;
                user.RetryPassword = password;
                user.LastActiveOn = DateTime.Now;
                user.Position = cu.Position;
                user.InternalPhone = cu.InternalPhone;
                user.Phone = cu.Phone;
                user.CellPhone = cu.CellPhone;

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
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
                return View(cu);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaMvc.Attributes.CaptchaVerify("Captcha is not valid")]
        public ActionResult ForgotPassword(FormCollection form)
        {
            try
            {
                TempData["Message"] = "";
                if (ModelState.IsValid)
                {
                    var mail = form["txtMail"];
                    var user = db.ChatUser.Where(x => x.Email == mail).FirstOrDefault();
                    if (user == null)
                    {
                        TempData["Message"] = "Usuario no registrado";
                        return View();
                    }
                    var password = Encript.RandomString(8);
                    var passEnc = Encript.Encrypt(password);

                    user.Password = passEnc;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    Mail.ForgotPassword(user, password);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["Message"] = "Codigo erroneo, vuelva a intentarlo";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error al procesar su solicitud";
                return View();
            }
        }

        [HttpGet]
        public ActionResult RecoverPassword()
        {
            return View();
        }

    }
}