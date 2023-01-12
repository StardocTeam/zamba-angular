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
using AjaxControlToolkit;
using System.Collections.Generic;
using Zamba.Services;
using ASP;
using Zamba.Core;

public partial class TaskInformation :
    UserControl
{
    #region Propiedades
    /// <summary>
    /// Sets the taskIds and loads them
    /// </summary>
    public List<Int64> TasksId
    {
        set
        {
            if (null == value || value.Count == 0)
                ChangeStatus(false);
            else
            {
                ChangeStatus(true);

                ITaskResult CurrentTask = null;
                TabPanel tbpCurrentTask = null;
                TabContainer tbcUcTasks = null;

                foreach (Int64 CurrentTaskId in value)
                {
                    CurrentTask = Tasks.GetTask(CurrentTaskId);

                    tbpCurrentTask = new TabPanel();
                    tbpCurrentTask.HeaderText = CurrentTask.Name;
                    tbcUcTasks = new TabContainer();

                    #region Asigned Documents
                    AsignedDocuments ucAsignedDocuments = new AsignedDocuments();
                    ucAsignedDocuments.TaskId = CurrentTaskId;

                    TabPanel tbpAsignedDocument = new TabPanel();
                    tbpAsignedDocument.HeaderText = "Documentos Asignados";
                    tbpAsignedDocument.Controls.Add(ucAsignedDocuments);
                    tbcUcTasks.Controls.Add(tbpAsignedDocument);
                    #endregion

                    #region History
                    History ucHistory = new History();
                    ucHistory.TaskId = CurrentTaskId;

                    TabPanel tbpHistory = new TabPanel();
                    tbpHistory.HeaderText = "Historial";
                    tbpHistory.Controls.Add(ucHistory);
                    tbcUcTasks.Controls.Add(tbpHistory);
                    #endregion

                    #region Indexs
                    Indexs ucIndexs = new Indexs();
                    ucIndexs.TaskId = CurrentTaskId;

                    TabPanel tbpIndexs = new TabPanel();
                    tbpIndexs.HeaderText = "Indices";
                    tbpIndexs.Controls.Add(ucIndexs);
                    tbcUcTasks.Controls.Add(tbpIndexs);
                    #endregion

                    #region Task Description
                    TaskInformation ucTask = new TaskInformation();
                    ucTask.TaskId = CurrentTaskId;

                    TabPanel tbpTaskDescription = new TabPanel();
                    tbpTaskDescription.HeaderText = "Informacion";
                    tbpTaskDescription.Controls.Add(ucTask);
                    tbcUcTasks.Controls.Add(tbpTaskDescription);
                    #endregion

                    tbpCurrentTask.Controls.Add(tbcUcTasks);

                    tbcTasksInformation.Tabs.Add(tbpCurrentTask);
                }

                if (null != CurrentTask)
                {
                    CurrentTask.Dispose();
                    CurrentTask = null;
                }

                if (null != tbpCurrentTask)
                {
                    tbpCurrentTask.Dispose();
                    tbpCurrentTask = null;
                }
                if (null != tbcUcTasks)
                {
                    tbcUcTasks.Dispose();
                    tbcUcTasks = null;
                }
            }
        }
    }

    /// <summary>
    /// Sets the current Task Id
    /// </summary>
    public Int64? TaskId
    {
        set
        {
            if (value.HasValue)
                hfTaskId.Value = value.Value.ToString();
            else
                hfTaskId.Value = string.Empty;

            ChangeStatus(value.HasValue);


            if (null == ucHistory)
                ucHistory = new wfmonitor_controls_taskinformation_history_ascx();
            ucHistory.TaskId = value.Value;

            if (null == ucIndexs)
                ucIndexs = new wfmonitor_controls_taskinformation_indexs_ascx();
            ucIndexs.TaskId = value.Value;

            if (null == ucTask)
                ucTask = new wfmonitor_controls_taskinformation_task_ascx();
            ucTask.TaskId = value.Value;
        }
        get
        {
            Int64? Value = null;
            Int64 TryValue ;

            if (Int64.TryParse(hfTaskId.Value, out TryValue))
                Value = TryValue;

            return Value;
        }
    }
    #endregion

    /// <summary>
    /// Changes the Status of the Control.
    /// </summary>
    /// <param name="enable"></param>
    private void ChangeStatus(Boolean enable)
    {
        tbcTasksInformation.Enabled = enable;

        if (!enable)
        {
            ucHistory.Clear();
            ucIndexs.Clear();
            ucTask.Clear();
        }
    }

    /// <summary>
    /// Clears every Task in the control
    /// </summary>
    public void Clear()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (TaskId.HasValue)
                    ChangeStatus(true);
                else
                    ChangeStatus(false);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

    #region MyRegion
    public TaskInformation()
    {
        ucHistory = (wfmonitor_controls_taskinformation_history_ascx)LoadControl("~/WfMonitor/Controls/TaskInformation/History.ascx");
        ucIndexs = (wfmonitor_controls_taskinformation_indexs_ascx)LoadControl("~/WfMonitor/Controls/TaskInformation/Indexs.ascx");
        ucTask = (wfmonitor_controls_taskinformation_task_ascx)LoadControl("~/WfMonitor/Controls/TaskInformation/Task.ascx");
        hfTaskId = new HiddenField();
        tbcTasksInformation = new TabContainer();
    } 
    #endregion
}