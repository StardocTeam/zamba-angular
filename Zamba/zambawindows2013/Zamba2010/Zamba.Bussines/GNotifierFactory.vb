Imports ZAMBA.Servers

Public Class GNotifierFactory
    Inherits ZClass

    Public Overrides Sub Dispose()

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Arraylist con los mensajes para el usuario usuario
    ''' </summary>
    ''' <param name="UserId">Id del usuario que se desea obtener la lista de mensajes</param>
    ''' <returns>Arraylist de Datarows con mensajes</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    '''     [Oscar]     12/07/2006  Modificacdo
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetMensajes(ByVal UserId As Int64) As ArrayList
        Dim ArrayMensajes As New ArrayList
        Dim ds As DataSet = Nothing
        Try 'o
            If Server.IsOracle Then
                Dim parnames As String() = {"my_id", "io_cursor"}
                Dim parvalues As String() = {UserId, 2}
                Dim partypes As String() = {13, 5}
                'ds = Server.Con.ExecuteDataset("zsp_messages_100.GetMyMessagesNew",  parvalues)
                'ds = Server.Con.ExecuteDataset("GET_MY_MESSAGES_PKG.getMymessages",  parvalues)
                ds = Server.Con.ExecuteDataset("ZSP_GETMYMESSAGESNEW_PKG.zsp_GetMyMessagesNew", parvalues) 'o
            Else
                Dim parvalues As String() = {UserId}
                ' ds = Server.Con.ExecuteDataset("zsp_messages_100_GetMyMessagesNew", parvalues)
                ds = Server.Con.ExecuteDataset("Getmymessages", parvalues)
            End If
            If Not IsNothing(ds) Then
                For Each Row As System.Data.DataRow In ds.Tables(0).Rows
                    ArrayMensajes.Add(Row(0))
                Next
                Return ArrayMensajes
            Else
                Return Nothing
            End If
        Catch ex As Exception 'o
            zamba.core.zclass.raiseerror(ex) 'o
        Finally 'o
            'ArrayMensajes.Clear() 'Si se pone en clear siempre va a devolver 
            'ArrayMensajes = Nothing 'un array vacio //Marcelo
            If Not IsNothing(ds) Then 'o
                ds.Dispose() 'o
                ds = Nothing 'o
            End If
        End Try 'o

        Return ArrayMensajes
    End Function


End Class

