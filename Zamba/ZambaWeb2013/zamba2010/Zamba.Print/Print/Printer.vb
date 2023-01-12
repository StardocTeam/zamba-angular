Imports Zamba.Core
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports interop.wiaNET
Public Class ZPrinter
    Inherits Form

    Public Event ShowMsg(ByVal Msg As String)
    Private WithEvents pdoc As New PrintDocument
    Private WithEvents pinx As New PrintDocument
    Dim Result2Print As iprintable

    Dim oFDimension As System.Drawing.Imaging.FrameDimension
    Dim iCount As Int32
    Dim actualFrame As Int32
    Dim Desde As Int32
    Dim Hasta As Int32
    Dim ok_cancel As Boolean
    Dim VarFromPage As Int32
    Dim VarToPage As Int32

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        ' So that we only need to set the title of the application once,
        ' we use the AssemblyInfo class (defined in the AssemblyInfo.vb file)
        ' to read the AssemblyTitle attribute.
        '	 Dim ainfo As New AssemblyInfo

        Me.Text = "Zamba Software"
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '    Friend WithEvents odlgDocument As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ZPrinter))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(120, 129)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(160, 121)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'ZPrinter
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(434, 365)
        Me.Controls.Add(Me.PictureBox1)
        Me.DockPadding.All = 2
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZPrinter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Title Comes from Assembly Info"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Print WIA"
    'Private Sub printWia(ByRef Result As iprintable)
    '    If Zamba.Tools.EnvironmentUtil.getWindowsVersion = Tools.EnvironmentUtil.Windows.WindowsXp Then
    '        Dim Prin As New WIA.CommonDialogClass
    '        Try
    '            RaiseEvent ShowMsg("Imprimiendo " & Result.Name)
    '            Prin.ShowPhotoPrintingWizard(CType(Result.PrintName, String))
    '            RaiseEvent Printed(0)
    '        Catch ex As System.Runtime.InteropServices.COMException
    '            Try
    '                'Regitra WIA
    '                Try
    '                    Dim str As String = "regsvr32 /s " & Chr(34) & Membership.MembershipHelper.StartUpPath & "\" & "Wiaaut.dll" & Chr(34)
    '                    Shell(str, AppWinStyle.Hide, False)
    '                Catch ex1 As Threading.SynchronizationLockException
    '                Catch ex1 As Threading.ThreadAbortException
    '                Catch ex1 As Threading.ThreadInterruptedException
    '                Catch ex1 As Threading.ThreadStateException
    '                Catch ex1 As Exception
    '                    Throw New System.Exception(ex.Message)
    '                End Try
    '                'Vuelve a ejecutar el metodo printWia
    '                RaiseEvent ShowMsg("Imprimiendo " & Result.Name)
    '                Prin.ShowPhotoPrintingWizard(CType(Result.PrintName, String))
    '                RaiseEvent Printed(0)
    '            Catch exc As Exception
    '                Throw New System.Exception(ex.Message)
    '            End Try
    '        Catch ex As System.Runtime.InteropServices.ExternalException
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Catch ex As System.Runtime.InteropServices.InvalidComObjectException
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Catch ex As System.Runtime.InteropServices.InvalidOleVariantTypeException
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Catch ex As System.Runtime.InteropServices.MarshalDirectiveException
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Catch ex As System.Runtime.InteropServices.SafeArrayRankMismatchException
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Catch ex As System.Runtime.InteropServices.SafeArrayTypeMismatchException
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Catch ex As Exception
    '                                      Zamba.AppBlock.ZException.Log(ex,True)
    '        Finally
    '            Prin = Nothing
    '        End Try
    '    End If
    'End Sub
    Public Sub printWia(ByVal Results() As iprintable)
        If Zamba.Tools.EnvironmentUtil.getWindowsVersion = Tools.EnvironmentUtil.Windows.WindowsXp Then
            Dim Prin As New CommonDialogClass
            Try

                Dim v As New Vector
                For i As Int16 = 0 To Results.Length - 1
                    Results(i).PrintName = Results(i).FullPath
                    v.Add(CType(Results(i).PrintName, String))
                    RaiseEvent ShowMsg("Imprimiendo " & Results(i).Name)
                Next
                Prin.ShowPhotoPrintingWizard(DirectCast(v, Object))
                RaiseEvent Printed(0)
            Catch ex As System.Runtime.InteropServices.COMException
                Try
                    'Registra Wia
                    Try
                        Dim str As String = "regsvr32 /s " & Chr(34) & Membership.MembershipHelper.StartUpPath & "\" & "Wiaaut.dll" & Chr(34)
                        Shell(str, AppWinStyle.Hide, False)
                    Catch ex1 As Threading.SynchronizationLockException
                    Catch ex1 As Threading.ThreadAbortException
                    Catch ex1 As Threading.ThreadInterruptedException
                    Catch ex1 As Threading.ThreadStateException
                    Catch ex1 As Exception
                        Throw New System.Exception(ex.Message)
                    End Try

                    'Vuelave a ejecutar el metodo printWia
                    Dim v As New Vector
                    For i As Int16 = 0 To Results.Length - 1
                        v.Add(CType(Results(i).PrintName, String))
                        RaiseEvent ShowMsg("Imprimiendo " & Results(i).Name)
                    Next
                    Prin.ShowPhotoPrintingWizard(DirectCast(v, Object))
                    RaiseEvent Printed(0)
                Catch exc As Exception
                    Zamba.AppBlock.ZException.Log(ex, True)
                End Try
            Catch ex As System.Runtime.InteropServices.ExternalException
                Zamba.AppBlock.ZException.Log(ex, True)
            Catch ex As System.Runtime.InteropServices.InvalidComObjectException
                Zamba.AppBlock.ZException.Log(ex, True)
            Catch ex As System.Runtime.InteropServices.InvalidOleVariantTypeException
                Zamba.AppBlock.ZException.Log(ex, True)
            Catch ex As System.Runtime.InteropServices.MarshalDirectiveException
                Zamba.AppBlock.ZException.Log(ex, True)
            Catch ex As System.Runtime.InteropServices.SafeArrayRankMismatchException
                Zamba.AppBlock.ZException.Log(ex, True)
            Catch ex As System.Runtime.InteropServices.SafeArrayTypeMismatchException
                Zamba.AppBlock.ZException.Log(ex, True)
            Catch ex As Exception
                Zamba.AppBlock.ZException.Log(ex, True)
            Finally
                Prin = Nothing
            End Try
        End If
    End Sub
