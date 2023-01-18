Imports Zamba.Core

<RuleMainCategory("Documentos y Asociados"), RuleCategory("Documentos Asociados"), RuleSubCategory("Asociar nuevo"), RuleDescription("Asociar nuevos formularios"), RuleHelp("Permite agregar un nuevo formulario asociado a un documento"), RuleFeatures(True)> <Serializable()> _
Public Class DoAddAsociatedForm
    Inherits WFRuleParent
    Implements IDoAddAsociatedForm, IRuleValidate

    Private _formID As Long
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _continueWithCurrentTasks As Boolean
    Private _dontOpenTaskAfterInsert As Boolean
    Private _fillCommonAttributes As Boolean
    Private _haveSpecificAttributes As Boolean
    Private _specificAttrubutes As String
    Private _isValid As Boolean

    Private playRule As Zamba.WFExecution.PlayDoAddAsociatedForm

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

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

    Public Property ContinueWithCurrentTasks As Boolean Implements Core.IDoAddAsociatedForm.ContinueWithCurrentTasks
        Get
            Return _continueWithCurrentTasks
        End Get
        Set(value As Boolean)
            _continueWithCurrentTasks = value
        End Set
    End Property

    Public Property DontOpenTaskAfterInsert As Boolean Implements Core.IDoAddAsociatedForm.DontOpenTaskAfterInsert
        Get
            Return _dontOpenTaskAfterInsert
        End Get
        Set(value As Boolean)
            _dontOpenTaskAfterInsert = value
        End Set
    End Property

    Public Property FillCommonAttributes As Boolean Implements Core.IDoAddAsociatedForm.FillCommonAttributes
        Get
            Return _fillCommonAttributes
        End Get
        Set(value As Boolean)
            _fillCommonAttributes = value
        End Set
    End Property
    ''' <summary>
    ''' Marca si la regla utilizara la configuracion para atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HaveSpecificAttributes As Boolean Implements Core.IDoAddAsociatedForm.HaveSpecificAttributes
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
    Public Property SpecificAttrubutes As String Implements Core.IDoAddAsociatedForm.SpecificAttrubutes
        Get
            Return _specificAttrubutes
        End Get
        Set(ByVal value As String)
            _specificAttrubutes = value
        End Set
    End Property

    Public Property FormID() As Long Implements IDoAddAsociatedForm.FormID
        Get
            Return _formID
        End Get
        Set(ByVal value As Long)
            _formID = value
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

    Public Overrides Sub Load()

    End Sub

    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepId As Int64, ByVal FormID As Long, ByVal ContinueWithCurrentTasks As Boolean, ByVal DontOpenTaskAfterInsert As Boolean, _
                   ByVal FillCommonAttributes As Boolean, ByVal haveSpecificAttributes As Boolean, ByVal specificAttributes As String)
        MyBase.New(ruleID, ruleName, wfstepId)

        _formID = FormID
        _continueWithCurrentTasks = ContinueWithCurrentTasks
        _dontOpenTaskAfterInsert = DontOpenTaskAfterInsert
        _fillCommonAttributes = FillCommonAttributes
        _haveSpecificAttributes = haveSpecificAttributes
        _specificAttrubutes = specificAttributes
        playRule = New Zamba.WFExecution.PlayDoAddAsociatedForm(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function


End Class
