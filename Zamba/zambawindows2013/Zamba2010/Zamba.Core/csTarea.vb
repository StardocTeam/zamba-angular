Imports System.Timers

Public Class csTarea
    'Interfaces
    Implements IComparable, IcsTarea
    'Atributos
    Private TaskId As Int32
    Private Fecha As DateTime
    Private Frec As Int32
    Private FFin As DateTime
    Private UltimaRealizacion As DateTime
    Private Activo As Boolean
    Private Reporte As Int32
    Private TipoFormato As Int32
    Private CarpetaDestino As String
    Private MailPara As String
    Private MailCc As String
    Private Imprimir As Int32
    Private MailCco As String
    Private RepetirCadaMin As Int32
    Private Descrip As String
    Private Prog As iProgramador

    Private WithEvents Reloj As New Timer
    Private Const INTERVALOMINIMO As Int32 = 10000
    Private Shared CantidadTareas As Int32 = 0
    Public Event alarmaTarea() Implements IcsTarea.alarmaTarea

    Public Shared Function NuevaTarea(ByVal TareaId As Int32, ByVal FechaInicial As DateTime, ByVal Frecuencia As Int32, ByVal FechaFinal As DateTime, ByVal UltimaRealizacion As DateTime, ByVal Estado As Boolean, ByVal ReporteId As Int32, ByVal Formato As Int32, ByVal Carpeta As String, ByVal MailPara As String, ByVal MailCC As String, ByVal MailCco As String, ByVal Programador As iProgramador) As csTarea
        Dim Tarea As New csTarea
        Tarea.TaskId = TareaId
        Tarea.Fecha = FechaInicial
        Tarea.Frec = Frecuencia
        Tarea.FFin = FechaFinal
        Tarea.Activo = Estado
        Tarea.Reporte = ReporteId
        Tarea.Prog = Programador
        Tarea.TipoFormato = Formato
        Tarea.CarpetaDestino = Carpeta
        Tarea.MailPara = MailPara
        Tarea.MailCc = MailCC
        Tarea.MailCco = MailCco
        Tarea.UltimaRealizacion = UltimaRealizacion

        CantidadTareas += 1
        Return Tarea
    End Function
    Public Shared Function NuevaTarea(ByVal Fila As System.Data.DataRow, ByVal Programador As iProgramador) As csTarea
        Dim Tarea As New csTarea
        'Dim i As Int16

        Tarea.TaskId = Convert.ToInt32(Fila("TASK_ID"))
        Tarea.Fecha = Date.Parse(Fila("FECHA").ToString())
        Tarea.Frec = Convert.ToInt32(Fila("FRECUENCIA"))
        Tarea.FFin = Date.Parse(Fila("FECHA_FIN").ToString())
        Tarea.UltimaRealizacion = Date.Parse(Fila("ULTIMA").ToString())
        Tarea.Activo = Convert.ToBoolean(Fila("ACTIVO"))
        Tarea.Reporte = Convert.ToInt32(Fila("REPORTE"))
        Tarea.Prog = Programador
        Tarea.TipoFormato = Convert.ToInt32(Fila("FORMATO"))
        Tarea.CarpetaDestino = Fila("CARPETA").ToString()
        Tarea.MailPara = Fila("MAIL_TO").ToString()
        Tarea.MailCc = Fila("MAIL_CC").ToString()
        Tarea.MailCco = Fila("MAIL_CCO").ToString()
        Tarea.Imprimir = Convert.ToInt32(Fila("IMPRIMIR"))
        Tarea.RepetirCadaMin = Convert.ToInt32(Fila("MINUTOS"))
        Tarea.Descrip = Fila("DESCRIP").ToString()

        CantidadTareas += 1
        Return Tarea
    End Function
    Public Sub comenzarTarea() Implements IcsTarea.comenzarTarea
        Dim dIntervalo As Double

        dIntervalo = Fecha.Subtract(DateTime.Now).TotalMilliseconds
        If Date.Now.CompareTo(Fecha) > 0 Then
            dIntervalo = INTERVALOMINIMO
        End If
        If dIntervalo <= 0 Then
            dIntervalo = INTERVALOMINIMO
        End If
        Reloj.AutoReset = False
        Reloj.Interval = dIntervalo
        Reloj.Start()
    End Sub
    Public Sub pararTarea() Implements IcsTarea.pararTarea
        Reloj.Enabled = False
        Reloj.AutoReset = False
        Reloj.Stop()
    End Sub
    Public Sub retomarTarea() Implements IcsTarea.retomarTarea
        Reloj.Enabled = True
        Reloj.AutoReset = False
        Reloj.Start()
    End Sub
    Private Sub alarma(ByVal sender As Object, ByVal e As ElapsedEventArgs) Handles Reloj.Elapsed
        Prog.ejecutarTarea(Me)
        If RepetirCadaMin > 0 Then
            Reloj.Interval = RepetirCadaMin * 60000
        End If
    End Sub
    Public Function reprogramarMe() As Boolean Implements IcsTarea.reprogramarMe
        Dim FechaNueva As New DateTime
        'Si Frec=0 es por que la tarea se ejecuta una unica vez
        If Activo AndAlso (Frec > 0) Then
            If RepetirCadaMin = 0 Then 'Si RepetirCadaMin > 0 se repite cada tanto minutos
                Dim Anios, Meses, Dias As Int32
                Dim Hoy As New DateTime
                Dim FechaAux As New DateTime

                Anios = Frec \ 365
                Meses = (Frec Mod 365) \ 30
                Dias = (Frec Mod 365) Mod 30

                Fecha = Fecha.AddYears(Anios).AddMonths(Meses).AddDays(Dias)

                If Fecha.CompareTo(FFin) > 0 Then
                    Return False
                Else 'Fecha.CompareTo(FFin) > 0 Then
                    Return True
                End If

            Else 'If RepetirCadaMin = 0 Then
                Return True
            End If

        Else 'If Activo Then
            Return False
        End If

    End Function
    Public Function estoyAtrasado() As Boolean Implements IcsTarea.estoyAtrasado
        Dim Fr As New DateTime
        Dim Fh As New DateTime
        Dim FrFrec As New DateTime

        Fh = FechaHora.FechaHoraAFecha(DateTime.Now)
        Fr = FechaHora.FechaHoraAFecha(Fecha)
        FrFrec = Fr.AddDays(Frec)

        If (Fh.CompareTo(FrFrec) > 0) Or (Fh.CompareTo(Fr) > 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function programadoParaHoy() As Boolean Implements IcsTarea.programadoParaHoy
        Dim Fr As New DateTime
        Dim FrFrec As New DateTime
        Dim Fh As New DateTime
        Dim Fur As New DateTime
        Fh = FechaHora.FechaHoraAFecha(DateTime.Now)
        Fr = FechaHora.FechaHoraAFecha(Fecha)
        FrFrec = Fr.AddDays(Frec)
        Fur = FechaHora.FechaHoraAFecha(UltimaRealizacion)
        Return ((Fr.CompareTo(Fh) = 0 Or FrFrec.CompareTo(Fh) = 0) And (Fur.CompareTo(Fh) < 0))
    End Function
    Public Function soyRepetitiva() As Boolean Implements IcsTarea.soyRepetitiva
        Return (RepetirCadaMin > 0)
    End Function

    ' SETERS AND GETTERS
    Public Function getId() As Int32 Implements IcsTarea.getId
        Return TaskId
    End Function
    Public Function getFechaEejecucion() As DateTime Implements IcsTarea.getFechaEejecucion
        Return Fecha
    End Function
    Public Function getFrecuencia() As Int32 Implements IcsTarea.getFrecuencia
        Return Frec
    End Function
    Public Function getFechaFin() As DateTime Implements IcsTarea.getFechaFin
        Return FFin
    End Function
    Public Function getEstado() As Boolean Implements IcsTarea.getEstado
        Return Activo
    End Function
    Public Function getReporteId() As Int32 Implements IcsTarea.getReporteId
        Return Reporte
    End Function
    Public Function getCarpeta() As String Implements IcsTarea.getCarpeta
        Return CarpetaDestino
    End Function
    Public Function getFormato() As Int32 Implements IcsTarea.getFormato
        Return TipoFormato
    End Function
    Public Function getMailCco() As String Implements IcsTarea.getMailCco
        Return MailCco
    End Function
    Public Function getMailCc() As String Implements IcsTarea.getMailCc
        Return MailCc
    End Function
    Public Function getMailPara() As String Implements IcsTarea.getMailPara
        Return MailPara
    End Function
    Public Function getImprimir() As Int32 Implements IcsTarea.getImprimir
        Return Imprimir
    End Function
    Public Function getRepetirCadaMin() As Int32 Implements IcsTarea.getRepetirCadaMin
        Return RepetirCadaMin
    End Function
    Public Function getDescripcion() As String Implements IcsTarea.getDescripcion
        Return Descrip
    End Function
    'Implementaciones de interfaces
    Public Overloads Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
        Dim Tarea As csTarea
        Tarea = CType(obj, csTarea)
        If (getId = Tarea.getId AndAlso getReporteId = Tarea.getReporteId AndAlso getFechaEejecucion = Tarea.getFechaEejecucion AndAlso getFechaEejecucion = Tarea.getFechaEejecucion AndAlso getFechaFin = Tarea.getFechaFin AndAlso getFormato = Tarea.getFormato AndAlso getFrecuencia = Tarea.getFrecuencia AndAlso getImprimir = Tarea.getImprimir AndAlso getMailCc = Tarea.getMailCc AndAlso getMailCco = Tarea.MailCco AndAlso getMailPara = Tarea.getMailPara AndAlso getRepetirCadaMin = Tarea.getRepetirCadaMin AndAlso getReporteId = Tarea.getReporteId AndAlso getDescripcion = Tarea.getDescripcion) Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class
