Imports Zamba.Servers.Server
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.ServerImportTaskView
''' Class	 : ServerImportTaskView.Engine
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear un motor que maneja el estado de los mails en cola para ser procesados
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Engine
    Implements IDisposable

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un DataTable con TODOS los mails cuyo estado es "No Insertado"
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetMailsToProcess() As DataTable
        Dim sql As String = "Select * from zexportcontrol where insertado='N'"
        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds.Tables(0)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un DataTable con los mails cuyo estado es "No Insertado" exportados por un usuario determinado
    ''' </summary>
    ''' <param name="usrname">Nombre del usuario</param>
    ''' <returns>DataTable</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetMailsToProcess(ByVal usrname As String) As DataTable
        If usrname <> String.Empty Then
            Dim sql As String = "Select * from zexportcontrol where insertado='N' and userid like '%" & usrname & "%'"
            Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sql)
            Return ds.Tables(0)
        Else
            Return GetMailsToProcess
        End If
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un DataTable conteniendo los mails Insertados correctamente en Zamba
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetInsertados() As DataTable
        Dim sql As String = "Select * from zexportcontrol where insertado='S'"
        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds.Tables(0)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Permite cambiar el estado de un mail de "insertado" a "No Insertado"
    ''' </summary>
    ''' <param name="Codigo">Codigo del mail generado en Lotus Notes</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub UpdateState(ByVal Codigo As String)
        Dim sql As String = "Update zexportcontrol set insertado='N' where Codigo='" & Codigo.Trim & "'"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
    End Sub
End Class
