using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;

public partial class Views_UC_Viewers_DocToolbar : System.Web.UI.UserControl
{
    public string ShowPrint { get { return hdnImprimir.Value; } set { hdnImprimir.Value = value; } }
    public string DocId { get { return hdnDocId.Value; } set { hdnDocId.Value = value; } }
    public string DocTypeId { get { return hdnDocTypeId.Value; } set { hdnDocTypeId.Value = value; } }
    public string FilePath { get { return hdnFilePath.Value; } set { hdnFilePath.Value = value; } }
    public string StepId { get { return hdnWfstepid.Value; } set { hdnWfstepid.Value = value; } }
    public string DocExt { get { return hdnDocExt.Value; } set { hdnDocExt.Value = value; } }
    public string DocContainerClientId { get { return hdnDocContainer.Value; } set { hdnDocContainer.Value = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Visualizacion de solapas
        hdnShowForumTab.Value = UserBusiness.Rights.GetUserRights(ObjectTypes.ForoWeb, RightsType.View).ToString();
        hdnShowHistoryTab.Value = UserBusiness.Rights.GetUserRights(ObjectTypes.FrmDocHistory, RightsType.View).ToString();
        hdnShowAsociatedTab.Value = UserBusiness.Rights.GetUserRights(ObjectTypes.AsocWeb, RightsType.View).ToString();
        hdnShowMailsTab.Value = UserBusiness.Rights.GetUserRights(ObjectTypes.MailsHistoryWeb, RightsType.View).ToString();

        //Visualizacion de opciones de documento
        SRights sRights = new SRights();
        btnEmail.Visible = (DocTypeId.Length > 0 &&
            sRights.GetUserRights(ObjectTypes.DocTypes, RightsType.RemoveSendMailInTasks, Int64.Parse(DocTypeId)) == false &&
            sRights.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMailWeb, -1) == true);
        btnAttach.Visible = bool.Parse((UserPreferences.getValue("ShowAddFolderButton", Sections.UserPreferences, "false")));
        btnPrint.Visible = sRights.GetUserRights(ObjectTypes.Documents, RightsType.Print, -1);
        sRights = null;
    }
}