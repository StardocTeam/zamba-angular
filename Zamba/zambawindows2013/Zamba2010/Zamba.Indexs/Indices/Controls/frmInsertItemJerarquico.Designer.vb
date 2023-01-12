<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInsertItemJerarquico
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
                If cmbChild IsNot Nothing Then
                    cmbChild.Dispose()
                    cmbChild = Nothing
                End If
                If cmbParent IsNot Nothing Then
                    cmbParent.Dispose()
                    cmbParent = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInsertItemJerarquico))
        Me.cmdAceptar = New ZButton()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New ZLabel()
        Me.Label3 = New ZLabel()
        Me.cmbParent = New System.Windows.Forms.ComboBox()
        Me.cmbChild = New System.Windows.Forms.ComboBox()
        Me.btnModify = New ZButton()
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
        Me.cmdAceptar.Image = CType(resources.GetObject("cmdAceptar.Image"), System.Drawing.Image)
        Me.cmdAceptar.Location = New System.Drawing.Point(72, 102)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(71, 68)
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
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbParent, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbChild, 1, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.29578!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.70422!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(319, 71)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 35)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Valor Padre:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(3, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 36)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Valor Hijo:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbParent
        '
        Me.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParent.FormattingEnabled = True
        Me.cmbParent.Location = New System.Drawing.Point(110, 3)
        Me.cmbParent.Name = "cmbParent"
        Me.cmbParent.Size = New System.Drawing.Size(206, 21)
        Me.cmbParent.TabIndex = 1
        '
        'cmbChild
        '
        Me.cmbChild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChild.FormattingEnabled = True
        Me.cmbChild.Location = New System.Drawing.Point(110, 38)
        Me.cmbChild.Name = "cmbChild"
        Me.cmbChild.Size = New System.Drawing.Size(206, 21)
        Me.cmbChild.TabIndex = 2
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
        Me.btnModify.Image = CType(resources.GetObject("btnModify.Image"), System.Drawing.Image)
        Me.btnModify.Location = New System.Drawing.Point(175, 102)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(71, 68)
        Me.btnModify.TabIndex = 4
        Me.btnModify.Text = "&Aceptar"
        Me.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnModify.UseVisualStyleBackColor = False
        Me.btnModify.Visible = False
        '
        'frmInsertItemJerarquico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(343, 182)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInsertItemJerarquico"
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
    Friend WithEvents btnModify As ZButton
    Friend WithEvents cmbParent As System.Windows.Forms.ComboBox
    Friend WithEvents cmbChild As System.Windows.Forms.ComboBox

End Class
