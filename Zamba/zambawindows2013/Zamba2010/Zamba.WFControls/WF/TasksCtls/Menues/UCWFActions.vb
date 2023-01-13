Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Core.WF.WF

'Imports Zamba.WFBusiness

Public Class UCWFActions
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PanelActions As Zamba.AppBlock.ZColorPanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PanelActions = New Zamba.AppBlock.ZColorPanel
        Me.Label2 = New System.Windows.Forms.Label
        Me.PanelActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelActions
        '
        Me.PanelActions.BackColor = System.Drawing.SystemColors.Control
        Me.PanelActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelActions.Color1 = System.Drawing.Color.White
        Me.PanelActions.Color2 = System.Drawing.Color.FromArgb(CType(231, Byte), CType(231, Byte), CType(239, Byte))
        Me.PanelActions.Controls.Add(Me.Label2)
        Me.PanelActions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelActions.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical
        Me.PanelActions.Location = New System.Drawing.Point(0, 0)
        Me.PanelActions.Name = "PanelActions"
        Me.PanelActions.Size = New System.Drawing.Size(176, 56)
        Me.PanelActions.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(152, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "X"
        '
        'UCActions
        '
        Me.Controls.Add(Me.PanelActions)
        Me.Name = "UCWFActions"
        Me.Size = New System.Drawing.Size(176, 56)
        Me.PanelActions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim _Result As TaskResult
    Public Sub New(ByVal _Result As TaskResult)
        MyBase.New()
        InitializeComponent()
        Try
            Me._Result = _Result
            Dim i As Int32
            If WFTaskBusiness.LockTask(_Result.TaskId, _Result.StepId, TaskStates.Ejecucion) Then

                Dim wfstep As IWFStep = WFStepBusiness.GetStepById(_Result.StepId, False)

                Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                DVDSRules.RowFilter = "Type = 5"
                For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                    Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                    Dim LinkAction As New RuleActionLink(Rule)
                    RemoveHandler LinkAction.LinkClicked, AddressOf lnkActions_LinkClicked
                    AddHandler LinkAction.LinkClicked, AddressOf lnkActions_LinkClicked
                    Me.PanelActions.Controls.Add(LinkAction)
                    LinkAction.Location = New Point(4, 12 + 20 * i)
                    LinkAction.SendToBack()
                    LinkAction.AutoSize = True
                    Me.Height += 20
                    i += 1
                Next
            Else
                ShowExecutedByOtherMessage()
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If _Result IsNot Nothing Then WFTaskBusiness.UnLockTask(_Result)
        End Try

    End Sub
    Private Sub lnkActions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            If WFTaskBusiness.LockTask(_Result.TaskId, _Result.StepId, TaskStates.Ejecucion) Then

                Dim Results As New System.Collections.Generic.List(Of ITaskResult)
                Results.Add(_Result)
                Dim WFRB As New WFRulesBusiness
                Dim list As List(Of ITaskResult) = WFRB.ExecutePrimaryRule(DirectCast(sender, RuleActionLink).Rule, Results, Nothing)
                list = Nothing
                Results = Nothing
                WFRB = Nothing
            Else
                ShowExecutedByOtherMessage()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If _Result IsNot Nothing Then WFTaskBusiness.UnLockTask(_Result)
        End Try

    End Sub

#Region "Class RuleActionLink"
    Private Class RuleActionLink
        Inherits LinkLabel
        Public Rule As WFRuleParent
        Sub New(ByRef rule As WFRuleParent)
            MyBase.New()
            Me.Rule = rule
            Me.Text = rule.Name

            'Link Properties
            Me.ActiveLinkColor = System.Drawing.Color.SlateGray
            Me.BackColor = System.Drawing.Color.Transparent
            Me.Cursor = System.Windows.Forms.Cursors.Hand
            Me.DisabledLinkColor = System.Drawing.Color.LightBlue
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ForeColor = System.Drawing.Color.MidnightBlue
            Me.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
            Me.LinkColor = System.Drawing.Color.MidnightBlue
            Me.Size = New System.Drawing.Size(552, 16)
            Me.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        End Sub
    End Class
#End Region

#Region "Close"
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        Try
            Me.Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UcActions_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Try
            Me.Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
