using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Zamba.Services;
using Zamba.Core;

public partial class Views_Aysa_DAL_PedidosSemaphoreImageHandler : System.Web.UI.Page
{
    enum ColorI
    {
        Red, Yellow, Green
    };

    UserPreferences UP = new UserPreferences();

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime datePedido;

        if (DateTime.TryParse(Request.QueryString["Date"],out datePedido))
        {
            ColorI colorToLoad = ColorI.Red;
            if (datePedido > DateTime.Now.AddDays(-1))
                colorToLoad = ColorI.Green;

            if (datePedido <= DateTime.Now.AddDays(-1) && datePedido > DateTime.Now.AddDays(-3))
                colorToLoad = ColorI.Yellow;

            byte[] byteArray;
            if (Cache[colorToLoad.ToString()] == null)
            {
                string path = Server.MapPath("~/Content/Images/icons/" + colorToLoad.ToString() + "I.png");
                byteArray = File.ReadAllBytes(path);
                Cache.Insert(colorToLoad.ToString(), byteArray, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            else
            {
                byteArray = (byte[])Cache[colorToLoad.ToString()];
            }

            Response.BinaryWrite(byteArray);
            Response.OutputStream.Write(byteArray, 0, byteArray.Length);
            Response.ContentType = "image/png";
        }

        if (!Page.IsPostBack && Zamba.Membership.MembershipHelper.CurrentUser != null)
        {
            try
            {
                IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                SRights rights = new SRights();
                Int32 type = 0;
                if (user.WFLic) type = 1;
                if (user.ConnectionId > 0)
                {
                    UserPreferences UserPreferences = new UserPreferences();
                    Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), type);
                    UserPreferences = null;
                }
                else
                    Response.Redirect("~/Views/Security/LogIn.aspx");
                rights = null;
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }
    }
}
