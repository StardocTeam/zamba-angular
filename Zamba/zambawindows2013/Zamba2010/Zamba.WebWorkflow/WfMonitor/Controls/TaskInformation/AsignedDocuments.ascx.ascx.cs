using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;

public partial class AsignedDocuments 
    : UserControl
{

    #region Propiedades
    /// <summary>
    /// Gets or Sets the Task Id
    /// </summary>
    public Int64? TaskId
    {
        get
        {
            Int64? NullableValue;
            Int64 Value;

            if (Int64.TryParse(hfTaskId.Value, out Value))
                NullableValue  = null;
            else
                NullableValue = Value;

            return NullableValue;
        }
        set
        {
            if (value.HasValue)
            {
                LoadAsignedDocuments(value.Value);
                hfTaskId.Value = value.Value.ToString() ;
            }
            else
            {
                Clear();
                hfTaskId.Value = String.Empty;
            }
        }
    }
    #endregion

    #region Eventos
    protected void btRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            if (TaskId.HasValue)
                LoadAsignedDocuments(TaskId.Value);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    private void LoadAsignedDocuments(Int64 taskId)
    {

    }

    /// <summary>
    /// Clears the inner Controls
    /// </summary>
    public void Clear()
    { 
    
    }

    public AsignedDocuments()
    {
        hfTaskId = new HiddenField();
        gvAsociatedDocuments = new GridView();
        btRefresh = new Button();
    }
}
