Imports ZAMBA.Servers
Imports Zamba.Data
Imports Zamba.Core

Public Class NotesBusiness
    Inherits ZClass
    Public Overrides Sub Dispose()
    End Sub

    'Public Shared Sub InsertNewNote(ByVal Doc_Id As Integer, ByVal table As String, ByVal Note As Note)
    '    NotesFactory.InsertNewNote(Doc_Id, table, Note)
    'End Sub
    'Public Shared Sub UpdateNote(ByVal Doc_Id As Integer, ByVal Note As Sign)
    '    NotesFactory.updatenote(Doc_Id, Note)
    'End Sub

    Public Shared Sub Save(ByVal NoteText As String, ByVal X As Decimal, ByVal Y As Decimal, ByVal NoteId As Int32)
        NotesFactory.Save(NoteText, X, Y, NoteId)
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
        NotesFactory.MoveNotes(OldDOCID, NewDOCID, Mover)
    End Sub
    Public Shared Sub Delete(ByVal NoteId As Int32)
        NotesFactory.Delete(NoteId)
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
        NotesFactory.DeleteNotes(DocumentId)
    End Sub
    Public Shared Sub NewNote(ByVal table As String, ByVal columns As String, ByVal Values As String)
        NotesFactory.NewNote(table, columns, Values)
    End Sub

    Public Shared Function GetNotes(ByVal Document_Id As Int32) As DataSet
        return NotesFactory.GetNotes(Document_Id)
    End Function

    Public Shared Sub SaveNote(ByVal query As String)
        NotesFactory.SaveNote(query)
    End Sub

End Class