#End Region

#Region "PrintDocument"
    Public Sub PrintDocument(ByRef Result As iprintable, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
        Try
            '  flagresult = True
            Dim dialog As New PrintDialog
            dialog.PrinterSettings = PrintSettings
            pdoc.PrinterSettings = PrintSettings
            pdoc.DefaultPageSettings = PageSettings
            pdoc.OriginAtMargins = True
            dialog.Document = pdoc
            Result2Print = Result
            RaiseEvent ShowMsg("Imprimiendo " & Result.Name)
            pdoc.OriginAtMargins = True
            oFDimension = New System.Drawing.Imaging.FrameDimension(Result.PrintPicture.Image.FrameDimensionsList(actualFrame))
            'TODO: El GetFrameCount no es lo mismo que ver el count del framelist?
            iCount = Result.PrintPicture.Image.GetFrameCount(oFDimension) - 1
            actualFrame = 0
            ShowPrintDialog(dialog)
            If ok_cancel Then
                imprimirDoc(Result2Print.Name, dialog.PrinterSettings, dialog.Document.DefaultPageSettings)
            End If
            RaiseEvent Printed(VarToPage - (VarFromPage + 1))
            pdoc.Dispose()
            pdoc = Nothing
            Me.Close()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub
    Public Sub PrintDocument(ByVal Results() As iprintable, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
        Try
            Dim dialog As New PrintDialog
            dialog.PrinterSettings = PrintSettings
            pdoc.PrinterSettings = PrintSettings
            pdoc.DefaultPageSettings = PageSettings
            pdoc.OriginAtMargins = True
            dialog.Document = pdoc
            Dim First As Boolean = True
            For Each R As IPrintable In Results
                Result2Print = R
                R.PrintName = R.FullPath
                RaiseEvent ShowMsg("Imprimiendo " & Result2Print.Name)
                If Result2Print.IsImage Then
                    oFDimension = New System.Drawing.Imaging.FrameDimension(R.PrintPicture.Image.FrameDimensionsList(actualFrame))
                    iCount = R.PrintPicture.Image.GetFrameCount(oFDimension) - 1
                    Desde = 1
                    Hasta = iCount + 1
                    actualFrame = 0
                    If First Then
                        First = False
                        ShowPrintDialog(dialog)
                        If ok_cancel Then
                            imprimirDoc(Result2Print.Name, dialog.PrinterSettings, dialog.Document.DefaultPageSettings)
                        Else
                            Exit For
                        End If
                    Else
                        If ok_cancel Then
                            imprimirDoc(Result2Print.Name, dialog.PrinterSettings, dialog.Document.DefaultPageSettings)
                        End If
                        oFDimension = Nothing
                        RaiseEvent Printed(VarToPage - (VarFromPage + 1))
                    End If
                End If
            Next
            pdoc.Dispose()
            pdoc = Nothing
            Me.Close()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub
#End Region

#Region "PrintIndex"
    Public Sub printindexs(ByRef Result As iprintable, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
        Try
            Dim dialog As New PrintDialog
            dialog.Document = pinx
            dialog.PrinterSettings = PrintSettings
            Result2Print = Result
            RaiseEvent ShowMsg("Imprimiendo indices " & Result2Print.Name)
            iCount = 0
            actualFrame = 0
            ShowPrintDialog(dialog)
            If ok_cancel Then
                imprimirInx(Result2Print.Name, dialog.PrinterSettings)
                RaiseEvent Printed(1)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     retorna un result con los indices a imprimir de documento
    ''' </summary>
    ''' <param name="result">result del documento</param>
    ''' <returns>result con los indices a imprimir</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	05/07/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Private Shared Function FilterDataIndex(ByRef result As iprintable) As iprintable
    '    Dim i As Int16
    '    Dim R As New IPrintable
    '    R.Name = " "
    '    If Not IsNothing(result) Then
    '        If Not IsNothing(result.Name) Then
    '            R.Name = result.Name
    '        End If
    '        R.DocType = result.Parent
    '        If Not IsNothing(result.Indexs) Then
    '            For i = 0 To result.Indexs.Count - 1
    '                If IsNothing(result.Indexs(i).data) Then
    '                    result.Indexs(i).data = " "
    '                End If
    '                If result.Indexs(i).Data.ToString.Trim <> "" Then
    '                    R.Indexs.Add(result.Indexs(i))
    '                End If
    '            Next
    '        End If
    '    End If
    '    Return R
    'End Function
    Public Sub printindexs(ByVal Results() As iprintable, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
        Try
            Dim dialog As New PrintDialog
            dialog.Document = pinx
            dialog.PrinterSettings = PrintSettings
            For i As Int16 = 0 To Results.Length - 1
                Result2Print = Results(i)
                RaiseEvent ShowMsg("Imprimiendo indices " & Result2Print.Name)
                iCount = 0
                actualFrame = 0
                If i = 0 Then
                    ShowPrintDialog(dialog)
                    If ok_cancel Then
                        imprimirInx(Result2Print.Name, dialog.PrinterSettings)
                    Else
                        Exit For
                    End If
                Else
                    If ok_cancel Then
                        If i <> Results.Length - 1 Then
                            imprimirInx(Result2Print.Name, dialog.PrinterSettings)
                        Else
                            imprimirInx(Result2Print.Name, dialog.PrinterSettings)
                        End If
                    End If
                End If
                RaiseEvent Printed(1)
            Next
            pinx.Dispose()
            pinx = Nothing
            Me.Close()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub
#End Region

    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Imprime indices directamente sin cuadro de dialogo de impresion
    '''' </summary>
    '''' <param name="Result">Resultado</param>
    '''' <param name="PrintSettings"></param>
    '''' <param name="PageSettings"></param>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Gonzalo]	31/05/2005	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Private Sub printindWithoutdlg(ByRef Result As Result, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
    '    Try
    '        pinx.PrinterSettings = PrintSettings
    '        pinx.DocumentName = Result.Name
    '        pinx.OriginAtMargins = True
    '        pinx.Print()
    '        pinx.Dispose()
    '        pinx = Nothing
    '        Me.Close()
    '    Catch ex As Exception
    '                                  Zamba.AppBlock.ZException.Log(ex,True)
    '    End Try
    'End Sub
    'Private Sub printDocumentsIndexs(ByRef Result As Result, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
    '    Try
    '        flagResultsIndexs = True
    '        flagIndexs = False
    '        flagresult = False
    '        Dim dialog As New PrintDialog
    '        dialog.Document = pdoc
    '        dialog.Document.PrinterSettings = PrintSettings
    '        dialog.Document.DefaultPageSettings = PageSettings
    '        dialog.Document.OriginAtMargins = True
    '        Result2Print = Result
    '        oFDimension = New System.Drawing.Imaging.FrameDimension(Result.PrintPicture.Image.FrameDimensionsList(actualFrame))
    '        iCount = Result.PrintPicture.Image.GetFrameCount(oFDimension) - 1
    '        actualFrame = 0
    '        Me.Result2Print.PrintPicture.Image.SelectActiveFrame(oFDimension, actualFrame)
    '        ShowPrintDialog(dialog)
    '        RaiseEvent Printed(VarToPage - (VarFromPage + 1))
    '    Catch ex As Exception
    '                                  Zamba.AppBlock.ZException.Log(ex,True)
    '    End Try
    'End Sub
    'Private Sub printResultIndexs(ByRef Result As Result, ByVal PrintSettings As PrinterSettings, ByVal PageSettings As PageSettings)
    '    Try
    '        flagResultsIndexs = True
    '        flagIndexs = False
    '        flagresult = False
    '        Dim dialog As New PrintDialog
    '        dialog.Document = pdoc
    '        dialog.PrinterSettings = PrintSettings
    '        dialog.Document.PrinterSettings = PrintSettings
    '        dialog.Document.DefaultPageSettings = PageSettings
    '        dialog.Document.OriginAtMargins = True
    '        Result2Print = Result
    '        oFDimension = New System.Drawing.Imaging.FrameDimension(Result.PrintPicture.Image.FrameDimensionsList(actualFrame))
    '        iCount = Result.PrintPicture.Image.GetFrameCount(oFDimension) - 1
    '        actualFrame = 0
    '        ShowPrintDialog(dialog)
    '    Catch ex As Exception
    '                                  Zamba.AppBlock.ZException.Log(ex,True)
    '    End Try
    'End Sub
    'Private Sub btnPageSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim psd As New PageSetupDialog
    '    With psd
    '        .Document = pdoc
    '        .PageSettings = pdoc.DefaultPageSettings
    '    End With
    '    If psd.ShowDialog = DialogResult.OK Then
    '        pdoc.DefaultPageSettings = psd.PageSettings
    '    End If
    'End Sub

#Region "PrintDialog"
    Private Sub ShowPrintDialog(ByVal dialog As PrintDialog)
        dialog.AllowPrintToFile = False
        dialog.AllowSelection = True
        If iCount > 0 Then
            dialog.AllowSomePages = True
        Else
            dialog.AllowSomePages = False
        End If
        dialog.PrinterSettings.Copies = 1
        dialog.PrinterSettings.MaximumPage = iCount + 1
        dialog.PrinterSettings.MinimumPage = 1
        dialog.PrinterSettings.FromPage = 1
        dialog.PrinterSettings.ToPage = iCount + 1
        VarFromPage = dialog.PrinterSettings.FromPage
        VarToPage = dialog.PrinterSettings.ToPage
        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            ok_cancel = True
            Desde = dialog.PrinterSettings.FromPage()
            Hasta = dialog.PrinterSettings.ToPage
            If Hasta > iCount + 1 Or Desde < 1 Or Desde > Hasta Then
                MessageBox.Show("El Rango de impresion es incorrecto", "Zamba Software", MessageBoxButtons.OK)
            End If
        End If
    End Sub

#End Region

#Region "ImprimirDoc"
    'Private Sub imprimirDoc(ByVal docName As String, ByVal PrinterSettings As PrinterSettings)
    '    pdoc.PrinterSettings = PrinterSettings
    '    'pdoc.DefaultPageSettings = dialog.Document.DefaultPageSettings
    '    pdoc.DocumentName = docName
    '    pdoc.OriginAtMargins = True
    '    pdoc.OriginAtMargins = True
    '    pdoc.Print()
    'End Sub
    Private Sub imprimirDoc(ByVal docName As String, ByVal PrinterSettings As PrinterSettings, ByVal PageSettings As PageSettings)
        pdoc.PrinterSettings = PrinterSettings
        pdoc.DefaultPageSettings = PageSettings
        pdoc.PrinterSettings = PrinterSettings
        pdoc.DocumentName = docName
        pdoc.Print()
    End Sub
#End Region

#Region "ImprimirInx"
    Private Sub imprimirInx(ByVal docName As String, ByVal PrinterSettings As PrinterSettings)
        pinx.PrinterSettings = PrinterSettings
        pinx.DocumentName = docName
        pinx.OriginAtMargins = True
        pinx.Print()
    End Sub
#End Region

    Public Shared Event Printed(ByVal pages As Int64)

#Region "Print Pages"
    Private Sub pdoc_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdoc.PrintPage
        Dim printed As Int32
        If (Desde <= Hasta) Then
            Try
                Dim image1 As Image
                '     If flagresult Then
                image1 = Me.Result2Print.PrintPicture.Image
                '     Else
                '         image1 = Me.Document2Print.OriginalImg
                '     End If
                image1.SelectActiveFrame(oFDimension, Desde - 1)
                PictureBox1.Width = image1.Width
                PictureBox1.Height = image1.Height
                PictureBox1.Image = image1

                'Dim R As Rectangle

                Dim Doc As PrintDocument
                Doc = sender

                If Doc.PrinterSettings.DefaultPageSettings.Landscape = False Then
                    If PictureBox1.Width > pdoc.DefaultPageSettings.PaperSize.Width Then
                        PictureBox1.Width = pdoc.DefaultPageSettings.PaperSize.Width - 70
                    End If
                    If PictureBox1.Height > pdoc.DefaultPageSettings.PaperSize.Height Then
                        PictureBox1.Height = pdoc.DefaultPageSettings.PaperSize.Height - 70
                    End If
                Else
                    If PictureBox1.Height > pdoc.DefaultPageSettings.PaperSize.Width Then
                        PictureBox1.Height = pdoc.DefaultPageSettings.PaperSize.Width - 70
                    End If
                    If PictureBox1.Width > pdoc.DefaultPageSettings.PaperSize.Height Then
                        PictureBox1.Width = pdoc.DefaultPageSettings.PaperSize.Height - 70
                    End If
                End If

                'If PictureBox1.Width > pdoc.DefaultPageSettings.PaperSize.Width Then
                '    PictureBox1.Width = pdoc.DefaultPageSettings.PaperSize.Width - 70
                'End If
                'If PictureBox1.Height > pdoc.DefaultPageSettings.PaperSize.Height Then
                '    PictureBox1.Height = pdoc.DefaultPageSettings.PaperSize.Height - 70
                'End If
                Dim R As New Rectangle(35, 35, PictureBox1.Width, PictureBox1.Height)
                e.Graphics.DrawImage(PictureBox1.Image, R)

                'TODO: Creo no se necesita hacer el thumbs mando la imagen directamente
                '                Dim image2 As Image = image1.GetThumbnailImage(image1.Width, image1.Height, thabort, IntPtr.Zero)
                'TODO: Ver la forma mas performante de dibujar la imagen
                'e.Graphics.DrawImageUnscaled(image1, 0, 0)
                printed += 1
                Desde += 1
                If Desde <= Hasta Then
                    e.HasMorePages = True
                Else
                    e.HasMorePages = False
                End If
                R = Nothing
            Catch ex As Exception
                RaiseEvent Printed(printed)
            End Try
        End If
    End Sub
    Private Sub pinx_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pinx.PrintPage
        'Dim i As Integer
        Dim y As Integer = 130
        Dim first As Boolean = True
        e.Graphics.DrawString("Entidad: " & Result2Print.Parent.Name, New Font(FontFamily.GenericSansSerif, 12, FontStyle.Underline), Brushes.Black, 40, 50)
        Dim Name As String
        If Result2Print.Name = String.Empty Then Name = " " Else Name = Result2Print.Name
        e.Graphics.DrawString("Nombre: " & Name, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 50, 80)
        For Each Index As IIndex In Result2Print.Indexs
            If Not first Then e.Graphics.DrawLine(New System.Drawing.Pen(Color.Black), 40, y - 10, 600, y - 10)
            e.Graphics.DrawString(Index.Name.Trim & ":", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 60, y)
            Dim Data As String
            If IsNothing(Index.dataDescription) OrElse Index.dataDescription = String.Empty Then
                If IsNothing(Index.Data) OrElse Index.Data = String.Empty Then
                    Data = " "
                Else
                    Data = Index.Data
                End If
            Else
                Data = Index.dataDescription
            End If
            e.Graphics.DrawString(Data, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Italic), Brushes.Black, 270, y)
            y += 35
            first = False
        Next
        e.Graphics.DrawLine(New System.Drawing.Pen(Color.Black), 40, 120, 40, y - 10)
        e.Graphics.DrawLine(New System.Drawing.Pen(Color.Black), 250, 120, 250, y - 10)
        e.Graphics.DrawLine(New System.Drawing.Pen(Color.Black), 600, 120, 600, y - 10)
        e.Graphics.DrawLine(New System.Drawing.Pen(Color.Black), 40, 120, 600, 120)
        e.Graphics.DrawLine(New System.Drawing.Pen(Color.Black), 40, y - 10, 600, y - 10)

    End Sub
#End Region

    Private Sub Printer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
    End Sub

End Class
