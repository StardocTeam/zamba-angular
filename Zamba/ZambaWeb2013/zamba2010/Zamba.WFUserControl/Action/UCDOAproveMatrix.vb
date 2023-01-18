'Imports Zamba.WFBusiness
Imports Zamba.Data

Public Class UCDOApproveMatrix
    Inherits ZRuleControl

    Public Sub New(ByRef CurrentRule As IDOApproveMatrix, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub

    Private Sub this_Load()
        Try
            CargarEntidades()

            Me.TextoInteligenteApprover.Text = MyRule.ApproverVariable
            Me.TextoInteligenteLevel.Text = MyRule.LevelVariable
            Me.TextoInteligenteSecuence.Text = MyRule.SecuenceVariable

            Me.TextoInteligenteVariable1.Text = MyRule.OutputVariable1
            Me.TextoInteligenteVariable2.Text = MyRule.OutputVariable2
            Me.TextoInteligenteVariable3.Text = MyRule.OutputVariable3

            Me.TextoInteligenteActions.Text = MyRule.RegistryActions

            Me.TextoInteligenteIsApproverVariable.Text = MyRule.IsApproverVariable
            Me.TextoInteligenteApproversListVariable.Text = MyRule.ApproversListVariable

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Sub BTNADD_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BTNADD.Click
        Save()
    End Sub
    Private Sub Save()
        Try
            WFRulesBusiness.UpdateParamItem(MyRule, 0, CBOEntities.SelectedValue)

            WFRulesBusiness.UpdateParamItem(MyRule, 1, DirectCast(CBOAmount.SelectedItem, Index).ID)
            WFRulesBusiness.UpdateParamItem(MyRule, 2, DirectCast(CBOSecuence.SelectedItem, Index).ID)
            WFRulesBusiness.UpdateParamItem(MyRule, 3, DirectCast(CBOLevel.SelectedItem, Index).ID)

            WFRulesBusiness.UpdateParamItem(MyRule, 4, Me.TextoInteligenteApprover.Text)
            WFRulesBusiness.UpdateParamItem(MyRule, 5, Me.TextoInteligenteSecuence.Text)
            WFRulesBusiness.UpdateParamItem(MyRule, 6, Me.TextoInteligenteLevel.Text)

            If CBOIndexs1.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 7, DirectCast(CBOIndexs1.SelectedItem, Index).ID)
            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 7, -1)
            End If
            If CBOIndexs2.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 8, DirectCast(CBOIndexs2.SelectedItem, Index).ID)

            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 8, -1)
            End If
            If CBOIndexs3.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 9, DirectCast(CBOIndexs3.SelectedItem, Index).ID)

            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 9, -1)
            End If
            WFRulesBusiness.UpdateParamItem(MyRule, 10, Me.TextoInteligenteVariable1.Text)
            WFRulesBusiness.UpdateParamItem(MyRule, 11, Me.TextoInteligenteVariable2.Text)
            WFRulesBusiness.UpdateParamItem(MyRule, 12, Me.TextoInteligenteVariable3.Text)

            If CBORegisterEntity.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 13, CBORegisterEntity.SelectedValue)

            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 13, -1)
            End If
            If CBOActionIndex.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 14, DirectCast(CBOActionIndex.SelectedItem, Index).ID)

            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 14, -1)
            End If
            If CBORegistryIdIndex.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 15, DirectCast(CBORegistryIdIndex.SelectedItem, Index).ID)

            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 15, -1)
            End If
            WFRulesBusiness.UpdateParamItem(MyRule, 16, Me.TextoInteligenteActions.Text)

            If CBOApprover.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 17, DirectCast(CBOApprover.SelectedItem, Index).ID)

            Else
                WFRulesBusiness.UpdateParamItem(MyRule, 17, -1)
            End If

            WFRulesBusiness.UpdateParamItem(MyRule, 18, Me.TextoInteligenteIsApproverVariable.Text)
            WFRulesBusiness.UpdateParamItem(MyRule, 19, Me.TextoInteligenteApproversListVariable.Text)

            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CargarEntidades()
        'zamba.Core.DocTypesFactory.GetDocTypes()
        RemoveHandler CBOEntities.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged
        Try
            Me.CBOEntities.DataSource = DocTypesFactory.GetDocTypesDataSet().Tables(0)
            Me.CBOEntities.DisplayMember = "Doc_Type_Name"
            Me.CBOEntities.ValueMember = "Doc_Type_ID"

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        RemoveHandler CBOEntities.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged
        AddHandler CBOEntities.SelectedIndexChanged, AddressOf CBODoctypes_SelectedIndexChanged

        If MyRule.MatrixEntityId > 0 Then
            Me.CBOEntities.SelectedValue = MyRule.MatrixEntityId
        End If



        RemoveHandler CBORegisterEntity.SelectedIndexChanged, AddressOf CBOCBORegisterEntity_SelectedIndexChanged
        Try
            Me.CBORegisterEntity.DataSource = DocTypesFactory.GetDocTypesDataSet().Tables(0)
            Me.CBORegisterEntity.DisplayMember = "Doc_Type_Name"
            Me.CBORegisterEntity.ValueMember = "Doc_Type_ID"

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        RemoveHandler CBORegisterEntity.SelectedIndexChanged, AddressOf CBOCBORegisterEntity_SelectedIndexChanged
        AddHandler CBORegisterEntity.SelectedIndexChanged, AddressOf CBOCBORegisterEntity_SelectedIndexChanged

        If MyRule.RegistryEntityId > 0 Then
            Me.CBORegisterEntity.SelectedValue = MyRule.RegistryEntityId
        End If
    End Sub


    Private Sub CBODoctypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBOEntities.SelectedIndexChanged
        FillIndexsCombos(CBOEntities, CBOIndexs1, MyRule.OutputIndex1)
        FillIndexsCombos(CBOEntities, CBOIndexs2, MyRule.OutputIndex2)
        FillIndexsCombos(CBOEntities, CBOIndexs3, MyRule.OutputIndex3)
        FillIndexsCombos(CBOEntities, CBOAmount, MyRule.AmountIndex)
        FillIndexsCombos(CBOEntities, CBOSecuence, MyRule.SecuenceIndex)
        FillIndexsCombos(CBOEntities, CBOLevel, MyRule.LevelIndex)
        FillIndexsCombos(CBOEntities, CBOApprover, MyRule.ApproverIndex)
    End Sub

    Private Sub CBOCBORegisterEntity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBORegisterEntity.SelectedIndexChanged
        FillIndexsCombos(CBORegisterEntity, CBORegistryIdIndex, MyRule.RegistryIdIndex)
        FillIndexsCombos(CBORegisterEntity, CBOActionIndex, MyRule.RegistryActionIndex)
    End Sub


    Private Sub FillIndexsCombos(CBOEntities As ComboBox, CBOIndexs As ComboBox, currentindexId As Int64)
        Try
            Dim TD As DataSet = IndexsBusiness.GetIndexSchemaAsDataSet(CBOEntities.SelectedValue)

            'Asigno el displaymember 
            CBOIndexs.Text = ""
            CBOIndexs.Items.Clear()
            CBOIndexs.DisplayMember = "Name"
            CBOIndexs.ValueMember = "ID"

            Dim IndexIdMatch As Boolean
            'Cargo los atributos
            For Each r As DataRow In TD.Tables(0).Rows
                Dim Ind = New Index()
                Ind.ID = r.Item(0)
                Ind.Name = r.Item(1)
                Ind.Type = r.Item(2)
                CBOIndexs.Items.Add(Ind)

                If Ind.id = currentindexId Then
                    IndexIdMatch = True
                    CBOIndexs.SelectedValue = currentindexId
                    CBOIndexs.SelectedText = Ind.Name
                    CBOIndexs.SelectedItem = Ind

                End If

            Next

            If IndexIdMatch = True Then
                CBOIndexs.SelectedValue = currentindexId
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDOApproveMatrix
        Get
            Return DirectCast(Rule, IDOApproveMatrix)
        End Get
    End Property

    Private Sub UCDOApproveMatrix_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
