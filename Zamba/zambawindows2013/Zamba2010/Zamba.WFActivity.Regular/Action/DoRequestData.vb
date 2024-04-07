Imports Zamba.Core

<RuleMainCategory("Atributos"), RuleCategory("Completar"), RuleSubCategory(""), RuleDescription("Solicitar varios atributos"), RuleHelp("Permite solicitar al usuario los datos de los atributos de un entidad seleccionado o de un indice en particular"), RuleFeatures(True)> <Serializable()> _
Public Class DoRequestData
    Inherits WFRuleParent
    Implements IDoRequestData, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoRequestData
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocTypeId As Int64, ByVal Ids As String)
        MyBase.New(Id, Name, wfstepid)

        Me.DocTypeId = DocTypeId
        ArrayIds = New List(Of Int64)

        For Each Item As String In Ids.Split("*")
            If Item.Length > 0 Then
                ArrayIds.Add(Int64.Parse(Item))
            End If
        Next
        playRule = New Zamba.WFExecution.PlayDoRequestData(Me)
    End Sub

    Private _docTypeId As Int64
    Private _arrayIds As List(Of Int64)

    Public Function JoinIds() As String Implements IDoRequestData.JoinIds
        Dim Str As String = ""
        For Each Item As Int64 In ArrayIds
            Str += "*" & Item
        Next
        If Str <> String.Empty Then
            Str = Str.Substring(1)
        End If
        Return Str
    End Function

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

    Public Property DocTypeId() As Int64 Implements IDoRequestData.DocTypeId
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Int64)
            _docTypeId = value
        End Set
    End Property
    Public Property ArrayIds() As List(Of Int64) Implements IDoRequestData.ArrayIds
        Get
            Return _arrayIds
        End Get
        Set(ByVal value As List(Of Int64))
            _arrayIds = value
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

    Public Overrides Sub Dispose()

    End Sub
End Class
