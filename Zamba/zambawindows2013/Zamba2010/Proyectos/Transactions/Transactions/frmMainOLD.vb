Imports Zamba.Servers
Imports Zamba.Core
Imports System.Text

''' <summary>
''' Se encarga de ejecutar un conjunto de consultas dentro de una transacción
''' </summary>
''' <remarks></remarks>
''' <history>
''' [Tomas] 07/07/2009 Created
'''</history>
Public Class FrmOldTransaction
    Private LISTADOPATH As String = Application.StartupPath & "\listado.txt"
    'Private TRIGGERSPATH As String = Application.StartupPath & "\triggers.txt"
    Private Const TIMEOUT As Integer = 180

    ''' <summary>
    ''' Ejecuta la transacción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas] 07/07/2009 Created
    '''</history>
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        'Estado de espera
        Dim cur As Cursor = Me.Cursor
        Dim separator As String() = {vbCrLf & "GO" & vbCrLf}
        Me.Cursor = Cursors.WaitCursor
        Me.Text = "Ejecutando transacción..."
        btnStart.Enabled = False

        'Verifica si el archivo 
        Trace.WriteLineIf(ZTrace.IsVerbose, "Verificando existencia del archivo: " & LISTADOPATH)
        If My.Computer.FileSystem.FileExists(LISTADOPATH) Then
            Try
                'Configura la conexión
                Trace.WriteLineIf(ZTrace.IsVerbose, "Creando conexión")
                Dim con As IDbConnection
                con = Server.Con.CN
                Trace.WriteLineIf(ZTrace.IsVerbose, "Abriendo conexión")
                con.Open()
                Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando transacción")
                Dim Trans As SqlClient.SqlTransaction = con.BeginTransaction()
                Dim command As SqlClient.SqlCommand
                command = New SqlClient.SqlCommand()
                command.Connection = con
                command.Transaction = Trans
                command.CommandTimeout = TIMEOUT

                'Crea el reader del listado de archivos con sentencias a ejecutar
                Dim sr As New IO.StreamReader(LISTADOPATH)
                Dim archivo As String

                Try
                    'Ejecución de archivos separados
                    While sr.EndOfStream = False
                        archivo = sr.ReadLine
                        Trace.WriteLineIf(ZTrace.IsVerbose, vbCrLf & "Verificando existencia del archivo: " & archivo & vbCrLf)
                        If My.Computer.FileSystem.FileExists(archivo) Then
                            Dim str As String = GetQuery(archivo).ToString
                            For Each str2 As String In str.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                                If Not String.IsNullOrEmpty(str2) Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Sentencia a ejecutar: " + str2)
                                    command.CommandText = str2
                                    command.ExecuteNonQuery()
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Sentencia ejecutada con éxito")
                                End If
                            Next
                            '[Ezequiel] - Se agrego un sleep por cada archivo ya que tiraba timeout.
                            '             Se agrego funcionalidad de que tome varias consultas con go y ejecute una por una
                            '             ya que antes al tirar todas sin go tiraba error.
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Se esperan 3 segundos...")
                            System.Threading.Thread.Sleep(3000)
                        Else
                            Throw New IO.IOException("El archivo " & archivo & " no existe." & vbCrLf & "Verifique su existencia y si se encuentra correctamente escrito en listado.txt")
                        End If
                    End While

                    'Aplica los cambios
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Aplicando Commit")
                    Trans.Commit()
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Commit aplicado con éxito")
                    MessageBox.Show("Transacción ejecutada con éxito.", "Éxito en la ejecución", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As IO.IOException
                    'Remueve los cambios
                    ZClass.raiseerror(ex)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Aplicando Rollback")
                    Trans.Rollback()
                    Trace.WriteLineIf(ZTrace.IsVerbose, "El rollback se ha ejecutado con éxito")
                    MessageBox.Show(ex.Message)

                Catch ex As Exception
                    'Remueve los cambios
                    ZClass.raiseerror(ex)
                    Trace.WriteLineIf(ZTrace.IsVerbose, ex.Message)
                    Try
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Aplicando Rollback")
                        Trans.Rollback()
                        Trace.WriteLineIf(ZTrace.IsVerbose, "El rollback se ha ejecutado con éxito")
                        MessageBox.Show("Error en la transacción. Verificar exceptions.")
                    Catch ex2 As Exception
                        ZClass.raiseerror(ex)
                        Trace.WriteLineIf(ZTrace.IsVerbose, ex.Message)
                        MessageBox.Show("Error al hacer rollback en la transacción. Error de TimeOut.")
                    End Try
                Finally
                    'Cierra la conexión
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Cerrando conexión")
                    command.Dispose()
                    Trans.Dispose()
                    con.Close()
                    con.Dispose()
                    con = Nothing
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Conexión cerrada con éxito")
                End Try

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Trace.WriteLineIf(ZTrace.IsVerbose, ex.Message)
                MessageBox.Show(ex.Message)
            End Try

        Else
            MessageBox.Show("El archivo " & LISTADOPATH & " no se encuentra en " & Application.StartupPath)
            Trace.WriteLineIf(ZTrace.IsVerbose, "El archivo " & LISTADOPATH & " no se encuentra en " & Application.StartupPath)
        End If

        'Vuelve al estado anterior
        Me.Text = "Transacciones"
        Me.Cursor = cur
        btnStart.Enabled = True
        ZTrace.RemoveListener("Transactions")
    End Sub

    ''' <summary>
    ''' Obtiene las sentencias a ejecutar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas] 07/07/2009 Created
    '''</history>
    Private Function GetQuery(ByVal queryPath As String) As StringBuilder
        Dim query As New StringBuilder()
        Dim reader As New IO.StreamReader(queryPath)

        Trace.WriteLineIf(ZTrace.IsVerbose, "Leyendo contenido del archivo")
        query.Append(reader.ReadToEnd())
        Trace.WriteLineIf(ZTrace.IsVerbose, "Contenido del archivo leido con éxito")

        reader.Close()
        reader.Dispose()
        reader = Nothing

        Return query
    End Function

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Se instancia el Trace
        ZTrace.SetLevel(1, "Transactions")
    End Sub
End Class
