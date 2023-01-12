Imports Zamba.Core
Public Class NotesControlFactory
    Inherits ZClass
    Public Overrides Sub Dispose()

    End Sub
    'Private Shared Sub InsertNewNote(ByVal Doc_Id As Integer, ByVal table As String, ByVal Note As Note)
    '    NotesFactory.InsertNewNote(Doc_Id, table, Note)
    'End Sub
    'Private Shared Sub UpdateNote(ByVal Doc_Id As Integer, ByVal Note As Sign)
    '    NotesFactory.updatenote(Doc_Id, Note)
    'End Sub
    Public Shared Function NewNote(ByVal Userid As Integer, ByVal UserName As String, ByVal UserApellidos As String, ByVal Doc_Id As Integer, ByVal Position As Point, ByVal type As Boolean, Optional ByVal signpath As String = "") As BaseNote

        If CInt(type) = 0 Then 'Nota
            Dim table As String = "Doc_Notes"
            Dim Note As New Note
            Note.Id = Core.CoreBusiness.GetNewID(IdTypes.NOTEID)
            Note.Notetext = "Escriba aqui el texto de la nota"
            Note.UserID = Userid
            Note.UserName = UserName.Trim
            Note.UserApellidos = UserApellidos.Trim
            '[Sebastian]10-06-2009 se agrego to string para salvar warning
            Note.NoteDate = Now.ToString
            Note.NoteTime = Now.ToString
            Note.Location = Position
            Note.Width = 30
            Note.Height = 30
            Note.AllowDrop = True
            Dim columns As String = "Doc_Id,User_Id,User_Name,Note_Text,Note_Date,Note_Time,X_Position,Y_Position,X_Size,Y_Size,Note_Id"
            Dim Values As New System.Text.StringBuilder
            Values.Append(Doc_Id)
            Values.Append(",")
            Values.Append(Note.UserID)
            Values.Append(",'")
            Values.Append(Note.UserName)
            Values.Append(" ")
            Values.Append(Note.UserApellidos)
            Values.Append("','")
            Values.Append(Note.Notetext)
            Values.Append("','")
            Values.Append(Note.NoteDate)
            Values.Append("','")
            Values.Append(Note.NoteTime)
            Values.Append("',")
            Values.Append(Note.Location.X)
            Values.Append(",")
            Values.Append(Note.Location.Y)
            Values.Append(",")
            Values.Append(Note.Width)
            Values.Append(",")
            Values.Append(Note.Height)
            Values.Append(",")
            Values.Append(Note.Id)
            NotesBusiness.NewNote(table, columns, Values.ToString)
            Values = Nothing
            Note.NoteDate = "Fecha: " & Note.NoteTime

            If Note.UserName.Trim = String.Empty AndAlso Note.UserApellidos.Trim = String.Empty Then
                Note.title = "Usuario: " & Note.UserID
            Else
                Note.title = "Usuario: " & Note.UserName & " " & Note.UserApellidos
            End If

            'Note.Location = New Drawing.Point(X, Y)
            Note.Edited = False
            Note.IsLoading = False
            Return Note
        Else 'Firma
            Dim Note As New Sign
            Note.Id = Core.CoreBusiness.GetNewID(IdTypes.NOTEID)
            Note.SignPath = signpath
            Note.UserID = Userid
            Note.UserName = UserName.Trim
            '[sebastian] 10-06-2009 se salvo warning agregando tostring
            Note.NoteDate = Now.ToString
            Note.NoteTime = Now.ToString
            Note.Location = Position
            Note.Width = 35
            Note.Height = 35
            Note.AllowDrop = True
            Note.NoteDate = "Fecha: " & Note.NoteTime
            If Note.UserName.Trim = String.Empty AndAlso Note.UserApellidos.Trim = String.Empty Then
                Note.title = "Usuario: " & Note.UserID
            Else
                Note.title = "Usuario: " & Note.UserName & " " & Note.UserApellidos
            End If
            '  Note.Location = New Drawing.Point(X, Y)
            Note.Edited = False
            Note.IsLoading = False
            Note.Type = 1
            'note.ImageId = userimageid
            Note.Encrypted = CBool(0)
            Dim table As String = "Doc_Notes"
            Dim columns As String = "Doc_Id,User_Id,User_Name,Note_Text,Note_Date,Note_Time,X_Position,Y_Position,X_Size,Y_Size,Note_Id,type,encrypted"
            Dim Values As String = Doc_Id & "," & Note.UserID & ",'" & Note.UserName & " " & Note.UserApellidos & "','" & Note.Notetext & "','" & Note.NoteDate & "','" _
            & Note.NoteTime & "'," & Note.Location.X & "," & Note.Location.Y & "," & Note.Width & "," & Note.Height & "," _
            & Note.Id & "," & 1 & "," & 0
            NotesBusiness.NewNote(table, columns, Values.ToString)
            Return Note
        End If
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist de notas
    ''' </summary>
    ''' <param name="Document_Id"></param>
    ''' <param name="ServerImagesPath"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Hernan]	11/06/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNotes(ByVal Document_Id As Integer, ByVal ServerImagesPath As String) As ArrayList
        Dim Notes As New ArrayList
        Try
            Dim dsnotes As New DataSet
            dsnotes = NotesBusiness.GetNotes(Document_Id)
            Dim i As Int32
            Dim note As BaseNote
            For Each r As DataRow In dsnotes.Tables(0).Rows

                i += 1
                Dim type As Int32

                If r.IsNull(10) = False Then
                    type = Int32.Parse(r.Item(10).ToString)
                Else
                    type = 0
                End If

                Select Case type
                    Case 0
                        note = New Note
                        note.Name = "Note"
                        note.TabIndex = i
                        note.Id = Int32.Parse(r.Item(0).ToString)
                        note.Notetext = r.Item(1).ToString.Trim
                        If note.Notetext = "" Then note.Notetext = "Escriba aqui el texto de la nota"
                        note.UserID = Int32.Parse(r.Item(2).ToString)
                        note.NoteDate = r.Item(3).ToString
                        note.NoteTime = r.Item(4).ToString
                        note.UserName = r.Item(0).ToString.Trim
                        note.title = "Usuario: " & note.UserName
                        note.NoteDate = "Fecha: " & note.NoteDate
                        note.Height = Int32.Parse(r.Item(6).ToString)
                        note.Width = Int32.Parse(r.Item(7).ToString)
                        note.Left = Int32.Parse(r.Item(8).ToString)
                        note.Top = Int32.Parse(r.Item(9).ToString)
                        note.Edited = False
                        note.IsLoading = False
                        note.AllowDrop = True
                        If r.IsNull(10) = False Then note.Type = Int32.Parse(r.Item(10).ToString)
                        If r.IsNull(11) = False Then note.ImageId = Int32.Parse(r.Item(11).ToString)
                        If r.IsNull(12) = False Then note.Encrypted = Boolean.Parse(r.Item(12).ToString)
                        Notes.Add(note)
                    Case 1
                        note = New Sign
                        note.Name = "Note"
                        note.TabIndex = i
                        note.Id = Int32.Parse(r.Item(0).ToString)
                        note.Notetext = r.Item(1).ToString.Trim
                        Try

                            Dim firma As String = UserBusiness.GetUserSignById(Int32.Parse(r.Item(0).ToString))

                            note.SignPath = ServerImagesPath & "\" & firma
                        Catch
                        End Try

                        note.UserID = Int32.Parse(r.Item(2).ToString)
                        note.NoteDate = r.Item(3).ToString
                        note.NoteTime = r.Item(4).ToString
                        note.UserName = r.Item(5).ToString.Trim
                        note.title = "Usuario: " & note.UserName
                        note.NoteDate = "Fecha: " & note.NoteDate
                        note.Height = Int32.Parse(r.Item(6).ToString)
                        note.Width = Int32.Parse(r.Item(7).ToString)
                        note.Left = Int32.Parse(r.Item(8).ToString)
                        note.Top = Int32.Parse(r.Item(9).ToString)
                        note.Edited = False
                        note.IsLoading = False
                        note.AllowDrop = True
                        If r.IsNull(10) = False Then note.Type = Int32.Parse(r.Item(10).ToString)
                        If r.IsNull(11) = False Then note.ImageId = Int32.Parse(r.Item(11).ToString)
                        If r.IsNull(12) = False Then note.Encrypted = Boolean.Parse(r.Item(12).ToString)
                        Notes.Add(note)
                End Select
            Next
            Return Notes

        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Guarda una Nota
    ''' </summary>
    ''' <param name="Note"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------


    Public Shared Sub Save(ByVal Note As BaseNote)
        If Note.Edited = True Then
            If Note.Notetext = "" Then Note.Notetext = "Escriba aqui el texto de la nota"

            NotesBusiness.Save(Note.Notetext, Note.Location.X, Note.Location.Y, Note.Id)
            ''Dim strUpdate As New System.Text.StringBuilder
            ''strUpdate.Append("UPDATE DOC_NOTES SET NOTE_TEXT='")
            ''strUpdate.Append(Note.Notetext)
            ''strUpdate.Append("', X_Position=")
            ''strUpdate.Append(Note.Location.X)
            ''strUpdate.Append(", Y_Position=")
            ''strUpdate.Append(Note.Location.Y)
            ''strUpdate.Append(" WHERE Note_Id=")
            ''strUpdate.Append(Note.Id)
            'Try
            '    NotesBusiness.SaveNote(strUpdate.ToString)
            'Catch ex As Exception
            '    Zamba.Core.ZClass.raiseerror(ex)
            'Finally
            '    strUpdate = Nothing
            'End Try
        End If
    End Sub





    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Mueve las notas de un Doc_type a otro
    ''' </summary>
    ''' <param name="OldDOCID"></param>
    ''' <param name="NewDOCID"></param>
    ''' <param name="Mover"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Sub MoveNotes(ByVal OldDOCID As Int64, ByVal NewDOCID As Int64, ByVal Mover As Boolean)

    '    'MOVER=FALSE then copiar
    '    Dim sql As New System.Text.StringBuilder
    '    Try
    '        If Mover = True Then
    '            sql.Append("Update doc_Notes set DOC_ID=" & NewDOCID & " where doc_ID=" & OldDOCID)
    '            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '        Else
    '            sql.Append("Select * from doc_notes where Doc_ID=" & OldDOCID)
    '            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
    '            Dim i, J As Int32
    '            For i = 0 To ds.Tables(0).Rows.Count - 1
    '                sql.Remove(0, sql.Length)
    '                sql.Append("Insert into Doc_Notes Values(")
    '                For J = 0 To ds.Tables(0).Columns.Count - 1
    '                    If J = 0 Then sql.Append(CoreData.GetNewID(IdTypes.NOTEID))
    '                    sql.Append(",")
    '                    If (J >= 1 AndAlso J <= 5) OrElse (J >= 10 AndAlso J <= 13) Then
    '                        sql.Append(ds.Tables(0).Rows(i).Item(J))
    '                    Else
    '                        sql.Append("'")
    '                        sql.Append(ds.Tables(0).Rows(i).Item(J))
    '                        sql.Append("',")
    '                    End If
    '                    'sql.Append(sql.ToString.TrimEnd(","))
    '                    sql.Append(")")
    '                Next
    '                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                'sql.Remove(0, sql.Length)
    '            Next
    '        End If
    '    Finally
    '        sql = Nothing
    '    End Try
    'End Sub

End Class
