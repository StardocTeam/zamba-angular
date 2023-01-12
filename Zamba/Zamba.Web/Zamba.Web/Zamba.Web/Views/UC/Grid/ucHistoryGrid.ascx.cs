using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Zamba.Services;
using Zamba.Core;
using System.Text;
using System.Web.Configuration;
using Zamba.Web.Helpers;
using Zamba.Web.App_Code.Helpers;
using System.Globalization;
using System.Threading;

public partial class Views_UC_Grid_ucHistoryGrid : System.Web.UI.UserControl
{


    UserPreferences UP = new UserPreferences();

    enum GridType
    {
        Task = 0,
        Mails = 1
    };

    public string currBind
    {
        get
        {
            return hdnAction.Value;
        }
        set
        {
            hdnAction.Value = value;
        }
    }

    public Int64 currTarget
    {
        get
        {
            return Int64.Parse(hdnTarget.Value);
        }
        set
        {
            hdnTarget.Value = value.ToString();
        }
    }

    public void LoadTaskHistory(Int64 TaskId)
    {
        CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
        CultureInfo oldUICulture = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");
        try
        {
            ZOptBusiness zopt = new ZOptBusiness();
            string str = zopt.GetValue("alignTableHistory");
            if (string.IsNullOrEmpty(str))
            {
                str = string.Empty;
            }
            switch (str.ToLower())
            {
                case "right":
                    grdHistory.HorizontalAlign = HorizontalAlign.Right;
                    break;
                case "left":
                    grdHistory.HorizontalAlign = HorizontalAlign.Left;
                    break;
                default:
                    grdHistory.HorizontalAlign = HorizontalAlign.Center;
                    break;
            }


            DataTable dt = getTaskHistory(TaskId);
            divIframe.Visible = false;

            if (dt.Rows.Count > 0)
            {
                string historyPageSize = UP.getValue("HistoryPageSize", Zamba.UPSections.UserPreferences, 20);
                grdHistory.PageSize = string.IsNullOrEmpty(historyPageSize) ? 10 : Int16.Parse(historyPageSize);
                FormatGridview();
                generateGridColumns(dt, GridType.Task);
                BindGrid(dt);
                lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Text = "No hay historial para la tarea";
                lblMessage.Visible = true;
                grdHistory.Visible = false;
            }

            currTarget = TaskId;
            currBind = "LoadTaskHistory";
            zopt = null;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Ha ocurrido un error al cargar el historial de la tarea";
            lblMessage.Visible = true;
            grdHistory.Visible = false;
            Zamba.AppBlock.ZException.Log(ex);
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = oldCulture;
            Thread.CurrentThread.CurrentUICulture = oldUICulture;
        }
    }

