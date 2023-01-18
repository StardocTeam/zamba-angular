Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic
Imports System.Xml.Serialization

<RuleCategory("Datos"), RuleDescription("Consumir un servicio web"), RuleHelp("Consume un servicio web especificando los valores de los parametros."), RuleFeatures(True)> <Serializable()> _
Public Class DoConsumeWebService_v2
    Inherits WFRuleParent
    Implements IDoConsumeWebService_v2

    Private _methodName As String
    Private _wsdl As String
    Private _params As ArrayList
    Private _paramsTypes As String
    Private _SaveInValue As String
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoConsumeWebService

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

    Public Property SaveInValue() As String Implements Core.IDoConsumeWebService_v2.SaveInValue
        Get
            Return _SaveInValue
        End Get
        Set(ByVal value As String)
            _SaveInValue = value
        End Set
    End Property

    Public Property MethodName() As String Implements Core.IDoConsumeWebService_v2.MethodName
        Get
            Return _methodName
        End Get
        Set(ByVal value As String)
            _methodName = value
        End Set
    End Property

    Public Property Param() As ArrayList Implements Core.IDoConsumeWebService_v2.Param
        Get
            Return _params
        End Get
        Set(ByVal value As ArrayList)
            _params = value
        End Set
    End Property

    Public Property ParamTypes() As String Implements Core.IDoConsumeWebService_v2.ParamTypes
        Get
            Return _paramsTypes
        End Get
        Set(ByVal value As String)
            _paramsTypes = value
        End Set
    End Property

    Public Property Wsdl() As String Implements Core.IDoConsumeWebService_v2.Wsdl
        Get
            Return _wsdl
        End Get
        Set(ByVal value As String)
            _wsdl = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName() As String 'Implements Core.IRule.MaskName
        Get
            Return "Consumir WebService"
        End Get
    End Property

    'Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal methodName As String, ByVal wsdl As String, ByVal params As ArrayList)
    '    MyBase.New(Id, Name, Wfstepid)
    '    _methodName = methodName
    '    _wsdl = wsdl
    '    _params = params

    'End Sub

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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal methodName As String, ByVal wsdl As String, ByVal params As String, ByVal paramsTypes As String, ByVal SaveInValue As String)
        MyBase.New(Id, Name, Wfstepid)
        _methodName = methodName
        _wsdl = wsdl
        _params = New ArrayList()
        _paramsTypes = paramsTypes

        If (Not String.IsNullOrEmpty(params.Trim())) Then
            For Each Param As String In params.Split(";")
                _params.Add(Param)
            Next
        End If

        _SaveInValue = SaveInValue
        Me.playRule = New Zamba.WFExecution.PlayDoConsumeWebService(Me)
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
End Class
