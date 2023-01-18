Imports System.Text
Imports System.Net.Mail
Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Controls
Imports Zamba.Data

Imports System.Windows.Forms
Imports Zamba.AdminControls

Public Class UCDOShowXoml
    Inherits ZRuleControl
    Friend WithEvents WorkFlowDesignerControl1 As Zamba.WorkFlow.Designer.WorkFlowDesignerControl
    Private CurrentRule As IDOShowXoml

#Region " Código generado por el Diseñador de Windows Forms "

    <System.Diagnostics.DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Me.WorkFlowDesignerControl1 = New Zamba.WorkFlow.Designer.WorkFlowDesignerControl
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.WorkFlowDesignerControl1)
        Me.tbRule.Size = New System.Drawing.Size(698, 528)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(706, 554)
        '
        'WorkFlowDesignerControl1
        '
        Me.WorkFlowDesignerControl1.BackColor = System.Drawing.Color.White
        Me.WorkFlowDesignerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkFlowDesignerControl1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.WorkFlowDesignerControl1.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
        Me.WorkFlowDesignerControl1.Location = New System.Drawing.Point(3, 3)
        Me.WorkFlowDesignerControl1.Name = "WorkFlowDesignerControl1"
        Me.WorkFlowDesignerControl1.Size = New System.Drawing.Size(692, 522)
        Me.WorkFlowDesignerControl1.TabIndex = 0
        '
        'UCDOShowXoml
        '
        Me.Name = "UCDOShowXoml"
        Me.Size = New System.Drawing.Size(706, 554)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Sub New(ByRef rule As IDOShowXoml, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule
        Me.WorkFlowDesignerControl1.showToolbar(False)
    End Sub
End Class