
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core.Enumerators;
using Zamba.Core.WF.WF;
using Zamba.Data;
using Zamba.Services;
using Zamba.Core;
using System.Linq;
using Zamba.Membership;
using Zamba;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.Http;


public partial class TaskHeader : System.Web.UI.UserControl, ITaskHeader
{
    private Zamba.Core.ITaskResult iTaskResult;
    private System.Collections.Generic.List<Int64> idRules = new System.Collections.Generic.List<Int64>();
    private Hashtable hshRulesNames = new Hashtable();

    private bool dontLoadUAC;
    UserPreferences UP = new UserPreferences();
    WFTaskBusiness WFTB = new WFTaskBusiness();
    RightsBusiness RB = new RightsBusiness();
    UserGroupBusiness UserGroupBusiness = new UserGroupBusiness();
    public ITaskResult TaskResult
    {
        get { return iTaskResult; }
        set
        {
            iTaskResult = value;

            try
            {
                FillHeader();

                if (iTaskResult.TaskState == TaskStates.Desasignada && iTaskResult.m_AsignedToId != 0)
                {
                    iTaskResult.TaskState = TaskStates.Asignada;
                    STasks staks = new STasks();
                    staks.UpdateTaskState(iTaskResult.TaskId, TaskStates.Asignada);
                    staks = null;
                }

                TaskResult.EditDate = DateTime.Now;
                SetStepName(TaskResult.StepId);
                UACCell.Controls.Clear();
                DisablePropertyControls();

                WFTB.RegisterTaskAsOpen(TaskResult.TaskId, MembershipHelper.CurrentUser.ID);

                List<long> users = UserGroupBusiness.GetUsersIds(TaskResult.m_AsignedToId);
                if (TaskResult.AsignedToId == MembershipHelper.CurrentUser.ID || TaskResult.AsignedToId == 0 || users.Contains(MembershipHelper.CurrentUser.ID)) // Si el asignado soy yo o nadie
                {
                    if (!IsPostBack && TaskResult.TaskState != TaskStates.Ejecucion)// Si no esta en ejecucion, se fija si inicia la tarea al abrir
                        IniciarTareaAlAbrir(TaskResult);
                }
                else
                {
                    SUsers SUsers;
                    string userOrGroupName = string.Empty;

                    if (TaskResult.TaskState == TaskStates.Ejecucion) // Si la tiene en ejecucion
                    {
                        Boolean IsGroup = false;

                        SUsers = new SUsers();
                        userOrGroupName = SUsers.GetUserorGroupNamebyId(TaskResult.AsignedToId,ref IsGroup);
                        DisableGUI("Esta tarea se encuentra en ejecución por el usuario " + userOrGroupName);
                    }
                    //else if (!RB.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId))) // O no tengo permiso para desasignar
                    //{
                    //    SUsers = new SUsers();
                    //    userOrGroupName = SUsers.GetUserorGroupNamebyId(TaskResult.AsignedToId);
                    //    //DisableGUI("No tiene permiso para desasignar tarea asignada a " + userOrGroupName);
                    //}
                }

                if (CheckUserActionLoad())
                    LoadUserAction();
                else
                    HideFormRules();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }
    }

    //private DateTime Now()
    //{
    //    throw new NotImplementedException();
    //}

    public object Conversion { get; private set; }
    public object Usercontrol { get; private set; }
    public object Strings { get; private set; }

    private void SetStepName(string name)
    {
        this.lbletapadata2.InnerHtml = string.Empty;
    }

    private void SetStepName(Int64 id)
    {
        try
        {
            this.lbletapadata2.InnerHtml = WFStepBusiness.GetStepNameById(id);
            this.IconStep.Attributes.Add("title", "Etapa: " + this.lbletapadata2.InnerHtml);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            this.lbletapadata2.InnerHtml = string.Empty;
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (Zamba.Membership.MembershipHelper.CurrentUser != null && (TaskResult != null))
        {

            HiddenCurrentUserID.Value = MembershipHelper.CurrentUser.ID.ToString();
            this.HiddenTaskId.Value = TaskResult.TaskId.ToString();
            this.Hiddendocid.Value = TaskResult.ID.ToString();
            this.HiddenDocTypeId.Value = TaskResult.DocTypeId.ToString();
            this.Hiddenwfstepid.Value = TaskResult.StepId.ToString();
            this.hdnBtnPostback.Value = Page.ClientScript.GetPostBackEventReference(BtnClose, string.Empty);

            if ((Page.Session["Entrada" + TaskResult.ID] == null) == false)
            {
                Page.Session.Remove("Entrada" + TaskResult.ID);
                List<ITaskResult> results = new List<ITaskResult>();
                results.Add(TaskResult);

                SRules RulesS = new SRules();
                List<IWFRuleParent> rules = RulesS.GetCompleteHashTableRulesByStep(TaskResult.StepId);

                if ((rules != null))
                {
                    //[Ezequiel] Obtengo los ids de las reglas de entrada.

                    var ruleIDs = from rule in rules where rule.ParentType == TypesofRules.Entrada select rule.ID;

                    foreach (Int64 rid in ruleIDs)
                    {
                        if (ExecuteRule != null)
                        {
                            ExecuteRule(rid, results);
                        }
                    }
                }

                //[Ezequiel] - Refresco la tarea por si se realizaron cambios en las reglas de entrada.
                //[Javier] - Se agrega validación que si tiene que mostrar una regla de interfaz grafica, no refresque.
                if (Session[TaskResult.TaskId + "CurrentExecution"] == null)
                {
                    //         string script = "try { parent.RefreshCurrentTab(); } catch (e) {  }";
                    //       Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
                }
            }
        }
    }

    #region "CommonActions"
    protected void BtnStart_Click(object sender, EventArgs e)
    {
        this.btnIniciar_Click();

        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "refresh", "$(document).ready(function(){Refresh_C();});", true);
    }

    //Eliminado el handler del botón
    protected void BtnFinish_Click(object sender, EventArgs e)
    {
        this.btnTerminar_Click();
    }

    protected void BtnReAsign_Click(object sender, EventArgs e)
    {
        string script = "$(document).ready(function() { showAssignModal(); });";
        Page.ClientScript.RegisterStartupScript(typeof(Page), "Message", script, true);
        this.btnDerivar_Click();
    }

    protected void BtnRemove_Click(object sender, EventArgs e)
    {
        Quitar();
    }



    #region "Iniciar"
    /// <summary>
    /// Botón que se ejecuta al hacer click sobre el botón Iniciar
    /// </summary>
    /// <remarks></remarks>
    /// <history>
    ///     [Gaston]    01/09/2008  Modified    Llamada al método loadWfStepRules
    /// </history>
    private void btnIniciar_Click()
    {
        SUsers SUsers = new SUsers();
        SRights SRights = new SRights();
        STasks Stasks = new STasks();
        SRules Srules = new SRules();

        try
        {
            System.Collections.Generic.List<long> users = SUsers.GetUsersIds(TaskResult.AsignedToId);
            //Si la tarea no esta asignada, esta asignada al usuario o asignada a algun grupo del usuario o tengo el permiso de desasignar

            if (TaskResult.AsignedToId == 0 || TaskResult.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID) || (TaskResult.TaskState == Zamba.Core.TaskStates.Asignada && RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)) == true))
            {
                if (TaskResult.AsignedToId == 0)
                {
                    Stasks.AsignTask(ref iTaskResult, Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, false);
                    ExecuteAsignedToRules();
                }
                else if (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID))
                {
                    DataTable dt = WFStepBusiness.getTypesOfPermit(TaskResult.StepId, TypesofPermits.DontAsignTaskAsignedToGroup);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][2] == null || dt.Rows[0][2].ToString() == "0")
                        {
                            Stasks.AsignTask(ref iTaskResult, Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, false);
                            ExecuteAsignedToRules();
                        }
                    }
                    else
                    {
                        Stasks.AsignTask(ref iTaskResult, Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, false);
                        ExecuteAsignedToRules();
                    }

                }
                else if (TaskResult.m_AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID && TaskResult.TaskState !=  TaskStates.Ejecucion)
                {
                    Stasks.AsignTask(ref iTaskResult, Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, true);
                    ExecuteAsignedToRules();
                }
                else if ((TaskResult.TaskState == Zamba.Core.TaskStates.Asignada && RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)) == true))
                {
                    Stasks.AsignTask(ref iTaskResult, Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, true);
                    ExecuteAsignedToRules();
                }

                //La coleccion de tareas se pasa por referencia
                TaskResult.TaskState = TaskStates.Ejecucion;
                Stasks.InitialiceTask(ref iTaskResult);

                System.Collections.Generic.List<ITaskResult> Results = new System.Collections.Generic.List<ITaskResult>();
                Results.Add(TaskResult);

                WFRulesBusiness WFRulesBusiness = new WFRulesBusiness();
                WFRulesBusiness.ExecuteStartRules(ref Results);

                GenerateUserActions();
            }
            else
            {
                UpdateGUITaskAsignedSituation(TaskResult);

                //Tomas: se valida que el usuario no sea el que genero la tarea (wi:6753)
                if (TaskResult.AsignedById != Zamba.Membership.MembershipHelper.CurrentUser.ID)
                {
                    this.lblmsj.InnerHtml = "El usuario no tiene permiso para iniciar la tarea o la misma esta siendo utilizada por otro usuario";
                }
                return;
            }


        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    private void UpdateGUITaskAsignedSituation(ITaskResult taskResult)
    {
        //throw new NotImplementedException();
    }

    /// <summary>
    /// Verifica si inicia la tarea al abrir el documento
    /// </summary>
    private void IniciarTareaAlAbrir(Zamba.Core.ITaskResult task)
    {
        if (RB.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Iniciar, Convert.ToInt32(TaskResult.StepId)))
        {
            if (task.TaskState == TaskStates.Servicio)
            {
                DisableGUI("Tarea en ejecución por un servicio");
            }
            else
            {
                Zamba.Core.IWFStep wfstep = default(Zamba.Core.IWFStep);

                try
                {
                   // string currentLockedUser = null;

                    //if (WFTaskBusiness.LockTask(TaskResult.TaskId, ref currentLockedUser))
                    //{
                    SSteps WFSTEPSER = new SSteps();
                    wfstep = WFSTEPSER.GetStep(task.StepId);

                    if (wfstep.StartAtOpenDoc)
                    {
                        System.Collections.Generic.List<long> users = UserGroupBusiness.GetUsersIds(task.m_AsignedToId);
                        if ((task.m_AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)))
                        {
                            //Asignada a mi o a un grupo al que pertenezco
                            btnIniciar_Click();
                        }
                        else if (task.m_AsignedToId != 0)
                        {
                            switch (task.TaskState)
                            {
                                case TaskStates.Asignada:
                                    if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)))
                                    {
                                        GenerateUserActions();
                                    }
                                    break;
                                case TaskStates.Desasignada:
                                    //Nunca deberia pasar por aca, porque si esta asignada a otro usuario o grupo, deberia estar en asignada o ejecucion
                                    btnIniciar_Click();
                                    break;
                                default:
                                    DisableGUI("Tarea en ejecución por otro usuario");
                                    break;
                            }
                        }
                        else if (task.m_AsignedToId == 0)
                        {
                            //Esta asignada a ninguno
                            btnIniciar_Click();
                        }
                    }
                    else
                    {
                        System.Collections.Generic.List<long> users = UserGroupBusiness.GetUsersIds(task.m_AsignedToId);
                        if (task.m_AsignedToId != 0 && task.m_AsignedToId != Zamba.Membership.MembershipHelper.CurrentUser.ID && users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID) == false)
                        {
                            if (task.TaskState == TaskStates.Ejecucion || task.TaskState == TaskStates.Servicio)
                            {
                                //BtnIniciar.Visible = false;
                            }
                            else
                            {
                                SetAsignedTo();
                            }
                        }
                        else
                        {
                            if (task.TaskState == TaskStates.Servicio)
                            {
                                //BtnIniciar.Visible = false;
                            }
                            else
                            {
                                SetAsignedTo();
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    DisableGUI("Esta tarea se encuentra en ejecución por el usuario " + currentLockedUser);
                    //}
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

            }

        }
        else
        {
            DisableGUI("No tiene permisos suficientes para iniciar la tarea");
        }
    }

    private void DisableGUI(string message)
    {
        //BtnIniciar.Visible = false;
        dontLoadUAC = true;
        lblmsj.InnerHtml = message;
        string script = "$(function (){$('#dropOptions').hide();}); ";
        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CloseOptionsScript", script, true);
    }

    private void GenerateUserActions()
    {
        SetAsignedTo();
        GetStatesOfTheButtonsRule();
    }
    #endregion

    #region "Finalizar"
    private void btnTerminar_Click()
    {
        SRights sRights = new SRights();
        STasks sTasks = new STasks();

        try
        {
            if (TaskResult != null)
            {
                WFTB.UnLockTask(TaskResult.TaskId);
            }

            if (chkTakeTask.Checked == false)
            {
                TaskResult.TaskState = Zamba.Core.TaskStates.Asignada;
                TaskResult.AsignedToId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
            }
            else if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(TaskResult.StepId)))
            {
                TaskResult.TaskState = Zamba.Core.TaskStates.Desasignada;
                TaskResult.AsignedToId = 0;
            }
            else
            {
                TaskResult.TaskState = Zamba.Core.TaskStates.Asignada;
                TaskResult.AsignedToId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
            }

            sTasks.Finalizar(TaskResult);
            ExecuteFinishRules();
            //Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByTaskId(TaskResult.TaskId)
            SetAsignedTo();
            LoadUserAction();
            GetStatesOfTheButtonsRule();
            EnablePropietaryControls();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
        finally
        {
            sRights = null;
            sTasks = null;
        }
    }

    private void ExecuteFinishRules()
    {
        try
        {
            System.Collections.Generic.List<Zamba.Core.ITaskResult> Results = new System.Collections.Generic.List<Zamba.Core.ITaskResult>();
            Results.Add(TaskResult);

            foreach (Zamba.Core.WFRuleParent Rule in TaskResult.WfStep.Rules)
            {
                if (Rule.RuleType == TypesofRules.Terminar)
                {
                    SRules SRules = new SRules();
                    SRules.ExecuteRule(Rule, Results, false);
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }
    #endregion

    #region "Derivar"
    private void btnDerivar_Click()
    {
        //Me.pnlAsign.Visible = True
        try
        {
            SSteps SSteps = new SSteps();
            //falta hacer el control de derivar y llamarlo con lightbox
            //            ucAssign = New UCAsignar(Me.TaskResult, UCAsignar.AsignTypes.Asignar)

            //todo agregar validacion que se haya realizado una derivacion

            try
            {
                this.lvwUsers.Items.Clear();
                List<IZBaseCore> WfStepUsersIdsAndNames = SSteps.GetStepUsersIdsAndNames(TaskResult.StepId);

                string Name = string.Empty;
                foreach (IZBaseCore u in WfStepUsersIdsAndNames)
                {
                    if (!(u.ID == 0))
                    {
                        ListItem i = new ListItem();
                        i.Value = u.ID.ToString();
                        i.Text = u.Name.ToString();
                        this.lvwUsers.Items.Add(i);
                    }
                }

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

            ExecuteDerivarRules();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    protected void BtnAsignar_Click(object sender, System.EventArgs e)
    {
        STasks stask = new STasks();
        SUsers susers = new SUsers();

        try
        {
            if (this.lvwUsers.SelectedItem.Selected == true)
            {
                //Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
                stask.DeriveTask(TaskResult, TaskResult.StepId, long.Parse(lvwUsers.SelectedItem.Value), lvwUsers.SelectedItem.Text, Zamba.Membership.MembershipHelper.CurrentUser.ID, DateTime.Now, true);
                susers.SaveAction(TaskResult.ID, ObjectTypes.ModuleWorkFlow, RightsType.DerivateTask, "Usuario Derivo La tarea");
                //lblNombreDocumento.InnerHtml = stask.NombreDocumento_currUserConfig;




            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }

        SetAsignedTo();
    }


    private void ExecuteDerivarRules()
    {
        try
        {
            System.Collections.Generic.List<Zamba.Core.ITaskResult> Results = new System.Collections.Generic.List<Zamba.Core.ITaskResult>();
            Results.Add(TaskResult);
            foreach (Zamba.Core.WFRuleParent Rule in TaskResult.WfStep.Rules)
            {
                if (Rule.RuleType == TypesofRules.Derivar)
                {
                    //Dim WFRB As New WFRulesBusiness()
                    SRules SRules = new SRules();
                    SRules.ExecuteRule(Rule, Results, false);
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }
    #endregion

    #region "Quitar"

    /// <summary>
    /// Quita la tarea del workflow.
    /// </summary>
    /// <remarks></remarks>
    /// <history>
    /// [Ezequiel] 07/05/09 - Created
    /// </history>
    public void Quitar()
    {
        //Dim WFTaskBusiness As New WFTaskBusiness(_user)
        STasks STasks = new STasks();
        STasks.RemoveTask(ref iTaskResult, false, Zamba.Membership.MembershipHelper.CurrentUser);
        Response.Write("<script language='javascript'> { parent.CloseTask(" + this.TaskResult.TaskId + ",true);}</script>");
    }

    #endregion

    #endregion

    /// <summary>
    /// Carga los datos del encabezado
    /// </summary>
    /// <remarks></remarks>
    private void FillHeader()
    {
        UserPreferences UserPreferences = new UserPreferences();
        SUsers SUsers = new SUsers();
        SSteps SSteps = new SSteps();
        STasks stask = new STasks();
        Boolean IsGroup = false;
        if (TaskResult.AsignedToId > 0)
            lblAsignedTo.InnerHtml = SUsers.GetUserorGroupNamebyId(TaskResult.AsignedToId, ref IsGroup).Replace("Zamba_", "").Replace("Zamba ", "");
        else
            lblAsignedTo.InnerHtml = "Ninguno";

      //  this.IconAsigned.Attributes.Add("title", "Asignado: " + this.lblAsignedTo.InnerHtml);
       // this.lblAsignedTo.Attributes.Add("title", this.lblAsignedTo.InnerHtml);

        lbletapadata2.InnerHtml = SSteps.GetStepNameById(TaskResult.StepId);
        // this.IconStep.Attributes.Add("title", "Etapa: " + this.lbletapadata2.InnerHtml);


        dtpFecVenc.InnerHtml = TaskResult.ExpireDate.ToString("dd/MM/yyyy");
 //       dtpFecVenc.Text = TaskResult.ExpireDate.ToString("dd/MM/yyyy");
        this.IconValidate.Attributes.Add("title", "Fecha de vencimiento: " + dtpFecVenc.InnerHtml);

        //CboStates.Items.Clear();
        //CboStates.DataTextField = "Name";
        //CboStates.DataValueField = "ID";
        //        CboStates.SelectedValue = null;
        CboStates.InnerHtml = string.Empty;
        //        WFStepBusiness WFSB = new WFStepBusiness();
        //23/09/11: Se cambia la forma en que accede a la lista de estados por etapas.
        //      CboStates.DataSource = WFSB.GetStepById(TaskResult.StepId).States;
        //    WFSB = null;
        //    CboStates.DataBind();
        CboStates.InnerHtml = TaskResult.State.Name;

//        CboStates.SelectedValue = TaskResult.State.ID.ToString();
        IconState.Attributes.Add("title", "Estado: " + TaskResult.State.Name.ToString());
  //      CboStates.SelectedIndexChanged += CboStates_SelectedIndexChanged;
        //lblNombreDocumento.InnerHtml = stask.NombreDocumento_currUserConfig;
    }

    public void SetAsignedTo()
    {
        try
        {
            UpdateGUITaskAsignedSituation(TaskResult);
            EnablePropietaryControls();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    private void UpdateGUITaskAsignedSituation(TaskResult AsignedTaskResult)
    {
        //Si la tarea esta asignada a algun usuario, va en negrita
        if (AsignedTaskResult.AsignedToId != 0)
        {
            Boolean IsGroup = false;
            lblAsignedTo.InnerHtml = UserGroups.GetUserorGroupNamebyId(AsignedTaskResult.AsignedToId, ref IsGroup).Replace("Zamba_", "").Replace("Zamba ", ""); ;
        }
        else
        {
            lblAsignedTo.InnerHtml = "[Ninguno]";
        }
    }

    /// <summary>
    /// Habilitación y deshabilitación de controles
    /// </summary>
    /// <remarks></remarks>
    /// <history>
    ///     [Gaston]    01/09/2008  Modified    Llamada al método loadWfStepRules
    ///     [Gaston]    08/01/2009  Modified    Llamada al método que permite ver si la etapa tiene o no permiso para habilitar o deshabilitar el
    ///                                         combo de estados
    /// </history>
    [AllowAnonymous]
    [System.Web.Services.WebMethod]
    private void EnablePropietaryControls()
    {

        SRights SRights = new SRights();
        UserPreferences UserPreferences = new UserPreferences();
        System.Collections.Generic.List<long> users = default(System.Collections.Generic.List<long>);

        try
        {
            //Si el id es un grupo, users tendra los usuarios del mismo, caso contrario se encontrara vacio
            users = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId);


            if (users.Count <= 0)
            {
                List<long> AsignedUser = new List<long>();
                AsignedUser.Add(TaskResult.AsignedToId);
                users = AsignedUser;

            }
            //Estado de tarea
            if ((TaskResult.TaskState == TaskStates.Desasignada && TaskResult.AsignedToId != 0))
            {
                TaskResult.TaskState = TaskStates.Asignada;
            }
            if (TaskResult.AsignedToId == 0)
            {
                TaskResult.TaskState = TaskStates.Desasignada;
            }

            //Iniciar
            //BtnIniciar.Visible = GetBtnIniciarVisibility(SRights, users);
            //BtnIniciar.Visible = true;

            //Acciones de usuario
            if (CheckUserActionLoad(users))
            {
                LoadUserAction();
            }
            else
            {
                HideFormRules();
            }

            //Habilitacion de opciones y combo de estados de etapa
            if ((TaskResult.TaskState == Zamba.Core.TaskStates.Ejecucion))
            {
                if ((RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.AllowStateComboBox, TaskResult.StepId) == true))
                {
                  //  CboStates.Enabled = true;
                }
                else
                {
                  //  CboStates.Enabled = false;
                }

                if (TaskResult.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID)
                {
                    this.chkTakeTask.Enabled = true;
                }
                else
                {
                    this.chkTakeTask.Enabled = false;
                }
                this.chkCloseTaskAfterDistribute.Enabled = true;
            }
            else
            {
               // this.CboStates.Enabled = false;
              //  this.dtpFecVenc.Enabled = false;
                this.chkTakeTask.Enabled = false;
                this.chkCloseTaskAfterDistribute.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
        finally
        {
            SRights = null;
            UserPreferences = null;
            users = null;
        }
    }

    private void HideFormRules()
    {
        string script = "$(function() { $('input[id^=zamba_rule_]').hide(); });";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "HideFormRules", script, true);
    }

    //TODO: MOVER A CAPA DE NEGOCIOS
    private bool GetBtnIniciarVisibility(SRights srights, List<long> users)
    {
        if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Iniciar, Convert.ToInt32(TaskResult.StepId)) && (TaskResult.TaskState == TaskStates.Desasignada || (TaskResult.TaskState == TaskStates.Asignada && RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)) && (Zamba.Membership.MembershipHelper.CurrentUser.ID != TaskResult.AsignedToId || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)))))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //TODO: MOVER A CAPA DE NEGOCIOS
    private bool CheckUserActionLoad(List<long> users)
    {
        if (!dontLoadUAC && TaskResult != null && TaskResult.TaskState == TaskStates.Ejecucion && (TaskResult.m_AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID || TaskResult.m_AsignedToId == 0 || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) || GetExecuteAssignedToOtherRight())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckUserActionLoad()
    {
        System.Collections.Generic.List<long> users = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId);
        return CheckUserActionLoad(users);
    }

    //TODO: MOVER A CAPA DE NEGOCIOS
    private bool GetExecuteAssignedToOtherRight()
    {
        return RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, Convert.ToInt32(TaskResult.StepId));
    }

    /// <summary>
    /// Habilita o deshabilita los botones basicos de la tarea
    /// </summary>
    /// <remarks></remarks>
    private void DisablePropertyControls()
    {
        this.BtnDerivar.Visible = false;
        this.BtnRemove.Visible = RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Delete, Convert.ToInt32(TaskResult.StepId));
        this.deleteCtrl.Visible = (bool.Parse(UP.getValue("ShowDeleteButton", UPSections.UserPreferences, "True")) && RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Delete, TaskResult.DocTypeId));

        if (this.deleteCtrl.Visible && deleteCtrl.Result == null)
        {
            deleteCtrl.Result = TaskResult;
        }


      //  this.dtpFecVenc.Enabled = RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.ModificarVencimiento, Convert.ToInt32(TaskResult.StepId));

      //  if (TaskResult.TaskState == TaskStates.Ejecucion || TaskResult.TaskState == TaskStates.Asignada)
       // {

            this.BtnDerivar.Visible = RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Delegates, Convert.ToInt32(TaskResult.StepId));

//        }



        if (TaskResult.TaskState != TaskStates.Ejecucion)
        {
          //  CboStates.Enabled = false;
           // this.dtpFecVenc.Enabled = false;
            this.chkTakeTask.Enabled = false;
            this.chkCloseTaskAfterDistribute.Enabled = false;
        }
    }

    /// <summary>
    /// Carga las acciones de usuario
    /// </summary>
    /// <remarks></remarks>
    [AllowAnonymous]
    [System.Web.Services.WebMethod]
    private void LoadUserAction()
    {
        try
        {
            SRules SRules = new SRules();
            string userActionName = string.Empty;
            idRules.Clear();
            UACCell.Controls.Clear();

            //todo wf falta ver si no se modificaron las reglas y cargarlas de nuevo desde la base en el wfstep

            List<Zamba.Core.IWFRuleParent> Rules = SRules.GetCompleteHashTableRulesByStep(TaskResult.StepId);

            if ((Rules != null))
            {
                WFRulesBusiness WFRB = new WFRulesBusiness();
                bool RuleEnabled = false;

                foreach (Zamba.Core.WFRuleParent Rule in Rules)
                {
                    if (TaskResult.UserRules.ContainsKey(Rule.ID))
                    {
                        //Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        //y en la 1 si se acumula a la habilitacion de las solapas o no
                        List<bool> lstRulesEnabled = (List<bool>)TaskResult.UserRules[Rule.ID];
                        RuleEnabled = lstRulesEnabled[0];
                    }
                    else
                    {
                        RuleEnabled = true;
                    }

                    if (RuleEnabled && Rule.ParentType == TypesofRules.AccionUsuario && !idRules.Contains(Rule.ID))
                    {
                        Button UAB = new Button();
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.Attributes.Add("style", "padding:1px");

                        UAB.ID = "UAB_Rule_" + Rule.ID;
                        UAB.CssClass = "btn btn-primary btn-xs";
                        

                        UAB.Style["height"] = "35px";
                        UAB.OnClientClick = "ShowLoadingAnimation();";
                        UAB.TabIndex = -1;

                        UAB.Click += UAB_Click;

                        //Busca en la tabla si existe un nombre de acción de usuario para esa regla
                        try
                        {
                            userActionName = SRules.GetUserActionName(Rule);
                        }
                        catch (Exception ex)
                        {
                            Zamba.Core.ZClass.raiseerror(ex);
                            userActionName = string.Empty;
                        }

                        //Si el nombre no existe entonces le asigna el nombre de la regla
                        if (string.IsNullOrEmpty(userActionName))
                        {
                            userActionName = Rule.Name.ToUpper();
                        }

                        //Asigna el nombre al botón. Si este es mayor que 20 lo corta y le agrega 3 puntos
                        UAB.ToolTip = userActionName;

                        if (userActionName.Length > 20)
                        {
                            UAB.Text = userActionName.Substring(0, 20) + "...";
                        }
                        else
                        {
                            UAB.Text = userActionName;
                        }

                        //Guarda el nombre en un hash para luego utilizarlo cuando se llame al saveAction
                        if (hshRulesNames.ContainsKey(Rule.ID))
                        {
                            hshRulesNames[Rule.ID] = userActionName;
                        }
                        else
                        {
                            hshRulesNames.Add(Rule.ID, userActionName);
                        }

                        li.Controls.Add(UAB);
                        this.UACCell.Controls.Add(li);

                        //Se guarda el id de la regla
                        idRules.Add(Rule.ID);
                    }
                }

                userActionName = string.Empty;
                WFRB = null;

                //Oculta/muestra reglas segun preferencias por cada result
                GetStatesOfTheButtonsRule();

            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    [AllowAnonymous]
    [System.Web.Services.WebMethod]
    public void GetStatesOfTheButtonsRule()
    {
        try
        {
            SRules SRules = new SRules();
            SSteps SSteps = new SSteps();
            SUsers SUsers = new SUsers();
            SWorkflow SWorkflow = new SWorkflow();
            Int32 contador = 0;
            //Dice si se va a usar el enable del tab o no
            bool useTabEnable = false;


            if (UACCell.Controls.Count > 0)
            {
                //Recorre cada regla activa en el documento
                foreach (Int64 idRule in idRules)
                {
                    useTabEnable = true;

                    //Si la regla no fue procesada antes por la DoEnable
                    if (TaskResult.UserRules.Contains(idRule))
                    {
                        //Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        //y en la 1 si se acumula a la habilitacion de las solapas o no
                        List<bool> lstRulesEnabled = (List<bool>)TaskResult.UserRules[idRule];
                        //Si la regla esta deshabilitada, no uso los estados de los tabs
                        if (lstRulesEnabled[0])
                        {
                            //Si no esta marcada la opcion de ejecucion conjunta con los tabs, no uso los estados
                            if (lstRulesEnabled[1] == false)
                            {
                                useTabEnable = false;
                            }
                        }
                        else
                        {
                            useTabEnable = false;
                        }
                    }

                    //Si utilizo los tabs (porq no uso la doenable o porq la ejecucion es conjunta)
                    if (useTabEnable)
                    {
                        WFBusiness WFB = new WFBusiness();
                        UACCell.Controls[contador].Visible = WFB.CanExecuteRules(idRule, Zamba.Membership.MembershipHelper.CurrentUser.ID, (WFStepState)TaskResult.State, (TaskResult)TaskResult);
                    }
                    contador = contador + 1;
                }
            }

        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Ejecuta una accion de usuario
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void UAB_Click(object sender, System.EventArgs e)
    {
        try
        {
            long RuleId = 0;
            Button UAB = new Button();
            List<Zamba.Core.ITaskResult> results = new List<Zamba.Core.ITaskResult>();

            UAB = (Button)sender;

            if ((UAB != null))
            {
                Session["ExecutingRule"] = RuleId;

                if (long.TryParse(UAB.ID.Replace("UAB_Rule_", string.Empty), out RuleId))
                {
                    results.Add(TaskResult);

                    WFTaskBusiness WFTB = new WFTaskBusiness();

                    WFTB.LogTask(TaskResult, hshRulesNames[Convert.ToInt64(UAB.ID.Split(char.Parse("_"))[2])].ToString());

                    WFTB = null;
                    if (ExecuteRule != null)
                    {
                        ExecuteRule(RuleId, results);
                    }
                }
            }

            LoadUserAction();
            string script = "$(document).ready(function(){ RefreshParentDataFromChildWindow()});";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "RefreshParentDataFromChildWindow", script, true);
        }
        catch (Exception ex)
        {
            lblmsj.InnerHtml = "Ha ocurrido un error en la ejecucion de la regla " + sender.ToString() + " , contactese con el administrador del sistema para mas informacion";
            ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Evento que ejecuta reglas
    /// </summary>
    /// <param name="ruleId">ID de la regla a ejecutar</param>
    /// <param name="results">Tareas a ejecutar</param>
    /// <remarks></remarks>
    public event ExecuteRuleEventHandler ExecuteRule;
    public delegate void ExecuteRuleEventHandler(Int64 ruleId, List<Zamba.Core.ITaskResult> results);

    /// <summary>
    /// Se cambio el estado de la tarea desde el combo de estados
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void CboStates_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            //update the change state
            int StateID = int.Parse(((DropDownList)sender).SelectedValue);
            WFTB.UpdateTaskState(TaskResult.TaskId, StateID);

            //log in the change state
            WFTB.LogTask(TaskResult,"Cambio Estado");

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }

    /// <summary>
    /// Cambiar el estado
    /// </summary>
    /// <param name="isExecuteSetStateRule"></param>
    /// <remarks></remarks>
    public void SetState(bool isExecuteSetStateRule)
    {
        try
        {
            //CboStates.SelectedIndexChanged -= CboStates_SelectedIndexChanged;
            //if (TaskResult.State.ID > 0)
            //{
            //    for (Int32 i = 0; i <= CboStates.Items.Count - 1; i++)
            //    {
            //        this.CboStates.SelectedIndex = i;
            //        if (CboStates.SelectedValue == TaskResult.State.ID.ToString())
            //        {
            //            break; // TODO: might not be correct. Was : Exit For
            //        }
            //    }
            //}

            //CboStates.SelectedIndexChanged += CboStates_SelectedIndexChanged;

            //if (isExecuteSetStateRule)
            //{
            //    ExecutedSetState();
            //}

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    private void ExecuteAsignedToRules()
    {

        try
        {
            System.Collections.Generic.List<ITaskResult> Results = new System.Collections.Generic.List<ITaskResult>();
            Results.Add(TaskResult);
            var rules = TaskResult.WfStep.Rules;

            loadWfStepRules(TaskResult.StepId, ref rules);

            WFRulesBusiness WFRB = new WFRulesBusiness();
            foreach (WFRuleParent Rule in TaskResult.WfStep.Rules)
            {
                if (Rule.RuleType == TypesofRules.Asignar)
                {
                    WFRB.ExecuteRule(Rule, Results, false);
                }
            }
            WFRB = null;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }


    private void ExecutedSetState()
    {
        try
        {
            List<ITaskResult> Results = new List<ITaskResult>();
            Results.Add(TaskResult);
            var rules = TaskResult.WfStep.Rules;

            loadWfStepRules(TaskResult.StepId, ref rules);

            foreach (WFRuleParent Rule in TaskResult.WfStep.Rules)
            {
                if (Rule.RuleType == TypesofRules.Estado)
                {
                    WFRulesBusiness WFRB = new WFRulesBusiness();
                    WFRB.ExecuteRule(Rule, Results, false);
                }
            }

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }

    private void loadWfStepRules(long stepId, ref List<IWFRuleParent> rules)
    {
        // Si la colección de reglas es Nothing o rules no posee elementos
        if (((rules == null) || (rules.Count == 0)))
        {
            WFRulesBusiness WFRulesBusiness = new WFRulesBusiness();
            rules = WFRulesBusiness.GetCompleteHashTableRulesByStep(stepId);
            WFRulesBusiness = null;
        }
    }

    protected void dtpFecVenc_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            string FecVenc = dtpFecVenc.InnerHtml;

            //update the change state
            if ((TaskResult != null))
            {
                WFTB.ChangeExpireDate( TaskResult.TaskId, DateTime.Parse(FecVenc));
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Este es un fix para guardar y obtener correctamente el valor del checkbox de Finalizar tarea al cerrar.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void chkTakeTask_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack)
            {
                string ctrl = Request.Params.Get("__EVENTTARGET");
                if (ctrl.Equals(chkTakeTask.UniqueID))
                {
                    bool value = false;
                    //Solo se guarda el valor cuando es diferente del hidden
                    if (!string.IsNullOrEmpty(hdnTakeTaskFix.Value) && bool.TryParse(hdnTakeTaskFix.Value, out value))
                    {
                        if (value != chkTakeTask.Checked)
                        {
                            SetOption(chkTakeTask.Checked, "CheckFinishTaskAfterClose");
                            hdnTakeTaskFix.Value = chkTakeTask.Checked.ToString();
                        }
                    }
                }
                else
                {
                    chkTakeTask.Checked = bool.Parse(hdnTakeTaskFix.Value);
                }
            }
            else
            {
                //Carga inicial del checkbox
                UserPreferences UserPreferences = new UserPreferences();
                SRights SRights = new SRights();
                if (TaskResult != null)
                {
                    chkTakeTask.Visible = RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(TaskResult.StepId));
                    if (chkTakeTask.Visible)
                    {
                        chkTakeTask.Checked = Convert.ToBoolean(UP.getValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, "True"));
                    }
                }
                SRights = null;
                UserPreferences = null;

                //Se guarda en un hidden para solucionar un problema de estados de checkbox
                hdnTakeTaskFix.Value = chkTakeTask.Checked.ToString();
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(new Exception("Error al cargar o guardar el valor de CheckFinishTaskAfterClose", ex));
        }
    }

    /// <summary>
    /// Este es un fix para guardar y obtener correctamente el valor del checkbox de Cerrar tarea al distribuir.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    protected void chkCloseTaskAfterDistribute_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack)
            {
                string ctrl = Request.Params.Get("__EVENTTARGET");
                if (ctrl.Equals(chkCloseTaskAfterDistribute.UniqueID))
                {
                    bool value = false;

                    //Solo se guarda el valor cuando es diferente del hidden
                    if (!string.IsNullOrEmpty(hdnCloseTaskFix.Value) && bool.TryParse(hdnCloseTaskFix.Value, out value))
                    {
                        if (value != chkCloseTaskAfterDistribute.Checked)
                        {
                            SetOption(chkCloseTaskAfterDistribute.Checked, "CloseTaskAfterDistribute");
                            hdnCloseTaskFix.Value = chkCloseTaskAfterDistribute.Checked.ToString();
                        }
                    }
                }
                else
                {
                    chkCloseTaskAfterDistribute.Checked = bool.Parse(hdnCloseTaskFix.Value);
                }
            }
            else
            {
                //Carga inicial del checkbox
                UserPreferences UserPreferences = new UserPreferences();
                chkCloseTaskAfterDistribute.Checked = Convert.ToBoolean(UP.getValue("CloseTaskAfterDistribute", UPSections.WorkFlow, "False"));
                UserPreferences = null;

                //Se guarda en un hidden para solucionar un problema de estados de checkbox
                hdnCloseTaskFix.Value = chkCloseTaskAfterDistribute.Checked.ToString();
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(new Exception("Error al cargar o guardar el valor de CloseTaskAfterDistribute", ex));
        }
    }

    private void SetOption(bool value, string checkOption)
    {
       // UserPreferences sup = default(UserPreferences);

        try
        {
            UserPreferences.setValue(checkOption, value.ToString(), UPSections.WorkFlow);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            //sup = null;
        }
    }

    void ITaskHeader.FillHeader()
    {
        throw new NotImplementedException();
    }

    //public TaskHeader()
    //{
    //    Load += Page_Load;
    //}
}


