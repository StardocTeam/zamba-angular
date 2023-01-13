Imports Zamba.Core

''' <summary>
''' Formulario que se encarga de la impresion de documentos
''' </summary>
''  <history>Marcelo modified 03/02/2009</history>
''' <remarks></remarks>
Public Class frmchooseprintmode
    Inherits Form

    Private OnlyIndexs As Boolean
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal Results() As IPrintable, ByVal OnlyIndexs As Boolean)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Me.Results = Results
        '  MoveToTemp()
        Me.OnlyIndexs = OnlyIndexs
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkidoc As System.Windows.Forms.CheckBox
    Friend WithEvents chkiind As System.Windows.Forms.CheckBox
    Friend WithEvents chkasistente As System.Windows.Forms.RadioButton
    Friend WithEvents lbldocs As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chknormal As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmchooseprintmode))
        Me.Button1 = New System.Windows.Forms.Button
        Me.chkidoc = New System.Windows.Forms.CheckBox
        Me.chkiind = New System.Windows.Forms.CheckBox
        Me.chkasistente = New System.Windows.Forms.RadioButton
        Me.lbldocs = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chknormal = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightSlateGray
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(336, 240)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(152, 35)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Imprimir"
        '
        'chkidoc
        '
        Me.chkidoc.Checked = True
        Me.chkidoc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkidoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkidoc.ForeColor = System.Drawing.Color.White
        Me.chkidoc.Location = New System.Drawing.Point(264, 40)
        Me.chkidoc.Name = "chkidoc"
        Me.chkidoc.Size = New System.Drawing.Size(168, 26)
        Me.chkidoc.TabIndex = 1
        Me.chkidoc.Text = "Imprimir Documento"
        '
        'chkiind
        '
        Me.chkiind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkiind.ForeColor = System.Drawing.Color.White
        Me.chkiind.Location = New System.Drawing.Point(264, 72)
        Me.chkiind.Name = "chkiind"
        Me.chkiind.Size = New System.Drawing.Size(160, 26)
        Me.chkiind.TabIndex = 2
        Me.chkiind.Text = "Imprimir Indices"
        '
        'chkasistente
        '
        Me.chkasistente.Checked = True
        Me.chkasistente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkasistente.ForeColor = System.Drawing.Color.White
        Me.chkasistente.Location = New System.Drawing.Point(16, 16)
        Me.chkasistente.Name = "chkasistente"
        Me.chkasistente.Size = New System.Drawing.Size(184, 26)
        Me.chkasistente.TabIndex = 3
        Me.chkasistente.Text = "Usar Asistente de Windows"
        '
        'lbldocs
        '
        Me.lbldocs.BackColor = System.Drawing.Color.White
        Me.lbldocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbldocs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldocs.Location = New System.Drawing.Point(24, 40)
        Me.lbldocs.Name = "lbldocs"
        Me.lbldocs.Size = New System.Drawing.Size(216, 288)
        Me.lbldocs.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(24, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(200, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Documentos Seleccionados"
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(256, 280)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(328, 40)
        Me.Label1.TabIndex = 6
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(40, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 32)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "El asistente permite visualizar las imagenes a imprimir y seleccionarlas visualme" & _
        "nte."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chknormal)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkasistente)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(256, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(328, 136)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'chknormal
        '
        Me.chknormal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chknormal.ForeColor = System.Drawing.Color.White
        Me.chknormal.Location = New System.Drawing.Point(16, 72)
        Me.chknormal.Name = "chknormal"
        Me.chknormal.Size = New System.Drawing.Size(184, 26)
        Me.chknormal.TabIndex = 8
        Me.chknormal.Text = "Usar impresion Normal"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(40, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(272, 32)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Se seleccionaran las paginas a imprimir indicando el numero de paginas"
        '
        'frmchooseprintmode
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(602, 344)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbldocs)
        Me.Controls.Add(Me.chkiind)
        Me.Controls.Add(Me.chkidoc)
        Me.Controls.Add(Me.Button1)
        Me.DockPadding.All = 2
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmchooseprintmode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba - Impresion"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Results() As IPrintable
    Dim m_sDirTemp As String = Membership.MembershipHelper.StartUpPath & "\TempPrinter\"

    Private Sub frmchooseprintmode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Zamba.Tools.EnvironmentUtil.getWindowsVersion() <> Tools.EnvironmentUtil.Windows.WindowsXp Then
            Me.chkasistente.Checked = False
        End If
        If Me.AreThereImages(Results) = False Then
            Me.chkasistente.Checked = False
            Me.chknormal.Checked = True
            Me.chkasistente.Enabled = False
        End If
        'solo habilito la impresion de atributos
        If Me.OnlyIndexs Then
            Me.chkiind.Checked = True
            Me.chkiind.Enabled = False
            Me.chkidoc.Checked = False
            Me.chkidoc.Enabled = False
            Me.chkasistente.Visible = False
            Me.chknormal.Visible = False
            Me.Label3.Visible = False
            Me.Label4.Visible = False
        End If
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
                Zamba.AppBlock.ZException.Log(ex, True)
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
                        Zamba.AppBlock.ZException.Log(ex, True)
                    End Try
                End If
            Next
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        Finally
            Me.lbldocs.Text = sBuffer.ToString
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'GC.Collect()
            Me.Cursor = Cursors.WaitCursor
            Me.Label1.Text = "Imprimiendo Documentos"
            Application.DoEvents()
            If Me.chkidoc.Checked Then Me.PrintDocument(Results)
            Me.Label1.Text = "Imprimiendo Indices"
            Application.DoEvents()
            If Me.chkiind.Checked Then Me.PrintIndexs(Results)
            Me.Label1.Text = "Impresion Finalizada"
            Application.DoEvents()
            Me.Close()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        Finally
            Me.Cursor = Cursors.Default
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
        Me.Label1.Text = Msg
        Application.DoEvents()
    End Sub

    Private Function CheckFile(ByVal strPath As String) As Boolean
        Try
            If Not FileIO.FileSystem.FileExists(strPath) Then
                Me.Label1.Text += " - El documento no se encuentra en el servidor"
                Return False
            End If

            Return True

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
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
    Public Sub PrintDocument(ByVal results() As IPrintable)
        Try
            If Me.AreThereImages(results) Then
                Dim Printer As New ZPrinter
                RemoveHandler Printer.Printed, AddressOf PrintedPages
                AddHandler Printer.Printed, AddressOf PrintedPages
                RemoveHandler Printer.ShowMsg, AddressOf ShowMsg
                AddHandler Printer.ShowMsg, AddressOf ShowMsg
                If Me.chkasistente.Checked Then
                    Me.Label1.Text = "Imprimiendo Imagenes con Asistente"
                    Application.DoEvents()
                    Printer.printWia(results)
                Else
                    Me.Label1.Text = "Imprimiendo Imagenes"
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
                            Me.Label1.Text = "Imprimiendo Documento " & R.Name
                            Application.DoEvents()
                            'Si es virtual no se imprime, sino que deja que lo imprima el formbrowser
                            If R.ISVIRTUAL = False Then
                                R.PrintName = R.FullPath
                                If Not IsNothing(R.PrintName) And CheckFile(R.PrintName) Then
                                    If R.IsPDF Then
                                        Dim psiPrint As New ProcessStartInfo
                                        psiPrint.UseShellExecute = True
                                        psiPrint.CreateNoWindow = True
                                        psiPrint.Verb = "print"
                                        psiPrint.WindowStyle = ProcessWindowStyle.Hidden
                                        psiPrint.FileName = R.PrintName
                                        Process.Start(psiPrint)
                                    Else
                                        Shell("rundll32.exe " & System.Environment.SystemDirectory & "\mshtml.dll,PrintHTML " & Chr(34) & R.PrintName & Chr(34), AppWinStyle.Hide, False)
                                    End If
                                End If
                            Else
                                RaiseEvent PrintVirtual(R)
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub
    Public Sub PrintIndexs(ByVal results() As IPrintable)
        Dim Printer As New ZPrinter
        Try
            RemoveHandler Printer.Printed, AddressOf PrintedPages
            AddHandler Printer.Printed, AddressOf PrintedPages
            RemoveHandler Printer.ShowMsg, AddressOf ShowMsg
            AddHandler Printer.ShowMsg, AddressOf ShowMsg
            For Each Result As IPrintable In results
                If Not IsNothing(Result) Then
                    Printer.printindexs(results, ZPrinting.PrintConfig, ZPrinting.PrintConfig.DefaultPageSettings)
                End If
            Next
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        Finally
            Printer.Dispose()
        End Try
    End Sub

    Private Function AreThereNoImages(ByVal Results As IPrintable()) As Boolean
        'Le agregue  "As Result()"
        For Each R As IPrintable In Results
            If Not R.IsImage Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function AreThereImages(ByVal Results As IPrintable()) As Boolean
        For Each R As IPrintable In Results
            If R.IsImage Then
                Return True
            End If
        Next
        Return False
    End Function
End Class


'rundll32.exe C:\WINDOWS\System32\shimgvw.dll,ImageView_PrintTo /pt "%1" "%2" "%3" "%4"

'rundll32.exe %SystemRoot%\System32\mshtml.dll,PrintHTML "%1" "%2" "%3" "%4"

'rundll32.exe %SystemRoot%\System32\mshtml.dll,PrintHTML "%1" "%2" "%3" "%4"


'FileOpen "%1":FilePrint 1, 32767

'FilePrint 1, 32767:FileExit 0

