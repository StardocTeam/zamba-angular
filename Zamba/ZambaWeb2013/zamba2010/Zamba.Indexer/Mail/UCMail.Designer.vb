Imports Zamba.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCMail
    Inherits System.Windows.Forms.UserControl

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
        Me.pnlPanel = New System.Windows.Forms.Panel
        Me.chklstOptions = New System.Windows.Forms.CheckedListBox
        Me.lblModoDeNotificacion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'pnlPanel
        '
        Me.pnlPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlPanel.Location = New System.Drawing.Point(0, 38)
        Me.pnlPanel.Name = "pnlPanel"
        Me.pnlPanel.Size = New System.Drawing.Size(491, 304)
        Me.pnlPanel.TabIndex = 4
        '
        ' Se agregan tres elementos al pnlPanel
        '
        Dim ucMail As UCSelectMail = New UCSelectMail(Me.ruleId, NotifyTypes.Mail, True)
        Dim ucInternalMessage As UCSelectMail = New UCSelectMail(Me.ruleId, NotifyTypes.InternalMessage, False)
        Dim ucAutomaticMessage As UCSelectAutoMail = New UCSelectAutoMail()

        Me.pnlPanel.Controls.Add(ucMail)
        Me.pnlPanel.Controls.Add(ucInternalMessage)
        Me.pnlPanel.Controls.Add(ucAutomaticMessage)

        Me.pnlPanel.Controls(0).Visible = False
        Me.pnlPanel.Controls(1).Visible = False
        Me.pnlPanel.Controls(2).Visible = False
        '
        'chklstOptions
        '
        Me.chklstOptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.chklstOptions.CheckOnClick = True
        Me.chklstOptions.ColumnWidth = 105
        Me.chklstOptions.FormattingEnabled = True
        Me.chklstOptions.IntegralHeight = False
        Me.chklstOptions.Items.AddRange(New Object() {"Mensaje interno", "Mail automático", "Mail"})
        Me.chklstOptions.Location = New System.Drawing.Point(126, 13)
        Me.chklstOptions.MultiColumn = True
        Me.chklstOptions.Name = "chklstOptions"
        Me.chklstOptions.ScrollAlwaysVisible = True
        Me.chklstOptions.Size = New System.Drawing.Size(354, 19)
        Me.chklstOptions.TabIndex = 5
        Me.chklstOptions.ThreeDCheckBoxes = True
        '
        'lblModoDeNotificacion
        '
        Me.lblModoDeNotificacion.AutoSize = True
        Me.lblModoDeNotificacion.Location = New System.Drawing.Point(12, 16)
        Me.lblModoDeNotificacion.Name = "lblModoDeNotificacion"
        Me.lblModoDeNotificacion.Size = New System.Drawing.Size(108, 13)
        Me.lblModoDeNotificacion.TabIndex = 6
        Me.lblModoDeNotificacion.Text = "Modo de Notificación"
        '
        'UCMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Controls.Add(Me.lblModoDeNotificacion)
        Me.Controls.Add(Me.chklstOptions)
        Me.Controls.Add(Me.pnlPanel)
        Me.Name = "UCMail"
        Me.Size = New System.Drawing.Size(491, 342)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlPanel As System.Windows.Forms.Panel
    Friend WithEvents chklstOptions As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblModoDeNotificacion As System.Windows.Forms.Label

End Class
