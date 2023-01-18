Imports zamba.core

<RuleCategory("Vencimientos"), RuleDescription("Validar Fecha de Tarea"), RuleHelp("Valida Fecha y Hora de una tarea"), RuleFeatures(False)> _
Public Class IfDates
    Inherits WFRuleParent
    Implements IIfDates, IRuleIFPlay

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
    Public Overrides Sub Dispose()

    End Sub
    Private Comp As Comparadores
    Private TipoComp As TipoComparaciones
    Private FechaFija As DateTime
    Private FechaTarea As TaskResult.DocumentDates
    Private FechaComp As TaskResult.DocumentDates
    Private Dias As Int32
    Private Horas As Int32
    Private sNombre As String
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal FechaDoc As TaskResult.DocumentDates, ByVal Comparador As Comparadores, ByVal TipoComparacion As TipoComparaciones, ByVal ValorComparativo As String)
        MyBase.New(Id, Name, wfstepid)

        Me.FechaTarea = FechaDoc
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Fecha de la Tarea: " & Me.FechaTarea.ToString)
        Me.Comp = Comparador
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparador: " & Me.Comp.ToString)
        Me.TipoComp = TipoComparacion
        If ValorComparativo = "" Then
            Select Case TipoComp
                Case TipoComparaciones.Dias
                    Me.Dias = 0
                Case TipoComparaciones.Especifica
                Case TipoComparaciones.Fija
                    Me.FechaFija = DateTime.Now
                Case TipoComparaciones.Horas
                    Me.Horas = 0
                Case Else
                    Dim ex As New Exception("Tipo de comparación no valida")
                    Throw ex
            End Select
        Else
            Select Case TipoComp
                Case TipoComparaciones.Dias
                    Me.Dias = Integer.Parse(ValorComparativo)
                Case TipoComparaciones.Especifica
                Case TipoComparaciones.Fija
                    Me.FechaFija = DateTime.Parse(ValorComparativo)
                Case TipoComparaciones.Horas
                    Me.Horas = Integer.Parse(ValorComparativo)
                Case Else
                    Dim ex As New Exception("Tipo de comparación no valida")
                    Throw ex
            End Select
        End If
        sNombre = "Valido la fecha de " & Me.FechaTarea.ToString
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfDates()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfDates()
        Return playRule.Play(results, Me)
    End Function
    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''       [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfDates()
        Return playRule.Play(results, Me, ifType)
    End Function

#Region "Properties"
    Public Property Comparador() As Comparadores Implements IIfDates.Comparador
        Get
            Return Comp
        End Get
        Set(ByVal Value As Comparadores)
            Comp = Value
        End Set
    End Property
    Public Property TipoComparacion() As TipoComparaciones Implements IIfDates.TipoComparacion
        Get
            Return TipoComp
        End Get
        Set(ByVal Value As TipoComparaciones)
            TipoComp = Value
        End Set
    End Property
    Public Property MiFecha() As DocumentDates Implements IIfDates.MiFecha
        Get
            Return FechaTarea
        End Get
        Set(ByVal Value As DocumentDates)
            FechaTarea = Value
        End Set
    End Property
    Public Property FechaDocumentoComparar() As DocumentDates Implements IIfDates.FechaDocumentoComparar
        Get
            Return FechaComp
        End Get
        Set(ByVal Value As DocumentDates)
            FechaComp = Value
        End Set
    End Property
    Public Property FechaAComparar() As DateTime Implements IIfDates.FechaAComparar
        Get
            Return FechaFija
        End Get
        Set(ByVal Value As DateTime)
            FechaFija = Value
        End Set
    End Property
    Public Property CantidadDias() As Int32 Implements IIfDates.CantidadDias
        Get
            Return Dias
        End Get
        Set(ByVal Value As Int32)
            Dias = Value
        End Set
    End Property
    Public Property CantidadHoras() As Int32 Implements IIfDates.CantidadHoras
        Get
            Return Horas
        End Get
        Set(ByVal Value As Int32)
            Horas = Value
        End Set
    End Property
#End Region
End Class

