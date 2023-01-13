using System;
using System.Drawing;
using System.Drawing.Imaging;
using Zamba.Services;
using System.IO;
using System.Web;

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
            
            Response.BinaryWrite(byteArray);
            Response.OutputStream.Write(byteArray, 0, byteArray.Length);
            Response.ContentType = "image/png";
        }
    }
}
