<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVersionedDetails
    Inherits Zamba.AppBlock.ZForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.treeViewVersionedDetails = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblComment = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblFechaCreacion = New System.Windows.Forms.Label
        Me.lblUsuarioCreador = New System.Windows.Forms.Label
        Me.lblFechaEdicion = New System.Windows.Forms.Label
        Me.txtComentarioVersion = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'treeViewVersionedDetails
        '
        Me.treeViewVersionedDetails.Location = New System.Drawing.Point(2, 1)
        Me.treeViewVersionedDetails.Name = "treeViewVersionedDetails"
        Me.treeViewVersionedDetails.Size = New System.Drawing.Size(536, 297)
        Me.treeViewVersionedDetails.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.SteelBlue
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(560, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha de Creación"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.SteelBlue
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label2.Location = New System.Drawing.Point(560, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Usuario Creador"
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.BackColor = System.Drawing.Color.SteelBlue
        Me.lblComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComment.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblComment.Location = New System.Drawing.Point(560, 178)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(134, 13)
        Me.lblComment.TabIndex = 4
        Me.lblComment.Text = "Comentario de Version"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.SteelBlue
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label3.Location = New System.Drawing.Point(560, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Fecha de Edición"
        '
        'lblFechaCreacion
        '
        Me.lblFechaCreacion.AutoSize = True
        Me.lblFechaCreacion.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblFechaCreacion.Location = New System.Drawing.Point(578, 35)
        Me.lblFechaCreacion.Name = "lblFechaCreacion"
        Me.lblFechaCreacion.Size = New System.Drawing.Size(39, 13)
        Me.lblFechaCreacion.TabIndex = 5
        Me.lblFechaCreacion.Text = "Label1"
        '
        'lblUsuarioCreador
        '
        Me.lblUsuarioCreador.AutoSize = True
        Me.lblUsuarioCreador.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblUsuarioCreador.Location = New System.Drawing.Point(578, 88)
        Me.lblUsuarioCreador.Name = "lblUsuarioCreador"
        Me.lblUsuarioCreador.Size = New System.Drawing.Size(39, 13)
        Me.lblUsuarioCreador.TabIndex = 6
        Me.lblUsuarioCreador.Text = "Label2"
        '
        'lblFechaEdicion
        '
        Me.lblFechaEdicion.AutoSize = True
        Me.lblFechaEdicion.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblFechaEdicion.Location = New System.Drawing.Point(578, 149)
        Me.lblFechaEdicion.Name = "lblFechaEdicion"
        Me.lblFechaEdicion.Size = New System.Drawing.Size(39, 13)
        Me.lblFechaEdicion.TabIndex = 7
        Me.lblFechaEdicion.Text = "Label3"
        '
        'txtComentarioVersion
        '
        Me.txtComentarioVersion.Location = New System.Drawing.Point(544, 206)
        Me.txtComentarioVersion.Multiline = True
        Me.txtComentarioVersion.Name = "txtComentarioVersion"
        Me.txtComentarioVersion.ReadOnly = True
        Me.txtComentarioVersion.Size = New System.Drawing.Size(179, 81)
        Me.txtComentarioVersion.TabIndex = 8
        '
        'frmVersionedDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(731, 299)
        Me.Controls.Add(Me.txtComentarioVersion)
        Me.Controls.Add(Me.lblFechaEdicion)
        Me.Controls.Add(Me.lblUsuarioCreador)
        Me.Controls.Add(Me.lblFechaCreacion)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.treeViewVersionedDetails)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVersionedDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Detalles de Versionado"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents treeViewVersionedDetails As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblFechaCreacion As System.Windows.Forms.Label
    Friend WithEvents lblUsuarioCreador As System.Windows.Forms.Label
    Friend WithEvents lblFechaEdicion As System.Windows.Forms.Label
    Friend WithEvents txtComentarioVersion As System.Windows.Forms.TextBox
End Class
