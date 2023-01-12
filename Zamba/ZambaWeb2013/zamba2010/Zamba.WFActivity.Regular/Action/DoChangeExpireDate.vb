Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DoChangeExpireDate
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla que permite modificar la fecha de vencimiento de una tarea
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Tareas"), RuleDescription("Modificar Vencimiento"), RuleHelp("Permite modificar el vencimiento de la tarea"), RuleFeatures(False)> <Serializable()> _
Public Class DoChangeExpireDate
    Inherits WFRuleParent
    Implements IDoChangeExpireDate
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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    'TODO Martin, completar los parametros

    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="Id"></param>
    '''' <param name="Name"></param>
    '''' <param name="WFStep"></param>
    '''' <param name="Direccion1"></param>
    '''' <param name="Direccion2"></param>
    '''' <param name="Direccion3"></param>
    '''' <param name="Direccion4"></param>
    '''' <param name="Direccion5"></param>
    '''' <param name="Value1"></param>
    '''' <param name="Value2"></param>
    '''' <param name="Value3"></param>
    '''' <param name="Value4"></param>
    '''' <param name="Value5"></param>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	29/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Direccion1 As Int32, ByVal Direccion2 As Int32, ByVal Direccion3 As Int32, ByVal Direccion4 As Int32, ByVal Direccion5 As Int32, ByVal Value1 As Int32, ByVal Value2 As Int32, ByVal Value3 As Int32, ByVal Value4 As Int32, ByVal Value5 As Int32)
        MyBase.New(Id, Name, wfstepId) ', ListofRules.DoChangeExpireDate)
        Me._Direccion1 = Direccion1
        Me._Direccion2 = Direccion2
        Me._Direccion3 = Direccion3
        Me._Direccion4 = Direccion4
        Me._Direccion5 = Direccion5
        Me._Value1 = Value1
        Me._Value2 = Value2
        Me._Value3 = Value3
        Me._Value4 = Value4
        Me._Value5 = Value5
    End Sub
    '--ITEMS--
    'StateId=0

    'Properties
    Public Property Direccion1() As Int32 Implements IDoChangeExpireDate.Direccion1
        Get
            Return _Direccion1
        End Get
        Set(ByVal value As Int32)
            _Direccion1 = value
        End Set
    End Property
    Private _Direccion1 As Int32
    Public Property Direccion2() As Int32 Implements IDoChangeExpireDate.Direccion2
        Get
            Return _Direccion2
        End Get
        Set(ByVal value As Int32)
            _Direccion2 = value
        End Set
    End Property
    Private _Direccion2 As Int32
    Public Property Direccion3() As Int32 Implements IDoChangeExpireDate.Direccion3
        Get
            Return _Direccion3
        End Get
        Set(ByVal value As Int32)
            _Direccion3 = value
        End Set
    End Property
    Private _Direccion3 As Int32
    Public Property Direccion4() As Int32 Implements IDoChangeExpireDate.Direccion4
        Get
            Return _Direccion4
        End Get
        Set(ByVal value As Int32)
            _Direccion4 = value
        End Set
    End Property
    Private _Direccion4 As Int32
    Public Property Direccion5() As Int32 Implements IDoChangeExpireDate.Direccion5
        Get
            Return _Direccion5
        End Get
        Set(ByVal value As Int32)
            _Direccion5 = value
        End Set
    End Property
    Private _Direccion5 As Int32

    Public Property Value1() As Int32 Implements IDoChangeExpireDate.Value1
        Get
            Return _Value1
        End Get
        Set(ByVal value As Int32)
            _Value1 = value
        End Set
    End Property
    Private _Value1 As Int32
    Public Property Value2() As Int32 Implements IDoChangeExpireDate.Value2
        Get
            Return _Value2
        End Get
        Set(ByVal value As Int32)
            _Value2 = value
        End Set
    End Property
    Private _Value2 As Int32
    Public Property Value3() As Int32 Implements IDoChangeExpireDate.Value3
        Get
            Return _Value3
        End Get
        Set(ByVal value As Int32)
            _Value3 = value
        End Set
    End Property
    Private _Value3 As Int32
    Public Property Value4() As Int32 Implements IDoChangeExpireDate.Value4
        Get
            Return _Value4
        End Get
        Set(ByVal value As Int32)
            _Value4 = value
        End Set
    End Property
    Private _Value4 As Int32
    Public Property Value5() As Int32 Implements IDoChangeExpireDate.Value5
        Get
            Return _Value5
        End Get
        Set(ByVal value As Int32)
            _Value5 = value
        End Set
    End Property
    Private _Value5 As Int32

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoChangeExpireDate()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playrule As New WFExecution.PlayDoChangeExpireDate()
        Return playrule.Play(results, Me)
    End Function
    'Protected Overloads Overrides Function Play(ByVal Result As TaskResult) As TaskResult
    '    Try
    '        If IsNothing(result.ExpireDate) = False Then
    '            Dim NuevaFecha As DateTime = result.ExpireDate

    '            Dim Minutos As Int32 = Value1
    '            Dim Horas As Int32 = Value2
    '            Dim Dias As Int32 = Value3
    '            Dim Semanas As Int32 = Value4
    '            Dim Meses As Int32 = Value5

    '            If Direccion1 = 1 Then
    '                Minutos = -Minutos
    '            End If
    '            If Direccion2 = 1 Then
    '                Horas = -Horas
    '            End If
    '            If Direccion3 = 1 Then
    '                Dias = -Dias
    '            End If
    '            If Direccion4 = 1 Then
    '                Semanas = -Semanas
    '            End If
    '            If Direccion5 = 1 Then
    '                Meses = -Meses
    '            End If

    '            NuevaFecha = NuevaFecha.AddMinutes(Minutos)
    '            NuevaFecha = NuevaFecha.AddHours(Horas)
    '            NuevaFecha = NuevaFecha.AddDays(Dias)
    '            NuevaFecha = NuevaFecha.AddDays(Semanas * 7)
    '            NuevaFecha = NuevaFecha.AddMonths(Meses)

    '            WFBusiness.ChangeExpireDateTask(result, NuevaFecha)
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Function

End Class