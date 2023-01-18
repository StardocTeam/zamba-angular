Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Web"), RuleDescription("Consumir un servicio web"), RuleHelp("Consume un servicio web especificando los valores de los parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoConsumeWebService
    Inherits WFRuleParent
    Implements IDoConsumeWebService, IRuleValidate

    Private _methodName As String
    Private _wsdl As String
    Private _params As ArrayList
    Private _paramsNT As ArrayList
    Private _paramsTypes As String
    Private _SaveInValue As String
    Private _useCredentials As Boolean
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoConsumeWebService
    Private _isValid As Boolean

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

    Public Property useCredentials() As Boolean Implements Core.IDoConsumeWebService.useCredentials
        Get
            Return _useCredentials
        End Get
        Set(ByVal value As Boolean)
            _useCredentials = value
        End Set
    End Property

    Public Property SaveInValue() As String Implements Core.IDoConsumeWebService.SaveInValue
        Get
            Return _SaveInValue
        End Get
        Set(ByVal value As String)
            _SaveInValue = value
        End Set
    End Property

    Public Property MethodName() As String Implements Core.IDoConsumeWebService.MethodName
        Get
            Return _methodName
        End Get
        Set(ByVal value As String)
            _methodName = value
        End Set
    End Property

    Public Property Param() As ArrayList Implements Core.IDoConsumeWebService.Param
        Get
            Return _params
        End Get
        Set(ByVal value As ArrayList)
            _params = value
        End Set
    End Property
    Public Property ParamNT() As ArrayList Implements Core.IDoConsumeWebService.ParamNT
        Get
            Return _paramsNT
        End Get
        Set(ByVal value As ArrayList)
            _paramsNT = value
        End Set
    End Property
    Public Property ParamTypes() As String Implements Core.IDoConsumeWebService.ParamTypes
        Get
            Return _paramsTypes
        End Get
        Set(ByVal value As String)
            _paramsTypes = value
        End Set
    End Property

    Public Property Wsdl() As String Implements Core.IDoConsumeWebService.Wsdl
        Get
            Return _wsdl
        End Get
        Set(ByVal value As String)
            _wsdl = value
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



    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal methodName As String, ByVal wsdl As String, ByVal params As String, ByVal SaveInValue As String, ByVal badValue As String, ByVal UseCredentials As Boolean, ByVal paramsNT As String)
        MyBase.New(Id, Name, Wfstepid)
        _methodName = methodName
        _wsdl = wsdl
        _params = New ArrayList()
        _paramsNT = New ArrayList()
        '        _paramsTypes = paramsTypes

        If (Not String.IsNullOrEmpty(params.Trim())) Then
            For Each Param As String In params.Split(";")
                _params.Add(Param)
            Next
        End If
        If (Not String.IsNullOrEmpty(paramsNT.Trim())) Then
            For Each ParamNT As String In paramsNT.Split(";")
                _paramsNT.Add(ParamNT)
            Next
        End If

        'Si hay un parametro de mas en la DoConsume, es un error que se metio en codigo y hubo que armar esto para saltearlo 
        If String.IsNullOrEmpty(badValue) Then
            _SaveInValue = SaveInValue
        Else
            _SaveInValue = badValue
        End If

        _useCredentials = UseCredentials
        playRule = New Zamba.WFExecution.PlayDoConsumeWebService(Me)
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

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
