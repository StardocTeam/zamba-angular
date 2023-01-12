<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInsertItemListaSustitucion
    Inherits Zamba.AppBlock.ZForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If cmdAceptar IsNot Nothing Then
                    cmdAceptar.Dispose()
                    cmdAceptar = Nothing
                End If
                If TableLayoutPanel2 IsNot Nothing Then
                    TableLayoutPanel2.Dispose()
                    TableLayoutPanel2 = Nothing
                End If
                If Label1 IsNot Nothing Then
                    Label1.Dispose()
                    Label1 = Nothing
                End If
                If Label3 IsNot Nothing Then
                    Label3.Dispose()
                    Label3 = Nothing
                End If
                If txtDescripcion IsNot Nothing Then
                    txtDescripcion.Dispose()
                    txtDescripcion = Nothing
                End If
                If txtCodigo IsNot Nothing Then
                    txtCodigo.Dispose()
                    txtCodigo = Nothing
                End If
                If btnModify IsNot Nothing Then
                    btnModify.Dispose()
                    btnModify = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInsertItemListaSustitucion))
        Me.cmdAceptar = New Zamba.AppBlock.ZButton()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.btnModify = New Zamba.AppBlock.ZButton()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdAceptar.BackColor = System.Drawing.Color.Transparent
        Me.cmdAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdAceptar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdAceptar.FlatAppearance.BorderSize = 0
        Me.cmdAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAceptar.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAceptar.ForeColor = System.Drawing.Color.Black
        Me.cmdAceptar.Image = CType(resources.GetObject("cmdAceptar.Image"), System.Drawing.Image)
        Me.cmdAceptar.Location = New System.Drawing.Point(96, 126)
        Me.cmdAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(95, 84)
        Me.cmdAceptar.TabIndex = 3
        Me.cmdAceptar.Text = "&Agregar"
        Me.cmdAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAceptar.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.54232!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.45768!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtDescripcion, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtCodigo, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(16, 15)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.29578!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.70422!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(425, 87)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'txtDescripcion
        '
        Me.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDescripcion.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtDescripcion.Location = New System.Drawing.Point(146, 46)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(275, 26)
        Me.txtDescripcion.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.FontSize = 11.25!
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 42)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Código:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 11.25!
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(4, 42)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 45)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Descripción:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCodigo
        '
        Me.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCodigo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCodigo.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtCodigo.Location = New System.Drawing.Point(146, 4)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(275, 26)
        Me.txtCodigo.TabIndex = 1
        '
        'btnModify
        '
        Me.btnModify.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnModify.BackColor = System.Drawing.Color.Transparent
        Me.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnModify.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModify.FlatAppearance.BorderSize = 0
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.ForeColor = System.Drawing.Color.Black
        Me.btnModify.Image = CType(resources.GetObject("btnModify.Image"), System.Drawing.Image)
        Me.btnModify.Location = New System.Drawing.Point(233, 126)
        Me.btnModify.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(95, 84)
        Me.btnModify.TabIndex = 4
        Me.btnModify.Text = "&Aceptar"
        Me.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnModify.UseVisualStyleBackColor = False
        Me.btnModify.Visible = False
        '
        'frmInsertItemListaSustitucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(457, 224)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInsertItemListaSustitucion"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Agregar Item"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdAceptar As ZButton
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents btnModify As ZButton

End Class
