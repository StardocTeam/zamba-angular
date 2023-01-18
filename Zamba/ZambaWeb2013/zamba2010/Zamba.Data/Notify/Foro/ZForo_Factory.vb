Imports Zamba.Core
'Imports zamba.foro.core
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Foro
''' Class	 : Foro.ZForo_Factory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory para trabajar con elementos del foro
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ZForo_Factory
    Inherits Zamba.Core.ZClass
    Public Overrides Sub Dispose()

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist de objetos Mensajes para un documento especifico dentro de un Tipo de Documento
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns>Arraylist de objetos Mensajes</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAllMessages(ByVal DocId As Int64) As ArrayList
        Dim ArrayMensajes As New ArrayList
        Try
            Dim StrSelect As String = ""
            '  Dim StrRestriccion As String

            If DocId = 0 Then
                StrSelect = "SELECT     ZForum.*, USRTABLE.NAME, USRTABLE.NOMBRES, USRTABLE.APELLIDO FROM ZForum INNER JOIN USRTABLE ON ZForum.UserId = USRTABLE.ID Where (ParentId = 0) Order By Fecha"
            Else
                StrSelect = "SELECT     ZForum.*, USRTABLE.NAME, USRTABLE.NOMBRES, USRTABLE.APELLIDO FROM ZForum INNER JOIN USRTABLE ON ZForum.UserId = USRTABLE.ID Where (DocId = " & DocId & " And ParentId = 0) Order By Fecha"
            End If

            Dim DsMensajes As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

            If DsMensajes.Tables(0).Rows.Count > 0 Then

                Dim i As Integer

                For i = 0 To DsMensajes.Tables(0).Rows.Count - 1
                    Dim Mensaje As MensajeForo
                    Mensaje = New MensajeForo
                    'todo marcos, mapear con nombres
                    'Mensaje.DocT = DsMensajes.Tables(0).Rows(i).Item(0)
                    Mensaje.DocId = DsMensajes.Tables(0).Rows(i).Item(1)
                    Mensaje.ID = DsMensajes.Tables(0).Rows(i).Item(2)
                    Mensaje.ParentId = DsMensajes.Tables(0).Rows(i).Item(3)
                    Mensaje.Name = DsMensajes.Tables(0).Rows(i).Item(4)
                    Mensaje.Mensaje = DsMensajes.Tables(0).Rows(i).Item(5)
                    Mensaje.Fecha = DsMensajes.Tables(0).Rows(i).Item(6)
                    Mensaje.UserId = DsMensajes.Tables(0).Rows(i).Item(7)
                    Mensaje.StateId = DsMensajes.Tables(0).Rows(i).Item(8)
                    Dim Nombreusuario As String
                    Try
                        Nombreusuario = DsMensajes.Tables(0).Rows(i).Item(9).trim & " " & DsMensajes.Tables(0).Rows(i).Item(11).trim
                        If Nombreusuario = String.Empty Then Nombreusuario = DsMensajes.Tables(0).Rows(i).Item(10)
                    Catch ex As Exception
                        Nombreusuario = DsMensajes.Tables(0).Rows(i).Item(10)
                    End Try
                    Mensaje.UserName = Nombreusuario
                    ArrayMensajes.Add(Mensaje)
                Next
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return ArrayMensajes
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist de objetos Mensajes los cuales son respuestas de un comentario
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' --------------------------------------------------------------------------
    Public Shared Function GetAllAnswers(ByVal DocId As Int64) As ArrayList
        Dim ArrayMensajes As New ArrayList
        Try
            Dim StrSelect As String

            If DocId = 0 Then

                StrSelect = "SELECT ZForum.*, USRTABLE.NAME, USRTABLE.NOMBRES, USRTABLE.APELLIDO FROM ZForum INNER JOIN USRTABLE ON ZForum.UserId = USRTABLE.ID Where (ParentId <> 0) Order By Fecha"""
            Else
                StrSelect = "SELECT ZForum.*, USRTABLE.NAME, USRTABLE.NOMBRES, USRTABLE.APELLIDO FROM ZForum INNER JOIN USRTABLE ON ZForum.UserId = USRTABLE.ID Where (DocId = " & DocId & " And ParentId <> 0) Order By Fecha"
            End If
            Dim DsMensajes As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

            If DsMensajes.Tables(0).Rows.Count > 0 Then

                Dim i As Integer
                For i = 0 To DsMensajes.Tables(0).Rows.Count - 1
                    Dim Mensaje As MensajeForo
                    Mensaje = New MensajeForo
                    'todo marcos, mapear con nombres
                    'Mensaje.DocT = DsMensajes.Tables(0).Rows(i).Item(0)
                    Mensaje.DocId = DsMensajes.Tables(0).Rows(i).Item(1)
                    Mensaje.ID = DsMensajes.Tables(0).Rows(i).Item(2)
                    Mensaje.ParentId = DsMensajes.Tables(0).Rows(i).Item(3)
                    Mensaje.Name = DsMensajes.Tables(0).Rows(i).Item(4)
                    Mensaje.Mensaje = DsMensajes.Tables(0).Rows(i).Item(5)
                    Mensaje.Fecha = DsMensajes.Tables(0).Rows(i).Item(6)
                    Mensaje.UserId = DsMensajes.Tables(0).Rows(i).Item(7)
                    Mensaje.StateId = DsMensajes.Tables(0).Rows(i).Item(8)
                    Dim Nombreusuario As String
                    Try
                        Nombreusuario = DsMensajes.Tables(0).Rows(i).Item(9).trim & " " & DsMensajes.Tables(0).Rows(i).Item(11).trim
                        If Nombreusuario = String.Empty Then Nombreusuario = DsMensajes.Tables(0).Rows(i).Item(10)
                    Catch ex As Exception
                        Nombreusuario = DsMensajes.Tables(0).Rows(i).Item(10)
                    End Try
                    Mensaje.UserName = Nombreusuario
                    ArrayMensajes.Add(Mensaje)
                Next
            End If
        Catch
        End Try
        Return ArrayMensajes
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un mensaje
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="ParentId">Id que representa el comentario original</param>
    ''' <param name="IdMensaje">Id del Mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteMessage(ByVal DocId As Int64, ByVal ParentId As Int32, ByVal IdMensaje As Int32)
        Try

            Dim strDelete As String
            If ParentId = 0 Then
                strDelete = "Delete From ZForum Where (DocId=" & DocId & " And IdMensaje=" & IdMensaje & ")"
            Else
                strDelete = "Delete From ZForum Where (DocId=" & DocId & " And IdMensaje=" & IdMensaje & " And ParentId=" & ParentId & ")"
            End If

            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un mensaje al foro
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="IdMensaje">Id del mensaje</param>
    ''' <param name="ParentId">Id que representa el comentario original, si no es respuesta a un comentario, entonces es 0</param>
    ''' <param name="LinkName">... ,maximo 60 caracteres</param>
    ''' <param name="Mensaje">Mensaje que se desea persistir, maximo 300 caracteres</param>
    ''' <param name="Fecha">Fecha de creacion</param>
    ''' <param name="UserId"></param>
    ''' <param name="StateId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertMessage(ByVal DocId As Int64, ByVal Doctypeid As Int64, ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal Fecha As Date, ByVal UserId As Int32, ByVal StateId As Integer)
        Try
            If Mensaje.Length > 300 Then
                Mensaje = Mensaje.Substring(0, 299)
            End If
            If LinkName.Length > 60 Then
                LinkName = LinkName.Substring(0, 59)
            End If
            Dim strInsert As String = "Insert Into ZForum (DocT, DocId,IdMensaje,ParentId,LinkName,Mensaje,Fecha,UserId,StateId) Values (" & Doctypeid & "," & DocId & "," & IdMensaje & "," & ParentId & ",'" & LinkName & "','" & Mensaje & "'," & Servers.Server.Con.ConvertDateTime(Fecha) & "," & UserId & "," & StateId & ")"
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un nuevo ID para un mensaje
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns>Numero entero que representa el nuevo ID</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function SiguienteId(ByVal DocId As Int64) As Integer
        Dim strSelect As String = "Select Max(IdMensaje) From ZForum Where DocId = " & DocId
        Dim Id As Integer
        Try
            Id = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, strSelect)
        Catch ex As Exception
            Id = 0
        End Try
        Return Id + 1
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un nuevo ParentID
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="IdMensaje">Id del mensaje</param>
    ''' <returns>ID que representa el Nuevo Parent ID</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function SiguienteParent(ByVal DocId As Int64, ByVal IdMensaje As Int32) As Int32
        Dim strSelect As String = "Select Max(ParentId) From ZForum Where (DocId = " & DocId & " And IdMensaje = " & IdMensaje & ")"
        Dim ParentId As Integer
        Try
            ParentId = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, strSelect)
        Catch ex As Exception
            ParentId = 0
        End Try
        Return ParentId + 1
    End Function
End Class
