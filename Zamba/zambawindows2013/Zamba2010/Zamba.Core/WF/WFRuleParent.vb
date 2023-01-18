Imports System.Collections.Generic
Imports Zamba.Core.Enumerators
Imports System.Text.RegularExpressions
Imports System.Text

<RuleDescription("Regla Padre"), RuleHelp("Clase de la que heredan todas las reglas")> <Serializable()>
Public MustInherit Class WFRuleParent
    Inherits ZambaCore
    Implements IWFRuleParent

#Region "Atributos y Propiedades"
    Private _parentType As TypesofRules
    Private _RuleType As TypesofRules
    Private _wFStepId As Int64
    Private _parentRule As IRule = Nothing
    Private _rulenode As IRuleNode = Nothing
    Private _childRules As List(Of IRule) = Nothing
    Private _version As Int32
    Private _oldStateEnable As Boolean
    Private _enable As Boolean
    Private _oldStateTrue As Boolean
    Private _isUI As Boolean
    Private _ExecuteWhenResult As Nullable(Of Boolean) = True
    Private _AlertExecution As Nullable(Of Boolean) = False
    'Private _WFStep As IWFStep
    '[Ezequiel] 30/03/2009 Created
    Private _refreshRule As Nullable(Of Boolean) = False
    '[Ezequiel] 05/06/2009 Created
    Private _continueWithError As Nullable(Of Boolean) = False
    '[Ezequiel] 22/06/2009 Created
    Private _closeTask As Nullable(Of Boolean) = False
    '[Marcelo] 02/09/2009 Created
    Private _Help As String
    '[Tomas] 16/09/2009 Created
    Private _cleanRule As Nullable(Of Boolean) = False
    '[Ezequiel] 18/09/09 - Created
    Private _startTime As Date
    '[Ezequiel] 21/09/09 - Created
    'Private _traceTime As System.Diagnostics.TextWriterTraceListener
    '[Marcelo] 13/10/2010 Created.
    Private _Category As Nullable(Of Int16) = 1
    '[Marcelo] 26/11/2010 Created
    Private _SaveUpdate As Nullable(Of Boolean) = False
    '[Marcelo] 26/11/2010 Created
    Private _UpdateComment As String
    '[Marcelo] 14/12/2010 Created
    Private _SaveUpdateInHistory As Nullable(Of Boolean) = False
    '[Marcelo] 03/01/2011 Created
    Private _Asynchronous As Nullable(Of Boolean) = False
    '[AlejandroR] 13/01/2011 Created
    Private _ExecuteRuleInCaseOfError As Nullable(Of Boolean) = False
    '[AlejandroR] 13/01/2011 Created
    Private _MessageToShowInCaseOfError As String
    '[AlejandroR] 13/01/2011 Created
    Private _RuleIdToExecuteAfterError As Nullable(Of Integer) = 0
    '[AlejandroR] 13/01/2011 Created
    Private _ThrowExceptionIfCancel As Nullable(Of Boolean) = False
    '[AlejandroR] 28/02/2011 Created
    Private _DisableChildRules As Nullable(Of Boolean) = False
    Private _comment As String

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
    Public Property ChildRules() As List(Of IRule) Implements IWFRuleParent.ChildRules
        Get
            If IsNothing(_childRules) Then _childRules = New List(Of IRule)

            Return _childRules
        End Get
        Set(ByVal Value As List(Of IRule))
            _childRules = Value
        End Set
    End Property
    Public ReadOnly Property RuleClass() As String Implements IWFRuleParent.RuleClass
        Get
            Return [GetType].Name.ToString
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
    Public Property IfType() As Boolean Implements IRule.IfType
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
            End If
            Return _continueWithError
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _continueWithError = value
        End Set
    End Property
    Public Property CloseTask() As Nullable(Of Boolean) Implements IRule.CloseTask
        Get
            If _closeTask.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
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
    Public Property Help() As String Implements IRule.Description
        Get
            Return _Help
        End Get
        Set(ByVal value As String)
            _Help = value
        End Set
    End Property
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
    Public Property CleanRule() As Nullable(Of Boolean) Implements IRule.CleanRule
        Get
            If _cleanRule.HasValue = False Then
                ZBaseCore.CallForceLoad(Me)
            End If
            Return _cleanRule
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _cleanRule = value
        End Set
    End Property
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
    Public Property UpdateComment() As String Implements IRule.UpdateComment
        Get
            Return _UpdateComment
        End Get
        Set(ByVal value As String)
            _UpdateComment = value
        End Set
    End Property
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
    Public Property MessageToShowInCaseOfError() As String Implements IRule.MessageToShowInCaseOfError
        Get
            Return _MessageToShowInCaseOfError
        End Get
        Set(ByVal value As String)
            _MessageToShowInCaseOfError = value
        End Set
    End Property
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
    Public Property TestResult As Object Implements IWFRuleParent.TestResult
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
        IconId = 31
        RuleType = TypesofRules.Regla

        'Marcelo: call event for load RulesPreferences
        'ZBaseCore.CallLoadRulePreference(Me)
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
    Public MustOverride Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult) Implements IWFRuleParent.Play
    Public MustOverride Function DiscoverParams() As List(Of String) Implements IWFRuleParent.DiscoverParams
    Public MustOverride Function PlayTest() As Boolean Implements IWFRuleParent.PlayTest
    Public Shared Event RuleToExecute(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
    Public Shared Event RuleExecuted(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
    Public Shared Event RuleExecutedError(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult), ByVal ex As Exception, ByRef errorbreakpoint As Boolean)


    Public MustOverride ReadOnly Property MaskName() As String Implements IWFRuleParent.MaskName





    ''' <summary>DisableChildRules
    ''' Execute a Rule
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="taskBusiness"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     Marcelo  03/01/2011  Created       
    ''' </history>
    Public Overloads Function ExecuteRule(ByVal results As List(Of ITaskResult), ByVal taskBusiness As IWFTaskBusiness, ByRef refreshTasks As List(Of Int64)) As List(Of ITaskResult) Implements IWFRuleParent.ExecuteRule
        'Execute the rule in Asynchronous mode
        If Asynchronous = True Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea se ejecuta en modo asincronico")
            Try
                Dim T1 As New Threading.Thread(AddressOf ExecuteRuleAsync)
                Dim params As New ArrayList(3)
                params.Add(results)
                params.Add(taskBusiness)
                T1.Start(params)
            Catch ex As Threading.SynchronizationLockException
            Catch ex As Threading.ThreadAbortException
            Catch ex As Threading.ThreadInterruptedException
            Catch ex As Threading.ThreadStateException
            Catch ex As Exception
                raiseerror(ex)
            End Try
            Return results
        Else
            Return ExecuteRuleSynchronical(results, taskBusiness, refreshTasks)
        End If
    End Function

    ''' <summary>
    '''     Execute a Rule
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="taskBusiness"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  06/12/2010  Modified    Get a updated taks to update the UI    
    ''' </history>
    Public Function ExecuteRuleSynchronical(ByVal results As List(Of ITaskResult), ByVal taskBusiness As IWFTaskBusiness, ByRef refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Dim newresults As New List(Of ITaskResult)()

        'Solo llamo a la ejecucion si esta marcado ejecutar cuando hay resultados y hay al menos un resultado o bien no esta marcado ejecutar cuando hay resultados
        If (((ExecuteWhenResult.HasValue AndAlso ExecuteWhenResult) OrElse Not ExecuteWhenResult.HasValue) AndAlso results IsNot Nothing AndAlso results.Count > 0) _
            OrElse (ExecuteWhenResult.HasValue AndAlso Not ExecuteWhenResult) Then
            If results IsNot Nothing AndAlso (results.Count > 0 OrElse (results.Count = 0 AndAlso Not ExecuteWhenResult)) Then

                If Me.Enable Then
                    RaiseEvent RuleToExecute(Me, results)
                    While BreakPointsUtil.CheckBreakPointOnRule(ID) AndAlso Not BreakPointsUtil.BreakPointContinue(ID)
                    End While

                    If BreakPointsUtil.BreakPointContinue(ID) Then
                        BreakPointsUtil.SetContinueBreakPointState(ID, False)
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "*****************************************************************************")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando: " & RuleClass & " " & ID & " '" & Name & "' para " & results.Count & " tarea/s.")
                    If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "TareaID: " & results(0).TaskId)
                    End If

                    'SETEA EL USUARIO ASIGNADO SEGUN PREFERENCIA DE REGLA
                    Dim params As New Hashtable
                    params.Add("RuleId", ID)
                    HandleRuleModule(ResultActions.UpdateUserAsigned, results, params)

                    Try
                        newresults = Play(results, refreshTasks)
                        RaiseEvent RuleExecuted(Me, results)

                        If Not IsNothing(newresults) Then
                            If RefreshRule And Not IsNothing(refreshTasks) Then
                                Dim i As Int32
                                For i = 0 To newresults.Count - 1
                                    'paso el result abierto en lugar de la nueva tarea generada
                                    If String.Compare(Name.ToLower, "dogeneratetaskresult") = 0 Then
                                        If refreshTasks.Contains(results(0).TaskId) = False Then
                                            refreshTasks.Add(results(0).TaskId)
                                        End If
                                    Else
                                        If refreshTasks.Contains(newresults(0).TaskId) = False Then
                                            refreshTasks.Add(newresults(i).TaskId)
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        'Guarda ultima actualizacion
                        Dim NewsID As Int64
                        If SaveUpdate Then
                            _comment = WFRuleParent.ReconocerVariables(UpdateComment)
                            If Not IsNothing(results) AndAlso results.Count > 0 Then
                                For Each Result As Result In results
                                    If Not IsNothing(SaveUpdateInHistory) Then
                                        NewsID = taskBusiness.SetLastUpdate(Result, _comment, SaveUpdateInHistory)
                                    Else
                                        NewsID = taskBusiness.SetLastUpdate(Result, _comment, False)
                                    End If
                                Next
                            End If
                            _comment = String.Empty
                        End If

                        'Limpieza de variables
                        If CleanRule Then
                            VariablesInterReglas.Clear()
                        End If

                        If SaveUpdate Then
                            If VariablesInterReglas.ContainsKey("LastNewsID") = False Then
                                VariablesInterReglas.Add("LastNewsID", NewsID, False)
                            Else
                                VariablesInterReglas.Item("LastNewsID") = NewsID
                            End If
                        End If
                    Catch ex As System.ArgumentException
                        Dim ex2string As String = ex.Message
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex2string)

                        If ContinueWithError = False Then

                            If ExecuteRuleInCaseOfError Then
                                If RuleIdToExecuteAfterError.HasValue AndAlso RuleIdToExecuteAfterError.Value > 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla despues del error: " + RuleIdToExecuteAfterError.Value.ToString())
                                    Dim p As New Hashtable
                                    p.Add("RuleId", RuleIdToExecuteAfterError.Value)
                                    p.Add("StepId", 0)
                                    HandleRuleModule(ResultActions.ExecuteRule, results, p)
                                End If
                            End If

                            'Si el usuario cancelo la regla entonces no se muestra la exception 
                            'ya que es una forzada y no una real
                            If Not ThrowExceptionIfCancel Then
                                If String.IsNullOrEmpty(MessageToShowInCaseOfError) Then
                                    Throw New Exception(ex2string & vbCrLf & vbCrLf & "Regla: " & Name + " (" + ID.ToString & ")", ex)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje de error mostrado al usuario: " + MessageToShowInCaseOfError)
                                    Throw New Exception(MessageToShowInCaseOfError, ex)
                                End If
                            Else
                                newresults = Nothing
                            End If
                        Else
                            raiseerror(ex)
                            newresults = results
                        End If

                        Dim errorbreakpoint As Boolean = False
                        RaiseEvent RuleExecutedError(Me, results, ex, errorbreakpoint)
                        While errorbreakpoint
                        End While
                    Catch ex As Exception
                        Dim ex2string As String = ex.Message
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex2string)

                        If ContinueWithError = False Then

                            If ExecuteRuleInCaseOfError Then
                                If RuleIdToExecuteAfterError.HasValue AndAlso RuleIdToExecuteAfterError.Value > 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla despues del error: " + RuleIdToExecuteAfterError.Value.ToString())
                                    Dim p As New Hashtable
                                    p.Add("RuleId", RuleIdToExecuteAfterError.Value)
                                    p.Add("StepId", 0)
                                    HandleRuleModule(ResultActions.ExecuteRule, results, p)
                                End If
                            End If

                            'Si el usuario cancelo la regla entonces no se muestra la exception 
                            'ya que es una forzada y no una real
                            If Not ThrowExceptionIfCancel Then
                                If String.IsNullOrEmpty(MessageToShowInCaseOfError) Then
                                    Throw New Exception(ex2string & vbCrLf & vbCrLf & "Regla: " & Name + " (" + ID.ToString & ")", ex)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje de error mostrado al usuario: " + MessageToShowInCaseOfError)
                                    Throw New Exception(MessageToShowInCaseOfError, ex)
                                End If
                            Else
                                newresults = Nothing
                            End If
                        Else
                            raiseerror(ex)
                            newresults = results
                        End If

                        Dim errorbreakpoint As Boolean = False
                        RaiseEvent RuleExecutedError(Me, results, ex, errorbreakpoint)
                        While errorbreakpoint
                        End While

                    Finally
                        params.Clear()
                        params = Nothing
                    End Try

                Else
                    'si no se ejecuto la regla, se asignan los nuevos results para que los usen las reglas hijas, sino no se ejecutan
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla esta deshabilitada: " & Name + " ID:" + ID.ToString)
                    newresults = results
                End If

                If Not IsNothing(ChildRules) AndAlso ChildRules.Count > 0 AndAlso Not newresults Is Nothing AndAlso Not IsNothing(results) AndAlso RuleClass.ToLower() <> "doforeach" Then
                    If (String.Compare(RuleClass, "IfBranch", True) = 0 AndAlso newresults.Count = 0) Then
                        'No se ejecuta la cadena hija, porque no hay resultados que hayan cumplido con la condicion y tipo de condicion
                    Else
                        '[AlejandroR] 01/03/2011 - verificar si la regla tiene configurado que no se ejecuten las hijas
                        If Not DisableChildRules Then

                            For Each R As WFRuleParent In ChildRules
                                Try
                                    Dim newchildresults As New List(Of ITaskResult)()
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, " CHILD RULE ")
                                    newchildresults = R.ExecuteRule(newresults, taskBusiness, refreshTasks)
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
                                        If ex.Message.Contains("Regla:") Then
                                            Throw ex
                                        Else
                                            Throw New Exception(ex.Message & vbCrLf & vbCrLf & "Regla: " & Name + " (" + ID.ToString & ")", ex)
                                        End If
                                    Else
                                        raiseerror(ex)
                                        Dim errorBreakPoint As Boolean = False
                                        RaiseEvent RuleExecutedError(Me, newresults, ex, errorBreakPoint)
                                        While errorBreakPoint
                                        End While
                                    End If
                                End Try
                            Next
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Las reglas hijas estan deshabilitadas")
                        End If

                    End If

                End If
                If CloseTask Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encuentra marcada la opcion de cerrado de tarea en regla: " & ID)
                    HandleRuleModule(ResultActions.CloseTask, results, Nothing)
                End If
            End If
        Else
            newresults = results
        End If
        Return newresults
    End Function

    Public Function TestRule(ByVal tasks As List(Of ITaskResult)) As List(Of ITaskResult) Implements IRuleTest.TestRule
        Dim newResults As New List(Of ITaskResult)
        Dim refreshTasks As New List(Of Int64)

        If Enable Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla: " & RuleClass & ": " & ID & ": " & Name)

            Try
                Dim breakpoint As Boolean = False
                RaiseEvent RuleToExecute(Me, tasks)
                While BreakPointsUtil.CheckBreakPointOnRule(Me.ID) > 0 AndAlso Not BreakPointsUtil.BreakPointContinue(Me.ID)
                End While

                If BreakPointsUtil.BreakPointContinue(Me.ID) Then
                    BreakPointsUtil.SetContinueBreakPointState(Me.ID, False)
                End If

                newResults = Me.Play(tasks, refreshTasks)
                RaiseEvent RuleExecuted(Me, tasks)

                'Limpieza de variables
                If CleanRule Then
                    VariablesInterReglas.Clear()
                End If

            Catch ex As Exception
                Dim errorbreakpoint As Boolean = False
                RaiseEvent RuleExecutedError(Me, tasks, ex, errorbreakpoint)
                While errorbreakpoint
                End While

                Dim ex2string As String = "Error en regla: " & Name + " ID:" + ID.ToString & " - " & ex.ToString
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex2string)

                If ContinueWithError = False Then
                    If ExecuteRuleInCaseOfError Then
                        If RuleIdToExecuteAfterError.HasValue AndAlso RuleIdToExecuteAfterError.Value > 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla despues del error: " + RuleIdToExecuteAfterError.Value.ToString())

                            Dim p As New Hashtable
                            p.Add("RuleId", RuleIdToExecuteAfterError.Value)
                            p.Add("StepId", 0)

                            HandleRuleModule(ResultActions.ExecuteRule, tasks, p)
                        End If
                    End If

                    'Si el usuario cancelo la regla entonces no se muestra la exception 
                    'ya que es una forzada y no una real
                    If Not ThrowExceptionIfCancel Then

                        RaiseEvent RuleExecutedError(Me, tasks, ex, errorbreakpoint)
                        While errorbreakpoint
                        End While
                        If String.IsNullOrEmpty(MessageToShowInCaseOfError) Then
                            Throw New Exception("Ocurrió un error en la ejecución de reglas de la tarea." & vbCrLf & "Contactese con el administrador del sistema. " & ex2string, ex)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje de error mostrado al usuario: " + MessageToShowInCaseOfError)
                            Throw New Exception(MessageToShowInCaseOfError, ex)
                        End If
                    Else
                        newResults = Nothing
                    End If
                Else
                    raiseerror(ex)

                    RaiseEvent RuleExecutedError(Me, tasks, ex, errorbreakpoint)
                    While errorbreakpoint
                    End While
                    newResults = tasks
                End If
            End Try
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla no se ejecuto, se encuentra deshabilitada: " & Name + " ID:" + ID.ToString)
            newResults = tasks
        End If

        If Not IsNothing(ChildRules) AndAlso ChildRules.Count > 0 AndAlso Not newResults Is Nothing AndAlso Not IsNothing(tasks) AndAlso RuleClass.ToLower() <> "doforeach" Then
            If (String.Compare(RuleClass, "IfBranch", True) = 0 AndAlso newResults.Count = 0) Then
                'No se ejecuta la cadena hija, porque no hay resultados que hayan cumplido con la condicion y tipo de condicion
            Else
                '[AlejandroR] 01/03/2011 - verificar si la regla tiene configurado que no se ejecuten las hijas
                If Not DisableChildRules Then

                    For Each R As WFRuleParent In ChildRules
                        Try
                            Dim newchildresults As New List(Of ITaskResult)()

                            newchildresults = R.TestRule(newResults)
                            If IsNothing(newchildresults) Then
                                'newResults = Nothing
                            Else
                                'ML: Cancelacion de ifbranch proximo, si la cantidad de results devueltos por el primer ifbranch 
                                'es igual al ingresado, es decir que todas las tareas cumplieron con la primer condicion.
                                If String.Compare(R.RuleClass, "IfBranch", True) = 0 AndAlso newchildresults.Count = newResults.Count Then
                                    Exit For
                                End If
                            End If

                        Catch ex As Exception
                            '[Ezequiel] 08/06/09 - Valido si debo cortar la ejecucion de la tarea
                            ' en base a la regla que se esta ejecutando actualmente.
                            If R.ContinueWithError = False Then
                                If ex.Message.Contains("Regla:") Then
                                    Throw ex
                                Else
                                    Throw New Exception("Ocurrio un Error: " + ex.Message & vbCrLf & "Regla: " & Name + " (" + ID.ToString & ")", ex)
                                End If
                            Else
                                raiseerror(ex)
                            End If
                        End Try
                    Next
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "- Child Rules Are Not Enabled -")
                End If
            End If
        End If
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
        ExecuteRuleSynchronical(DirectCast(params, ArrayList)(0), DirectCast(params, ArrayList)(1), Nothing)

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
        Dim ValorVar As Object

        For Each dt As DataTable In ds.Tables

            If dt.Rows.Count <= 1 Then

                For Each col As DataColumn In dt.Columns

                    NombreVar = col.ColumnName.Replace(" ", "_")

                    If dt.Rows.Count = 0 Then
                        ValorVar = String.Empty
                    Else
                        If IsDBNull(dt.Rows(0).Item(col.Ordinal)) Then
                            ValorVar = String.Empty
                        ElseIf dt.Rows(0).Item(col.Ordinal) Is Nothing Then
                            ValorVar = String.Empty
                        Else
                            ValorVar = dt.Rows(0).Item(col.Ordinal)
                        End If
                    End If

                    If VariablesInterReglas.ContainsKey(NombreVar) = False Then
                        VariablesInterReglas.Add(NombreVar, ValorVar, False)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " Creada con valor: " & ValorVar)
                    Else
                        VariablesInterReglas.Item(NombreVar) = ValorVar
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable " + NombreVar + " Guardada con valor: " & ValorVar)
                    End If

                Next

            End If

        Next

    End Sub

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
        Dim TextoReconocido As String = TextoaValidar
        Dim ValorVariable As Object

        Try

            If String.IsNullOrEmpty(TextoaValidar) Then
                Return String.Empty
            End If
            If TextoaValidar.ToLower().Contains("zvar") = False Then
                Return TextoaValidar
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
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

                            If isHtml Then
                                Variable = Variable.Replace("zvar(", "zvarhtml(")
                                TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                            Else
                                If ValorVariable Is Nothing Then
                                    ValorVariable = String.Empty
                                End If
                                TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & TextoReconocido)
                            End If

                        Catch ex As Exception
                            raiseerror(ex)
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                        End Try
                    Else
                        Exit While
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                Finally
                    Variable = Nothing
                End Try
            End While
        Finally
            ValorVariable = Nothing
        End Try

        Return TextoReconocido
    End Function

    Public Shared Function ReconocerVariablesValuesSoloTextoAsHashTB(ByVal TextoaValidar As String) As Hashtable
        Dim R As String = String.Empty
        Dim ValorVariable As Object
        Dim cambios As New Hashtable()

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables" & vbCrLf & "Texto a Validar: " & TextoaValidar)
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
                If Variable <> String.Empty Then
                    Try
                        R = String.Empty
                        TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)
                        If cambios.ContainsKey(Variable) = False Then

                            cambios.Add(Variable, ValorVariable.ToString.Trim)
                        Else
                            cambios(Variable) = ValorVariable.ToString.Trim
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & ValorVariable.ToString.Trim)
                    Catch ex As Exception
                        raiseerror(ex)
                        TextoaValidar = TextoaValidar.Replace(Variable, "")
                    End Try
                Else
                    Exit While
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            End Try
        End While

        Return cambios
    End Function

    Public Shared Function ReconocerVariablesAsObject(ByVal TextoaValidar As String) As Object
        Dim R As String = String.Empty
        Dim ValorVariable As Object = Nothing
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables" & vbCrLf & "Texto a Validar: " & TextoaValidar)
        If TextoaValidar.ToLower().Contains("zvar") = False Then
            ValorVariable = TextoaValidar
        Else
            While TextoaValidar.ToLower().Contains("zvar")
                Try
                    Dim Variable As String = WFRuleParent.ObtenerNombreVariable(TextoaValidar)
                    Variable = "zvar(" & Variable & ")"
                    If Variable <> String.Empty Then
                        Try
                            R = String.Empty
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                            ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & ValorVariable.ToString.Trim)
                        Catch ex As Exception
                            TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                            raiseerror(ex)
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

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar")
        TextoaValidar = TextoaValidar.Replace("ZVar", "zvar")

        While TextoaValidar.ToLower().Contains("zvar")
            Dim Variable As String = TextoaValidar.Substring(TextoaValidar.IndexOf("zvar", StringComparison.CurrentCultureIgnoreCase).ToString(), TextoaValidar.IndexOf(")", TextoaValidar.IndexOf("zvar", StringComparison.CurrentCultureIgnoreCase)) - TextoaValidar.IndexOf("zvar", StringComparison.CurrentCultureIgnoreCase) + 1)
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
    Public Shared Function ReconocerVariables(ByVal TextoaValidar As String, Optional ByVal TraceChanges As Boolean = True) As String
        Dim R As String = String.Empty

        If TextoaValidar.Length = 0 Then Return R

        ZTrace.WriteLineIf(TraceChanges, "Texto a Validar: " & TextoaValidar)
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

                If Variable <> String.Empty Then
                    Dim ValorVariable As Object
                    Try
                        R = String.Empty
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(TextoaValidar)

                        If ValorVariable IsNot Nothing Then
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
                                TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), R, RegexOptions.IgnoreCase)
                                ZTrace.WriteLineIf(TraceChanges, "Texto Reconocido: " & R)
                                ZTrace.WriteLineIf(TraceChanges, "Texto Final: " & TextoaValidar)
                            Else
                                If isHtml Then
                                    Dim Simbols As String() = {"á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú", "ñ", "Ñ"}
                                    Dim simbolReplacement As String() = {"&aacute;", "&eacute;", "&iacute;", "&oacute;", "&uacute;", "&Aacute;", "&Eacute;", "&Iacute;", "&Oacute;", "&Uacute;", "&ntilde;", "&Ntilde;"}

                                    Dim textoSinAcentos As String = String.Empty
                                    If TypeOf ValorVariable Is Enumerable Then
                                        For Each caracter As String In ValorVariable
                                            If (Simbols.Contains(caracter)) Then
                                                textoSinAcentos = textoSinAcentos + (simbolReplacement(Simbols.Length - 1))
                                            Else
                                                textoSinAcentos = textoSinAcentos + (caracter)
                                            End If
                                        Next
                                    Else
                                        For Each caracter As String In ValorVariable.ToString().ToCharArray()
                                            If (Simbols.Contains(caracter)) Then
                                                textoSinAcentos = textoSinAcentos + (simbolReplacement(Simbols.Length - 1))
                                            Else
                                                textoSinAcentos = textoSinAcentos + (caracter)
                                            End If
                                        Next

                                    End If

                                    ValorVariable = textoSinAcentos
                                End If

                                If Variable.Contains("(") = True AndAlso Variable.Contains(")") = False Then
                                    Variable = Variable & ")"
                                End If

                                If isHtml Then
                                    TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvarhtml(" & Variable & ")"), ValorVariable.ToString, RegexOptions.IgnoreCase)
                                Else
                                    TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), ValorVariable.ToString, RegexOptions.IgnoreCase)
                                End If
                                ZTrace.WriteLineIf(TraceChanges, "Texto Reconocido: " & ValorVariable.ToString)
                                ZTrace.WriteLineIf(TraceChanges, "Texto Final: " & TextoaValidar)
                            End If
                        Else
                            ZTrace.WriteLineIf(TraceChanges, "No se recupero el Valor de la Variable")
                            TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                        TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
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
        End While

        R = Nothing
        Return TextoaValidar
    End Function

    Public Shared Function ReconocerVariablesYTablas(ByVal TextoaValidar As String) As String
        Dim R As String = String.Empty

        If TextoaValidar.Length = 0 Then Return R

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
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

                If Variable <> String.Empty Then
                    Dim ValorVariable As Object
                    Try
                        R = String.Empty
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(TextoaValidar)

                        If ValorVariable IsNot Nothing Then
                            If TypeOf (ValorVariable) Is DataSet Then
                                Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        Dim sb As New StringBuilder
                                        sb.Append("<div id=""" & Variable & """ ></div>")
                                        sb.Append("<script>")
                                        sb.Append("$(""#" & Variable & """).kendoGrid({")
                                        sb.Append("dataSource: {")
                                        sb.Append("data: {")
                                        sb.Append("""items"" :[")

                                        For Each DR As DataRow In ds.Tables(0).Rows
                                            sb.Append("{")
                                            For Each column As DataColumn In ds.Tables(0).Columns
                                                sb.Append("""" & column.ColumnName.Replace(" ", "_") & """:""" & DR(column.Ordinal) & """,")
                                            Next
                                            sb.Remove(sb.Length - 1, 1)
                                            sb.Append("},")
                                        Next
                                        sb.Remove(sb.Length - 1, 1)
                                        sb.Append("]")
                                        sb.Append("},")
                                        sb.Append("schema: {")
                                        sb.Append("data: ""items""")
                                        sb.Append("}},")
                                        sb.Append("columns: [")
                                        For Each column As DataColumn In ds.Tables(0).Columns
                                            sb.Append("{ field: """)
                                            sb.Append(column.ColumnName.Replace(" ", "_") & """, title: """ & column.ColumnName & """")
                                            sb.Append("},")
                                        Next
                                        sb.Remove(sb.Length - 1, 1)
                                        sb.Append("]")
                                        sb.Append(",scrollable: false	,resizable: true")
                                        sb.Append("})")
                                        sb.Append("</script>")
                                        R = sb.ToString()
                                    End If
                                End If

                                TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), R, RegexOptions.IgnoreCase)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & R)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & TextoaValidar)
                            Else
                                If isHtml Then
                                    Dim Simbols As String() = {"á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú", "ñ", "Ñ"}
                                    Dim simbolReplacement As String() = {"&aacute;", "&eacute;", "&iacute;", "&oacute;", "&uacute;", "&Aacute;", "&Eacute;", "&Iacute;", "&Oacute;", "&Uacute;", "&ntilde;", "&Ntilde;"}

                                    Dim textoSinAcentos As String = String.Empty
                                    If TypeOf ValorVariable Is Enumerable Then
                                        For Each caracter As String In ValorVariable
                                            If (Simbols.Contains(caracter)) Then
                                                textoSinAcentos = textoSinAcentos + (simbolReplacement(Simbols.Length - 1))
                                            Else
                                                textoSinAcentos = textoSinAcentos + (caracter)
                                            End If
                                        Next
                                    Else
                                        For Each caracter As String In ValorVariable.ToString().ToCharArray()
                                            If (Simbols.Contains(caracter)) Then
                                                textoSinAcentos = textoSinAcentos + (simbolReplacement(Simbols.Length - 1))
                                            Else
                                                textoSinAcentos = textoSinAcentos + (caracter)
                                            End If
                                        Next

                                    End If

                                    ValorVariable = textoSinAcentos
                                End If

                                If Variable.Contains("(") = True AndAlso Variable.Contains(")") = False Then
                                    Variable = Variable & ")"
                                End If

                                If isHtml Then
                                    TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvarhtml(" & Variable & ")"), ValorVariable.ToString, RegexOptions.IgnoreCase)
                                Else
                                    TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), ValorVariable.ToString, RegexOptions.IgnoreCase)
                                End If
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & ValorVariable.ToString)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & TextoaValidar)
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se recupero el Valor de la Variable")
                            TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                        TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
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
        End While

        R = Nothing
        Return TextoaValidar
    End Function

    ''' <summary>
    ''' Obtiene el objeto de alguna variable
    ''' </summary>
    ''' <param name="TextoaValidar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerValorVariableObjeto(ByVal TextoaValidar As String) As Object

        If TextoaValidar = String.Empty Then Return String.Empty

        Dim variable As String
        Dim ValorVariable As Object = Nothing
        If TextoaValidar <> String.Empty AndAlso TextoaValidar.ToLower().IndexOf("zvar") <> -1 Then
            Dim TxtToValidate As String
            TxtToValidate = TextoaValidar.Trim()
            If TextoaValidar.Trim.ToLower().IndexOf("zvarhtml(") > TextoaValidar.Trim.ToLower().IndexOf("zvar(") Then
                TxtToValidate = TextoaValidar.ToLower.IndexOf("zvarhtml(") + 9
            Else
                TxtToValidate = TextoaValidar.ToLower.IndexOf("zvar(") + 5
            End If

            variable = TextoaValidar.Remove(0, TxtToValidate)
            variable = variable.Remove(variable.IndexOf(")"))
            If variable.Contains("(") AndAlso variable.Substring(variable.Length - 1).CompareTo(")") <> 0 Then
                variable &= ")"
            End If
        Else
            variable = TextoaValidar
        End If

        If VariablesInterReglas.ContainsKey(variable) Then
            ValorVariable = VariablesInterReglas.Item(variable)
        Else
            If TextoaValidar.ToLower.Contains("(") = True Then
                Dim Value() As String
                Dim mander As Char = Char.Parse("(")
                Dim objValue As Object
                Dim drValue As DataRow

                Try
                    Value = variable.Split(mander)
                    objValue = VariablesInterReglas.Item(Value(0))

                    If Not IsNothing(objValue) Then
                        If String.Compare(objValue.GetType().Name.ToLower, "datarow") = 0 Then
                            drValue = DirectCast(objValue, DataRow)
                            Dim numcol As Int32
                            If Int32.TryParse(Value(1).ToString.Trim.Substring(0, Value(1).ToString.Length - 1), numcol) Then
                                ValorVariable = drValue(numcol)
                            Else
                                ValorVariable = drValue(Value(1).ToString.Trim.Substring(0, Value(1).ToString.Length - 1))
                            End If
                        Else
                            ValorVariable = objValue
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la variable")
                        ValorVariable = New Object
                        ValorVariable = String.Empty
                        VariablesInterReglas.Add(variable, ValorVariable, False)
                    End If
                Finally
                    Value = Nothing
                    mander = Nothing
                    objValue = Nothing
                    drValue = Nothing
                End Try
            Else
                ValorVariable = String.Empty
            End If
        End If

        variable = Nothing
        Return ValorVariable
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
    Public Shared Function AttachPathsFromZvar(ByVal attachs As Object) As String
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

End Class