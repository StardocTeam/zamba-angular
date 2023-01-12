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
    public class EncriptController : Controller
    {
        // GET: Encript
        public string EncriptStr(string str)
        {
            return Encript.Encrypt(str); 
        }
        public string DecriptStr(string str)
        {
            return Encript.Decrypt(str); 
        }
    }
}