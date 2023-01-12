Imports Zamba.Core

Public Class UCRuleContainer
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New System.Windows.Forms.Panel
        PanelTop = New ZLabel
        PanelRule = New System.Windows.Forms.Panel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.Controls.Add(PanelTop)
        Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(372, 27)
        Panel1.TabIndex = 109
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.White
        PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelTop.Dock = System.Windows.Forms.DockStyle.Fill
        PanelTop.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        PanelTop.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(372, 27)
        PanelTop.TabIndex = 105
        PanelTop.Text = " Regla"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'PanelRule
        '
        PanelRule.AutoScroll = True
        PanelRule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelRule.Dock = System.Windows.Forms.DockStyle.Fill
        PanelRule.Location = New System.Drawing.Point(0, 27)
        PanelRule.Name = "PanelRule"
        PanelRule.Size = New System.Drawing.Size(372, 341)
        PanelRule.TabIndex = 111
        '
        'UCRuleContainer
        '
        Controls.Add(PanelRule)
        Controls.Add(Panel1)
        Name = "UCRuleContainer"
        Size = New System.Drawing.Size(372, 368)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region


#Region "Atributos"
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents PanelRule As System.Windows.Forms.Panel
    Dim _rule As IRule
#End Region

#Region "Constructores"
    Public Sub New(ByVal rule As IRule)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        _rule = rule
        'Try
        '    Dim uchelp As New UCHelp(_rule)

        'Catch ex As Exception
        '   ZClass.raiseerror(ex)
        'End Try
    End Sub
#End Region

#Region "Eventos"
    'Private Sub PictureBox1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pctInformation.MouseHover
    '    toolTip.Show(pctInformation)
    'End Sub
#End Region

#Region "Eventos"
    'Private Sub PictureBox1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pctInformation.MouseHover
    '    toolTip.Show(pctInformation)
    'End Sub

#End Region

End Class
