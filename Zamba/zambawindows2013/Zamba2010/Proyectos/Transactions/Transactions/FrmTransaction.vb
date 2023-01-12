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
Public Class FrmTransaction

    Private Const TIMEOUT As Integer = 180

    Private Sub FrmTransaction_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Height = txtMessages.Top + 25
        Try
            Dim path As String = My.Settings.Path
            If IO.File.Exists(path) Then
                txtFile.Text = path
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnSearchFile_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchFile.Click
        Using frmFile As New OpenFileDialog
            With frmFile
                .CheckFileExists = True
                .CheckPathExists = True
                .Multiselect = False
                .Title = "Seleccione el archivo de consultas SQL"

                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    txtFile.Text = .FileName
                    My.Settings.Path = .FileName
                    My.Settings.Save()
                End If
            End With
        End Using
    End Sub

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
        Dim filePath As String = txtFile.Text
        Dim cur As Cursor = Me.Cursor
        Dim separator As String() = {vbCrLf & "GO" & vbCrLf}
        Dim queries As String
        ZTrace.SetLevel(1, "Transactions")

        'Estado de espera
        Me.Cursor = Cursors.WaitCursor
        Me.Text = "Ejecutando transacción..."
        Me.Enabled = False
        Me.txtMessages.Text = String.Empty
        Me.Refresh()

        'Verifica si el archivo 
        Log("Verificando existencia del archivo: " & filePath)
        If My.Computer.FileSystem.FileExists(filePath) Then
            Try
                'Configura la conexión
                Log("Creando conexión")
                Dim con As IDbConnection
                con = Server.Con.CN

                Log("Abriendo conexión")
                con.Open()

                Log("Iniciando transacción")
                Dim Trans As SqlClient.SqlTransaction = con.BeginTransaction()
                Dim command As SqlClient.SqlCommand
                command = New SqlClient.SqlCommand()
                command.Connection = con
                command.Transaction = Trans
                command.CommandTimeout = TIMEOUT

                'Crea el reader del listado de archivos con sentencias a ejecutar
                Using sr As New IO.StreamReader(filePath, Encoding.UTF7)

                    Try
                        'Ejecución de archivos separados
                        queries = sr.ReadToEnd()
                        For Each str2 As String In queries.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                            If Not String.IsNullOrEmpty(str2) Then
                                Log("Sentencia a ejecutar: " + str2)
                                command.CommandText = str2
                                command.ExecuteNonQuery()
                                Log("Sentencia ejecutada con éxito")
                            End If
                        Next

                        'Aplica los cambios
                        Log("Aplicando Commit")
                        Trans.Commit()
                        Log("Commit aplicado con éxito")
                        btnSearchFile.Enabled = False
                        btnStart.Enabled = False
                        txtFile.Enabled = False
                        MessageBox.Show("Transacción ejecutada con éxito.", "Éxito en la ejecución", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As IO.IOException
                        'Remueve los cambios
                        ZClass.raiseerror(ex)
                        Log("Aplicando Rollback")
                        Trans.Rollback()
                        Log("El rollback se ha ejecutado con éxito")
                        MessageBox.Show(ex.Message)

                    Catch ex As Exception
                        'Remueve los cambios
                        ZClass.raiseerror(ex)
                        Log(ex.Message)

                        Try
                            Log("Aplicando Rollback")
                            Trans.Rollback()
                            Log("El rollback se ha ejecutado con éxito")
                            txtMessages.Text = ex.Message + vbCrLf + "Verifique los logs de errores para mayor información."
                            Me.Height = 450
                        Catch ex2 As Exception
                            ZClass.raiseerror(ex)
                            Log(ex.Message)
                            MessageBox.Show("Error al hacer rollback en la transacción. Error de TimeOut.")
                        End Try

                    Finally

                        'Cierra la conexión
                        Log("Cerrando conexión")
                        Try
                            command.Dispose()
                            Trans.Dispose()
                            con.Close()
                            con.Dispose()
                            con = Nothing
                            Log("Conexión cerrada con éxito")
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                            Log(ex.Message)
                        End Try

                    End Try

                End Using

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Log(ex.Message)
                MessageBox.Show(ex.Message)
            End Try

        Else
            MessageBox.Show("El archivo " & filePath & " no se encuentra en " & Application.StartupPath)
            Log("El archivo " & filePath & " no se encuentra en " & Application.StartupPath)
        End If

        'Vuelve al estado anterior
        ZTrace.RemoveListener("Transactions")
        Me.Text = "Transacciones"
        Me.Cursor = cur
        Me.Enabled = True
        Me.Refresh()

    End Sub

    Private Sub Log(ByVal message As String)
        Trace.WriteLine(message)
    End Sub

End Class
