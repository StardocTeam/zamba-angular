Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Formularios"), RuleDescription("Mostrar Fomulario"), RuleHelp("Permite mostrar un formulario a elecci�n, pero previamente este debe estar generado con la regla 'crear formulario'"), RuleFeatures(True)> <Serializable()> _
Public Class DoShowForm
    Inherits WFRuleParent
    Implements IDoShowForm

#Region "Atributos"

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _formID As Int64
    Private _associatedDocDataShow As Boolean
    Private _varDocId As String
    Private _DontShowDialogMaximized As Boolean
    Private _ruleparenttype As TypesofRules
    Private _ViewOriginal As Boolean
    Private _ControlBox As Boolean
    Private _CloseFormWindowAfterRuleExecution
#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la regla
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Name"></param>
    ''' <param name="wfstepid"></param>
    ''' <param name="tmpFormID"></param>
    ''' <param name="DocDataShow"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/07/2008	Modified   Se agrego una nueva propiedad (associatedDocDataShow)
    '''                 18/07/2008	Modified   Se agrego una nueva propiedad (nameDocId)
    ''' </history>
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal tmpFormID As Int64, ByVal DocDataShow As Boolean, ByVal nameDocId As String, ByVal DontShowDialogMaximized As Boolean, ByVal ViewOriginalDoc As Boolean, ByVal CrossClose As Boolean, ByVal CloseFormWindowAfterRuleExecution As Boolean, ByVal ViewAsociatedDocuments As Boolean, ByVal RefreshForm As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Me.FormID = tmpFormID
        Me.associatedDocDataShow = DocDataShow
        Me.varDocId = nameDocId
        Me.DontShowDialogMaximized = DontShowDialogMaximized
        Me.RuleParentType = Me.RuleType
        Me.ViewOriginal = ViewOriginalDoc
        Me.ControlBox = CrossClose
        Me.CloseFormWindowAfterRuleExecution = CloseFormWindowAfterRuleExecution
        Me.ViewAsociatedDocs = ViewAsociatedDocuments
        Me.RefreshForm = RefreshForm
    End Sub

#End Region

#Region "Propiedades"

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Property ControlBox() As Boolean Implements Core.IDoShowForm.ControlBox
        Get
            Return _ControlBox
        End Get
        Set(ByVal value As Boolean)
            _ControlBox = value
        End Set
    End Property
    Public Property ViewOriginal() As Boolean Implements Core.IDoShowForm.ViewOriginal
        Get
            Return _ViewOriginal
        End Get
        Set(ByVal value As Boolean)
            _ViewOriginal = value
        End Set
    End Property
    Public Property ViewAsociatedDocs() As Boolean Implements Core.IDoShowForm.ViewAsociatedDocs
    Public Property RefreshForm() As Boolean Implements Core.IDoShowForm.RefreshForm

    Public Property FormID() As Long Implements Core.IDoShowForm.FormID
        Get
            Return _formID
        End Get
        Set(ByVal value As Long)
            _formID = value
        End Set
    End Property

    Public Property associatedDocDataShow() As Boolean Implements Core.IDoShowForm.associatedDocDataShow
        Get
            Return _associatedDocDataShow
        End Get
        Set(ByVal value As Boolean)
            _associatedDocDataShow = value
        End Set
    End Property

    Public Property CloseFormWindowAfterRuleExecution() As Boolean Implements Core.IDoShowForm.CloseFormWindowAfterRuleExecution
        Get
            Return _CloseFormWindowAfterRuleExecution
        End Get
        Set(ByVal value As Boolean)
            _CloseFormWindowAfterRuleExecution = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que guarda la variable nombre doc_id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	18/07/2008	Created
    ''' </history>
    Public Property varDocId() As String Implements IDoShowForm.varDocId
        Get
            Return _varDocId
        End Get
        Set(ByVal value As String)
            _varDocId = value
        End Set
    End Property

    Public Property RuleParentType() As TypesofRules Implements IDoShowForm.RuleParentType
        Get
            Return Me._ruleparenttype
        End Get
        Set(ByVal value As TypesofRules)
            Me._ruleparenttype = value
        End Set
    End Property

    ''' <summary>
    ''' Muestra el dialogo maximizado
    ''' </summary>
    ''' <value></value>
    ''' <history>
    '''    Marcelo Created 16/10/2009
    ''' </history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DontShowDialogMaximized() As Boolean Implements IDoShowForm.DontShowDialogMaximized
        Get
            Return _DontShowDialogMaximized
        End Get
        Set(ByVal value As Boolean)
            _DontShowDialogMaximized = value
        End Set
    End Property
#End Region

#Region "M�todos"

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoShowForm(Me)
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoShowForm(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.DoShowForm
            Return playRule.PlayWeb(results, Params, Me)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
#End Region

End Class