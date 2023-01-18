<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            Trace.Close()
            If listDocNotFound IsNot Nothing Then listDocNotFound.Clear()
            If listFileNotFound IsNot Nothing Then listFileNotFound.Clear()
            Worker.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtExcelPath = New System.Windows.Forms.TextBox
        Me.btnGetExcel = New System.Windows.Forms.Button
        Me.txtFolderPath = New System.Windows.Forms.TextBox
        Me.btnGetFolder = New System.Windows.Forms.Button
        Me.btnOptions = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblFileNotFound = New System.Windows.Forms.Label
        Me.lblBadNumbers = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblOk = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblDocNotFound = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ubicación del excel"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Carpeta destino"
        '
        'txtExcelPath
        '
        Me.txtExcelPath.Enabled = False
        Me.txtExcelPath.Location = New System.Drawing.Point(119, 10)
        Me.txtExcelPath.Name = "txtExcelPath"
        Me.txtExcelPath.Size = New System.Drawing.Size(443, 20)
        Me.txtExcelPath.TabIndex = 2
        '
        'btnGetExcel
        '
        Me.btnGetExcel.Location = New System.Drawing.Point(568, 10)
        Me.btnGetExcel.Name = "btnGetExcel"
        Me.btnGetExcel.Size = New System.Drawing.Size(32, 20)
        Me.btnGetExcel.TabIndex = 0
        Me.btnGetExcel.Text = "..."
        Me.btnGetExcel.UseVisualStyleBackColor = True
        '
        'txtFolderPath
        '
        Me.txtFolderPath.Enabled = False
        Me.txtFolderPath.Location = New System.Drawing.Point(119, 37)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.Size = New System.Drawing.Size(443, 20)
        Me.txtFolderPath.TabIndex = 4
        '
        'btnGetFolder
        '
        Me.btnGetFolder.Location = New System.Drawing.Point(568, 37)
        Me.btnGetFolder.Name = "btnGetFolder"
        Me.btnGetFolder.Size = New System.Drawing.Size(32, 20)
        Me.btnGetFolder.TabIndex = 1
        Me.btnGetFolder.Text = "..."
        Me.btnGetFolder.UseVisualStyleBackColor = True
        '
        'btnOptions
        '
        Me.btnOptions.Location = New System.Drawing.Point(165, 73)
        Me.btnOptions.Name = "btnOptions"
        Me.btnOptions.Size = New System.Drawing.Size(75, 23)
        Me.btnOptions.TabIndex = 2
        Me.btnOptions.Text = "Opciones"
        Me.btnOptions.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(270, 73)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 3
        Me.btnStart.Text = "Comenzar"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(375, 73)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Salir"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lblFileNotFound)
        Me.Panel1.Controls.Add(Me.lblBadNumbers)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.lblOk)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblDocNotFound)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(12, 117)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(588, 44)
        Me.Panel1.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Archivos copiados"
        '
        'lblFileNotFound
        '
        Me.lblFileNotFound.AutoSize = True
        Me.lblFileNotFound.Location = New System.Drawing.Point(473, 24)
        Me.lblFileNotFound.Name = "lblFileNotFound"
        Me.lblFileNotFound.Size = New System.Drawing.Size(13, 13)
        Me.lblFileNotFound.TabIndex = 10
        Me.lblFileNotFound.Text = "0"
        '
        'lblBadNumbers
        '
        Me.lblBadNumbers.AutoSize = True
        Me.lblBadNumbers.Location = New System.Drawing.Point(197, 24)
        Me.lblBadNumbers.Name = "lblBadNumbers"
        Me.lblBadNumbers.Size = New System.Drawing.Size(13, 13)
        Me.lblBadNumbers.TabIndex = 9
        Me.lblBadNumbers.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(269, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(159, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Archivos físicos no encontrados"
        '
        'lblOk
        '
        Me.lblOk.AutoSize = True
        Me.lblOk.Location = New System.Drawing.Point(197, 4)
        Me.lblOk.Name = "lblOk"
        Me.lblOk.Size = New System.Drawing.Size(13, 13)
        Me.lblOk.TabIndex = 6
        Me.lblOk.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Valores erroneos en excel"
        '
        'lblDocNotFound
        '
        Me.lblDocNotFound.AutoSize = True
        Me.lblDocNotFound.Location = New System.Drawing.Point(473, 4)
        Me.lblDocNotFound.Name = "lblDocNotFound"
        Me.lblDocNotFound.Size = New System.Drawing.Size(13, 13)
        Me.lblDocNotFound.TabIndex = 5
        Me.lblDocNotFound.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(269, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(198, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Documentos en  Zamba no encontrados"
        '
        'Worker
        '
        Me.Worker.WorkerReportsProgress = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(614, 175)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnOptions)
        Me.Controls.Add(Me.btnGetFolder)
        Me.Controls.Add(Me.txtFolderPath)
        Me.Controls.Add(Me.btnGetExcel)
        Me.Controls.Add(Me.txtExcelPath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exportación de Pólizas"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtExcelPath As System.Windows.Forms.TextBox
    Friend WithEvents btnGetExcel As System.Windows.Forms.Button
    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents btnGetFolder As System.Windows.Forms.Button
    Friend WithEvents btnOptions As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblOk As System.Windows.Forms.Label
    Friend WithEvents lblDocNotFound As System.Windows.Forms.Label
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblFileNotFound As System.Windows.Forms.Label
    Friend WithEvents lblBadNumbers As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
