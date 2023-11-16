using System;
using System.Web;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using System.Collections.Generic;
using System.Web.UI;

/// <summary>
/// Lists a collection of Tasks and allows the client to select any of them.
/// </summary>
public partial class FinishedTasksList : UserControl
{
   #region Constants
   /// <summary>
   /// Name of the Label with the TaskId in the GridView TemplateField
   /// </summary>
   private const String MESSAGE_NO_TASKS = "No hay tareas para finalizadas mostrar";

   /// <summary>
   /// Index of the "View" Button in the Gridview
   /// </summary>
   private const Int32 VIEW_BUTTON_INDEX = 0;
   /// <summary>
   /// Index of the Task id column
   /// </summary>
   private const Int32 TASK_ID_INDEX = 1;
   /// <summary>
   /// Index of the Workflow id Column
   /// </summary>
   private const Int32 WORKFLOW_ID_INDEX = 2;
   /// <summary>
   /// Index of the Task name column
   /// </summary>
   private const Int32 TASK_NAME_INDEX = 3;
   /// <summary>
   /// Index of the Task IsExpired column
   /// </summary>
   private const Int32 TASK_IS_EXPIRED_INDEX = 4;
    /// <summary>
   /// Index of the executed rule name column
    /// </summary>
   private const Int32 TASK_RULE_NAME = 5;
    /// <summary>
   /// Index of the user name column
    /// </summary>
   private const Int32 TASK_USER_NAME = 6;

   /// <summary>
   /// Index of the Task total task count column
   /// </summary>
   private const Int32 MAX_NAME_LENGHT = 20;
   /// <summary>
   /// Message Shown when a Task is Expired
   /// </summary>
   private const String EXPIRED_MESSAGE = "SI";
   /// <summary>
   /// Message Shown when a Task isn't Expired
   /// </summary>
   private const String NOT_EXPIRED_MESSAGE = "NO";

   #endregion

   #region Properties
   /// <summary>
   /// Get every Task in the control
   /// </summary>
   public List<RequestActionTask > Tasks
   {
      set
      {
         gvTasks.DataSource = null;

         gvTasks.DataSource = value;
         gvTasks.DataBind();

         Int32 BaseIndex = gvTasks.PageSize * gvTasks.PageIndex;
         Int32 CurrentIndex;
         ITaskResult CurrentTask = null;
         RequestActionTask RequestTask = null;
         GridViewRow CurrentRow = null;
         String ShortenedName = null;

         for (int i = 0; i < gvTasks.PageSize; i++)
         {
            CurrentIndex = BaseIndex + i;
            if (CurrentIndex <= value.Count && i < gvTasks.Rows.Count)
            {
                RequestTask = value[CurrentIndex];
                CurrentTask = WFTaskBussines.GetTaskById(RequestTask.TaskId);
                CurrentRow = gvTasks.Rows[i];

                CurrentRow.Cells[TASK_ID_INDEX].Text = CurrentTask.TaskId.ToString();
                CurrentRow.Cells[WORKFLOW_ID_INDEX].Text = CurrentTask.WorkId.ToString();
                CurrentRow.Cells[TASK_NAME_INDEX].ToolTip = CurrentTask.Name;

                if (CurrentTask.Name.Length > MAX_NAME_LENGHT)
                {
                    ShortenedName = CurrentTask.Name.Substring(0, MAX_NAME_LENGHT - 3);
                    ShortenedName = ShortenedName + "...";
                    CurrentRow.Cells[TASK_NAME_INDEX].Text = ShortenedName;
                }
                else
                    CurrentRow.Cells[TASK_NAME_INDEX].Text = CurrentTask.Name;

                if (CurrentTask.IsExpired)
                {
                    CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].Text = EXPIRED_MESSAGE;
                    CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].ToolTip = EXPIRED_MESSAGE;
                }
                else
                {
                    CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].Text = NOT_EXPIRED_MESSAGE;
                    CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].ToolTip = NOT_EXPIRED_MESSAGE;
                }

                String UserName = UserBusiness.GetUserNamebyId((Int32)RequestTask.UserID);
                CurrentRow.Cells[TASK_USER_NAME].Text = UserName;
                CurrentRow.Cells[TASK_USER_NAME].ToolTip = UserName;

                String RuleName = WFRulesBussines.GetRuleName(RequestTask.RuleId);
                CurrentRow.Cells[TASK_RULE_NAME].Text = RuleName;
                CurrentRow.Cells[TASK_RULE_NAME].ToolTip = RuleName;
            }
         }

         ValidateStatus();
      }
   }
   #endregion

   #region Eventos
   /// <summary>
   /// Ocurrs when the selected Tasks are changed
   /// </summary>
   public event ChangedSelectedTask SelectedTaskChanged;
   public delegate void ChangedSelectedTask(Int64? taskId);

   /// <summary>
   /// Ocurrs when the controls needs to be refilled
   /// </summary>
   public event Refresh ForceRefresh;
   public delegate void Refresh();

   protected void gvTasks_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {
      try
      {
         gvTasks.PageIndex = e.NewPageIndex;
         gvTasks.DataBind();

         ForceRefresh();
      }
      catch (Exception ex)
      {
         ZClass.raiseerror(ex);
      }
   }
   protected void gvTasks_SelectedIndexChanged(object sender, EventArgs e)
   {
      if (null != gvTasks.SelectedRow)
      {
         Int64 TaskId;
         try
         {
            if (Int64.TryParse(gvTasks.SelectedRow.Cells[TASK_ID_INDEX].Text, out TaskId))
               SelectedTaskChanged((Int64?)TaskId);
            else
               SelectedTaskChanged(null);
         }
         catch (Exception ex)
         {
            ZClass.raiseerror(ex);
         }
      }
   }
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!Page.IsPostBack)
      {
         try
         {
            ValidateStatus();
         }
         catch (Exception ex)
         {
            ZClass.raiseerror(ex);
         }
      }
   }
   protected void btUpdate_Click(object sender, EventArgs e)
   {
      try
      {
         ForceRefresh();
      }
      catch (Exception ex)
      {
         ZClass.raiseerror(ex);
      }
   }
   #endregion

   #region Constructores
   public FinishedTasksList()
   {
      lbNoTasks = new Label();
      gvTasks = new GridView();
   }
   #endregion

   public void Select(Int64? taskId)
   {
      if (taskId.HasValue)
      {
         GridViewRow CurrentRow = null;
         Int64 RowTaskId;

         for (int i = 0; i < gvTasks.Rows.Count; i++)
         {
            CurrentRow = gvTasks.Rows[i];
            if (Int64.TryParse(CurrentRow.Cells[TASK_ID_INDEX].Text, out RowTaskId))
            {
               if (RowTaskId == taskId)
               {
                  gvTasks.SelectedIndex = i;
                  SelectedTaskChanged((Int64?)RowTaskId);

                  break;
               }
            }
         }

         if (null != CurrentRow)
         {
            CurrentRow.Dispose();
            CurrentRow = null;
         }
      }
      else
      {
         gvTasks.SelectedIndex = -1;
         SelectedTaskChanged(null);
      }
   }

   /// <summary>
   /// Shows or Hides this control according to its status
   /// </summary>
   private void ValidateStatus()
   {
      if (gvTasks.Rows.Count == 0)
      {
         lbNoTasks.Visible = true;
         lbNoTasks.Text = MESSAGE_NO_TASKS;
         gvTasks.SelectedIndex = -1;
         gvTasks.Visible = false;
         SelectedTaskChanged(null);
      }
      else
      {
         lbNoTasks.Visible = false;
         gvTasks.Visible = true;
      }
   }

}