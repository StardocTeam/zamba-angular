Option Strict Off
Option Explicit Off


'TODO TERMINAR CON EZEQUIEL


''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.LotusBtn
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para generar el Boton "Ver en ZAMBA" y utilizarlo en los mails enviados
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	19/07/2006	Created
'''     [Hernan]    17/10/2006  Modified
''' </history>
''' -----------------------------------------------------------------------------
Public Class LotusBtn
    Inherits ZClass
    Implements ILotusBtn

    Public Event Log() Implements ILotusBtn.Log

    Public Function CreateNewButton(ByVal s As Object, ByVal this_db As Object, ByVal sLink As String) As Object Implements ILotusBtn.CreateNewButton
        ' Purpose: Build a document using DXL and import into the current database and return the new document
        Dim newdoc As Object
        Dim stream As Object
        Dim dmp As Object
        Dim btn As String
        Try
            stream = s.CreateStream
            ' Build the DXL document including the button in the Rich Text field
            btn = "<?xml version='1.0' encoding='utf-8' ?>" & Chr(13) _
            & "<!DOCTYPE database SYSTEM 'xmlschemas/domino_6_5_1.dtd'>" & Chr(13) _
            & "<database xmlns=""http://www.lotus.com/dxl"" version=""1.01"">" & Chr(13) _
            & "<databaseinfo replicaid=""" & this_db.replicaid & """/>" & Chr(13) _
            & "<document form=""tmpButtonProfile"">" & Chr(13) _
            & "<item name='tmpButtonBody'>" & Chr(13) _
            & "<richtext>" & Chr(13) _
            & "<par>" & Chr(13) _
            & "<button width=""2in"" widthtype=""fitcontent"" wraptext=""true"" bgcolor=""system"" name = ""AccessButton"" type=""normal"" default=""false"" edge=""rounded"" readingorder=""lefttoright"">" & Chr(13) _
            & "<font size=""9pt"" style=""bold"" name=""Arial"" pitch=""variable"" truetype=""true"" familyid=""20"" />" & Chr(13) _
            & "<code event=""click""><formula>@URLOpen(""" & sLink.Replace("&", "&amp;") & """)</formula>" & Chr(13) _
            & "</code>Ver en Zamba</button>" & Chr(13) _
            & "</par>" & Chr(13) _
            & "</richtext>" & Chr(13) _
            & "</item>" & Chr(13) _
            & "</document>" & Chr(13) _
            & "</database>"
            stream.WriteText(btn)

            ' Import new document with button into current database
            dmp = s.CreateDXLImporter(stream, this_db)
            dmp.DocumentImportOption = 2
            'dmp.DocumentImportOption =DXLIMPORTOPTION_CREATE   
            dmp.Process()

            ' Get the NoteID of the newly created document
            Dim tmpNoteID As String
            tmpNoteID = dmp.GetFirstImportedNoteID()

            ' Get the document by the NoteID and return it
            newdoc = this_db.GetDocumentByID(tmpNoteID)
            If Not (newdoc Is Nothing) Then
                Return newdoc
            Else
                Return Nothing
            End If
        Catch ex As Exception
            raiseerror(ex)
            ' If there's a problem processing the DXL, you will be prompted with the Row and Column
            'MsgBox(dmp.Log)
            'Print Lsi_info(2) + " - Error #" & Format$(Err) & " at line " & Format$(Erl) & ": " & Error$
            '   	Messagebox.Show(Lsi_info(2) + " - Error #" & Format$(Err) & " at line " & Format$(Erl) & ": " & Error$, 0, "Error")
        End Try
        Return Nothing
    End Function

    '#Region "Llamada"

    '   ''''' Nuevo que genera el boton en el body

    'Set body = New NotesRichTextItem(docA, "Body")

    'Set newdoc = CreateNewButton(session, session.currentdatabase)

    '   ' Get the Rich Text field
    'Set nitem = newdoc.GetFirstItem( "tmpButtonBody" )
    'If ( nitem.Type = RICHTEXT ) Then
    '	Set rtitem = nitem
    '   ' Append the newly created button
    '	Call body.AppendRTItem( rtitem )
    'Else
    '   ' If there's a problem getting the Rich Text field, print to the memo
    '	Call body.AppendText("Error creating button.")
    'End If

    '   ' Delete the imported document
    'Call newdoc.Remove(True)


    '   ' Update the Rich Text field
    'Call body.Update


    '''''
    '#End Region

    Public Overrides Sub Dispose()

    End Sub
End Class

