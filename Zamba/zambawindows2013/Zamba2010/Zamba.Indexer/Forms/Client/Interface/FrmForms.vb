Public Class FrmForms
    Inherits ZForm

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
    Friend WithEvents PanelMain As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents PanelRight As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents PanelOptions As System.Windows.Forms.Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrmForms))
        PanelMain = New System.Windows.Forms.Panel()
        Splitter1 = New System.Windows.Forms.Splitter()
        PanelOptions = New System.Windows.Forms.Panel()
        PanelBottom = New System.Windows.Forms.Panel()
        PanelTop = New System.Windows.Forms.Panel()
        PanelRight = New System.Windows.Forms.Panel()
        PanelLeft = New System.Windows.Forms.Panel()
        SuspendLayout
        '
        'PanelMain
        '
        PanelMain.BackColor = System.Drawing.Color.White
        PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        PanelMain.Location = New System.Drawing.Point(278, 5)
        PanelMain.Name = "PanelMain"
        PanelMain.Size = New System.Drawing.Size(421, 467)
        PanelMain.TabIndex = 46
        '
        'Splitter1
        '
        Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Splitter1.Location = New System.Drawing.Point(274, 5)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New System.Drawing.Size(4, 467)
        Splitter1.TabIndex = 45
        Splitter1.TabStop = false
        '
        'PanelOptions
        '
        PanelOptions.BackColor = System.Drawing.Color.White
        PanelOptions.Dock = System.Windows.Forms.DockStyle.Left
        PanelOptions.Location = New System.Drawing.Point(5, 5)
        PanelOptions.Name = "PanelOptions"
        PanelOptions.Size = New System.Drawing.Size(269, 467)
        PanelOptions.TabIndex = 44
        '
        'PanelBottom
        '
        PanelBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        PanelBottom.Location = New System.Drawing.Point(5, 472)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New System.Drawing.Size(694, 4)
        PanelBottom.TabIndex = 41
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Location = New System.Drawing.Point(5, 2)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(694, 3)
        PanelTop.TabIndex = 43
        '
        'PanelRight
        '
        PanelRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        PanelRight.Location = New System.Drawing.Point(699, 2)
        PanelRight.Name = "PanelRight"
        PanelRight.Size = New System.Drawing.Size(3, 474)
        PanelRight.TabIndex = 42
        '
        'PanelLeft
        '
        PanelLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        PanelLeft.Location = New System.Drawing.Point(2, 2)
        PanelLeft.Name = "PanelLeft"
        PanelLeft.Size = New System.Drawing.Size(3, 474)
        PanelLeft.TabIndex = 40
        '
        'FrmForms
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(704, 478)
        Controls.Add(PanelMain)
        Controls.Add(Splitter1)
        Controls.Add(PanelOptions)
        Controls.Add(PanelBottom)
        Controls.Add(PanelTop)
        Controls.Add(PanelRight)
        Controls.Add(PanelLeft)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "FrmForms"
        Text = "FrmForms"
        ResumeLayout(false)

    End Sub

#End Region

#Region "Ctor"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        LoadMyControls()
    End Sub
    Dim WithEvents ucOptions As ucOptions
    Dim WithEvents ucNewWinForm As ucNewWinForm
    Private Sub LoadMyControls()
        Try
            'ucOptions
            ucOptions = New ucOptions
            ucOptions.Dock = DockStyle.Fill
            PanelOptions.Controls.Add(ucOptions)

            'ucNewWinForm
            ucNewWinForm = New ucNewWinForm
            ucNewWinForm.Dock = DockStyle.Fill
            PanelMain.Controls.Add(ucNewWinForm)

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region



End Class
