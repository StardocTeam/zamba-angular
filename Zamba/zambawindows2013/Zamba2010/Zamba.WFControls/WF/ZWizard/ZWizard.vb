Public Class ZWizard
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Private Sub New()
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
    Friend WithEvents PanelBottom As ZPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PanelFill As System.Windows.Forms.Panel
    Friend WithEvents lnkCan As ZLinkLabel
    Friend WithEvents lnkFin As ZLinkLabel
    Friend WithEvents lnkAnt As ZLinkLabel
    Friend WithEvents lnkSig As ZLinkLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        PanelBottom = New ZPanel()
        lnkCan = New ZLinkLabel()
        lnkFin = New ZLinkLabel()
        lnkAnt = New ZLinkLabel()
        lnkSig = New ZLinkLabel()
        Panel1 = New System.Windows.Forms.Panel()
        PanelFill = New System.Windows.Forms.Panel()
        PanelBottom.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'PanelBottom
        '
        PanelBottom.BackColor = System.Drawing.Color.DodgerBlue
        PanelBottom.Controls.Add(lnkCan)
        PanelBottom.Controls.Add(lnkFin)
        PanelBottom.Controls.Add(lnkAnt)
        PanelBottom.Controls.Add(lnkSig)
        PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        PanelBottom.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        PanelBottom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        PanelBottom.Location = New System.Drawing.Point(2, 387)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New System.Drawing.Size(550, 64)
        PanelBottom.TabIndex = 0
        '
        'lnkCan
        '
        lnkCan.ActiveLinkColor = System.Drawing.Color.White
        lnkCan.BackColor = System.Drawing.Color.Transparent
        lnkCan.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkCan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lnkCan.LinkBehavior = LinkBehavior.NeverUnderline
        lnkCan.LinkColor = System.Drawing.Color.White
        lnkCan.Location = New System.Drawing.Point(14, 18)
        lnkCan.Name = "lnkCan"
        lnkCan.Size = New System.Drawing.Size(104, 28)
        lnkCan.TabIndex = 3
        lnkCan.TabStop = True
        lnkCan.Text = "Cancelar"
        lnkCan.TextAlign = ContentAlignment.MiddleCenter
        '
        'lnkFin
        '
        lnkFin.ActiveLinkColor = System.Drawing.Color.White
        lnkFin.BackColor = System.Drawing.Color.Transparent
        lnkFin.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkFin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lnkFin.LinkBehavior = LinkBehavior.NeverUnderline
        lnkFin.LinkColor = System.Drawing.Color.White
        lnkFin.Location = New System.Drawing.Point(427, 18)
        lnkFin.Name = "lnkFin"
        lnkFin.Size = New System.Drawing.Size(101, 28)
        lnkFin.TabIndex = 0
        lnkFin.TabStop = True
        lnkFin.Text = "Finalizar"
        lnkFin.TextAlign = ContentAlignment.MiddleCenter
        '
        'lnkAnt
        '
        lnkAnt.ActiveLinkColor = System.Drawing.Color.White
        lnkAnt.BackColor = System.Drawing.Color.Transparent
        lnkAnt.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkAnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lnkAnt.LinkBehavior = LinkBehavior.NeverUnderline
        lnkAnt.LinkColor = System.Drawing.Color.White
        lnkAnt.Location = New System.Drawing.Point(185, 18)
        lnkAnt.Name = "lnkAnt"
        lnkAnt.Size = New System.Drawing.Size(95, 28)
        lnkAnt.TabIndex = 2
        lnkAnt.TabStop = True
        lnkAnt.Text = "Anterior"
        lnkAnt.TextAlign = ContentAlignment.MiddleCenter
        '
        'lnkSig
        '
        lnkSig.ActiveLinkColor = System.Drawing.Color.White
        lnkSig.BackColor = System.Drawing.Color.Transparent
        lnkSig.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkSig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lnkSig.LinkBehavior = LinkBehavior.NeverUnderline
        lnkSig.LinkColor = System.Drawing.Color.White
        lnkSig.Location = New System.Drawing.Point(286, 18)
        lnkSig.Name = "lnkSig"
        lnkSig.Size = New System.Drawing.Size(95, 28)
        lnkSig.TabIndex = 1
        lnkSig.TabStop = True
        lnkSig.Text = "Siguiente"
        lnkSig.TextAlign = ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Panel1.Controls.Add(PanelFill)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(2, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(550, 385)
        Panel1.TabIndex = 1
        '
        'PanelFill
        '
        PanelFill.BackColor = System.Drawing.Color.White
        PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        PanelFill.Location = New System.Drawing.Point(0, 0)
        PanelFill.Name = "PanelFill"
        PanelFill.Size = New System.Drawing.Size(550, 385)
        PanelFill.TabIndex = 0
        '
        'ZWizard
        '
        AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        CancelButton = lnkCan
        ClientSize = New System.Drawing.Size(554, 453)
        Controls.Add(Panel1)
        Controls.Add(PanelBottom)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Location = New System.Drawing.Point(0, 0)
        MaximizeBox = False
        MinimizeBox = False
        Name = "ZWizard"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = ""
        PanelBottom.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region


    Public Event FinalizeWizard()

    Public Sub New(ByVal Name As String, ByVal controls() As UserControl)
        Me.new()
        Text = Name
        WControls = controls
        PageUbound = CByte(WControls.Length - 1)
        PageIndex = 0
    End Sub

    Dim _WControls() As UserControl
    Private Property WControls() As UserControl()
        Get
            Return _WControls
        End Get
        Set(ByVal Value() As UserControl)
            _WControls = Value
            For Each uc As UserControl In _WControls
                uc.Dock = DockStyle.Fill
                PanelFill.Controls.Add(uc)
            Next
        End Set
    End Property
    Dim _PageIndex As Byte
    Private Property PageIndex() As Byte
        Get
            Return _PageIndex
        End Get
        Set(ByVal Value As Byte)
            _PageIndex = Value
            If _PageIndex = 0 Then
                lnkAnt.Enabled = False
            Else
                lnkAnt.Enabled = True
            End If
            If _PageIndex = PageUbound Then
                lnkSig.Enabled = False
                AcceptButton = lnkFin
            Else
                lnkSig.Enabled = True
                AcceptButton = lnkSig
            End If
            For Each uc As UserControl In _WControls
                If uc Is WControls(_PageIndex) Then
                    uc.TabStop = True
                    uc.BringToFront()
                    uc.Select()
                Else
                    uc.TabStop = False
                End If
            Next
        End Set
    End Property
    Dim _PageUbound As Byte
    Private Property PageUbound() As Byte
        Get
            Return _PageUbound
        End Get
        Set(ByVal Value As Byte)
            _PageUbound = Value
        End Set
    End Property

#Region "Before/Next"
    Private Sub lnkAnt_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkAnt.LinkClicked
        PageIndex -= CByte(1)
    End Sub
    Private Sub lnlSig_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkSig.LinkClicked
        PageIndex += CByte(1)
    End Sub
#End Region

#Region "Buttons Finish/Cancel"
    Private Sub lnkFin_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkFin.LinkClicked
        RaiseEvent FinalizeWizard()
        DialogResult = DialogResult.OK
        Close()
    End Sub
    Private Sub lnkCan_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkCan.LinkClicked
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
#End Region

End Class
