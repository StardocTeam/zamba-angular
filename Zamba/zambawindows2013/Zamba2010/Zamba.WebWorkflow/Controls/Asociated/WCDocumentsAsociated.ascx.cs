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
using System.Collections.Generic;

public partial class Controls_Asociated_WCDocumentsAsociated : System.Web.UI.UserControl
{
    private const String FULL_PATH_COLUMN_NAME = "fullpath";
    private const String DOC_ID_COLUMN_NAME = "docId";


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void LoadAssociatedResults(Int64 docId, Int64 docTypeId)
    {
        DocType DocType = DocTypesBusiness.GetDocType(docTypeId);
        IResult Result = Results_Business.GetNewResult(docId, DocType);
        IUser Cuser = UserBusiness.CurrentUser();
        ArrayList AsociatedResults = Zamba.Core.DocAsociatedBussines.getAsociatedResultsFromResult(Result);

        if (null != AsociatedResults && AsociatedResults.Count > 0)
        {

            List<IResult> results = new List<IResult>(AsociatedResults.Count);

            foreach (object item in AsociatedResults)
            {
                if (item is IResult)
                    results.Add((IResult)item);
            }

            DataTable dt = MakeDataTable(results);
            ucResults.LoadGridview(dt, "Nombre Documento");
        }
    }


    private DataTable MakeDataTable(System.Collections.Generic.List<Zamba.Core.IResult> Results)
    {
        //CopyPaste , estamos en tiempo de descuento con la entrega

        DataTable DT = new DataTable();

        foreach (Zamba.Core.IResult R in Results)
        {
            DataColumn DC = null;

            if (!DT.Columns.Contains("Nombre Documento"))
                DC = DT.Columns.Add("Nombre Documento");
            else
                DC = DT.Columns["Nombre Documento"];

            if (!DT.Columns.Contains("Tipo"))
                DC = DT.Columns.Add("Tipo");
            else
                DC = DT.Columns["Tipo"];

            if (!DT.Columns.Contains(FULL_PATH_COLUMN_NAME))
                DC = DT.Columns.Add(FULL_PATH_COLUMN_NAME);
            else
                DC = DT.Columns[FULL_PATH_COLUMN_NAME];

            if (!DT.Columns.Contains(DOC_ID_COLUMN_NAME))
                DC = DT.Columns.Add(DOC_ID_COLUMN_NAME);
            else
                DC = DT.Columns[DOC_ID_COLUMN_NAME];


            foreach (Zamba.Core.IIndex I in R.Indexs)
            {
                DC = null;
                if (!DT.Columns.Contains(I.Name))
                    DC = DT.Columns.Add(I.Name);
                else
                    DC = DT.Columns[I.Name];
            }
        }

        DT.AcceptChanges();


        foreach (Zamba.Core.IResult R in Results)
        {
            DataRow DR = DT.NewRow();

            DR[FULL_PATH_COLUMN_NAME] = R.FullPath;
            DR["Nombre Documento"] = R.Name;
            DR["Tipo"] = R.DocType.Name;
            DR[DOC_ID_COLUMN_NAME] = R.ID;

            foreach (Zamba.Core.IIndex I in R.Indexs)
            {
                DR[I.Name] = I.Data;
            }
            DT.Rows.Add(DR);
        }
        return DT;
    }

    protected void ucResult_OnReloadValues()
    {
    }

    protected void ucResult_OnSelectResult(Int64 docId)
    {

    }
}
