Imports System.Collections.Generic
Imports Zamba.Core.Enumerators
Imports System.Data
Imports Zamba.Core.WF.WF
Imports Zamba.Data
Imports Zamba.Services
Imports Zamba.Core
Imports System.Linq
Imports Zamba.Membership
Imports Zamba

Partial Public Class TaskHeader
    Inherits System.Web.UI.UserControl
    Implements ITaskHeader

    Private _userid As Long
    Private _user As Zamba.Core.IUser
    Private _TaskId As Integer
    Private iTaskResult As Zamba.Core.TaskResult
    Private idRules As New Generic.List(Of Int64)
    Private hshRulesNames As New Hashtable
    Private dontLoadUAC As Boolean

    Public Property TaskResult() As Zamba.Core.ITaskResult Implements ITaskHeader.TaskResult
        Get
            Return iTaskResult
        End Get
        Set(ByVal value As Zamba.Core.ITaskResult)
            iTaskResult = value

            Try
                FillHeader()

                If iTaskResult.TaskState = TaskStates.Desasignada AndAlso iTaskResult.m_AsignedToId <> 0 Then
                    iTaskResult.TaskState = TaskStates.Asignada
                    Dim staks As New STasks
                    staks.UpdateTaskState(iTaskResult.TaskId, TaskStates.Asignada)
                    staks = Nothing
                End If

                TaskResult.EditDate = Now()
                SetStepName(TaskResult.StepId)
                UACCell.Controls.Clear()
                DisablePropertyControls()
                IniciarTareaAlAbrir(TaskResult)
                Zamba.Core.WF.WF.WFTaskBusiness.RegisterTaskAsOpen(TaskResult.TaskId, Page.Session("UserId"))
                If CheckUserActionLoad() Then
                    LoadUserAction()
                Else
                    HideFormRules()
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Set
    End Property

    Private Sub SetStepName(ByVal name As String)
        Me.lbletapadata.InnerHtml = name.Trim
    End Sub

    Private Sub SetStepName(ByVal id As Int64)
        Try
            Me.lbletapadata.InnerHtml = WFStepBusiness.GetStepNameById(id)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Me.lbletapadata.InnerHtml = String.Empty
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.Session("UserId") Is Nothing = False AndAlso Not (TaskResult Is Nothing) Then

            _userid = Long.Parse(Session("UserId"))

            Me.HiddenTaskId.Value = TaskResult.TaskId
            Me.Hiddendocid.Value = TaskResult.ID
            Me.HiddenDocTypeId.Value = TaskResult.DocTypeId
            Me.Hiddenwfstepid.Value = TaskResult.StepId
            Me.hdnBtnPostback.Value = Page.ClientScript.GetPostBackEventReference(BtnClose, String.Empty)

            If Page.Session("Entrada" & TaskResult.ID) Is Nothing = False Then
                Page.Session.Remove("Entrada" & TaskResult.ID)
                Dim results As New List(Of ITaskResult)
                results.Add(TaskResult)

                Dim RulesS As New SRules()
                Dim rules As List(Of IWFRuleParent) = RulesS.GetCompleteHashTableRulesByStep(TaskResult.StepId)
                Dim ruleID As Int64 = 0
                If Not IsNothing(rules) Then
                    '[Ezequiel] Obtengo los ids de las reglas de entrada.

                    Dim ruleIDs = From rule In rules _
                                  Where rule.ParentType = TypesofRules.Entrada _
                                  Select rule.ID

                    For Each rid As Int64 In ruleIDs.ToList()
                        RaiseEvent ExecuteRule(rid, results)
                    Next
                End If

                '[Ezequiel] - Refresco la tarea por si se realizaron cambios en las reglas de entrada.
                '[Javier] - Se agrega validación que si tiene que mostrar una regla de interfaz grafica, no refresque.
                If Session(TaskResult.TaskId & "CurrentExecution") Is Nothing Then
                    Dim script As String = "try { parent.RefreshCurrentTab(); } catch (e) { alert(e.description); }"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "DoOpenTaskScript", script, True)
                End If
            End If
        End If
    End Sub

#Region "CommonActions"
    Protected Sub BtnStart_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.btnIniciar_Click()
    End Sub

    'Eliminado el handler del botón
    Protected Sub BtnFinish_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.btnTerminar_Click()
    End Sub

    Protected Sub BtnReAsign_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim script As String = "$(document).ready(function() { showAssignModal(); });"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "Message", script, True)
        Me.btnDerivar_Click()
    End Sub

    Protected Sub BtnRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        Quitar()
    End Sub



