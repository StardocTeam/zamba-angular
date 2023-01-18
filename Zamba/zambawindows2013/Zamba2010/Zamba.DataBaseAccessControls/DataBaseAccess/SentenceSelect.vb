Public Class SentenceSelect
    Inherits Zamba.AppBlock.ZForm

#Region " Código generado por el Diseñador de Windows Forms "



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
    Friend WithEvents grdSentence As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdSentence = New System.Windows.Forms.DataGrid
        CType(Me.grdSentence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdSentence
        '
        Me.grdSentence.AllowNavigation = False
        Me.grdSentence.AlternatingBackColor = System.Drawing.Color.White
        Me.grdSentence.BackColor = System.Drawing.Color.White
        Me.grdSentence.BackgroundColor = System.Drawing.Color.White
        Me.grdSentence.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdSentence.CaptionBackColor = System.Drawing.Color.White
        Me.grdSentence.CaptionFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSentence.CaptionForeColor = System.Drawing.Color.White
        Me.grdSentence.CaptionVisible = False
        Me.grdSentence.DataMember = ""
        Me.grdSentence.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSentence.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSentence.ForeColor = System.Drawing.Color.Black
        Me.grdSentence.GridLineColor = System.Drawing.Color.White
        Me.grdSentence.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdSentence.HeaderBackColor = System.Drawing.Color.LightGray
        Me.grdSentence.HeaderForeColor = System.Drawing.Color.Black
        Me.grdSentence.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.grdSentence.LinkColor = System.Drawing.Color.White
        Me.grdSentence.Location = New System.Drawing.Point(0, 0)
        Me.grdSentence.Name = "grdSentence"
        Me.grdSentence.ParentRowsBackColor = System.Drawing.Color.White
        Me.grdSentence.ParentRowsForeColor = System.Drawing.Color.White
        Me.grdSentence.ParentRowsVisible = False
        Me.grdSentence.PreferredColumnWidth = 150
        Me.grdSentence.PreferredRowHeight = 14
        Me.grdSentence.ReadOnly = True
        Me.grdSentence.RowHeadersVisible = False
        Me.grdSentence.SelectionBackColor = System.Drawing.Color.White
        Me.grdSentence.SelectionForeColor = System.Drawing.Color.White
        Me.grdSentence.Size = New System.Drawing.Size(488, 421)
        Me.grdSentence.TabIndex = 5
        '
        'SentenceSelect
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(488, 421)
        Me.Controls.Add(Me.grdSentence)
        Me.Name = "SentenceSelect"
        Me.Text = "Resultado de Sentencia"
        CType(Me.grdSentence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal ds As DataSet)
        MyBase.New()
        InitializeComponent()
        Me.grdSentence.DataSource = ds.Tables(0)
    End Sub

End Class