    public void LoadOnlyIndexsHistory(Int64 docId)
    {
        formBrowser.Visible = false;

        try
        {
            DataTable dt = getOnlyIndexsHistory(docId);

            divIframe.Visible = false;

            if (dt.Rows.Count > 0)
            {
                string historyPageSize = UP.getValue("HistoryPageSize", Zamba.UPSections.UserPreferences, 20);
                grdHistory.PageSize = string.IsNullOrEmpty(historyPageSize) ? 10 : Int16.Parse(historyPageSize);
                FormatGridview();
                generateGridColumns(dt, GridType.Task);
                BindGrid(dt);
                lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Text = "No hay historial de atributos";
                lblMessage.Visible = true;
                grdHistory.Visible = false;
            }

            currTarget = docId;
            currBind = "LoadOnlyIndexsHistory";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Ha ocurrido un error al cargar el historial";
            lblMessage.Visible = true;
            grdHistory.Visible = false;
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    public void LoadMailsHistory(Int64 taskId)
    {
        STasks Tasks;

        try
        {
            Tasks = new STasks();
            Int64 docId = Tasks.GetDocId(taskId);

            LoadMailsHistoryByDocId(docId);

            currTarget = taskId;
            currBind = "LoadMailsHistory";
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
        finally
        {
            Tasks = null;
        }
    }

    public void LoadMailsHistoryByDocId(Int64 docId)
    {
        try
        {
            DataTable dt = EmailBusiness.getHistory(docId).Tables[0];

            if (dt.Rows.Count > 0)
            {
                
                FormatGridview();
                generateGridColumns(dt, GridType.Mails);
                dt.Columns.Add("ICONID");
                dt.Columns["ICONID"].SetOrdinal(0);
                grdHistory.DataSource = dt;
                grdHistory.DataBind();
                foreach(DataControlField dataControlField in grdHistory.Columns)
                {
                    if (dataControlField.HeaderText == "ID")
                        dataControlField.Visible = false;
                }
                
                lblMessage.Visible = false;
                for(int i = 0;i<dt.Rows.Count;i++)
                {
                    if (grdHistory.Rows.Count > i) {
                        grdHistory.Rows[i].Attributes.Add("onclick", "javascript: showMailSwal(\"" + dt.Rows[i]["ID"].ToString() + "\");");
                        grdHistory.Rows[i].Style.Add("cursor", "pointer");
                    }
                }
                   
                
                

            }
            else
            {
                lblMessage.Text = "No se han encontrado mails";
                lblMessage.Visible = true;
                grdHistory.Visible = false;                
            }

            currTarget = docId;
            currBind = "LoadMailsHistoryByDocId";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Ha ocurrido un error al cargar el historial de mails";
            lblMessage.Visible = true;
            grdHistory.Visible = false;
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private DataTable getTaskHistory(Int64 task_id)
    {
        DataSet ds = new DataSet();

        STasks Tasks = new STasks();
        ds = Tasks.GetTaskHistory(task_id);

        return ds.Tables[0];
    }

    private DataTable getOnlyIndexsHistory(Int64 docId)
    {
        DataSet ds = new DataSet();

        STasks Tasks = new STasks();
        ds = Tasks.GetOnlyIndexsHistory(docId);

        return ds.Tables[0];
    }

    private void BindGrid(DataTable dt)
    {
        try
        {
            grdHistory.DataSource = dt;
            grdHistory.DataBind();

            formatValues(dt);
            grdHistory.Visible = true;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void formatValues(DataTable _dt)
    {
        string headerText;

        if (_dt.Rows.Count > 0)
        {
            for (int col = 0; col < grdHistory.Columns.Count; col++)
            {
                string colname = grdHistory.Columns[col].HeaderText;

                if (colname.ToLower() == "Original")
                    grdHistory.Columns[col].Visible = false;

                if (GridHelper.GetVisibility(colname.ToLower(), GridHelper.GridType.Task) == false)
                {
                    grdHistory.Columns[col].Visible = false;
                }
                //else
                //{
                //    if (_dt.Columns.Contains(colname))
                //    {
                //        if (_dt.Columns[colname].DataType == Type.GetType("System.DateTime"))
                //        {
                //            for (int row = 0; row < grdHistory.Rows.Count; row++)
                //            {
                //                if (string.IsNullOrEmpty(grdHistory.Rows[row].Cells[col].Text))
                //                    continue;

                //                string value = grdHistory.Rows[row].Cells[col].Text;

                //                if (DateTime.TryParse(value, out date))
                //                    grdHistory.Rows[row].Cells[col].Text = date.ToShortDateString();
                //            }
                //        }
                //    }
                //}
            }

            foreach (DataControlField col in grdHistory.Columns)
            {
                headerText = col.HeaderText;

                if (headerText.StartsWith("I") && GridHelper.IsNumeric(headerText.Substring(1, headerText.Length - 1)))
                    col.Visible = false;
            }
        }
    }

    private void FormatGridview()
    {
        try
        {
            grdHistory.AutoGenerateColumns = false;
            grdHistory.AllowSorting = false;
            grdHistory.ShowFooter = false;
            grdHistory.Columns.Clear();
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void generateGridColumns(DataTable dt, GridType tipo)
    {
        try
        {
            if (dt != null && dt.Columns.Count > 0)
            {
                if (tipo == GridType.Mails)
                {
                    //Se agrega la columna Ver que ejecutará el evento

                    #region HACER LUEGO // TO DO

                    //TO DO: Analizar (para Boston Desktop): 

                    //Si se implementara elboton "VER"
                    //Si hara falta crear un archivo para el Historial de mensajes enviados.
                    //Como crea el archivo de historial si es que lo hace donde lo guarda.

                    //CommandField cf = new CommandField { SelectText = "Ver", ShowSelectButton = true };
                    //grdHistory.Columns.Add(cf);
                    //grdHistory.Columns[0].Visible = false;
                    #endregion

                    //Evento que abre el mail
                    //grdHistory.SelectedIndexChanged -= new EventHandler(grdHistory_SelectedEventChange);
                    //grdHistory.SelectedIndexChanged += new EventHandler(grdHistory_SelectedEventChange);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    BoundField f = new BoundField
                    {
                        DataField = c.Caption,
                        ShowHeader = true,
                        HeaderText = c.Caption,
                        SortExpression = c.Caption + " ASC"
                    };

                    grdHistory.Columns.Add(f);
                }

            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Evento disparado al presionar el link Select de una fila.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdHistory_SelectedEventChange(object sender, EventArgs e)
    {
        formBrowser.Attributes["style"] = "display:block";
        lblAttachError.Visible = false;

        try
        {
            string url = string.Format(Tools.GetProtocol(Request) + Request.ServerVariables["HTTP_HOST"] +
                Request.ApplicationPath + "/Services/GetMessageFile.ashx?id={0}&user={1}",
                grdHistory.SelectedRow.Cells[7].Text,
                Zamba.Membership.MembershipHelper.CurrentUser.ID);

            formBrowser.Attributes["src"] = url;
        }
        catch (Exception ex)
        {
            formBrowser.Attributes["style"] = "display:none";
            lblAttachError.Visible = true;
            ZClass.raiseerror(ex);
        }
    }

    private void ExecuteBindAction(string ActionName, Int64 Target)
    {
        //formBrowser.Visibility = "hidden";

        switch (ActionName)
        {
            case "LoadTaskHistory":
                LoadTaskHistory(Target);
                break;
            case "LoadOnlyIndexsHistory":
                LoadOnlyIndexsHistory(Target);
                break;
            case "LoadMailsHistory":
                LoadMailsHistory(Target);
                break;
            case "LoadMailsHistoryByDocId":
                LoadMailsHistoryByDocId(Target);
                break;
        }
    }

    protected void pageChangeEvent(object sender, GridViewPageEventArgs args)
    {
        if (!string.IsNullOrEmpty(currBind))
        {
            ExecuteBindAction(currBind, currTarget);
            grdHistory.PageIndex = args.NewPageIndex;
            grdHistory.DataBind();
        }
    }
}
