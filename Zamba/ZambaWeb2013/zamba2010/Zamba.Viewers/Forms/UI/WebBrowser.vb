Imports ZAMBA.AppBlock
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Web
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
            Me.CloseWebBrowser()
            If Not IsNothing(AxWebBrowser1) Then
                Me.AxWebBrowser1.Dispose()
                Me.AxWebBrowser1 = Nothing
            End If

            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AxWebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents Panel3 As ZBluePanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.AxWebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.Button1 = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3 = New Zamba.AppBlock.ZBluePanel
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxWebBrowser1
        '
        Me.AxWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxWebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.AxWebBrowser1.Name = "AxWebBrowser1"
        Me.AxWebBrowser1.Size = New System.Drawing.Size(552, 421)
        Me.AxWebBrowser1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 445)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(552, 32)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(552, 24)
        Me.Panel2.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(512, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(40, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Ir"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(100, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(452, 21)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dirección"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel3.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel3.Controls.Add(Me.AxWebBrowser1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 24)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(552, 421)
        Me.Panel3.TabIndex = 5
        '
        'NavigateBrowser
        '
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "NavigateBrowser"
        Me.Size = New System.Drawing.Size(552, 477)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

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
                Me.Dispose(True)
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
            Me.AxWebBrowser1.print()
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
        Me.Dispose(False)
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

    Private Sub AxWebBrowser1_BeforeNavigate2(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles AxWebBrowser1.Navigating
        Try
            'El Form debe tener Id = zamba_(doctypeid) o Id = zamba_(Nombre del DocType)
            'Los controles deben tener Id = "zamba(Id de Indice)"  o Id = "zamba_(Nombre del indice)"

            '            Me.AsignValues(e)
            'Dim doc1 As mshtml.HTMLDocumentClass
            'doc1 = e.pDisp.document

            'Dim i As Int32
            ''metodo por id
            'For i = 0 To doc1.all.length - 1
            '    Try
            '        If doc1.all.item(i).id.ToString.Substring(0, 5).ToLower = "zamba" Then
            '            Select Case doc1.all.item(i).tagname
            '                Case "INPUT", "SELECT"
            '                    Select Case doc1.all.item(i).type
            '                        Case "submit"
            '                            Select Case doc1.all.item(i).id.ToString.Substring(6)
            '                                Case "insert", "Insert", "Insertar"
            '                                    Me.FormType = ZwebForm.FormTypes.Insert
            '                                Case "update", "Update", "Actualizar"
            '                                    Me.FormType = ZwebForm.FormTypes.Edit
            '                                Case "search", "Search", "Buscar"
            '                                    Me.FormType = ZwebForm.FormTypes.Search
            '                            End Select
            '                        Case "text"
            '                            Dim x As Int32
            '                            For x = 0 To Result.Indexs.Count - 1
            '                                Try
            '                                    If Result.Indexs(x).Index_Id = DirectCast(doc1.all.item(i).id.ToString.Substring(6)) Then
            '                                        Result.Indexs(x).Data = doc1.all.item(i).value
            '                                    End If
            '                                Catch
            '                                    If Result.Indexs(x).Index_Name = doc1.all.item(i).id.ToString Then
            '                                        Result.Indexs(x).Data = doc1.all.item(i).value
            '                                    End If
            '                                End Try
            '                            Next
            '                        Case "checkbox"
            '                        Case "radio"
            '                        Case "select-one"
            '                    End Select
            '                Case "FORM"
            '                    Try
            '                        Result.DocTypeId = DirectCast(doc1.all.item(i).Id.ToString.Substring(6))
            '                    Catch
            '                        Result.DocTypeId = ZAMBA.Core.DocType.GetDocTypeIdByName(doc1.all.item(i).Id.ToString.Substring(6))
            '                    End Try
            '                Case "IMG"
            '                    '  Result.FullPath = doc1.all.item(i).Src
            '            End Select
            '        End If
            '    Catch ex As Exception
            '       zamba.core.zclass.raiseerror(ex)
            '    End Try
            'Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

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
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        openFile()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'LoadIndex()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.TextBox1.Text.Trim <> "" Then
                Me.Navigate(Me.TextBox1.Text.Trim)
            Else
                Me.Navigate("About:Blank")
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class

