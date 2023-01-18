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
//using ChatJsMvcSample.Code.LongPolling;
//using ChatJsMvcSample.Code.LongPolling.Chat;
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
//using System.Data.Objects;


namespace ChatJsMvcSample.Code.SignalR
{
    [HubName("inCH")]
    [EnableCors("*", "*", "*")]
    public class InternalChatHub : ChatHub
    {
        
    }
}