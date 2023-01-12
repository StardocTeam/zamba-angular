Imports Zamba.Core
Public Class FrmExportList
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''   Objeto para exportar la lista de sustitución a un archivo TXT
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks>EXPORTAR LISTA DE SUSTITUCION</remarks>
    ''' <history>
    ''' 	[Hernan]	07/06/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal id As Int32)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        IndexID = id
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If txtseparador IsNot Nothing Then
                    txtseparador.Dispose()
                    txtseparador = Nothing
                End If
                If txtfile IsNot Nothing Then
                    txtfile.Dispose()
                    txtfile = Nothing
                End If
                If btnfile IsNot Nothing Then
                    btnfile.Dispose()
                    btnfile = Nothing
                End If
                If lbl IsNot Nothing Then
                    lbl.Dispose()
                    lbl = Nothing
                End If
                If btnexportar IsNot Nothing Then
                    btnexportar.Dispose()
                    btnexportar = Nothing
                End If
                If btncancell IsNot Nothing Then
                    btncancell.Dispose()
                    btncancell = Nothing
                End If
                If log IsNot Nothing Then
                    log.Dispose()
                    log = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents txtseparador As TextBox
    Friend WithEvents txtfile As TextBox
    Friend WithEvents btnfile As ZButton
    Friend WithEvents lbl As ZLabel
    Friend WithEvents btnexportar As ZButton
    Friend WithEvents btncancell As ZButton
    Friend WithEvents log As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmExportList))
        txtseparador = New TextBox
        txtfile = New TextBox
        btnfile = New ZButton
        lbl = New ZLabel
        btnexportar = New ZButton
        btncancell = New ZButton
        log = New ZLabel
        SuspendLayout()
        '
        'ZIconList
        '
        '
        'txtseparador
        '
        txtseparador.BackColor = Color.White
        txtseparador.Location = New Point(144, 17)
        txtseparador.Name = "txtseparador"
        txtseparador.Size = New Size(40, 21)
        txtseparador.TabIndex = 0
        txtseparador.Text = ""
        '
        'txtfile
        '
        txtfile.BackColor = Color.White
        txtfile.Location = New Point(144, 52)
        txtfile.Name = "txtfile"
        txtfile.Size = New Size(208, 21)
        txtfile.TabIndex = 1
        txtfile.Text = ""
        '
        'btnfile
        '
        btnfile.DialogResult = System.Windows.Forms.DialogResult.None
        btnfile.Location = New Point(64, 52)
        btnfile.Name = "btnfile"
        btnfile.Size = New Size(80, 26)
        btnfile.TabIndex = 2
        btnfile.Text = "Archivo"
        '
        'lbl
        '
        lbl.BorderStyle = BorderStyle.FixedSingle
        lbl.Location = New Point(64, 17)
        lbl.Name = "lbl"
        lbl.Size = New Size(80, 25)
        lbl.TabIndex = 3
        lbl.Text = "Separador"
        lbl.TextAlign = ContentAlignment.MiddleCenter
        '
        'btnexportar
        '
        btnexportar.DialogResult = System.Windows.Forms.DialogResult.None
        btnexportar.Location = New Point(120, 112)
        btnexportar.Name = "btnexportar"
        btnexportar.Size = New Size(112, 25)
        btnexportar.TabIndex = 4
        btnexportar.Text = "Exportar"
        '
        'btncancell
        '
        btncancell.DialogResult = System.Windows.Forms.DialogResult.None
        btncancell.Location = New Point(240, 112)
        btncancell.Name = "btncancell"
        btncancell.Size = New Size(112, 25)
        btncancell.TabIndex = 5
        btncancell.Text = "Cancelar"
        '
        'log
        '
        log.Location = New Point(112, 86)
        log.Name = "log"
        log.Size = New Size(224, 17)
        log.TabIndex = 6
        '
        'FrmExportList
        '
        AutoScaleBaseSize = New Size(5, 14)
        ClientSize = New Size(416, 152)
        Controls.Add(log)
        Controls.Add(btncancell)
        Controls.Add(btnexportar)
        Controls.Add(lbl)
        Controls.Add(btnfile)
        Controls.Add(txtfile)
        Controls.Add(txtseparador)
        DockPadding.All = 2
        Name = "FrmExportList"
        Text = "Exportar Lista de sustitución"
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub btncancell_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btncancell.Click
        Close()
    End Sub
    Private IndexID As Int32

    Private Sub btnexportar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnexportar.Click
        Try
            Validar()
            Cursor = Cursors.WaitCursor
            log.Text = "exportando datos..."
            procesar()
            log.Text = "Exportación finalizada"
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub procesar()
        Dim cr As New CreateTablesBusiness
        cr.ExportSustitucionTable(txtfile.Text.Trim, txtseparador.Text, IndexID)
    End Sub
    Private Sub Validar()
        If txtseparador.Text.Trim = String.Empty Then Throw New Exception("Complete el separador")
        If txtfile.Text.Trim = String.Empty Then Throw New Exception("Complete el nombre del archivo")
        'Try
        '    If IO.File.Exists(txtfile.Text.Trim) = False Then
        '        IO.File.Create(txtfile.Text.Trim)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub txtseparador_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtseparador.TextChanged
        log.Text = String.Empty
    End Sub

    Private Sub txtfile_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtfile.TextChanged
        log.Text = String.Empty
    End Sub

    Private Sub btnfile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnfile.Click
        Try
            SelectFile()
        Catch
        End Try
    End Sub
    Private Sub SelectFile()
        log.Text = String.Empty
        Dim file As New SaveFileDialog
        file.ShowDialog()
        txtfile.Text = file.FileName
    End Sub
End Class
