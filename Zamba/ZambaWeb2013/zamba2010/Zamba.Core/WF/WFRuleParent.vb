Imports System.Collections.Generic
Imports Zamba.Core.Enumerators
Imports System.Text.RegularExpressions
Imports Zamba.Core

<RuleDescription("Regla Padre"), RuleHelp("Clase de la que heredan todas las reglas")> <Serializable()>
Public MustInherit Class WFRuleParent
    Inherits ZambaCore
    Implements IWFRuleParent, IRule

#Region "Atributos"
    Private _parentType As TypesofRules
    Private _RuleType As TypesofRules
    Private _wFStepId As Int64
    Private _parentRule As IRule = Nothing
    Private _rulenode As IRuleNode = Nothing
    ' Private _childRules As List(Of IRule) = Nothing
    Private _version As Int32
    Private _oldStateEnable As Boolean
    Private _enable As Boolean
    Private _oldStateTrue As Boolean
    Private _isUI As Boolean
    Private _ExecuteWhenResult As Nullable(Of Boolean) = Nothing
    Private _AlertExecution As Nullable(Of Boolean) = Nothing
    'Private _WFStep As IWFStep
    '[Ezequiel] 30/03/2009 Created
    Private _refreshRule As Nullable(Of Boolean)
    '[Ezequiel] 05/06/2009 Created
    Private _continueWithError As Nullable(Of Boolean)
    '[Ezequiel] 22/06/2009 Created
    Private _closeTask As Nullable(Of Boolean)
    '[Marcelo] 02/09/2009 Created
    Private _Help As String
    '[Tomas] 16/09/2009 Created
    Private _cleanRule As Nullable(Of Boolean)
    '[Ezequiel] 18/09/09 - Created
    Private _startTime As Date
    '[Ezequiel] 21/09/09 - Created
    'Private _traceTime As System.Diagnostics.TextWriterTraceListener
    '[Marcelo] 13/10/2010 Created.
    Private _Category As Nullable(Of Int16)
    '[Marcelo] 26/11/2010 Created
    Private _SaveUpdate As Nullable(Of Boolean)
    '[Marcelo] 26/11/2010 Created
    Private _UpdateComment As String
    '[Marcelo] 14/12/2010 Created
    Private _SaveUpdateInHistory As Nullable(Of Boolean)
    '[Marcelo] 03/01/2011 Created
    Private _Asynchronous As Nullable(Of Boolean)
    Private _IsAsync As Nullable(Of Boolean) = False
    '[AlejandroR] 13/01/2011 Created
    Private _ExecuteRuleInCaseOfError As Nullable(Of Boolean)
    '[AlejandroR] 13/01/2011 Created
    Private _MessageToShowInCaseOfError As String
    '[AlejandroR] 13/01/2011 Created
    Private _RuleIdToExecuteAfterError As Nullable(Of Integer)
    '[AlejandroR] 13/01/2011 Created
    Private _ThrowExceptionIfCancel As Nullable(Of Boolean)
    '[AlejandroR] 28/02/2011 Created
    Private _DisableChildRules As Nullable(Of Boolean)
    Private _comment As String
#End Region

