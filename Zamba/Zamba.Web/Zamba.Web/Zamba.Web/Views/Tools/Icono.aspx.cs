using System;
using System.Drawing;
using System.Drawing.Imaging;
using Zamba.Services;
using System.IO;
using System.Web;

namespace Zamba.Web
{
    public partial class Icono : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                //27/07/11: Se cambia la forma de cargar los íconos,
                //a partir de ahora se buscan las imágenes y se retornan a donde sean requeridos.
                string id = Request.QueryString[0];
                byte[] byteArray;
                var iconType = Request.QueryString["type"];

                switch (iconType)
                {
                    case null:
                    case "view":
                        if (Cache[id] == null)
                        {
                            string path = Server.MapPath("~/Content/Images/icons/" + id + ".png");
                            FileInfo fInfo = new FileInfo(path);
                            if (!fInfo.Exists)
                                path = Server.MapPath("~/Content/Images/icons/0.png");

                            byteArray = File.ReadAllBytes(path);
                            Cache.Insert(id, byteArray, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
                        }
                        else
                        {
                            byteArray = (byte[])Cache[id];
                        }
                        ReturnResponse(byteArray);
                        break;
                    case "taskstate":
                        if (Cache["taskstate" + id] == null)
                        {
                            string path = Server.MapPath(GetTaskStateIcon(id));
                            FileInfo fInfo = new FileInfo(path);
                            if (!fInfo.Exists)
                                path = Server.MapPath("~/Content/Images/empty.png");

                            byteArray = File.ReadAllBytes(path);
                            Cache.Insert("taskstate" + id, byteArray, null, DateTime.Now.AddDays(1), TimeSpan.Zero);

                        }
                        else
                        {
                            byteArray = (byte[])Cache["taskstate" + id];
                        }
                        ReturnResponse(byteArray);
                        break;
                }
            }
        }

        private void ReturnResponse(byte[] byteArray)
        {
            Response.BinaryWrite(byteArray);
            Response.OutputStream.Write(byteArray, 0, byteArray.Length);
            Response.ContentType = "image/png";
        }

        private string GetTaskStateIcon(string id)
        {
            var loc = "~/Content/Images/";
            switch (id)
            {
                case "2":
                    return loc + "userlock.png";
                case "1":
                    return loc + "userasigned.png";
                case "0":
                default:
                    return loc + "empty.png";
            }
        }
    }
}