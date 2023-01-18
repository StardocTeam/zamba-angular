Imports Zamba.Core
Public Class PlayIfTimePass
    Private Minute As Int32
    Private Hour As Int32
    Private Day As Int32
    Private Week As Int32
    Private lastExecute As Date
    Private myRule As IIfTimePass

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Minute = Int32.Parse(myrule.Minute)
        Hour = Int32.Parse(myrule.Hour)
        Day = Int32.Parse(myrule.Day)
        Week = Int32.Parse(myrule.Week)
        lastExecute = myrule.lastExecute
        Trace.WriteLineIf(ZTrace.IsInfo, "Ingreso al PLAY de IfTimePass")
        If results.Count > 0 Then
            Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de Results: " & results.Count)
            results = ValidarTiempo(results, ifType)
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No hay results para generar")
        End If
        Return results
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Si paso el tiempo devuelve results, sino nothing
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	01/03/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function ValidarTiempo(ByVal Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Trace.WriteLineIf(ZTrace.IsInfo, "Valido el Tiempo Transcurrido desde la ultima consulta")
        Dim Transcurridos As Int64
        Dim aTranscurrir As Int64
        aTranscurrir = Minute + Hour * 60 + Day * 60 * 24 + Week * 60 * 24 * 7
        Transcurridos = DateDiff(DateInterval.Minute, lastExecute, Now)
        If (Transcurridos <= aTranscurrir) = ifType Then
            Results.Clear()
        End If
        Return Results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfTimePass)
        Me.myRule = rule
    End Sub
End Class
