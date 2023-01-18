Imports Zamba.Core


<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Generar Plantilla de Word"), RuleHelp("Crea un documento a partir de la entidad seleccionado y al mismo le asigna la plantilla de word seleccionada."), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateBarcodeInWord
    Inherits WFRuleParent
    Implements IDoGenerateBarcodeInWord, IRuleValidate


#Region "Atributos"

    Private playRule As Zamba.WFExecution.PlayDoGenerateBarcodeInWord
    Private _docTypeId As Int64
    Private _Indices As String
    Private _FilePath As String
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _top As String
    Private _left As String
    Private _continueWithCurrentTasks As Boolean
    Private _autoPrint As Boolean
    Private _insertBarcode As Boolean
    Private _docPathVar As String
    Private _saveDocPathVar As Boolean
    Private _withoutInsert As Boolean
    Private _isValid As Boolean

#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal docTypeId As Int64, ByVal atributos As String, ByVal filePath As String, ByVal top As String, ByVal left As String, ByVal continueWithCurrentTasks As Boolean, ByVal autoPrint As Boolean, ByVal insertBarcode As Boolean, ByVal saveDocPathVar As Boolean, ByVal DocPathVar As String, ByVal withoutInsert As Boolean)
        MyBase.New(Id, Name, wfstepId)
        _docTypeId = docTypeId
        _FilePath = filePath
        _Indices = atributos
        _left = left
        _top = top
        _continueWithCurrentTasks = continueWithCurrentTasks
        _autoPrint = autoPrint
        _insertBarcode = insertBarcode
        _saveDocPathVar = saveDocPathVar
        _docPathVar = DocPathVar
        _withoutInsert = withoutInsert
        playRule = New Zamba.WFExecution.PlayDoGenerateBarcodeInWord(Me)
    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Overrides Sub Load()

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

    Public Property docTypeId() As Long Implements Core.IDoGenerateBarcodeInWord.docTypeId
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Long)
            _docTypeId = value
        End Set
    End Property

    Public Property FilePath() As String Implements Core.IDoGenerateBarcodeInWord.FilePath
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property

    Public Property Indices() As String Implements Core.IDoGenerateBarcodeInWord.Indices
        Get
            Return _Indices
        End Get
        Set(ByVal value As String)
            _Indices = value
        End Set
    End Property

    Public Property Top() As String Implements Core.IDoGenerateBarcodeInWord.Top
        Get
            Return _top
        End Get
        Set(ByVal value As String)
            _top = value
        End Set
    End Property

    Public Property Left() As String Implements Core.IDoGenerateBarcodeInWord.Left
        Get
            Return _left
        End Get
        Set(ByVal value As String)
            _left = value
        End Set
    End Property

    Public Property AutoPrint() As Boolean Implements Core.IDoGenerateBarcodeInWord.AutoPrint
        Get
            Return _autoPrint
        End Get
        Set(ByVal value As Boolean)
            _autoPrint = value
        End Set
    End Property

    Public Property ContinueWithCurrentTasks() As Boolean Implements Core.IDoGenerateBarcodeInWord.ContinueWithCurrentTasks
        Get
            Return _continueWithCurrentTasks
        End Get
        Set(ByVal value As Boolean)
            _continueWithCurrentTasks = value
        End Set
    End Property

    Public Property InsertBarcode() As Boolean Implements Core.IDoGenerateBarcodeInWord.InsertBarcode
        Get
            Return _insertBarcode
        End Get
        Set(ByVal value As Boolean)
            _insertBarcode = value
        End Set
    End Property

    Public Property DocPathVar() As String Implements Core.IDoGenerateBarcodeInWord.DocPathVar
        Get
            Return _docPathVar
        End Get
        Set(ByVal value As String)
            _docPathVar = value
        End Set
    End Property

    Public Property SaveDocPathVar() As Boolean Implements Core.IDoGenerateBarcodeInWord.SaveDocPathVar
        Get
            Return _saveDocPathVar
        End Get
        Set(ByVal value As Boolean)
            _saveDocPathVar = value
        End Set
    End Property

    Public Property WithoutInsert() As Boolean Implements Core.IDoGenerateBarcodeInWord.WithoutInsert
        Get
            Return _withoutInsert
        End Get
        Set(ByVal value As Boolean)
            _withoutInsert = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
