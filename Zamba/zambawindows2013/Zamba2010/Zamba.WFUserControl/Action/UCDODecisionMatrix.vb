'Imports Zamba.WFBusiness
Imports Zamba.Data

Public Class UCDODecisionMatrix
    Inherits ZRuleControl

    Public Sub New(ByRef CurrentRule As IDODecisionMatrix, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub

    Private Sub this_Load()
        Try
            CargarEntidades()
            Me.TextoInteligenteTextBox1.Text = MyRule.OutputVariable
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Sub BTNADD_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BTNADD.Click
        Save()
    End Sub
    Private Sub Save()
        Try
            If CBOIndexs.SelectedIndex > -1 Then
                WFRulesBusiness.UpdateParamItem(MyRule, 0, CBOEntities.SelectedValue)
                WFRulesBusiness.UpdateParamItem(MyRule, 1, DirectCast(CBOIndexs.SelectedItem, Index).ID)
                WFRulesBusiness.UpdateParamItem(MyRule, 2, Me.TextoInteligenteTextBox1.Text)
                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
            End If
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

        If MyRule.EntityId > 0 Then
            Me.CBOEntities.SelectedValue = MyRule.EntityId
        End If
    End Sub


    Private Sub CBODoctypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBOEntities.SelectedIndexChanged
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
                If Ind.id = MyRule.OutputIndex Then
                    IndexIdMatch = True
                    Me.CBOIndexs.SelectedValue = MyRule.OutputIndex
                    Me.CBOIndexs.SelectedText = Ind.Name
                    Me.CBOIndexs.SelectedItem = Ind

                End If

                'If Ind.id = MyRule.AltOutputIndex Then
                '    IndexIdMatch = True
                '    Me.CBOIndexsAlt.SelectedValue = MyRule.AltOutputIndex
                '    Me.CBOIndexsAlt.SelectedText = Ind.Name
                '    Me.CBOIndexsAlt.SelectedItem = Ind

                'End If
            Next

            If IndexIdMatch = True Then
                Me.CBOIndexs.SelectedValue = MyRule.OutputIndex
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDODecisionMatrix
        Get
            Return DirectCast(Rule, IDODecisionMatrix)
        End Get
    End Property


End Class
