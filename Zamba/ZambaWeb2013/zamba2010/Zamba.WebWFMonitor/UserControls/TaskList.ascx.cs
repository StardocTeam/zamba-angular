using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.WFBusiness;

public partial class UserControls_TaskList : System.Web.UI.UserControl
{
    public delegate void ShowTaskDetails(Int32 TaskId);
    public event ShowTaskDetails EventShowTaskDetails;
    //public event ShowTaskDetails(Int32 TaskId )
    /// <summary>
    /// Devuelve una lista de las tareas seleccionadas
    /// </summary>
    /// <returns></returns>
    public List<TaskResult> GetSelectedTasks()
    {
        List<TaskResult> Tasks = new List<TaskResult>();
        Int32 WorkflowId = Int32.Parse(Session["WfId"].ToString());
        Int32 TaskId = 0;
        GridViewRow CurrentRow = null;

        for (int i = 0; i < gvTasks.Rows.Count; i++)
        {
            if (((CheckBox)gvTasks.Rows[i].FindControl("CheckBox1")).Checked)
            {
                TaskId = (Int32)gvTasks.DataKeys[i].Value;
                Tasks.Add(WFTaskBussines.GetTaskByTaskId(TaskId, WorkflowId));
            }
        }
        return Tasks;
    }
    /// <summary>
    /// Devuelve los IDs de las tareas seleccionadas
    /// </summary>
    /// <returns></returns>
    public List<Int32> GetSelectedIds()
    {
        List<Int32> TasksIds = new List<int>();
        GridViewRow CurrentRow = null;
        for (int i = 0; i < gvTasks.Rows.Count; i++)
        {
            if (((CheckBox)gvTasks.Rows[i].FindControl("CheckBox1")).Checked)
                TasksIds.Add((Int32)gvTasks.DataKeys[i].Value);
        }
        return TasksIds;
    }
    public bool HasSelectedTasks()
    {
        if (gvTasks.Rows == null)
            return false;
        else
            return (gvTasks.Rows.Count > 0);
    }

    public void SetDataSource()
    {
        try
        {
            // 'Dim StepId As Integer = Session("StepID")
            //'Dim WorkflowId As Integer = Session("WfId")
            //'Dim StepStates As SortedList = WFStepBussines.GetStepStates(StepId, WorkflowId)
            //'If IsNothing(StepStates) Then
            //'    'poner ninguno
            //'Else
            //'    DropDownList3.DataSource = StepStates.Values
            //'    DropDownList3.DataBind()
            //'End If
        }
        catch (Exception ex)
        {
            ZClass.RaiseError(ex);
        }
    }

    public void Refresh()
    {
        gvTasks.DataBind();
    }

    public void Imprimir()
    {
        //Session["PrintGridviewColumns"] = gvTasks;
        //StringBuilder StrBuilder = new StringBuilder();
        //StrBuilder.Append("<script>");
        //StrBuilder.Append(Environment.NewLine);
        //StrBuilder.Append("window.open(\"PrintGridview.aspx\",\"Print\",\"top=5,left=5\");");
        //StrBuilder.Append(Environment.NewLine);
        //StrBuilder.Append("</script>");
        //Page.ClientScript.RegisterStartupScript(this.GetType, "print", StrBuilder.ToString);
    }

    public void sendemail()
    {
        //System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //System.Net.Configuration.MailSettingsSectionGroup settings = ((System.Net.Configuration.MailSettingsSectionGroup)(config.GetSectionGroup("system.net/mailSettings")));
        //System.Net.NetworkCredential credential = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //client.Host = settings.Smtp.Network.Host;
        //client.Credentials = credential;
        //MailMessage email = new MailMessage();
        //email.From = new MailAddress("patricio.mosse@stardoc.com.ar");
        //email.To.Add("patricio.mosse@stardoc.com.ar");
        //email.Subject = "Test Email";
        //email.IsBodyHtml = true;
        //email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //StringBuilder s = new StringBuilder();
        //s.AppendLine("<HTML>");
        //s.AppendLine("</table>");
        //s.AppendLine("<table width=" + Chr(34) + "80%" + Chr(34) + " border=" + Chr(34) + "0" + Chr(34) + " cellpadding=" + Chr(34) + "1" + Chr(34) + " cellspacing=" + Chr(34) + "1" + Chr(34) + " align=" + Chr(34) + "center" + ">");
        //s.AppendLine("<tr bgcolor=" + Chr(34) + "white" + Chr(34) + ">");
        //s.AppendLine("<td width=" + Chr(34) + " 40%" + Chr(34) + "align=" + Chr(34) + "left" + Chr(34) + "><font class=" + Chr(34) + "program_header" + Chr(34) + ">Nombre</font></td>");
        //s.AppendLine("<td width=" + Chr(34) + "25%" + Chr(34) + " align=" + Chr(34) + "center" + Chr(34) + "><font class=" + Chr(34) + "program_header" + Chr(34) + ">Fecha de Expiracion</font></td>");
        //s.AppendLine("</tr>");
        //foreach (GridViewRow R in gvTasks.Rows)
        //{
        //    s.AppendLine("<tr bgcolor=" + Chr(34) + "#ffffff" + Chr(34) + ">");
        //    s.AppendLine("<td width=" + Chr(34) + "40%" + Chr(34) + ">" + GetText(R.Cells(1).Controls(0)) + "</td>");
        //    s.AppendLine("<td width=" + Chr(34) + "25%" + Chr(34) + "align=" + Chr(34) + "center" + Chr(34) + ">" + R.Cells(4).Text + "</td>");
        //    s.AppendLine("</tr>");
        //}
        //s.AppendLine("</table>");
        //s.AppendLine("</HTML>");
        //email.Body = s.ToString;
        //client.Send(email);
    }

    protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (String.Compare(e.CommandName, "ShowDetails") == 0)
        {
            Int32 TaskId = Convert.ToInt32(gvTasks.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value);
            Session.Add("TaskId", TaskId);
            if (TaskId != 0)
                EventShowTaskDetails(TaskId);
        }
    }

    // / <summary>
    /// Asigna un usuario a las tareas seleccionadas 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    /// 

    protected void btAsignarAceptar_Click(Object sender, System.EventArgs e)
    {
        if (this. HasSelectedTasks())
        {
            try
            {
                User AsignedTo = UserBussines.GetUserById(Int32.Parse(ddlAsignarUsuarios.SelectedValue));
                List<TaskResult> Tasks = this.GetSelectedTasks();
                Business Logica = new Business();
                Logica.Asignar(Tasks, AsignedTo);
                this.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
            }
        }
    }

    /// <summary>
    /// Quita del Workflow o elimina las tareas seleccionadas 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void btQuitarTarea_Click(Object sender, System.EventArgs e)
    {
        if (this.HasSelectedTasks())
        {
            try
            {
                Boolean DeleteDocument = false;

                if (String.Compare(rblQuitarOpciones.SelectedValue, "Tarea") == 0)
                    DeleteDocument = false;
                else if (String.Compare(rblQuitarOpciones.SelectedValue, "Todo") == 0)
                    DeleteDocument = true;

                List<TaskResult> Tasks = this.GetSelectedTasks();
                Business Logica = new Business();
                Logica.Quitar(Tasks, DeleteDocument);
                this.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex );
            }
        }
    }

    /// <summary>
    /// Distribuye a otra etapa las tareas seleccionadas mediante un PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    /// 
    protected void btDistribuirAceptar_Click(Object sender, System.EventArgs e)
    {
        if (this.HasSelectedTasks())
        {
            try
            {
                //Dim Value As String = HiddenField2.Value
                //            Dim SessionWfId As Long = DirectCast(Session("WfId"), Long)
                //            Dim Wf As WorkFlow
                //            Dim DsWF As DsWF = WFBusiness.GetAllWorkflows()
                //            For Each WW As WorkFlow In WFFactory.GetWFs()
                //                If WW.ID = SessionWfId Then
                //                    Wf = WW
                //                    Exit For
                //                End If
                //            Next
                //            'DISTRIBUYO
                //            Dim NewWfStep As WFStep = WFStepsFactory.GetStep(Wf, Value.Split("*")(0))

                //            For Each taskResult As TaskResult In this.GetSelectedTasks()
                //                DistributeTask(taskResult, NewWfStep, True)
                //            Next
                //            'ASIGNO
                //            Dim AsignedTo As User
                //            If Not Value.StartsWith("-1") Then
                //                AsignedTo = GetUserById(Value.Split("*")(1))
                //            Else
                //                AsignedTo = New User
                //            End If

                //            For Each taskResult As TaskResult In this.GetSelectedTasks()
                //                FillUsersAndGroups(taskResult.WfStep)
                //                AsignTask(taskResult, AsignedTo, AsignedTo)
                //            Next
                //            'If Value.Split("*")(2) = "on" andalso Not Value.StartsWith("-1")
                //            this.Refresh()

            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
            }
        }
    }

    /// <summary>
    /// Renueva la fecha de vencimiento de las tareas seleccionadas mediaten un PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void btRenovar_Click(object sender, System.EventArgs e)
    {
        if (this.HasSelectedTasks())
        {
            try
            {
                DateTime Fecha = DateTime.Parse(tbRenovarFecha.Text);
                String Hora = ddlRenovarHoras.SelectedValue;
                Fecha.AddHours(Double.Parse(Hora.Substring(1, 2)));
                Fecha.AddHours(Double.Parse(Hora.Substring(3, 2)));
                List<TaskResult> Tasks = this.GetSelectedTasks();
                Business Logica = new Business();
                Logica.Renovar(Tasks, Fecha);
                this.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
            }
        }
    }

    /// <summary>
    /// Desasigna las tareas seleccionadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void btDesasignar_Click(object sender, System.EventArgs e)
    {
        if (this.HasSelectedTasks())
        {
            try
            {
                List<TaskResult> Tasks = this.GetSelectedTasks();
                Business Logica = new Business();
                Logica.Desasignar(Tasks, null);
                this.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
            }
        }
    }
    /// <summary>
    /// Cambia el estado de las tareas seleccionadas 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void btEstado_Click(Object sender, System.EventArgs e)
    {
        if (this.HasSelectedTasks())
        {
            try
            {
                Int32 SelectedStateId = Int32.Parse(ddlEstados.SelectedValue);
                Int32 StepId = Int32.Parse(Session["StepId"].ToString());
                Int32 WorkFlowId = Int32.Parse(Session["WfId"].ToString());
                List<TaskResult> Tasks = this.GetSelectedTasks();
                Business Logica = new Business();
                Logica.CambiarEstado(Tasks, WorkFlowId , StepId, SelectedStateId);
                this.Refresh();
            }
            catch (Exception ex)
            {
                ZClass.RaiseError(ex);
            }
        }
    }

    /// <summary>
    /// Imprime parte de la grilla de resultados
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void btImprimir_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Imprimir();
        }
        catch (Exception ex)
        {
            ZClass.RaiseError(ex);
        }
    }
    /// <summary>
    /// Manda por E-Mail la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>

    protected void btMail_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.sendemail();
        }
        catch (Exception ex)
        {
            ZClass.RaiseError(ex);
        }
    }

}