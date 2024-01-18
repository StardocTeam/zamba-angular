Imports Zamba.Core

<RuleMainCategory("Base de Datos"), RuleCategory("Consultas"), RuleSubCategory(""), RuleDescription("Ejecutar consulta a Base de Datos"), RuleHelp("Permite realizar una consulta a una Base de Datos y, en caso de que la misma devuelva resultados, poder almacenarlos en variables para utilizarlos en reglas hijas."), RuleFeatures(False)> <Serializable()> _
Public Class DOSELECT
    Inherits WFRuleParent
    Implements IDOSELECT, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOSELECT
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
    Private _servidor As String
    Private _dbuser As String
    Private _dbpassword As String
    Private _dbname As String
    Private _servertype As Int32
    Private _Name As String
    Private _SQL As String
    Private _HashTable As String
    Private _ExecuteType As String = "ESCALAR"
    Private _isValid As Boolean

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal pSQLName As String, ByVal pSql As String, ByVal pHashTable As String, ByVal pServerName As String, ByVal pDBName As String, ByVal pUserName As String, ByVal pPassword As String, ByVal pServerType As Integer, ByVal pExecuteType As String)
        MyBase.New(Id, Name, wfStepId)
        _Name = pSQLName
        _SQL = pSql
        _HashTable = pHashTable
        _servertype = pServerType
        _dbuser = pUserName
        _dbpassword = pPassword
        _dbname = pDBName
        _servidor = pServerName
        _ExecuteType = pExecuteType
        playRule = New Zamba.WFExecution.PlayDOSELECT(Me)
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


    Public Property Dbuser() As String Implements IDOSELECT.Dbuser
        Get
            Return _dbuser
        End Get
        Set(ByVal value As String)
            _dbuser = value
        End Set
    End Property
    Public Property Dbname() As String Implements IDOSELECT.Dbname
        Get
            Return _dbname
        End Get
        Set(ByVal value As String)
            _dbname = value
        End Set
    End Property
    Public Property Dbpassword() As String Implements IDOSELECT.Dbpassword
        Get
            Return _dbpassword
        End Get
        Set(ByVal value As String)
            _dbpassword = value
        End Set
    End Property
    Public Property SqlName() As String Implements IDOSELECT.SQLName
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property HashTable() As String Implements IDOSELECT.HashTable
        Get
            Return _HashTable
        End Get
        Set(ByVal value As String)
            _HashTable = value
        End Set
    End Property
    Public Property Servertype() As Int32 Implements IDOSELECT.Servertype
        Get
            Return _servertype
        End Get
        Set(ByVal value As Int32)
            _servertype = value
        End Set
    End Property
    Public Property SQL() As String Implements IDOSELECT.SQL
        Get
            Return _SQL
        End Get
        Set(ByVal value As String)
            _SQL = value
        End Set
    End Property
    Public Property Servidor() As String Implements IDOSELECT.Servidor
        Get
            Return _servidor
        End Get
        Set(ByVal value As String)
            _servidor = value
        End Set
    End Property
    Public Property ExecuteType() As String Implements IDOSELECT.ExecuteType
        Get
            Return _ExecuteType
        End Get
        Set(ByVal value As String)
            _ExecuteType = value
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
End Class

'    Public Overrides Function PrivateCheck() As Boolean
'        Try
'            Dim i As Int32
'            '    If Document.DocTypeId = DocTypeId Then
'            Task.Indexs = Results_Factory.GetIndexs(Task, True)
'            For i = 0 To Me.ReplaceIndexs.Count - 1
'                Dim h As Int32
'                For h = 0 To Task.Indexs.Count - 1
'                    If Me.ReplaceIndexs(i) = "<<" & Task.Indexs(h).Name & ">>" Then
'                        Dim IndexData As String = Task.Indexs(h).data
'                        strSelect = strSelect.Replace(Me.ReplaceIndexs(i), IndexData)
'                    End If
'                Next
'            Next
'            'Ver el tema de instanciar la conexion elegida
'            If Me.dbuser <> "" And Me.dbname <> "" And Me.dbpassword <> "" Then
'                Try
'                    Dim server As New server
'                    server.MakeConnection(Me.servertype, Me.servidor, Me.dbname, Me.dbuser, Me.dbpassword)
'                    Dim conex As IConnection
'                    conex = server.Con
'                    Dim Dt As New DataTable
'                    Dt = server.Con.ExecuteDataset(CommandType.Text, strSelect).Tables(0)
'                    Me.HashTable.Add(Ht, Dt)
'                    Return True
'                Catch ex As Exception
'                   zclass.raiseerror(ex)
'                    Return False
'                End Try
'            Else
'                Me.HashTable.Add(Ht, server.Con.ExecuteDataset(CommandType.Text, strSelect))
'                Return True
'            End If
'            '   Else
'            '     Return False
'            '  End If
'        Catch ex As Exception
'           zclass.raiseerror(ex)
'            Return False
'        End Try
'    End Function
'#End Region

'#Region "Propietary"
'    Dim DocTypeName As String
'    Dim DocTypeId As String
'    Dim strSelect As String
'    Dim ReplaceIndexs As New ArrayList
'    Dim servidor, dbuser, dbpassword, dbname As String
'    Dim servertype As Zamba.Servers.DBTYPES
'    Dim Ht As String

'#End Region

'End Class