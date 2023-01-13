Imports Zamba.Core
Public Class FrmExportList
    Inherits Zamba.AppBlock.ZForm

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
        Me.IndexID = id
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
    Friend WithEvents txtseparador As System.Windows.Forms.TextBox
    Friend WithEvents txtfile As System.Windows.Forms.TextBox
    Friend WithEvents btnfile As Zamba.AppBlock.ZButton
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents btnexportar As Zamba.AppBlock.ZButton
    Friend WithEvents btncancell As Zamba.AppBlock.ZButton1
    Friend WithEvents log As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmExportList))
        Me.txtseparador = New System.Windows.Forms.TextBox
        Me.txtfile = New System.Windows.Forms.TextBox
        Me.btnfile = New Zamba.AppBlock.ZButton
        Me.lbl = New System.Windows.Forms.Label
        Me.btnexportar = New Zamba.AppBlock.ZButton
        Me.btncancell = New Zamba.AppBlock.ZButton1
        Me.log = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ZIconList
        '
        '
        'txtseparador
        '
        Me.txtseparador.BackColor = System.Drawing.Color.White
        Me.txtseparador.Location = New System.Drawing.Point(144, 17)
        Me.txtseparador.Name = "txtseparador"
        Me.txtseparador.Size = New System.Drawing.Size(40, 21)
        Me.txtseparador.TabIndex = 0
        Me.txtseparador.Text = ""
        '
        'txtfile
        '
        Me.txtfile.BackColor = System.Drawing.Color.White
        Me.txtfile.Location = New System.Drawing.Point(144, 52)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.Size = New System.Drawing.Size(208, 21)
        Me.txtfile.TabIndex = 1
        Me.txtfile.Text = ""
        '
        'btnfile
        '
        Me.btnfile.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnfile.Location = New System.Drawing.Point(64, 52)
        Me.btnfile.Name = "btnfile"
        Me.btnfile.Size = New System.Drawing.Size(80, 26)
        Me.btnfile.TabIndex = 2
        Me.btnfile.Text = "Archivo"
        '
        'lbl
        '
        Me.lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl.Location = New System.Drawing.Point(64, 17)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(80, 25)
        Me.lbl.TabIndex = 3
        Me.lbl.Text = "Separador"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnexportar
        '
        Me.btnexportar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnexportar.Location = New System.Drawing.Point(120, 112)
        Me.btnexportar.Name = "btnexportar"
        Me.btnexportar.Size = New System.Drawing.Size(112, 25)
        Me.btnexportar.TabIndex = 4
        Me.btnexportar.Text = "Exportar"
        '
        'btncancell
        '
        Me.btncancell.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btncancell.Location = New System.Drawing.Point(240, 112)
        Me.btncancell.Name = "btncancell"
        Me.btncancell.Size = New System.Drawing.Size(112, 25)
        Me.btncancell.TabIndex = 5
        Me.btncancell.Text = "Cancelar"
        '
        'log
        '
        Me.log.Location = New System.Drawing.Point(112, 86)
        Me.log.Name = "log"
        Me.log.Size = New System.Drawing.Size(224, 17)
        Me.log.TabIndex = 6
        '
        'FrmExportList
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(416, 152)
        Me.Controls.Add(Me.log)
        Me.Controls.Add(Me.btncancell)
        Me.Controls.Add(Me.btnexportar)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.btnfile)
        Me.Controls.Add(Me.txtfile)
        Me.Controls.Add(Me.txtseparador)
        Me.DockPadding.All = 2
        Me.Name = "FrmExportList"
        Me.Text = "Exportar Lista de sustitución"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btncancell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancell.Click
        Me.Close()
    End Sub
    Private IndexID As Int32

    Private Sub btnexportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportar.Click
        Try
            Validar()
            Me.Cursor = Cursors.WaitCursor
            Me.log.Text = "exportando datos..."
            Me.procesar()
            Me.log.Text = "Exportación finalizada"
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub procesar()
        Dim cr As New CreateTablesBusiness
        cr.ExportSustitucionTable(txtfile.Text.Trim, txtseparador.Text, Me.IndexID)
    End Sub
    Private Sub Validar()
        If txtseparador.Text.Trim = "" Then Throw New Exception("Complete el separador")
        If txtfile.Text.Trim = "" Then Throw New Exception("Complete el nombre del archivo")
        'Try
        '    If IO.File.Exists(txtfile.Text.Trim) = False Then
        '        IO.File.Create(txtfile.Text.Trim)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub txtseparador_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtseparador.TextChanged
        Me.log.Text = ""
    End Sub

    Private Sub txtfile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfile.TextChanged
        Me.log.Text = ""
    End Sub

    Private Sub btnfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfile.Click
        Try
            Me.SelectFile()
        Catch
        End Try
    End Sub
    Private Sub SelectFile()
        Me.log.Text = ""
        Dim file As New SaveFileDialog
        file.ShowDialog()
        txtfile.Text = file.FileName
    End Sub
End Class
