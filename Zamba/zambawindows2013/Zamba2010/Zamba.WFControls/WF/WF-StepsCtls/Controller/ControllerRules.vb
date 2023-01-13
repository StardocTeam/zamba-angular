Imports Telerik.WinControls.UI
Imports Zamba.Core
Public Class ControllerRules
    Inherits ZControl

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
    Friend WithEvents TreeView1 As RadTreeView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TreeView1 = New RadTreeView
        SuspendLayout()
        '
        'TreeView1
        '
        TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TreeView1.BackColor = System.Drawing.Color.White
        'Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        TreeView1.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TreeView1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        TreeView1.FullRowSelect = True
        ' Me.TreeView1.Indent = 32
        TreeView1.ItemHeight = 24
        TreeView1.Location = New System.Drawing.Point(40, 20)
        TreeView1.Name = "TreeView1"
        ' Me.TreeView1.ShowPlusMinus = False
        TreeView1.Size = New System.Drawing.Size(284, 252)
        TreeView1.TabIndex = 0
        TreeView1.AllowDrop = True
        TreeView1.FullRowSelect = True
        TreeView1.HideSelection = False
        TreeView1.HotTracking = True
        TreeView1.Location = New System.Drawing.Point(0, 0)
        TreeView1.Margin = New System.Windows.Forms.Padding(0)
        TreeView1.ShowLines = False
        TreeView1.ShowNodeToolTips = True
        TreeView1.ShowRootLines = False
        TreeView1.ItemHeight = 22
        TreeView1.ExpandAnimation = ExpandAnimation.None
        TreeView1.AllowPlusMinusAnimation = False
        TreeView1.PlusMinusAnimationStep = 1
        TreeView1.TreeViewElement.AutoSizeItems = Telerik.WinControls.Enumerations.ToggleState.On
        TreeView1.TreeViewElement.AllowAlternatingRowColor = Telerik.WinControls.Enumerations.ToggleState.Off
        TreeView1.FullRowSelect = Telerik.WinControls.Enumerations.ToggleState.On
        TreeView1.ShowExpandCollapse = Telerik.WinControls.Enumerations.ToggleState.On
        TreeView1.ShowRootLines = Telerik.WinControls.Enumerations.ToggleState.Off
        TreeView1.ShowLines = Telerik.WinControls.Enumerations.ToggleState.Off
        TreeView1.EnableTheming = True
        TreeView1.ThemeName = "TelerikMetroBlue"
        TreeView1.TreeIndent = 18

        '
        'ControllerRules
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(TreeView1)
        Name = "ControllerRules"
        Size = New System.Drawing.Size(364, 292)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Public Sub ShowRules(ByRef wfstep As WFStep)
        Try
            WFRulesBusiness.LoadMonitorRules(wfstep, TreeView1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

End Class
