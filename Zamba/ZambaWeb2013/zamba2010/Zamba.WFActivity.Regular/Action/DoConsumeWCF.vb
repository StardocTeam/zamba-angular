Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic
Imports System.Xml.Serialization

<RuleCategory("Datos"), RuleDescription("Consumir un servicio WCF"), RuleHelp("Consume un servicio WCF especificando los valores de los parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoConsumeWCF
    Inherits WFRuleParent
    Implements IDoConsumeWCF ', IRuleValidate

    Private _methodName As String
    Private _wsdl As String
    Private _params As ArrayList
    Private _paramsTypes As String
    Private _SaveInValue As String
    Private _useCredentials As Boolean
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoConsumeWCF
    'Private _isValid As Boolean
    Private _contract As String

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

    Public Property useCredentials() As Boolean Implements Core.IDoConsumeWCF.useCredentials
        Get
            Return _useCredentials
        End Get
        Set(ByVal value As Boolean)
            _useCredentials = value
        End Set
    End Property

    Public Property SaveInValue() As String Implements Core.IDoConsumeWCF.SaveInValue
        Get
            Return _SaveInValue
        End Get
        Set(ByVal value As String)
            _SaveInValue = value
        End Set
    End Property

    Public Property MethodName() As String Implements Core.IDoConsumeWCF.MethodName
        Get
            Return _methodName
        End Get
        Set(ByVal value As String)
            _methodName = value
        End Set
    End Property

    Public Property Param() As ArrayList Implements Core.IDoConsumeWCF.Param
        Get
            Return _params
        End Get
        Set(ByVal value As ArrayList)
            _params = value
        End Set
    End Property

    Public Property ParamTypes() As String Implements Core.IDoConsumeWCF.ParamTypes
        Get
            Return _paramsTypes
        End Get
        Set(ByVal value As String)
            _paramsTypes = value
        End Set
    End Property

    Public Property Wsdl() As String Implements Core.IDoConsumeWCF.Wsdl
        Get
            Return _wsdl
        End Get
        Set(ByVal value As String)
            _wsdl = value
        End Set
    End Property


    'Public Property IsValid As Boolean Implements IRuleValidate.isValid
    '    Get
    '        Return _isValid
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _isValid = value
    '    End Set
    'End Property

    Public Property Contract() As String Implements Core.IDoConsumeWCF.Contract
        Get
            Return _contract
        End Get
        Set(ByVal value As String)
            _contract = value
        End Set
    End Property

    ''' <summary>
    ''' Constructor de la regla
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Name"></param>
    ''' <param name="Wfstepid"></param>
    ''' <param name="methodName"></param>
    ''' <param name="wsdl"></param>
    ''' <param name="params"></param>
    ''' <param name="SaveInValue"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal methodName As String, ByVal wsdl As String, ByVal params As String, ByVal SaveInValue As String, ByVal badValue As String, ByVal UseCredentials As Boolean, ByVal Contract As String)
        MyBase.New(Id, Name, Wfstepid)
        _methodName = methodName
        _wsdl = wsdl
        _params = New ArrayList()
        '        _paramsTypes = paramsTypes

        If (Not String.IsNullOrEmpty(params.Trim())) Then
            For Each Param As String In params.Split(";")
                _params.Add(Param)
            Next
        End If

        'Si hay un parametro de mas en la DoConsume, es un error que se metio en codigo y hubo que armar esto para saltearlo 
        If String.IsNullOrEmpty(badValue) Then
            _SaveInValue = SaveInValue
        Else
            _SaveInValue = badValue
        End If

        _useCredentials = UseCredentials
        _contract = Contract
        Me.playRule = New Zamba.WFExecution.PlayDoConsumeWCF(Me)
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub

     Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function


    Public Overloads Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class
