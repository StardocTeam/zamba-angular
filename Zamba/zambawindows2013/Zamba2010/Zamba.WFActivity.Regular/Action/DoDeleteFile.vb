Imports Zamba.Core
Imports System.Xml.Serialization

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Administrar"), RuleDescription("Eliminar Archivo"), RuleHelp("Permite eliminar un archivo especifico"), RuleFeatures(False)> <Serializable()> _
Public Class DoDeleteFile
    Inherits WFRuleParent
    Implements IDoDeleteFile, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoDeleteFile
    Private _isValid As Boolean
    Private _file As String
    Private _path As String

    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
        Me.playRule = New WFExecution.PlayDoDeleteFile(Me)
    End Sub

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


    Public Property File() As String Implements Core.IDoDeleteFile.File
        Get
            Return _file
        End Get
        Set(ByVal value As String)
            Me._file = value
        End Set
    End Property

    Public Property Path() As String Implements Core.IDoDeleteFile.Path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            Me._path = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Borro el archivo fisico"
        End Get
    End Property
End Class

