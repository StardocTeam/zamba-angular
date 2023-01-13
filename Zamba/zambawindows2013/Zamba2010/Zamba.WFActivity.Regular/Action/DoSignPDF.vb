Imports Zamba.Core
Imports Zamba.WFExecution

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Firmar PDF"), RuleDescription("Firma archivos PDF mediante certificados"), RuleHelp("Certicación de  archivos PDF"), RuleFeatures(False)> <Serializable()>
Public Class DoSignPDF
    Inherits WFRuleParent
    Implements IDoSignPDF, IRuleValidate

#Region "Attributes"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As PlayDoSignPDF
    Private _isValid As Boolean

    Private _fullPath As String
    Private _fileName As String
    'PDF Metadata
    Private _author As String
    Private _title As String
    Private _subject As String
    Private _keywords As String
    Private _creator As String
    Private _producer As String
    'Certificate
    Private _certificate As String
    Private _password As String
    'Signature
    Private _reason As String
    Private _contact As String
    Private _location As String
    Private _writePDF As Boolean
#End Region

#Region "Properties"
    Public Property FullPath As String Implements IDoSignPDF.FullPath
        Get
            Return _fullPath
        End Get
        Set(ByVal value As String)
            _fullPath = value
        End Set
    End Property

    Public Property FileName As String Implements IDoSignPDF.FileName
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property

    Public Property Author As String Implements IDoSignPDF.Author
        Get
            Return _author
        End Get
        Set(ByVal value As String)
            _author = value
        End Set
    End Property

    Public Property Title As String Implements IDoSignPDF.Title
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property Subject As String Implements IDoSignPDF.Subject
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Public Property Keywords As String Implements IDoSignPDF.Keywords
        Get
            Return _keywords
        End Get
        Set(ByVal value As String)
            _keywords = value
        End Set
    End Property

    Public Property Creator As String Implements IDoSignPDF.Creator
        Get
            Return _creator
        End Get
        Set(ByVal value As String)
            _creator = value
        End Set
    End Property

    Public Property Producer As String Implements IDoSignPDF.Producer
        Get
            Return _producer
        End Get
        Set(ByVal value As String)
            _producer = value
        End Set
    End Property

    Public Property Certificate As String Implements IDoSignPDF.Certificate
        Get
            Return _certificate
        End Get
        Set(ByVal value As String)
            _certificate = value
        End Set
    End Property

    Public Property Password As String Implements IDoSignPDF.Password
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Public Property Reason As String Implements IDoSignPDF.Reason
        Get
            Return _reason
        End Get
        Set(ByVal value As String)
            _reason = value
        End Set
    End Property

    Public Property Contact As String Implements IDoSignPDF.Contact
        Get
            Return _contact
        End Get
        Set(ByVal value As String)
            _contact = value
        End Set
    End Property

    Public Property Location As String Implements IDoSignPDF.Location
        Get
            Return _location
        End Get
        Set(ByVal value As String)
            _location = value
        End Set
    End Property

    Public Property WritePDF As Boolean Implements IDoSignPDF.WritePDF
        Get
            Return _writePDF
        End Get
        Set(ByVal value As Boolean)
            _writePDF = value
        End Set
    End Property

    Public Overrides ReadOnly Property IsFull As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return _isLoaded
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            If FullPath <> String.Empty Then
                Return String.Format("Firma Digital de Documento {0}", New IO.FileInfo(FullPath).Name)
            Else
                Return "Firma Digital de Documento"
            End If
        End Get
    End Property
#End Region

#Region "Methods"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64,
                   ByVal FullPath As String, ByVal FileName As String,
                   ByVal Author As String, ByVal Title As String, ByVal Subject As String, ByVal Keywords As String,
                    ByVal Creator As String, ByVal Producer As String,
                   ByVal Certificate As String, ByVal Password As String,
                ByVal Reason As String, ByVal Contact As String, ByVal Location As String, ByVal WritePDF As Boolean)
        MyBase.New(Id, Name, wfstepId)

        _fullPath = FullPath
        _fileName = FileName

        _author = Author
        _title = Title
        _subject = Subject
        _keywords = Keywords
        _creator = Creator
        _producer = Producer

        _certificate = Certificate
        _password = Password

        _reason = Reason
        _contact = Contact
        _location = Location
        _writePDF = WritePDF

        playRule = New PlayDoSignPDF(Me)
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

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub
#End Region
End Class
