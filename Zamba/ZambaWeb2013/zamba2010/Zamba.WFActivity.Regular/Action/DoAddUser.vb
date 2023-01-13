Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Usuarios"), RuleDescription("Crea un nuevo usuario"), RuleHelp("Crea un nuevo usuario con sus atributos"), RuleFeatures(False)> <Serializable()>
Public Class DoAddUser
    Inherits WFRuleParent
    Implements IDoAddUser
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _nameUser As String
    Private _name As String
    Private _apellido As String
    Private _telefono As String
    Private _email As String
    Private _avatar As String
    Private _password As String
    Private _puesto As String
    Private _var As String
    Private playRule As Zamba.WFExecution.PlayDoAddUser

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal nombre As String, ByVal surname As String, ByVal usrName As String, ByVal contraseña As String, ByVal telephone As String, ByVal mail As String, ByVal picture As String, ByVal sector As String, ByVal var As String)
        MyBase.New(Id, Name, wfstepid)
        Me.nombre = nombre
        Me.apellido = surname
        Me.nameUser = usrName
        Me.password = contraseña
        Me.telefono = telephone
        Me.email = mail
        Me.avatar = picture
        Me.puesto = sector
        Me.varUsr = var
        Me.playRule = New Zamba.WFExecution.PlayDoAddUser(Me)
    End Sub

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

    Public Property nameUser As String Implements IDoAddUser.nameUser
        Get
            Return _nameUser
        End Get
        Set(ByVal value As String)
            _nameUser = value
        End Set
    End Property

    Private Property nombre As String Implements IDoAddUser.nombre
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property apellido As String Implements IDoAddUser.apellido
        Get
            Return _apellido
        End Get
        Set(ByVal value As String)
            _apellido = value
        End Set
    End Property

    Public Property telefono As String Implements IDoAddUser.telefono
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property

    Public Property email As String Implements IDoAddUser.email
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Public Property avatar As String Implements IDoAddUser.avatar
        Get
            Return _avatar
        End Get
        Set(ByVal value As String)
            _avatar = value
        End Set
    End Property

    Public Property password As String Implements IDoAddUser.password
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Public Property puesto As String Implements IDoAddUser.puesto
        Get
            Return _puesto
        End Get
        Set(ByVal value As String)
            _puesto = value
        End Set
    End Property

    Public Property varUsr As String Implements IDoAddUser.varUsr
        Get
            Return _var
        End Get
        Set(ByVal value As String)
            _var = value
        End Set
    End Property

    Public Overrides Sub Dispose()
    End Sub

    Public Overrides Sub FullLoad()
    End Sub

    Public Overrides Sub Load()
    End Sub

    Public Overrides Function Play(results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides Function PlayWeb(results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class
