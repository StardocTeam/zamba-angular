using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba;
using Zamba.Core;
using Zamba.Services;

public partial class Views_UC_Viewers_DocToolbar : System.Web.UI.UserControl
{

    RightsBusiness RiB = new RightsBusiness();

    public string ShowPrint { get { return hdnImprimir.Value; } set { hdnImprimir.Value = value; } }
    public string DocId { get { return hdnDocId.Value; } set { hdnDocId.Value = value; } }
    public string DocTypeId { get { return hdnDocTypeId.Value; } set { hdnDocTypeId.Value = value; } }
    public string FilePath { get { return hdnFilePath.Value; } set { hdnFilePath.Value = value; } }
    public string StepId { get { return hdnWfstepid.Value; } set { hdnWfstepid.Value = value; } }
    public string DocExt { get { return hdnDocExt.Value; } set { hdnDocExt.Value = value; } }
    public string DocContainerClientId { get { return hdnDocContainer.Value; } set { hdnDocContainer.Value = value; } }
    public bool IsImportant { get; set; }
    public bool IsFavorite { get; set; }
    public long CurrentUserID { get; set; }
    public IResult Result { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Visualizacion de solapas
        CurrentUserID = Zamba.Membership.MembershipHelper.CurrentUser.ID;
        hdnShowForumTab.Value = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.ForoWeb, RightsType.View).ToString();
        hdnShowHistoryTab.Value = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.FrmDocHistory, RightsType.View).ToString();
        hdnShowAsociatedTab.Value = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.AsocWeb, RightsType.View).ToString();
        hdnShowMailsTab.Value = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.MailsHistoryWeb, RightsType.View).ToString();
        var thisUrl = Zamba.Membership.MembershipHelper.AppUrl;
        const string _URLFORMAT = "{0}{1}/Services/GetDocFile.ashx?DocTypeId={2}&DocId={3}&UserID={4}";
        var url = string.Format(Zamba.Web.Helpers.Tools.GetProtocol(Request) + _URLFORMAT, Request.ServerVariables["HTTP_HOST"], Request.ApplicationPath, DocTypeId, DocId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
        hdnFilePath.Value = url;
        //Visualizacion de opciones de documento
        SRights sRights = new SRights();
        //btnOpenNewTab.Visible = true;
        btnEmail.Visible = true;
        if (!IsFavorite)
            btnFav.InnerHtml = btnFav.InnerHtml.Replace("glyphicon-heart", "glyphicon-heart-empty ");
        if (!IsImportant)
            btnImportant.InnerHtml = btnImportant.InnerHtml.Replace("glyphicon-star", "glyphicon-star-empty ");

        btnFav.Attributes.Add("isset", IsFavorite.ToString());
        btnFav.Attributes.Add("title", IsFavorite ? "Desmarcar favorito" : "Marcar como favorito");
        btnImportant.Attributes.Add("isset", IsImportant.ToString());
        btnImportant.Attributes.Add("title", IsImportant ? "Desmarcar importante" : "Marcar como importante");
        //btnEmail.Visible = (DocTypeId.Length > 0 &&
        //    RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.DocTypes, RightsType.RemoveSendMailInTasks, Int64.Parse(DocTypeId)) != false &&
        //    RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Documents, RightsType.EnviarPorMailWeb, -1) == true);
        // btnPrint.Visible = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Documents, RightsType.Print, -1);
        Results_Business RB = new Results_Business();
        if (DocTypeId != "")
        {
            string ResultName = RB.GetName(Convert.ToInt64(DocId), Convert.ToInt64(DocTypeId));
            lbltitulodocumento.InnerHtml = ResultName;
        }
        
    }


    //public void DownLoad_File(object sender, EventArgs e)
    //{

    //    long DocTypeId = Convert.ToInt32(hdnDocTypeId.Value);
    //    long DocId = Convert.ToInt32(hdnDocId.Value);
    //    long Userid = Zamba.Membership.MembershipHelper.CurrentUser.ID;
    //    var url = "../../Services/GetDocFile.ashx?DocTypeId=" + DocTypeId + "&DocId=" + DocId + "&UserID=" + Userid + "&ConvertToPDf=false";
    //    Response.Redirect(url);
    //}
}