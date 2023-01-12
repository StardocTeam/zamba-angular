<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.btnRun = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblParent = New System.Windows.Forms.Label
        Me.btnKill = New System.Windows.Forms.Button
        Me.btnCloseWin = New System.Windows.Forms.Button
        Me.btnDestroyWin = New System.Windows.Forms.Button
        Me.btnSMCloseWin = New System.Windows.Forms.Button
        Me.lblCloseProc = New System.Windows.Forms.Label
        Me.lblApiCW = New System.Windows.Forms.Label
        Me.lblApiDesWin = New System.Windows.Forms.Label
        Me.lblApiSMCW = New System.Windows.Forms.Label
        Me.lblApiSMCP = New System.Windows.Forms.Label
        Me.btnSMSysCWin = New System.Windows.Forms.Button
        Me.TmrProc = New System.Windows.Forms.Timer(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblPanelParent = New System.Windows.Forms.Label
        Me.btnGacTest = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblReturnGacTest = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtTitulo = New System.Windows.Forms.TextBox
        Me.txtHwnd = New System.Windows.Forms.TextBox
        Me.txtClassName = New System.Windows.Forms.TextBox
        Me.btnCatch = New System.Windows.Forms.Button
        Me.chkUseProc = New System.Windows.Forms.CheckBox
        Me.lblKill = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(452, 464)
        Me.Panel1.TabIndex = 2
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(481, 12)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(210, 20)
        Me.txtPath.TabIndex = 1
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(481, 38)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 0
        Me.btnRun.Text = "Ejecutar"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(478, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "ClassName:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(478, 130)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Titulo:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(478, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Handle:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(478, 175)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Old Parent:"
        '
        'lblParent
        '
        Me.lblParent.AutoSize = True
        Me.lblParent.Location = New System.Drawing.Point(547, 175)
        Me.lblParent.Name = "lblParent"
        Me.lblParent.Size = New System.Drawing.Size(0, 13)
        Me.lblParent.TabIndex = 10
        '
        'btnKill
        '
        Me.btnKill.Location = New System.Drawing.Point(518, 264)
        Me.btnKill.Name = "btnKill"
        Me.btnKill.Size = New System.Drawing.Size(173, 23)
        Me.btnKill.TabIndex = 11
        Me.btnKill.Text = "CloseProcess"
        Me.btnKill.UseVisualStyleBackColor = True
        '
        'btnCloseWin
        '
        Me.btnCloseWin.Location = New System.Drawing.Point(518, 293)
        Me.btnCloseWin.Name = "btnCloseWin"
        Me.btnCloseWin.Size = New System.Drawing.Size(173, 23)
        Me.btnCloseWin.TabIndex = 12
        Me.btnCloseWin.Text = "Api CloseWindow"
        Me.btnCloseWin.UseVisualStyleBackColor = True
        '
        'btnDestroyWin
        '
        Me.btnDestroyWin.Location = New System.Drawing.Point(518, 322)
        Me.btnDestroyWin.Name = "btnDestroyWin"
        Me.btnDestroyWin.Size = New System.Drawing.Size(173, 23)
        Me.btnDestroyWin.TabIndex = 13
        Me.btnDestroyWin.Text = "Api DestroyWindow"
        Me.btnDestroyWin.UseVisualStyleBackColor = True
        '
        'btnSMCloseWin
        '
        Me.btnSMCloseWin.Location = New System.Drawing.Point(518, 351)
        Me.btnSMCloseWin.Name = "btnSMCloseWin"
        Me.btnSMCloseWin.Size = New System.Drawing.Size(173, 23)
        Me.btnSMCloseWin.TabIndex = 14
        Me.btnSMCloseWin.Text = "Api SendMessage CloseWin"
        Me.btnSMCloseWin.UseVisualStyleBackColor = True
        '
        'lblCloseProc
        '
        Me.lblCloseProc.AutoSize = True
        Me.lblCloseProc.Location = New System.Drawing.Point(470, 269)
        Me.lblCloseProc.Name = "lblCloseProc"
        Me.lblCloseProc.Size = New System.Drawing.Size(0, 13)
        Me.lblCloseProc.TabIndex = 15
        '
        'lblApiCW
        '
        Me.lblApiCW.AutoSize = True
        Me.lblApiCW.Location = New System.Drawing.Point(470, 298)
        Me.lblApiCW.Name = "lblApiCW"
        Me.lblApiCW.Size = New System.Drawing.Size(0, 13)
        Me.lblApiCW.TabIndex = 16
        '
        'lblApiDesWin
        '
        Me.lblApiDesWin.AutoSize = True
        Me.lblApiDesWin.Location = New System.Drawing.Point(470, 327)
        Me.lblApiDesWin.Name = "lblApiDesWin"
        Me.lblApiDesWin.Size = New System.Drawing.Size(0, 13)
        Me.lblApiDesWin.TabIndex = 17
        '
        'lblApiSMCW
        '
        Me.lblApiSMCW.AutoSize = True
        Me.lblApiSMCW.Location = New System.Drawing.Point(470, 356)
        Me.lblApiSMCW.Name = "lblApiSMCW"
        Me.lblApiSMCW.Size = New System.Drawing.Size(0, 13)
        Me.lblApiSMCW.TabIndex = 18
        '
        'lblApiSMCP
        '
        Me.lblApiSMCP.AutoSize = True
        Me.lblApiSMCP.Location = New System.Drawing.Point(470, 385)
        Me.lblApiSMCP.Name = "lblApiSMCP"
        Me.lblApiSMCP.Size = New System.Drawing.Size(0, 13)
        Me.lblApiSMCP.TabIndex = 19
        '
        'btnSMSysCWin
        '
        Me.btnSMSysCWin.Location = New System.Drawing.Point(518, 380)
        Me.btnSMSysCWin.Name = "btnSMSysCWin"
        Me.btnSMSysCWin.Size = New System.Drawing.Size(173, 23)
        Me.btnSMSysCWin.TabIndex = 20
        Me.btnSMSysCWin.Text = "Api SendMessage CloseProc"
        Me.btnSMSysCWin.UseVisualStyleBackColor = True
        '
        'TmrProc
        '
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(478, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Panel Parent:"
        '
        'lblPanelParent
        '
        Me.lblPanelParent.AutoSize = True
        Me.lblPanelParent.Location = New System.Drawing.Point(547, 197)
        Me.lblPanelParent.Name = "lblPanelParent"
        Me.lblPanelParent.Size = New System.Drawing.Size(0, 13)
        Me.lblPanelParent.TabIndex = 22
        '
        'btnGacTest
        '
        Me.btnGacTest.Location = New System.Drawing.Point(15, 19)
        Me.btnGacTest.Name = "btnGacTest"
        Me.btnGacTest.Size = New System.Drawing.Size(179, 35)
        Me.btnGacTest.TabIndex = 23
        Me.btnGacTest.Text = "Test"
        Me.btnGacTest.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblReturnGacTest)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnGacTest)
        Me.GroupBox1.Location = New System.Drawing.Point(481, 407)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 112)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GAC Test"
        '
        'lblReturnGacTest
        '
        Me.lblReturnGacTest.AutoSize = True
        Me.lblReturnGacTest.Location = New System.Drawing.Point(60, 77)
        Me.lblReturnGacTest.Name = "lblReturnGacTest"
        Me.lblReturnGacTest.Size = New System.Drawing.Size(0, 13)
        Me.lblReturnGacTest.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Return:"
        '
        'txtTitulo
        '
        Me.txtTitulo.Location = New System.Drawing.Point(550, 127)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(141, 20)
        Me.txtTitulo.TabIndex = 26
        '
        'txtHwnd
        '
        Me.txtHwnd.Location = New System.Drawing.Point(550, 104)
        Me.txtHwnd.Name = "txtHwnd"
        Me.txtHwnd.Size = New System.Drawing.Size(141, 20)
        Me.txtHwnd.TabIndex = 27
        '
        'txtClassName
        '
        Me.txtClassName.Location = New System.Drawing.Point(550, 149)
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.Size = New System.Drawing.Size(141, 20)
        Me.txtClassName.TabIndex = 28
        '
        'btnCatch
        '
        Me.btnCatch.Location = New System.Drawing.Point(616, 72)
        Me.btnCatch.Name = "btnCatch"
        Me.btnCatch.Size = New System.Drawing.Size(75, 23)
        Me.btnCatch.TabIndex = 29
        Me.btnCatch.Text = "Catch"
        Me.btnCatch.UseVisualStyleBackColor = True
        '
        'chkUseProc
        '
        Me.chkUseProc.AutoSize = True
        Me.chkUseProc.Location = New System.Drawing.Point(562, 38)
        Me.chkUseProc.Name = "chkUseProc"
        Me.chkUseProc.Size = New System.Drawing.Size(132, 17)
        Me.chkUseProc.TabIndex = 30
        Me.chkUseProc.Text = "Usar Proccess Handle"
        Me.chkUseProc.UseVisualStyleBackColor = True
        '
        'lblKill
        '
        Me.lblKill.AutoSize = True
        Me.lblKill.Location = New System.Drawing.Point(470, 240)
        Me.lblKill.Name = "lblKill"
        Me.lblKill.Size = New System.Drawing.Size(0, 13)
        Me.lblKill.TabIndex = 32
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(518, 235)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(173, 23)
        Me.Button1.TabIndex = 31
        Me.Button1.Text = "KillProcess"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 540)
        Me.Controls.Add(Me.lblKill)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkUseProc)
        Me.Controls.Add(Me.txtClassName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCatch)
        Me.Controls.Add(Me.txtHwnd)
        Me.Controls.Add(Me.txtTitulo)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblPanelParent)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSMSysCWin)
        Me.Controls.Add(Me.lblApiSMCP)
        Me.Controls.Add(Me.lblApiSMCW)
        Me.Controls.Add(Me.lblApiDesWin)
        Me.Controls.Add(Me.lblApiCW)
        Me.Controls.Add(Me.lblCloseProc)
        Me.Controls.Add(Me.btnSMCloseWin)
        Me.Controls.Add(Me.btnDestroyWin)
        Me.Controls.Add(Me.btnCloseWin)
        Me.Controls.Add(Me.btnKill)
        Me.Controls.Add(Me.lblParent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.Text = "WinCatch1000"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblParent As System.Windows.Forms.Label
    Friend WithEvents btnKill As System.Windows.Forms.Button
    Friend WithEvents btnCloseWin As System.Windows.Forms.Button
    Friend WithEvents btnDestroyWin As System.Windows.Forms.Button
    Friend WithEvents btnSMCloseWin As System.Windows.Forms.Button
    Friend WithEvents lblCloseProc As System.Windows.Forms.Label
    Friend WithEvents lblApiCW As System.Windows.Forms.Label
    Friend WithEvents lblApiDesWin As System.Windows.Forms.Label
    Friend WithEvents lblApiSMCW As System.Windows.Forms.Label
    Friend WithEvents lblApiSMCP As System.Windows.Forms.Label
    Friend WithEvents btnSMSysCWin As System.Windows.Forms.Button
    Friend WithEvents TmrProc As System.Windows.Forms.Timer
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPanelParent As System.Windows.Forms.Label
    Friend WithEvents btnGacTest As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblReturnGacTest As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTitulo As System.Windows.Forms.TextBox
    Friend WithEvents txtHwnd As System.Windows.Forms.TextBox
    Friend WithEvents txtClassName As System.Windows.Forms.TextBox
    Friend WithEvents btnCatch As System.Windows.Forms.Button
    Friend WithEvents chkUseProc As System.Windows.Forms.CheckBox
    Friend WithEvents lblKill As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