#Region "Propiedades"
    Public Property ParentType() As TypesofRules Implements IWFRuleParent.ParentType
        Get
            Return _parentType
        End Get
        Set(ByVal Value As TypesofRules)
            _parentType = Value
        End Set
    End Property
    Public Property RuleType() As TypesofRules Implements IWFRuleParent.RuleType
        Get
            Return _RuleType
        End Get
        Set(ByVal Value As TypesofRules)
            _RuleType = Value
        End Set
    End Property
    Public Property WFStepId() As Int64 Implements IWFRuleParent.WFStepId
        Get
            Return _wFStepId
        End Get
        Set(ByVal Value As Int64)
            _wFStepId = Value
        End Set
    End Property
    Public Property ParentRule() As IRule Implements IWFRuleParent.ParentRule
        Get
            Return _parentRule
        End Get
        Set(ByVal Value As IRule)
            _parentRule = Value
        End Set
    End Property
    Public Property RuleNode() As IRuleNode Implements IWFRuleParent.RuleNode
        Get
            If IsNothing(_rulenode) Then _rulenode = New RuleNode(Nothing)

            Return _rulenode
        End Get
        Set(ByVal Value As IRuleNode)
            _rulenode = Value
        End Set
    End Property
    'Public Property ChildRules() As List(Of IRule) Implements IWFRuleParent.ChildRules
    '    Get
    '        If IsNothing(_childRules) Then _childRules = New List(Of IRule)

    '        Return _childRules
    '    End Get
    '    Set(ByVal Value As List(Of IRule))
    '        _childRules = Value
    '    End Set
    'End Property
    Public Property ChildRulesIds As List(Of Int64) Implements IWFRuleParent.ChildRulesIds
    Public ReadOnly Property RuleClass() As String Implements IWFRuleParent.RuleClass
        Get
            Return Me.GetType.Name.ToString
        End Get
    End Property
    Public Property Enable() As Boolean Implements IWFRuleParent.Enable
        Get
            Return _enable
        End Get
        Set(ByVal Value As Boolean)
            _enable = Value
        End Set
    End Property
    Public Property OldStateEnable() As Boolean Implements IWFRuleParent.OldStateEnable
        Get
            Return _oldStateEnable
        End Get
        Set(ByVal Value As Boolean)
            _oldStateEnable = Value
        End Set
    End Property
    Public Property OldStateTrue() As Boolean Implements IWFRuleParent.OldStateTrue
        Get
            Return _oldStateTrue
        End Get
        Set(ByVal Value As Boolean)
            _oldStateTrue = Value
        End Set
    End Property
    Public Property IsUI() As Boolean Implements IWFRuleParent.IsUI
        Get
            Return _isUI
        End Get
        Set(ByVal Value As Boolean)
            _isUI = Value
        End Set
    End Property
    Public Property Version() As Int32 Implements IWFRuleParent.Version
        Get
            Return _version
        End Get
        Set(ByVal Value As Int32)
            _version = Value
        End Set
    End Property

    Public Property AlertExecution() As Nullable(Of Boolean) Implements IRule.AlertExecution
        Get
            If _AlertExecution.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _AlertExecution
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _AlertExecution = value
        End Set
    End Property


    Public Property ExecuteWhenResult() As Nullable(Of Boolean) Implements IRule.ExecuteWhenResult
        Get
            If _ExecuteWhenResult.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _ExecuteWhenResult
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _ExecuteWhenResult = value
        End Set
    End Property

    Public Property RefreshRule() As Nullable(Of Boolean) Implements IRule.RefreshRule
        Get
            If _refreshRule.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _refreshRule
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _refreshRule = value
        End Set
    End Property

    Public Property ContinueWithError() As Nullable(Of Boolean) Implements IRule.ContinueWithError
        Get
            If _continueWithError.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
                '_continueWithError = False
            End If
            Return _continueWithError
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _continueWithError = value
        End Set
    End Property

    ''' <summary>
    ''' Define si se cierra la tarea al finalizar la ejecucion de la regla.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 22/06/09 - Created
    Public Property CloseTask() As Nullable(Of Boolean) Implements IRule.CloseTask
        Get
            If _closeTask.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
                '_closeTask = False
            End If
            Return _closeTask
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _closeTask = value
        End Set
    End Property

    Public Property StartTime() As Date Implements IRule.StartTime
        Get
            Return _startTime
        End Get
        Set(ByVal value As Date)
            _startTime = value
        End Set
    End Property

    ''' <summary>
    ''' Define la ayuda de la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Marcelo] 02/09/09 - Created
    Public Property Help() As String Implements IRule.Description
        Get
            Return _Help
        End Get
        Set(ByVal value As String)
            _Help = value
        End Set
    End Property

    ''' <summary>
    ''' Limpia la memoria de la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Tomas] 16/09/2009 - Created
    Public Property Category() As Nullable(Of Int16) Implements IRule.Category
        Get
            If _Category Is Nothing Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _Category
        End Get
        Set(ByVal value As Nullable(Of Int16))
            _Category = value
        End Set
    End Property

    ''' <summary>
    ''' Categoria de la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Tomas] 16/09/2009 - Created
    Public Property CleanRule() As Nullable(Of Boolean) Implements IRule.CleanRule
        Get
            If _cleanRule.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
                '_cleanRule = False
            End If
            Return _cleanRule
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _cleanRule = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda datos actualizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [Marcelo] 26/11/2010 Created </History>
    Public Property SaveUpdate() As Nullable(Of Boolean) Implements IRule.SaveUpdate
        Get
            If _SaveUpdate.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _SaveUpdate
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _SaveUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' Valor comentario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [Marcelo] 26/11/2010 Created </History>
    Public Property UpdateComment() As String Implements IRule.UpdateComment
        Get
            Return _UpdateComment
        End Get
        Set(ByVal value As String)
            _UpdateComment = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda datos actualizacion en historial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [Marcelo] 14/12/2010 Created </History>
    Public Property SaveUpdateInHistory() As Nullable(Of Boolean) Implements IRule.SaveUpdateInHistory
        Get
            If _SaveUpdateInHistory.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _SaveUpdateInHistory
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _SaveUpdateInHistory = value
        End Set
    End Property

    ''' <summary>
    ''' Execute the rule in asynchronous mode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [Marcelo] 03/01/2011 Created </History>
    Public Property Asynchronous() As Nullable(Of Boolean) Implements IRule.Asynchronous
        Get
            If _Asynchronous.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _Asynchronous
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _Asynchronous = value
        End Set
    End Property

    Public Property IsAsync() As Nullable(Of Boolean) Implements IRule.IsAsync
        Get
            If _IsAsync.HasValue = False Then
                _IsAsync = False
            End If
            Return _IsAsync
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _IsAsync = value
        End Set
    End Property

    ''' <summary>
    ''' Set if a specific rule is executed in case of error
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [AlejandroR] 13/01/2011 Created </History>
    Public Property ExecuteRuleInCaseOfError() As Nullable(Of Boolean) Implements IRule.ExecuteRuleInCaseOfError
        Get
            If _ExecuteRuleInCaseOfError.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _ExecuteRuleInCaseOfError
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _ExecuteRuleInCaseOfError = value
        End Set
    End Property

    ''' <summary>
    ''' Message to show in case of error in WF execution
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [AlejandroR] 13/01/2011 Created </History>
    Public Property MessageToShowInCaseOfError() As String Implements IRule.MessageToShowInCaseOfError
        Get
            Return _MessageToShowInCaseOfError
        End Get
        Set(ByVal value As String)
            _MessageToShowInCaseOfError = value
        End Set
    End Property

    ''' <summary>
    ''' Rule to play in case of error in WF execution
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [AlejandroR] 13/01/2011 Created </History>
    Public Property RuleIdToExecuteAfterError() As Nullable(Of Integer) Implements IRule.RuleIdToExecuteAfterError
        Get
            If _RuleIdToExecuteAfterError.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _RuleIdToExecuteAfterError
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _RuleIdToExecuteAfterError = value
        End Set
    End Property

    ''' <summary>
    ''' If true, when a rule is cancelled by the user an exception is thrown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [AlejandroR] 17/01/2011 Created </History>
    Public Property ThrowExceptionIfCancel() As Nullable(Of Boolean) Implements IRule.ThrowExceptionIfCancel
        Get
            If _ThrowExceptionIfCancel.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _ThrowExceptionIfCancel
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _ThrowExceptionIfCancel = value
        End Set
    End Property

    ''' <summary>
    ''' If true, when a rule is cancelled by the user an exception is thrown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History> [AlejandroR] 28/02/2011 Created </History>
    Public Property DisableChildRules() As Nullable(Of Boolean) Implements IRule.DisableChildRules
        Get
            If _DisableChildRules.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _DisableChildRules
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _DisableChildRules = value
        End Set
    End Property
#End Region

