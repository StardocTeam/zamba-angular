<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDODocToPDF
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.opOtroDoc = New System.Windows.Forms.RadioButton()
        Me.optDocEnTarea = New System.Windows.Forms.RadioButton()
        Me.btnAceptar = New ZButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New ZLabel()
        Me.txtFileName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label2 = New ZLabel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.opNuevaConversion = New System.Windows.Forms.RadioButton()
        Me.opConversionAnterior = New System.Windows.Forms.RadioButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.GroupBox3)
        Me.tbRule.Controls.Add(Me.GroupBox2)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Controls.Add(Me.GroupBox1)
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtPath)
        Me.GroupBox1.Controls.Add(Me.opOtroDoc)
        Me.GroupBox1.Controls.Add(Me.optDocEnTarea)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(580, 100)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Archivo a convertir"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(278, 59)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(284, 21)
        Me.txtPath.TabIndex = 19
        Me.txtPath.Text = ""
        '
        'opOtroDoc
        '
        Me.opOtroDoc.AutoSize = True
        Me.opOtroDoc.Location = New System.Drawing.Point(34, 60)
        Me.opOtroDoc.Name = "opOtroDoc"
        Me.opOtroDoc.Size = New System.Drawing.Size(226, 17)
        Me.opOtroDoc.TabIndex = 1
        Me.opOtroDoc.Text = "Convertir a PDF el documento ubicado en:"
        Me.opOtroDoc.UseVisualStyleBackColor = True
        '
        'optDocEnTarea
        '
        Me.optDocEnTarea.AutoSize = True
        Me.optDocEnTarea.Checked = True
        Me.optDocEnTarea.Location = New System.Drawing.Point(34, 37)
        Me.optDocEnTarea.Name = "optDocEnTarea"
        Me.optDocEnTarea.Size = New System.Drawing.Size(220, 17)
        Me.optDocEnTarea.TabIndex = 0
        Me.optDocEnTarea.TabStop = True
        Me.optDocEnTarea.Text = "Convertir a PDF el documento de la tarea"
        Me.optDocEnTarea.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.Location = New System.Drawing.Point(501, 295)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(98, 23)
        Me.btnAceptar.TabIndex = 13
        Me.btnAceptar.Text = "Aceptar"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtFileName)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 125)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(580, 58)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Nombre del archivo generado"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(283, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Nombre de la variable donde se guardara el PDF temporal"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(306, 24)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(256, 21)
        Me.txtFileName.TabIndex = 19
        Me.txtFileName.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(161, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Poner como nombre del archivo:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.opNuevaConversion)
        Me.GroupBox3.Controls.Add(Me.opConversionAnterior)
        Me.GroupBox3.Location = New System.Drawing.Point(19, 189)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(580, 100)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Archivo a convertir"
        '
        'opNuevaConversion
        '
        Me.opNuevaConversion.AutoSize = True
        Me.opNuevaConversion.Location = New System.Drawing.Point(34, 60)
        Me.opNuevaConversion.Name = "opNuevaConversion"
        Me.opNuevaConversion.Size = New System.Drawing.Size(141, 17)
        Me.opNuevaConversion.TabIndex = 1
        Me.opNuevaConversion.Text = "Utilizar conversor interno"
        Me.opNuevaConversion.UseVisualStyleBackColor = True
        '
        'opConversionAnterior
        '
        Me.opConversionAnterior.AutoSize = True
        Me.opConversionAnterior.Checked = True
        Me.opConversionAnterior.Location = New System.Drawing.Point(34, 37)
        Me.opConversionAnterior.Name = "opConversionAnterior"
        Me.opConversionAnterior.Size = New System.Drawing.Size(135, 17)
        Me.opConversionAnterior.TabIndex = 0
        Me.opConversionAnterior.TabStop = True
        Me.opConversionAnterior.Text = "Utilizar impresora virtual"
        Me.opConversionAnterior.UseVisualStyleBackColor = True
        '
        'UCDODocToPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDODocToPDF"
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents opOtroDoc As System.Windows.Forms.RadioButton
    Friend WithEvents optDocEnTarea As System.Windows.Forms.RadioButton
    Friend WithEvents txtPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFileName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents opNuevaConversion As System.Windows.Forms.RadioButton
    Friend WithEvents opConversionAnterior As System.Windows.Forms.RadioButton

End Class
