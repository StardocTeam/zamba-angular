Public Class UCDoAutoName
    Inherits ZRuleControl

    Private Const _STRSELECTED As String = "Seleccionados: "
    Private CurrentRule As IDoAutoName
    Private WFRulesBusiness As WFRulesBusiness
    Private docTypesBusiness As DocTypesBusiness
    Private selected As Int32 = 0

    ''' <summary>
    ''' se modifico el constructor para que se pueda cargar los valores de la regla en el administrador
    ''' </summary>
    ''' <param name="CurrentRule"></param>
    ''' <remarks>sebastian 12/12/2008</remarks>
    Public Sub New(ByVal CurrentRule As IDoAutoName, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        WFRulesBusiness = New WFRulesBusiness()
        docTypesBusiness = New DocTypesBusiness()
        Me.CurrentRule = CurrentRule

        'Ubico el panel multiple como el individual
        pnlMultiple.Location = pnlIndividual.Location

        'Se cargan las opciones
        txtVariableDocId.Text = CurrentRule.variabledocid
        TxtVariableDocTypeId.Text = CurrentRule.variabledoctypeid
        TxtVariableNombreDeLaColumna.Text = CurrentRule.nombreColumna
        txtDays.Text = CurrentRule.days

        If (CurrentRule.Seleccion = "Seleccion Actual") Then
            RdbUsarActual.Checked = True
        End If

        If (CurrentRule.Seleccion = "Seleccion por Id") Then
            RdbEspecificarId.Checked = True
        End If

        If CurrentRule.updateMultiple Then
            rdoMultiple.Checked = True
        Else
            rdoIndividual.Checked = True
        End If

        LoadDocTypesList()

    End Sub


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

        If RdbUsarActual.Checked Then
            CurrentRule.Seleccion = "Seleccion Actual"
        End If

        If RdbEspecificarId.Checked Then

            CurrentRule.Seleccion = "Seleccion por Id"
        End If

        CurrentRule.updateMultiple = rdoMultiple.Checked


        CurrentRule.variabledocid = txtVariableDocId.Text
        CurrentRule.variabledoctypeid = TxtVariableDocTypeId.Text
        CurrentRule.nombreColumna = TxtVariableNombreDeLaColumna.Text
        CurrentRule.docTypeIds = GetCheckedDocTypesIds()
        CurrentRule.days = txtDays.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Seleccion)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.variabledocid)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.variabledoctypeid)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.nombreColumna)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.updateMultiple.ToString)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.docTypeIds)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.days)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
    End Sub

    Private Sub rdoIndividual_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdoIndividual.CheckedChanged
        If rdoIndividual.Checked Then
            pnlMultiple.Visible = False
            pnlIndividual.Visible = True
        Else
            pnlMultiple.Visible = True
            pnlIndividual.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Carga los DocTypes en la lista con sus valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocTypesList()
        'Se cargan los doctypes
        For Each dr As DataRow In docTypesBusiness.GetDocTypeNamesAndIds().Rows
            chkDocTypeList.Items.Add(dr(0).ToString & " " & dr(1).ToString)
        Next

        'Se seleccionan los configurados
        Dim i As Int32

        For Each id As String In CurrentRule.docTypeIds.Split(New Char() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To chkDocTypeList.Items.Count - 1
                If chkDocTypeList.Items(i).ToString.Contains(id & " ") Then
                    chkDocTypeList.SetItemChecked(i, True)
                    selected += 1
                    Exit For
                End If
            Next
        Next

        'Ahora que temrino de cargar la lista, agrego el handler que maneja el contador de seleccionados.
        AddHandler chkDocTypeList.ItemCheck, AddressOf chkDocTypeList_ItemCheck

        'Actualiza la cantidad de seleccionados.
        SetCountLabel(selected)

    End Sub

    ''' <summary>
    ''' Obtiene los doctypeids seleccionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCheckedDocTypesIds() As String
        Dim dtids As String = String.Empty
        Dim temp As String
        Dim i As Int32

        For i = 0 To chkDocTypeList.Items.Count - 1
            If chkDocTypeList.GetItemChecked(i) Then
                temp = chkDocTypeList.Items(i).ToString
                dtids &= temp.Remove(temp.IndexOf(" ")) & ","
            End If
        Next
        If String.IsNullOrEmpty(dtids) Then
            Return String.Empty
        Else
            Return dtids.Remove(dtids.LastIndexOf(","))
        End If
    End Function

    ''' <summary>
    ''' Modifica el texto de items seleccionados
    ''' </summary>
    ''' <param name="count"></param>
    ''' <remarks></remarks>
    Private Sub SetCountLabel(ByVal count As Int32)
        lblSelected.Text = _STRSELECTED & count.ToString & "/" & chkDocTypeList.Items.Count.ToString
    End Sub

    ''' <summary>
    ''' Actualiza la cantidad de items seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkDocTypeList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs)
        Select Case e.NewValue
            Case CheckState.Checked
                selected += 1
                SetCountLabel(selected)
            Case CheckState.Unchecked
                selected -= 1
                SetCountLabel(selected)
        End Select
    End Sub
End Class