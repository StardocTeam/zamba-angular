Imports Zamba.Core.Enumerators
Imports Zamba.Core

<RuleMainCategory("Zamba"), RuleCategory("Formularios"), RuleSubCategory(""), RuleDescription("Mostrar Fomulario"), RuleHelp("Permite mostrar un formulario a elección, pero previamente este debe estar generado con la regla 'crear formulario'"), RuleFeatures(True)> <Serializable()> _
Public Class DoShowForm
    Inherits WFRuleParent
    Implements IDoShowForm, IRuleValidate

#Region "Atributos"

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _formID As Int64
    Private _associatedDocDataShow As Boolean
    Private _varDocId As String
    Private _DontShowDialogMaximized As Boolean
    Private _ruleparenttype As TypesofRules
    Private _ViewOriginal As Boolean
    Private _ViewAsociatedDocs As Boolean
    Private _ControlBox As Boolean
    Private _CloseFormWindowAfterRuleExecution
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoShowForm
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
        Me.RuleParentType = RuleType
        Me.ViewOriginal = ViewOriginalDoc
        Me.ControlBox = CrossClose
        Me.CloseFormWindowAfterRuleExecution = CloseFormWindowAfterRuleExecution
        Me.ViewAsociatedDocs = ViewAsociatedDocuments
        Me.RefreshForm = RefreshForm
        playRule = New WFExecution.PlayDoShowForm(Me)
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
    ''' <summary>
    ''' Ver el tab de asociados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewAsociatedDocs() As Boolean Implements Core.IDoShowForm.ViewAsociatedDocs
        Get
            Return _ViewAsociatedDocs
        End Get
        Set(ByVal value As Boolean)
            _ViewAsociatedDocs = value
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
            Return _ruleparenttype
        End Get
        Set(ByVal value As TypesofRules)
            _ruleparenttype = value
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

#End Region

#Region "Métodos"

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
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

#End Region

End Class