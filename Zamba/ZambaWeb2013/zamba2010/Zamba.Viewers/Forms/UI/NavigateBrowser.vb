Imports ZAMBA.Core

Public Class NavigateBrowser
    Inherits ZControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        Try
            InitializeComponent()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Ocurrio un error al Visualizar el objeto " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
        End Try
        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            CloseWebBrowser()
            If Not IsNothing(AxWebBrowser1) Then
                AxWebBrowser1.Dispose()
                AxWebBrowser1 = Nothing
            End If

            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AxWebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents Panel3 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        AxWebBrowser1 = New System.Windows.Forms.WebBrowser
        Panel1 = New ZPanel
        Panel2 = New ZPanel
        Button1 = New ZButton
        TextBox1 = New TextBox
        Label1 = New ZLabel
        Panel3 = New ZPanel
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        '
        'AxWebBrowser1
        '
        AxWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        AxWebBrowser1.Location = New System.Drawing.Point(0, 0)
        AxWebBrowser1.Name = "AxWebBrowser1"
        AxWebBrowser1.Size = New System.Drawing.Size(552, 421)
        AxWebBrowser1.TabIndex = 0
        '
        'Panel1
        '


        Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel1.Location = New System.Drawing.Point(0, 445)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(552, 32)
        Panel1.TabIndex = 3
        '
        'Panel2
        '


        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(TextBox1)
        Panel2.Controls.Add(Label1)
        Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(552, 24)
        Panel2.TabIndex = 4
        '
        'Button1
        '
        Button1.BackColor = System.Drawing.Color.MediumSeaGreen
        Button1.Dock = System.Windows.Forms.DockStyle.Right
        Button1.Font = New Font("Microsoft Sans Serif", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Button1.ForeColor = System.Drawing.Color.White
        Button1.Location = New System.Drawing.Point(512, 0)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(40, 24)
        Button1.TabIndex = 2
        Button1.Text = "Ir"
        Button1.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        TextBox1.Location = New System.Drawing.Point(100, 0)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(452, 21)
        TextBox1.TabIndex = 1
        '
        'Label1
        '
        Label1.Dock = System.Windows.Forms.DockStyle.Left
        Label1.Location = New System.Drawing.Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(100, 24)
        Label1.TabIndex = 0
        Label1.Text = "Dirección"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        '
        'Panel3
        '


        Panel3.Controls.Add(AxWebBrowser1)
        Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Panel3.Location = New System.Drawing.Point(0, 24)
        Panel3.Name = "Panel3"
        Panel3.Size = New System.Drawing.Size(552, 421)
        Panel3.TabIndex = 5
        '
        'NavigateBrowser
        '
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "NavigateBrowser"
        Size = New System.Drawing.Size(552, 477)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Navigate"
    Public Sub Navigate(ByVal fi As IO.FileInfo)
        Try
            AxWebBrowser1.Navigate(fi.FullName)
        Catch ex As System.ExecutionEngineException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            'Catch
        End Try
    End Sub
    Public Sub Navigate(ByVal file As String)
        Try
            AxWebBrowser1.Navigate(file)
        Catch ex As System.ExecutionEngineException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Save"
    Public Sub SaveDocument()
        Try
            AxWebBrowser1.ShowSaveAsDialog()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

    End Sub

#End Region

    Public Sub CloseWebBrowser()
        Try
            Try
                If Not IsNothing(AxWebBrowser1) Then AxWebBrowser1.Navigate("about:blank")
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try

            Try
                If Not IsNothing(AxWebBrowser1) Then
                    AxWebBrowser1.Dispose()
                    AxWebBrowser1 = Nothing
                End If
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try
            Try
                Dispose(True)
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

    End Sub

    Public Sub Print()
        Try
            'Me.AxWebBrowser2.QueryStatusWB(SHDocVw.OLECMDID.OLECMDID_PRINT)
            'If IsPrinterEnabled() Then
            AxWebBrowser1.print()
            '.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
            'End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Private Function IsPrinterEnabled() As Boolean
    '    Try
    '        Dim response As Int32
    '        response = Me.AxWebBrowser1.print
    '        '.QueryStatusWB(SHDocVw.OLECMDID.OLECMDID_PRINT)
    '        If response <> 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Function


    Protected Overrides Sub Finalize()

        MyBase.Finalize()
        Dispose(False)
    End Sub

    Public Sub ShowToolbars()
        Try
            AxWebBrowser1.ScrollBarsEnabled = True

        Catch ex As Exception
        End Try
    End Sub


#Region "Events"
    Private Sub AxWebBrowser2_NavigateComplete2(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles AxWebBrowser1.Navigated
        Try
            AxWebBrowser1.ScrollBarsEnabled = True
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AxWebBrowser2_DocumentComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles AxWebBrowser1.DocumentCompleted
        Try
            '   e.pDisp.document.all("zamba").value = "martin"
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    'Private Sub AxWebBrowser1_BeforeNavigate2(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles AxWebBrowser1.Navigating
    '    Try
    '        El Form debe tener Id = zamba_(doctypeid) o Id = zamba_(Nombre del DocType)
    '        Los controles deben tener Id = "zamba(Id de Atributo)"  o Id = "zamba_(Nombre del indice)"

    '        Me.AsignValues(e)
    '        Dim doc1 As mshtml.HTMLDocumentClass
    '        doc1 = e.pDisp.document

    '        Dim i As Int32
    '        'metodo por id
    '        For i = 0 To doc1.all.length - 1
    '            Try
    '                If doc1.all.item(i).id.ToString.Substring(0, 5).ToLower = "zamba" Then
    '                    Select Case doc1.all.item(i).tagname
    '                        Case "INPUT", "SELECT"
    '                            Select Case doc1.all.item(i).type
    '                                Case "submit"
    '                                    Select Case doc1.all.item(i).id.ToString.Substring(6)
    '                                        Case "insert", "Insert", "Insertar"
    '                                            Me.FormType = ZwebForm.FormTypes.Insert
    '                                        Case "update", "Update", "Actualizar"
    '                                            Me.FormType = ZwebForm.FormTypes.Edit
    '                                        Case "search", "Search", "Buscar"
    '                                            Me.FormType = ZwebForm.FormTypes.Search
    '                                    End Select
    '                                Case "text"
    '                                    Dim x As Int32
    '                                    For x = 0 To Result.Indexs.Count - 1
    '                                        Try
    '                                            If Result.Indexs(x).Index_Id = DirectCast(doc1.all.item(i).id.ToString.Substring(6)) Then
    '                                                Result.Indexs(x).Data = doc1.all.item(i).value
    '                                            End If
    '                                        Catch
    '                                            If Result.Indexs(x).Index_Name = doc1.all.item(i).id.ToString Then
    '                                                Result.Indexs(x).Data = doc1.all.item(i).value
    '                                            End If
    '                                        End Try
    '                                    Next
    '                                Case "checkbox"
    '                                Case "radio"
    '                                Case "select-one"
    '                            End Select
    '                        Case "FORM"
    '                            Try
    '                                Result.DocTypeId = DirectCast(doc1.all.item(i).Id.ToString.Substring(6))
    '                            Catch
    '                                Result.DocTypeId = Zamba.Core.DocType.GetDocTypeIdByName(doc1.all.item(i).Id.ToString.Substring(6))
    '                            End Try
    '                        Case "IMG"
    '                            '  Result.FullPath = doc1.all.item(i).Src
    '                    End Select
    '                End If
    '            Catch ex As Exception
    '                Zamba.Core.ZClass.raiseerror(ex)
    '            End Try
    '        Next
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

#End Region
    Private Sub openFile()
        Try
            Dim Open As New OpenFileDialog
            Open.ShowDialog()
            '       Me.ShowDocument(Open.FileName)
            Open.Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        openFile()
    End Sub
    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'LoadIndex()
    'End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text.Trim <> "" Then
                Navigate(TextBox1.Text.Trim)
            Else
                Navigate("About:Blank")
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class

