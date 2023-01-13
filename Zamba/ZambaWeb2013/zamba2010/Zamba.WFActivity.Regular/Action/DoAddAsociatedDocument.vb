Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleCategory("Documentos Asociados"), RuleDescription("Asociar nuevos documentos"), RuleHelp("Permite agregar un nuevo documento de excel, word, power point, planillas de usuario o cualquier otro docuemento asociandolo con el tipo de docuemento elegido"), RuleFeatures(True)> <Serializable()> _
 Public Class DoAddAsociatedDocument
    Inherits WFRuleParent
    Implements IDoAddAsociatedDocument
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _asociatedDocType As DocType
    Private _TemplateId As Int32
    Private _TypeId As OfficeTypes
    Private _Selectionid As IDoAddAsociatedDocument.Selection
    Private _DefaultScreenSelectionid As IDoAddAsociatedDocument.DefaultScreenSelection
    Private _OpenDefaultScreen As Boolean
    Private _DontOpenTaskIfIsAsociatedToWF As Boolean
    Private _haveSpecificAttributes As Boolean
    Private _specificAttrubutes As String

    Public Overrides Sub Dispose()
    End Sub

#Region "Constructores"
    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepId As Int64, ByVal asociatedDocTypeId As Long, ByVal TemplateId As Int32, ByVal typeID As OfficeTypes, _
                   ByVal selectionid As IDoAddAsociatedDocument.Selection, ByVal openDefaultScreen As Boolean, _
                   ByVal defaultselectionid As IDoAddAsociatedDocument.DefaultScreenSelection, ByVal DontOpenTaskIfIsAsociatedToWF As Boolean, _
                    ByVal haveSpecificAttributes As Boolean, ByVal specificAttributes As String)
        MyBase.New(ruleID, ruleName, wfstepId)
        Dim DTB As New DocTypesBusiness
        '[Sebastian 23-10-2009] se agrego validacion porque cuando el doc type id es cero
        ' en el caso de una tarea generada por la regla genera exception.
        If asociatedDocTypeId <> 0 Then
            _asociatedDocType = DTB.GetDocType(asociatedDocTypeId)
        End If
        DTB = Nothing
        Me._TemplateId = TemplateId
        Me.Typeid = typeID
        Me.SelectionId = selectionid
        Me.OpenDefaultScreen = openDefaultScreen
        Me.DefaultScreenSelectionId = defaultselectionid
        Me.DontOpenTaskIfIsAsociatedToWF = DontOpenTaskIfIsAsociatedToWF
        _haveSpecificAttributes = haveSpecificAttributes
        _specificAttrubutes = specificAttributes
    End Sub
#End Region

#Region "Properties"
    Public Property AsociatedDocType() As IDocType Implements IDoAddAsociatedDocument.AsociatedDocType
        Get
            If IsNothing(_asociatedDocType) Then
                _asociatedDocType = New DocType()
            End If

            Return _asociatedDocType
        End Get
        Set(ByVal value As IDocType)
            _asociatedDocType = value
        End Set
    End Property
    Public Property Templateid() As Int32 Implements IDoAddAsociatedDocument.TemplateId
        Get
            Return _TemplateId
        End Get
        Set(ByVal value As Int32)
            _TemplateId = value
        End Set
    End Property
    Public Property Typeid() As OfficeTypes Implements IDoAddAsociatedDocument.Typeid
        Get
            Return _TypeId
        End Get
        Set(ByVal value As OfficeTypes)
            _TypeId = value
        End Set
    End Property
    Public Property SelectionId() As IDoAddAsociatedDocument.Selection Implements IDoAddAsociatedDocument.SelectionId
        Get
            Return _Selectionid
        End Get
        Set(ByVal value As IDoAddAsociatedDocument.Selection)
            _Selectionid = value
        End Set
    End Property

    Public Property DefaultScreenSelectionId() As IDoAddAsociatedDocument.DefaultScreenSelection Implements IDoAddAsociatedDocument.DefaultScreenId
        Get
            Return _DefaultScreenSelectionid
        End Get
        Set(ByVal value As IDoAddAsociatedDocument.DefaultScreenSelection)
            _DefaultScreenSelectionid = value
        End Set
    End Property

    Public Property OpenDefaultScreen() As Boolean Implements IDoAddAsociatedDocument.OpenDefaultScreen
        Get
            Return _OpenDefaultScreen
        End Get
        Set(ByVal value As Boolean)
            _OpenDefaultScreen = value
        End Set
    End Property

    Public Property DontOpenTaskIfIsAsociatedToWF() As Boolean Implements IDoAddAsociatedDocument.DontOpenTaskIfIsAsociatedToWF
        Get
            Return _DontOpenTaskIfIsAsociatedToWF
        End Get
        Set(ByVal value As Boolean)
            _DontOpenTaskIfIsAsociatedToWF = value
        End Set
    End Property

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

    ''' <summary>
    ''' Marca si la regla utilizara la configuracion para atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HaveSpecificAttributes As Boolean Implements IDoAddAsociatedDocument.HaveSpecificAttributes
        Get
            Return _haveSpecificAttributes
        End Get
        Set(ByVal value As Boolean)
            _haveSpecificAttributes = value
        End Set
    End Property
    ''' <summary>
    ''' Contiene todos la configuracion de los atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecificAttrubutes As String Implements IDoAddAsociatedDocument.SpecificAttrubutes
        Get
            Return _specificAttrubutes
        End Get
        Set(ByVal value As String)
            _specificAttrubutes = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

    'Public Overrides Function Play(ByVal results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOAddAsociatedDocument(Me)
        Return playRule.Play(results, Me)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOAddAsociatedDocument(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ShowDoAddAsociatedDoc
            Return playRule.PlayWeb(results, Me, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
#End Region

End Class
