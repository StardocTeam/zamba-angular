using System;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ChatJs.Net;// quitar
using Microsoft.AspNet.SignalR;
using ChatJsMvcSample.Models; //Colocar
using System.Web.Http.Cors;
using ChatJsMvcSample.Code.SignalR;
using ChatJsMvcSample.Controllers;

namespace ChatJsMvcSample.Code
{
    /// <summary>
    /// Stub methods for obtaining the db user from the cookie.
    /// In a normal situation this would be done using the forms authentication cookie
    /// </summary>
    /// 
    [EnableCors("*", "*", "*")]
    public class ChatHelper
    {
        public static string COOKIE_NAME = "CookieChat";

        /// <summary>
        /// Returns information about the user from cookie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ChatUser GetChatUserFromCookie(HttpRequestBase request)
        {
            if (request == null) throw new ArgumentNullException("request");
            var cookie = request.Cookies[COOKIE_NAME];
            if (cookie == null) return null;

            var cookieBytes = Convert.FromBase64String(cookie.Value);
            var cookieString = Encoding.UTF8.GetString(cookieBytes);
            return new JavaScriptSerializer().Deserialize<ChatUser>(cookieString);
        }

        public static ChatUser GetChatUserFromDB(HttpRequestBase request)
        {
            if (request == null) throw new ArgumentNullException("request");
            var cookie = request.Cookies[COOKIE_NAME];
            if (cookie == null) return null;

            var cookieBytes = Convert.FromBase64String(cookie.Value);
            var cookieString = Encoding.UTF8.GetString(cookieBytes);
            return new JavaScriptSerializer().Deserialize<ChatUser>(cookieString);
        }

        /// <summary>
        /// Returns information about the user from cookie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ChatUser GetChatUserFromCookie(IRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            var cookie = request.Cookies[COOKIE_NAME];

            if (cookie == null)
                return null;

            var cookieBytes = Convert.FromBase64String(cookie.Value);
            var cookieString = Encoding.UTF8.GetString(cookieBytes);
            return new JavaScriptSerializer().Deserialize<ChatUser>(cookieString);
        }

        /// <summary>
        /// Removes the cookie. Probably because it's invalid
        /// </summary>
        /// <param name="response"></param>
        public static void RemoveCookie(HttpResponseBase response)
        {
            if (response == null) throw new ArgumentNullException("response");
            var cookie = response.Cookies[COOKIE_NAME];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);
        }
        // if (request == null) throw new ArgumentNullException("request");
        //Cookie cookie;
        //    if (request.Cookies.Keys.Contains(COOKIE_NAME))
        //        cookie=request.Cookies[COOKIE_NAME];
        //    else
        //        return null;

        /// <summary>
        /// Creates a new cookie with information about the user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="chatUser"></param>
        public static void CreateNewUserCookie(HttpResponseBase response, ChatUser chatUser)
        {
            if (chatUser == null) throw new ArgumentNullException("chatUser");

            var cookie = new HttpCookie(COOKIE_NAME)
            {
                Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(chatUser))),
                Expires = DateTime.UtcNow.AddDays(30)
            };
            response.Cookies.Add(cookie);
        }
        //internal static ChatUser GetChatUser(HttpRequestBase request, HttpResponseBase response, decimal userId)
        //{
        //    if (request == null) throw new ArgumentNullException("request");
        //    ChatUser user;
        //        if (userId == 0)
        //        {
        //            user = new ChatUser()
        //            {
        //                Id = 0,
        //                Avatar = "",
        //                Name = "administrador",
        //                Status = ChatUser.StatusType.Online,
        //                LastActiveOn = DateTime.Now,
        //                RoomId = ChatController.ROOM_ID_STUB,
        //            };
        //        }
        //        else
        //        {
        //            user = ChatHub.GetUserInfo(userId);
        //        }
        //        if (user != null)
        //            ChatHelper.CreateNewUserCookie(response, user);            
        //    return user;
        //}
    }
}