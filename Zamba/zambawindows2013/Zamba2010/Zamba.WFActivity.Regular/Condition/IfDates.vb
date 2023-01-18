Imports zamba.core

<RuleMainCategory("Tareas"), RuleCategory("Vencimientos"), RuleSubCategory(""), RuleDescription("Validar Fecha de Tarea"), RuleHelp("Valida Fecha y Hora de una tarea"), RuleFeatures(False)> _
Public Class IfDates
    Inherits WFRuleParent
    Implements IIfDates, IRuleIFPlay, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayIfDates
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
        playRule = New WFExecution.PlayIfDates(Me)
        FechaTarea = FechaDoc
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Fecha de la Tarea: " & FechaTarea.ToString)
        Comp = Comparador
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparador: " & Comp.ToString)
        TipoComp = TipoComparacion
        If ValorComparativo = "" Then
            Select Case TipoComp
                Case TipoComparaciones.Dias
                    Dias = 0
                Case TipoComparaciones.Especifica
                Case TipoComparaciones.Fija
                    FechaFija = DateTime.Now
                Case TipoComparaciones.Horas
                    Horas = 0
                Case Else
                    Dim ex As New Exception("Tipo de comparación no valida")
                    Throw ex
            End Select
        Else
            Select Case TipoComp
                Case TipoComparaciones.Dias
                    Dias = Integer.Parse(ValorComparativo)
                Case TipoComparaciones.Especifica
                Case TipoComparaciones.Fija
                    FechaFija = DateTime.Parse(ValorComparativo)
                Case TipoComparaciones.Horas
                    Horas = Integer.Parse(ValorComparativo)
                Case Else
                    Dim ex As New Exception("Tipo de comparación no valida")
                    Throw ex
            End Select
        End If
        sNombre = "Valido la fecha de " & FechaTarea.ToString
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
        Return playRule.Play(results, ifType)
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
#End Region
End Class

