using System;
using System.Web.UI;
using Zamba.Core;
using System.Web.Security;

public partial class ZambaMaster
        : MasterPage
{

    protected void ContentPlaceHolder1_Load(object sender, EventArgs e)
    {
                 //aca leer el web.config
            System.Configuration.Configuration RootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Zamba.WebWorkFlow");
            if (0 < RootWebConfig.AppSettings.Settings.Count)
            {
                System.Configuration.KeyValueConfigurationElement customSetting =
                    RootWebConfig.AppSettings.Settings["LinksSearhResults"];
                if (string.Compare(customSetting.Value.ToLower(), "true") == 0)
                {
                    btnLnkCloseSesion.Visible = true;
                    LinkResultados.Visible = true;
                }
                else
                {
                    btnLnkCloseSesion.Visible = false;
                    LinkResultados.Visible = false;
                }
            }
    }

    /// <summary>
    /// Evento que se ejecuta cuando se presiona el botón "Cerrar Sesión"
    /// </summary>
    /// <history>
    ///     [Gaston]  09/01/2009  Created   
    ///     [Gaston]  14/01/2009  Modified  Llamada al método "RemoveConnectionFromWeb" y redirección a la página de login
    /// </history>
    protected void btnLnkCloseSesion_Click(object sender, EventArgs e)
    {
        // Si el ConnectionId y el ComputerNameOrIP son distintos a null entonces se remueve la conexión que tiene el usuario en la tabla UCM
        if ((Session["ConnectionId"] != null) && (Session["ComputerNameOrIP"] != null))
        {
            Ucm.RemoveConnectionFromWeb(Convert.ToInt32(Session["ConnectionId"].ToString()), Session["ComputerNameOrIP"].ToString());
            // Se elimina todo lo que haya en Session
            Session.RemoveAll();
            // Se vuelve a la página de login
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}