Imports System.Collections.Generic
Imports Zamba.Core

''' <summary>
''' Formulario que se encarga de la impresion de documentos
''' </summary>
''  <history>Marcelo modified 03/02/2009</history>
''' <remarks></remarks>
Public Class frmchooseprintmode
    Inherits Form

    Private LoadAction As LoadAction

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal Results As List(Of IPrintable), ByVal action As LoadAction)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Me.Results = Results
        '  MoveToTemp()
        LoadAction = action
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btnPrint As ZButton
    Friend WithEvents chkidoc As System.Windows.Forms.CheckBox
    Friend WithEvents chkiind As System.Windows.Forms.CheckBox
    Friend WithEvents chkasistente As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chknormal As System.Windows.Forms.RadioButton
    Friend WithEvents pnlPrint As ZPanel
    Friend WithEvents btnCancel As ZButton
    Friend WithEvents Label4 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(frmchooseprintmode))
        btnPrint = New ZButton()
        chkidoc = New System.Windows.Forms.CheckBox()
        chkiind = New System.Windows.Forms.CheckBox()
        chkasistente = New System.Windows.Forms.RadioButton()
        Label1 = New ZLabel()
        Label3 = New ZLabel()
        GroupBox1 = New GroupBox()
        chknormal = New System.Windows.Forms.RadioButton()
        Label4 = New ZLabel()
        pnlPrint = New ZPanel()
        btnCancel = New ZButton()
        GroupBox1.SuspendLayout()
        pnlPrint.SuspendLayout()
        SuspendLayout()
        '
        'btnPrint
        '
        btnPrint.BackColor = System.Drawing.Color.DodgerBlue
        btnPrint.FlatAppearance.BorderSize = 0
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.Font = New Font("Tahoma", 10.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnPrint.ForeColor = System.Drawing.Color.Black
        btnPrint.Location = New System.Drawing.Point(196, 236)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New System.Drawing.Size(127, 29)
        btnPrint.TabIndex = 0
        btnPrint.Text = "Imprimir"
        btnPrint.UseVisualStyleBackColor = False
        '
        'chkidoc
        '
        chkidoc.Checked = True
        chkidoc.CheckState = System.Windows.Forms.CheckState.Checked
        chkidoc.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        chkidoc.ForeColor = System.Drawing.Color.Black
        chkidoc.Location = New System.Drawing.Point(17, 18)
        chkidoc.Name = "chkidoc"
        chkidoc.Size = New System.Drawing.Size(266, 24)
        chkidoc.TabIndex = 1
        chkidoc.Text = "Imprimir Documento"
        '
        'chkiind
        '
        chkiind.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        chkiind.ForeColor = System.Drawing.Color.Black
        chkiind.Location = New System.Drawing.Point(289, 18)
        chkiind.Name = "chkiind"
        chkiind.Size = New System.Drawing.Size(266, 24)
        chkiind.TabIndex = 2
        chkiind.Text = "Imprimir Atributos"
        '
        'chkasistente
        '
        chkasistente.Checked = True
        chkasistente.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        chkasistente.ForeColor = System.Drawing.Color.Black
        chkasistente.Location = New System.Drawing.Point(16, 19)
        chkasistente.Name = "chkasistente"
        chkasistente.Size = New System.Drawing.Size(184, 24)
        chkasistente.TabIndex = 3
        chkasistente.TabStop = True
        chkasistente.Text = "Usar Asistente de Windows"
        '
        'Label1
        '
        Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 8.0!)
        Label1.FontSize = 8.0!
        Label1.ForeColor = System.Drawing.Color.White
        Label1.Location = New System.Drawing.Point(17, 268)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(610, 29)
        Label1.TabIndex = 6
        Label1.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 8.0!)
        Label3.FontSize = 8.0!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(40, 46)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(564, 30)
        Label3.TabIndex = 7
        Label3.Text = "El asistente permite visualizar las imagenes a imprimir y seleccionarlas visualme" &
    "nte."
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GroupBox1.Controls.Add(chknormal)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(chkasistente)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Location = New System.Drawing.Point(17, 48)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(610, 182)
        GroupBox1.TabIndex = 8
        GroupBox1.TabStop = False
        '
        'chknormal
        '
        chknormal.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        chknormal.ForeColor = System.Drawing.Color.Black
        chknormal.Location = New System.Drawing.Point(16, 95)
        chknormal.Name = "chknormal"
        chknormal.Size = New System.Drawing.Size(184, 24)
        chknormal.TabIndex = 8
        chknormal.Text = "Usar impresion Normal"
        '
        'Label4
        '
        Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 8.0!)
        Label4.FontSize = 8.0!
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(40, 122)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(564, 30)
        Label4.TabIndex = 9
        Label4.Text = "Se seleccionaran las paginas a imprimir indicando el numero de paginas."
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'pnlPrint
        '
        pnlPrint.BackColor = System.Drawing.Color.White
        pnlPrint.Controls.Add(Label1)
        pnlPrint.Controls.Add(GroupBox1)
        pnlPrint.Controls.Add(btnCancel)
        pnlPrint.Controls.Add(btnPrint)
        pnlPrint.Controls.Add(chkidoc)
        pnlPrint.Controls.Add(chkiind)
        pnlPrint.Dock = System.Windows.Forms.DockStyle.Fill
        pnlPrint.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        pnlPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        pnlPrint.Location = New System.Drawing.Point(2, 2)
        pnlPrint.Name = "pnlPrint"
        pnlPrint.Size = New System.Drawing.Size(644, 273)
        pnlPrint.TabIndex = 9
        '
        'btnCancel
        '
        btnCancel.BackColor = System.Drawing.Color.DodgerBlue
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Tahoma", 10.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnCancel.ForeColor = System.Drawing.Color.Black
        btnCancel.Location = New System.Drawing.Point(329, 236)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New System.Drawing.Size(127, 29)
        btnCancel.TabIndex = 0
        btnCancel.Text = "Cancelar"
        btnCancel.UseVisualStyleBackColor = False
        '
        'frmchooseprintmode
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        BackColor = System.Drawing.Color.WhiteSmoke
        ClientSize = New System.Drawing.Size(648, 277)
        Controls.Add(pnlPrint)
        ForeColor = System.Drawing.Color.Black
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmchooseprintmode"
        Padding = New System.Windows.Forms.Padding(2)
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Zamba - Impresion"
        GroupBox1.ResumeLayout(False)
        pnlPrint.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private Results As List(Of IPrintable)
    Dim m_sDirTemp As String = Application.StartupPath & "\TempPrinter\"

    Private Sub frmchooseprintmode_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        If Tools.EnvironmentUtil.getWindowsVersion() <> Tools.EnvironmentUtil.Windows.WindowsXp Then
            chkasistente.Checked = False
        End If
        If AreThereImages(Results) = False Then
            chkasistente.Checked = False
            chknormal.Checked = True
            chkasistente.Enabled = False
        End If
        Select Case LoadAction
            Case LoadAction.OnlyIndexs
                'solo habilito la impresion de atributos
                chkiind.Checked = True
                chkiind.Enabled = False
                chkidoc.Checked = False
                chkidoc.Enabled = False
                chkasistente.Visible = False
                chknormal.Visible = False
                Label3.Visible = False
                Label4.Visible = False
            Case LoadAction.ShowPreview
                'muestra directamente el preview sin pasar por el form
                chkidoc.Checked = True
                Print()
        End Select
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una copia temporal de los los documentos a imprimir
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	01/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub MoveToTemp()
        Dim sBuffer As New System.Text.StringBuilder
        Try
            Dim sNombreDeArchivo As System.String
            Dim sPathTemporal As System.String
            Dim sExtension As System.String

            Try
                If IO.Directory.Exists(m_sDirTemp) Then
                    DeleteTempFiles()
                    IO.Directory.CreateDirectory(m_sDirTemp)
                Else
                    IO.Directory.CreateDirectory(m_sDirTemp)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            For Each R As IPrintable In Results
                If R.IsOpen = False Then
                    Try
                        sNombreDeArchivo = FileName(R)
                        sPathTemporal = m_sDirTemp & sNombreDeArchivo
                        sExtension = sNombreDeArchivo.Substring(sNombreDeArchivo.LastIndexOf(".")).Replace(".", "")

                        IO.File.Copy(R.FullPath, sPathTemporal, True)

                        R.PrintName = sPathTemporal

                        With sBuffer
                            .Append(R.Name)
                            .Append(" ")
                            .Append(sExtension)
                            .Append(ControlChars.NewLine)
                        End With

                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            sBuffer.Remove(0, sBuffer.Length)
            sBuffer = Nothing
        End Try

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el nombre completo del archivo partiendo de su fullpath
    ''' </summary>
    ''' <param name="result">un result</param>
    ''' <returns>nombre completo del archivo</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	01/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function FileName(ByRef Result As IPrintable) As String
        Dim sName As String
        Dim sFullPath As String

        Try
            If Not IsNothing(Result) AndAlso Not IsNothing(Result.FullPath) Then
                sFullPath = Result.FullPath
                'Extrae el nombre
                If IO.File.Exists(sFullPath) Then
                    sName = sFullPath.Substring(sFullPath.LastIndexOf("\")).Replace("\", "")
                Else
                    Throw New ArgumentException("La ruta que hace referencia a " & sFullPath & " , No es  valida.")
                End If
            Else
                Throw New ArgumentException("La ruta no es valida.")
            End If

            Return sName
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        End Try

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub Print()
        Try
            'GC.Collect()
            Cursor = Cursors.WaitCursor
            Label1.Text = "Imprimiendo Documentos"
            Application.DoEvents()

            If chkidoc.Checked Then
                PrintDocument(Results)
            End If

            Label1.Text = "Imprimiendo Atributos"
            Application.DoEvents()

            If chkiind.Checked Then
                PrintIndexs(Results)
            End If

            Label1.Text = "Impresion Finalizada"
            Application.DoEvents()

            Close()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DeleteTempFiles()
        Try
            IO.Directory.Delete(m_sDirTemp, True)
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        End Try
    End Sub

#Region "Browser y Grilla"
    'Private Sub ImprimirDesdeBrowser()
    '    For Each dc As DockContent In Me.DockPanelResults.Contents
    '        If TypeOf dc Is UCDocumentViewer2 Then
    '            Dim zvc As UCDocumentViewer2 = dc
    '            If zvc.Result.Id = Me.LocalResult.Id Then
    '                zvc.Activate()
    '                DirectCast(zvc.Controls(0), WebBrowser).PrintDocument()
    '                Exit Sub
    '            End If
    '        End If
    '    Next
    'End Sub
    'Private Sub EnviarKeys()
    '    '  Dim key As SendKeys
    '    ImprimirDesdeBrowser()
    '    'SendKeys.Send("^(p)")
    'End Sub
    ''Public Sub PrintTXT()
    ''    'Try
    ''    Dim owb As WebBrowser = Me.ContentDocumento.Controls(0)
    ''    owb.PrintTxt()
    ''    'Catch ex As Exception
    ''    '                              Zamba.AppBlock.ZException.Log(ex,True)
    ''    'End Try
    ''End Sub

    ''Public Sub printGrillaIndexs()
    ''    Dim results2print As New ArrayList
    ''    Dim temp As Windows.Forms.ListView.ListViewItemCollection
    ''    Try
    ''        If Not IsNothing(ResultsGrid) Then
    ''            'For i As Int16 = 0 To ResultsGrid.SelectedResults.Length - 1
    ''            For Each res As Result In ResultsGrid.SelectedResults
    ''                If RightFactory.GetUserRights(iuser.ObjectTypes.Documents, iuser.RightsType.Print) Then
    ''                    If res.IsImage Then
    ''                        Dim Printer As New ZPrinter
    ''                        AddHandler Printer.LogError, AddressOf zamba.core.zclass.raiseerror
    ''                        AddHandler Printer.Printed, AddressOf PrintedPages
    ''                        Printer.printindexs(res, ZPrinting.PrintConfig, ZPrinting.PrintConfig.DefaultPageSettings)
    ''                    End If
    ''                    RightFactory.SaveAction(Result.Id, iuser.ObjectTypes.Documents, iuser.RightsType.Print, Result.Name & " Hojas Impresas: " & Me.PagesCount)
    ''                End If
    ''            Next
    ''        Else
    ''            printDoc()
    ''        End If
    ''    Catch ex As Exception
    ''        '                          Zamba.AppBlock.ZException.Log(ex,True)
    ''        MessageBox.Show(ex.ToString)
    ''    End Try
    ''End Sub
#End Region

    Dim PagesCount As Int64
    Private Sub PrintedPages(ByVal count As Int64)
        PagesCount = count
    End Sub
    Public Event SavePrintAction()
    'Evento que se dispara si el formulario a imprimir es virtual
    Public Event PrintVirtual(ByVal result As IPrintable)
    Private Sub SaveAction(ByRef Result As IPrintable)
        RaiseEvent SavePrintAction()
        'TodoÑ falta que se envieen los argumentos y que el controlador atrape el evento y logee
        'RightFactory.SaveAction(Result.ID, IUser.ObjectTypes.Documents, IUser.RightsType.Print, Result.Name & " Hojas Impresas: " & Me.PagesCount)
    End Sub
    Private Sub ShowMsg(ByVal Msg As String)
        Label1.Text = Msg
        Application.DoEvents()
    End Sub

    Private Function CheckFile(ByVal strPath As String) As Boolean
        Try
            If Not FileIO.FileSystem.FileExists(strPath) Then
                Label1.Text += " - El documento no se encuentra en el servidor"
                Return False
            End If

            Return True

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Imprime los documentos que estan relacionados con un conjunto de Result
    ''' </summary>
    ''' <param name="results">Result de Documentos</param>
    ''' <remarks>
    '''     Para imprimir utiliza la funcionalidad de la libreria 
    '''     mshtml.dll PrintHTML.
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	    31/07/2006	Created
    ''' 	[Marcelo]	03/02/2009	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub PrintDocument(ByVal results As List(Of IPrintable))
        Try
            If AreThereImages(results) Then
                Dim Printer As New ZPrinter
                RemoveHandler Printer.Printed, AddressOf PrintedPages
                AddHandler Printer.Printed, AddressOf PrintedPages
                RemoveHandler Printer.ShowMsg, AddressOf ShowMsg
                AddHandler Printer.ShowMsg, AddressOf ShowMsg
                If chkasistente.Checked Then
                    Label1.Text = "Imprimiendo Imagenes con Asistente"
                    Application.DoEvents()
                    Printer.printWia(results)
                Else
                    Label1.Text = "Imprimiendo Imagenes"
                    Application.DoEvents()

                    Printer.PrintDocument(results, ZPrinting.PrintConfig, ZPrinting.PrintConfig.DefaultPageSettings)
                End If
            End If
            If AreThereNoImages(results) Then
                Dim Files As New System.Text.StringBuilder
                For Each R As IResult In results
                    If Not R.IsImage AndAlso R.IsOpen = False Then
                        If R.IsExcel Then
                            If Not IsNothing(R.File) And CheckFile(R.File) Then
                                Zamba.Print.PrintGrilla.ImprimirExcel(R.File, True)
                            ElseIf CheckFile(R.FullPath) Then
                                Zamba.Print.PrintGrilla.ImprimirExcel(R.FullPath, True)
                            End If
                        ElseIf R.IsWord Then
                            If Not IsNothing(R.File) And CheckFile(R.File) Then
                                Zamba.Print.PrintGrilla.ImprimirWord(R.File)
                            ElseIf CheckFile(R.FullPath) Then
                                Zamba.Print.PrintGrilla.ImprimirWord(R.FullPath)
                            End If
                        Else
                            Label1.Text = "Imprimiendo Documento " & R.Name
                            Application.DoEvents()
                            'Si es virtual no se imprime, sino que deja que lo imprima el formbrowser
                            If Not R.ISVIRTUAL Then
                                R.PrintName = R.FullPath
                                If Not IsNothing(R.PrintName) And CheckFile(R.PrintName) Then
                                    If R.IsPDF Then
                                        Dim psiPrint As New ProcessStartInfo
                                        psiPrint.UseShellExecute = True
                                        psiPrint.CreateNoWindow = True
                                        psiPrint.Verb = "print"
                                        psiPrint.WindowStyle = ProcessWindowStyle.Hidden
                                        psiPrint.FileName = R.PrintName
                                        System.Diagnostics.Process.Start(psiPrint)
                                    Else
                                        Shell("rundll32.exe " & Environment.SystemDirectory & "\mshtml.dll,PrintHTML " & Chr(34) & R.PrintName & Chr(34), AppWinStyle.Hide, False)
                                    End If
                                End If
                            Else
                                RaiseEvent PrintVirtual(R)
                                'Mostrar el preview
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub PrintIndexs(ByVal results As List(Of IPrintable))
        Dim Printer As New ZPrinter
        Try
            RemoveHandler Printer.Printed, AddressOf PrintedPages
            AddHandler Printer.Printed, AddressOf PrintedPages
            RemoveHandler Printer.ShowMsg, AddressOf ShowMsg
            AddHandler Printer.ShowMsg, AddressOf ShowMsg
            For Each Result As IPrintable In results
                If Not IsNothing(Result) Then
                    Printer.printindexs(Result, ZPrinting.PrintConfig, ZPrinting.PrintConfig.DefaultPageSettings)
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Printer.Dispose()
        End Try
    End Sub

    Private Function AreThereNoImages(ByVal Results As List(Of IPrintable)) As Boolean
        'Le agregue  "As Result()"
        For Each R As IPrintable In Results
            If Not R.IsImage Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function AreThereImages(ByVal Results As List(Of IPrintable)) As Boolean
        For Each R As IPrintable In Results
            If R.IsImage Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class

Public Enum LoadAction
    ShowForm
    OnlyIndexs
    ShowPreview
End Enum
'rundll32.exe C:\WINDOWS\System32\shimgvw.dll,ImageView_PrintTo /pt "%1" "%2" "%3" "%4"

'rundll32.exe %SystemRoot%\System32\mshtml.dll,PrintHTML "%1" "%2" "%3" "%4"

'rundll32.exe %SystemRoot%\System32\mshtml.dll,PrintHTML "%1" "%2" "%3" "%4"


'FileOpen "%1":FilePrint 1, 32767

'FilePrint 1, 32767:FileExit 0

