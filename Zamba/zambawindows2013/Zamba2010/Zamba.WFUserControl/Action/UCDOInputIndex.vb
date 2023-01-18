'Imports Zamba.WFBusiness
Public Class UCDOInputIndex
    Inherits ZRuleControl

    Public Sub New(ByRef CurrentRule As IDOInputIndex, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub

    Private Sub this_Load()
        Try
            CargarIndices()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BTNADD_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BTNADD.Click
        GuardarIndices()
    End Sub
    Private Sub CargarIndices()
        CBOIndexs.DataSource = IndexsBusiness.GetIndex.Tables(0)
        CBOIndexs.DisplayMember = "INDEX_NAME"
        CBOIndexs.ValueMember = "INDEX_ID"
        CBOIndexs.SelectedValue = MyRule.Index
    End Sub
    Private Sub GuardarIndices()
        Try
            Dim index As Int64
            ' Dim doctype As Int32
            If CBOIndexs.SelectedIndex > -1 Then
                index = CBOIndexs.SelectedValue
                'doctype = Me.CBODoctypes.SelectedValue
                WFRulesBusiness.UpdateParamItem(MyRule, 1, index)
                WFRulesBusiness.UpdateParamItem(MyRule, 0, 0)
                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
                MyRule.Index = index
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    'Private Sub CargarDocTypes()
    '    'zamba.Core.DocTypesFactory.GetDocTypes()
    '    RemoveHandler CBODoctypes.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged
    '    Try
    '        Me.CBODoctypes.DataSource = Zamba.Core.DocTypesFactory.GetDocTypesDataset().Tables(0)
    '        Me.CBODoctypes.DisplayMember = "Doc_Type_Name"
    '        Me.CBODoctypes.ValueMember = "Doc_Type_ID"

    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    RemoveHandler CBODoctypes.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged
    '    AddHandler CBODoctypes.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged

    '    If MyRule.DocTypeId > 0 Then
    '        Me.CBODoctypes.SelectedValue = MyRule.DocTypeId
    '    End If
    'End Sub


    'Private Sub CBODoctypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBODoctypes.SelectedIndexChanged
    '    Try
    '        Me.CBOIndexs.DataSource = Zamba.Core.DocTypesFactory.GetIndexs(Zamba.Core.DocTypesFactory.GetDocType(Convert.ToInt32(DirectCast(sender, ComboBox).SelectedValue))).Tables(0)
    '        Me.CBOIndexs.DisplayMember = "Index_Name"
    '        Me.CBOIndexs.ValueMember = "Index_Id"
    '        Me.CBOIndexs.SelectedValue = MyRule.Index
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

    Public Shadows ReadOnly Property MyRule() As IDOInputIndex
        Get
            Return DirectCast(Rule, IDOInputIndex)
        End Get
    End Property
End Class
