''' -----------------------------------------------------------------------------
''' Project	 : Zamba.ServerImportTaskView
''' Class	 : ServerImportTaskView.Form1
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Formulario para la visualizacion del seguimiento de importación de mails
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilter As System.Windows.Forms.RadioButton
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents Grilla As System.Windows.Forms.DataGrid
    Friend WithEvents btnEstado As System.Windows.Forms.Button
    Friend WithEvents rbinsertado As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbinsertado = New System.Windows.Forms.RadioButton
        Me.btnEstado = New System.Windows.Forms.Button
        Me.btnsearch = New System.Windows.Forms.Button
        Me.txtname = New System.Windows.Forms.TextBox
        Me.rbFilter = New System.Windows.Forms.RadioButton
        Me.rbAll = New System.Windows.Forms.RadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Grilla = New System.Windows.Forms.DataGrid
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.Panel1.Controls.Add(Me.rbinsertado)
        Me.Panel1.Controls.Add(Me.btnEstado)
        Me.Panel1.Controls.Add(Me.btnsearch)
        Me.Panel1.Controls.Add(Me.txtname)
        Me.Panel1.Controls.Add(Me.rbFilter)
        Me.Panel1.Controls.Add(Me.rbAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(664, 100)
        Me.Panel1.TabIndex = 0
        '
        'rbinsertado
        '
        Me.rbinsertado.Location = New System.Drawing.Point(112, 72)
        Me.rbinsertado.Name = "rbinsertado"
        Me.rbinsertado.Size = New System.Drawing.Size(104, 16)
        Me.rbinsertado.TabIndex = 5
        Me.rbinsertado.Text = "Insertados"
        '
        'btnEstado
        '
        Me.btnEstado.Location = New System.Drawing.Point(544, 56)
        Me.btnEstado.Name = "btnEstado"
        Me.btnEstado.Size = New System.Drawing.Size(96, 24)
        Me.btnEstado.TabIndex = 4
        Me.btnEstado.Text = "Cambiar Estado"
        '
        'btnsearch
        '
        Me.btnsearch.BackColor = System.Drawing.Color.Silver
        Me.btnsearch.Location = New System.Drawing.Point(544, 24)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(96, 24)
        Me.btnsearch.TabIndex = 3
        Me.btnsearch.Text = "Buscar"
        '
        'txtname
        '
        Me.txtname.Location = New System.Drawing.Point(224, 40)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(200, 20)
        Me.txtname.TabIndex = 2
        Me.txtname.Text = ""
        Me.txtname.Visible = False
        '
        'rbFilter
        '
        Me.rbFilter.Location = New System.Drawing.Point(112, 40)
        Me.rbFilter.Name = "rbFilter"
        Me.rbFilter.TabIndex = 1
        Me.rbFilter.Text = "Filtrar Nombre"
        '
        'rbAll
        '
        Me.rbAll.Location = New System.Drawing.Point(112, 16)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.TabIndex = 0
        Me.rbAll.Text = "TODO"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Grilla)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 100)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(664, 202)
        Me.Panel2.TabIndex = 1
        '
        'Grilla
        '
        Me.Grilla.AlternatingBackColor = System.Drawing.Color.OldLace
        Me.Grilla.BackColor = System.Drawing.Color.OldLace
        Me.Grilla.BackgroundColor = System.Drawing.Color.Tan
        Me.Grilla.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Grilla.CaptionBackColor = System.Drawing.Color.SaddleBrown
        Me.Grilla.CaptionForeColor = System.Drawing.Color.OldLace
        Me.Grilla.DataMember = ""
        Me.Grilla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grilla.FlatMode = True
        Me.Grilla.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Grilla.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.Grilla.GridLineColor = System.Drawing.Color.Tan
        Me.Grilla.HeaderBackColor = System.Drawing.Color.Wheat
        Me.Grilla.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Grilla.HeaderForeColor = System.Drawing.Color.SaddleBrown
        Me.Grilla.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.Grilla.Location = New System.Drawing.Point(0, 0)
        Me.Grilla.Name = "Grilla"
        Me.Grilla.ParentRowsBackColor = System.Drawing.Color.OldLace
        Me.Grilla.ParentRowsForeColor = System.Drawing.Color.DarkSlateGray
        Me.Grilla.SelectionBackColor = System.Drawing.Color.SlateGray
        Me.Grilla.SelectionForeColor = System.Drawing.Color.White
        Me.Grilla.Size = New System.Drawing.Size(664, 202)
        Me.Grilla.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(664, 302)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Procesos de Mails"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub rbFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilter.CheckedChanged
        Me.TXTVisible()
    End Sub
    Private Sub TXTVisible()
        Me.txtname.Visible = Me.rbFilter.Checked
    End Sub

    Private Sub rbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAll.CheckedChanged
        Me.TXTVisible()

    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Dim obj As New Engine
        If Me.rbAll.Checked Then
            Me.Grilla.DataSource = obj.GetMailsToProcess()
        ElseIf Me.rbFilter.Checked Then
            Me.Grilla.DataSource = obj.GetMailsToProcess(Me.txtname.Text.Trim)
        ElseIf Me.rbinsertado.Checked Then
            Me.Grilla.DataSource = obj.GetInsertados
        End If
        Me.Grilla.Refresh()
        obj.Dispose()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim server As New Servers.Server
        server.MakeConnection()
        server.dispose()
    End Sub


    Private Sub btnEstado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEstado.Click
        Try
            Dim obj As New Engine
            obj.UpdateState(Grilla.Item(Grilla.CurrentRowIndex, 4).ToString)
            MessageBox.Show("Dato Actualizado")
            obj.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rbinsertado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbinsertado.CheckedChanged

    End Sub
End Class
