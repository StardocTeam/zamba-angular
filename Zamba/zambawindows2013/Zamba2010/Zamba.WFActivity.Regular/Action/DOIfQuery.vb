Imports zamba.Core


<RuleMainCategory("Base de Datos"), RuleCategory("Consultas"), RuleSubCategory(""), RuleDescription("Consuta SQL condicional"), RuleHelp("Permite realizar consultas SQL en la base de datos del tipo condicional. En la cual dependiendo del resultado de la mismas se ejecuta otra consulta por verdadero o falso "), RuleFeatures(False)> <Serializable()> _
Public Class DOIfQuery
    Inherits WFRuleParent
    Implements IDOIfQuery
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _compType As Comparadores
    Private _executeType As String
    Private playRule As Zamba.WFExecution.PlayDOIfQuery

    Public Property CompType() As Comparadores Implements IDOIfQuery.CompType
        Get
            Return _compType
        End Get
        Set(ByVal value As Comparadores)
            _compType = value
        End Set
    End Property
    Public Property ExecuteType() As String Implements IDOIfQuery.ExecuteType
        Get
            Return _executeType
        End Get
        Set(ByVal value As String)
            _executeType = value
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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Private _SQL As String
    Private _SQL2 As String
    Private _IFSQL As String
    Private _IFValue As String
    Private _HashTable As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal pSql As String, ByVal pSql2 As String, ByVal pIfSql As String, ByVal pIFValue As String, ByVal pHashTable As String, ByVal compType As Comparadores, ByVal exType As String)
        MyBase.New(Id, Name, wfStepId)
        _SQL = pSql
        _IFSQL = pIfSql
        _IFValue = pIFValue
        _SQL2 = pSql2
        _HashTable = pHashTable
        _executeType = exType
        _compType = compType
        playRule = New Zamba.WFExecution.PlayDOIfQuery(Me)
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

    Public Property SQL() As String Implements IDOIfQuery.SQL
        Get
            Return _SQL
        End Get
        Set(ByVal value As String)
            _SQL = value
        End Set
    End Property
    Public Property SQL2() As String Implements IDOIfQuery.SQL2
        Get
            Return _SQL2
        End Get
        Set(ByVal value As String)
            _SQL2 = value
        End Set
    End Property
    Public Property IFSQL() As String Implements IDOIfQuery.IFSQL
        Get
            Return _IFSQL
        End Get
        Set(ByVal value As String)
            _IFSQL = value
        End Set
    End Property
    Public Property IFVALUE() As String Implements IDOIfQuery.IFValue
        Get
            Return _IFValue
        End Get
        Set(ByVal value As String)
            _IFValue = value
        End Set
    End Property
    Public Property HashTable() As String Implements IDOIfQuery.HashTable
        Get
            Return _HashTable
        End Get
        Set(ByVal value As String)
            _HashTable = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class