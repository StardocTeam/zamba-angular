using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Zamba.Core;

public partial class TaskInformation 
    : UserControl
{
   #region Constantes
   private const Char SEPARATOR = ';';
   #endregion

   #region Propiedades

   /// <summary>
   /// Sets or Gets the current Task Id
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

         ucHistory.TaskId = value.Value;
         ucIndexs.TaskId = value.Value;
         ucTask.TaskId = value.Value;
      }
      get
      {
         Int64? Value = null;
         Int64 TryValue;

         if (Int64.TryParse(hfTaskId.Value, out TryValue))
            Value = TryValue;

         return Value;
      }
   }
   #endregion

   #region Eventos
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
   #endregion

   #region Constructores
   public TaskInformation()
   {
      hfTaskId = new HiddenField();

      tbcTasksInformation = new TabContainer();

      tbpHistory = new TabPanel();
      tbpIndexs = new TabPanel();
      tbpTask = new TabPanel();

      ucHistory = new ASP.requestaction_controls_history_ascx();
      ucIndexs = new ASP.requestaction_controls_indexs_ascx();
      ucTask = new  ASP.requestaction_controls_task_ascx();
   }
   #endregion

   /// <summary>
   /// Changes the Status of the Control.
   /// </summary>
   /// <param name="enable"></param>
   private void ChangeStatus(Boolean enable)
   {
      tbcTasksInformation.Enabled = enable;
      tbcTasksInformation.Visible = enable;

      if (!enable)
         tbcTasksInformation.Tabs.Clear();
   }

   /// <summary>
   /// Clears every Task in the control
   /// </summary>
   public void Clear()
   {
      tbcTasksInformation.Tabs.Clear();
   }
}