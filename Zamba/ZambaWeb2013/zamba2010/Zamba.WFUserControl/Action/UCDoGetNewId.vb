'Imports Zamba.WFBusiness
Imports Zamba.Data

Public Class UCDoGetNewId
    Inherits ZRuleControl


    Public Sub New(ByRef CurrentRule As IDoGetNewId, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub

    Private Sub this_Load()
        Try
            CargarDocTypes()
            CargarIndices()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BTNADD_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BTNADD.Click
        CargarIndices()
    End Sub

    Private Sub CargarIndices()
        Try
            Dim index As Int64
            Dim doctype As Int64
            If CBOIndexs.SelectedIndex > -1 Then
                index = CBOIndexs.SelectedValue
                doctype = CBODoctypes.SelectedValue
                WFRulesBusiness.UpdateParamItem(MyRule, 1, index)
                WFRulesBusiness.UpdateParamItem(MyRule, 0, doctype)
                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
                MyRule.IndexId = index
                MyRule.DocTypeId = doctype
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CargarDocTypes()
        'zamba.Core.DocTypesFactory.GetDocTypes()
        RemoveHandler CBODoctypes.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged
        Try
            CBODoctypes.DataSource = DocTypesFactory.GetDocTypesDsDocType().Tables(0)
            CBODoctypes.DisplayMember = "Doc_Type_Name"
            CBODoctypes.ValueMember = "Doc_Type_ID"

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        RemoveHandler CBODoctypes.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged
        AddHandler CBODoctypes.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged

        If MyRule.DocTypeId > 0 Then
            CBODoctypes.SelectedValue = MyRule.DocTypeId
        End If
    End Sub


    Private Sub CBODoctypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CBODoctypes.SelectedIndexChanged
        Try
            CBOIndexs.DataSource = IndexsBusiness.GetIndexSchemaAsDataSet(Convert.ToInt64(DirectCast(sender, ComboBox).SelectedValue)).Tables(0)
            CBOIndexs.DisplayMember = "Index_Name"
            CBOIndexs.ValueMember = "Index_Id"
            CBOIndexs.SelectedValue = MyRule.IndexId
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoGetNewId
        Get
            Return DirectCast(Rule, IDoGetNewId)
        End Get
    End Property
End Class
