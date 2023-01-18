'Imports Zamba.WFBusiness

Public Class UCDOAddToFolder
    Inherits ZRuleControl

    'Regla que se va a configurar
    Dim CurrentRule As IDOADDTOFOLDER

    Public Sub New(ByRef CurrentRule As IDOADDTOFOLDER, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule
        fillView()
    End Sub

    Protected Sub fillView()
    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, _
 ByVal e As EventArgs) _
 Handles btnSeleccionar.Click
        'WFRulesBusiness.UpdateParamItem(CurrentRule, 0, ??? )
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDOADDTOFOLDER
        Get
            Return DirectCast(Rule, IDOADDTOFOLDER)
        End Get
    End Property

    'Clase item de la lista...
    Private Class StepItem
        Inherits ListViewItem
        Public doAddToFolder As IDOADDTOFOLDER
        Private _SelectedStep As Boolean
        Private _SelectedCheck As Boolean

        Public Property SelectedStep() As Boolean
            Get
                Return _SelectedStep
            End Get
            Set(ByVal Value As Boolean)
                _SelectedStep = Value
                If Value = 0 Then
                    Font = _
                    New Font("Tahoma", _
                    9.0!, _
                    FontStyle.Regular, _
                    GraphicsUnit.Point, _
                    CType(0, Byte))
                Else
                    Font = _
                    New Font("Tahoma", _
                    9.75!, _
                    FontStyle.Bold, _
                    GraphicsUnit.Point, _
                    CType(0, Byte))
                End If
            End Set
        End Property

        Sub New(ByVal doAddToFolder As IDOADDTOFOLDER, ByVal SelectedStep As Boolean)
            Me.doAddToFolder = doAddToFolder
            'Me.Text = doAddToFolder.Name
            Me.SelectedStep = SelectedStep
        End Sub
    End Class

    Private Sub tbRule_Click(sender As Object, e As EventArgs) Handles tbRule.Click

    End Sub
End Class