#Region "Constructores"
    ''' <summary>
    ''' Base Rule Class
    ''' </summary>
    ''' <param name="id">Id of the rule</param>
    ''' <param name="name">Rule name</param>
    ''' <param name="wfStepId">StepId of the rule</param>
    ''' <history>
    '''         Marcelo Modified    01/10/2009  Add call to load rule preferences
    ''' </history>
    ''' <remarks></remarks>
    Protected Sub New(ByVal id As Int64, ByVal name As String, ByVal wfStepId As Int64)
        Me.ID = id
        Me.Name = name
        Me.WFStepId = wfStepId
        Me.IconId = 31
        Me.RuleType = TypesofRules.Regla

        'Marcelo: call event for load RulesPreferences
        ZBaseCore.CallLoadRulePreference(Me)
    End Sub
    'Protected Sub New(ByVal Id As Int64, ByVal Name As String, ByRef WFStep As WFStep, ByVal RuleType As TypesofRules, ByVal ZConditionParams As ArrayList)
    '    Me.New(Id, Name, WFStep, RuleType)
    '    Me.ZConditionParams = ZConditionParams
    'End Sub
    'Private Sub New()
    '    Me.New(0, String.Empty, 0)
    'End Sub
#End Region

    'Metodo Generico para ejecutar la regla
    'Public MustOverride Function Play(ByVal results As System.Collections.SortedList) As System.Collections.SortedList Implements IWFRuleParent.Play
    Public MustOverride Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult) Implements IWFRuleParent.Play
    Public MustOverride Function PlayWeb(ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult) Implements IWFRuleParent.PlayWeb

    ''' <summary>
    ''' Execute a Rule
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="taskBusiness"></param>

    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     Marcelo  03/01/2011  Created       
    ''' </history>
    Public Overloads Function ExecuteRule(ByVal results As List(Of ITaskResult), ByVal taskBusiness As IWFTaskBusiness, ByVal ruleBusiness As IWFRuleBusiness, ByVal IsAsync As Boolean) As List(Of ITaskResult) Implements IWFRuleParent.ExecuteRule
        'Execute the rule in Asynchronous mode
        If Me.Asynchronous = True Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea se ejecuta en modo asincronico")
            Try
                Dim T1 As New Threading.Thread(AddressOf ExecuteRuleAsync)
                Dim params As New ArrayList(3)
                params.Add(results)
                params.Add(taskBusiness)
                params.Add(ruleBusiness)
                params.Add(IsAsync)
                T1.Start(params)
            Catch ex As Threading.SynchronizationLockException
            Catch ex As Threading.ThreadAbortException
            Catch ex As Threading.ThreadInterruptedException
            Catch ex As Threading.ThreadStateException
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return results
        Else
            Return ExecuteRuleSynchronical(results, taskBusiness, ruleBusiness, IsAsync)
        End If
    End Function


    Public Event RuleExecuted(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))

    Public Event RuleExecutedError(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))

    ''' <summary>
    '''     Execute a Rule
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="taskBusiness"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  06/12/2010  Modified    Get a updated taks to update the UI    
    ''' </history>
    Public Function ExecuteRuleSynchronical(ByVal results As List(Of ITaskResult), ByVal taskBusiness As IWFTaskBusiness, ByVal ruleBusiness As IWFRuleBusiness, ByVal IsAsync As Boolean) As List(Of ITaskResult)
        Dim newresults As New List(Of ITaskResult)()

        '''ML: Solo llamo a la ejecucion si esta marcado ejecutar cuando hay resultados y hay al menos un resultado o bien no esta marcado ejecutar cuando hay resultados
        If (((ExecuteWhenResult.HasValue AndAlso ExecuteWhenResult) OrElse ExecuteWhenResult.HasValue = False) AndAlso Not results Is Nothing AndAlso results.Count > 0) OrElse (ExecuteWhenResult.HasValue AndAlso ExecuteWhenResult = False) Then

            Try
                If IsNothing(results) Then
                    If Me.ExecuteWhenResult.HasValue AndAlso ExecuteWhenResult = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La Regla Se Ejecuta Aunque no reciba Tareas")
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
            End Try


            'Try
            If (ChildRulesIds Is Nothing OrElse ChildRulesIds.Count = 0) Then
                ChildRulesIds = ruleBusiness.GetChildRulesIds(ID, Me.RuleClass, results)
            End If

            If Not IsNothing(results) AndAlso (results.Count > 0 OrElse ((results.Count = 0) AndAlso Me.ExecuteWhenResult = False)) Then

                '[AlejandroR] 01/03/2011 - se ejecuta solo si la regla esta habilitada 
                If Me.Enable Then

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "*******************************************************************")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Executing Rule: " & RuleClass & " " & Name & " " & ID & "  with results count: " & results.Count)


                    'SETEA EL USUARIO ASIGNADO SEGUN PREFERENCIA DE REGLA
                    Dim params As Hashtable = New Hashtable
                    params.Add("RuleId", Me.ID)

                    Try
                        newresults = Me.Play(results)
                        RaiseEvent RuleExecuted(Me, results)

                        If Not IsNothing(newresults) Then
                            If Me.RefreshRule Then
                                Dim i As Int32
                                For i = 0 To newresults.Count - 1
                                    'Se obtienen los datos de la tarea para tener los más actualizados
                                    Dim task As ITaskResult = taskBusiness.GetTaskByTaskIdAndDocTypeIdAndStepId(newresults(i).TaskId, newresults(i).DocTypeId, newresults(i).WfStep.ID, 0)

                                    If IsNothing(task) Then
                                        Dim stepId As Int64 = taskBusiness.GetStepIDByTaskId(newresults(i).TaskId)

                                        task = taskBusiness.GetTaskByTaskIdAndDocTypeIdAndStepId(newresults(i).TaskId, newresults(i).DocTypeId, stepId, 0)
                                    End If

                                    newresults(i) = task
                                Next
                            End If
                        End If

                        'Guarda ultima actualizacion
                        Dim BCHistoryID As Int64
                        If Me.SaveUpdate Then
                            Dim VarInterReglas As New VariablesInterReglas()
                            Me._comment = VarInterReglas.ReconocerVariables(Me.UpdateComment)
                            VarInterReglas = Nothing
                            If Not IsNothing(results) AndAlso results.Count > 0 Then
                                For Each Result As Result In results
                                    If Not IsNothing(Me.SaveUpdateInHistory) Then
                                        BCHistoryID = taskBusiness.SetLastUpdate(Result, _comment, Me.SaveUpdateInHistory, Membership.MembershipHelper.CurrentUser.ID, Me.Name)
                                    Else
                                        BCHistoryID = taskBusiness.SetLastUpdate(Result, _comment, False, Membership.MembershipHelper.CurrentUser.ID, Me.Name)
                                    End If
                                Next
                            End If
                            _comment = String.Empty
                        End If

                        'Limpieza de variables
                        If Me.CleanRule Then
                            VariablesInterReglas.clear()
                        End If

                        If Me.SaveUpdate Then
                            If VariablesInterReglas.ContainsKey("LastBCHistoryID") = False Then
                                VariablesInterReglas.Add("LastBCHistoryID", BCHistoryID)
                            Else
                                VariablesInterReglas.Item("LastBCHistoryID") = BCHistoryID
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Id de novedad: " & BCHistoryID)
                        End If

                        If Me.SaveUpdate Then
                            If VariablesInterReglas.ContainsKey("LastNewsID") = False Then
                                VariablesInterReglas.Add("LastNewsID", BCHistoryID)
                            Else
                                VariablesInterReglas.Item("LastNewsID") = BCHistoryID
                            End If

                        End If

                    Catch ex As Exception
                        Dim ex2string As String = "Error en regla: " & Me.Name + " ID:" + Me.ID.ToString & " - " & ex.ToString
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex2string)

                        If Me.ContinueWithError = False Then

                            If Me.ExecuteRuleInCaseOfError Then
                                If Me.RuleIdToExecuteAfterError.HasValue AndAlso Me.RuleIdToExecuteAfterError.Value > 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla despues del error: " + Me.RuleIdToExecuteAfterError.Value.ToString())
                                    Try
                                        Dim R As WFRuleParent = ruleBusiness.GetInstanceRuleById(Me.RuleIdToExecuteAfterError.Value)
                                        R.ParentRule = Me
                                        R.IsAsync = IsAsync
                                        'R.ExecuteRule(results, taskBusiness, ruleBusiness)
                                        If params.Contains("ErrorRuleId") Then
                                            params("ErrorRuleId") = Me.RuleIdToExecuteAfterError.Value
                                        Else
                                            params.Add("ErrorRuleId", Me.RuleIdToExecuteAfterError.Value)
                                        End If

                                        R.ExecuteWebRule(results, taskBusiness, ruleBusiness, RulePendingEvents.ExecuteErrorRule, RuleExecutionResult.PendingEventExecution, params, IsAsync)

                                    Catch e As Exception
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Error ejecutando regla despues del error: " + e.Message)
                                        ZClass.raiseerror(e)
                                    End Try

                                End If

                            End If

                            'Si el usuario cancelo la regla entonces no se muestra la exception 
                            'ya que es una forzada y no una real
                            If Not Me.ThrowExceptionIfCancel Then
                                RaiseEvent RuleExecutedError(Me, results)
                                If String.IsNullOrEmpty(Me.MessageToShowInCaseOfError) Then
                                    Throw New Exception("Ocurrió un error en la ejecución de reglas de la tarea." & vbCrLf & "Contactese con el administrador del sistema. " & ex2string, ex)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje de error mostrado al usuario: " + Me.MessageToShowInCaseOfError)
                                    Throw New Exception(Me.MessageToShowInCaseOfError, ex)
                                End If
                            Else
                                ZClass.raiseerror(ex)
                                newresults = Nothing
                            End If

                        Else
                            ZClass.raiseerror(ex)
                            RaiseEvent RuleExecutedError(Me, results)
                            newresults = results
                        End If

                    End Try

                Else
                    '[AlejandroR] 01/03/2011 
                    'si no se ejecuto la regla, se asignan los nuevos results para que los usen las reglas hijas, sino no se ejecutan
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla esta deshabilitada: " & Me.Name + " ID: " + Me.ID.ToString)
                    newresults = results
                End If


                'Comente esta linea porque hay reglas q no necesitan results para ejecutarse - MC
                'If newresults.Count > 0 Then
                If Not IsNothing(ChildRulesIds) AndAlso ChildRulesIds.Count > 0 AndAlso Not newresults Is Nothing AndAlso Not IsNothing(results) AndAlso Me.RuleClass.ToLower() <> "doforeach" Then
                    If (String.Compare(Me.RuleClass, "IfBranch", True) = 0 AndAlso newresults.Count = 0) Then
                        'No se ejecuta la cadena hija, porque no hay resultados que hayan cumplido con la condicion y tipo de condicion
                    Else
                        '[AlejandroR] 01/03/2011 - verificar si la regla tiene configurado que no se ejecuten las hijas
                        If Not Me.DisableChildRules Then

                            For Each RId As Int64 In ChildRulesIds
                                Dim R As WFRuleParent = ruleBusiness.GetInstanceRuleById(RId)
                                R.ParentRule = Me
                                R.IsAsync = IsAsync
                                Try
                                    Dim newchildresults As New List(Of ITaskResult)()
                                    newchildresults = R.ExecuteRule(newresults, taskBusiness, ruleBusiness, IsAsync)
                                    If IsNothing(newchildresults) Then
                                        newresults = Nothing
                                    Else
                                        'ML: Cancelacion de ifbranch proximo, si la cantidad de results devueltos por el primer ifbranch 
                                        'es igual al ingresado, es decir que todas las tareas cumplieron con la primer condicion.
                                        If String.Compare(R.RuleClass, "IfBranch", True) = 0 AndAlso newchildresults.Count = newresults.Count Then
                                            Exit For
                                        End If
                                    End If

                                Catch ex As Exception
                                    '[Ezequiel] 08/06/09 - Valido si debo cortar la ejecucion de la tarea
                                    ' en base a la regla que se esta ejecutando actualmente.
                                    If R.ContinueWithError = False Then
                                        Throw New Exception("Se finaliza la ejecución de la tarea debido a un error en la regla: " & R.Name + " ID: " + Me.ID.ToString, ex)
                                    Else
                                        ZClass.raiseerror(ex)
                                    End If
                                End Try
                            Next
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Las Reglas hijas estan deshabilitadas: " & Me.Name + " ID: " + Me.ID.ToString)
                        End If
                    End If
                End If
            End If
        Else
            newresults = results
        End If

        If CloseTask Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encuentra marcada la opcion de cerrado de tarea en regla: " & ID)

        End If


        Return newresults
    End Function

    ''' <summary>
    '''     Execute a Rule
    ''' </summary>
    ''' <param name="params">Object que contiene un array de 3 objetos</param>
    ''' <returns></returns>
    ''' <history>
    '''     Marcelo  03/01/2011  Created       
    ''' </history>
    Public Sub ExecuteRuleAsync(ByVal params As Object)
        ExecuteRuleSynchronical(DirectCast(params, ArrayList)(0), DirectCast(params, ArrayList)(1), DirectCast(params, ArrayList)(2), DirectCast(params, ArrayList)(3))

    End Sub




    ''' <summary>
    '''     Genera variables con nombres y los valores de la primer fila del dataset
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Alejandro  13/01/2011  Created       
    ''' </history>
    Public Shared Sub GeneratarVariablesDesdeDS(ByVal ds As DataSet)
        ''''''''''''''''''''''''''''''''''''''''''
        'WI 6096, si el ds tiene 1 o 0 filas se guardan las columnas en variables

        Dim NombreVar As String
        Dim ValorVar As String

        For Each dt As DataTable In ds.Tables

            If dt.Rows.Count <= 1 Then

                For Each col As DataColumn In dt.Columns

                    NombreVar = col.ColumnName.Replace(" ", "_")

                    If dt.Rows.Count = 0 Then
                        ValorVar = String.Empty
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " con valor: NULO x SIN REGISTROS")
                    Else
                        If IsDBNull(dt.Rows(0).Item(col.Ordinal)) Then
                            ValorVar = String.Empty
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " con valor: NULO")
                        ElseIf String.IsNullOrEmpty(dt.Rows(0).Item(col.Ordinal)) Then
                            ValorVar = String.Empty
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " con valor: NULO o VACIO")
                        Else
                            ValorVar = dt.Rows(0).Item(col.Ordinal).ToString
                        End If
                    End If

                    If VariablesInterReglas.ContainsKey(NombreVar) = False Then
                        VariablesInterReglas.Add(NombreVar, ValorVar)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " Creada valor: " & ValorVar)
                    Else
                        VariablesInterReglas.Item(NombreVar) = ValorVar
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " Guardada valor: " & ValorVar)
                    End If

                Next

            End If

        Next

    End Sub

    ''' <summary>
    ''' Obtiene el objeto de alguna variable
    ''' </summary>
    ''' <param name="TextoaValidar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerValorVariableObjeto(ByVal TextoaValidar As String) As Object
        Dim variable As String
        Dim ValorVariable As Object = Nothing
        If TextoaValidar <> String.Empty AndAlso TextoaValidar.ToLower().IndexOf("zvar") <> -1 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a buscar la variable: " & TextoaValidar)

            'ZTrace.WriteLineIf(ZTrace.IsInfo, "IndexOF: " & TextoaValidar.ToLower().Trim.IndexOf("zvar("))
            ZTrace.WriteLineIf(ZTrace.IsInfo, "IndexOF: " & TextoaValidar.ToLower().IndexOf("zvar("))
            Dim zvar As String = "zvar("
            'variable = TextoaValidar.Remove(0, TextoaValidar.ToLower().Trim.IndexOf("zvar(") + 5)
            variable = TextoaValidar.Remove(0, TextoaValidar.ToLower().IndexOf(zvar) + 5)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variable: " & variable)
            variable = variable.Remove(variable.IndexOf(")"))
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variable: " & variable)
            If variable.Contains("(") AndAlso variable.Substring(variable.Length - 1).CompareTo(")") <> 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variable: " & variable)
                variable &= ")"
            End If
        Else
            variable = TextoaValidar
        End If


        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable a obtener: " & variable)
        If (variable IsNot Nothing AndAlso variable <> String.Empty) AndAlso VariablesInterReglas.ContainsKey(variable) Then
            ValorVariable = VariablesInterReglas.Item(variable)
        Else
            If TextoaValidar.ToLower.Contains("(") = True Then
                Dim Value() As String
                Dim mander As Char = Char.Parse("(")
                Dim objValue As Object
                Dim drValue As DataRow

                Value = variable.Split(mander)
                objValue = VariablesInterReglas.Item(Value(0))


                If Not IsNothing(objValue) Then

                    If String.Compare(objValue.GetType().Name.ToLower, "datarow") = 0 Then
                        drValue = DirectCast(objValue, DataRow)
                        Dim numcol As Int32
                        'If Int32.TryParse(Value(1).ToString.Trim.Substring(0, Value(1).ToString.Length - 1), numcol) Then
                        If Int32.TryParse(Value(1).ToString.Substring(0, Value(1).ToString.Length - 1), numcol) Then
                            ValorVariable = drValue(numcol)
                        Else
                            ValorVariable = drValue(Value(1).ToString.Substring(0, Value(1).ToString.Length - 1))
                        End If
                    Else
                        ValorVariable = objValue
                    End If

                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la variable")
                    ValorVariable = New Object
                    ValorVariable = String.Empty
                    If variable.Contains("(") = False Then
                        VariablesInterReglas.Add(variable, ValorVariable)
                    End If

                End If
            Else
                ValorVariable = String.Empty
            End If
        End If
        variable = Nothing
        Return ValorVariable
    End Function



    Public Shared Function ObtenerNombreVariable(ByVal TextoaValidar As String) As String
        Dim variable As String
        If TextoaValidar <> String.Empty AndAlso TextoaValidar.ToLower().IndexOf("zvar") <> -1 Then
            If TextoaValidar.Trim.ToLower().IndexOf("zvarhtml(") > TextoaValidar.Trim.ToLower().IndexOf("zvar(") Then
                variable = "§html§" & TextoaValidar.Trim().Remove(0, TextoaValidar.Trim.ToLower().IndexOf("zvarhtml(") + 9)
            Else
                variable = TextoaValidar.Trim().Remove(0, TextoaValidar.Trim.ToLower().IndexOf("zvar(") + 5)
            End If
            variable = variable.Remove(variable.IndexOf(")"))
            If variable.Contains("(") AndAlso variable.Substring(variable.Length - 1).CompareTo(")") <> 0 Then
                variable &= ")"
            End If
        Else
            variable = TextoaValidar
        End If
        Return variable
    End Function

    Public Shared Function ReconocerVariablesValuesSoloTexto(ByVal TextoaValidar As String) As String
        TextoaValidar = TextoaValidar.Trim

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)

        TextoaValidar = ReconocerZambaDot(TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar").Replace("ZVAr", "zvar").Replace("ZVar", "zvar").Replace("ZVaR", "zvar").Replace("Zvar", "zvar").Replace("ZvAR", "zvar").Replace("ZvaR", "zvar").Replace("ZvAr", "zvar").Replace("zVAR", "zvar").Replace("zvAR", "zvar").Replace("zvaR", "zvar")

        Dim TextoReconocido As String = TextoaValidar
        Dim ValorVariable As Object

        Try

            If String.IsNullOrEmpty(TextoaValidar) Then
                Return String.Empty
            End If
            If TextoaValidar.ToLower().Contains("zvar") = False Then
                Return TextoaValidar
            End If

            While TextoaValidar.ToLower().Contains("zvar") Or TextoaValidar.ToLower().Contains("zvarhtml")
                Dim Variable As String
                Try
                    Dim isHtml As Boolean
                    Variable = WFRuleParent.ObtenerNombreVariable(TextoaValidar)
                    If Variable.StartsWith("§html§") Then
                        isHtml = True
                        Variable = Variable.Replace("§html§", "")
                    End If
                    Variable = "zvar(" & Variable & ")"

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable a reconocer: " & Variable)
                    If Variable <> String.Empty Then
                        Try
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                            ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)

                            If isHtml Then
                                Dim Simbols As String() = {"á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú", "ñ", "Ñ"}
                                Dim simbolReplacement As String() = {"&aacute;", "&eacute;", "&iacute;", "&oacute;", "&uacute;", "&Aacute;", "&Eacute;", "&Iacute;", "&Oacute;", "&Uacute;", "&ntilde;", "&Ntilde;"}

                                Dim textoSinAcentos As String = String.Empty
                                For Each caracter As String In ValorVariable
                                    If (Simbols.Contains(caracter)) Then
                                        textoSinAcentos = textoSinAcentos + (simbolReplacement(Simbols.Length - 1))
                                    Else
                                        textoSinAcentos = textoSinAcentos + (caracter)
                                    End If
                                Next

                                ValorVariable = textoSinAcentos
                            End If

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido: " & ValorVariable.ToString.Trim)
                            If isHtml Then
                                Variable = Variable.Replace("zvar(", "zvarhtml(")
                                TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                            Else
                                TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                            End If

                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Texto Reconocido: " & TextoReconocido)
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                        End Try
                    Else
                        Exit While
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, ex.ToString())
                Finally
                    Variable = Nothing
                End Try
            End While
        Finally
            ValorVariable = Nothing
        End Try

        Return TextoReconocido
    End Function

    Public Shared Function ReconocerZambaDot(TextoaValidar As String) As String
        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.id")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.id", Membership.MembershipHelper.CurrentUser.ID.ToString())
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.usuario")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.usuario", Membership.MembershipHelper.CurrentUser.Name)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.nombre")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.nombre", Membership.MembershipHelper.CurrentUser.Nombres)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.apellido")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.apellido", Membership.MembershipHelper.CurrentUser.Apellidos)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.mail")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.mail", Membership.MembershipHelper.CurrentUser.eMail.Mail)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.fechaactual")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.fechaactual", DateTime.Now.ToShortDateString())
        End If

        If (TextoaValidar.ToLower().Contains("zamba.temp")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.temp", Membership.MembershipHelper.AppTempPath)
        End If

        Return TextoaValidar
    End Function
    Public Shared Function ReconocerVariablesValuesSoloTextoAsHashTB(ByVal TextoaValidar As String) As Hashtable
        Dim R As String = String.Empty
        'Dim TextoReconocido As String = TextoaValidar
        Dim ValorVariable As Object
        Dim cambios As New Hashtable()

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconocer Variables" & vbCrLf & "Texto a Validar: " & TextoaValidar)
        If String.IsNullOrEmpty(TextoaValidar) Then
            Return cambios
        End If
        If TextoaValidar.ToLower().Contains("zvar") = False Then
            Return cambios
        End If

        While TextoaValidar.ToLower().Contains("zvar")
            Try
                Dim Variable As String = WFRuleParent.ObtenerNombreVariable(TextoaValidar)
                Variable = "zvar(" & Variable & ")"
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable: " & Variable)
                If Variable <> String.Empty Then
                    Try
                        R = String.Empty
                        TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)

                        'If String.Compare(ValorVariable.GetType().Name.ToLower, "datarow") = 0 Then
                        '    Dim drvalue As DataRow
                        '    drvalue = DirectCast(ValorVariable, DataRow)
                        'Dim numcol As Int32
                        'If Value(1).ToString.Split(Char.Parse(")")).Length > 2 Then
                        '    Value(1) = Value(1).ToString.Split(Char.Parse(")"))(0).ToString & ")"
                        'End If
                        'If Int32.TryParse(Value(1).ToString.Trim.Substring(0, Value(1).ToString.Length - 1), numcol) Then
                        '    TextoReconocido = drValue(numcol).ToString.Trim
                        'Else
                        '    TextoReconocido = drValue(Value(1).ToString.Trim.Substring(0, Value(1).ToString.Length - 1)).ToString.Trim
                        'End If
                        'Return TextoaValidar.Replace("zvar(" & Value(0) & "(" & Value(1) & ")", TextoReconocido)
                        'Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "ValorVariable: " & ValorVariable.ToString.Trim)
                        'TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                        cambios.Add(Variable, ValorVariable.ToString.Trim)
                        'ZTrace.WriteLineIf(ZTrace.IsVerbose, "Texto Reconocido: " & TextoReconocido)
                        'End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        TextoaValidar = TextoaValidar.Replace(Variable, "")
                    End Try
                Else
                    Exit While
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsVerbose, ex.ToString())
            End Try
        End While

        Return cambios
    End Function

    Public Shared Function ReconocerVariablesAsObject(ByVal TextoaValidar As String) As Object
        Dim R As String = String.Empty
        Dim ValorVariable As Object = Nothing
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconocer Variables" & vbCrLf & "Texto a Validar: " & TextoaValidar)
        If TextoaValidar.ToLower().Contains("zvar") = False Then
            ValorVariable = TextoaValidar
        Else
            While TextoaValidar.ToLower().Contains("zvar")
                Try
                    Dim Variable As String = WFRuleParent.ObtenerNombreVariable(TextoaValidar)
                    Variable = "zvar(" & Variable & ")"
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable: " & Variable)
                    If Variable <> String.Empty Then
                        Try
                            R = String.Empty
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                            ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)
                        Catch ex As Exception
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                            ZClass.raiseerror(ex)
                        End Try
                    Else
                        Exit While
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsWarning, ex.ToString())
                End Try
            End While
        End If

        Return ValorVariable
    End Function

    Public Shared Function ReconocerZvar(ByVal TextoaValidar As String) As List(Of String)

        Dim Params As New List(Of String)



        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconocer Zvars")
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Texto a Validar: " & TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar")
        TextoaValidar = TextoaValidar.Replace("ZVar", "zvar")

        While TextoaValidar.ToLower().Contains("zvar")
            Dim Variable As String = TextoaValidar.Substring(TextoaValidar.IndexOf("zvar").ToString(), TextoaValidar.IndexOf(")", TextoaValidar.IndexOf("zvar")) - TextoaValidar.IndexOf("zvar") + 1)

            Params.Add(Variable)

            TextoaValidar = TextoaValidar.Replace(Variable, "")
        End While

        Return Params
    End Function

    ''' <summary> 
    ''' Reconoce las variables 
    ''' </summary> 
    ''' <param name="TextoaValidar"></param> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Shared Function ReconocerVariables(ByVal TextoaValidar As String) As String
        Dim R As String = String.Empty
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconocer Variables")
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Texto a Validar: " & TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar")
        TextoaValidar = TextoaValidar.Replace("ZVar", "zvar")

        While TextoaValidar.ToLower().Contains("zvar") Or TextoaValidar.ToLower().Contains("zvarhtml")
            Try
                Dim isHtml As Boolean
                Dim Variable As String = WFRuleParent.ObtenerNombreVariable(TextoaValidar)
                If Variable.StartsWith("§html§") Then
                    isHtml = True
                    Variable = Variable.Replace("§html§", "")
                End If

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable: " & Variable)
                If Variable <> String.Empty Then
                    Dim ValorVariable As Object
                    Try
                        R = String.Empty
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(TextoaValidar)

                        If IsNothing(ValorVariable) = False Then
                            If TypeOf (ValorVariable) Is DataSet Then
                                Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        For Each DR As DataRow In ds.Tables(0).Rows
                                            R &= DR.Item(0).ToString & ","
                                        Next
                                    End If
                                End If
                                R = R.Remove(R.Length - 1, 1)
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Variable: " & R)
                                TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), R, RegexOptions.IgnoreCase)
                                'TextoaValidar = TextoaValidar.Replace("zvar(" & Variable & ")", R) 
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Variable: " & ValorVariable.ToString())

                                If isHtml Then
                                    Dim Simbols As String() = {"á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú", "ñ", "Ñ"}
                                    Dim simbolReplacement As String() = {"&aacute;", "&eacute;", "&iacute;", "&oacute;", "&uacute;", "&Aacute;", "&Eacute;", "&Iacute;", "&Oacute;", "&Uacute;", "&ntilde;", "&Ntilde;"}

                                    Dim textoSinAcentos As String = String.Empty
                                    For Each caracter As String In ValorVariable
                                        If (Simbols.Contains(caracter)) Then
                                            textoSinAcentos = textoSinAcentos + (simbolReplacement(Simbols.Length - 1))
                                        Else
                                            textoSinAcentos = textoSinAcentos + (caracter)
                                        End If
                                    Next

                                    ValorVariable = textoSinAcentos
                                End If

                                If Variable.Contains("(") = True AndAlso Variable.Contains(")") = False Then
                                    Variable = Variable & ")"
                                End If
                                If isHtml Then
                                    TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvarhtml(" & Variable & ")"), ValorVariable.ToString(), RegexOptions.IgnoreCase)
                                Else
                                    TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), ValorVariable.ToString(), RegexOptions.IgnoreCase)
                                End If
                                'TextoaValidar = TextoaValidar.Replace("zvar(" & Variable & ")", ValorVariable.ToString()) 
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se recupero el Valor de la Variable")
                            TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                            'TextoaValidar = TextoaValidar.Replace("zvar(" & Variable & ")", String.Empty) 
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                        'TextoaValidar = TextoaValidar.Replace("zvar(" & Variable & ")", String.Empty) 
                    Finally
                        ValorVariable = Nothing
                    End Try
                Else
                    Exit While
                End If
                Variable = Nothing
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsWarning, ex.ToString())
            End Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Texto a Validar: " & TextoaValidar)
        End While

        R = Nothing
        Return TextoaValidar
    End Function



    ''' <summary>
    '''     Obtain files from an object and give them a specified format
    ''' </summary>
    ''' <param name="params">attachs</param>
    ''' <returns></returns>
    ''' <history>
    '''     Tomas  07/08/2011  Created
    '''     Pablo  08/08/2011  Modified
    ''' </history>
    Public Shared Function AttachPathsFromZvar(ByVal attachs As Object) As Object
        Dim attType As String = attachs.GetType().ToString()
        Dim paths As String = String.Empty
        If attachs IsNot Nothing Then
            'Verifica la existencia de adjuntos por variable
            Select Case attType
                Case "System.String"
                    paths = ";" & attachs.ToString()
                Case "System.String[]"
                    For Each ruta As String In DirectCast(attachs, String())
                        paths += (ruta)
                    Next
                Case "System.Data.DataSet"
                    Dim ds As DataSet = DirectCast(attachs, DataSet)
                    For Each dr As DataRow In ds.Tables(0).Rows
                        paths += (";" & dr.Item(0).ToString)
                    Next
                    ds.Dispose()
                    ds = Nothing
                Case "System.Data.DataTable"
                    Dim dt As DataTable = DirectCast(attachs, DataTable)
                    For Each dr As DataRow In dt.Rows
                        paths += (";" & dr.Item(0).ToString)
                    Next
                    dt.Dispose()
                    dt = Nothing
            End Select
        End If
        Return paths
    End Function



    Public Overloads Function ExecuteWebRule(ByVal results As List(Of ITaskResult), ByVal taskbusiness As IWFTaskBusiness, ByVal ruleBusiness As IWFRuleBusiness, ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable, ByVal IsAsync As Boolean) As List(Of ITaskResult) Implements IWFRuleParent.ExecuteWebRule
        Dim newresults As List(Of ITaskResult) = Nothing

        'todo: ver que hace esto
        'SETEA EL USUARIO ASIGNADO SEGUN PREFERENCIA DE REGLA
        'Dim params As Hashtable = New Hashtable
        'params.Add("RuleId", Me.ID)
        'HandleRuleModule(ResultActions.UpdateUserAsigned, results, params)
        If (((ExecuteWhenResult.HasValue AndAlso ExecuteWhenResult) OrElse ExecuteWhenResult.HasValue = False) AndAlso Not results Is Nothing AndAlso results.Count > 0) OrElse (ExecuteWhenResult.HasValue AndAlso ExecuteWhenResult = False) Then
            Try
                If IsNothing(Params) OrElse Params.Count = 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "**************************************************************************************")
                    If results.Count > 1 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, $"<b>EXEC Rule: {RuleClass} ({ID}) '{Name}' -- Tasks Count: {results.Count}<b/>")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, $"<b>EXEC Rule: {RuleClass} ({ID}) '{Name}' -- Task: {results(0).Name} ({results(0).TaskId})<b/>")
                    End If
                End If

                Dim RID As Int64 = ID
                Me.StartTime = Date.Now
                newresults = Me.PlayWeb(results, RulePendingEvent, ExecutionResult, Params)

                If IsNothing(ExecutionResult) OrElse ExecutionResult = RuleExecutionResult.NoExecution Then
                    ExecutionResult = RuleExecutionResult.CorrectExecution
                End If

                If Not IsNothing(newresults) Then
                    If Me.RefreshRule AndAlso RulePendingEvent = RulePendingEvents.NoPendingEvent Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Refrescar la tarea")
                        RulePendingEvent = RulePendingEvents.RefreshTask
                    End If
                End If
            Catch ex As Exception
                Dim ex2string As String = "Error en regla: " & Me.Name + " ID:" + Me.ID.ToString & " - " & ex.Message
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex2string)


                If VariablesInterReglas.ContainsKey("error") = False Then
                    VariablesInterReglas.Add("error", ex2string)
                Else
                    VariablesInterReglas.Item("error") = ex2string
                End If

                '[Ezequiel] 08/06/09 - Valido si debo cortar la ejecucion de la tarea
                If Me.ContinueWithError = False Then
                    ' ChildRules.Clear()
                    If Me.ExecuteRuleInCaseOfError Then
                        If Me.RuleIdToExecuteAfterError.HasValue AndAlso Me.RuleIdToExecuteAfterError.Value > 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla despues del error: " + Me.RuleIdToExecuteAfterError.Value.ToString())
                            Params.Add("RuleId", Me.RuleIdToExecuteAfterError.Value)

                            RulePendingEvent = RulePendingEvents.ExecuteErrorRule
                            ExecutionResult = RuleExecutionResult.PendingEventExecution
                        End If
                    End If

                    'Si el usuario cancelo la regla entonces no se muestra la exception 
                    'ya que es una forzada y no una real
                    If Not Me.ThrowExceptionIfCancel Then
                        If String.IsNullOrEmpty(Me.MessageToShowInCaseOfError) Then
                            Throw New Exception("Ocurrió un error en la ejecución de reglas de la tarea." & vbCrLf & "Contactese con el administrador del sistema. " & ex2string, ex)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje de error mostrado al usuario: " + Me.MessageToShowInCaseOfError)
                            Throw New Exception(Me.MessageToShowInCaseOfError, ex)
                        End If
                    Else
                        If ex.Message <> "El usuario cancelo la ejecucion de la regla" Then
                            ZClass.raiseerror(ex)
                        End If
                        newresults = results
                    End If
                Else
                    ZClass.raiseerror(ex)
                    newresults = results
                End If
            End Try

            Dim BCHistoryID As Int64
            'Guarda ultima actualizacion
            If Me.SaveUpdate Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se guardan datos de actualizacion")
                Dim VarInterReglas As New VariablesInterReglas()
                Me._comment = VarInterReglas.ReconocerVariables(Me.UpdateComment)
                VarInterReglas = Nothing
                If Not IsNothing(results) AndAlso results.Count > 0 Then
                    For Each Result As Result In results
                        If Not IsNothing(Me.SaveUpdateInHistory) Then
                            BCHistoryID = taskbusiness.SetLastUpdate(Result, _comment, Me.SaveUpdateInHistory, Membership.MembershipHelper.CurrentUser.ID, Me.Name)
                        Else
                            BCHistoryID = taskbusiness.SetLastUpdate(Result, _comment, False, Membership.MembershipHelper.CurrentUser.ID, Me.Name)
                        End If
                    Next
                End If

                If VariablesInterReglas.ContainsKey("LastBCHistoryID") = False Then
                    VariablesInterReglas.Add("LastBCHistoryID", BCHistoryID)
                Else
                    VariablesInterReglas.Item("LastBCHistoryID") = BCHistoryID
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Id de novedad: " & BCHistoryID)



                If VariablesInterReglas.ContainsKey("LastNewsID") = False Then
                    VariablesInterReglas.Add("LastNewsID", BCHistoryID)
                Else
                    VariablesInterReglas.Item("LastNewsID") = BCHistoryID
                End If


                _comment = String.Empty
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se han guardado los datos de actualizacion")
            Else
                'ZTrace.WriteLineIf(ZTrace.IsInfo, "No se guardan datos de actualizacion")
            End If

            'Limpieza de variables
            If Me.CleanRule Then
                ZTrace.WriteLineIf(ZTrace.IsWarning, "Limpiar variables al finalizar")
                VariablesInterReglas.clear()
            End If

            If Me.CloseTask And RulePendingEvent = RulePendingEvents.NoPendingEvent Then
                RulePendingEvent = RulePendingEvents.CloseTask
            ElseIf Me.RefreshRule And RulePendingEvent = RulePendingEvents.NoPendingEvent Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "!! Se realizara la actualizacion de la tarea !!")
                RulePendingEvent = RulePendingEvents.RefreshTask
            End If
        Else
            newresults = results
        End If
        Return newresults
    End Function
End Class