#Region "Iniciar"
    ''' <summary>
    ''' Botón que se ejecuta al hacer click sobre el botón Iniciar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    01/09/2008  Modified    Llamada al método loadWfStepRules
    ''' </history>
    Private Sub btnIniciar_Click()
        Dim SUsers As New SUsers()
        Dim SRights As New SRights()
        Dim Stasks As New STasks()
        Dim Srules As New SRules()

        Try
            Dim users As Generic.List(Of Long) = SUsers.GetUsersIds(TaskResult.AsignedToId, True)

            'Si la tarea no esta asignada, esta asignada al usuario o asignada a algun grupo del usuario o tengo el permiso de desasignar
            If TaskResult.AsignedToId = 0 OrElse TaskResult.AsignedToId = _user.ID OrElse users.Contains(_user.ID) OrElse (TaskResult.TaskState = Zamba.Core.TaskStates.Asignada AndAlso SRights.GetUserRights(ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) = True) Then

                If TaskResult.AsignedToId = 0 Then
                    Stasks.AsignTask(DirectCast(TaskResult, TaskResult), _user.ID, _user.ID, False)
                    ExecuteAsignedToRules()
                ElseIf users.Contains(_user.ID) Then
                    Dim dt As DataTable = WFStepBusiness.getTypesOfPermit(TaskResult.StepId, TypesofPermits.DontAsignTaskAsignedToGroup)

                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0)(2) = False Then
                            Stasks.AsignTask(DirectCast(TaskResult, TaskResult), _user.ID, _user.ID, False)
                            ExecuteAsignedToRules()
                        End If
                    Else
                        Stasks.AsignTask(DirectCast(TaskResult, TaskResult), _user.ID, _user.ID, False)
                        ExecuteAsignedToRules()
                    End If
                ElseIf TaskResult.m_AsignedToId = _user.ID Then

                ElseIf (TaskResult.TaskState = Zamba.Core.TaskStates.Asignada AndAlso SRights.GetUserRights(ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) = True) Then
                    Stasks.AsignTask(DirectCast(TaskResult, TaskResult), _user.ID, _user.ID, True)
                    ExecuteAsignedToRules()
                End If

                'La coleccion de tareas se pasa por referencia
                TaskResult.TaskState = TaskStates.Ejecucion
                Stasks.InitialiceTask(DirectCast(TaskResult, TaskResult))

                Dim Results As New System.Collections.Generic.List(Of ITaskResult)
                Results.Add(TaskResult)

                Dim WFRulesBusiness As New WFRulesBusiness
                WFRulesBusiness.ExecuteStartRules(Results)

                GenerateUserActions()
            Else
                UpdateGUITaskAsignedSituation(TaskResult)

                'Tomas: se valida que el usuario no sea el que genero la tarea (wi:6753)
                If TaskResult.AsignedById <> MembershipHelper.CurrentUser.ID Then
                    Me.lblmsj.InnerHtml = "El usuario no tiene permiso para iniciar la tarea o la misma esta siendo utilizada por otro usuario"
                End If
                Exit Sub
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Verifica si inicia la tarea al abrir el documento
    ''' </summary>
    Private Sub IniciarTareaAlAbrir(ByVal task As Zamba.Core.ITaskResult)
        If RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Iniciar, CInt(TaskResult.StepId)) Then
            If task.TaskState = TaskStates.Servicio Then
                DisableGUI("Tarea en ejecución por un servicio")
            Else
                Dim wfstep As Zamba.Core.IWFStep

                Try
                    Dim currentLockedUser As String
                    If WFTaskBusiness.LockTask(TaskResult.TaskId, currentLockedUser) Then

                        Dim WFSTEPSER As New SSteps()
                        wfstep = WFSTEPSER.GetStep(task.StepId)

                        If wfstep.StartAtOpenDoc Then
                            Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(task.m_AsignedToId, True)
                            If (task.m_AsignedToId = MembershipHelper.CurrentUser.ID OrElse users.Contains(MembershipHelper.CurrentUser.ID)) Then
                                'Asignada a mi o a un grupo al que pertenezco
                                btnIniciar_Click()
                            ElseIf task.m_AsignedToId <> 0 Then
                                Select Case task.TaskState
                                    Case TaskStates.Asignada
                                        If RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) Then
                                            GenerateUserActions()
                                        End If
                                    Case TaskStates.Desasignada
                                        'Nunca deberia pasar por aca, porque si esta asignada a otro usuario o grupo, deberia estar en asignada o ejecucion
                                        btnIniciar_Click()
                                    Case Else
                                        DisableGUI("Tarea en ejecución por otro usuario")
                                End Select
                            ElseIf task.m_AsignedToId = 0 Then
                                'Esta asignada a ninguno
                                btnIniciar_Click()
                            End If
                        Else
                            Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(task.m_AsignedToId, True)
                            If task.m_AsignedToId <> 0 AndAlso task.m_AsignedToId <> MembershipHelper.CurrentUser.ID AndAlso users.Contains(MembershipHelper.CurrentUser.ID) = False Then
                                If task.TaskState = TaskStates.Ejecucion OrElse task.TaskState = TaskStates.Servicio Then
                                    BtnIniciar.Visible = False
                                Else
                                    SetAsignedTo()
                                End If
                            Else
                                If task.TaskState = TaskStates.Servicio Then
                                    BtnIniciar.Visible = False
                                Else
                                    SetAsignedTo()
                                End If
                            End If
                        End If
                    Else
                        DisableGUI("Tarea en Ejecucion por: " & currentLockedUser)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

            End If

        Else
            DisableGUI("No tiene permisos suficientes para iniciar la tarea")
        End If
    End Sub

    Private Sub DisableGUI(ByVal message As String)
        BtnIniciar.Visible = False
        dontLoadUAC = True
        CboStates.Enabled = False
        lblmsj.InnerHtml = message
        Dim script As String = "$(function (){$('#dropOptions').hide();}); "
        ScriptManager.RegisterClientScriptBlock(Me, Page.GetType(), "CloseOptionsScript", script, True)
    End Sub

    Private Sub GenerateUserActions()
        SetAsignedTo()
        GetStatesOfTheButtonsRule()
    End Sub
