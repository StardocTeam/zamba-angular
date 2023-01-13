Option Strict Off
Option Explicit Off
Imports System.Runtime.InteropServices
Imports System.Text

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
''' </history>
''' -----------------------------------------------------------------------------
Public Class LotusBtn
    Implements IDisposable

    Public Event Log(ByVal Msg As String)

    ''' <summary>
    ''' Crea los botones de los links
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="this_db"></param>
    ''' <param name="arrayLinks"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateNewButton(ByVal s As Object, ByVal this_db As Object, ByVal arrayLinks As ArrayList) As Object
        ' Purpose: Build a document using DXL and import into the current database and return the new document
        Dim newdoc As Object
        Dim stream As Object
        Dim dmp As Object
        Dim btn As Stringbuilder = New Stringbuilder()
        Try
            stream = s.CreateStream
            ' Build the DXL document including the button in the Rich Text field
            btn.Append("<?xml version='1.0' encoding='utf-8' ?>")
            btn.Append(Chr(13))
            btn.Append("<!DOCTYPE database SYSTEM 'xmlschemas/domino_6_5_1.dtd'>")
            btn.Append(Chr(13))
            btn.Append("<database xmlns=""http://www.lotus.com/dxl"" version=""1.01"">")
            btn.Append(Chr(13))
            btn.Append("<databaseinfo replicaid=""")
            btn.Append(this_db.replicaid)
            btn.Append("""/>")
            btn.Append(Chr(13))
            btn.Append("<document form=""tmpButtonProfile"">")
            btn.Append(Chr(13))
            btn.Append("<item name='tmpButtonBody'>")
            btn.Append(Chr(13))
            btn.Append("<richtext>")
            btn.Append(Chr(13))
            btn.Append("<par>")
            btn.Append(Chr(13))

            For Each link As String In arrayLinks
                If link.ToLower.Contains("zamba") = True Then
                    Trace.WriteLine("Link: " & link)
                    If link.ToLower.StartsWith("Zamba:\\\\") = False Then
                        link = link.Replace("\", "\\")
                        Trace.WriteLine("Link Modificado: " & link)
                    End If
                    btn.Append("<button width=""2in"" widthtype=""fitcontent"" wraptext=""true"" bgcolor=""system"" name = ""AccessButton"" type=""normal"" default=""false"" edge=""rounded"" readingorder=""lefttoright"">")
                    btn.Append(Chr(13))
                    btn.Append("<font size=""9pt"" style=""bold"" name=""Arial"" pitch=""variable"" truetype=""true"" familyid=""20"" />")
                    btn.Append(Chr(13))
                    btn.Append("<code event=""click""><formula>@URLOpen(""")
                    btn.Append(link.Replace("&", "&amp;"))
                    btn.Append(""")</formula>")
                    btn.Append(Chr(13))
                    btn.Append("</code>")
                    If link.ToLower.Contains("task") Then
                        btn.Append("Ver en Workflow")
                    Else
                        btn.Append("Ver en Zamba")
                    End If
                    btn.Append("</button>")
                    btn.Append(Chr(13))
                End If
            Next

            btn.Append("</par>")
            btn.Append(Chr(13))
            btn.Append("</richtext>")
            btn.Append(Chr(13))
            btn.Append("</item>")
            btn.Append(Chr(13))
            btn.Append("</document>")
            btn.Append(Chr(13))
            btn.Append("</database>")
            Trace.WriteLine("Codigo Botones:" & btn.ToString())
            stream.WriteText(btn.ToString())

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
            Zamba.AppBlock.ZException.Log(ex)
            ' If there's a problem processing the DXL, you will be prompted with the Row and Column
            'MsgBox(dmp.Log)
            'Print Lsi_info(2) + " - Error #" & Format$(Err) & " at line " & Format$(Erl) & ": " & Error$
            '   	Messagebox.Show(Lsi_info(2) + " - Error #" & Format$(Err) & " at line " & Format$(Erl) & ": " & Error$, 0, "Error")
        End Try
    End Function

#Region "Llamada"

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
#End Region

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
End Class

