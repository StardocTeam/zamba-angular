Imports Zamba.Core

<RuleMainCategory("Documentos y Asociados"), RuleCategory("Documentos Asociados"), RuleSubCategory("Asociar nuevo"), RuleDescription("Asociar nuevos documentos"), RuleHelp("Permite agregar un nuevo documento de excel, word, power point, planillas de usuario o cualquier otro docuemento asociandolo con el tipo de docuemento elegido"), RuleFeatures(True)> <Serializable()> _
Public Class DoAddAsociatedDocument
    Inherits WFRuleParent
    Implements IDoAddAsociatedDocument, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOAddAsociatedDocument
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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

    Private _asociatedDocType As DocType
    Private _TemplateId As Int32
    Private _TypeId As OfficeTypes
    Private _Selectionid As IDoAddAsociatedDocument.Selection
    Private _DefaultScreenSelectionid As IDoAddAsociatedDocument.DefaultScreenSelection
    Private _OpenDefaultScreen As Boolean
    Private _DontOpenTaskIfIsAsociatedToWF As Boolean
    Private _haveSpecificAttributes As Boolean
    Private _specificAttrubutes As String
    Private _isValid As Boolean

    Public Overrides Sub Dispose()
    End Sub

#Region "Constructores"
    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepId As Int64, ByVal asociatedDocTypeId As Long, _
                   ByVal TemplateId As Int32, ByVal typeID As OfficeTypes, ByVal selectionid As IDoAddAsociatedDocument.Selection, _
                   ByVal openDefaultScreen As Boolean, ByVal defaultselectionid As IDoAddAsociatedDocument.DefaultScreenSelection, _
                   ByVal DontOpenTaskIfIsAsociatedToWF As Boolean, ByVal haveSpecificAttributes As Boolean, ByVal specificAttributes As String)
        MyBase.New(ruleID, ruleName, wfstepId)
        '[Sebastian 23-10-2009] se agrego validacion porque cuando el doc type id es cero
        ' en el caso de una tarea generada por la regla genera exception.
        If asociatedDocTypeId <> 0 Then
            _asociatedDocType = DocTypesBusiness.GetDocType(asociatedDocTypeId, True)
        End If

        _TemplateId = TemplateId
        Me.Typeid = typeID
        Me.SelectionId = selectionid
        Me.OpenDefaultScreen = openDefaultScreen
        DefaultScreenSelectionId = defaultselectionid
        Me.DontOpenTaskIfIsAsociatedToWF = DontOpenTaskIfIsAsociatedToWF
        _haveSpecificAttributes = haveSpecificAttributes
        _specificAttrubutes = specificAttributes
        playRule = New Zamba.WFExecution.PlayDOAddAsociatedDocument(Me)
    End Sub
#End Region

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
    ''' <summary>
    ''' Marca si la regla utilizara la configuracion para atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HaveSpecificAttributes As Boolean Implements Core.IDoAddAsociatedDocument.HaveSpecificAttributes
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
    Public Property SpecificAttrubutes As String Implements Core.IDoAddAsociatedDocument.SpecificAttrubutes
        Get
            Return _specificAttrubutes
        End Get
        Set(ByVal value As String)
            _specificAttrubutes = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    'Public Overrides Function Play(ByVal results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Asociar Nuevos Documentos"
        End Get
    End Property
End Class
