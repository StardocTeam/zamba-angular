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
using Zamba.Core;

public partial class DocHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {//recibo el docid y luego con sulto el historial del documento para
        //mostrarlo en el gridview [sebastian 08/01/2009]

        try
        {
            string DocId = Request.QueryString["docid"];
            DataSet dsDocHistory = UserBusiness.Actions.GetDocumentActions(Int32.Parse(DocId));
            Int64 CurrentDocTypeId = DocTypesBusiness.GetDocTypeIdByDocId(Int32.Parse(DocId));
            Result CurrentResult = Results_Business.GetResult(Int32.Parse(DocId), CurrentDocTypeId);
            lblDocHistory.Text = lblDocHistory.Text + " " + Results_Business.GetName(CurrentResult.ID, CurrentDocTypeId);
            gvDocHistory.DataSource = dsDocHistory;
            gvDocHistory.DataBind();
        }

        catch (Exception ex)
        {
//            lblDoc.Text = "Error";
            //writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }
}
