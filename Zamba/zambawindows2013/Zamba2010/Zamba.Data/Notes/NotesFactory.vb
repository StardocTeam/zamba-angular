Imports Zamba.Core
Public NotInheritable Class NotesFactory
    Inherits ZClass
    Public Overrides Sub Dispose()
    End Sub
    'Public Shared Sub InsertNewNote(ByVal Doc_Id As Integer, ByVal table As String, ByVal Note As Note)
    '    Dim columns As String = "Doc_Id,User_Id,User_Name,Note_Text,Note_Date,Note_Time,X_Position,Y_Position,X_Size,Y_Size,Note_Id"
    '    Dim Values As New System.Text.StringBuilder
    '    Values.Append(Doc_Id)
    '    Values.Append(",")
    '    Values.Append(Note.UserID)
    '    Values.Append(",'")
    '    Values.Append(Note.UserName)
    '    Values.Append(" ")
    '    Values.Append(Note.UserApellidos)
    '    Values.Append("','")
    '    Values.Append(Note.Notetext)
    '    Values.Append("','")
    '    Values.Append(Note.NoteDate)
    '    Values.Append("','")
    '    Values.Append(Note.NoteTime)
    '    Values.Append("',")
    '    Values.Append(Note.Location.x)
    '    Values.Append(",")
    '    Values.Append(Note.Location.y)
    '    Values.Append(",")
    '    Values.Append(Note.Width)
    '    Values.Append(",")
    '    Values.Append(Note.Height)
    '    Values.Append(",")
    '    Values.Append(Note.Id)
    '    Dim Strinsert As String = "INSERT INTO " & table & " (" & columns & ") VALUES (" & Values.ToString & ")"
    '    Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
    '    Values = Nothing
    'End Sub
    'Public Shared Sub UpdateNote(ByVal Doc_Id As Integer, ByVal Note As Sign)
    '    Dim table As String = "Doc_Notes"
    '    Dim columns As String = "Doc_Id,User_Id,User_Name,Note_Text,Note_Date,Note_Time,X_Position,Y_Position,X_Size,Y_Size,Note_Id,type,encrypted"
    '    Dim Values As String = Doc_Id & "," & Note.UserID & ",'" & Note.UserName & " " & Note.UserApellidos & "','" & Note.Notetext & "','" & Note.NoteDate & "','" _
    '    & Note.NoteTime & "'," & Note.Location.X & "," & Note.Location.Y & "," & Note.Width & "," & Note.Height & "," _
    '    & Note.Id & "," & 1 & "," & 0
    '    Dim Strinsert As String = "INSERT INTO " & table & " (" & columns & ") VALUES (" & Values & ")"
    '    Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
    'End Sub



    'Public Shared Function NewNote(ByVal Userid As Integer, ByVal UserName As String, ByVal UserApellidos As String, ByVal Doc_Id As Integer, ByVal Position As Point, ByVal type As Boolean, Optional ByVal signpath As String = "") As BaseNote

    '    If type = 0 Then 'Nota
    '        Dim table As String = "Doc_Notes"
    '        Dim Note As New Note
    '        Note.ID = CoreData.GetNewID(IdTypes.NOTEID)
    '        Note.NoteText = "Escriba aqui el texto de la nota"
    '        Note.UserId = Userid
    '        Note.UserName = UserName.Trim
    '        Note.UserApellidos = UserApellidos.Trim
    '        Note.NoteDate = Now
    '        Note.NoteTime = Now
    '        Note.Location = Position
    '        Note.Width = 30
    '        Note.Height = 30
    '        Note.AllowDrop = True
    '        Dim columns As String = "Doc_Id,User_Id,User_Name,Note_Text,Note_Date,Note_Time,X_Position,Y_Position,X_Size,Y_Size,Note_Id"
    '        Dim Values As New System.Text.StringBuilder
    '        values.Append(Doc_Id)
    '        values.Append(",")
    '        values.Append(Note.UserId)
    '        values.Append(",'")
    '        values.Append(Note.UserName)
    '        values.Append(" ")
    '        values.Append(note.UserApellidos)
    '        values.Append("','")
    '        values.Append(Note.NoteText)
    '        values.Append("','")
    '        values.Append(Note.NoteDate)
    '        values.Append("','")
    '        values.Append(Note.NoteTime)
    '        values.Append("',")
    '        values.Append(Note.Location.x)
    '        values.Append(",")
    '        values.Append(Note.Location.y)
    '        values.Append(",")
    '        values.Append(Note.Width)
    '        values.Append(",")
    '        values.Append(Note.Height)
    '        values.Append(",")
    '        values.Append(Note.ID)
    '        Dim Strinsert As String = "INSERT INTO " & table & " (" & columns & ") VALUES (" & Values.ToString & ")"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
    '        values = Nothing
    '        Note.NoteDate = "Fecha: " & Note.NoteTime

    '        If Note.UserName.Trim = String.Empty AndAlso Note.UserApellidos.Trim = String.Empty Then
    '            Note.Title = "Usuario: " & Note.UserID
    '        Else
    '            Note.Title = "Usuario: " & Note.UserName & " " & Note.UserApellidos
    '        End If

    '        'Note.Location = New Drawing.Point(X, Y)
    '        Note.Edited = False
    '        Note.IsLoading = False
    '        Return Note
    '    Else 'Firma
    '        Dim Note As New Sign
    '        Note.ID = CoreData.GetNewID(IdTypes.NOTEID)
    '        Note.SignPath = signpath
    '        Note.UserId = Userid
    '        Note.UserName = UserName.Trim
    '        Note.NoteDate = Now
    '        Note.NoteTime = Now
    '        Note.Location = Position
    '        Note.Width = 35
    '        Note.Height = 35
    '        Note.AllowDrop = True
    '        Note.NoteDate = "Fecha: " & Note.NoteTime
    '        If Note.UserName.Trim = String.Empty AndAlso Note.UserApellidos.Trim = String.Empty Then
    '            Note.Title = "Usuario: " & Note.UserID
    '        Else
    '            Note.Title = "Usuario: " & Note.UserName & " " & Note.UserApellidos
    '        End If
    '        '  Note.Location = New Drawing.Point(X, Y)
    '        Note.Edited = False
    '        Note.IsLoading = False
    '        Note.Type = 1
    '        'note.ImageId = userimageid
    '        note.encrypted = 0
    '        Dim table As String = "Doc_Notes"
    '        Dim columns As String = "Doc_Id,User_Id,User_Name,Note_Text,Note_Date,Note_Time,X_Position,Y_Position,X_Size,Y_Size,Note_Id,type,encrypted"
    '        Dim Values As String = Doc_Id & "," & Note.UserId & ",'" & Note.UserName & " " & note.UserApellidos & "','" & Note.NoteText & "','" & Note.NoteDate & "','" _
    '        & Note.NoteTime & "'," & Note.Location.x & "," & Note.Location.Y & "," & Note.Width & "," & Note.Height & "," _
    '        & Note.ID & "," & 1 & "," & 0
    '        Dim Strinsert As String = "INSERT INTO " & table & " (" & columns & ") VALUES (" & Values & ")"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
    '        Return Note
    '    End If
    'End Function
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Obtiene un Arraylist de notas
    '''' </summary>
    '''' <param name="Document_Id"></param>
    '''' <param name="ServerImagesPath"></param>
    '''' <returns></returns>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	26/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Shared Function GetNotes(ByVal Document_Id As Integer, ByVal ServerImagesPath As String) As ArrayList
    '    Dim Notes As New ArrayList
    '    Dim Dr As IDataReader
    '    Try
    '        Dim StrSelect As String = "Select NOTE_ID,NOTE_TEXT,USER_ID,NOTE_DATE,NOTE_TIME,USER_NAME,X_SIZE,Y_SIZE,X_POSITION,Y_POSITION,type,imageid,encrypted from Doc_Notes where Doc_Id = " & Document_Id
    '        Dr = Server.Con.ExecuteReader(CommandType.Text, StrSelect)
    '        Dim i As Int32
    '        Dim note As BaseNote
    '        While Dr.Read
    '            i += 1
    '            Dim type As Int32

    '            If Dr.IsDBNull(10) = False Then
    '                type = Dr.GetInt32(10)
    '            Else
    '                type = 0
    '            End If

    '            Select Case type
    '                Case 0
    '                    note = New note
    '                    note.Name = "Note"
    '                    note.TabIndex = i
    '                    note.Id = Dr.GetInt32(0)
    '                    note.Notetext = Dr.GetString(1).Trim
    '                    If note.Notetext = "" Then note.Notetext = "Escriba aqui el texto de la nota"
    '                    note.UserID = Dr.GetInt32(2)
    '                    note.NoteDate = Dr.GetString(3)
    '                    note.NoteTime = Dr.GetString(4)
    '                    note.UserName = Dr.GetString(5).Trim
    '                    note.title = "Usuario: " & note.UserName
    '                    note.NoteDate = "Fecha: " & note.NoteDate
    '                    note.Height = Dr.GetInt32(6)
    '                    note.Width = Dr.GetInt32(7)
    '                    note.Left = Dr.GetInt32(8)
    '                    note.Top = Dr.GetInt32(9)
    '                    note.Edited = False
    '                    note.IsLoading = False
    '                    note.AllowDrop = True
    '                    If Dr.IsDBNull(10) = False Then note.Type = Dr.GetInt32(10)
    '                    If Dr.IsDBNull(11) = False Then note.ImageId = Dr.GetDecimal(11)
    '                    If Dr.IsDBNull(12) = False Then note.Encrypted = Dr.GetInt32(12)
    '                    Notes.Add(note)
    '                Case 1
    '                    note = New Sign
    '                    note.Name = "Note"
    '                    note.TabIndex = i
    '                    note.Id = Dr.GetInt32(0)
    '                    note.Notetext = Dr.GetString(1).Trim
    '                    Try
    '                        StrSelect = "Select firma from usrtable where Id = " & Dr.GetInt32(2)
    '                        Dim firma As String = Server.Con.ExecuteScalar(CommandType.Text, StrSelect)

    '                        note.SignPath = ServerImagesPath & "\" & firma
    '                    Catch
    '                    End Try

    '                    note.UserID = Dr.GetInt32(2)
    '                    note.NoteDate = Dr.GetString(3)
    '                    note.NoteTime = Dr.GetString(4)
    '                    note.UserName = Dr.GetString(5).Trim
    '                    note.title = "Usuario: " & note.UserName
    '                    note.NoteDate = "Fecha: " & note.NoteDate
    '                    note.Height = Dr.GetInt32(6)
    '                    note.Width = Dr.GetInt32(7)
    '                    note.Left = Dr.GetInt32(8)
    '                    note.Top = Dr.GetInt32(9)
    '                    note.Edited = False
    '                    note.IsLoading = False
    '                    note.AllowDrop = True
    '                    If Dr.IsDBNull(10) = False Then note.Type = Dr.GetInt32(10)
    '                    If Dr.IsDBNull(11) = False Then note.ImageId = Dr.GetDecimal(11)
    '                    If Dr.IsDBNull(12) = False Then note.Encrypted = Dr.GetInt32(12)
    '                    Notes.Add(note)
    '            End Select
    '        End While
    '        Return Notes
    '    Finally
    '        If IsNothing(Dr) = False Then
    '            Try
    '                Server.Con.Command.Cancel()
    '            Catch ex As Exception
    '            End Try
    '            Dr.Close()
    '            Dr.Dispose()
    '            Dr = Nothing
    '        End If
    '        Try
    '            Server.Con.dispose()
    '        Catch
    '        End Try
    '    End Try
    'End Function

    ''' <summary>
    ''' Graba una nota en un documento
    ''' </summary>
    ''' <param name="NoteText">Texto de la Nota</param>
    ''' <param name="X">Posicion X en el plano donde se ubicará la nota</param>
    ''' <param name="Y">Posicion Y en el plano donde se ubicará la nota</param>
    ''' <param name="NoteId">ID identificador de la nota</param>
    ''' <remarks></remarks>
    Public Shared Sub Save(ByVal NoteText As String, ByVal X As Decimal, ByVal Y As Decimal, ByVal NoteId As Int32)
        If NoteText = "" Then NoteText = "Escriba aqui el texto de la nota"
        Dim strUpdate As New System.Text.StringBuilder
        strUpdate.Append("UPDATE DOC_NOTES SET NOTE_TEXT='")
        strUpdate.Append(NoteText)
        strUpdate.Append("', X_Position=")
        strUpdate.Append(X)
        strUpdate.Append(", Y_Position=")
        strUpdate.Append(Y)
        strUpdate.Append(" WHERE Note_Id=")
        strUpdate.Append(NoteId)
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate.ToString)
        Catch ex As Exception
            raiseerror(ex)
        Finally
            strUpdate = Nothing
        End Try
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
    Public Shared Sub MoveNotes(ByVal OldDOCID As Int64, ByVal NewDOCID As Int64, ByVal Mover As Boolean)
        'MOVER=FALSE then copiar
        Dim sql As New System.Text.StringBuilder
        Try
            If Mover = True Then
                sql.Append("Update doc_Notes set DOC_ID=" & NewDOCID & " where doc_ID=" & OldDOCID)
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                sql.Append("Select * from doc_notes where Doc_ID=" & OldDOCID)
                Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
                Dim i, J As Int32
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sql.Remove(0, sql.Length)
                    sql.Append("Insert into Doc_Notes Values(")
                    For J = 0 To ds.Tables(0).Columns.Count - 1
                        If J = 0 Then sql.Append(CoreData.GetNewID(IdTypes.NOTEID))
                        sql.Append(",")
                        If (J >= 1 AndAlso J <= 5) OrElse (J >= 10 AndAlso J <= 13) Then
                            sql.Append(ds.Tables(0).Rows(i).Item(J))
                        Else
                            sql.Append("'")
                            sql.Append(ds.Tables(0).Rows(i).Item(J))
                            sql.Append("',")
                        End If
                        'sql.Append(sql.ToString.TrimEnd(","))
                        sql.Append(")")
                    Next
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    'sql.Remove(0, sql.Length)
                Next
            End If
        Finally
            sql = Nothing
        End Try
    End Sub
    Public Shared Sub Delete(ByVal NoteId As Int32)
        Dim strDelete As String = "DELETE FROM DOC_Notes WHERE Note_Id=" & NoteId
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
        strDelete = Nothing
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina una nota.
    ''' </summary>
    ''' <param name="DocumentId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteNotes(ByVal DocumentId As Int64)
        Dim strDelete As String = "DELETE FROM DOC_Notes WHERE Doc_Id = " & DocumentId
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
        strDelete = Nothing
    End Sub

    Public Shared Sub NewNote(ByVal table As String, ByVal columns As String, ByVal Values As String)
        Dim Strinsert As String = "INSERT INTO " & table & " (" & columns & ") VALUES (" & Values & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
    End Sub
    Public Shared Function GetNotes(ByVal Document_Id As Int32) As DataSet
        Dim StrSelect As String = "Select NOTE_ID,NOTE_TEXT,USER_ID,NOTE_DATE,NOTE_TIME,USER_NAME,X_SIZE,Y_SIZE,X_POSITION,Y_POSITION,type,imageid,encrypted from Doc_Notes where Doc_Id = " & Document_Id
        Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
    End Function

    Public Shared Sub SaveNote(ByVal query As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

End Class
