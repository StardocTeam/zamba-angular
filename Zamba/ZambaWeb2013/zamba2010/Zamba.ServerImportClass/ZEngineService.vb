Imports Zamba.Servers
Public Class ZEngineService
    Dim CB As New System.Threading.TimerCallback(AddressOf Procesar)
    Dim Timer As System.Threading.Timer
    Private StartUpPath As String
    Public Sub New(ByVal Periodo As Int64, ByVal startUpPath As String)
        Timer = New System.Threading.Timer(CB, Nothing, 1000, Periodo)
        Me.StartUpPath = startUpPath
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los mails que no fueron insertados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Function GetMailsToProcess() As DataSet
        Dim sql As String = "Select * from zexportcontrol where INSERTADO='N'"
        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds
    End Function
    Private Sub Procesar(ByVal state As Object)
        Timer.Change(1800000, 60000)
        Dim t1 As New Threading.Thread(AddressOf P)
        t1.Start()
    End Sub
    Private Sub P()
        Dim Count As Int64 = 0
        Try
            Dim ds As DataSet = GetMailsToProcess()
            Dim objserver As New ZServEngine()
            Dim i As Int32
            Count = ds.Tables(0).Rows.Count
            For i = 0 To Count - 1
                objserver.Run("INSERTARMAIL", ds.Tables(0).Rows(i).Item(2).ToString(), Nothing)
            Next
        Catch
        Finally
            If Count > 0 Then
                Timer.Change(0, 30000)
            Else
                Timer.Change(300000, 300000)
            End If
        End Try
    End Sub
End Class
