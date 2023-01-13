Imports Zamba.Servers
Imports System.Text
Imports System.Data.SqlClient
Imports System.IO

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.RemoteInsert
''' Class	 : Form
''' -----------------------------------------------------------------------------
''' <summary>
''' Formulario que sirve para insertar varias filas en las tablas RemoteInsert y DocumentsIndexs
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Gaston]	02/03/2009	Created
''' </history>
''' -----------------------------------------------------------------------------

Public Class Form1

    Dim bnBan As Boolean = True

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        Dim counter As Integer = 0

        btnCancelProcess.Enabled = True

        Dim documentName As String = "Test"
        Dim docTypeId As String = "7216"
        Dim fileExtension As String = String.Empty
        Dim status As Integer = 3

        Dim docId As Integer = Nothing

        Dim indexId As Integer = 160
        Dim indexValue As String = "prueba"

        Dim fs As New FileStream(Application.StartupPath & "\Prueba.txt", FileMode.OpenOrCreate, FileAccess.Read)
        Dim br As New BinaryReader(fs)
        br.BaseStream.Position = 0
        Dim BinaryImage As Byte() = br.ReadBytes(CInt(br.BaseStream.Length))
        Dim SqlServerParameter As SqlParameter = New SqlParameter("@Blob", BinaryImage)
        SqlServerParameter.DbType = DbType.Binary
        Dim parameters As IDbDataParameter = DirectCast(SqlServerParameter, IDbDataParameter)

        Dim queryRemoteInsert As New StringBuilder
        queryRemoteInsert.Append("INSERT INTO RemoteInsert ")
        queryRemoteInsert.Append("(DocumentName, DocTypeId, SerializedFile, FileExtension, Status) ")
        queryRemoteInsert.Append("VALUES ('")
        queryRemoteInsert.Append(documentName)
        queryRemoteInsert.Append("',")
        queryRemoteInsert.Append(docTypeId)
        queryRemoteInsert.Append(",@Blob,'")
        queryRemoteInsert.Append(fileExtension)
        queryRemoteInsert.Append("',")
        queryRemoteInsert.Append(status)
        queryRemoteInsert.Append(")")

        While (bnBan = True)

            ' Cada cien filas
            If (counter = 100) Then
                ' Se duerme el programa por un segundo
                Threading.Thread.Sleep(1000)
                counter = 0
            End If

            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, queryRemoteInsert.ToString(), parameters)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error al insertar en RemoteInsert")
                Exit Sub
            End Try

            docId = getLastInsertedId().ToString()

            If Not (IsNothing(docId)) Then

                Dim queryDocumentsIndexs As New StringBuilder
                queryDocumentsIndexs.Append("INSERT INTO DocumentsIndexs(Id, IndexId, IndexValue) ")
                queryDocumentsIndexs.Append("VALUES (")
                queryDocumentsIndexs.Append(docId)
                queryDocumentsIndexs.Append(",")
                queryDocumentsIndexs.Append(indexId)
                queryDocumentsIndexs.Append(",'")
                queryDocumentsIndexs.Append(indexValue)
                queryDocumentsIndexs.Append("')")

                Try
                    Server.Con.ExecuteNonQuery(CommandType.Text, queryDocumentsIndexs.ToString())
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error al insertar en DocumentsIndexs")
                    Exit Sub
                End Try

                queryDocumentsIndexs = Nothing

            Else
                MessageBox.Show("Ha ocurrido un error al generar el temporaryId")
                Exit Sub
            End If

            counter = counter + 1

        End While

    End Sub

    Private Sub btnCancelProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelProcess.Click
        btnCancelProcess.Text = "Proceso Cancelado"
        bnBan = False
    End Sub

    Private Function getLastInsertedId() As Integer

        Dim value As Integer = 0

        Dim returnId As Object = Server.Con.ExecuteScalar(CommandType.Text, "select max(temporaryId) as 'Id' from remoteinsert")

        If Not (IsNothing(returnId)) Then
            Return (CInt(returnId))
        End If

        Return (Nothing)

    End Function

End Class