#End Region

#Region "Finalizar"
    Private Sub btnTerminar_Click()
        Dim sRights As New SRights()
        Dim sTasks As New STasks()

        Try
            If TaskResult IsNot Nothing Then
                WFTaskBusiness.UnLockTask(TaskResult.TaskId)
            End If

            If chkTakeTask.Checked = False Then
                TaskResult.TaskState = Zamba.Core.TaskStates.Asignada
                TaskResult.AsignedToId = _user.ID
            ElseIf sRights.GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, CInt(TaskResult.StepId)) Then
                TaskResult.TaskState = Zamba.Core.TaskStates.Desasignada
                TaskResult.AsignedToId = 0
            Else
                TaskResult.TaskState = Zamba.Core.TaskStates.Asignada
                TaskResult.AsignedToId = _user.ID
            End If

            sTasks.Finalizar(TaskResult)
            ExecuteFinishRules()
            'Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByTaskId(TaskResult.TaskId)
            SetAsignedTo()
            LoadUserAction()
            GetStatesOfTheButtonsRule()
            EnablePropietaryControls()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            sRights = Nothing
            sTasks = Nothing
        End Try
    End Sub

    Private Sub ExecuteFinishRules()
        Try
            Dim Results As New System.Collections.Generic.List(Of Zamba.Core.ITaskResult)
            Results.Add(TaskResult)

            For Each Rule As Zamba.Core.WFRuleParent In TaskResult.WfStep.Rules
                If Rule.RuleType = TypesofRules.Terminar Then
                    Dim SRules As New SRules()
                    SRules.ExecuteRule(Rule, Results)
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Derivar"
    Private Sub btnDerivar_Click()
        'Me.pnlAsign.Visible = True
        Try
            Dim SSteps As SSteps = New SSteps()
            'falta hacer el control de derivar y llamarlo con lightbox
            '            ucAssign = New UCAsignar(Me.TaskResult, UCAsignar.AsignTypes.Asignar)

            'todo agregar validacion que se haya realizado una derivacion

            Try
                Me.lvwUsers.Items.Clear()
                Dim WfStepUsersIdsAndNames As Generic.List(Of IZBaseCore) = SSteps.GetStepUsersIdsAndNames(TaskResult.StepId)
                Dim Id As Int64 = 0
                Dim Name As String = String.Empty
                For Each u As IZBaseCore In WfStepUsersIdsAndNames
                    If Not u.ID = 0 Then
                        Dim i As New ListItem()
                        i.Value = u.ID.ToString
                        i.Text = u.Name.ToString
                        Me.lvwUsers.Items.Add(i)
                    End If
                Next

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            ExecuteDerivarRules()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Protected Sub BtnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAsignar.Click
        Dim stask As STasks = New STasks()
        Dim susers As SUsers = New SUsers()

        Try
            If Me.lvwUsers.SelectedItem.Selected = True Then
                'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
                stask.DeriveTask(TaskResult, TaskResult.StepId, Long.Parse(Me.lvwUsers.SelectedItem().Value), Me.lvwUsers.SelectedItem().Text, _user.ID, Now, True)
                susers.SaveAction(TaskResult.ID, ObjectTypes.ModuleWorkFlow, RightsType.DerivateTask, "Usuario Derivo La tarea")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        SetAsignedTo()
    End Sub


    Private Sub ExecuteDerivarRules()
        Try
            Dim Results As New System.Collections.Generic.List(Of Zamba.Core.ITaskResult)
            Results.Add(TaskResult)
            For Each Rule As Zamba.Core.WFRuleParent In TaskResult.WfStep.Rules
                If Rule.RuleType = TypesofRules.Derivar Then
                    'Dim WFRB As New WFRulesBusiness()
                    Dim SRules As New SRules()
                    SRules.ExecuteRule(Rule, Results)
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Quitar"

    ''' <summary>
    ''' Quita la tarea del workflow.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 07/05/09 - Created
    ''' </history>
    Public Sub Quitar()
        'Dim WFTaskBusiness As New WFTaskBusiness(_user)
        Dim STasks As New STasks()
        STasks.RemoveTask(DirectCast(Me.TaskResult, Zamba.Core.TaskResult), False, _user)
        Response.Write("<script language='javascript'> { parent.CloseTask(" & Me.TaskResult.TaskId & ",true);}</script>")
    End Sub

#End Region

#End Region

    ''' <summary>
    ''' Carga los datos del encabezado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillHeader() Implements ITaskHeader.FillHeader
        _user = Session("User")

        Dim SUserPreferences As New SUserPreferences()
        Dim SUsers As New SUsers()
        Dim SSteps As New SSteps()

        lblAsignedTo.InnerHtml = SUsers.GetUserNamebyId(TaskResult.AsignedToId)
        lbletapadata.InnerHtml = SSteps.GetStepNameById(TaskResult.StepId)
        dtpFecVenc.Text = TaskResult.ExpireDate.ToShortDateString()

        CboStates.Items.Clear()
        CboStates.DataTextField = "Name"
        CboStates.DataValueField = "ID"
        CboStates.SelectedValue = Nothing

        '23/09/11: Se cambia la forma en que accede a la lista de estados por etapas.
        CboStates.DataSource = WFStepBusiness.GetStepById(TaskResult.StepId).States
        CboStates.DataBind()
        CboStates.SelectedValue = TaskResult.State.ID
        AddHandler CboStates.SelectedIndexChanged, AddressOf CboStates_SelectedIndexChanged
    End Sub

    Public Sub SetAsignedTo()
        Try
            UpdateGUITaskAsignedSituation(iTaskResult)
            EnablePropietaryControls()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UpdateGUITaskAsignedSituation(ByVal AsignedTaskResult As TaskResult)
        'Si la tarea esta asignada a algun usuario, va en negrita
        If AsignedTaskResult.AsignedToId <> 0 Then
            lblAsignedTo.InnerHtml = UserGroups.GetUserorGroupNamebyId(AsignedTaskResult.AsignedToId)
        Else
            lblAsignedTo.InnerHtml = "[Ninguno]"
        End If
    End Sub

    ''' <summary>
    ''' Habilitación y deshabilitación de controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    01/09/2008  Modified    Llamada al método loadWfStepRules
    '''     [Gaston]    08/01/2009  Modified    Llamada al método que permite ver si la etapa tiene o no permiso para habilitar o deshabilitar el
    '''                                         combo de estados
    ''' </history>
    Private Sub EnablePropietaryControls()
        Dim EnableIniciar As Boolean = True
        Dim SRights As New SRights()
        Dim SUserPreferences As New SUserPreferences()
        Dim users As Generic.List(Of Long)

        Try
            'Si el id es un grupo, users tendra los usuarios del mismo, caso contrario se encontrara vacio
            users = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId, True)

            'Estado de tarea
            If (TaskResult.TaskState = TaskStates.Desasignada AndAlso TaskResult.AsignedToId <> 0) Then
                TaskResult.TaskState = TaskStates.Asignada
            End If
            If TaskResult.AsignedToId = 0 Then
                TaskResult.TaskState = TaskStates.Desasignada
            End If

            'Iniciar
            BtnIniciar.Visible = GetBtnIniciarVisibility(SRights, users)

            'Acciones de usuario
            If CheckUserActionLoad(users) Then
                LoadUserAction()
            Else
                HideFormRules()
            End If

            'Habilitacion de opciones y combo de estados de etapa
            If (TaskResult.TaskState = Zamba.Core.TaskStates.Ejecucion) Then
                If (SRights.GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.AllowStateComboBox, TaskResult.StepId) = True) Then
                    CboStates.Enabled = True
                Else
                    CboStates.Enabled = False
                End If

                If TaskResult.AsignedToId = MembershipHelper.CurrentUser.ID Then
                    Me.chkTakeTask.Enabled = True
                Else
                    Me.chkTakeTask.Enabled = False
                End If
                Me.chkCloseTaskAfterDistribute.Enabled = True
            Else
                Me.CboStates.Enabled = False
                Me.dtpFecVenc.Enabled = False
                Me.chkTakeTask.Enabled = False
                Me.chkCloseTaskAfterDistribute.Enabled = False
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            SRights = Nothing
            SUserPreferences = Nothing
            users = Nothing
        End Try
    End Sub

    Private Sub HideFormRules()
        Dim script As String = "$(function() { $('input[id^=zamba_rule_]').hide(); });"
        Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "HideFormRules", script, True)
    End Sub

    'TODO: MOVER A CAPA DE NEGOCIOS
    Private Function GetBtnIniciarVisibility(ByVal srights As SRights, ByVal users As List(Of Long)) As Boolean
        If srights.GetUserRights(ObjectTypes.WFSteps, RightsType.Iniciar, CInt(TaskResult.StepId)) AndAlso _
            (TaskResult.TaskState = TaskStates.Desasignada OrElse _
             (TaskResult.TaskState = TaskStates.Asignada AndAlso _
              srights.GetUserRights(ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) AndAlso _
              (_user.ID <> TaskResult.AsignedToId OrElse _
               users.Contains(MembershipHelper.CurrentUser.ID)))) Then
            Return True
        Else
            Return False
        End If
    End Function

    'TODO: MOVER A CAPA DE NEGOCIOS
    Private Function CheckUserActionLoad(ByVal users As List(Of Long)) As Boolean
        If Not dontLoadUAC AndAlso _
            iTaskResult IsNot Nothing AndAlso _
            iTaskResult.TaskState = TaskStates.Ejecucion AndAlso _
            (iTaskResult.m_AsignedToId = MembershipHelper.CurrentUser.ID OrElse _
                iTaskResult.m_AsignedToId = 0 OrElse _
                users.Contains(MembershipHelper.CurrentUser.ID) OrElse _
                GetExecuteAssignedToOtherRight()) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckUserActionLoad() As Boolean
        Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId, True)
        Return CheckUserActionLoad(users)
    End Function

    'TODO: MOVER A CAPA DE NEGOCIOS
    Private Function GetExecuteAssignedToOtherRight() As Boolean
        Return UserBusiness.Rights.GetUserRights(MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, TaskResult.StepId)
    End Function

    ''' <summary>
    ''' Habilita o deshabilita los botones basicos de la tarea
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisablePropertyControls()
        Me.BtnDerivar.Visible = RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Delegates, CInt(TaskResult.StepId))
        Me.BtnRemove.Visible = RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Delete, CInt(TaskResult.StepId))
        Me.deleteCtrl.Visible = (Boolean.Parse(UserPreferences.getValue("ShowDeleteButton", Sections.UserPreferences, "True")) AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.Delete, TaskResult.DocTypeId))

        If Me.deleteCtrl.Visible AndAlso deleteCtrl.Result Is Nothing Then
            deleteCtrl.Result = TaskResult
        End If

        liFecVenc.Visible = Boolean.Parse(UserPreferences.getValue("LabelDateExpireVisible", Sections.WorkFlow, "False"))
        If liFecVenc.Visible Then
            Me.dtpFecVenc.Enabled = RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.ModificarVencimiento, CInt(TaskResult.StepId))
        End If

        If TaskResult.TaskState <> TaskStates.Ejecucion Then
            CboStates.Enabled = False
            Me.dtpFecVenc.Enabled = False
            Me.chkTakeTask.Enabled = False
            Me.chkCloseTaskAfterDistribute.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Carga las acciones de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadUserAction()
        Try
            Dim SRules As SRules = New SRules()
            Dim userActionName As String = String.Empty
            idRules.Clear()
            UACCell.Controls.Clear()

            'todo wf falta ver si no se modificaron las reglas y cargarlas de nuevo desde la base en el wfstep
            'ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargando las acciones de usuario.")
            Dim Rules As List(Of Zamba.Core.IWFRuleParent) = SRules.GetCompleteHashTableRulesByStep(TaskResult.StepId)

            If Not Rules Is Nothing Then
                Dim WFRB As New WFRulesBusiness
                Dim RuleEnabled As Boolean

                For Each Rule As Zamba.Core.WFRuleParent In Rules
                    If TaskResult.UserRules.ContainsKey(Rule.ID) Then
                        'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        'y en la 1 si se acumula a la habilitacion de las solapas o no
                        Dim lstRulesEnabled As List(Of Boolean) = DirectCast(TaskResult.UserRules(Rule.ID), List(Of Boolean))
                        RuleEnabled = lstRulesEnabled(0)
                    Else
                        RuleEnabled = True
                    End If

                    If RuleEnabled AndAlso Rule.ParentType = TypesofRules.AccionUsuario AndAlso Not idRules.Contains(Rule.ID) Then
                        Dim UAB As New Button
                        Dim li As New HtmlGenericControl("li")
                        li.Attributes.Add("style", "padding:1px")

                        UAB.ID = "UAB_Rule_" & Rule.ID
                        UAB.CssClass = "btn btn-primary btn-xs"
                        UAB.OnClientClick = "ShowLoadingAnimation();"
                        UAB.TabIndex = -1

                        AddHandler UAB.Click, AddressOf UAB_Click

                        'Busca en la tabla si existe un nombre de acción de usuario para esa regla
                        Try
                            userActionName = SRules.GetUserActionName(Rule)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                            userActionName = String.Empty
                        End Try

                        'Si el nombre no existe entonces le asigna el nombre de la regla
                        If String.IsNullOrEmpty(userActionName) Then
                            userActionName = Rule.Name.ToUpper
                        End If

                        'Asigna el nombre al botón. Si este es mayor que 20 lo corta y le agrega 3 puntos
                        UAB.ToolTip = userActionName

                        If userActionName.Length > 20 Then
                            UAB.Text = userActionName.Substring(0, 20) & "..."
                        Else
                            UAB.Text = userActionName
                        End If

                        'Guarda el nombre en un hash para luego utilizarlo cuando se llame al saveAction
                        If hshRulesNames.ContainsKey(Rule.ID) Then
                            hshRulesNames.Item(Rule.ID) = userActionName
                        Else
                            hshRulesNames.Add(Rule.ID, userActionName)
                        End If

                        li.Controls.Add(UAB)
                        Me.UACCell.Controls.Add(li)

                        'Se guarda el id de la regla
                        idRules.Add(Rule.ID)
                    End If
                Next

                userActionName = String.Empty
                WFRB = Nothing

                'Oculta/muestra reglas segun preferencias por cada result
                GetStatesOfTheButtonsRule()

            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub GetStatesOfTheButtonsRule()
        Try
            Dim SRules As New SRules()
            Dim SSteps As New SSteps()
            Dim SUsers As New SUsers()
            Dim SWorkflow As New SWorkflow()
            Dim contador As Int32 = 0
            'Dice si se va a usar el enable del tab o no
            Dim useTabEnable As Boolean

            If UACCell.Controls.Count > 0 Then

                'Recorre cada regla activa en el documento
                For Each idRule As Int64 In idRules
                    useTabEnable = True

                    'Si la regla no fue procesada antes por la DoEnable
                    If TaskResult.UserRules.Contains(idRule) Then
                        'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        'y en la 1 si se acumula a la habilitacion de las solapas o no
                        Dim lstRulesEnabled As List(Of Boolean) = DirectCast(TaskResult.UserRules(idRule), List(Of Boolean))
                        'Si la regla esta deshabilitada, no uso los estados de los tabs
                        If lstRulesEnabled(0) Then
                            'Si no esta marcada la opcion de ejecucion conjunta con los tabs, no uso los estados
                            If lstRulesEnabled(1) = False Then
                                useTabEnable = False
                            End If
                        Else
                            useTabEnable = False
                        End If
                    End If

                    'Si utilizo los tabs (porq no uso la doenable o porq la ejecucion es conjunta)
                    If useTabEnable Then
                        UACCell.Controls(contador).Visible = WFBusiness.CanExecuteRules(idRule, _user.ID, TaskResult.State, TaskResult)
                    End If
                    contador = contador + 1
                Next
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta una accion de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub UAB_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim RuleId As Long
            Dim UAB As New Button
            Dim results As New List(Of Zamba.Core.ITaskResult)

            UAB = DirectCast(sender, Button)

            If Not UAB Is Nothing Then
                Session("ExecutingRule") = RuleId

                If Long.TryParse(UAB.ID.Replace("UAB_Rule_", String.Empty), RuleId) Then
                    results.Add(TaskResult)
                    WFTaskBusiness.LogUserAction(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, hshRulesNames.Item(Convert.ToInt64(Split(sender.id, "_")(2))).ToString)
                    RaiseEvent ExecuteRule(RuleId, results)
                End If
            End If

            LoadUserAction()
        Catch ex As Exception
            lblmsj.InnerHtml = "Ha ocurrido un error en la ejecucion de la regla " & sender.text & " , contactese con el administrador del sistema para mas informacion"
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que ejecuta reglas
    ''' </summary>
    ''' <param name="ruleId">ID de la regla a ejecutar</param>
    ''' <param name="results">Tareas a ejecutar</param>
    ''' <remarks></remarks>
    Public Event ExecuteRule(ByVal ruleId As Int64, ByVal results As List(Of Zamba.Core.ITaskResult))

    ''' <summary>
    ''' Se cambio el estado de la tarea desde el combo de estados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CboStates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboStates.SelectedIndexChanged, CboStates.TextChanged
        Try
            'update the change state
            Dim StateID As Integer = Val(DirectCast(sender, DropDownList).SelectedValue)
            WFTasksFactory.UpdateState(TaskResult.TaskId, TaskResult.StepId, StateID)

            'log in the change state
            WFTasksFactory.LogChangeStepState(TaskResult.ID)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Cambiar el estado
    ''' </summary>
    ''' <param name="isExecuteSetStateRule"></param>
    ''' <remarks></remarks>
    Public Sub SetState(ByVal isExecuteSetStateRule As Boolean)
        Try
            RemoveHandler CboStates.SelectedIndexChanged, AddressOf CboStates_SelectedIndexChanged
            If TaskResult.State.ID > 0 Then
                For i As Int32 = 0 To CboStates.Items.Count - 1
                    Me.CboStates.SelectedIndex = i
                    If CboStates.SelectedValue = TaskResult.State.ID.ToString Then
                        Exit For
                    End If
                Next
            End If

            AddHandler CboStates.SelectedIndexChanged, AddressOf CboStates_SelectedIndexChanged

            If isExecuteSetStateRule Then
                ExecutedSetState()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ExecuteAsignedToRules()
        Try

            Dim Results As New System.Collections.Generic.List(Of ITaskResult)
            Results.Add(TaskResult)

            loadWfStepRules(TaskResult.StepId, TaskResult.WfStep.Rules)

            Dim WFRB As New WFRulesBusiness()
            For Each Rule As WFRuleParent In TaskResult.WfStep.Rules
                If Rule.RuleType = TypesofRules.Asignar Then
                    WFRB.ExecuteRule(Rule, Results)
                End If
            Next
            WFRB = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub ExecutedSetState()

        Try
            Dim Results As New System.Collections.Generic.List(Of ITaskResult)
            Results.Add(TaskResult)
            loadWfStepRules(TaskResult.StepId, TaskResult.WfStep.Rules)

            For Each Rule As WFRuleParent In TaskResult.WfStep.Rules
                If Rule.RuleType = TypesofRules.Estado Then
                    Dim WFRB As New WFRulesBusiness()
                    WFRB.ExecuteRule(Rule, Results)
                End If
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub loadWfStepRules(ByRef stepId As Long, ByRef rules As List(Of IWFRuleParent))
        ' Si la colección de reglas es Nothing o rules no posee elementos
        If (IsNothing(rules) OrElse (rules.Count = 0)) Then
            Dim WFRulesBusiness As New WFRulesBusiness()
            rules = WFRulesBusiness.GetCompleteHashTableRulesByStep(stepId)
            WFRulesBusiness = Nothing
        End If
    End Sub

    Protected Sub dtpFecVenc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFecVenc.TextChanged
        Try
            Dim FecVenc As String = dtpFecVenc.Text

            'update the change state
            If Not IsNothing(TaskResult) Then
                WFTasksFactory.UpdateExpiredDate(TaskResult.TaskId, FecVenc)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Este es un fix para guardar y obtener correctamente el valor del checkbox de Finalizar tarea al cerrar.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkTakeTask_Load(sender As Object, e As EventArgs)
        Try
            If Page.IsPostBack Then
                If chkTakeTask.Enabled Then
                    Dim value As Boolean

                    'Solo se guarda el valor cuando es diferente del hidden
                    If Not String.IsNullOrEmpty(hdnTakeTaskFix.Value) AndAlso Boolean.TryParse(hdnTakeTaskFix.Value, value) Then
                        If value <> chkTakeTask.Checked Then
                            SetOption(chkTakeTask.Checked, "CheckFinishTaskAfterClose")
                            hdnTakeTaskFix.Value = chkTakeTask.Checked.ToString()
                        End If
                    End If
                End If
            Else
                'Carga inicial del checkbox
                Dim SUserPreferences As New SUserPreferences()
                Dim SRights As New SRights

                chkTakeTask.Visible = SRights.GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, CInt(TaskResult.StepId))
                If chkTakeTask.Visible Then
                    chkTakeTask.Checked = CBool(SUserPreferences.getValue("CheckFinishTaskAfterClose", Sections.WorkFlow, "True"))
                End If

                SRights = Nothing
                SUserPreferences = Nothing

                'Se guarda en un hidden para solucionar un problema de estados de checkbox
                hdnTakeTaskFix.Value = chkTakeTask.Checked.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(New Exception("Error al cargar o guardar el valor de CheckFinishTaskAfterClose", ex))
        End Try
    End Sub

    ''' <summary>
    ''' Este es un fix para guardar y obtener correctamente el valor del checkbox de Cerrar tarea al distribuir.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkCloseTaskAfterDistribute_Load(sender As Object, e As EventArgs)
        Try
            If Page.IsPostBack Then
                If chkCloseTaskAfterDistribute.Enabled Then
                    Dim value As Boolean

                    'Solo se guarda el valor cuando es diferente del hidden
                    If Not String.IsNullOrEmpty(hdnCloseTaskFix.Value) AndAlso Boolean.TryParse(hdnCloseTaskFix.Value, value) Then
                        If value <> chkCloseTaskAfterDistribute.Checked Then
                            SetOption(chkCloseTaskAfterDistribute.Checked, "CloseTaskAfterDistribute")
                            hdnCloseTaskFix.Value = chkCloseTaskAfterDistribute.Checked.ToString()
                        End If
                    End If
                End If
            Else
                'Carga inicial del checkbox
                Dim SUserPreferences As New SUserPreferences()
                chkCloseTaskAfterDistribute.Checked = CBool(SUserPreferences.getValue("CloseTaskAfterDistribute", Sections.WorkFlow, "False"))
                SUserPreferences = Nothing

                'Se guarda en un hidden para solucionar un problema de estados de checkbox
                hdnCloseTaskFix.Value = chkCloseTaskAfterDistribute.Checked.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(New Exception("Error al cargar o guardar el valor de CloseTaskAfterDistribute", ex))
        End Try
    End Sub

    Private Sub SetOption(ByVal value As Boolean, ByVal checkOption As String)
        Dim sup As SUserPreferences

        Try
            sup = New SUserPreferences()
            sup.setValue(checkOption, value, Sections.WorkFlow)
            sup = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            sup = Nothing
        End Try
    End Sub
End Class


